using System;
using System.Collections.Generic;
using System.IO;
using NgimuApi.Data;
using Rug.Osc;

namespace NgimuApi.Logging
{
    internal class DeviceLogger
    {
        /// <summary>
        /// Gets the connection that is the source of the logging messages.
        /// </summary>
        public readonly Connection Connection;

        public readonly DeviceMetadata Metadata;

        private readonly Dictionary<string, CsvFileWriter> csvFileWriters = new Dictionary<string, CsvFileWriter>();
        private readonly Dictionary<string, DataBase> dataObjects = new Dictionary<string, DataBase>();
        private readonly List<string> excludedAddresses = new List<string>();
        private OscTimeTag firstTimestamp;
        private bool hasFirstTimestamp;
        private bool isRunning = false;
        private OscTimeTag lastTimestamp;

        /// <summary>Gets the root directory path.</summary>
        private readonly string rootDirectory;

        private readonly SessionLogger session;
        private readonly object syncLock = new object();

        public string ConnectionDescriptor { get; private set; }

        public string Directory => Path.Combine(rootDirectory, ConnectionDescriptor);

        public readonly int IndexInSession;

        public DeviceLogger(SessionLogger session, Connection connection, int indexInSession = 0)
        {
            IndexInSession = indexInSession;

            Metadata = new DeviceMetadata();

            Connection = connection;
            this.session = session;

            rootDirectory = session.SessionDirectory;

            AddDataObject(new AltitudeData());
            AddDataObject(new AnalogueInputsData());
            AddDataObject(new BatteryData());
            AddDataObject(new LinearAccelerationData());
            AddDataObject(new MagnitudesData());
            AddDataObject(new EarthAccelerationData());
            AddDataObject(new SensorsData());
            AddDataObject(new TemperatureData());
            AddDataObject(new AuxiliarySerialData());
            AddDataObject(new AuxiliarySerialCtsData());
            AddDataObject(new SerialCtsData());
            AddDataObject(new QuaternionData());
            AddDataObject(new RotationMatrixData());
            AddDataObject(new EulerAnglesData());
            AddDataObject(new HumidityData());
            AddDataObject(new RssiData());
            AddDataObject(new ButtonData());

            foreach (CommandMetaData command in Commands.GetCommands())
            {
                excludedAddresses.Add(command.OscAddress);
            }

            foreach (ISettingValue setting in connection.Settings.AllValues)
            {
                excludedAddresses.Add(setting.OscAddress);
            }

            // exclude errors
            excludedAddresses.Add("/error");
        }

        public void Start()
        {
            ConnectionDescriptor = Helper.CleanFileName(Connection.Settings.GetDeviceDescriptor());

            int index = 0;
            while (System.IO.Directory.Exists(Directory) == true)
            {
                ConnectionDescriptor = Helper.CleanFileName(Connection.Settings.GetDeviceDescriptor() + " (" + (++index) + ")");
            }

            System.IO.Directory.CreateDirectory(Directory);

            Metadata.DeviceInformation.SetValues(Connection.Settings.DeviceInformation);

            isRunning = true;

            Connection.Message += new MessageEvent(Connection_Message);
        }

        public void Stop()
        {
            lock (syncLock)
            {
                isRunning = false;

                Connection.Message -= new MessageEvent(Connection_Message);

                foreach (CsvFileWriter file in csvFileWriters.Values)
                {
                    file.Dispose();
                }

                // stash the path before the move (if one is to take place). 
                string writtenToBasePath = Directory;

                // Rename directory now that device information has been read from the file.
                if (Connection.ConnectionType == ConnectionType.File)
                {
                    string resolvedSessionDescriptor = Helper.CleanFileName(Connection.Settings.GetDeviceDescriptor());

                    string resolvedDirectory = Path.Combine(rootDirectory, resolvedSessionDescriptor);

                    if (Directory.Equals(resolvedDirectory) == false)
                    {
                        Helper.MoveDirectory(Directory, resolvedDirectory, true);

                        ConnectionDescriptor = resolvedSessionDescriptor;
                    }
                }

                Metadata.DirectoryName = ConnectionDescriptor;

                // Use the last available setting for the meta data. 
                Metadata.DeviceInformation.SetValues(Connection.Settings.DeviceInformation);

                Metadata.TimeStamps.FirstTimeTag = firstTimestamp;
                Metadata.TimeStamps.LastTimeTag = lastTimestamp;
                Metadata.ConnectionInformation.ConnectionInfo = Connection.ConnectionInfo;
                Metadata.ConnectionInformation.ConnectionType = Connection.ConnectionType;

                string basePath = Directory;

                foreach (CsvFileWriter file in csvFileWriters.Values)
                {
                    // use the stashed path for substring-ing as the directory might have changed since the CSV file was created. 
                    Metadata.FilesCreated.Add(file.FilePath.Substring(writtenToBasePath.Length + 1), file.MessageCount);
                }

                Metadata.Statistics.SetValues(Connection.CommunicationStatistics);

                Metadata.Save(Path.Combine(Directory, "Device.xml"));

                Connection.Settings.Save(Path.Combine(Directory, "Settings.txt"), null);
            }
        }

        private void AddDataObject(DataBase data)
        {
            dataObjects.Add(data.OscAddress, data);
        }

        #region Message Event

        private void Connection_Message(Connection source, MessageDirection direction, Rug.Osc.OscMessage message)
        {
            lock (syncLock)
            {
                try
                {
                    if (direction == MessageDirection.Transmit)
                    {
                        return;
                    }

                    if (isRunning == false)
                    {
                        return;
                    }

                    if (SetFirstAndLastTimestamps(message.TimeTag) == false)
                    {
                        return;
                    }

                    if (dataObjects.ContainsKey(message.Address) == true)
                    {
                        DataBase data = dataObjects[message.Address];

                        data.OnMessage_Inner(message);

                        if (data.HasValidValues == false)
                        {
                            return;
                        }

                        CsvFileWriter logger;

                        if (csvFileWriters.TryGetValue(message.Address, out logger) == false)
                        {
                            logger =
                                new CsvFileWriter(Path.Combine(Directory,
                                    Helper.ToLowerCamelCase(message.Address) + ".csv"));

                            logger.AddLine(data.CsvHeader);

                            csvFileWriters.Add(message.Address, logger);
                        }

                        logger.AddLine(data.ToCsv(session.FirstTimestamp));

                        logger.IncrementMessageCount();

                        return;
                    }

                    if (excludedAddresses.Contains(message.Address) == false)
                    {
                        CsvFileWriter logger;

                        if (csvFileWriters.TryGetValue(message.Address, out logger) == false)
                        {
                            logger =
                                new CsvFileWriter(Path.Combine(Directory,
                                    Helper.ToLowerCamelCase(message.Address) + ".csv"));

                            logger.AddLine(Helper.UnknownMessageToCsvHeader(message));

                            csvFileWriters.Add(message.Address, logger);
                        }

                        logger.AddLine(Helper.UnknownMessageToCsv(session.FirstTimestamp, message));

                        logger.IncrementMessageCount();

                        return;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error while processing log message.", ex);
                }
            }
        }

        private bool SetFirstAndLastTimestamps(OscTimeTag? timetag)
        {
            if (timetag == null)
            {
                return true;
            }

            session.SetFirstTimestamp(timetag);

            if (hasFirstTimestamp == true)
            {
                // if the time-tag is from the past then do not process it 
                if (timetag.Value.Value < firstTimestamp.Value)
                {
                    return false;
                }

                lastTimestamp = timetag.Value;

                return true;
            }

            lastTimestamp = timetag.Value;

            firstTimestamp = timetag.Value;

            hasFirstTimestamp = true;

            return true;
        }

        #endregion Message Event
    }
}