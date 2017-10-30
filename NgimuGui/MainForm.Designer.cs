namespace NgimuGui
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer command.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region m_Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.m_FastTimer = new System.Windows.Forms.Timer(this.components);
            this.m_SlowTimer = new System.Windows.Forms.Timer(this.components);
            this.m_TabControl = new System.Windows.Forms.TabControl();
            this.m_TerminalTab = new System.Windows.Forms.TabPage();
            this.m_TerminalPanel = new NgimuGui.Panels.Terminal();
            this.m_MessagesTab = new System.Windows.Forms.TabPage();
            this.filterPanel1 = new NgimuGui.Panels.FilterPanel();
            this.m_SettingsTab = new System.Windows.Forms.TabPage();
            this.m_SettingsPanel = new NgimuGui.Panels.SettingsPanel();
            this.m_StatusStrip = new System.Windows.Forms.StatusStrip();
            this.m_TotalReceived = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_ReceiveRate = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_StatusSeparator1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_TotalSent = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_SendRate = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_StatusSeparator2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_BatteryStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_BatteryChargerStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_MenuStrip = new System.Windows.Forms.MenuStrip();
            this.m_ConnectionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ConnectUdpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_OpenUdpConnectionDialogMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ConnectSerialMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SerialSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.m_OpenSerialConnectionDialog = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SearchForConnectionsItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_DisconnectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SettingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ReadFromDeviceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_WriteToDeviceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_RestoreDefaultsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SendCommandMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_GraphsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_Form3DViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.m_MessagePerSecondMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_GyroscopeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_AccelerometerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MagnetometerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_BarometerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_EulerAnglesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_LinearAccelerationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_EarthAccelerationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_AltimeterMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_TemperatureMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.m_BatteryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.m_AnalogueInputsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_AuxSerialTerminalMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_LogDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ConvertSDCardFile = new System.Windows.Forms.ToolStripMenuItem();
            this.m_UploadFirmwareMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_GyroscopeAndAccelerometerCalibrationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MagneticCalibrationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SaveCalibrationToFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_LoadCalibrationValuesFromFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_TabControl.SuspendLayout();
            this.m_TerminalTab.SuspendLayout();
            this.m_MessagesTab.SuspendLayout();
            this.m_SettingsTab.SuspendLayout();
            this.m_StatusStrip.SuspendLayout();
            this.m_MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_FastTimer
            // 
            this.m_FastTimer.Interval = 50;
            this.m_FastTimer.Tick += new System.EventHandler(this.FastTimer_Tick);
            // 
            // m_SlowTimer
            // 
            this.m_SlowTimer.Enabled = true;
            this.m_SlowTimer.Interval = 1000;
            this.m_SlowTimer.Tick += new System.EventHandler(this.SlowTimer_Tick);
            // 
            // m_TabControl
            // 
            this.m_TabControl.Controls.Add(this.m_TerminalTab);
            this.m_TabControl.Controls.Add(this.m_MessagesTab);
            this.m_TabControl.Controls.Add(this.m_SettingsTab);
            this.m_TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_TabControl.Location = new System.Drawing.Point(0, 24);
            this.m_TabControl.Name = "m_TabControl";
            this.m_TabControl.SelectedIndex = 0;
            this.m_TabControl.Size = new System.Drawing.Size(784, 516);
            this.m_TabControl.TabIndex = 2;
            this.m_TabControl.SelectedIndexChanged += new System.EventHandler(this.m_TabControl_SelectedIndexChanged);
            // 
            // m_TerminalTab
            // 
            this.m_TerminalTab.BackColor = System.Drawing.Color.Transparent;
            this.m_TerminalTab.Controls.Add(this.m_TerminalPanel);
            this.m_TerminalTab.Location = new System.Drawing.Point(4, 22);
            this.m_TerminalTab.Name = "m_TerminalTab";
            this.m_TerminalTab.Padding = new System.Windows.Forms.Padding(3);
            this.m_TerminalTab.Size = new System.Drawing.Size(776, 490);
            this.m_TerminalTab.TabIndex = 0;
            this.m_TerminalTab.Text = "Terminal";
            // 
            // m_TerminalPanel
            // 
            this.m_TerminalPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_TerminalPanel.HistoryLength = 1024;
            this.m_TerminalPanel.Location = new System.Drawing.Point(3, 3);
            this.m_TerminalPanel.Margin = new System.Windows.Forms.Padding(4);
            this.m_TerminalPanel.Name = "m_TerminalPanel";
            this.m_TerminalPanel.Size = new System.Drawing.Size(770, 484);
            this.m_TerminalPanel.Statistics = null;
            this.m_TerminalPanel.TabIndex = 0;
            // 
            // m_MessagesTab
            // 
            this.m_MessagesTab.Controls.Add(this.filterPanel1);
            this.m_MessagesTab.Location = new System.Drawing.Point(4, 22);
            this.m_MessagesTab.Name = "m_MessagesTab";
            this.m_MessagesTab.Padding = new System.Windows.Forms.Padding(3);
            this.m_MessagesTab.Size = new System.Drawing.Size(776, 490);
            this.m_MessagesTab.TabIndex = 2;
            this.m_MessagesTab.Text = "Messages";
            this.m_MessagesTab.UseVisualStyleBackColor = true;
            // 
            // filterPanel1
            // 
            this.filterPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterPanel1.Location = new System.Drawing.Point(3, 3);
            this.filterPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.filterPanel1.Name = "filterPanel1";
            this.filterPanel1.Size = new System.Drawing.Size(770, 484);
            this.filterPanel1.TabIndex = 0;
            // 
            // m_SettingsTab
            // 
            this.m_SettingsTab.Controls.Add(this.m_SettingsPanel);
            this.m_SettingsTab.Location = new System.Drawing.Point(4, 22);
            this.m_SettingsTab.Name = "m_SettingsTab";
            this.m_SettingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.m_SettingsTab.Size = new System.Drawing.Size(776, 490);
            this.m_SettingsTab.TabIndex = 1;
            this.m_SettingsTab.Text = "Settings";
            this.m_SettingsTab.UseVisualStyleBackColor = true;
            // 
            // m_SettingsPanel
            // 
            this.m_SettingsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_SettingsPanel.Location = new System.Drawing.Point(3, 3);
            this.m_SettingsPanel.Margin = new System.Windows.Forms.Padding(4);
            this.m_SettingsPanel.Name = "m_SettingsPanel";
            this.m_SettingsPanel.Size = new System.Drawing.Size(770, 484);
            this.m_SettingsPanel.TabIndex = 0;
            // 
            // m_StatusStrip
            // 
            this.m_StatusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.m_StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_TotalReceived,
            this.m_ReceiveRate,
            this.m_StatusSeparator1,
            this.m_TotalSent,
            this.m_SendRate,
            this.m_StatusSeparator2,
            this.m_BatteryStatusLabel,
            this.m_BatteryChargerStatusLabel});
            this.m_StatusStrip.Location = new System.Drawing.Point(0, 540);
            this.m_StatusStrip.Name = "m_StatusStrip";
            this.m_StatusStrip.Size = new System.Drawing.Size(784, 22);
            this.m_StatusStrip.TabIndex = 1;
            this.m_StatusStrip.Text = "statusStrip1";
            // 
            // m_TotalReceived
            // 
            this.m_TotalReceived.Name = "m_TotalReceived";
            this.m_TotalReceived.Size = new System.Drawing.Size(95, 17);
            this.m_TotalReceived.Tag = "Total Received: {0}";
            this.m_TotalReceived.Text = "Total Received: 0";
            // 
            // m_ReceiveRate
            // 
            this.m_ReceiveRate.Name = "m_ReceiveRate";
            this.m_ReceiveRate.Size = new System.Drawing.Size(85, 17);
            this.m_ReceiveRate.Tag = "Receive Rate: {0}";
            this.m_ReceiveRate.Text = "Receive Rate: 0";
            // 
            // m_StatusSeparator1
            // 
            this.m_StatusSeparator1.ForeColor = System.Drawing.Color.Silver;
            this.m_StatusSeparator1.Name = "m_StatusSeparator1";
            this.m_StatusSeparator1.Size = new System.Drawing.Size(22, 17);
            this.m_StatusSeparator1.Text = "  |  ";
            // 
            // m_TotalSent
            // 
            this.m_TotalSent.Name = "m_TotalSent";
            this.m_TotalSent.Size = new System.Drawing.Size(71, 17);
            this.m_TotalSent.Tag = "Total Sent: {0}";
            this.m_TotalSent.Text = "Total Sent: 0";
            // 
            // m_SendRate
            // 
            this.m_SendRate.Name = "m_SendRate";
            this.m_SendRate.Size = new System.Drawing.Size(71, 17);
            this.m_SendRate.Tag = "Send Rate: {0}";
            this.m_SendRate.Text = "Send Rate: 0";
            // 
            // m_StatusSeparator2
            // 
            this.m_StatusSeparator2.ForeColor = System.Drawing.Color.Silver;
            this.m_StatusSeparator2.Name = "m_StatusSeparator2";
            this.m_StatusSeparator2.Size = new System.Drawing.Size(22, 17);
            this.m_StatusSeparator2.Text = "  |  ";
            // 
            // m_BatteryStatusLabel
            // 
            this.m_BatteryStatusLabel.Name = "m_BatteryStatusLabel";
            this.m_BatteryStatusLabel.Size = new System.Drawing.Size(66, 17);
            this.m_BatteryStatusLabel.Tag = "Battery: {0}%";
            this.m_BatteryStatusLabel.Text = "Battery: 0%";
            // 
            // m_BatteryChargerStatusLabel
            // 
            this.m_BatteryChargerStatusLabel.Name = "m_BatteryChargerStatusLabel";
            this.m_BatteryChargerStatusLabel.Size = new System.Drawing.Size(84, 17);
            this.m_BatteryChargerStatusLabel.Text = "Charger Status";
            // 
            // m_MenuStrip
            // 
            this.m_MenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.m_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ConnectionMenuItem,
            this.m_SettingsMenuItem,
            this.m_SendCommandMenuItem,
            this.m_GraphsMenuItem,
            this.m_ToolsMenuItem,
            this.m_HelpMenuItem});
            this.m_MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.m_MenuStrip.Name = "m_MenuStrip";
            this.m_MenuStrip.Size = new System.Drawing.Size(784, 24);
            this.m_MenuStrip.TabIndex = 0;
            this.m_MenuStrip.Text = "menuStrip1";
            // 
            // m_ConnectionMenuItem
            // 
            this.m_ConnectionMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ConnectUdpMenuItem,
            this.m_ConnectSerialMenuItem,
            this.m_SearchForConnectionsItem,
            this.m_DisconnectMenuItem,
            this.reconnectToolStripMenuItem});
            this.m_ConnectionMenuItem.Name = "m_ConnectionMenuItem";
            this.m_ConnectionMenuItem.Size = new System.Drawing.Size(81, 20);
            this.m_ConnectionMenuItem.Text = "Connection";
            // 
            // m_ConnectUdpMenuItem
            // 
            this.m_ConnectUdpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_OpenUdpConnectionDialogMenuItem});
            this.m_ConnectUdpMenuItem.Name = "m_ConnectUdpMenuItem";
            this.m_ConnectUdpMenuItem.Size = new System.Drawing.Size(218, 22);
            this.m_ConnectUdpMenuItem.Text = "UDP (Wi-Fi)";
            // 
            // m_OpenUdpConnectionDialogMenuItem
            // 
            this.m_OpenUdpConnectionDialogMenuItem.Name = "m_OpenUdpConnectionDialogMenuItem";
            this.m_OpenUdpConnectionDialogMenuItem.Size = new System.Drawing.Size(83, 22);
            this.m_OpenUdpConnectionDialogMenuItem.Text = "...";
            this.m_OpenUdpConnectionDialogMenuItem.Click += new System.EventHandler(this.OpenUdpConnectionDialog_Click);
            // 
            // m_ConnectSerialMenuItem
            // 
            this.m_ConnectSerialMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_SerialSeparator,
            this.m_OpenSerialConnectionDialog});
            this.m_ConnectSerialMenuItem.Name = "m_ConnectSerialMenuItem";
            this.m_ConnectSerialMenuItem.Size = new System.Drawing.Size(218, 22);
            this.m_ConnectSerialMenuItem.Text = "Serial";
            // 
            // m_SerialSeparator
            // 
            this.m_SerialSeparator.Name = "m_SerialSeparator";
            this.m_SerialSeparator.Size = new System.Drawing.Size(80, 6);
            // 
            // m_OpenSerialConnectionDialog
            // 
            this.m_OpenSerialConnectionDialog.Name = "m_OpenSerialConnectionDialog";
            this.m_OpenSerialConnectionDialog.Size = new System.Drawing.Size(83, 22);
            this.m_OpenSerialConnectionDialog.Text = "...";
            this.m_OpenSerialConnectionDialog.Click += new System.EventHandler(this.OpenSerialConnectionDialog_Click);
            // 
            // m_SearchForConnectionsItem
            // 
            this.m_SearchForConnectionsItem.Name = "m_SearchForConnectionsItem";
            this.m_SearchForConnectionsItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.m_SearchForConnectionsItem.Size = new System.Drawing.Size(218, 22);
            this.m_SearchForConnectionsItem.Text = "Search For Connections";
            this.m_SearchForConnectionsItem.Click += new System.EventHandler(this.SearchForConnectionsItem_Click);
            // 
            // m_DisconnectMenuItem
            // 
            this.m_DisconnectMenuItem.Enabled = false;
            this.m_DisconnectMenuItem.Name = "m_DisconnectMenuItem";
            this.m_DisconnectMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.m_DisconnectMenuItem.Size = new System.Drawing.Size(218, 22);
            this.m_DisconnectMenuItem.Text = "Disconnect";
            this.m_DisconnectMenuItem.Click += new System.EventHandler(this.DisconnectMenuItem_Click);
            // 
            // reconnectToolStripMenuItem
            // 
            this.reconnectToolStripMenuItem.Enabled = false;
            this.reconnectToolStripMenuItem.Name = "reconnectToolStripMenuItem";
            this.reconnectToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.reconnectToolStripMenuItem.Text = "Reconnect";
            this.reconnectToolStripMenuItem.Visible = false;
            // 
            // m_SettingsMenuItem
            // 
            this.m_SettingsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ReadFromDeviceMenuItem,
            this.m_WriteToDeviceMenuItem,
            this.toolStripSeparator1,
            this.saveAsToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.toolStripSeparator2,
            this.m_RestoreDefaultsMenuItem});
            this.m_SettingsMenuItem.Name = "m_SettingsMenuItem";
            this.m_SettingsMenuItem.Size = new System.Drawing.Size(61, 20);
            this.m_SettingsMenuItem.Text = "Settings";
            // 
            // m_ReadFromDeviceMenuItem
            // 
            this.m_ReadFromDeviceMenuItem.Name = "m_ReadFromDeviceMenuItem";
            this.m_ReadFromDeviceMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.m_ReadFromDeviceMenuItem.Size = new System.Drawing.Size(188, 22);
            this.m_ReadFromDeviceMenuItem.Text = "Read From Device";
            this.m_ReadFromDeviceMenuItem.Click += new System.EventHandler(this.ReadFromDeviceMenuItem_Click);
            // 
            // m_WriteToDeviceMenuItem
            // 
            this.m_WriteToDeviceMenuItem.Name = "m_WriteToDeviceMenuItem";
            this.m_WriteToDeviceMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.m_WriteToDeviceMenuItem.Size = new System.Drawing.Size(188, 22);
            this.m_WriteToDeviceMenuItem.Text = "Write To Device";
            this.m_WriteToDeviceMenuItem.Click += new System.EventHandler(this.WriteToDeviceMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(185, 6);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.saveAsToolStripMenuItem.Text = "Save To File";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.loadToolStripMenuItem.Text = "Load From File";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(185, 6);
            // 
            // m_RestoreDefaultsMenuItem
            // 
            this.m_RestoreDefaultsMenuItem.Name = "m_RestoreDefaultsMenuItem";
            this.m_RestoreDefaultsMenuItem.Size = new System.Drawing.Size(188, 22);
            this.m_RestoreDefaultsMenuItem.Text = "Restore Defaults";
            this.m_RestoreDefaultsMenuItem.Click += new System.EventHandler(this.restoreDefaultsToolStripMenuItem_Click);
            // 
            // m_SendCommandMenuItem
            // 
            this.m_SendCommandMenuItem.Enabled = false;
            this.m_SendCommandMenuItem.Name = "m_SendCommandMenuItem";
            this.m_SendCommandMenuItem.Size = new System.Drawing.Size(76, 20);
            this.m_SendCommandMenuItem.Text = "Command";
            // 
            // m_GraphsMenuItem
            // 
            this.m_GraphsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_Form3DViewMenuItem,
            this.toolStripSeparator3,
            this.m_MessagePerSecondMenuItem,
            this.m_GyroscopeMenuItem,
            this.m_AccelerometerMenuItem,
            this.m_MagnetometerMenuItem,
            this.m_BarometerMenuItem,
            this.m_EulerAnglesMenuItem,
            this.m_LinearAccelerationMenuItem,
            this.m_EarthAccelerationMenuItem,
            this.m_AltimeterMenuItem,
            this.m_TemperatureMenuItem,
            this.toolStripMenuItem3,
            this.toolStripMenuItem2,
            this.toolStripMenuItem1,
            this.m_BatteryMenuItem,
            this.toolStripMenuItem4,
            this.m_AnalogueInputsMenuItem,
            this.toolStripMenuItem6,
            this.toolStripMenuItem5});
            this.m_GraphsMenuItem.Name = "m_GraphsMenuItem";
            this.m_GraphsMenuItem.Size = new System.Drawing.Size(56, 20);
            this.m_GraphsMenuItem.Text = "Graphs";
            // 
            // m_Form3DViewMenuItem
            // 
            this.m_Form3DViewMenuItem.CheckOnClick = true;
            this.m_Form3DViewMenuItem.Name = "m_Form3DViewMenuItem";
            this.m_Form3DViewMenuItem.Size = new System.Drawing.Size(194, 22);
            this.m_Form3DViewMenuItem.Text = "3D View";
            this.m_Form3DViewMenuItem.CheckedChanged += new System.EventHandler(this.Form3DViewMenuItem_CheckedChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(191, 6);
            // 
            // m_MessagePerSecondMenuItem
            // 
            this.m_MessagePerSecondMenuItem.CheckOnClick = true;
            this.m_MessagePerSecondMenuItem.Name = "m_MessagePerSecondMenuItem";
            this.m_MessagePerSecondMenuItem.Size = new System.Drawing.Size(194, 22);
            this.m_MessagePerSecondMenuItem.Tag = "Messages Per Second";
            this.m_MessagePerSecondMenuItem.Text = "Messages Per Second";
            this.m_MessagePerSecondMenuItem.CheckedChanged += new System.EventHandler(this.ScopeMenuItem_CheckedChanged);
            // 
            // m_GyroscopeMenuItem
            // 
            this.m_GyroscopeMenuItem.CheckOnClick = true;
            this.m_GyroscopeMenuItem.Name = "m_GyroscopeMenuItem";
            this.m_GyroscopeMenuItem.Size = new System.Drawing.Size(194, 22);
            this.m_GyroscopeMenuItem.Tag = "Gyroscope";
            this.m_GyroscopeMenuItem.Text = "Gyroscope";
            this.m_GyroscopeMenuItem.CheckedChanged += new System.EventHandler(this.ScopeMenuItem_CheckedChanged);
            // 
            // m_AccelerometerMenuItem
            // 
            this.m_AccelerometerMenuItem.CheckOnClick = true;
            this.m_AccelerometerMenuItem.Name = "m_AccelerometerMenuItem";
            this.m_AccelerometerMenuItem.Size = new System.Drawing.Size(194, 22);
            this.m_AccelerometerMenuItem.Tag = "Accelerometer";
            this.m_AccelerometerMenuItem.Text = "Accelerometer";
            this.m_AccelerometerMenuItem.CheckedChanged += new System.EventHandler(this.ScopeMenuItem_CheckedChanged);
            // 
            // m_MagnetometerMenuItem
            // 
            this.m_MagnetometerMenuItem.CheckOnClick = true;
            this.m_MagnetometerMenuItem.Name = "m_MagnetometerMenuItem";
            this.m_MagnetometerMenuItem.Size = new System.Drawing.Size(194, 22);
            this.m_MagnetometerMenuItem.Tag = "Magnetometer";
            this.m_MagnetometerMenuItem.Text = "Magnetometer";
            this.m_MagnetometerMenuItem.CheckedChanged += new System.EventHandler(this.ScopeMenuItem_CheckedChanged);
            // 
            // m_BarometerMenuItem
            // 
            this.m_BarometerMenuItem.CheckOnClick = true;
            this.m_BarometerMenuItem.Name = "m_BarometerMenuItem";
            this.m_BarometerMenuItem.Size = new System.Drawing.Size(194, 22);
            this.m_BarometerMenuItem.Tag = "Barometer";
            this.m_BarometerMenuItem.Text = "Barometer";
            this.m_BarometerMenuItem.CheckedChanged += new System.EventHandler(this.ScopeMenuItem_CheckedChanged);
            // 
            // m_EulerAnglesMenuItem
            // 
            this.m_EulerAnglesMenuItem.CheckOnClick = true;
            this.m_EulerAnglesMenuItem.Name = "m_EulerAnglesMenuItem";
            this.m_EulerAnglesMenuItem.Size = new System.Drawing.Size(194, 22);
            this.m_EulerAnglesMenuItem.Tag = "Euler Angles";
            this.m_EulerAnglesMenuItem.Text = "Euler Angles";
            this.m_EulerAnglesMenuItem.CheckedChanged += new System.EventHandler(this.ScopeMenuItem_CheckedChanged);
            // 
            // m_LinearAccelerationMenuItem
            // 
            this.m_LinearAccelerationMenuItem.CheckOnClick = true;
            this.m_LinearAccelerationMenuItem.Name = "m_LinearAccelerationMenuItem";
            this.m_LinearAccelerationMenuItem.Size = new System.Drawing.Size(194, 22);
            this.m_LinearAccelerationMenuItem.Tag = "Linear Acceleration";
            this.m_LinearAccelerationMenuItem.Text = "Linear Acceleration";
            this.m_LinearAccelerationMenuItem.CheckedChanged += new System.EventHandler(this.ScopeMenuItem_CheckedChanged);
            // 
            // m_EarthAccelerationMenuItem
            // 
            this.m_EarthAccelerationMenuItem.CheckOnClick = true;
            this.m_EarthAccelerationMenuItem.Name = "m_EarthAccelerationMenuItem";
            this.m_EarthAccelerationMenuItem.Size = new System.Drawing.Size(194, 22);
            this.m_EarthAccelerationMenuItem.Tag = "Earth Acceleration";
            this.m_EarthAccelerationMenuItem.Text = "Earth Acceleration";
            this.m_EarthAccelerationMenuItem.CheckedChanged += new System.EventHandler(this.ScopeMenuItem_CheckedChanged);
            // 
            // m_AltimeterMenuItem
            // 
            this.m_AltimeterMenuItem.CheckOnClick = true;
            this.m_AltimeterMenuItem.Name = "m_AltimeterMenuItem";
            this.m_AltimeterMenuItem.Size = new System.Drawing.Size(194, 22);
            this.m_AltimeterMenuItem.Tag = "Altimeter";
            this.m_AltimeterMenuItem.Text = "Altimeter";
            this.m_AltimeterMenuItem.CheckedChanged += new System.EventHandler(this.ScopeMenuItem_CheckedChanged);
            // 
            // m_TemperatureMenuItem
            // 
            this.m_TemperatureMenuItem.CheckOnClick = true;
            this.m_TemperatureMenuItem.Name = "m_TemperatureMenuItem";
            this.m_TemperatureMenuItem.Size = new System.Drawing.Size(194, 22);
            this.m_TemperatureMenuItem.Tag = "Temperature";
            this.m_TemperatureMenuItem.Text = "Temperature";
            this.m_TemperatureMenuItem.CheckedChanged += new System.EventHandler(this.ScopeMenuItem_CheckedChanged);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.CheckOnClick = true;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(194, 22);
            this.toolStripMenuItem3.Tag = "Humidity";
            this.toolStripMenuItem3.Text = "Humidity";
            this.toolStripMenuItem3.CheckedChanged += new System.EventHandler(this.ScopeMenuItem_CheckedChanged);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.CheckOnClick = true;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(194, 22);
            this.toolStripMenuItem2.Tag = "Battery Percentage";
            this.toolStripMenuItem2.Text = "Battery Percentage";
            this.toolStripMenuItem2.CheckedChanged += new System.EventHandler(this.ScopeMenuItem_CheckedChanged);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.CheckOnClick = true;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(194, 22);
            this.toolStripMenuItem1.Tag = "Battery Time To Empty";
            this.toolStripMenuItem1.Text = "Battery Time To Empty";
            this.toolStripMenuItem1.CheckedChanged += new System.EventHandler(this.ScopeMenuItem_CheckedChanged);
            // 
            // m_BatteryMenuItem
            // 
            this.m_BatteryMenuItem.CheckOnClick = true;
            this.m_BatteryMenuItem.Name = "m_BatteryMenuItem";
            this.m_BatteryMenuItem.Size = new System.Drawing.Size(194, 22);
            this.m_BatteryMenuItem.Tag = "Battery Voltage";
            this.m_BatteryMenuItem.Text = "Battery Voltage";
            this.m_BatteryMenuItem.CheckedChanged += new System.EventHandler(this.ScopeMenuItem_CheckedChanged);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.CheckOnClick = true;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(194, 22);
            this.toolStripMenuItem4.Tag = "Battery Current";
            this.toolStripMenuItem4.Text = "Battery Current";
            this.toolStripMenuItem4.CheckedChanged += new System.EventHandler(this.ScopeMenuItem_CheckedChanged);
            // 
            // m_AnalogueInputsMenuItem
            // 
            this.m_AnalogueInputsMenuItem.CheckOnClick = true;
            this.m_AnalogueInputsMenuItem.Name = "m_AnalogueInputsMenuItem";
            this.m_AnalogueInputsMenuItem.Size = new System.Drawing.Size(194, 22);
            this.m_AnalogueInputsMenuItem.Tag = "Analogue Inputs";
            this.m_AnalogueInputsMenuItem.Text = "Analogue Inputs";
            this.m_AnalogueInputsMenuItem.CheckedChanged += new System.EventHandler(this.ScopeMenuItem_CheckedChanged);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.CheckOnClick = true;
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(194, 22);
            this.toolStripMenuItem6.Tag = "RSSI Power";
            this.toolStripMenuItem6.Text = "RSSI Power";
            this.toolStripMenuItem6.CheckedChanged += new System.EventHandler(this.ScopeMenuItem_CheckedChanged);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.CheckOnClick = true;
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(194, 22);
            this.toolStripMenuItem5.Tag = "RSSI Percentage";
            this.toolStripMenuItem5.Text = "RSSI Percentage";
            this.toolStripMenuItem5.CheckedChanged += new System.EventHandler(this.ScopeMenuItem_CheckedChanged);
            // 
            // m_ToolsMenuItem
            // 
            this.m_ToolsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_AuxSerialTerminalMenuItem,
            this.m_LogDataMenuItem,
            this.m_ConvertSDCardFile,
            this.m_UploadFirmwareMenuItem,
            this.m_GyroscopeAndAccelerometerCalibrationMenuItem,
            this.m_MagneticCalibrationMenuItem,
            this.m_SaveCalibrationToFileMenuItem,
            this.m_LoadCalibrationValuesFromFileMenuItem});
            this.m_ToolsMenuItem.Name = "m_ToolsMenuItem";
            this.m_ToolsMenuItem.Size = new System.Drawing.Size(47, 20);
            this.m_ToolsMenuItem.Text = "Tools";
            // 
            // m_AuxSerialTerminalMenuItem
            // 
            this.m_AuxSerialTerminalMenuItem.CheckOnClick = true;
            this.m_AuxSerialTerminalMenuItem.Name = "m_AuxSerialTerminalMenuItem";
            this.m_AuxSerialTerminalMenuItem.Size = new System.Drawing.Size(294, 22);
            this.m_AuxSerialTerminalMenuItem.Text = "Auxiliary Serial Terminal";
            this.m_AuxSerialTerminalMenuItem.CheckedChanged += new System.EventHandler(this.AuxSerialTerminalMenuItem_CheckedChanged);
            // 
            // m_LogDataMenuItem
            // 
            this.m_LogDataMenuItem.CheckOnClick = true;
            this.m_LogDataMenuItem.Name = "m_LogDataMenuItem";
            this.m_LogDataMenuItem.Size = new System.Drawing.Size(294, 22);
            this.m_LogDataMenuItem.Text = "Data Logger";
            this.m_LogDataMenuItem.CheckedChanged += new System.EventHandler(this.LogDataMenuItem_CheckedChanged);
            // 
            // m_ConvertSDCardFile
            // 
            this.m_ConvertSDCardFile.CheckOnClick = true;
            this.m_ConvertSDCardFile.Name = "m_ConvertSDCardFile";
            this.m_ConvertSDCardFile.Size = new System.Drawing.Size(294, 22);
            this.m_ConvertSDCardFile.Text = "SD Card File Converter";
            this.m_ConvertSDCardFile.CheckedChanged += new System.EventHandler(this.ConvertSDCardFile_CheckedChanged);
            // 
            // m_UploadFirmwareMenuItem
            // 
            this.m_UploadFirmwareMenuItem.CheckOnClick = true;
            this.m_UploadFirmwareMenuItem.Name = "m_UploadFirmwareMenuItem";
            this.m_UploadFirmwareMenuItem.Size = new System.Drawing.Size(294, 22);
            this.m_UploadFirmwareMenuItem.Text = "Firmware Uploader";
            this.m_UploadFirmwareMenuItem.CheckedChanged += new System.EventHandler(this.UploadFirmwareMenuItem_CheckedChanged);
            // 
            // m_GyroscopeAndAccelerometerCalibrationMenuItem
            // 
            this.m_GyroscopeAndAccelerometerCalibrationMenuItem.Enabled = false;
            this.m_GyroscopeAndAccelerometerCalibrationMenuItem.Name = "m_GyroscopeAndAccelerometerCalibrationMenuItem";
            this.m_GyroscopeAndAccelerometerCalibrationMenuItem.Size = new System.Drawing.Size(294, 22);
            this.m_GyroscopeAndAccelerometerCalibrationMenuItem.Text = "Gyroscope and Accelerometer Calibration";
            this.m_GyroscopeAndAccelerometerCalibrationMenuItem.Visible = false;
            // 
            // m_MagneticCalibrationMenuItem
            // 
            this.m_MagneticCalibrationMenuItem.Enabled = false;
            this.m_MagneticCalibrationMenuItem.Name = "m_MagneticCalibrationMenuItem";
            this.m_MagneticCalibrationMenuItem.Size = new System.Drawing.Size(294, 22);
            this.m_MagneticCalibrationMenuItem.Text = "Magnetic Calibration";
            this.m_MagneticCalibrationMenuItem.Visible = false;
            // 
            // m_SaveCalibrationToFileMenuItem
            // 
            this.m_SaveCalibrationToFileMenuItem.Enabled = false;
            this.m_SaveCalibrationToFileMenuItem.Name = "m_SaveCalibrationToFileMenuItem";
            this.m_SaveCalibrationToFileMenuItem.Size = new System.Drawing.Size(294, 22);
            this.m_SaveCalibrationToFileMenuItem.Text = "Save Calibration To File";
            this.m_SaveCalibrationToFileMenuItem.Visible = false;
            // 
            // m_LoadCalibrationValuesFromFileMenuItem
            // 
            this.m_LoadCalibrationValuesFromFileMenuItem.Enabled = false;
            this.m_LoadCalibrationValuesFromFileMenuItem.Name = "m_LoadCalibrationValuesFromFileMenuItem";
            this.m_LoadCalibrationValuesFromFileMenuItem.Size = new System.Drawing.Size(294, 22);
            this.m_LoadCalibrationValuesFromFileMenuItem.Text = "Load Calibration From File";
            this.m_LoadCalibrationValuesFromFileMenuItem.Visible = false;
            // 
            // m_HelpMenuItem
            // 
            this.m_HelpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_AboutMenuItem,
            this.optionsToolStripMenuItem});
            this.m_HelpMenuItem.Name = "m_HelpMenuItem";
            this.m_HelpMenuItem.Size = new System.Drawing.Size(44, 20);
            this.m_HelpMenuItem.Text = "Help";
            // 
            // m_AboutMenuItem
            // 
            this.m_AboutMenuItem.Name = "m_AboutMenuItem";
            this.m_AboutMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.m_AboutMenuItem.Size = new System.Drawing.Size(126, 22);
            this.m_AboutMenuItem.Text = "About";
            this.m_AboutMenuItem.Visible = false;
            this.m_AboutMenuItem.Click += new System.EventHandler(this.m_AboutMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.m_TabControl);
            this.Controls.Add(this.m_StatusStrip);
            this.Controls.Add(this.m_MenuStrip);
            this.MainMenuStrip = this.m_MenuStrip;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.m_TabControl.ResumeLayout(false);
            this.m_TerminalTab.ResumeLayout(false);
            this.m_MessagesTab.ResumeLayout(false);
            this.m_SettingsTab.ResumeLayout(false);
            this.m_StatusStrip.ResumeLayout(false);
            this.m_StatusStrip.PerformLayout();
            this.m_MenuStrip.ResumeLayout(false);
            this.m_MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip m_MenuStrip;
        private System.Windows.Forms.StatusStrip m_StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel m_TotalReceived;
        private System.Windows.Forms.ToolStripStatusLabel m_TotalSent;
        private System.Windows.Forms.ToolStripStatusLabel m_SendRate;
        private System.Windows.Forms.TabControl m_TabControl;
        private System.Windows.Forms.TabPage m_TerminalTab;
        private Panels.Terminal m_TerminalPanel;
        private System.Windows.Forms.ToolStripMenuItem m_ConnectionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_ConnectUdpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_ConnectSerialMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_SearchForConnectionsItem;
        private System.Windows.Forms.Timer m_FastTimer;
        private System.Windows.Forms.ToolStripMenuItem m_DisconnectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_OpenUdpConnectionDialogMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_SettingsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_ReadFromDeviceMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_WriteToDeviceMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_SendCommandMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_GraphsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_Form3DViewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_GyroscopeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_AccelerometerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_MagnetometerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_BarometerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_ToolsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_LogDataMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_ConvertSDCardFile;
        private System.Windows.Forms.ToolStripMenuItem m_GyroscopeAndAccelerometerCalibrationMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_UploadFirmwareMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_SaveCalibrationToFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_LoadCalibrationValuesFromFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_MagneticCalibrationMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_HelpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_AboutMenuItem;
        private System.Windows.Forms.TabPage m_SettingsTab;
        private Panels.SettingsPanel m_SettingsPanel;
        private System.Windows.Forms.ToolStripStatusLabel m_StatusSeparator1;
        private System.Windows.Forms.ToolStripStatusLabel m_StatusSeparator2;
        private System.Windows.Forms.ToolStripStatusLabel m_BatteryStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem m_BatteryMenuItem;
        private System.Windows.Forms.Timer m_SlowTimer;
        private System.Windows.Forms.ToolStripStatusLabel m_BatteryChargerStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem m_OpenSerialConnectionDialog;
        private System.Windows.Forms.ToolStripSeparator m_SerialSeparator;
        private System.Windows.Forms.ToolStripMenuItem m_AltimeterMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_EarthAccelerationMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_TemperatureMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_AnalogueInputsMenuItem;
        private System.Windows.Forms.TabPage m_MessagesTab;
        private Panels.FilterPanel filterPanel1;
        private System.Windows.Forms.ToolStripMenuItem m_LinearAccelerationMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel m_ReceiveRate;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem m_MessagePerSecondMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem m_RestoreDefaultsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_AuxSerialTerminalMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem m_EulerAnglesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
    }
}

