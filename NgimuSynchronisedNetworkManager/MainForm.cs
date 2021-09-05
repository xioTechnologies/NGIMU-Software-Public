using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
        public static ushort SynchronisationMasterPort = 9000;
        public static string DefaultWifiClientSSID = "NGIMU Network";
        public static string DefaultWifiClientKey = "xiotechnologies";
        public static float DefaultSendRateRssi = 2;

        List<ConnectionRow> connectionsRows = new List<ConnectionRow>();

        private List<Image> icons = new List<Image>();

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

        private TimestampWindow timestamp;
        private DataLoggerWindow dataLogger;
        private SendRatesWindow sendRates;

        private OscListener synchronisationMasterListener;
        private DateTime synchronisationMasterTimeTimeStamp;
        private bool synchronisationMasterTimeConfirmedByUser = false;
        private DataForwardingDialog dataForwardingDialog;

        private ConnectionRow SynchronisationMasterRow => (from connectionRow in connectionsRows
                                                           let isMaster = connectionRow.Connection.Settings.SynchronisationMasterEnabled.HasRemoteValue == true && connectionRow.Connection.Settings.SynchronisationMasterEnabled.RemoteValue == true
                                                           where isMaster == true
                                                           select connectionRow).FirstOrDefault();


        public MainForm()
        {
            RC.Verbosity = ConsoleVerbosity.Normal;
            RC.BackgroundColor = ConsoleColorExt.Black;
            RC.Theme = ConsoleColorTheme.Load(ConsoleColorDefaultThemes.Colorful);

            InitializeComponent();

            title = typeof(MainForm).Assembly.GetName().Name + " v" + typeof(MainForm).Assembly.GetName().Version.Major + "." + typeof(MainForm).Assembly.GetName().Version.Minor;

            SetTitle();

            icons.Add(null);
            icons.Add(GetImage(SystemIcons.Question));
            icons.Add(GetImage(SystemIcons.Asterisk));
            icons.Add(GetImage(SystemIcons.Exclamation));
            icons.Add(GetImage(SystemIcons.Error));
            icons.Add(GetImage(SystemIcons.Information));
            icons.Add(GetImage(SystemIcons.Warning));

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

                    this.SendCommand(connectionsRows, true, item.Tag as CommandMetaData);
                };

                commandItems.Add(menuItem);
            }

            sendCommandToolStripMenuItem.DropDownItems.AddRange(commandItems.ToArray());
        }

        private Image GetImage(Icon icon)
        {
            using (Image image = icon.ToBitmap())
            {
                return TextAndIconColumn.ResizeImage(image, 24, 24);
            }
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
                    sendRates?.ActiveConnections.Add(connectionRow);

                    connection.Message += Connection_Message;

                    object[] cells = new object[Enum.GetNames(typeof(ColumnIndex)).Length];
                    DateTime[] timestamps = new DateTime[cells.Length];

                    StringBuilder nameBuilder = new StringBuilder();

                    nameBuilder.AppendLine(connection.Settings.DeviceInformation.DeviceName.Value);
                    nameBuilder.AppendLine(connection.Settings.DeviceInformation.SerialNumber.Value);

                    cells[(int)ColumnIndex.Device] = nameBuilder.ToString().Trim();

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

                    connectionRow.Information = new IconInfo() { Image = icons[(int)ConnectionIcon.Information], Message = "", MessageIcon = MessageBoxIcon.Information, Visible = false };
                    connectionRow.Warning = new IconInfo() { Image = icons[(int)ConnectionIcon.Warning], Message = "", MessageIcon = MessageBoxIcon.Warning, Visible = false };
                    connectionRow.Error = new IconInfo() { Image = icons[(int)ConnectionIcon.Error], Message = "", MessageIcon = MessageBoxIcon.Error, Visible = false };

                    (connectionRow.Row.Cells[(int)ColumnIndex.Device] as TextAndIconCell).Add(connectionRow.Information);
                    (connectionRow.Row.Cells[(int)ColumnIndex.Device] as TextAndIconCell).Add(connectionRow.Warning);
                    (connectionRow.Row.Cells[(int)ColumnIndex.Device] as TextAndIconCell).Add(connectionRow.Error);

                    connectionRow.Connection.DeviceError.Received += (s, args) =>
                    {
                        StringBuilder stringBuilder = new StringBuilder();

                        stringBuilder.AppendLine("Error message received:");
                        stringBuilder.AppendLine();
                        stringBuilder.AppendLine(connectionRow.Connection.DeviceError.Message);

                        connectionRow.SetInformation(stringBuilder.ToString());
                    };

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

            sendRates?.UpdateColumns();
            dataLogger?.UpdateConnections();

            if (result == DialogResult.Cancel)
            {
                DisconnectAll();
                return;
            }

            if (connectionsRows.Count == 0)
            {
                DisconnectAll();
                return;
            }

            sendCommandToolStripMenuItem.Enabled = true;

            SetTitle();

            VerifyConfiguration();

            CreateSynchronisationMasterListener();

            ApplySort();
        }

        private void Connection_Message(Connection source, MessageDirection direction, OscMessage message)
        {
            if (direction == MessageDirection.Transmit)
            {
                return;
            }

            dataForwardingDialog?.OnMessage(source, message);
        }

        private void ApplySort()
        {
            if (dataGridView1.SortedColumn == null || dataGridView1.SortOrder == SortOrder.None)
            {
                return;
            }

            ListSortDirection direction =
                dataGridView1.SortOrder == SortOrder.Ascending ?
                    ListSortDirection.Ascending :
                    ListSortDirection.Descending;

            dataGridView1.Sort(dataGridView1.SortedColumn, direction);
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

                if (this.TryGetIncompatableFirmwareWarningMessage(settings, out string message) == true)
                {
                    connectionRow.SetWarning(message);
                }

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
                        this.WriteSettings(connectionsRows, true, connectionsRows.Select(connectionRowItem => connectionRowItem.Connection.Settings.WifiReceivePort));

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
                        this.WriteSettings(connectionsRows, true, connectionsRows.Select(connectionRowItem => connectionRowItem.Connection.Settings.SynchronisationMasterPort));

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
                sb.Append($"Click OK to set the RSSI send rate to {DefaultSendRateRssi} Hz for all devices.");

                switch (this.ShowError(sb.ToString(), MessageBoxButtons.OKCancel))
                {
                    case DialogResult.OK:
                        this.SetSettingValueOnAllDevices(connectionsRows.Select(connectionRow => connectionRow.Connection.Settings.SendRateRssi), 2f);
                        this.WriteSettings(connectionsRows, true, connectionsRows.Select(connectionRowItem => connectionRowItem.Connection.Settings.SendRateRssi));
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

            string currentConnectionString = "";

            Thread connectingToMultipleConnections = new Thread(() =>
            {
                progress.OnCancel += (o, args) =>
                {
                    canceled = true;
                };

                EventHandler<MessageEventArgs> reportInfo = (o, args) =>
                {
                    progress.UpdateProgress(currentProgress, currentConnectionString + args.Message);
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

                        currentConnectionString = "Connecting to " + autoConnectionInfo.DeviceDescriptor + ". ";

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
            sendCommandToolStripMenuItem.Enabled = false;

            if (connectionsRows.Count == 0)
            {
                return;
            }

            dataLogger?.Stop();
            dataLogger?.ActiveConnections.Clear();
            dataLogger?.UpdateConnections();

            sendRates?.ActiveConnections.Clear();
            sendRates?.UpdateColumns();

            ProgressDialog progress = new ProgressDialog();

            progress.Text = "Disconnecting";
            progress.Style = ProgressBarStyle.Continuous;

            progress.CancelButtonEnabled = false;
            progress.ProgressMessage = "";
            progress.Progress = 0;
            progress.ProgressMaximum = connectionsRows.Count;

            ManualResetEvent dialogDoneEvent = new ManualResetEvent(false);

            Thread disconnectThread = new Thread(() =>
            {
                try
                {
                    int index = 0;
                    foreach (ConnectionRow connection in connectionsRows)
                    {
                        if (progress.Visible == true)
                        {
                            Invoke(new MethodInvoker(() =>
                            {
                                progress.ProgressMessage = "Disconnecting from " + connection.Connection.Settings.GetDeviceDescriptor() + ".";
                                progress.Progress = ++index;
                            }));
                        }

                        connection.Connection.Message -= Connection_Message;
                        connection.Connection.Dispose();
                    }

                    Thread.CurrentThread.Join(100);
                }
                finally
                {
                    dialogDoneEvent.Set();
                }

                Invoke(new MethodInvoker(() => { progress.Close(); }));

            });

            disconnectThread.Name = "Disconnecting from " + connectionsRows.Count + " Devices";
            disconnectThread.Start();

            progress.ShowDialog(this);

            dialogDoneEvent.WaitOne();

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

            ConnectionRow synchronisationMasterRow = SynchronisationMasterRow;

            synchronisationMasterTimeTimeStamp = DateTime.UtcNow;

            DateTime synchronisationMasterTime = ((OscTimeTag)message[0]).ToDataTime();

            Invoke(new MethodInvoker(() =>
            {
                synchronisationMasterTimeLabel.Text = string.Format(synchronisationMasterTimeLabel.Tag.ToString(), NgimuApi.Helper.DateTimeToString(synchronisationMasterTime, false));
            }));

            if (synchronisationMasterRow == null)
            {
                return;
            }

            if (Equals(((UdpConnectionInfo)synchronisationMasterRow.Connection.ConnectionInfo).SendIPAddress, message.Origin.Address) == false)
            {

                StringBuilder sb = new StringBuilder();

                sb.AppendLine("More than one synchronisation master is sending on the network.");

                //this.InvokeShowError(sb.ToString());

                sb.AppendLine();
                sb.Append("Click OK to configure the first device as the synchronisation master.");

                Invoke(new MethodInvoker(() =>
                {
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
                }));

                // destroy and recreate the sync master listener to clear out any junk messages that might have backed up when waiting for user input
                synchronisationMasterListener?.Dispose();
                CreateSynchronisationMasterListener();

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

                this.SendCommand(synchronisationMasterRow, true, Command.Time, OscTimeTag.Now);

                this.SendCommand(
                    (from connectionRow in connectionsRows
                     where connectionRow != synchronisationMasterRow
                     select connectionRow).ToList(),
                    true,
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

            dataForwardingDialog?.Close();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            switch ((ColumnIndex)e.ColumnIndex)
            {
                case ColumnIndex.Device:
                    Point mousePoint = dataGridView1.PointToClient(Control.MousePosition);

                    if ((dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as TextAndIconCell).TryClick(mousePoint, out IconInfo iconInfo) == false)
                    {
                        return;
                    }

                    iconInfo.Visible = false;

                    switch (iconInfo.MessageIcon)
                    {
                        case MessageBoxIcon.None:
                            break;
                        case MessageBoxIcon.Warning:
                            this.ShowWarning(iconInfo.Message, MessageBoxButtons.OK);
                            break;
                        case MessageBoxIcon.Error:
                            this.ShowError(iconInfo.Message, MessageBoxButtons.OK);
                            break;
                        case MessageBoxIcon.Information:
                            this.ShowInformation(iconInfo.Message, MessageBoxButtons.OK);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
                default:
                    break;
            }
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
                    this.SendCommand((dataGridView1.Rows[e.RowIndex].Tag as ConnectionRow), true, Command.Identify);
                    break;
                default:
                    break;
            }
        }

        private void SelectSynchronisationMaster(ConnectionRow selectedConnectionRow)
        {
            this.SetSettingValueOnAllDevices(connectionsRows.Select(connectionRowItem => connectionRowItem.Connection.Settings.SynchronisationMasterEnabled), false);

            selectedConnectionRow.Connection.Settings.SynchronisationMasterEnabled.Value = true;

            this.WriteSettings(connectionsRows, true, connectionsRows.Select(connectionRowItem => connectionRowItem.Connection.Settings.SynchronisationMasterEnabled));

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
                    StringBuilder nameBuilder = new StringBuilder();

                    nameBuilder.AppendLine(connection.Connection.Settings.DeviceInformation.DeviceName.Value);
                    nameBuilder.Append(connection.Connection.Settings.DeviceInformation.SerialNumber.Value);

                    connection.Cells[(int)ColumnIndex.Device] = nameBuilder.ToString();
                }

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

                if (connection.CheckForIconRedraw())
                {
                    dataGridView1.InvalidateCell(connection.Row.Cells[0]);
                }
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

            dataLogger?.UpdateConnections();
            dataLogger.Show();
        }

        private void DataLogger_FormClosed(object sender, FormClosedEventArgs e)
        {
            dataLogger.Dispose();
            dataLogger = null;
        }

        private void timestampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (timestamp != null)
            {
                timestamp.Show();

                return;
            }

            timestamp = new TimestampWindow();
            timestamp.FormClosed += Timestamp_FormClosed;

            foreach (ConnectionRow row in connectionsRows)
            {
                timestamp?.Connections.Add(row.Connection);
            }

            timestamp.Show();
        }

        private void Timestamp_FormClosed(object sender, FormClosedEventArgs e)
        {
            timestamp.Dispose();
            timestamp = null;
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
                sendRates?.ActiveConnections.Add(row);
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

        private void ConfigureWirelessSettingsViaUSBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (WifiSettingsDialog dialog = new WifiSettingsDialog())
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }
            }
        }

        private void dataForwardingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataForwardingDialog != null)
            {
                return;
            }

            dataForwardingDialog = new DataForwardingDialog();

            dataForwardingDialog.FormClosed += DataForwardingDialog_FormClosed;

            dataForwardingDialog.Show();
        }

        private void DataForwardingDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            dataForwardingDialog = null;
        }

        private string deviceNameEditOriginalValue;

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            switch ((ColumnIndex)e.ColumnIndex)
            {
                case ColumnIndex.Device:
                    break;
                default:
                    return;
            }

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            ConnectionRow connectionRow = row.Tag as ConnectionRow;

            string newValue = row.Cells[(int)ColumnIndex.Device].Value.ToString();

            if (newValue.Equals(deviceNameEditOriginalValue) == true)
            {
                return;
            }

            connectionRow.Connection.Settings.DeviceName.Value = newValue;

            this.WriteSettings(connectionsRows, true, new[] { connectionRow.Connection.Settings.DeviceName });
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            switch ((ColumnIndex)e.ColumnIndex)
            {
                case ColumnIndex.Device:
                    break;
                default:
                    e.Cancel = true;
                    return;
            }

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            ConnectionRow connectionRow = row.Tag as ConnectionRow;

            deviceNameEditOriginalValue = row.Cells[(int)ColumnIndex.Device].Value.ToString();

            row.Cells[(int)ColumnIndex.Device].Value = connectionRow.Connection.Settings.DeviceInformation.DeviceName.Value;
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
