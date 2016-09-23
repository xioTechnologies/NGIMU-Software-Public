using Rug.Osc;

namespace NgimuApi
{
    /// <summary>
    /// A single command to be sent to the imu and confirmed by an asynchronous process.
    /// </summary>
    public sealed class CommandMetaData
    {
        /// <summary>
        /// Get the friendly name of the command.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the menu text for the command.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Is the command visible in the GUI.
        /// </summary>
        public bool IsVisible { get; private set; }

        /// <summary>
        /// True if the connection should be disconnected upon the successful completion of the command. 
        /// </summary>
        public bool DisconnectSerialOnSuccess { get; private set; }

        /// <summary>
        /// Gets the OSC address string for the command.
        /// </summary>
        public string OscAddress { get; private set; }

        /// <summary>
        /// Gets the OSC message arguments to supply when executing the command.
        /// </summary>
        public string[] Arguments { get; private set; }

        /// <summary>
        /// Get the commands OSC message. 
        /// </summary>
        public OscMessage Message
        {
            get
            {
                if (Arguments.Length == 0)
                {
                    return new OscMessage(OscAddress);
                }

                object[] objs = new object[Arguments.Length];

                for (int i = 0; i < Arguments.Length; i++)
                {
                    if ("OscTimeTag.Now".Equals(Arguments[i]) == true)
                    {
                        objs[i] = OscTimeTag.Now;
                    }
                    else if ("OscTimeTag.UtcNow".Equals(Arguments[i]) == true)
                    {
                        objs[i] = OscTimeTag.UtcNow;
                    }
                    else
                    {
                        objs[i] = OscHelper.ParseArgument(Arguments[i]);
                    }
                }

                return new OscMessage(OscAddress, objs);
            }
        }

        /// <summary>
        /// Creates a IMU command instance.
        /// </summary>
        /// <param name="text">The text to use in menus.</param>
        /// <param name="shortcut">The shortcut keys.</param>
        /// <param name="confirm">Should the GUI confirm this command.</param>
        /// <param name="confirmMessage">The message to be displayed when confirming.</param>
        /// <param name="disconnectSerialOnSuccess">True if the connection should be disconnected upon the successful completion of the command.</param>
        /// <param name="isVisible">Is the command visible in the GUI.</param>
        /// <param name="address">The OSC address of the command.</param>
        /// <param name="arguments">An array of arguments to send with the command. Arguments will be parsed as if part of an OSC message string.</param>
        internal CommandMetaData(string text, bool disconnectSerialOnSuccess, bool isVisible, string address, string[] arguments)
        {
            Name = text.Replace("&", "");
            Text = text;

            DisconnectSerialOnSuccess = disconnectSerialOnSuccess;

            IsVisible = isVisible;

            OscAddress = address;
            Arguments = arguments;
        }

        /// <summary>
        /// Get a command OscMessages with the arguments replaced with the supplied ones.
        /// </summary>
        /// <param name="args">Osc arguments.</param>
        /// <returns>A Osc Message.</returns>
        public OscMessage GetMessage(params object[] args)
        {
            return new OscMessage(OscAddress, args);
        }
    }
}
