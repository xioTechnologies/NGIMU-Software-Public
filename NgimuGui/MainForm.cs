using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;
using NgimuApi;
using NgimuApi.Maths;
using NgimuApi.SearchForConnections;
using NgimuGui.DialogsAndWindows;
using Rug.Cmd;
using Rug.Cmd.Colors;
using Rug.LiteGL.Controls;
using Rug.Osc;

namespace NgimuGui
{
    public partial class MainForm : BaseForm
    {
        #region Private Members

        private string m_Title;

        private Connection m_Connection;
        private List<ToolStripItem> m_SerialItems = new List<ToolStripItem>();

        /// <summary>
        /// Form3DView to display orientation.
        /// </summary>
        private Form3DView m_Form3DView = new Form3DView();

        private Dictionary<string, GraphWindow> m_GraphWindows = new Dictionary<string, GraphWindow>();

        private AuxiliarySerialTerminalWindow m_AuxiliarySerialTerminal;

        private DataLoggerWindow m_LogDataDialog;
        private SDCardFileConverterWindow m_ConvertSDCardFileDialog;
        private FirmwareUploaderWindow m_UploadFirmwareDialog;

        private object m_CommandProcessLock = new object();
        private List<CommandProcess> m_CommandProcesses = new List<CommandProcess>();
        private ReceivedErrorMessagesWindow receivedErrorMessagesWindow;

        #endregion

        #region Setup

        public MainForm()
        {
            RC.Verbosity = ConsoleVerbosity.Normal;
            RC.BackgroundColor = ConsoleColorExt.Black;
            RC.Theme = ConsoleColorTheme.Load(ConsoleColorDefaultThemes.Colorful);

            InitializeComponent();

            float lowSpeedTimespan = 60;
            float highSpeedTimespan = 10;

            uint graphSampleBufferSize = Options.GraphSampleBufferSize * 1000;

            m_GraphWindows.Add("Messages Per Second", new GraphWindow(
                "Messages Per Second", "Messages per second",
                new AxesRange(0, 0, lowSpeedTimespan, 1000),
                false,
                new Graph.Trace() { Color = Colors.Yellow, Name = "Messages Per Second", MaxDataPoints = graphSampleBufferSize }
                ));


            m_GraphWindows.Add("Gyroscope", new GraphWindow(
                "Gyroscope", "Angular velocity (°/s)",
                new AxesRange(0, -10, highSpeedTimespan, 10),
                true,
                new Graph.Trace() { Color = Colors.Red, Name = "X", MaxDataPoints = graphSampleBufferSize },
                new Graph.Trace() { Color = Colors.Green, Name = "Y", MaxDataPoints = graphSampleBufferSize },
                new Graph.Trace() { Color = Colors.Blue, Name = "Z", MaxDataPoints = graphSampleBufferSize },
                new Graph.Trace() { Color = Colors.White, Name = "Magnitude", MaxDataPoints = graphSampleBufferSize }
                ));

            m_GraphWindows.Add("Accelerometer", new GraphWindow(
                "Accelerometer", "Acceleration (g)",
                new AxesRange(0, -10, highSpeedTimespan, 10),
                true,
                new Graph.Trace() { Color = Colors.Red, Name = "X", MaxDataPoints = graphSampleBufferSize },
                new Graph.Trace() { Color = Colors.Green, Name = "Y", MaxDataPoints = graphSampleBufferSize },
                new Graph.Trace() { Color = Colors.Blue, Name = "Z", MaxDataPoints = graphSampleBufferSize },
                new Graph.Trace() { Color = Colors.White, Name = "Magnitude", MaxDataPoints = graphSampleBufferSize }
                ));

            m_GraphWindows.Add("Magnetometer", new GraphWindow(
                "Magnetometer", "Intensity (uT)",
                new AxesRange(0, -10, highSpeedTimespan, 10),
                true,
                new Graph.Trace() { Color = Colors.Red, Name = "X", MaxDataPoints = graphSampleBufferSize },
                new Graph.Trace() { Color = Colors.Green, Name = "Y", MaxDataPoints = graphSampleBufferSize },
                new Graph.Trace() { Color = Colors.Blue, Name = "Z", MaxDataPoints = graphSampleBufferSize },
                new Graph.Trace() { Color = Colors.White, Name = "Magnitude", MaxDataPoints = graphSampleBufferSize }
                ));

            m_GraphWindows.Add("Barometer", new GraphWindow(
                "Barometer", "Pressure (hPa)",
                new AxesRange(0, -10, highSpeedTimespan, 10),
                false,
                new Graph.Trace() { Color = Colors.Yellow, Name = "Barometer", MaxDataPoints = graphSampleBufferSize }
                ));

            m_GraphWindows.Add("Euler Angles", new GraphWindow(
                "Euler Angles", "Angle (°)",
                new AxesRange(0, -10, highSpeedTimespan, 10),
                true,
                new Graph.Trace() { Color = Colors.Red, Name = "Roll", MaxDataPoints = graphSampleBufferSize },
                new Graph.Trace() { Color = Colors.Green, Name = "Pitch", MaxDataPoints = graphSampleBufferSize },
                new Graph.Trace() { Color = Colors.Blue, Name = "Yaw", MaxDataPoints = graphSampleBufferSize }
                ));

            m_GraphWindows.Add("Linear Acceleration", new GraphWindow(
                "Linear Acceleration", "Acceleration (g)",
                new AxesRange(0, -10, highSpeedTimespan, 10),
                true,
                new Graph.Trace() { Color = Colors.Red, Name = "X", MaxDataPoints = graphSampleBufferSize },
                new Graph.Trace() { Color = Colors.Green, Name = "Y", MaxDataPoints = graphSampleBufferSize },
                new Graph.Trace() { Color = Colors.Blue, Name = "Z", MaxDataPoints = graphSampleBufferSize }
                ));

            m_GraphWindows.Add("Earth Acceleration", new GraphWindow(
                "Earth Acceleration", "Acceleration (g)",
                new AxesRange(0, -10, highSpeedTimespan, 10),
                true,
                new Graph.Trace() { Color = Colors.Red, Name = "X", MaxDataPoints = graphSampleBufferSize },
                new Graph.Trace() { Color = Colors.Green, Name = "Y", MaxDataPoints = graphSampleBufferSize },
                new Graph.Trace() { Color = Colors.Blue, Name = "Z", MaxDataPoints = graphSampleBufferSize }
                ));

            m_GraphWindows.Add("Altimeter", new GraphWindow(
                "Altimeter", "Altitude (m)",
                new AxesRange(0, -100, highSpeedTimespan, 100),
                false,
                new Graph.Trace() { Color = Colors.Yellow, Name = "Altitude", MaxDataPoints = graphSampleBufferSize }
                ));

            m_GraphWindows.Add("Thermometers", new GraphWindow(
                "Thermometers", "Temperature (°C)",
                new AxesRange(0, -10, lowSpeedTimespan, 10),
                true,
                new Graph.Trace() { Color = Colors.Cyan, Name = "Processor", MaxDataPoints = graphSampleBufferSize },
                new Graph.Trace() { Color = Colors.Yellow, Name = "Sensor", MaxDataPoints = graphSampleBufferSize },
                new Graph.Trace() { Color = Colors.Magenta, Name = "Barometer", MaxDataPoints = graphSampleBufferSize }
                ));

            m_GraphWindows.Add("Humidity", new GraphWindow(
                "Humidity", "Humidity (%)",
                new AxesRange(0, 0, lowSpeedTimespan, 100),
                false,
                new Graph.Trace() { Color = Colors.Yellow, Name = "Humidity", MaxDataPoints = graphSampleBufferSize }
                ));

            m_GraphWindows.Add("Battery Percentage", new GraphWindow(
                "Battery Percentage", "Percentage (%)",
                new AxesRange(0, 0, lowSpeedTimespan, 100),
                false,
                new Graph.Trace() { Color = Colors.Yellow, Name = "Battery Percentage", MaxDataPoints = graphSampleBufferSize }
                ));

            m_GraphWindows.Add("Battery Time To Empty", new GraphWindow(
                "Battery Time To Empty", "Time (minutes)",
                new AxesRange(0, 0, lowSpeedTimespan, 800),
                false,
                new Graph.Trace() { Color = Colors.Yellow, Name = "Battery Time To Empty", MaxDataPoints = graphSampleBufferSize }
                ));

            m_GraphWindows.Add("Battery Voltage", new GraphWindow(
                "Battery Voltage", "Voltage (V)",
                new AxesRange(0, 0, lowSpeedTimespan, 5),
                false,
                new Graph.Trace() { Color = Colors.Yellow, Name = "Battery Voltage", MaxDataPoints = graphSampleBufferSize }
                ));

            m_GraphWindows.Add("Battery Current", new GraphWindow(
                "Battery Current", "Current (mA)",
                new AxesRange(0, -1000, lowSpeedTimespan, 1000),
                false,
                new Graph.Trace() { Color = Colors.Yellow, Name = "Battery Current", MaxDataPoints = graphSampleBufferSize }
                ));

            m_GraphWindows.Add("Analogue Inputs", new GraphWindow(
                "Analogue Inputs", "Voltage (V)",
                new AxesRange(0, 0, highSpeedTimespan, 3.2f),
                true,
                new Graph.Trace() { Color = Colors.Red, Name = "1", MaxDataPoints = graphSampleBufferSize },
                new Graph.Trace() { Color = Colors.Green, Name = "2", MaxDataPoints = graphSampleBufferSize },
                new Graph.Trace() { Color = Colors.Blue, Name = "3", MaxDataPoints = graphSampleBufferSize },
                new Graph.Trace() { Color = Colors.Cyan, Name = "4", MaxDataPoints = graphSampleBufferSize },
                new Graph.Trace() { Color = Colors.Yellow, Name = "5", MaxDataPoints = graphSampleBufferSize },
                new Graph.Trace() { Color = Colors.Magenta, Name = "6", MaxDataPoints = graphSampleBufferSize },
                new Graph.Trace() { Color = Colors.Orange, Name = "7", MaxDataPoints = graphSampleBufferSize },
                new Graph.Trace() { Color = Colors.White, Name = "8", MaxDataPoints = graphSampleBufferSize }
                ));

            m_GraphWindows.Add("RSSI Power", new GraphWindow(
                "RSSI Power", "Power (dBm)",
                new AxesRange(0, -60, lowSpeedTimespan, 0),
                false,
                new Graph.Trace() { Color = Colors.Yellow, Name = "Power", MaxDataPoints = graphSampleBufferSize }
                ));


            m_GraphWindows.Add("RSSI Percentage", new GraphWindow(
                "RSSI Percentage", "Percentage (%)",
                new AxesRange(0, 0, lowSpeedTimespan, 100),
                false,
                new Graph.Trace() { Color = Colors.Yellow, Name = "Percentage (%)", MaxDataPoints = graphSampleBufferSize }
                ));
        }

        #endregion

        #region Form Load / Closing

        private void MainForm_Load(object sender, EventArgs e)
        {
            //using (Scope2D.GraphWindow testForm = new Scope2D.GraphWindow())
            //{
            //    testForm.ShowDialog(); 
            //}

            //Close();

            //return; 

            m_BatteryChargerStatusLabel.Text = "";

            if (Options.Bounds != Rectangle.Empty)
            {
                this.DesktopBounds = Options.Bounds;
            }

            WindowState = Options.WindowState;

            m_Title = typeof(MainForm).Assembly.GetName().Name + " v" + typeof(MainForm).Assembly.GetName().Version.Major + "." + typeof(MainForm).Assembly.GetName().Version.Minor;

            this.Text = m_Title + " - Not Connected";

            List<ToolStripMenuItem> commandItems = new List<ToolStripMenuItem>();

            foreach (CommandMetaData command in Commands.GetCommands())
            {
                if (command.IsVisible == false)
                {
                    continue;
                }

                ToolStripMenuItem menuItem = new ToolStripMenuItem(command.Text)
                {
                    Tag = command,
                };

                menuItem.Click += delegate (object menuSender, EventArgs args)
                {
                    if (m_Connection == null)
                    {
                        return;
                    }

                    ToolStripMenuItem item = menuSender as ToolStripMenuItem;

                    CommandMetaData comm = item.Tag as CommandMetaData;

                    lock (m_CommandProcessLock)
                    {
                        Reporter reporter = new Reporter();
                        CommandProcess process = new CommandProcess(m_Connection, reporter, new CommandCallback(comm.Message), Options.Timeout, Options.MaximumNumberOfRetries);
                        m_CommandProcesses.Add(process);

                        reporter.Info += new EventHandler<MessageEventArgs>(Process_Info);
                        reporter.Error += new EventHandler<MessageEventArgs>(Process_Error);

                        if (comm.DisconnectSerialOnSuccess == true &&
                            Options.DisconnectFromSerialAfterSleepAndResetCommands == true &&
                            m_Connection.ConnectionType == ConnectionType.Serial)
                        {
                            reporter.Completed += new EventHandler(Process_Stopped_DisconnectSerialOnSuccess);
                        }
                        else
                        {
                            reporter.Completed += new EventHandler(Process_Stopped);
                        }

                        process.SendAsync();
                    }
                };

                commandItems.Add(menuItem);
            }

            m_SendCommandMenuItem.DropDownItems.AddRange(commandItems.ToArray());

            m_ConnectSerialMenuItem.DropDownOpening += new EventHandler(ConnectSerialMenuItem_DropDownOpening);

            ConnectSerialMenuItem_DropDownOpening(null, EventArgs.Empty);

            UdpConnectionInfo defaultConnection = new UdpConnectionInfo();

            Size size;

            m_ConnectUdpMenuItem.DropDownItems.Insert(0, new ToolStripMenuItem(defaultConnection.ToString(), null, ConnectMenuItem_Click) { Tag = defaultConnection });

            m_ConnectUdpMenuItem.DropDownOpening += new EventHandler(ConnectUdpMenuItem_DropDownOpening);

            m_TerminalPanel.PacketRecived += new OscPacketEvent(Terminal_PacketRecived);

            m_SettingsPanel.ReadWriteStateChanged += new EventHandler(Settings_ReadWriteStateChanged);

            RC.WriteLine();

            UncheckAllConnections();

            m_TerminalPanel.HistoryLength = 1024;

            foreach (ToolStripItem item in this.m_GraphsMenuItem.DropDownItems)
            {
                if (string.IsNullOrEmpty(item.Text) == true)
                {
                    continue;
                }

                item.Name = (string)item.Tag;
            }

            foreach (string name in m_GraphWindows.Keys)
            {
                GraphWindow graph = m_GraphWindows[name];

                ToolStripItem item = this.m_GraphsMenuItem.DropDownItems[name];

                if (item == null)
                {
                    continue;
                }

                ToolStripMenuItem toolStripMenuItem = item as ToolStripMenuItem;

                toolStripMenuItem.Checked = Options.Windows[name].IsOpen;

                graph.VisibleChanged += new EventHandler(delegate (object s, EventArgs a)
                {
                    toolStripMenuItem.Checked = graph.Visible;
                });
            }

            m_Form3DView.VisibleChanged += Form3DView_FormClosed;

            m_Form3DViewMenuItem.Checked = Options.Windows[m_Form3DView.ID].IsOpen;
            m_AuxSerialTerminalMenuItem.Checked = Options.Windows[AuxiliarySerialTerminalWindow.ID].IsOpen;
            m_UploadFirmwareMenuItem.Checked = Options.Windows[FirmwareUploaderWindow.ID].IsOpen;
            m_LogDataMenuItem.Checked = Options.Windows[DataLoggerWindow.ID].IsOpen;
            m_ConvertSDCardFile.Checked = Options.Windows[SDCardFileConverterWindow.ID].IsOpen;

            if (Options.SearchForConnectionsAtStartup == true)
            {
                SearchForConnectionsItem_Click(null, EventArgs.Empty);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_Connection != null)
            {
                CloseConnection();
            }

            foreach (GraphWindow graph in m_GraphWindows.Values)
            {
                graph.Dispose();
            }

            m_Form3DView.Dispose();


            if (m_LogDataDialog != null)
            {
                m_LogDataDialog.Dispose();
            }

            if (m_UploadFirmwareDialog != null)
            {
                m_UploadFirmwareDialog.Dispose();
            }

            if (m_AuxiliarySerialTerminal != null)
            {
                m_AuxiliarySerialTerminal.Dispose();
            }

            if (m_ConvertSDCardFileDialog != null)
            {
                m_ConvertSDCardFileDialog.Dispose();
            }
        }

        #endregion

        #region Connect / Disconnect

        private void ConnectTo(ToolStripMenuItem item)
        {
            ReceivedErrorMessages.Clear();

            m_BatteryChargerStatusLabel.Text = "";

            string existingConnection = null;

            if (m_Connection != null)
            {
                existingConnection = m_Connection.ID;
            }

            CloseConnection();

            if (item.Tag != null && item.Tag is UdpConnectionInfo)
            {
                UdpConnectionInfo info = item.Tag as UdpConnectionInfo;

                if (info.ToIDString() == existingConnection)
                {
                    return;
                }

                m_Connection = new Connection(info);
            }
            else if (item.Tag != null && item.Tag is SerialConnectionInfo)
            {
                SerialConnectionInfo info = item.Tag as SerialConnectionInfo;

                if (info.ToIDString() == existingConnection)
                {
                    return;
                }

                m_Connection = new Connection(info);
            }
            else
            {
                return;
            }

            AttachReceiveEvents();

            m_Connection.Tag = item;

            try
            {
                m_Connection.Connect();
            }
            catch
            {
                //GuiTerminal.WriteException(ex);

                ActiveConnection_Disconnected(m_Connection, EventArgs.Empty);
            }
        }

        private void CloseConnection()
        {
            m_BatteryChargerStatusLabel.Text = "";

            if (m_Connection == null)
            {
                return;
            }

            if (m_UploadFirmwareDialog != null && m_UploadFirmwareDialog.IsRunning == false)
            {
                m_UploadFirmwareDialog.Stop();
                m_UploadFirmwareDialog.ActiveConnection = null;
            }

            if (m_LogDataDialog != null)
            {
                m_LogDataDialog.Stop();
                m_LogDataDialog.ActiveConnection = null;
            }

            m_Connection.Dispose();

            m_Connection = null;

            //Text
        }

        private void UncheckAllConnections()
        {
            m_ReadFromDeviceMenuItem.Enabled = false;
            m_WriteToDeviceMenuItem.Enabled = false;
            m_LoadDefaultsMenuItem.Enabled = false;

            m_ConnectUdpMenuItem.CheckState = CheckState.Unchecked;
            m_ConnectSerialMenuItem.CheckState = CheckState.Unchecked;

            foreach (ToolStripItem item in m_ConnectSerialMenuItem.DropDownItems)
            {
                if (item is ToolStripMenuItem)
                {
                    (item as ToolStripMenuItem).Checked = false;
                }
            }

            foreach (ToolStripMenuItem item in m_ConnectUdpMenuItem.DropDownItems)
            {
                item.Checked = false;
            }
        }

        #endregion

        #region ActiveConnection Event Setup

        private void AttachReceiveEvents()
        {
            m_Connection.Connected += new EventHandler(ActiveConnection_Connected);
            m_Connection.Disconnected += new EventHandler(ActiveConnection_Disconnected);
            m_Connection.Error += new EventHandler<MessageEventArgs>(ActiveConnection_Error);
            m_Connection.Exception += new EventHandler<ExceptionEventArgs>(ActiveConnection_Exception);
            m_Connection.Info += new EventHandler<MessageEventArgs>(ActiveConnection_Info);
            m_Connection.Message += new MessageEvent(ActiveConnection_Message);

            m_Connection.Altitude.Received += new EventHandler(Altitude_Received);
            m_Connection.AnalogueInputs.Received += new EventHandler(AnalogueInputs_Updated);
            m_Connection.Battery.Received += new EventHandler(Battery_Updated);
            m_Connection.LinearAcceleration.Received += new EventHandler(LinearAcceleration_Updated);
            m_Connection.Magnitudes.Received += Magnitudes_Received;

            m_Connection.EarthAcceleration.Received += new EventHandler(EarthAcceleration_Updated);
            m_Connection.Sensors.Received += new EventHandler(Sensors_Updated);
            m_Connection.Temperature.Received += new EventHandler(Temperature_Updated);
            m_Connection.AuxiliarySerial.Received += new EventHandler(AuxiliarySerial_Updated);
            m_Connection.AuxiliarySerialCts.Received += new EventHandler(AuxiliarySerialCts_Updated);
            m_Connection.Quaternion.Received += new EventHandler(Quaternion_Updated);
            m_Connection.RotationMatrix.Received += new EventHandler(RotationMatrix_Updated);
            m_Connection.EulerAngles.Received += new EventHandler(EulerAngles_Updated);
            m_Connection.Humidity.Received += new EventHandler(Humidity_Updated);
            m_Connection.Rssi.Received += new EventHandler(Rssi_Updated);

            m_Connection.DeviceError.Received += new EventHandler(DeviceError_Received);

            if (m_LogDataDialog != null)
            {
                m_LogDataDialog.ActiveConnection = m_Connection;
            }

            if (m_UploadFirmwareDialog != null)
            {
                m_UploadFirmwareDialog.ActiveConnection = m_Connection;
            }
        }

        void DeviceError_Received(object sender, EventArgs e)
        {
            ReceivedErrorMessages.Add(m_Connection.DeviceError.Message);

            if (Options.DisplayReceivedErrorMessagesInMessageBox == false)
            {
                return;
            }

            if (receivedErrorMessagesWindow != null)
            {
                return;
            }

            Invoke(new EventHandler(ShowReceivedErrorMessagesWindow), sender, e);

        }

        void ShowReceivedErrorMessagesWindow(object sender, EventArgs e)
        {
            if (receivedErrorMessagesWindow != null)
            {
                return;
            }

            receivedErrorMessagesWindow = new ReceivedErrorMessagesWindow();
            receivedErrorMessagesWindow.FormClosed += ReceivedErrorMessagesWindow_FormClosed;
            receivedErrorMessagesWindow.StartPosition = FormStartPosition.CenterParent;
            receivedErrorMessagesWindow.Show(this);
        }

        private void ReceivedErrorMessagesWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            receivedErrorMessagesWindow.Dispose();
            receivedErrorMessagesWindow = null;
        }

        void ActiveConnection_Connected(object sender, EventArgs e)
        {
            if (m_Connection.ConnectionType == ConnectionType.Udp)
            {
                m_ConnectUdpMenuItem.CheckState = CheckState.Indeterminate;
            }
            else if (m_Connection.ConnectionType == ConnectionType.Serial)
            {
                m_ConnectSerialMenuItem.CheckState = CheckState.Indeterminate;
            }

            m_SettingsPanel.OnConnect(m_Connection);

            m_FastTimer.Enabled = true;
            m_DisconnectMenuItem.Enabled = true;

            m_ReadFromDeviceMenuItem.Enabled = true;
            m_WriteToDeviceMenuItem.Enabled = true;
            m_LoadDefaultsMenuItem.Enabled = true;

            this.Text = m_Title + " - " + m_Connection.Name;

            ((sender as Connection).Tag as ToolStripMenuItem).Checked = true;

            if (Options.ReadDeviceSettingsAfterOpeningConnection == true)
            {
                // Read all the settings
                m_SettingsPanel.ReadFromDevice();
            }
        }

        void ActiveConnection_Disconnected(object sender, EventArgs e)
        {
            if (InvokeRequired == true)
            {
                Invoke(new EventHandler(ActiveConnection_Disconnected), sender, e);
            }
            else
            {
                m_DisconnectMenuItem.Enabled = false;

                if (m_UploadFirmwareDialog != null && m_UploadFirmwareDialog.IsRunning == false)
                {
                    m_UploadFirmwareDialog.Stop();
                    m_UploadFirmwareDialog.ActiveConnection = null;
                }

                if (m_LogDataDialog != null)
                {
                    m_LogDataDialog.Stop();
                    m_LogDataDialog.ActiveConnection = null;
                }

                UncheckAllConnections();

                m_SettingsPanel.OnDisconnect();

                m_Connection = null;

                this.Text = m_Title + " - Not Connected";
            }
        }

        #endregion

        #region Message Handlers

        void ActiveConnection_Message(Connection source, MessageDirection direction, OscMessage message)
        {
            ActiveConnection_Message_Inner(source, direction, message);
        }

        void ActiveConnection_Message_Inner(Connection source, MessageDirection direction, OscMessage message)
        {
            if (message.Error != OscPacketError.None)
            {
                return;
            }

            if (direction == MessageDirection.Receive)
            {
                filterPanel1.AddFilter(message);
            }

            if (filterPanel1.ShouldWriteMessage(message.Address) == true)
            {
                GuiTerminal.WriteMessage(direction, message.TimeTag, message.ToString());
            }
        }

        void ActiveConnection_Info(object sender, MessageEventArgs e)
        {
            if (InvokeRequired == true)
            {
                BeginInvoke(new Action<string, string>(GuiTerminal.WriteInfo), e.Message, e.Detail);
            }
            else
            {
                GuiTerminal.WriteInfo(e.Message, e.Detail);
            }
        }

        void ActiveConnection_Error(object sender, MessageEventArgs e)
        {
            if (InvokeRequired == true)
            {
                BeginInvoke(new Action<string, string>(GuiTerminal.WriteError), e.Message, e.Detail);
            }
            else
            {
                GuiTerminal.WriteError(e.Message, e.Detail);
            }
        }

        void ActiveConnection_Exception(object sender, ExceptionEventArgs e)
        {
            if (InvokeRequired == true)
            {
                BeginInvoke(new Action<string, Exception>(GuiTerminal.WriteException), e.Message, e.Exception);
            }
            else
            {
                GuiTerminal.WriteException(e.Message, e.Exception);
            }
        }

        void Sensors_Updated(object sender, EventArgs e)
        {
            DateTime timestamp = m_Connection.Sensors.Timestamp.ToDataTime();

            AddGraphData("Gyroscope", timestamp, m_Connection.Sensors.Gyroscope.X, m_Connection.Sensors.Gyroscope.Y, m_Connection.Sensors.Gyroscope.Z);
            AddGraphData("Accelerometer", timestamp, m_Connection.Sensors.Accelerometer.X, m_Connection.Sensors.Accelerometer.Y, m_Connection.Sensors.Accelerometer.Z);
            AddGraphData("Magnetometer", timestamp, m_Connection.Sensors.Magnetometer.X, m_Connection.Sensors.Magnetometer.Y, m_Connection.Sensors.Magnetometer.Z);

            AddGraphData("Barometer", timestamp, m_Connection.Sensors.Barometer);
        }

        void Altitude_Received(object sender, EventArgs e)
        {
            AddGraphData("Altimeter", m_Connection.Altitude.Timestamp.ToDataTime(), m_Connection.Altitude.Altitude);
        }

        void AnalogueInputs_Updated(object sender, EventArgs e)
        {

            AddGraphData("Analogue Inputs",
                m_Connection.AnalogueInputs.Timestamp.ToDataTime(),
                m_Connection.AnalogueInputs.Channel1,
                m_Connection.AnalogueInputs.Channel2,
                m_Connection.AnalogueInputs.Channel3,
                 m_Connection.AnalogueInputs.Channel4,
                 m_Connection.AnalogueInputs.Channel5,
                 m_Connection.AnalogueInputs.Channel6,
                 m_Connection.AnalogueInputs.Channel7,
                 m_Connection.AnalogueInputs.Channel8
                 );
        }

        void LinearAcceleration_Updated(object sender, EventArgs e)
        {
            AddGraphData("Linear Acceleration", m_Connection.LinearAcceleration.Timestamp.ToDataTime(), m_Connection.LinearAcceleration.LinearAcceleration.X, m_Connection.LinearAcceleration.LinearAcceleration.Y, m_Connection.LinearAcceleration.LinearAcceleration.Z);
        }

        private void Magnitudes_Received(object sender, EventArgs e)
        {
            DateTime timestamp = m_Connection.Magnitudes.Timestamp.ToDataTime();

            AddGraphData("Gyroscope", timestamp, 3, m_Connection.Magnitudes.Gyroscope);
            AddGraphData("Accelerometer", timestamp, 3, m_Connection.Magnitudes.Accelerometer);
            AddGraphData("Magnetometer", timestamp, 3, m_Connection.Magnitudes.Magnetometer);
        }

        void EarthAcceleration_Updated(object sender, EventArgs e)
        {
            AddGraphData("Earth Acceleration", m_Connection.EarthAcceleration.Timestamp.ToDataTime(), m_Connection.EarthAcceleration.EarthAcceleration.X, m_Connection.EarthAcceleration.EarthAcceleration.Y, m_Connection.EarthAcceleration.EarthAcceleration.Z);
        }

        void Temperature_Updated(object sender, EventArgs e)
        {

            AddGraphData("Thermometers", m_Connection.Temperature.Timestamp.ToDataTime(), m_Connection.Temperature.ProcessorTemperature, m_Connection.Temperature.SensorTemperature, m_Connection.Temperature.BarometerTemperature);
        }

        void AuxiliarySerial_Updated(object sender, EventArgs e)
        {
            if (m_AuxiliarySerialTerminal != null)
            {
                if (m_Connection.AuxiliarySerial.IsDataAString == false)
                {
                    m_AuxiliarySerialTerminal.OnData(m_Connection.AuxiliarySerial.Data);
                }
                else
                {
                    m_AuxiliarySerialTerminal.OnData(m_Connection.AuxiliarySerial.StringData);
                }
            }
        }

        void AuxiliarySerialCts_Updated(object sender, EventArgs e)
        {
            if (m_AuxiliarySerialTerminal != null)
            {
                m_AuxiliarySerialTerminal.CtsValue = m_Connection.AuxiliarySerialCts.State;
            }
        }

        void Quaternion_Updated(object sender, EventArgs e)
        {
            m_Form3DView.Quaternion = m_Connection.Quaternion.Quaternion;

            EulerAngles eulerAngles = Quaternion.ToEulerAngles(m_Connection.Quaternion.Quaternion);
            AddGraphData("Euler Angles", m_Connection.Quaternion.Timestamp.ToDataTime(), eulerAngles.Roll, eulerAngles.Pitch, eulerAngles.Yaw);
        }

        void RotationMatrix_Updated(object sender, EventArgs e)
        {
            m_Form3DView.RotationMatrix = m_Connection.RotationMatrix.RotationMatrix;

            EulerAngles eulerAngles = Quaternion.ToEulerAngles(Quaternion.FromRotationMatrix(m_Connection.RotationMatrix.RotationMatrix));
            AddGraphData("Euler Angles", m_Connection.RotationMatrix.Timestamp.ToDataTime(), m_Connection.EulerAngles.EulerAngles.Roll, m_Connection.EulerAngles.EulerAngles.Pitch, m_Connection.EulerAngles.EulerAngles.Yaw);
        }

        void EulerAngles_Updated(object sender, EventArgs e)
        {
            m_Form3DView.EulerAngles = m_Connection.EulerAngles.EulerAngles;

            AddGraphData("Euler Angles", m_Connection.EulerAngles.Timestamp.ToDataTime(), m_Connection.EulerAngles.EulerAngles.Roll, m_Connection.EulerAngles.EulerAngles.Pitch, m_Connection.EulerAngles.EulerAngles.Yaw);
        }

        void Battery_Updated(object sender, EventArgs e)
        {
            DateTime timestamp = m_Connection.Battery.Timestamp.ToDataTime();

            AddGraphData("Battery Percentage", timestamp, m_Connection.Battery.Percentage);
            AddGraphData("Battery Time To Empty", timestamp, (float)m_Connection.Battery.TimeToEmpty.TotalMinutes);
            AddGraphData("Battery Voltage", timestamp, m_Connection.Battery.Voltage);
            AddGraphData("Battery Current", timestamp, m_Connection.Battery.Current);
        }

        void Humidity_Updated(object sender, EventArgs e)
        {
            AddGraphData("Humidity", m_Connection.Humidity.Timestamp.ToDataTime(), m_Connection.Humidity.Humidity);
        }

        private void Rssi_Updated(object sender, EventArgs e)
        {
            DateTime timestamp = m_Connection.Rssi.Timestamp.ToDataTime();

            AddGraphData("RSSI Power", timestamp, m_Connection.Rssi.Power);
            AddGraphData("RSSI Percentage", timestamp, m_Connection.Rssi.Percentage);
        }

        #endregion

        #region Update Statistics

        private void UpdateStatistics()
        {
            long totalReceived = 0;
            float receiveRate = 0;

            long receiveErrors = 0;

            long totalSent = 0;
            float sendRate = 0;

            if (m_Connection != null)
            {
                totalReceived = m_Connection.CommunicationStatistics.MessagesReceived.Total;
                receiveRate = m_Connection.CommunicationStatistics.MessagesReceived.Rate;

                receiveErrors = m_Connection.CommunicationStatistics.ReceiveErrors.Total;

                totalSent = m_Connection.CommunicationStatistics.MessagesSent.Total;
                sendRate = m_Connection.CommunicationStatistics.MessagesSent.Rate;
            }

            m_TotalReceived.Text = String.Format((string)m_TotalReceived.Tag, totalReceived.ToString(CultureInfo.InvariantCulture));
            m_ReceiveRate.Text = String.Format((string)m_ReceiveRate.Tag, receiveRate.ToString("F0", CultureInfo.InvariantCulture));

            m_TotalSent.Text = String.Format((string)m_TotalSent.Tag, totalSent.ToString(CultureInfo.InvariantCulture));
            m_SendRate.Text = String.Format((string)m_SendRate.Tag, sendRate.ToString("F0", CultureInfo.InvariantCulture));

            if (m_Connection != null)
            {
                m_BatteryStatusLabel.Text = String.Format((string)m_BatteryStatusLabel.Tag, m_Connection.Battery.Percentage.ToString("F0", CultureInfo.InvariantCulture));

                if (m_Connection.Battery.IsChargerConnected == true)
                {
                    m_BatteryChargerStatusLabel.Text = String.IsNullOrEmpty(m_Connection.Battery.ChargerState) == false ? "(" + m_Connection.Battery.ChargerState + ")" : String.Empty;
                }
                else
                {
                    m_BatteryChargerStatusLabel.Text = "(" + NgimuApi.Helper.TimeSpanToString(m_Connection.Battery.TimeToEmpty, TimeSpanStringFormat.Longhand) + " remaining)";
                }
            }
        }

        #endregion

        #region Panel Events

        void Settings_ReadWriteStateChanged(object sender, EventArgs e)
        {
            m_SettingsTab.Text = "Settings" + (m_SettingsPanel.NeedsWrite == true ? "*" : "");
        }

        void Terminal_PacketRecived(OscPacket packet)
        {
            if (m_Connection != null)
            {
                try
                {
                    m_Connection.Send(packet);
                }
                catch (Exception ex)
                {
                    GuiTerminal.WriteException(ex);
                }
            }
            else
            {
                GuiTerminal.WriteError("Unable to send. Not connected.");
            }
        }

        #endregion

        #region Timer Events

        private void FastTimer_Tick(object sender, EventArgs e)
        {
            UpdateStatistics();
        }

        private void SlowTimer_Tick(object sender, EventArgs e)
        {
            float receiveRate = 0;

            if (m_Connection != null)
            {
                receiveRate = m_Connection.CommunicationStatistics.MessagesReceived.Rate;
            }

            AddGraphData("Messages Per Second", DateTime.Now, receiveRate);

            if (m_Connection == null)
            {
                return;
            }

            this.Text = m_Title + " - " + m_Connection.Name;

            m_Connection.CheckConnectionState();

            filterPanel1.CalculateRate();
        }

        #endregion

        #region Menu Items

        void ConnectUdpMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (ToolStripMenuItem item in m_ConnectUdpMenuItem.DropDownItems)
            {
                if (item.Tag == null)
                {
                    continue;
                }

                UdpConnectionInfo info = item.Tag as UdpConnectionInfo;

                info.RefreshAdapterAddress(interfaces);

                item.Text = info.ToString();
            }
        }

        void ConnectSerialMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            SerialConnectionInfo currentConnection = null;

            if (m_Connection != null &&
                m_Connection.ConnectionInfo is SerialConnectionInfo)
            {
                currentConnection = m_Connection.ConnectionInfo as SerialConnectionInfo;
            }

            string selected = null;
            foreach (ToolStripItem item in m_ConnectSerialMenuItem.DropDownItems)
            {
                if (item is ToolStripMenuItem &&
                    (item as ToolStripMenuItem).Checked == true)
                {
                    selected = item.Text;
                }
            }

            m_ConnectSerialMenuItem.DropDownItems.Clear();

            List<SerialConnectionInfo> defaultInfos = new List<SerialConnectionInfo>();

            foreach (string str in NgimuApi.Helper.GetSerialPortNames())
            {
                defaultInfos.Add(new SerialConnectionInfo()
                {
                    PortName = str,
                    BaudRate = 115200,
                    RtsCtsEnabled = false,
                });
            }

            int i = 0;
            foreach (SerialConnectionInfo info in defaultInfos)
            {
                m_ConnectSerialMenuItem.DropDownItems.Insert(i++, new ToolStripMenuItem(info.ToString(), null, new EventHandler(ConnectMenuItem_Click)) { Checked = info.Equals(currentConnection), Tag = info });
            }

            if (m_ConnectSerialMenuItem.DropDownItems.Count == 0)
            {
                m_ConnectSerialMenuItem.DropDownItems.Insert(0, new ToolStripMenuItem("No Serial Ports Available") { Enabled = false });
            }

            m_ConnectSerialMenuItem.DropDownItems.Add(m_SerialSeparator);

            m_ConnectSerialMenuItem.DropDownItems.AddRange(m_SerialItems.ToArray());

            m_ConnectSerialMenuItem.DropDownItems.Add(m_OpenSerialConnectionDialog);
        }

        private void ConnectMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;

            ConnectTo(item);
        }

        private void OpenUdpConnectionDialog_Click(object sender, EventArgs e)
        {
            using (UdpConnectionDialog dialog = new UdpConnectionDialog())
            {
                if (dialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    UdpConnectionInfo info = new UdpConnectionInfo()
                    {
                        AdapterName = dialog.AdapterName,
                        NetworkAdapter = dialog.NetworkAdapter,
                        AdapterIPAddress = dialog.AdapterIPAddress,

                        SendIPAddress = dialog.SendIPAddress,
                        SendPort = (int)dialog.SendPort,

                        ReceivePort = (int)dialog.ReceivePort
                    };

                    ConnectUdp(info);
                }
            }
        }

        private void ConnectUdp(UdpConnectionInfo info)
        {
            ToolStripMenuItem foundItem = null;

            foreach (ToolStripMenuItem item in m_ConnectUdpMenuItem.DropDownItems)
            {
                if (item.Tag == null)
                {
                    continue;
                }

                if (info.Equals(item.Tag))
                {
                    foundItem = item;

                    break;
                }
            }

            if (foundItem == null)
            {
                foundItem = new ToolStripMenuItem(info.ToString(), null, ConnectMenuItem_Click) { Tag = info };

                m_ConnectUdpMenuItem.DropDownItems.Insert(0, foundItem);
            }

            if (foundItem.Checked == false)
            {
                ConnectTo(foundItem);
            }
        }


        private void OpenSerialConnectionDialog_Click(object sender, EventArgs e)
        {
            using (SerialConnectionDialog dialog = new SerialConnectionDialog())
            {
                if (dialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    SerialConnectionInfo info = new SerialConnectionInfo()
                    {
                        PortName = dialog.PortName,
                        BaudRate = dialog.BaudRate,
                        RtsCtsEnabled = dialog.RtsCtsEnabled,
                    };

                    ConnectSerial(info);
                }
            }
        }

        private void ConnectSerial(SerialConnectionInfo info)
        {
            ToolStripMenuItem foundItem = null;

            foreach (ToolStripItem item in m_ConnectSerialMenuItem.DropDownItems)
            {
                if (item.Tag == null)
                {
                    continue;
                }

                if (!(item is ToolStripMenuItem))
                {
                    continue;
                }

                if (info.Equals(item.Tag))
                {
                    foundItem = item as ToolStripMenuItem;

                    break;
                }
            }

            if (foundItem == null)
            {
                foundItem = new ToolStripMenuItem(info.ToString(), null, ConnectMenuItem_Click) { Tag = info };

                m_ConnectSerialMenuItem.DropDownItems.Insert(0, foundItem);

                m_SerialItems.Add(foundItem);
            }

            if (foundItem.Checked == false)
            {
                ConnectTo(foundItem);
            }
        }

        private void ConnectAuto(ConnectionSearchInfo info)
        {
            switch (info.ConnectionType)
            {
                case ConnectionType.Udp:
                    UdpConnectionInfo udpInfo = info.ConnectionInfo as UdpConnectionInfo;

                    if (Options.ConfigureUniqueUdpConnection == true)
                    {
                        Thread thread = new Thread(delegate ()
                        {
                            try
                            {
                                Reporter reporter = new Reporter();

                                reporter.Error += new EventHandler<MessageEventArgs>(ActiveConnection_Error);
                                reporter.Exception += new EventHandler<ExceptionEventArgs>(ActiveConnection_Exception);
                                reporter.Info += new EventHandler<MessageEventArgs>(ActiveConnection_Info);
                                reporter.Message += new MessageEvent(ActiveConnection_Message);

                                UdpConnectionInfo newInfo = Connection.ConfigureUniqueUdpConnection(udpInfo, reporter);

                                reporter.Error -= new EventHandler<MessageEventArgs>(ActiveConnection_Error);
                                reporter.Exception -= new EventHandler<ExceptionEventArgs>(ActiveConnection_Exception);
                                reporter.Info -= new EventHandler<MessageEventArgs>(ActiveConnection_Info);
                                reporter.Message -= new MessageEvent(ActiveConnection_Message);

                                Invoke(new Action<UdpConnectionInfo>(ConnectUdp), newInfo);
                            }
                            catch (Exception ex)
                            {
                                ActiveConnection_Exception(this, new ExceptionEventArgs("The device could not be configured to use a unique connection. " + ex.Message, ex));
                            }
                        });
                        thread.Name = "Configure Unique UDP Connection";
                        thread.Start();
                    }
                    else
                    {
                        ConnectUdp(udpInfo);
                    }

                    break;
                case ConnectionType.Serial:
                    ConnectSerial(info.ConnectionInfo as SerialConnectionInfo);
                    break;
                default:
                    break;
            }
        }

        private void Reconnect(UdpConnectionInfo info)
        {
            try
            {
                CloseConnection();

                ConnectUdp(info);
            }
            catch (Exception ex)
            {
                using (ExceptionDialog dialog = new ExceptionDialog())
                {
                    dialog.Title = "An Exception Occurred";

                    dialog.Label = ex.Message;
                    dialog.Detail = ex.ToString();

                    dialog.ShowDialog(this);
                }
            }
        }

        private void DisconnectMenuItem_Click(object sender, EventArgs e)
        {
            CloseConnection();
        }

        private void ClearMenuItem_Click(object sender, EventArgs e)
        {
            m_TerminalPanel.Clear();
        }

        private void ScopeMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;

            if (item.Checked == true)
            {
                ShowGraph(item.Tag.ToString());
            }
            else
            {
                HideGraph(item.Tag.ToString());
            }
        }

        private void Form3DViewMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (m_Form3DViewMenuItem.Checked)
            {
                m_Form3DView.Show();
            }
            else
            {
                m_Form3DView.Hide();
            }
        }

        private void Form3DView_FormClosed(object sender, EventArgs e)
        {
            m_Form3DViewMenuItem.Checked = m_Form3DView.Visible;
        }

        private void ReadFromDeviceMenuItem_Click(object sender, EventArgs e)
        {
            m_SettingsPanel.ReadFromDevice();
        }

        private void WriteToDeviceMenuItem_Click(object sender, EventArgs e)
        {
            m_SettingsPanel.WriteToDevice();
        }

        private void AuxSerialTerminalMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (m_AuxSerialTerminalMenuItem.Checked == true)
            {
                m_AuxiliarySerialTerminal = new AuxiliarySerialTerminalWindow();

                m_AuxiliarySerialTerminal.PacketRecived += new OscPacketEvent(Terminal_PacketRecived);

                m_AuxiliarySerialTerminal.FormClosed += new FormClosedEventHandler(AuxiliarySerialTerminal_FormClosed);

                m_AuxiliarySerialTerminal.Show();
            }
            else
            {
                m_AuxiliarySerialTerminal.Close();

                m_AuxiliarySerialTerminal.Dispose();

                m_AuxiliarySerialTerminal = null;
            }
        }

        void AuxiliarySerialTerminal_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_AuxiliarySerialTerminal.PacketRecived -= new OscPacketEvent(Terminal_PacketRecived);

            m_AuxSerialTerminalMenuItem.Checked = false;
        }

        private void ConvertSDCardFile_CheckedChanged(object sender, EventArgs e)
        {
            if (m_ConvertSDCardFile.Checked == true)
            {
                m_ConvertSDCardFileDialog = new SDCardFileConverterWindow();

                m_ConvertSDCardFileDialog.FormClosed += new FormClosedEventHandler(ConvertSDCardFileDialog_FormClosed);

                m_ConvertSDCardFileDialog.Show();
            }
            else
            {
                m_ConvertSDCardFileDialog.Close();

                m_ConvertSDCardFileDialog.Dispose();

                m_ConvertSDCardFileDialog = null;
            }
        }

        void ConvertSDCardFileDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_ConvertSDCardFile.Checked = false;
        }

        private void UploadFirmwareMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (m_UploadFirmwareMenuItem.Checked == true)
            {
                m_UploadFirmwareDialog = new FirmwareUploaderWindow();
                m_UploadFirmwareDialog.ActiveConnection = m_Connection;
                m_UploadFirmwareDialog.FormClosed += new FormClosedEventHandler(m_UploadFirmwareDialog_FormClosed);
                m_UploadFirmwareDialog.AutoConnectRequest += SearchForConnectionsItem_Click;
                //m_UploadFirmwareDialog.StartPosition = FormStartPosition.CenterParent; 
                //m_UploadFirmwareDialog.Show(this);
                m_UploadFirmwareDialog.Show();
            }
            else
            {
                m_UploadFirmwareDialog.Close();

                m_UploadFirmwareDialog.Dispose();

                m_UploadFirmwareDialog = null;
            }
        }

        void m_UploadFirmwareDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_UploadFirmwareDialog.AutoConnectRequest -= SearchForConnectionsItem_Click;

            m_UploadFirmwareMenuItem.Checked = false;
        }

        private void LogDataMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (m_LogDataMenuItem.Checked == true)
            {
                m_LogDataDialog = new DataLoggerWindow();
                m_LogDataDialog.ActiveConnection = m_Connection;
                m_LogDataDialog.FormClosed += new FormClosedEventHandler(LogDataDialog_FormClosed);
                //m_LogDataDialog.Show(this);
                m_LogDataDialog.Show();
            }
            else
            {
                m_LogDataDialog.Close();

                m_LogDataDialog.Dispose();

                m_LogDataDialog = null;
            }
        }

        void LogDataDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_LogDataMenuItem.Checked = false;
        }

        #endregion

        #region Process Messages

        void Process_Info(object sender, MessageEventArgs e)
        {
            GuiTerminal.WriteInfo(e.Message, e.Detail);
        }

        void Process_Error(object sender, MessageEventArgs e)
        {
            GuiTerminal.WriteError(e.Message, e.Detail);
        }

        void Process_Stopped(object sender, EventArgs e)
        {
            CommandProcess process = sender as CommandProcess;

            lock (m_CommandProcessLock)
            {

                Reporter reporter = process.Reporter as Reporter;

                reporter.Info -= new EventHandler<MessageEventArgs>(Process_Info);
                reporter.Error -= new EventHandler<MessageEventArgs>(Process_Error);
                reporter.Completed -= new EventHandler(Process_Stopped);

                m_CommandProcesses.Remove(process);
            }

            if (process.Result != CommunicationProcessResult.Success)
            {
                Invoke((MethodInvoker)(() =>
                {
                    MessageBox.Show(this, process.CommandOscAddress + " command could not be confirmed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }));
            }
        }

        void Process_Stopped_DisconnectSerialOnSuccess(object sender, EventArgs e)
        {
            CommandProcess process = sender as CommandProcess;

            lock (m_CommandProcessLock)
            {
                Reporter reporter = process.Reporter as Reporter;

                reporter.Info -= new EventHandler<MessageEventArgs>(Process_Info);
                reporter.Error -= new EventHandler<MessageEventArgs>(Process_Error);
                reporter.Completed -= new EventHandler(Process_Stopped_DisconnectSerialOnSuccess);

                m_CommandProcesses.Remove(process);

                if (process.Result == CommunicationProcessResult.Success)
                {
                    CloseConnection();
                }
            }

            if (process.Result != CommunicationProcessResult.Success)
            {
                Invoke((MethodInvoker)(() =>
                {
                    MessageBox.Show(this, process.CommandOscAddress + " command could not be confirmed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }));
            }
        }

        #endregion

        #region Graph

        private void AddGraphData(string name, DateTime timestamp, int index, float trace)
        {
            if (m_GraphWindows.ContainsKey(name) == false)
            {
                return;
            }

            m_GraphWindows[name].AddData(timestamp, index, trace);
        }

        private void AddGraphData(string name, DateTime timestamp, params float[] traces)
        {
            if (m_GraphWindows.ContainsKey(name) == false)
            {
                return;
            }

            m_GraphWindows[name].AddData(timestamp, traces);
        }

        private void ShowGraph(string name)
        {
            if (m_GraphWindows.ContainsKey(name) == false)
            {
                return;
            }

            m_GraphWindows[name].Show();
        }

        private void HideGraph(string name)
        {
            if (m_GraphWindows.ContainsKey(name) == false)
            {
                return;
            }

            m_GraphWindows[name].Hide();
        }

        #endregion

        #region Window Resize / Move Events

        private void Form_ResizeBegin(object sender, EventArgs e)
        {

        }

        private void Form_ResizeEnd(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                Options.Bounds = this.DesktopBounds;
            }
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                Options.WindowState = WindowState;
            }
        }

        #endregion

        private void SearchForConnectionsItem_Click(object sender, EventArgs e)
        {
            if (m_DisconnectMenuItem.Enabled == true)
            {
                DisconnectMenuItem_Click(sender, e);
            }

            /* 
            using (AutoConnectionWorker worker = new AutoConnectionWorker())
            {
                //dialog.Reporter.Connected += new EventHandler(ActiveConnection_Connected);
                //dialog.Reporter.Disconnected += new EventHandler(ActiveConnection_Disconnected);
                worker.Reporter.Error += new EventHandler<MessageEventArgs>(ActiveConnection_Error);
                worker.Reporter.Exception += new EventHandler<ExceptionEventArgs>(ActiveConnection_Exception);
                worker.Reporter.Info += new EventHandler<MessageEventArgs>(ActiveConnection_Info);
                worker.Reporter.Message += new MessageEvent(ActiveConnection_Message);

                if (worker.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    ConnectAuto(worker.Info); 
                }

                //dialog.Reporter.Connected -= new EventHandler(ActiveConnection_Connected);
                //dialog.Reporter.Disconnected -= new EventHandler(ActiveConnection_Disconnected);
                worker.Reporter.Error -= new EventHandler<MessageEventArgs>(ActiveConnection_Error);
                worker.Reporter.Exception -= new EventHandler<ExceptionEventArgs>(ActiveConnection_Exception);
                worker.Reporter.Info -= new EventHandler<MessageEventArgs>(ActiveConnection_Info);
                worker.Reporter.Message -= new MessageEvent(ActiveConnection_Message);
            }
            */

            using (SearchingForConnectionsDialog dialog = new SearchingForConnectionsDialog())
            {
                if (dialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    ConnectAuto(dialog.Info);
                }
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OptionsDialog prefs = new OptionsDialog())
            {
                prefs.ShowDialog(this);
            }
        }

        //UploadFirmwareDialog m_UploadFirmwareDialog = new UploadFirmwareDialog(); 

        private void loadDefaultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_Connection == null)
            {
                return;
            }

            lock (m_CommandProcessLock)
            {
                if (MessageBox.Show(this, "All settings will be returned to the default values. This may result in your current connection being lost.", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                Reporter reporter = new Reporter();

                CommandProcess process = new CommandProcess(m_Connection, reporter, new CommandCallback(new OscMessage("/default")), Options.Timeout, Options.MaximumNumberOfRetries);

                m_CommandProcesses.Add(process);

                reporter.Info += new EventHandler<MessageEventArgs>(Process_Info);
                reporter.Error += new EventHandler<MessageEventArgs>(Process_Error);
                reporter.Completed += new EventHandler(Process_Stopped);
                reporter.Completed += new EventHandler(LoadDefaults_Stopped);

                process.SendAsync();
            }
        }

        void LoadDefaults_Stopped(object sender, EventArgs e)
        {
            CommandProcess process = sender as CommandProcess;

            if (process.Result != CommunicationProcessResult.Success)
            {
                return;
            }

            m_SettingsPanel.ReadFromDevice();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporter reporter = new Reporter();

            reporter.Error += new EventHandler<MessageEventArgs>(ActiveConnection_Error);
            reporter.Exception += new EventHandler<ExceptionEventArgs>(ActiveConnection_Exception);
            reporter.Info += new EventHandler<MessageEventArgs>(ActiveConnection_Info);
            reporter.Message += new MessageEvent(ActiveConnection_Message);

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "Text Files|*.txt|All Files|*.*";
                dialog.Title = "Save Settings To File";

                if (String.IsNullOrEmpty(m_SettingsPanel.Settings.DeviceName.Value) == true ||
                   String.IsNullOrEmpty(m_SettingsPanel.Settings.SerialNumber.Value) == true)
                {
                    dialog.FileName = NgimuApi.Helper.CleanFileName("Settings.txt");
                }
                else
                {
                    dialog.FileName = NgimuApi.Helper.CleanFileName(m_SettingsPanel.Settings.DeviceName.Value + " - " + m_SettingsPanel.Settings.SerialNumber.Value + " Settings.txt");
                }


                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Settings settings = m_Connection != null ? m_Connection.Settings : m_SettingsPanel.Settings;

                    settings.Save(dialog.FileName, reporter);
                }
            }

            reporter.Error -= new EventHandler<MessageEventArgs>(ActiveConnection_Error);
            reporter.Exception -= new EventHandler<ExceptionEventArgs>(ActiveConnection_Exception);
            reporter.Info -= new EventHandler<MessageEventArgs>(ActiveConnection_Info);
            reporter.Message -= new MessageEvent(ActiveConnection_Message);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporter reporter = new Reporter();

            reporter.Error += new EventHandler<MessageEventArgs>(ActiveConnection_Error);
            reporter.Exception += new EventHandler<ExceptionEventArgs>(ActiveConnection_Exception);
            reporter.Info += new EventHandler<MessageEventArgs>(ActiveConnection_Info);
            reporter.Message += new MessageEvent(ActiveConnection_Message);

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Text Files|*.txt|All Files|*.*";
                dialog.Title = "Load Settings From File";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Settings settings = m_Connection != null ? m_Connection.Settings : m_SettingsPanel.Settings;

                    settings.Load(dialog.FileName, reporter);

                    m_SettingsPanel.OnLoadedFromFile();
                }
            }

            reporter.Error -= new EventHandler<MessageEventArgs>(ActiveConnection_Error);
            reporter.Exception -= new EventHandler<ExceptionEventArgs>(ActiveConnection_Exception);
            reporter.Info -= new EventHandler<MessageEventArgs>(ActiveConnection_Info);
            reporter.Message -= new MessageEvent(ActiveConnection_Message);
        }

        private void m_TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (m_TabControl.SelectedIndex == 1)
            {
                filterPanel1.InvalidateAndAlign();
            }
        }
    }
}

