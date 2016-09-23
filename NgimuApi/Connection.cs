using System;
using NgimuApi.ConnectionImplementations;
using NgimuApi.Data;
using Rug.Osc;

namespace NgimuApi
{
    public enum ConnectionType
    {
        Udp,
        Serial,
        File,
        //FileWrite,
    }

    /// <summary>
    /// Connection to a IMU
    /// </summary>
    public class Connection : IDisposable
    {
        /// <summary>
        /// Enumerate all available connections. Note: This method blocks until connections have been enumerated.
        /// </summary>
        /// <returns>All available connections.</returns>
        public static SearchForConnections.ConnectionSearchInfo[] EnumerateConnections()
        {
            return SearchForConnections.SearchForConnections.EnumerateConnections();
        }

        #region Private Members

        private readonly OscAddressManager oscAddressManager = new OscAddressManager();

        private ConnectionImplementation connectionImplementation;

        #endregion

        #region Public Members

        /// <summary>
        /// Occurs when an unknown OSC address is encountered.
        /// </summary>
        public event EventHandler<UnknownAddressEventArgs> UnknownAddress;

        /// <summary>
        /// Communication statistics.
        /// </summary>
        public readonly OscCommunicationStatistics CommunicationStatistics = new OscCommunicationStatistics();

        /// <summary>
        /// A ID derived for the unique key of the connection info.
        /// </summary>
        public string ID { get; private set; }

        /// <summary>
        /// User object.
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Get the connection info used to create this connection.
        /// </summary>
        public IConnectionInfo ConnectionInfo { get; private set; }

        /// <summary>
        /// Gets the type of connection.
        /// </summary>
        public ConnectionType ConnectionType { get; private set; }


        /// <summary>
        /// Gets a flag indicating if the connection is open.
        /// </summary>
        public bool IsConnected { get; private set; }

        /// <summary>
        /// Gets the human friendly name of this connection.
        /// </summary>
        public string Name
        {
            get
            {
                if (ConnectionInfo != null)
                {
                    return ConnectionInfo.ToString();
                }
                else
                {
                    return ID;
                }
            }
        }

        /// <summary>
        /// Gets the most recent button data received from the device.
        /// </summary>
        public readonly ButtonData Button = new ButtonData();

        /// <summary>
        /// Gets the most recent XXX data received from the device.
        /// </summary>
        public readonly SensorsData Sensors = new SensorsData();

        /// <summary>
        /// Gets the most recent quaternion data received from the device.
        /// </summary>
        public readonly QuaternionData Quaternion = new QuaternionData();

        /// <summary>
        /// Gets the most recent rotation matrix data received from the device.
        /// </summary>
        public readonly RotationMatrixData RotationMatrix = new RotationMatrixData();

        /// <summary>
        /// Gets the most recent Euler angle data received from the device.
        /// </summary>
        public readonly EulerAnglesData EulerAngles = new EulerAnglesData();

        /// <summary>
        /// Gets the most recent linear acceleration data received from the device.
        /// </summary>
        public readonly LinearAccelerationData LinearAcceleration = new LinearAccelerationData();

        /// <summary>
        /// Gets the most recent magnitudes data received from the device.
        /// </summary>
        public readonly MagnitudesData Magnitudes = new MagnitudesData();

        /// <summary>
        /// Gets the most recent Earth acceleration data received from the device.
        /// </summary>
        public readonly EarthAccelerationData EarthAcceleration = new EarthAccelerationData();

        /// <summary>
        /// Gets the most recent altitude data received from the device.
        /// </summary>
        public readonly AltitudeData Altitude = new AltitudeData();

        /// <summary>
        /// Gets the most recent temperature data received from the device.
        /// </summary>
        public readonly TemperatureData Temperature = new TemperatureData();

        /// <summary>
        /// Gets the most recent humidity data received from the device.
        /// </summary>
        public readonly HumidityData Humidity = new HumidityData();

        /// <summary>
        /// Gets the most recent battery data received from the device.
        /// </summary>
        public readonly BatteryData Battery = new BatteryData();

        /// <summary>
        /// Gets the most recent analogue input data received from the device.
        /// </summary>
        public readonly AnalogueInputsData AnalogueInputs = new AnalogueInputsData();

        /// <summary>
        /// Gets the most recent RSSI data received from the device.
        /// </summary>
        public readonly RssiData Rssi = new RssiData();

        /// <summary>
        /// Gets the most recent auxiliary serial data received from the device.
        /// </summary>
        public readonly AuxiliarySerialData AuxiliarySerial = new AuxiliarySerialData();

        /// <summary>
        /// Gets the most recent auxiliary serial CTS data received from the device.
        /// </summary>
        public readonly AuxiliarySerialCtsData AuxiliarySerialCts = new AuxiliarySerialCtsData();

        /// <summary>
        /// Gets the most recent serial CTS data received from the device.
        /// </summary>
        public readonly SerialCtsData SerialCts = new SerialCtsData();

        /// <summary>
        /// Gets the last error message received from the device.
        /// </summary>
        public readonly ErrorData DeviceError = new ErrorData();

        /// <summary>
        /// Gets the device settings.
        /// </summary>
        public readonly Settings Settings;

        /// <summary>
        /// Occurs when a connection has been made.
        /// </summary>
        public event EventHandler Connected;

        /// <summary>
        /// Occurs when the connection is disconnected.
        /// </summary>
        public event EventHandler Disconnected;

        /// <summary>
        /// Occurs when an internal exception is thrown.
        /// </summary>
        public event EventHandler<ExceptionEventArgs> Exception;

        /// <summary>
        /// Occurs when an non-exception error is encountered.
        /// </summary>
        public event EventHandler<MessageEventArgs> Error;

        /// <summary>
        /// Occurs when there is state info for the user.
        /// </summary>
        public event EventHandler<MessageEventArgs> Info;

        /// <summary>
        /// Occurs when a message is sent or received.
        /// </summary>
        public event MessageEvent Message;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a connection using the supplied auto connection info.
        /// </summary>
        /// <param name="connectionInfo">Auto connection info that describes the connection.</param>
        public Connection(SearchForConnections.ConnectionSearchInfo connectionInfo)
            : this(connectionInfo.ConnectionInfo)
        {

        }

        /// <summary>
        /// Create a connection using the supplied connection info.
        /// </summary>
        /// <param name="connectionInfo">Connection info that describes the connection.</param>
        public Connection(IConnectionInfo connectionInfo)
        {
            Settings = new Settings(this);

            ConnectionInfo = connectionInfo;
            ID = ConnectionInfo.ToIDString();

            CreateReceiver();

            AttachDataReceivers();

            if (connectionInfo is UdpConnectionInfo)
            {
                ConnectionType = ConnectionType.Udp;
                CreateUdpComms();
            }
            else if (connectionInfo is SerialConnectionInfo)
            {
                ConnectionType = ConnectionType.Serial;
                CreateSerialComms();
            }
            else if (connectionInfo is SDCardFileConnectionInfo)
            {
                ConnectionType = ConnectionType.File;
                CreateFileReader();
            }
            else
            {
                throw new Exception(Strings.Exception_UnknownConnectionType);
            }
        }

        private void AttachDataReceivers()
        {
            Altitude.Attach(this);
            AnalogueInputs.Attach(this);
            AuxiliarySerial.Attach(this);
            AuxiliarySerialCts.Attach(this);
            SerialCts.Attach(this);
            Battery.Attach(this);
            Button.Attach(this);
            EarthAcceleration.Attach(this);
            EulerAngles.Attach(this);
            LinearAcceleration.Attach(this);
            Magnitudes.Attach(this);
            Quaternion.Attach(this);
            RotationMatrix.Attach(this);
            Sensors.Attach(this);
            Temperature.Attach(this);
            Humidity.Attach(this);
            Rssi.Attach(this);
            DeviceError.Attach(this);
        }

        private void CreateReceiver()
        {
            oscAddressManager.UnknownAddress += new EventHandler<UnknownAddressEventArgs>(OnUnknownAddress);
            oscAddressManager.Statistics = CommunicationStatistics;
        }

        private void CreateUdpComms()
        {
            UdpConnectionInfo info = ConnectionInfo as UdpConnectionInfo;

            connectionImplementation = new UdpConnectionImplementation(this, info, CommunicationStatistics);
        }

        private void CreateSerialComms()
        {
            SerialConnectionInfo info = ConnectionInfo as SerialConnectionInfo;

            connectionImplementation = new SerialConnectionImplementation(this, info, CommunicationStatistics);
        }

        private void CreateFileReader()
        {
            SDCardFileConnectionInfo info = ConnectionInfo as SDCardFileConnectionInfo;

            connectionImplementation = new FileReadConnectionImplementation(this, info, CommunicationStatistics);
        }

        #endregion

        /// <summary>
        /// Force the connection to test itself.
        /// </summary>
        public void CheckConnectionState()
        {
            connectionImplementation.CheckConnectionState();
        }

        /// <summary>
        /// Connect to the device.
        /// </summary>
        public void Connect()
        {
            try
            {
                connectionImplementation.Connect();

                CommunicationStatistics.Reset();
                CommunicationStatistics.Start();

                IsConnected = true;
                OnConnect();

                connectionImplementation.Start();
            }
            catch (Exception ex)
            {
                connectionImplementation.Close();

                OnException(string.Format(Strings.Connection_FailedToConnect, Name), ex);

                throw new Exception(string.Format(Strings.Connection_FailedToConnect, Name), ex);
            }
        }

        /// <summary>
        /// Close the connection.
        /// </summary>
        public void Close()
        {
            bool wasConnected = IsConnected;

            IsConnected = false;

            connectionImplementation.Close();

            CommunicationStatistics.Stop();

            if (wasConnected == true)
            {
                OnInfo(string.Format(Strings.Connection_Disconnected, Name));
            }

            OnDisconnect();
        }

        /// <summary>
        /// Dispose of all resources.
        /// </summary>
        public void Dispose()
        {
            Close();

            connectionImplementation.Dispose();

            oscAddressManager.Dispose();
        }

        /// <summary>
        /// Send a OscPacket to the device.
        /// </summary>
        /// <param name="packet">A packet to be sent.</param>
        public void Send(OscPacket packet)
        {
            try
            {
                OnTransmitPacket(packet);

                connectionImplementation.Send(packet);
            }
            catch (Exception ex)
            {
                throw new Exception(Strings.Connection_CouldNotSendPacket, ex);
            }
        }


        /// <summary>
        /// Process a single command synchronously. Note: This method will block until the process is complete.
        /// </summary>
        /// <param name="command">Command.</param>
        /// <returns>The result of the process.</returns>
        public CommunicationProcessResult SendCommand(Command command)
        {
            return Commands.Send(this, command);
        }

        /// <summary>
        /// Process a single command synchronously. Note: This method will block until the process is complete.
        /// </summary>
        /// <param name="command">Command.</param>
        /// <param name="args">OSC message arguments</param>
        /// <returns>The result of the process.</returns>
        public CommunicationProcessResult SendCommand(Command command, params object[] args)
        {
            return Commands.Send(this, command, args);
        }

        /// <summary>
        /// Process a single command synchronously. Note: This method will block until the process is complete.
        /// </summary>
        /// <param name="command">Command.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="retryLimit">The retry limit.</param>
        /// <param name="args">OSC message arguments</param>
        /// <returns>The result of the process.</returns>
        public CommunicationProcessResult SendCommand(Command command, int timeout, int retryLimit, params object[] args)
        {
            return Commands.Send(this, command, timeout, retryLimit, args);
        }

        /// <summary>
        /// Process a single command synchronously. Note: This method will block until the process is complete.
        /// </summary>
        /// <param name="reporter">Progress reporter.</param>
        /// <param name="command">Command.</param>
        /// <returns>The result of the process.</returns>
        public CommunicationProcessResult SendCommand(IReporter reporter, Command command)
        {
            return Commands.Send(this, reporter, command);
        }

        /// <summary>
        /// Process a single command synchronously. Note: This method will block until the process is complete.
        /// </summary>
        /// <param name="reporter">Progress reporter.</param>
        /// <param name="command">Command.</param>
        /// <param name="args">OSC message arguments</param>
        /// <returns>The result of the process.</returns>
        public CommunicationProcessResult SendCommand(IReporter reporter, Command command, params object[] args)
        {
            return Commands.Send(this, reporter, command, args);
        }

        /// <summary>
        /// Process a single command synchronously. Note: This method will block until the process is complete.
        /// </summary>
        /// <param name="command">Command.</param>
        /// <returns>The result of the process.</returns>
        public CommunicationProcessResult SendCommand<T>(Command command, out T result)
        {
            return Commands.Send(this, command, out result);
        }

        /// <summary>
        /// Process a single command synchronously. Note: This method will block until the process is complete.
        /// </summary>
        /// <param name="command">Command.</param>
        /// <param name="args">OSC message arguments</param>
        /// <returns>The result of the process.</returns>
        public CommunicationProcessResult SendCommand<T>(Command command, out T result, params object[] args)
        {
            return Commands.Send(this, command, out result, args);
        }

        /// <summary>
        /// Process a single command synchronously. Note: This method will block until the process is complete.
        /// </summary>
        /// <param name="reporter">Progress reporter.</param>
        /// <param name="command">Command.</param>
        /// <returns>The result of the process.</returns>
        public CommunicationProcessResult SendCommand<T>(IReporter reporter, Command command, out T result)
        {
            return Commands.Send(this, reporter, command, out result);
        }

        /// <summary>
        /// Process a single command synchronously. Note: This method will block until the process is complete.
        /// </summary>
        /// <param name="reporter">Progress reporter.</param>
        /// <param name="command">Command.</param>
        /// <param name="args">OSC message arguments</param>
        /// <returns>The result of the process.</returns>
        public CommunicationProcessResult SendCommand<T>(IReporter reporter, Command command, out T result, params object[] args)
        {
            return Commands.Send(this, reporter, command, out result, args);
        }

        /// <summary>
        /// Attach an event listener on to the given address.
        /// </summary>
        /// <param name="address">The address of the container.</param>
        /// <param name="event">The event to attach.</param>
        public void Attach(string address, OscMessageEvent @event)
        {
            oscAddressManager.Attach(address, @event);
        }

        /// <summary>
        /// Detach an event listener.
        /// </summary>
        /// <param name="address">The address of the container.</param>
        /// <param name="event">The event to remove.</param>
        public void Detach(string address, OscMessageEvent @event)
        {
            oscAddressManager.Detach(address, @event);
        }

        void OnUnknownAddress(object sender, UnknownAddressEventArgs e)
        {
            if (UnknownAddress != null)
            {
                UnknownAddress(this, e);
            }
        }

        internal void PacketReceived(OscPacket packet)
        {
            if (packet.Error != OscPacketError.None)
            {
                OnError(Strings.Connection_ReceivedCorruptedPacket, packet.ErrorMessage);
            }
            else
            {
                OnReceivePacket(packet);

                try
                {
                    oscAddressManager.Invoke(packet);
                }
                catch (Exception ex)
                {
                    OnException(Strings.Connection_ErrorWhileProcessingPacket, ex);
                }
            }
        }

        #region Message Handlers

        private void OnTransmitPacket(OscPacket packet)
        {
            UnpackMessages(MessageDirection.Transmit, packet);
        }

        private void OnReceivePacket(OscPacket packet)
        {
            UnpackMessages(MessageDirection.Receive, packet);
        }

        private void UnpackMessages(MessageDirection direction, OscPacket packet)
        {
            if (packet is OscMessage)
            {
                OnMessage(direction, packet as OscMessage);
            }
            else if (packet is OscBundle)
            {
                OscBundle bundle = packet as OscBundle;

                foreach (OscPacket sub in bundle)
                {
                    UnpackMessages(direction, sub);
                }
            }
        }

        internal void OnMessage(MessageDirection direction, OscMessage message)
        {
            if (Message == null)
            {
                return;
            }

            try
            {
                Message(this, direction, message);
            }
            catch (Exception ex)
            {
                if (Message == null)
                {
                    return;
                }

                throw ex;
            }
        }

        internal void OnError(string message)
        {
            if (Error == null)
            {
                return;
            }

            try
            {
                Error(this, new MessageEventArgs(message));
            }
            catch (Exception ex)
            {
                if (Message == null)
                {
                    return;
                }

                throw ex;
            }
        }

        internal void OnError(string message, string detail)
        {
            if (Error == null)
            {
                return;
            }

            try
            {
                Error(this, new MessageEventArgs(message, detail));
            }
            catch (Exception ex)
            {
                if (Message == null)
                {
                    return;
                }

                throw ex;
            }
        }

        internal void OnException(string message, Exception exception)
        {
            if (Exception == null)
            {
                return;
            }

            try
            {
                Exception(this, new ExceptionEventArgs(message, exception));
            }
            catch (Exception ex)
            {
                if (Message == null)
                {
                    return;
                }

                throw ex;
            }
        }

        internal void OnInfo(string message)
        {
            if (Info == null)
            {
                return;
            }

            try
            {
                Info(this, new MessageEventArgs(message));
            }
            catch (Exception ex)
            {
                if (Message == null)
                {
                    return;
                }

                throw ex;
            }
        }

        private void OnConnect()
        {
            if (Connected == null)
            {
                return;
            }

            try
            {
                Connected(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                if (Message == null)
                {
                    return;
                }

                throw ex;
            }
        }

        private void OnDisconnect()
        {
            if (Disconnected == null)
            {
                return;
            }

            try
            {
                Disconnected(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                if (Message == null)
                {
                    return;
                }

                throw ex;
            }
        }

        #endregion

        /// <summary>
        /// Configures the unique UDP connection.
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="reporter">The reporter.</param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <param name="retryLimit">The retry limit.</param>
        /// <returns>UdpConnectionInfo.</returns>
        /// <exception cref="System.Exception">Device send IP address and/or port could not be configured for unique connection.</exception>
        /// <autogeneratedoc />
        /// <remarks>
        /// The device may not respond for up to 5 seconds. The total retry period must therefore be greater than 5 seconds. 
        /// </remarks>
        public static UdpConnectionInfo ConfigureUniqueUdpConnection(UdpConnectionInfo connectionInfo, IReporter reporter = null, int timeout = 500, int retryLimit = 20)
        {
            UdpConnectionInfo uniqueConnectionInfo = UdpConnectionInfo.CreateUniqueConnectionInfo(connectionInfo);

            Connection uniqueConnection = new Connection(uniqueConnectionInfo);

            try
            {
                if (reporter != null)
                {
                    uniqueConnection.Error += reporter.OnError;
                    uniqueConnection.Exception += reporter.OnException;
                    uniqueConnection.Info += reporter.OnInfo;
                    uniqueConnection.Message += reporter.OnMessage;
                }

                // connect using the correct receive Port 
                uniqueConnection.Connect();

                uniqueConnection.Settings.WifiSendIPAddress.Value = uniqueConnectionInfo.AdapterIPAddress;
                uniqueConnection.Settings.WifiSendPort.Value = (ushort)uniqueConnectionInfo.ReceivePort;

                ISettingItem[] settingsToBeWritten = new ISettingItem[] { uniqueConnection.Settings.WifiSendIPAddress, uniqueConnection.Settings.WifiSendPort };

                int retryCount = 0;

                while (true)
                {
                    if (uniqueConnection.Settings.Write(settingsToBeWritten, null, timeout, 0) == CommunicationProcessResult.Success)
                    {
                        break;
                    }
                    else if (retryCount++ <= retryLimit)
                    {
                        Commands.Send(uniqueConnection, Command.Apply, 0, 0);
                    }
                    else
                    {
                        throw new Exception("Device send IP address and/or port could not be configured for unique connection.");
                    }
                }
            }
            finally
            {
                uniqueConnection.Close();

                if (reporter != null)
                {
                    uniqueConnection.Error -= reporter.OnError;
                    uniqueConnection.Exception -= reporter.OnException;
                    uniqueConnection.Info -= reporter.OnInfo;
                    uniqueConnection.Message -= reporter.OnMessage;
                }
            }

            return uniqueConnectionInfo;
        }
    }
}
