using Rug.Osc;

namespace NgimuApi
{
    /// <summary>
    /// Command process callback.
    /// </summary>
    public sealed class CommandCallback
    {
        /// <summary>
        /// Command callback to be sent.
        /// </summary>
        public OscMessage Message { get; private set; }

        /// <summary>
        /// The address of the callback.
        /// </summary>
        public string OscAddress { get { return Message.Address; } }

        /// <summary>
        /// True if the callback is complete.
        /// </summary>
        public bool HasCallbackCompleted { get; private set; }

        /// <summary>
        /// Gets the message that was returned if there was one.
        /// </summary>
        public OscMessage ReturnMessage { get; private set; }

        /// <summary>
        /// Create a new command callback.
        /// </summary>
        /// <param name="message">The callback to be sent and listened for the response from.</param>
        public CommandCallback(OscMessage message)
        {
            Message = message;
        }

        /// <summary>
        /// Called when a command confirmation callback message is received.
        /// </summary>
        /// <param name="message">A OSC message.</param>
        public void OnMessageReceived(OscMessage message)
        {
            ReturnMessage = message;

            HasCallbackCompleted = true;
        }

        /// <summary>
        /// Reset the callback state to non-complete.
        /// </summary>
        public void ResetCallbackState()
        {
            ReturnMessage = null;

            HasCallbackCompleted = false;
        }
    }
}
