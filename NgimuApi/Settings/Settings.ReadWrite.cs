using System;
using System.Collections.Generic;
using System.Text;
using Rug.Osc;

namespace NgimuApi
{
    partial class Settings
    {
        private object readWriteLock = new object();
        private SettingProcess currentProcess = null;
        private ReporterWrapper reporterWrapper = null;

        private bool IsRunningReadWrite_NoLock
        {
            get
            {
                return currentProcess != null && currentProcess.IsRunning;
            }
        }

        public bool IsRunningReadWrite
        {
            get
            {
                lock (readWriteLock)
                {
                    return IsRunningReadWrite_NoLock;
                }
            }
        }

        private void CheckConnection()
        {
            if (Connection == null)
            {
                throw new Exception("Not connected");
            }
        }

        private List<ISettingValue> GetAllValues(ISettingItem[] items)
        {
            List<ISettingValue> values = new List<ISettingValue>();

            foreach (ISettingItem item in items)
            {
                if (item is ISettingValue)
                {
                    values.Add(item as ISettingValue);
                }
                else if (item is SettingCategrory)
                {
                    values.AddRange((item as SettingCategrory).Values);
                }
            }

            return values;
        }

        public override CommunicationProcessResult Read(IReporter reporter, int timeout = 100, int retryLimit = 3)
        {
            return Read(new ISettingItem[] { this }, reporter, timeout, retryLimit);
        }

        public override CommunicationProcessResult Read(params ISettingItem[] items)
        {
            return Read(items, null, 100, 3);
        }

        public override CommunicationProcessResult Read(ISettingItem[] items, IReporter reporter = null, int timeout = 100, int retryLimit = 3)
        {
            CheckConnection();

            List<ISettingValue> settings = GetAllValues(items);

            SettingProcess process = new SettingProcess(Connection, reporter, settings, timeout, retryLimit);

            lock (readWriteLock)
            {
                return process.Read();
            }
        }

        public override void ReadAync(IReporter reporter, int timeout = 100, int retryLimit = 3)
        {
            ReadAync(new ISettingItem[] { this }, reporter, timeout, retryLimit);
        }

        public override void ReadAync(params ISettingItem[] items)
        {
            ReadAync(items, null, 100, 3);
        }

        public override void ReadAync(ISettingItem[] items, IReporter reporter = null, int timeout = 100, int retryLimit = 3)
        {
            CheckConnection();

            List<ISettingValue> settings = GetAllValues(items);

            lock (readWriteLock)
            {
                if (IsRunningReadWrite_NoLock == true)
                {
                    throw new Exception("Already running a " + currentProcess.Mode + " async process.");
                }

                reporterWrapper = new ReporterWrapper(reporter);

                currentProcess = new SettingProcess(Connection, reporterWrapper, settings, timeout, retryLimit);

                try
                {
                    reporterWrapper.Completed += CurrentProcess_Completed;

                    currentProcess.ReadAsync();
                }
                catch (Exception ex)
                {
                    reporterWrapper.Completed -= CurrentProcess_Completed;

                    throw ex;
                }
            }
        }

        public override CommunicationProcessResult Write(IReporter reporter, int timeout = 100, int retryLimit = 3)
        {
            return Write(new ISettingItem[] { this }, reporter, timeout, retryLimit);
        }

        public override CommunicationProcessResult Write(params ISettingItem[] items)
        {
            return Write(items, null, 100, 3);
        }

        public override CommunicationProcessResult Write(ISettingItem[] items, IReporter reporter = null, int timeout = 100, int retryLimit = 3)
        {
            CheckConnection();

            List<ISettingValue> settings = GetAllValues(items);

            SettingProcess process = new SettingProcess(Connection, reporter, settings, timeout, retryLimit);

            lock (readWriteLock)
            {
                return process.Write();
            }
        }

        public override void WriteAync(IReporter reporter, int timeout = 100, int retryLimit = 3)
        {
            WriteAync(new ISettingItem[] { this }, reporter, timeout, retryLimit);
        }

        public override void WriteAync(params ISettingItem[] items)
        {
            WriteAync(items, null, 100, 3);
        }

        public override void WriteAync(ISettingItem[] items, IReporter reporter = null, int timeout = 100, int retryLimit = 3)
        {
            CheckConnection();

            List<ISettingValue> settings = GetAllValues(items);

            lock (readWriteLock)
            {
                if (IsRunningReadWrite_NoLock == true)
                {
                    throw new Exception("Already running a " + currentProcess.Mode + " async process.");
                }

                reporterWrapper = new ReporterWrapper(reporter);

                currentProcess = new SettingProcess(Connection, reporterWrapper, settings, timeout, retryLimit);

                try
                {
                    reporterWrapper.Completed += CurrentProcess_Completed;

                    currentProcess.WriteAsync();
                }
                catch (Exception ex)
                {
                    reporterWrapper.Completed -= CurrentProcess_Completed;

                    throw ex;
                }
            }
        }

        public void Stop()
        {
            if (IsRunningReadWrite_NoLock == false)
            {
                return;
            }

            currentProcess.Stop();
        }

        void CurrentProcess_Completed(object sender, EventArgs e)
        {
            lock (readWriteLock)
            {
                reporterWrapper.Completed -= CurrentProcess_Completed;

                reporterWrapper = null;
                currentProcess = null;
            }
        }

        public void Load(string filePath, IReporter reporter = null)
        {
            lock (readWriteLock)
            {
                using (OscFileReader fileReader = new OscFileReader(Helper.ResolvePath(filePath), OscPacketFormat.String))
                using (OscAddressManager manager = new OscAddressManager())
                {
                    foreach (ISettingValue value in AllValues)
                    {
                        if (value.IsReadOnly == true)
                        {
                            continue;
                        }

                        manager.Attach(value.OscAddress, value.OnLocalMessage);
                    }

                    fileReader.PacketRecived += delegate (OscPacket packet)
                    {
                        if (packet.Error != OscPacketError.None)
                        {
                            if (reporter != null) reporter.OnError(this, new MessageEventArgs(packet.ErrorMessage));

                            return;
                        }

                        manager.Invoke(packet);
                    };

                    fileReader.ReadToEnd();
                }
            }
        }

        public void Save(string filePath, IReporter reporter = null)
        {
            lock (readWriteLock)
            {
                bool allValuesUndefined = true;

                foreach (ISettingValue value in AllValues)
                {
                    if (value.IsValueUndefined == false)
                    {
                        allValuesUndefined = false;
                        break;
                    }
                }

                if (allValuesUndefined == true)
                {
                    return;
                }

                using (OscFileWriter fileWriter = new OscFileWriter(Helper.ResolvePath(filePath), System.IO.FileMode.Create, OscPacketFormat.String))
                {
                    foreach (ISettingValue value in AllValues)
                    {
                        if (value.IsValueUndefined == true)
                        {
                            continue;
                        }

                        fileWriter.Send(value.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the device descriptor.
        /// </summary>
        /// <returns>string in the format "{DeviceName} - {SerialNumber}"</returns>
        public string GetDeviceDescriptor()
        {
            string deviceName = DeviceName.Value;
            string serialNumber = SerialNumber.Value;

            if (string.IsNullOrEmpty(deviceName) == true)
            {
                deviceName = "Unknown Device Name";
            }

            if (string.IsNullOrEmpty(serialNumber) == true)
            {
                serialNumber = "Unknown Serial Number";
            }

            return $"{deviceName} - {serialNumber}";
        }

        public static string GetCommunicationFailureString(IEnumerable<ISettingValue> values, int max, out bool allValuesFailed, out bool allValuesSucceeded)
        {
            allValuesFailed = true;
            allValuesSucceeded = true;
            StringBuilder sb = new StringBuilder();
            int count = 0;
            int truncatedCount = 0;

            sb.AppendLine();
            sb.AppendLine();

            foreach (ISettingValue value in values)
            {
                if (value.CommunicationResult.HasValue && value.CommunicationResult.Value == CommunicationProcessResult.Success)
                {
                    allValuesFailed = false;

                    continue;
                }

                if (value.CommunicationResult.HasValue == false)
                {
                    continue;
                }

                allValuesSucceeded = false;

                if (count++ >= max)
                {
                    truncatedCount++;
                    continue;
                }

                sb.AppendLine(value.OscAddress);
            }

            if (truncatedCount > 0)
            {
                sb.Append("... (" + truncatedCount + " more)");
            }

            return sb.ToString();
        }

    }
}
