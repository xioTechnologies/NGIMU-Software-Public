using System;
using System.Threading;
using Rug.Osc;

namespace NgimuApi
{
    /// <summary>
    /// Asynchronous process.
    /// </summary>
    public sealed class CommandProcess : ICommunicationProcess
    {
        #region Private Members

        /// <summary>
        /// All the callbacks in this process.
        /// </summary>
        protected readonly CommandCallback commandCallback;

        private readonly ManualResetEvent commandCallbackComplete = new ManualResetEvent(true);
        private object syncLock = new object();

        private bool shouldExit = true;
        private Thread commandThread;

        #endregion Private Members

        #region Public Members

        /// <summary>
        /// Gets the connection to be used.
        /// </summary>
        public Connection Connection { get; private set; }

        /// <summary>
        /// True if the process is running.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Gets the reporter for the process.
        /// </summary>
        public IReporter Reporter { get; protected set; }

        /// <summary>
        /// Gets the result of the asynchronous process.
        /// </summary>
        public CommunicationProcessResult Result { get; protected set; }

        /// <summary>
        /// Gets the maximum number of retries before failure.
        /// </summary>
        public int RetryLimit { get; set; }

        /// <summary>
        /// Gets the time out in milliseconds between each process iteration.
        /// </summary>
        public int Timeout { get; set; }

        public string CommandOscAddress { get { return commandCallback.OscAddress; } }

        #endregion Public Members

        /// <summary>
        /// Create an asynchronous process.
        /// </summary>
        /// <param name="connection">An Connection to be used for communication.</param>
        /// <param name="reporter">Progress reporter.</param>
        /// <param name="callback">Callback callback to be processed.</param>
        /// <param name="timeout">Time out in milliseconds between each process iteration.</param>
        /// <param name="retryLimit">The maximum number of retries before failure.</param>
        public CommandProcess(Connection connection, IReporter reporter, CommandCallback callback, int timeout = 100, int retryLimit = 3)
        {
            RetryLimit = retryLimit;
            Timeout = timeout;

            Connection = connection;

            Reporter = reporter;

            commandCallback = callback;
        }

        /// <summary>
        /// Process a single command synchronously. Note: This method will block until the process is complete.
        /// </summary>
        /// <param name="connection">The connection to use.</param>
        /// <param name="reporter">Progress reporter.</param>
        /// <param name="message">Command OSC message.</param>
        /// <returns>The result of the process.</returns>
        public static CommunicationProcessResult Send(Connection connection, IReporter reporter, OscMessage message)
        {
            CommandProcess process = new CommandProcess(connection, reporter, new CommandCallback(message));

            return process.Send();
        }

        /// <summary>
        /// Process a single command synchronously. Note: This method will block until the process is complete.
        /// </summary>
        /// <param name="connection">The connection to use</param>
        /// <param name="reporter">Progress reporter.</param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <param name="retryLimit">The number of retries to try before aborting.</param>
        /// <param name="message">Command OSC message.</param>
        /// <returns>The result of the process.</returns>
        public static CommunicationProcessResult Send(Connection connection, IReporter reporter, int timeout, int retryLimit, OscMessage message)
        {
            CommandProcess process = new CommandProcess(connection, reporter, new CommandCallback(message), timeout, retryLimit);

            return process.Send();
        }

        /// <summary>
        /// Stop asynchronous process as soon as possible.
        /// </summary>
        public void Abort()
        {
            lock (syncLock)
            {
                // if the process is not running then we can abort
                if (IsRunning == false)
                {
                    return;
                }

                // stop the process
                shouldExit = true;

                commandThread?.Join();

                commandThread = null;
            }
        }

        /// <summary>
        /// Run the process synchronised and return the result immediately.
        /// </summary>
        /// <returns>The result of the process</returns>
        public CommunicationProcessResult Send()
        {
            // start
            SendAsync();

            // just wait for the process to complete
            commandThread?.Join();

            // nullify the thread
            commandThread = null;

            // return the result
            return Result;
        }

        /// <summary>
        /// Start the asynchronous process.
        /// </summary>
        public void SendAsync()
        {
            lock (syncLock)
            {
                if (IsRunning == true)
                {
                    throw new Exception(Strings.CommandProcess_AlreadyRunning);
                }

                // mark the process as running
                Result = CommunicationProcessResult.Running;

                shouldExit = false;

                // reset the callback state
                commandCallback.ResetCallbackState();
                commandCallbackComplete.Reset();

                // attach to the connection
                Connection.Attach(commandCallback.OscAddress, commandCallback.OnMessageReceived);
                Connection.Attach(commandCallback.OscAddress, OnCommandMessageReceived);

                // mark as running
                IsRunning = true;

                // create the asynchronous process thread
                commandThread = new Thread(AsynchronousProcessThread);
                commandThread.Name = "Send Command Async " + commandCallback.Message.ToString();

                // start the asynchronous process thread
                commandThread.Start();
            }
        }

        /// <summary>
        /// Run the asynchronous process thread.
        /// </summary>
        private void AsynchronousProcessThread()
        {
            int retryCount = 0;
            int retryLimit = RetryLimit;

            OnInfo(string.Format("Sending command."));

            try
            {
                // while we should not exit and we have callbacks remaining
                while (shouldExit == false)
                {
                    // get the message for the callback
                    OscMessage message = commandCallback.Message;

                    // send the message
                    if (SendMessage(message) == false)
                    {
                        // an error happened at the transport layer
                        Result = CommunicationProcessResult.ConnectionError;

                        return;
                    }

                    // wait for the timeout
                    commandCallbackComplete.WaitOne(Timeout);

                    // check if the callback has completed
                    if (commandCallback.HasCallbackCompleted == true)
                    {
                        OnInfo(string.Format("Command confirmed."));

                        Result = CommunicationProcessResult.Success;

                        return;
                    }

                    // if the retry count is greater or equal to the retry limit
                    if (retryCount >= retryLimit)
                    {
                        // raise the error message
                        OnError("Command could not be confirmed.");

                        if (Result != CommunicationProcessResult.ConnectionError)
                        {
                            // flag the error
                            Result = CommunicationProcessResult.RetryLimitExceeded;
                        }

                        return;
                    }

                    // increment the retry count
                    retryCount++;

                    // raise the current completion info event
                    OnInfo(string.Format("No confirmation after {0} ms timeout. Retry {1} of {2}.", Timeout, retryCount, retryLimit));
                }
            }
            finally
            {
                // detach all the callbacks
                Connection.Detach(commandCallback.OscAddress, commandCallback.OnMessageReceived);
                Connection.Detach(commandCallback.OscAddress, OnCommandMessageReceived);

                // flag that we are
                IsRunning = false;

                // raise the stop event
                OnCompleted();
            }
        }

        private void OnCommandMessageReceived(OscMessage message)
        {
            commandCallbackComplete.Set();
        }

        /// <summary>
        /// Called when the process has stopped.
        /// </summary>
        private void OnCompleted()
        {
            if (Reporter == null)
            {
                return;
            }

            Reporter.OnCompleted(this, EventArgs.Empty);
        }

        /// <summary>
        /// Called when an error occurs.
        /// </summary>
        /// <param name="message">A textual message.</param>
        private void OnError(string message)
        {
            if (Reporter == null)
            {
                return;
            }

            Reporter.OnError(this, new MessageEventArgs(message));
        }

        /// <summary>
        /// Called when an error occurs.
        /// </summary>
        /// <param name="message">A textual message.</param>
        /// <param name="detail">Additional detail about the error.</param>
        private void OnError(string message, string detail)
        {
            if (Reporter == null)
            {
                return;
            }

            Reporter.OnError(this, new MessageEventArgs(message, detail));
        }

        /// <summary>
        /// Called when there is info to display.
        /// </summary>
        /// <param name="message">A textual message.</param>
        private void OnInfo(string message)
        {
            if (Reporter == null)
            {
                return;
            }

            Reporter.OnInfo(this, new MessageEventArgs(message));
        }

        /// <summary>
        /// Send a OSC message to the device.
        /// </summary>
        /// <param name="message">An OSC device.</param>
        /// <returns>True if the message was sent.</returns>
        private bool SendMessage(OscMessage message)
        {
            try
            {
                Connection.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                if (Reporter != null)
                {
                    Reporter.OnException(this, new ExceptionEventArgs(Strings.CommandProcess_CommunicationError, ex));
                }

                return false;
            }
        }
    }
}