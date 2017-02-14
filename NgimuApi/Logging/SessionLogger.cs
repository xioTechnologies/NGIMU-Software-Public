using System;
using System.IO;
using System.Threading;
using Rug.Osc;

namespace NgimuApi.Logging
{
    public sealed class SessionLogger : IDisposable
    {
        private ManualResetEvent CompletedEvent = new ManualResetEvent(true);

        private Thread thread;

        internal SessionSettings Settings;

        private bool hasStarted = false;
        private bool isRunning = false;

        private DeviceLogger[] ConnectionLoggers;

        /// <summary>
        /// Occurs when logging has stopped.  
        /// </summary>
        public event EventHandler Stopped;

        /// <summary>
        /// Occurs when an exception is thrown.
        /// </summary>
        public event EventHandler<ExceptionEventArgs> Exception;

        /// <summary>
        /// Gets the date and time when logging started. 
        /// </summary>
        public DateTime LoggingStart { get; private set; }

        /// <summary>
        /// Gets the time span of the desired logging period. 
        /// </summary>
        public TimeSpan LoggingPeriod { get; private set; }

        /// <summary>
        /// Gets the time span of the duration of the current logging session. 
        /// </summary>
        public TimeSpan LoggingTime
        {
            get
            {
                if (hasStarted == false)
                {
                    return TimeSpan.Zero;
                }

                TimeSpan span = DateTime.Now.Subtract(LoggingStart);

                if (LoggingPeriod.Ticks > 0 &&
                    LoggingPeriod.Ticks < span.Ticks)
                {
                    span = LoggingPeriod;
                }

                return span;
            }
        }

        public bool IsLogging { get { return isRunning; } }

        private bool hasFirstTimestamp;

        internal OscTimeTag FirstTimestamp { get; private set; }

        public string SessionDirectory { get; private set; }

        public readonly SessionMetadata Metadata = new SessionMetadata();

        /// <summary>
        /// Creates a IMU logger. 
        /// </summary>
        /// <param name="loggerSettings">Settings for the logger.</param>
        /// <param name="connections">IMU connections that is the source of messages to be logged.</param>
        public SessionLogger(SessionSettings loggerSettings, params Connection[] connections)
        {
            Settings = loggerSettings.Clone();

            if (connections.Length == 0)
            {
                throw new ArgumentException("IMU connections cannot be empty.", "connections");
            }

            if (string.IsNullOrEmpty(Settings.SessionName) == true)
            {
                throw new Exception("Session name cannot be null or empty.");
            }

            if (Helper.IsInvalidFileName(Settings.SessionName) == true)
            {
                throw new Exception("Session name contains invalid characters.");
            }

            SessionDirectory = Path.Combine(Settings.RootDirectory, Settings.SessionName);

            ConnectionLoggers = new DeviceLogger[connections.Length];

            for (int i = 0; i < connections.Length; i++)
            {
                if (connections[i] == null)
                {
                    throw new ArgumentNullException(string.Format("The connection at index {0} is null.", i), "connections");
                }

                if (connections[i].ConnectionType == ConnectionType.File)
                {
                    if (connections.Length > 1)
                    {
                        throw new ArgumentException("Can only convert one file per session.", "connections");
                    }
                }

                ConnectionLoggers[i] = new DeviceLogger(this, connections[i], i);
            }

            LoggingPeriod = new TimeSpan(0, 0, (int)Settings.LoggingPeriod);

            DirectoryInfo directoryInfo = new DirectoryInfo(SessionDirectory);

            if (directoryInfo.Exists == true)
            {
                throw new System.Exception(string.Format("Session directory already exists. \"{0}\"", SessionDirectory));
            }

            directoryInfo.Create();
        }

        internal void SetFirstTimestamp(OscTimeTag? timetag)
        {
            if (timetag == null)
            {
                return;
            }

            if (hasFirstTimestamp == true)
            {
                return;
            }

            FirstTimestamp = timetag.Value;

            hasFirstTimestamp = true;
        }


        /// <summary>
        /// Start logging. 
        /// </summary>
        public void Start()
        {
            Dispose();

            hasStarted = false;

            CompletedEvent.Reset();

            thread = new Thread(RunProcess);
            thread.Name = "Session Logger " + Settings.SessionName;
            thread.Start();
        }

        public void WaitUntilComplete()
        {
            if (LoggingPeriod.Ticks <= 0)
            {
                throw new System.Exception("Cannot wait for infinite time period.");
            }

            CompletedEvent.WaitOne();
        }

        void RunProcess()
        {
            try
            {
                LoggingStart = DateTime.Now;

                hasStarted = true;
                isRunning = true;

                foreach (DeviceLogger logger in ConnectionLoggers)
                {
                    logger.Start();
                }

                while (isRunning == true)
                {
                    if (LoggingPeriod.Ticks > 0 &&
                        LoggingStart.Add(LoggingPeriod) <= DateTime.Now)
                    {
                        isRunning = false;
                    }

                    Thread.CurrentThread.Join(1);
                }

                foreach (DeviceLogger logger in ConnectionLoggers)
                {
                    logger.Stop();
                }

                Metadata.SessionInformation.Name = Settings.SessionName;
                Metadata.SessionInformation.Date = LoggingStart;
                Metadata.SessionInformation.Duration = LoggingTime;

                foreach (DeviceLogger logger in ConnectionLoggers)
                {
                    Metadata.Devices.Add(logger.Metadata);
                }

                Metadata.Save(Path.Combine(SessionDirectory, "Session.xml"));
            }
            catch (Exception ex)
            {
                OnException(ex.Message, ex);
            }
            finally
            {
                OnStopped();
            }
        }

        private void OnException(string message, System.Exception exception)
        {
            Exception?.Invoke(this, new ExceptionEventArgs(message, exception));
        }

        private void OnStopped()
        {
            CompletedEvent.Set();

            Stopped?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Stop logging, disposes the logger and finalises all files. 
        /// </summary>
        public void Stop()
        {
            Dispose();
        }

        /// <summary>
        /// Dispose the logger and finalise all files. 
        /// </summary>
        public void Dispose()
        {
            isRunning = false;

            CompletedEvent.WaitOne();
        }
    }
}
