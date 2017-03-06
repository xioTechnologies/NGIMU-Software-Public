using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using NgimuApi;
using NgimuApi.SearchForConnections;
using NgimuForms.Controls;
using NgimuForms.DialogsAndWindows;
using NgimuSynchronisedNetworkManager.Controls;
using NgimuSynchronisedNetworkManager.DialogsAndWindows;
using Rug.Cmd;
using Rug.Cmd.Colors;
using Rug.Osc;

namespace NgimuSynchronisedNetworkManager
{
    public partial class MainForm : BaseForm
    {
        public const int SynchronisationMasterPort = 9000;

        List<ConnectionRow> connectionsRows = new List<ConnectionRow>();

        enum ColumnIndex
        {
            Device,
            Master,
            MessagesReceived,
            MessagesSent,
            Battery,
            Signal,
            Identify,
        };

        private string title;

        class ConnectionRow
        {
            public object[] Cells;
            public DateTime[] Timestamps;

            public Connection Connection { get; set; }

            public DataGridViewRow Row { get; set; }
        }

        private DataLoggerWindow dataLogger;
        private SendRatesWindow sendRates;

        private OscListener synchronisationMasterListener;
        private DateTime synchronisationMasterTimeTimeStamp;
        private bool synchronisationMasterTimeConfirmedByUser = false;

        private ConnectionRow SynchronisationMasterRow
        {
            get
            {
                return (from connectionRow in connectionsRows
                        let isMaster = connectionRow.Connection.Settings.SynchronisationMasterEnabled.HasRemoteValue == true && connectionRow.Connection.Settings.SynchronisationMasterEnabled.RemoteValue == true
                        where isMaster == true
                        select connectionRow).FirstOrDefault();
            }
        }


        public MainForm()
        {
            RC.Verbosity = ConsoleVerbosity.Normal;
            RC.BackgroundColor = ConsoleColorExt.Black;
            RC.Theme = ConsoleColorTheme.Load(ConsoleColorDefaultThemes.Colorful);

            InitializeComponent();

            title = typeof(MainForm).Assembly.GetName().Name + " v" + typeof(MainForm).Assembly.GetName().Version.Major + "." + typeof(MainForm).Assembly.GetName().Version.Minor;

            SetTitle();

            List<ToolStripMenuItem> commandItems = new List<ToolStripMenuItem>();

            foreach (CommandMetaData command in Commands.GetCommands())
            {
                if (command.IsVisible == false)
                {
                    continue;
                }

                if (command.OscAddress == "/time")
                {
                    continue;
                }

                ToolStripMenuItem menuItem = new ToolStripMenuItem(command.Text)
                {
                    Tag = command,
                };

                menuItem.Click += delegate (object menuSender, EventArgs args)
                {
                    ToolStripMenuItem item = menuSender as ToolStripMenuItem;

                    this.SendCommand(connectionsRows.Select(connectionRow => connectionRow.Connection).ToList(), item.Tag as CommandMetaData);
                };

                commandItems.Add(menuItem);
            }

            commandsToolStripMenuItem.DropDownItems.AddRange(commandItems.ToArray());
        }

        private void searchForConnectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            disconnectAllToolStripMenuItem_Click(sender, e);

            DialogResult result;

            using (SearchingForConnectionsDialog searchDialog = new SearchingForConnectionsDialog()
            {
                ConnectionSearchType = ConnectionSearchTypes.Udp,
                AllowMultipleConnections = true
            })
            {
                if (searchDialog.ShowDialog(this) == DialogResult.Cancel)
                {
                    return;
                }

                List<Connection> newConnections;

                result = ConnectToAllDevices(searchDialog.ConnectionSearchResults, out newConnections);

                foreach (Connection connection in newConnections)
                {
                    ConnectionRow connectionRow = new ConnectionRow();

                    connectionRow.Connection = connection;

                    connectionsRows.Add(connectionRow);

                    dataLogger?.ActiveConnections.Add(connection);
                    sendRates?.ActiveConnections.Add(connection);
                    sendRates?.UpdateColumns();

                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine(connection.Settings.DeviceInformation.DeviceName.Value);
                    sb.AppendLine(connection.Settings.DeviceInformation.SerialNumber.Value);

                    object[] cells = new object[Enum.GetNames(typeof(ColumnIndex)).Length];
                    DateTime[] timestamps = new DateTime[cells.Length];

                    cells[(int)ColumnIndex.Device] = sb.ToString().Trim();

                    cells[(int)ColumnIndex.Master] = connectionRow.Connection.Settings.SynchronisationMasterEnabled.Value;
                    cells[(int)ColumnIndex.MessagesReceived] = "Unknown";
                    cells[(int)ColumnIndex.MessagesSent] = "Unknown";
                    cells[(int)ColumnIndex.Battery] = new DataGridViewProgressValue() { Value = 0, Text = "Unknown" };
                    cells[(int)ColumnIndex.Signal] = new DataGridViewProgressValue() { Value = 0, Text = "Unknown" };
                    cells[(int)ColumnIndex.Identify] = "Identify";

                    timestamps[(int)ColumnIndex.Device] = DateTime.UtcNow;
                    timestamps[(int)ColumnIndex.Master] = DateTime.UtcNow;
                    timestamps[(int)ColumnIndex.MessagesReceived] = DateTime.UtcNow;
                    timestamps[(int)ColumnIndex.MessagesSent] = DateTime.UtcNow;
                    timestamps[(int)ColumnIndex.Battery] = DateTime.UtcNow;
                    timestamps[(int)ColumnIndex.Signal] = DateTime.UtcNow;
                    timestamps[(int)ColumnIndex.Identify] = DateTime.UtcNow;

                    connectionRow.Timestamps = timestamps;
                    connectionRow.Cells = cells;
                    connectionRow.Row = dataGridView1.Rows[dataGridView1.Rows.Add(cells)];
                    connectionRow.Row.Tag = connectionRow;

                    connectionRow.Connection.Battery.Received += (s, arg) =>
                    {
                        string text = String.Format("{0}%" + Environment.NewLine, connectionRow.Connection.Battery.Percentage.ToString("F0", CultureInfo.InvariantCulture));

                        if (connectionRow.Connection.Battery.IsChargerConnected == true)
                        {
                            text += String.IsNullOrEmpty(connectionRow.Connection.Battery.ChargerState) == false ? connectionRow.Connection.Battery.ChargerState : String.Empty;
                        }
                        else
                        {
                            text += NgimuApi.Helper.TimeSpanToString(connectionRow.Connection.Battery.TimeToEmpty, TimeSpanStringFormat.Shorthand) + " remaining";
                        }

                        connectionRow.Timestamps[(int)ColumnIndex.Battery] = DateTime.UtcNow;
                        connectionRow.Cells[(int)ColumnIndex.Battery] = new DataGridViewProgressValue() { Value = (int)connectionRow.Connection.Battery.Percentage, Text = text };
                    };

                    connectionRow.Connection.Rssi.Received += (s, arg) =>
                    {
                        string text = String.Format("{0}%" + Environment.NewLine, connectionRow.Connection.Rssi.Percentage.ToString("F0", CultureInfo.InvariantCulture));
                        text += connectionRow.Connection.Rssi.Power.ToString("F2", CultureInfo.InvariantCulture) + " dBm";

                        connectionRow.Timestamps[(int)ColumnIndex.Signal] = DateTime.UtcNow;
                        connectionRow.Cells[(int)ColumnIndex.Signal] = new DataGridViewProgressValue() { Value = (int)connectionRow.Connection.Rssi.Percentage, Text = text };
                    };
                }
            }

            if (result == DialogResult.Cancel)
            {
                DisconnectAll();
                return;
            }

            SetTitle();

            VerifyConfiguration();

            CreateSynchronisationMasterListener();
        }

        private void VerifyConfiguration()
        {
            int numberOfSynchronisationMasters = 0;
            bool allDevicesHaveCorrectWifiReceivePort = true;
            bool allDevicesHaveCorrectSynchronisationMasterPort = true;
            bool someDevicesHaveRssiSendRateOfZero = false;

            foreach (ConnectionRow connectionRow in connectionsRows)
            {
                Settings settings = connectionRow.Connection.Settings;

                this.ShowIncompatableFirmwareWarning(settings);

                if (settings.SynchronisationMasterEnabled.Value == true)
                {
                    numberOfSynchronisationMasters++;
                }

                if (settings.WifiReceivePort.Value != MainForm.SynchronisationMasterPort)
                {
                    allDevicesHaveCorrectWifiReceivePort = false;
                }

                if (settings.SynchronisationMasterPort.Value != MainForm.SynchronisationMasterPort)
                {
                    allDevicesHaveCorrectSynchronisationMasterPort = false;
                }

                if (settings.SendRateRssi.Value == 0f)
                {
                    someDevicesHaveRssiSendRateOfZero = true;
                }
            }

            if (allDevicesHaveCorrectWifiReceivePort == false)
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine($"The UDP receive port must be set to { MainForm.SynchronisationMasterPort } on all devices.");
                sb.AppendLine();
                sb.Append($"Click OK to set the UDP receive port to { MainForm.SynchronisationMasterPort } on all devices and disconnect.");

                switch (this.ShowError(sb.ToString(), MessageBoxButtons.OKCancel))
                {
                    case DialogResult.OK:
                        this.SetSettingValueOnAllDevices(connectionsRows.Select(connectionRow => connectionRow.Connection.Settings.WifiReceivePort), (ushort)MainForm.SynchronisationMasterPort);
                        this.WriteSettings(connectionsRows.Select(connectionRowItem => connectionRowItem.Connection.Settings.WifiReceivePort));

                        DisconnectAll();
                        return;
                    case DialogResult.Cancel:
                        DisconnectAll();
                        return;
                    default:
                        break;
                }
            }

            if (allDevicesHaveCorrectSynchronisationMasterPort == false)
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine($"The synchronisation master send port must be set to { MainForm.SynchronisationMasterPort } on all devices.");
                sb.AppendLine();
                sb.Append($"Click OK to set the synchronisation master send port to { MainForm.SynchronisationMasterPort } on all devices.");

                switch (this.ShowError(sb.ToString(), MessageBoxButtons.OKCancel))
                {
                    case DialogResult.OK:
                        this.SetSettingValueOnAllDevices(connectionsRows.Select(connectionRow => connectionRow.Connection.Settings.SynchronisationMasterPort), (ushort)MainForm.SynchronisationMasterPort);
                        this.WriteSettings(connectionsRows.Select(connectionRowItem => connectionRowItem.Connection.Settings.SynchronisationMasterPort));

                        break;
                    case DialogResult.Cancel:
                        DisconnectAll();
                        return;
                    default:
                        break;
                }
            }

            if (numberOfSynchronisationMasters < 1)
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("None of the devices have been configured as the synchronisation master.");
                sb.AppendLine();
                sb.Append("Click OK to configure the first device as the synchronisation master.");

                switch (this.ShowError(sb.ToString(), MessageBoxButtons.OKCancel))
                {
                    case DialogResult.OK:
                        SelectSynchronisationMaster(connectionsRows[0]);
                        break;
                    case DialogResult.Cancel:
                        DisconnectAll();
                        return;
                    default:
                        break;
                }
            }

            if (numberOfSynchronisationMasters > 1)
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("More than one device has been configured as the synchronisation master.");
                sb.AppendLine();
                sb.Append("Click OK to configure only the first device as the synchronisation master.");

                switch (this.ShowError(sb.ToString(), MessageBoxButtons.OKCancel))
                {
                    case DialogResult.OK:
                        SelectSynchronisationMaster(connectionsRows[0]);
                        break;
                    case DialogResult.Cancel:
                        DisconnectAll();
                        return;
                    default:
                        break;
                }
            }

            if (someDevicesHaveRssiSendRateOfZero == true)
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("The RSSI send rate of one or more devices is set to zero.");
                sb.AppendLine();
                sb.Append("Click OK to set the RSSI send rate to 2 Hz for all devices.");

                switch (this.ShowError(sb.ToString(), MessageBoxButtons.OKCancel))
                {
                    case DialogResult.OK:
                        this.SetSettingValueOnAllDevices(connectionsRows.Select(connectionRow => connectionRow.Connection.Settings.SendRateRssi), 2f);
                        this.WriteSettings(connectionsRows.Select(connectionRowItem => connectionRowItem.Connection.Settings.SendRateRssi));
                        break;
                    case DialogResult.Cancel:
                        DisconnectAll();
                        return;
                    default:
                        break;
                }
            }
        }

        // TODO refactor this method to a helper / class of some kind 
        private DialogResult ConnectToAllDevices(List<ConnectionSearchResult> connectionSearchResults, out List<Connection> resultConnections)
        {
            if (connectionSearchResults.Count == 0)
            {
                resultConnections = new List<Connection>();

                return DialogResult.OK;
            }

            List<Connection> connections = new List<Connection>();

            DialogResult result = System.Windows.Forms.DialogResult.Cancel;

            // Begin 
            ProgressDialog progress = new ProgressDialog();

            progress.Text = "Connecting";
            progress.Style = ProgressBarStyle.Continuous;

            progress.ProgressMessage = "";
            progress.Progress = 0;
            progress.ProgressMaximum = (connectionSearchResults.Count * 4) - 1;

            Reporter reporter = new Reporter();

            bool canceled = false;
            int currentProgress = 0;

            Thread connectingToMultipleConnections = new Thread(() =>
            {
                progress.OnCancel += (o, args) =>
                {
                    canceled = true;
                };

                EventHandler<MessageEventArgs> reportInfo = (o, args) =>
                {
                    progress.UpdateProgress(currentProgress, args.Message);
                };

                reporter.Info += reportInfo;

                try
                {
                    progress.UpdateProgress(0);

                    for (int i = 0; i < connectionSearchResults.Count; i++)
                    {
                        if (canceled == true)
                        {
                            progress.UpdateProgress(currentProgress, string.Empty);

                            return;
                        }

                        ConnectionSearchResult autoConnectionInfo = connectionSearchResults[i];

                        progress.UpdateProgress(currentProgress++, autoConnectionInfo.DeviceDescriptor);

                        Connection connection = null;

                        if (autoConnectionInfo.ConnectionType == ConnectionType.Udp)
                        {
                            #region Configure Unique Connection

                            bool retryThis = true;

                            do
                            {
                                try
                                {
                                    UdpConnectionInfo udpConnectionInfo =
                                        NgimuApi.Connection.ConfigureUniqueUdpConnection(
                                            autoConnectionInfo.ConnectionInfo as UdpConnectionInfo, reporter);

                                    connection = new Connection(udpConnectionInfo);

                                    break;
                                }
                                catch (Exception ex)
                                {

                                    StringBuilder fullMessageString = new StringBuilder();

                                    fullMessageString.AppendLine("Error while connecting to " + autoConnectionInfo.DeviceDescriptor + ".");
                                    fullMessageString.AppendLine();

                                    fullMessageString.Append("Could not configure a unique UDP connection.");

                                    switch (this.InvokeShowError(fullMessageString.ToString(), MessageBoxButtons.AbortRetryIgnore))
                                    {
                                        case DialogResult.Abort:
                                            CloseAllConnections(connections);

                                            progress.DialogResult = DialogResult.Cancel;

                                            return;

                                        case DialogResult.Retry:
                                            break;

                                        case DialogResult.Ignore:
                                            retryThis = false;
                                            break;

                                        default:
                                            break;
                                    }
                                }
                            }
                            while (retryThis == true);

                            #endregion
                        }
                        else
                        {
                            connection = new Connection(autoConnectionInfo);
                        }

                        if (connection == null)
                        {
                            currentProgress += 3;
                            progress.UpdateProgress(currentProgress);

                            continue;
                        }

                        progress.UpdateProgress(currentProgress++);

                        if (canceled == true)
                        {
                            progress.UpdateProgress(currentProgress);

                            return;
                        }

                        #region Read Settings

                        try
                        {
                            connection.Connect();

                            progress.UpdateProgress(currentProgress++);


                            bool retryThis = false;

                            do
                            {
                                if (connection.Settings.Read(reporter) != CommunicationProcessResult.Success)
                                {
                                    bool allValuesFailed;
                                    bool allValuesSucceeded;

                                    string messageString =
                                        Settings.GetCommunicationFailureString(connection.Settings.Values, 20,
                                            out allValuesFailed, out allValuesSucceeded);

                                    StringBuilder fullMessageString = new StringBuilder();

                                    fullMessageString.AppendLine("Error while connecting to " + autoConnectionInfo.DeviceDescriptor + ".");
                                    fullMessageString.AppendLine();

                                    if (allValuesFailed == true)
                                    {
                                        fullMessageString.Append("Failed to read all settings.");
                                    }
                                    else
                                    {
                                        fullMessageString.Append("Failed to read the following settings:" + messageString);
                                    }

                                    switch (this.InvokeShowError(fullMessageString.ToString(), MessageBoxButtons.AbortRetryIgnore))
                                    {
                                        case DialogResult.Abort:
                                            connection.Dispose();

                                            CloseAllConnections(connections);

                                            progress.DialogResult = DialogResult.Cancel;

                                            return;

                                        case DialogResult.Retry:
                                            retryThis = true;
                                            break;

                                        case DialogResult.Ignore:
                                            retryThis = false;
                                            break;

                                        default:
                                            break;
                                    }
                                }

                                connections.Add(connection);
                            }
                            while (retryThis);
                        }
                        catch (Exception ex)
                        {
                            connection?.Dispose();
                        }

                        #endregion

                        progress.UpdateProgress(currentProgress++);
                    }

                    progress.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                finally
                {
                    reporter.Info -= reportInfo;
                }
            });

            connectingToMultipleConnections.Name = "Connecting to multiple connections";
            connectingToMultipleConnections.Start();

            result = progress.ShowDialog(this);

            resultConnections = new List<Connection>(connections);

            return result;
        }

        private void CloseAllConnections(List<Connection> connections)
        {
            foreach (Connection connection in connections)
            {
                connection.Dispose();
            }
        }

        private void DisconnectAll()
        {
            synchronisationMasterListener?.Dispose();

            if (connectionsRows.Count == 0)
            {
                return;
            }

            dataLogger?.Stop();
            dataLogger?.ActiveConnections.Clear();

            sendRates?.ActiveConnections.Clear();
            sendRates?.UpdateColumns();

            ProgressDialog progress = new ProgressDialog();

            progress.Text = "Disconnecting";
            progress.Style = ProgressBarStyle.Continuous;

            progress.ProgressMessage = "";
            progress.Progress = 0;
            progress.ProgressMaximum = connectionsRows.Count;

            Thread disconnectThread = new Thread(() =>
            {
                int index = 0;
                foreach (ConnectionRow connection in connectionsRows)
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        progress.ProgressMessage = "Disconnecting from " + connection.Connection.Settings.GetDeviceDescriptor() + ".";
                        progress.Progress = ++index;
                    }));

                    connection.Connection.Dispose();
                }

                Thread.CurrentThread.Join(100);

                Invoke(new MethodInvoker(() => { progress.Close(); }));
            });
            disconnectThread.Name = "Disconnecting from " + connectionsRows.Count + " Devices";
            disconnectThread.Start();

            progress.ShowDialog(this);

            connectionsRows.Clear();
            dataGridView1.Rows.Clear();

            SetTitle();
        }

        private void CreateSynchronisationMasterListener()
        {
            synchronisationMasterTimeConfirmedByUser = false;

            synchronisationMasterListener?.Dispose();
            synchronisationMasterListener = new OscListener(MainForm.SynchronisationMasterPort);
            synchronisationMasterListener.Attach("/sync", OnSyncMessage);
            synchronisationMasterListener.Connect();
        }

        private void OnSyncMessage(OscMessage message)
        {
            if (message.Count != 1)
            {
                return;
            }

            if ((message[0] is OscTimeTag) == false)
            {
                return;
            }

            synchronisationMasterTimeTimeStamp = DateTime.UtcNow;

            DateTime synchronisationMasterTime = ((OscTimeTag)message[0]).ToDataTime();

            Invoke(new MethodInvoker(() =>
            {
                synchronisationMasterTimeLabel.Text = string.Format(synchronisationMasterTimeLabel.Tag.ToString(), NgimuApi.Helper.DateTimeToString(synchronisationMasterTime, false));
            }));

            if (SynchronisationMasterRow == null)
            {
                return;
            }

            if (synchronisationMasterTimeConfirmedByUser == true)
            {
                return;
            }

            if (synchronisationMasterTime < DateTime.Now.AddSeconds(-30) || synchronisationMasterTime > DateTime.Now.AddSeconds(30))
            {
                synchronisationMasterTimeConfirmedByUser = true;

                StringBuilder sb = new StringBuilder();

                sb.AppendLine("Synchronisation master time does not match computer time.");
                sb.AppendLine();
                sb.Append("Set synchronisation master time now?");

                if (this.InvokeShowWarning(sb.ToString(), MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Invoke(new MethodInvoker(SetSynchronisationMasterTime));
                }
            }
        }

        private void SetSynchronisationMasterTime()
        {
            try
            {
                ConnectionRow synchronisationMasterRow = SynchronisationMasterRow;

                this.SendCommand(synchronisationMasterRow.Connection, Command.Time, OscTimeTag.Now);

                this.SendCommand(
                    (from connectionRow in connectionsRows
                     where connectionRow != synchronisationMasterRow
                     select connectionRow.Connection).ToList(),
                    Command.Time, new OscTimeTag(0));
            }
            catch (Exception ex)
            {
                this.InvokeShowError(ex.Message);
            }
        }

        private void SetTitle()
        {
            if (connectionsRows.Count == 0)
            {
                this.Text = title + " - Not Connected";
            }
            else
            {
                this.Text = title + " - Connected To " + dataGridView1.Rows.Count + " Devices";
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DoubleBuffered(true);

            Show();

            BeginInvoke(new EventHandler(searchForConnectionsToolStripMenuItem_Click), sender, e);
        }

        private void disconnectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisconnectAll();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisconnectAll();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            switch ((ColumnIndex)e.ColumnIndex)
            {
                case ColumnIndex.Device:
                    break;
                case ColumnIndex.Master:
                    SelectSynchronisationMaster(dataGridView1.Rows[e.RowIndex].Tag as ConnectionRow);
                    break;
                case ColumnIndex.MessagesReceived:
                    break;
                case ColumnIndex.MessagesSent:
                    break;
                case ColumnIndex.Battery:
                    break;
                case ColumnIndex.Signal:
                    break;
                case ColumnIndex.Identify:
                    this.SendCommand((dataGridView1.Rows[e.RowIndex].Tag as ConnectionRow).Connection, Command.Identify);
                    break;
                default:
                    break;
            }
        }

        private void SelectSynchronisationMaster(ConnectionRow selectedConnectionRow)
        {
            this.SetSettingValueOnAllDevices(connectionsRows.Select(connectionRowItem => connectionRowItem.Connection.Settings.SynchronisationMasterEnabled), false);

            selectedConnectionRow.Connection.Settings.SynchronisationMasterEnabled.Value = true;

            this.WriteSettings(connectionsRows.Select(connectionRowItem => connectionRowItem.Connection.Settings.SynchronisationMasterEnabled));

            foreach (ConnectionRow connectionRow in connectionsRows)
            {
                connectionRow.Cells[(int)ColumnIndex.Master] =
                    connectionRow.Connection.Settings.SynchronisationMasterEnabled.HasRemoteValue == true &&
                    connectionRow.Connection.Settings.SynchronisationMasterEnabled.RemoteValue == true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dataGridView1.SuspendLayout();

            DateTime thresholdTime = DateTime.UtcNow.AddSeconds(-5);

            foreach (ConnectionRow connection in connectionsRows)
            {
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("Total: " + connection.Connection.CommunicationStatistics.MessagesReceived.Total.ToString());
                    sb.Append("Rate: " + connection.Connection.CommunicationStatistics.MessagesReceived.Rate.ToString("F0", CultureInfo.InvariantCulture));

                    connection.Cells[(int)ColumnIndex.MessagesReceived] = sb.ToString();
                }

                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("Total: " + connection.Connection.CommunicationStatistics.MessagesSent.Total.ToString());
                    sb.Append("Rate: " + connection.Connection.CommunicationStatistics.MessagesSent.Rate.ToString("F0", CultureInfo.InvariantCulture));

                    connection.Cells[(int)ColumnIndex.MessagesSent] = sb.ToString();
                }

                foreach (ColumnIndex index in new ColumnIndex[] { ColumnIndex.Battery, ColumnIndex.Signal })
                {
                    if (connection.Timestamps[(int)index] < thresholdTime)
                    {
                        connection.Cells[(int)index] = new DataGridViewProgressValue() { Value = 0, Text = "Unknown" };
                    }
                }

                if (synchronisationMasterTimeTimeStamp < thresholdTime)
                {
                    synchronisationMasterTimeLabel.Text = string.Format(synchronisationMasterTimeLabel.Tag.ToString(), "Unknown");
                }

                connection.Row.SetValues(connection.Cells);
            }

            dataGridView1.ResumeLayout();
        }

        private void dataLoggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataLogger != null)
            {
                dataLogger.Show();

                return;
            }

            dataLogger = new DataLoggerWindow();
            dataLogger.FormClosed += DataLogger_FormClosed;

            foreach (ConnectionRow row in connectionsRows)
            {
                dataLogger?.ActiveConnections.Add(row.Connection);
            }

            dataLogger.Show();
        }

        private void DataLogger_FormClosed(object sender, FormClosedEventArgs e)
        {
            dataLogger.Dispose();
            dataLogger = null;
        }

        private void sendRatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sendRates != null)
            {
                sendRates.Show();

                return;
            }

            sendRates = new SendRatesWindow();
            sendRates.FormClosed += SendRates_FormClosed;

            foreach (ConnectionRow row in connectionsRows)
            {
                sendRates?.ActiveConnections.Add(row.Connection);
            }

            sendRates.Show();
        }

        private void SendRates_FormClosed(object sender, FormClosedEventArgs e)
        {
            sendRates.Dispose();
            sendRates = null;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutDialog dialog = new AboutDialog())
            {
                dialog.ShowDialog(this);
            }
        }
    }

    public static class ExtensionMethods
    {
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
    }
}
