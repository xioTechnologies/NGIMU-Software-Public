using System;
using System.Collections.Generic;
using Rug.Osc;

namespace NgimuApi
{
    public enum SettingProcessMode { Read, Write }

    public sealed class SettingProcess
    {
        #region Private Members

        private object syncLock = new object();

        private List<ISettingValue> allVariables;

        private CommunicationProcess asyncProcess;

        #endregion

        #region Public Members

        /// <summary>
        /// 
        /// </summary>
        public bool IsRunning
        {
            get
            {
                if (asyncProcess == null)
                {
                    return false;
                }

                return asyncProcess.IsRunning;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SettingProcessMode Mode { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Connection Communicator { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public IReporter Reporter { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int Remaining
        {
            get
            {
                if (asyncProcess == null)
                {
                    return 0;
                }

                return asyncProcess.Remaining;
            }
        }

        /// <summary>
        /// Gets the maximum number of retries before failure.
        /// </summary>
        public int RetryLimit { get; set; }

        /// <summary>
        /// Gets the time out in milliseconds between each process iteration. 
        /// </summary>
        public int Timeout { get; set; }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comms"></param>
        /// <param name="reporter">Progress reporter.</param>
        /// <param name="variables"></param>
        /// <param name="timeout"></param>
        /// <param name="retryLimit"></param>
        public SettingProcess(Connection comms, IReporter reporter, IEnumerable<ISettingValue> variables,
            int timeout = 100,
            int retryLimit = 3)
        {
            RetryLimit = retryLimit;
            Timeout = timeout;
            Reporter = reporter;

            Communicator = comms;

            allVariables = new List<ISettingValue>(variables);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comms"></param>
        /// <param name="reporter">Progress reporter.</param>
        /// <param name="variables"></param>
        /// <param name="timeout"></param>
        /// <param name="retryLimit"></param>
        public SettingProcess(Connection comms, IReporter reporter, ISettingValue variables,
            int timeout = 100,
            int retryLimit = 3)
        {
            RetryLimit = retryLimit;
            Timeout = timeout;
            Reporter = reporter;

            Communicator = comms;

            allVariables = new List<ISettingValue>();
            allVariables.Add(variables);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            lock (syncLock)
            {
                if (IsRunning == false)
                {
                    return;
                }

                asyncProcess.Stop();

                asyncProcess = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CommunicationProcessResult Read()
        {
            lock (syncLock)
            {
                if (IsRunning == true)
                {
                    throw new Exception(string.Format(Strings.SettingsProcess_ProcessAlreadyRunning, Mode.ToString().ToLower()));
                }

                CreateReadProcess();

                return asyncProcess.Run();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CommunicationProcessResult Write()
        {
            lock (syncLock)
            {
                if (IsRunning == true)
                {
                    throw new Exception(string.Format(Strings.SettingsProcess_ProcessAlreadyRunning, Mode.ToString().ToLower()));
                }

                CreateWriteProcess();

                return asyncProcess.Run();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ReadAsync()
        {
            lock (syncLock)
            {
                if (IsRunning == true)
                {
                    throw new Exception(string.Format(Strings.SettingsProcess_ProcessAlreadyRunning, Mode.ToString().ToLower()));
                }

                CreateReadProcess();

                asyncProcess.RunAsync();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void WriteAsync()
        {
            lock (syncLock)
            {
                if (IsRunning == true)
                {
                    throw new Exception(string.Format(Strings.SettingsProcess_ProcessAlreadyRunning, Mode.ToString().ToLower()));
                }

                CreateWriteProcess();

                asyncProcess.RunAsync();
            }
        }

        private void CreateReadProcess()
        {
            asyncProcess = new SettingsReadProcess(Communicator, Reporter, allVariables, Timeout, RetryLimit);  //, MuteToStart); 

            Mode = SettingProcessMode.Read;
        }

        private void CreateWriteProcess()
        {
            asyncProcess = new SettingsWriteProcess(Communicator, Reporter, allVariables, Timeout, RetryLimit); // MuteToStart

            Mode = SettingProcessMode.Write;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="reporter">Progress reporter.</param>
        /// <param name="muteToStart"></param>
        /// <param name="message"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CommunicationProcessResult ReadSingle<T>(Connection connection, IReporter reporter, ISettingValue<T> setting, out T value) // bool muteToStart,
        {
            SettingProcess process = new SettingProcess(connection, reporter, new ISettingValue[] { setting }, 100, 3); // muteToStart

            CommunicationProcessResult result = process.Read();

            switch (result)
            {
                case CommunicationProcessResult.Success:
                    value = setting.RemoteValue;
                    break;
                default:
                    value = default(T);
                    break;
            }

            return result;
        }

        /// <summary>
        /// Reads a single <see cref="ISettingValue"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection">The connection.</param>
        /// <param name="reporter">The reporter.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="retryLimit">The retry limit.</param>
        /// <param name="setting">The setting.</param>
        /// <param name="value">The value.</param>
        /// <returns>ImuAsyncProcessResult.</returns>
        public static CommunicationProcessResult ReadSingle<T>(Connection connection, IReporter reporter, int timeout, int retryLimit, ISettingValue<T> setting, out T value)
        {
            SettingProcess process = new SettingProcess(connection, reporter, setting, timeout, retryLimit);

            CommunicationProcessResult result = process.Read();

            switch (result)
            {
                case CommunicationProcessResult.Success:
                    value = setting.RemoteValue;
                    break;
                default:
                    value = default(T);
                    break;
            }

            return result;
        }

        public static CommunicationProcessResult WriteSingle<T>(Connection connection, IReporter reporter, ISettingValue<T> setting, T value)
        {
            setting.Value = value;

            SettingProcess process = new SettingProcess(connection, reporter, new ISettingValue[] { setting }, 100, 3);

            CommunicationProcessResult result = process.Write();

            return result;
        }

        public static CommunicationProcessResult WriteSingle<T>(Connection connection, IReporter reporter, int timeout, int retryLimit, ISettingValue<T> setting, T value)
        {
            setting.Value = value;

            SettingProcess process = new SettingProcess(connection, reporter, setting, timeout, retryLimit);

            CommunicationProcessResult result = process.Write();

            return result;
        }
    }

    internal class SettingsReadProcess : CommunicationProcess
    {
        protected override string String_Starting { get { return Strings.SettingsRead_Starting; } }
        protected override string String_AlreadyRunning { get { return Strings.SettingsRead_AlreadyRunning; } }
        protected override string String_CommunicationError { get { return Strings.SettingsRead_CommunicationError; } }
        protected override string String_NoSettingsToProcess { get { return Strings.SettingsRead_NoValidSettings; } }
        protected override string String_CouldNotDoAll { get { return Strings.SettingsRead_CouldNotDoAll; } }
        protected override string String_Retrying { get { return Strings.SettingsRead_Retrying; } }
        protected override string String_Successful { get { return Strings.SettingsRead_Successful; } }

        public SettingsReadProcess(Connection comms, IReporter reporter, IEnumerable<ISettingValue> variables, int timeout, int retryLimit)
            : base(comms, reporter, variables, timeout, retryLimit)
        {
        }

        protected override bool SetupCallback(ISettingValue callback)
        {
            return true;
        }

        protected override OscMessage GetMessageFor(ISettingValue var)
        {
            return new OscMessage(var.OscAddress);
        }

        protected override bool OnBegin()
        {
            return true;
        }

        protected override void OnEnd()
        {

        }
    }

    internal class SettingsWriteProcess : CommunicationProcess
    {
        protected override string String_Starting { get { return Strings.SettingsWrite_Starting; } }
        protected override string String_AlreadyRunning { get { return Strings.SettingsWrite_AlreadyRunning; } }
        protected override string String_CommunicationError { get { return Strings.SettingsWrite_CommunicationError; } }
        protected override string String_NoSettingsToProcess { get { return Strings.SettingsWrite_NoValidSettings; } }
        protected override string String_CouldNotDoAll { get { return Strings.SettingsWrite_CouldNotDoAll; } }
        protected override string String_Retrying { get { return Strings.SettingsWrite_Retrying; } }
        protected override string String_Successful { get { return Strings.SettingsWrite_Successful; } }

        //public ImuSettingsWriteProcess(Connection comms, IImuReporter reporter, IEnumerable<ISettingValue> variables, int timeout, int retryLimit, bool muteToStart, ApplyMode applyMode)
        public SettingsWriteProcess(Connection comms, IReporter reporter, IEnumerable<ISettingValue> variables, int timeout, int retryLimit)
            : base(comms, reporter, variables, timeout, retryLimit)
        {

        }

        protected override bool SetupCallback(ISettingValue callback)
        {
            if (callback.IsReadOnly == true)
            {
                return false;
            }

            if (callback.IsValueUndefined == true)
            {
                return false;
            }

            return true;
        }

        protected override OscMessage GetMessageFor(ISettingValue var)
        {
            return var.Message;
        }

        protected override bool OnBegin()
        {
            return true;
        }

        protected override void OnEnd()
        {
            if (Connection.IsConnected == false)
            {
                return;
            }
        }
    }
}