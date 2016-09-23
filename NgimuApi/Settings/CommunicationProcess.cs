using System;
using System.Collections.Generic;
using System.Threading;
using Rug.Osc;

namespace NgimuApi
{
    /// <summary>
    /// Communication process result / state
    /// </summary>
    public enum CommunicationProcessResult
    {
        /// <summary>
        /// The communication process is running.
        /// </summary>
        Running,

        /// <summary>
        /// The communication process completed successfully.
        /// </summary>
        Success,

        /// <summary>
        /// The communication process could not complete because there was an error with the connection.
        /// </summary>
        ConnectionError,

        /// <summary>
        /// The communication process could not complete because the retry limit was exceeded.
        /// </summary>
        RetryLimitExceeded,
    }

    public interface ICommunicationProcess
    {
        CommunicationProcessResult Result { get; }
    }

    /// <summary>
    /// Communication process.
    /// </summary>
    internal abstract class CommunicationProcess : ICommunicationProcess
    {
        #region Private Members

        private object syncLock = new object();

        private Thread thread;

        private bool shouldExit = true;

        private readonly ManualResetEvent settingCallbackComplete = new ManualResetEvent(true);

        /// <summary>
        /// All the callbacks in this process.
        /// </summary>
        protected readonly List<ISettingValue> AllCallbacks;

        /// <summary>
        /// The callbacks that are remaining.
        /// </summary>
        protected readonly List<ISettingValue> RemainingCallbacks = new List<ISettingValue>();

        private readonly List<string> RemainingOscAddresses = new List<string>();

        #endregion

        #region Public Members

        protected abstract string String_Starting { get; }
        protected abstract string String_AlreadyRunning { get; }
        protected abstract string String_CommunicationError { get; }
        protected abstract string String_NoSettingsToProcess { get; }
        protected abstract string String_CouldNotDoAll { get; }
        protected abstract string String_Retrying { get; }
        protected abstract string String_Successful { get; }

        /// <summary>
        /// True if the process is running.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Gets the connection to be used.
        /// </summary>
        public Connection Connection { get; private set; }

        /// <summary>
        /// Gets the number of callbacks that have not been completed.
        /// </summary>
        public int Remaining { get { return RemainingCallbacks.Count; } }

        /// <summary>
        /// Gets the maximum number of retries before failure.
        /// </summary>
        public int RetryLimit { get; set; }

        /// <summary>
        /// Gets the time out in milliseconds between each process iteration. 
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// Gets the result of the asynchronous process.
        /// </summary>
        public CommunicationProcessResult Result { get; protected set; }

        /// <summary>
        /// Gets the reporter for this process.
        /// </summary>
        public IReporter Reporter { get; private set; }

        #endregion

        /// <summary>
        /// Create an communication process.
        /// </summary>
        /// <param name="comms">An Connection to be used for communication.</param>
        /// <param name="reporter">Progress reporter.</param>
        /// <param name="callback">Callback callback to be processed.</param>
        /// <param name="timeout">Time out in milliseconds between each process iteration.</param>
        /// <param name="retryLimit">The maximum number of retries before failure.</param>
        internal CommunicationProcess(Connection comms, IReporter reporter, ISettingValue callback, int timeout = 100, int retryLimit = 3)
        {
            RetryLimit = retryLimit;
            Timeout = timeout;

            Connection = comms;

            Reporter = reporter;

            AllCallbacks = new List<ISettingValue>(new ISettingValue[] { callback });
        }

        /// <summary>
        /// Create an communication process.
        /// </summary>
        /// <param name="comms">An Connection to be used for communication.</param>
        /// <param name="reporter">Progress reporter.</param>
        /// <param name="callbacks">Callbacks to be processed.</param>
        /// <param name="timeout">Time out in milliseconds between each process iteration.</param>
        /// <param name="retryLimit">The maximum number of retries before failure.</param>
        internal CommunicationProcess(Connection comms, IReporter reporter, IEnumerable<ISettingValue> callbacks, int timeout = 100, int retryLimit = 3)
        {
            RetryLimit = retryLimit;
            Timeout = timeout;

            Connection = comms;

            Reporter = reporter;

            AllCallbacks = new List<ISettingValue>(callbacks);
        }

        /// <summary>
        /// Start the asynchronous communication process.
        /// </summary>
        public void RunAsync()
        {
            lock (syncLock)
            {
                if (IsRunning == true)
                {
                    throw new Exception(String_AlreadyRunning);
                }

                // mark the process as running
                Result = CommunicationProcessResult.Running;

                shouldExit = false;

                // clear remaining callbacks
                RemainingCallbacks.Clear();
                RemainingOscAddresses.Clear();
                settingCallbackComplete.Reset();

                // iterate through all the callbacks
                foreach (ISettingValue callback in AllCallbacks)
                {
                    // allow callbacks to remove themselves from the process pool
                    if (SetupCallback(callback) == false)
                    {
                        continue;
                    }

                    // reset the callback state
                    callback.ResetCallbackState();
                    callback.CommunicationResult = CommunicationProcessResult.Running;

                    // add to the remaining pool 
                    RemainingCallbacks.Add(callback);

                    if (callback.Category.Connection == null)
                    {
                        // attach to the connection
                        Connection.Attach(callback.OscAddress, callback.OnMessageReceived);
                    }

                    RemainingOscAddresses.Add(callback.OscAddress);
                    Connection.Attach(callback.OscAddress, OnSettingCallbackReceived);
                }

                if (RemainingCallbacks.Count == 0)
                {
                    OnError(String_NoSettingsToProcess);

                    shouldExit = true;

                    Result = CommunicationProcessResult.Success;

                    return;
                }

                // mark as running
                IsRunning = true;

                // create the asynchronous process thread
                thread = new Thread(AsynchronousProcessThread);
                thread.Name = "Communication Process Run Async " + string.Format(String_Starting, RemainingCallbacks.Count) + " on " + Connection.ConnectionInfo.ToString();

                // start the asynchronous process thread
                thread.Start();
            }
        }

        private void OnSettingCallbackReceived(OscMessage message)
        {
            RemainingOscAddresses.Remove(message.Address);

            if (RemainingOscAddresses.Count == 0)
            {
                settingCallbackComplete.Set();
            }
        }

        /// <summary>
        /// Run the communication process synchronised and return the result immediately.
        /// </summary>
        /// <returns>The result of the process</returns>
        public CommunicationProcessResult Run()
        {
            // start 
            RunAsync();

            // just wait for the process to complete
            if (thread != null && thread.IsAlive == true)
            {
                thread.Join();
            }

            // nullify the thread
            thread = null;

            // return the result
            return Result;
        }

        /// <summary>
        /// Stop asynchronous communication process as soon as possible.
        /// </summary>
        public void Stop()
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

                if (thread != null)
                {
                    // wait for the thread to exit
                    thread.Join();
                }

                thread = null;
            }
        }

        /// <summary>
        /// Run the asynchronous communication process thread. 
        /// </summary>
        private void AsynchronousProcessThread()
        {
            int retryCount = 0;
            int retryLimit = RetryLimit;

            int lastCount = RemainingCallbacks.Count;

            int totalTodo = RemainingCallbacks.Count;

            List<ISettingValue> toRemove = new List<ISettingValue>();

            OnInfo(string.Format(String_Starting, totalTodo));

            bool gotPastStart = false;

            try
            {
                // raise the begin event if it returns fails then abort immediately
                if (OnBegin() == false)
                {
                    return;
                }

                // we got past the start
                gotPastStart = true;

                // while we should not exit and we have callbacks remaining
                while (Connection.IsConnected == true && shouldExit == false && RemainingCallbacks.Count > 0)
                {
                    // iterate over all remaining callbacks 
                    foreach (ISettingValue var in RemainingCallbacks)
                    {
                        if (Connection.IsConnected == false)
                        {
                            Result = CommunicationProcessResult.ConnectionError;
                            return;
                        }

                        // if the callback has completed then continue to the next one
                        if (var.HasCallbackCompleted == true)
                        {
                            continue;
                        }

                        // get the message for the callback 
                        OscMessage message = GetMessageFor(var);

                        // send the message
                        if (SendMessage(message) == false)
                        {
                            // an error happened at the transport layer
                            Result = CommunicationProcessResult.ConnectionError;

                            return;
                        }

                        // wait a moment 
                        thread.Join(10);
                    }

                    // wait for the timeout
                    settingCallbackComplete.WaitOne(Timeout);

                    // iterate over all the remaining callbacks
                    foreach (ISettingValue callback in RemainingCallbacks)
                    {
                        // check if the callback has completed 
                        if (callback.HasCallbackCompleted == false)
                        {
                            continue;
                        }

                        callback.CommunicationResult = CommunicationProcessResult.Success;

                        // add it to the 'to remove' list
                        toRemove.Add(callback);
                    }

                    // iterate over all the callbacks that are to be removed
                    foreach (ISettingValue callback in toRemove)
                    {
                        RemainingCallbacks.Remove(callback);
                    }

                    // if the number of callbacks has changed
                    if (toRemove.Count > 0 &&
                        Reporter != null)
                    {
                        Reporter.OnUpdated(this, EventArgs.Empty);
                    }

                    // clear the 'to remove' list
                    toRemove.Clear();

                    // if the last count is the same as the current count then no callbacks completed in the last iteration
                    if (lastCount == RemainingCallbacks.Count)
                    {
                        // if the retry count is greater or equal to the retry limit 
                        if (retryCount >= retryLimit)
                        {
                            // raise the error message
                            OnError(String_CouldNotDoAll, Strings.AsyncProcess_RetryLimitReached);

                            if (Result != CommunicationProcessResult.ConnectionError)
                            {
                                // flag the error
                                Result = CommunicationProcessResult.RetryLimitExceeded;
                            }

                            return;
                        }

                        // increment the retry count 
                        retryCount++;
                    }
                    else
                    {
                        // reset the retry count
                        retryCount = 0;
                    }

                    // set the last count to the current count 
                    lastCount = RemainingCallbacks.Count;

                    // if there are remaining callbacks left to process
                    if (RemainingCallbacks.Count > 0)
                    {
                        // raise the current completion info event
                        OnInfo(string.Format(String_Successful, totalTodo - RemainingCallbacks.Count, totalTodo));

                        // tell the user that we are going to retry 
                        OnInfo(string.Format(String_Retrying, RemainingCallbacks.Count, totalTodo));
                    }
                }

                // we are done 
                Result = CommunicationProcessResult.Success;
            }
            finally
            {
                foreach (ISettingValue callback in RemainingCallbacks)
                {
                    callback.CommunicationResult = Result;
                }

                // if the first stage got past the start 
                if (gotPastStart == true)
                {
                    if (RemainingCallbacks.Count == 0)
                    {
                        // raise the current completion info event
                        OnInfo(string.Format(String_Successful, totalTodo - RemainingCallbacks.Count, totalTodo));
                    }
                }

                // detach all the callbacks
                foreach (ISettingValue var in AllCallbacks)
                {
                    if (var.Category.Connection == null)
                    {
                        Connection.Detach(var.OscAddress, var.OnMessageReceived);
                    }

                    Connection.Detach(var.OscAddress, OnSettingCallbackReceived);
                }


                OnEnd();

                // flag that we are 
                IsRunning = false;

                // raise the stop event
                OnStop();
            }
        }

        /// <summary>
        /// Send a OSC callback to the remote.
        /// </summary>
        /// <param name="message">An OSC callback.</param>
        /// <returns>True of the callback was sent.</returns>
        private bool SendMessage(OscMessage message)
        {
            try
            {
                Connection.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                if (Connection.IsConnected == true)
                {
                    Reporter?.OnException(this, new ExceptionEventArgs(String_CommunicationError, ex));
                }

                return false;
            }
        }

        /// <summary>
        /// Setup a single callback, this method is called for each callback prior to the start of the process. 
        /// Inheriting classes should implement this method for the type of callback that is used.
        /// </summary>
        /// <param name="callback">A callback object.</param>
        /// <returns>True if the callback should be included in the process.</returns>
        protected abstract bool SetupCallback(ISettingValue callback);

        /// <summary>
        /// Get an OSC callback for a callback object this callback is the one sent to the remote.
        /// Inheriting classes should implement this method for the type of callback that is used.
        /// </summary>
        /// <param name="callback">A callback object.</param>
        /// <returns>The OSC callback to be sent.</returns>
        protected abstract OscMessage GetMessageFor(ISettingValue callback);

        /// <summary>
        /// Called when the process has stopped.
        /// </summary>
        private void OnStop()
        {
            if (Reporter == null)
            {
                return;
            }

            Reporter.OnCompleted(this, EventArgs.Empty);
        }

        /// <summary>
        /// Called when the process begins.
        /// </summary>
        /// <returns>True if the process should continue.</returns>
        protected abstract bool OnBegin();

        /// <summary>
        /// Called when the process ends.
        /// </summary>
        protected abstract void OnEnd();

        /// <summary>
        /// Called when there is info to display. 
        /// </summary>
        /// <param name="message">A textual message.</param>
        protected void OnInfo(string message)
        {
            if (Reporter == null)
            {
                return;
            }

            Reporter.OnInfo(this, new MessageEventArgs(message));
        }

        /// <summary>
        /// Called when an error occurs.
        /// </summary>
        /// <param name="message">A textual message.</param>
        protected void OnError(string message)
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
        protected void OnError(string message, string detail)
        {
            if (Reporter == null)
            {
                return;
            }

            Reporter.OnError(this, new MessageEventArgs(message, detail));
        }
    }
}
