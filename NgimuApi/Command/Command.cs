using System;
using System.Collections.Generic;

namespace NgimuApi
{
    public enum Command
    {
        Time,

        Mute,

        Unmute,

        Reset,

        Sleep,

        Identify,

        Unlock,

        Apply,

        Default,

        Upload,

        AhrsInitialise,

        AhrsZero,

        Echo,

        EepromVersion,

        EepromErase,

        Ahoy,
    }

    /// <summary>
    /// Static collection of commands.
    /// </summary>
    public static class Commands
    {
        private static Dictionary<Command, CommandMetaData> commandLookup = new Dictionary<Command, CommandMetaData>();
        private static List<CommandMetaData> commands = new List<CommandMetaData>();
        private static List<string> commandAddresses = new List<string>();

        static Commands()
        {
            AddCommands();
        }

        private static void AddCommands()
        {
            AddCommand(Command.Time, "Set Date/Time", false, true, "/time", new string[] { "OscTimeTag.Now" });

            AddCommand(Command.Mute, "Mute", false, true, "/mute", new string[0]);

            AddCommand(Command.Unmute, "Unmute", false, true, "/unmute", new string[0]);

            AddCommand(Command.Sleep, "Sleep", true, true, "/sleep", new string[0]);

            AddCommand(Command.Reset, "Reset", true, true, "/reset", new string[0]);

            AddCommand(Command.Identify, "Identify", false, true, "/identify", new string[0]);

            AddCommand(Command.Unlock, "Unlock", false, false, "/unlock", new string[0]);

            AddCommand(Command.Apply, "Apply", false, false, "/apply", new string[0]);

            AddCommand(Command.Default, "Default", false, false, "/default", new string[0]);

            AddCommand(Command.Upload, "Upload", false, false, "/upload", new string[0]);

            AddCommand(Command.AhrsInitialise, "AHRS Initialise", false, true, "/ahrs/initialise", new string[0]);

            AddCommand(Command.AhrsZero, "AHRS Zero Yaw", false, true, "/ahrs/zero", new string[0]);

            AddCommand(Command.Echo, "Echo", false, false, "/echo", new string[0]);

            AddCommand(Command.EepromVersion, "EEPROM Version", false, false, "/eeprom/version", new string[0]);

            AddCommand(Command.EepromErase, "EEPROM Erase", false, false, "/eeprom/erase", new string[0]);

            AddCommand(Command.Ahoy, "Ahoy", false, false, "/ahoy", new string[0]);
        }

        private static void AddCommand(Command command, string name, bool disconnectSerialOnSuccess, bool isVisible, string address, string[] args)
        {
            CommandMetaData imuCommand = new CommandMetaData(name, disconnectSerialOnSuccess, isVisible, address, args);

            commandLookup.Add(command, imuCommand);
            commandAddresses.Add(address);
            commands.Add(imuCommand);
        }

        /// <summary>
        /// Does this collection contain a command with a given OSC address.
        /// </summary>
        /// <param name="oscAddress">The OSC address to check.</param>
        /// <returns>True if a command with the supplied address exists in this collection.</returns>
        public static bool ContainsAddress(string oscAddress)
        {
            return commandAddresses.Contains(oscAddress);
        }


        /// <summary>
        /// Process a single command synchronously. Note: This method will block until the process is complete.
        /// </summary>
        /// <param name="connection">The connection to use.</param>
        /// <param name="command">Command.</param>
        /// <returns>The result of the process.</returns>
        public static CommunicationProcessResult Send(Connection connection, Command command)
        {
            return Send(connection, null, command);
        }

        /// <summary>
        /// Process a single command synchronously. Note: This method will block until the process is complete.
        /// </summary>
        /// <param name="connection">The connection to use.</param>
        /// <param name="command">Command.</param>
        /// <param name="args">OSC message arguments</param>
        /// <returns>The result of the process.</returns>
        public static CommunicationProcessResult Send(Connection connection, Command command, params object[] args)
        {
            return Send(connection, null, command, args);
        }

        /// <summary>
        /// Process a single command synchronously. Note: This method will block until the process is complete.
        /// </summary>
        /// <param name="connection">The connection to use.</param>
        /// <param name="reporter">Progress reporter.</param>
        /// <param name="command">Command.</param>
        /// <returns>The result of the process.</returns>
        public static CommunicationProcessResult Send(Connection connection, IReporter reporter, Command command)
        {
            CommandProcess process = new CommandProcess(connection, reporter, new CommandCallback(commandLookup[command].Message));

            return process.Send();
        }

        /// <summary>
        /// Process a single command synchronously. Note: This method will block until the process is complete.
        /// </summary>
        /// <param name="connection">The connection to use.</param>
        /// <param name="reporter">Progress reporter.</param>
        /// <param name="command">Command.</param>
        /// <param name="args">OSC message arguments</param>
        /// <returns>The result of the process.</returns>
        public static CommunicationProcessResult Send(Connection connection, IReporter reporter, Command command, params object[] args)
        {
            CommandProcess process = new CommandProcess(connection, reporter, new CommandCallback(commandLookup[command].GetMessage(args)));

            return process.Send();
        }

        /// <summary>
        /// Process a single command synchronously. Note: This method will block until the process is complete.
        /// </summary>
        /// <param name="connection">The connection to use.</param>
        /// <param name="command">Command.</param>
        /// <returns>The result of the process.</returns>
        public static CommunicationProcessResult Send<T>(Connection connection, Command command, out T result)
        {
            return Send(connection, null, command, out result);
        }

        /// <summary>
        /// Process a single command synchronously. Note: This method will block until the process is complete.
        /// </summary>
        /// <param name="connection">The connection to use.</param>
        /// <param name="command">Command.</param>
        /// <param name="args">OSC message arguments</param>
        /// <returns>The result of the process.</returns>
        public static CommunicationProcessResult Send<T>(Connection connection, Command command, out T result, params object[] args)
        {
            return Send(connection, null, command, out result, args);
        }

        /// <summary>
        /// Process a single command synchronously. Note: This method will block until the process is complete.
        /// </summary>
        /// <param name="connection">The connection to use.</param>
        /// <param name="reporter">Progress reporter.</param>
        /// <param name="command">Command.</param>
        /// <returns>The result of the process.</returns>
        public static CommunicationProcessResult Send<T>(Connection connection, IReporter reporter, Command command, out T result)
        {
            CommandCallback callback = new CommandCallback(commandLookup[command].Message);

            return SendInner<T>(connection, reporter, callback, out result);
        }

        /// <summary>
        /// Process a single command synchronously. Note: This method will block until the process is complete.
        /// </summary>
        /// <param name="connection">The connection to use.</param>
        /// <param name="reporter">Progress reporter.</param>
        /// <param name="command">Command.</param>
        /// <param name="args">OSC message arguments</param>
        /// <returns>The result of the process.</returns>
        public static CommunicationProcessResult Send<T>(Connection connection, IReporter reporter, Command command, out T result, params object[] args)
        {
            CommandCallback callback = new CommandCallback(commandLookup[command].GetMessage(args));

            return SendInner<T>(connection, reporter, callback, out result);
        }

        private static CommunicationProcessResult SendInner<T>(Connection connection, IReporter reporter, CommandCallback callback, out T result)
        {
            CommandProcess process = new CommandProcess(connection, reporter, callback);

            result = default(T);

            CommunicationProcessResult processResult = process.Send();

            if (processResult == CommunicationProcessResult.Success)
            {
                if (callback.ReturnMessage != null &&
                    callback.ReturnMessage.Count > 0 &&
                    callback.ReturnMessage[0] is T)
                {
                    result = (T)callback.ReturnMessage[0];
                }
                else
                {
                    throw new Exception("Command did not return the expected message. " + callback.ReturnMessage.ToString());
                }
            }

            return processResult;
        }

        public static string GetOscAddress(Command command)
        {
            return commandLookup[command].OscAddress;
        }

        public static CommandMetaData GetCommandMetaData(string oscAddress)
        {
            int index = commandAddresses.IndexOf(oscAddress);

            if (index < 0)
            {
                return null; 
            }

            return commands[index]; 
        }

        public static CommandMetaData GetCommandMetaData(Command command)
        {
            return commandLookup[command];
        }


        /// <summary>
        /// Process a single command synchronously. Note: This method will block until the process is complete.
        /// </summary>
        /// <param name="connection">The connection to use</param>
        /// <param name="command">Command.</param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <param name="retryLimit">The number of retries to try before aborting.</param>
        /// <returns>The result of the process.</returns>
        public static CommunicationProcessResult Send(Connection connection, Command command, int timeout, int retryLimit)
        {
            return Send(connection, null, command, timeout, retryLimit);
        }

        /// <summary>
        /// Process a single command synchronously. Note: This method will block until the process is complete.
        /// </summary>
        /// <param name="connection">The connection to use</param>
        /// <param name="command">Command.</param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <param name="retryLimit">The number of retries to try before aborting.</param>
        /// <param name="args">OSC message arguments</param>
        /// <returns>The result of the process.</returns>
        public static CommunicationProcessResult Send(Connection connection, Command command, int timeout, int retryLimit, params object[] args)
        {
            return Send(connection, null, command, timeout, retryLimit, args);
        }

        /// <summary>
        /// Process a single command synchronously. Note: This method will block until the process is complete.
        /// </summary>
        /// <param name="connection">The connection to use</param>
        /// <param name="reporter">Progress reporter.</param>
        /// <param name="command">Command.</param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <param name="retryLimit">The number of retries to try before aborting.</param>
        /// <returns>The result of the process.</returns>
        public static CommunicationProcessResult Send(Connection connection, IReporter reporter, Command command, int timeout, int retryLimit)
        {
            CommandProcess process = new CommandProcess(connection, reporter, new CommandCallback(commandLookup[command].Message), timeout, retryLimit);

            return process.Send();
        }

        /// <summary>
        /// Process a single command synchronously. Note: This method will block until the process is complete.
        /// </summary>
        /// <param name="connection">The connection to use</param>
        /// <param name="reporter">Progress reporter.</param>
        /// <param name="command">Command.</param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <param name="retryLimit">The number of retries to try before aborting.</param>
        /// <param name="args">OSC message arguments</param>
        /// <returns>The result of the process.</returns>
        public static CommunicationProcessResult Send(Connection connection, IReporter reporter, Command command, int timeout, int retryLimit, params object[] args)
        {
            CommandProcess process = new CommandProcess(connection, reporter, new CommandCallback(commandLookup[command].GetMessage(args)), timeout, retryLimit);

            return process.Send();
        }

        /// <summary>
        /// Process a single command synchronously. Note: This method will block until the process is complete.
        /// </summary>
        /// <param name="connection">The connection to use</param>
        /// <param name="command">Command.</param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <param name="retryLimit">The number of retries to try before aborting.</param>
        /// <returns>The result of the process.</returns>
        public static CommunicationProcessResult Send<T>(Connection connection, Command command, int timeout, int retryLimit, out T result)
        {
            return Send(connection, null, command, timeout, retryLimit, out result);
        }

        /// <summary>
        /// Process a single command synchronously. Note: This method will block until the process is complete.
        /// </summary>
        /// <param name="connection">The connection to use</param>
        /// <param name="command">Command.</param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <param name="retryLimit">The number of retries to try before aborting.</param>
        /// <param name="args">OSC message arguments</param>
        /// <returns>The result of the process.</returns>
        public static CommunicationProcessResult Send<T>(Connection connection, Command command, int timeout, int retryLimit, out T result, params object[] args)
        {
            return Send(connection, null, command, timeout, retryLimit, out result, args);
        }


        /// <summary>
        /// Process a single command synchronously. Note: This method will block until the process is complete.
        /// </summary>
        /// <param name="connection">The connection to use</param>
        /// <param name="reporter">Progress reporter.</param>
        /// <param name="command">Command.</param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <param name="retryLimit">The number of retries to try before aborting.</param>
        /// <returns>The result of the process.</returns>
        public static CommunicationProcessResult Send<T>(Connection connection, IReporter reporter, Command command, int timeout, int retryLimit, out T result)
        {
            CommandCallback callback = new CommandCallback(commandLookup[command].Message);

            return SendInner<T>(connection, reporter, timeout, retryLimit, callback, out result);
        }

        /// <summary>
        /// Process a single command synchronously. Note: This method will block until the process is complete.
        /// </summary>
        /// <param name="connection">The connection to use</param>
        /// <param name="reporter">Progress reporter.</param>
        /// <param name="command">Command.</param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <param name="retryLimit">The number of retries to try before aborting.</param>
        /// <param name="args">OSC message arguments</param>
        /// <returns>The result of the process.</returns>
        public static CommunicationProcessResult Send<T>(Connection connection, IReporter reporter, Command command, int timeout, int retryLimit, out T result, params object[] args)
        {
            CommandCallback callback = new CommandCallback(commandLookup[command].GetMessage(args));

            return SendInner<T>(connection, reporter, timeout, retryLimit, callback, out result);
        }

        private static CommunicationProcessResult SendInner<T>(Connection connection, IReporter reporter, int timeout, int retryLimit, CommandCallback callback, out T result)
        {

            CommandProcess process = new CommandProcess(connection, reporter, callback, timeout, retryLimit);

            result = default(T);

            CommunicationProcessResult processResult = process.Send();

            if (processResult == CommunicationProcessResult.Success)
            {
                if (callback.ReturnMessage != null &&
                    callback.ReturnMessage.Count > 0 &&
                    callback.ReturnMessage[0] is T)
                {
                    result = (T)callback.ReturnMessage[0];
                }
                else
                {
                    throw new Exception("Command did not return the expected message. " + callback.ReturnMessage.ToString());
                }
            }

            return processResult;
        }

        public static CommandEnumerator GetCommands()
        {
            return new CommandEnumerator();
        }

        public sealed class CommandEnumerator : IEnumerable<CommandMetaData>
        {
            public CommandMetaData this[Command command] { get { return commandLookup[command]; } }

            /// <summary>
            /// Gets commands by index.
            /// </summary>
            /// <param name="index">The zero based index of a command.</param>
            /// <returns>A command.</returns>
            public CommandMetaData this[int index] { get { return commands[index]; } }

            /// <summary>
            /// Gets the number of commands in this collection.
            /// </summary>
            public int Count { get { return commands.Count; } }

            /// <summary>
            /// Enumerate all commands in this collection.
            /// </summary>
            /// <returns>An enumerator for all the commands in this collection.</returns>
            public IEnumerator<CommandMetaData> GetEnumerator()
            {
                return commands.GetEnumerator();
            }

            /// <summary>
            /// Enumerate all commands in this collection.
            /// </summary>
            /// <returns>An enumerator for all the commands in this collection.</returns>
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return (commands as System.Collections.IEnumerable).GetEnumerator();
            }
        }
    }
}
