namespace NgimuSynchronisedNetworkManager
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.synchronisationMasterTimeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.connectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchForConnectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendRatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendCommandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataLoggerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataForwardingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureWirelessSettingsViaUSBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerFast = new System.Windows.Forms.Timer(this.components);
            this.Device = new NgimuForms.Controls.TextAndIconColumn();
            this.Master = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.MessagesReceived = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MessagesSent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatteryPercentage = new NgimuSynchronisedNetworkManager.Controls.DataGridViewProgressColumn();
            this.Signal = new NgimuSynchronisedNetworkManager.Controls.DataGridViewProgressColumn();
            this.Identify = new System.Windows.Forms.DataGridViewButtonColumn();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.synchronisationMasterTimeLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // synchronisationMasterTimeLabel
            // 
            this.synchronisationMasterTimeLabel.Name = "synchronisationMasterTimeLabel";
            this.synchronisationMasterTimeLabel.Size = new System.Drawing.Size(76, 17);
            this.synchronisationMasterTimeLabel.Tag = "Master Time: {0}";
            this.synchronisationMasterTimeLabel.Text = "Master Time:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Device,
            this.Master,
            this.MessagesReceived,
            this.MessagesSent,
            this.BatteryPercentage,
            this.Signal,
            this.Identify});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 24);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(784, 515);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionsToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.sendCommandToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // connectionsToolStripMenuItem
            // 
            this.connectionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchForConnectionsToolStripMenuItem,
            this.disconnectAllToolStripMenuItem});
            this.connectionsToolStripMenuItem.Name = "connectionsToolStripMenuItem";
            this.connectionsToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.connectionsToolStripMenuItem.Text = "Connection";
            // 
            // searchForConnectionsToolStripMenuItem
            // 
            this.searchForConnectionsToolStripMenuItem.Name = "searchForConnectionsToolStripMenuItem";
            this.searchForConnectionsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.searchForConnectionsToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.searchForConnectionsToolStripMenuItem.Text = "Search For Connections";
            this.searchForConnectionsToolStripMenuItem.Click += new System.EventHandler(this.searchForConnectionsToolStripMenuItem_Click);
            // 
            // disconnectAllToolStripMenuItem
            // 
            this.disconnectAllToolStripMenuItem.Name = "disconnectAllToolStripMenuItem";
            this.disconnectAllToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.disconnectAllToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.disconnectAllToolStripMenuItem.Text = "Disconnect All";
            this.disconnectAllToolStripMenuItem.Click += new System.EventHandler(this.disconnectAllToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendRatesToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // sendRatesToolStripMenuItem
            // 
            this.sendRatesToolStripMenuItem.Name = "sendRatesToolStripMenuItem";
            this.sendRatesToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.sendRatesToolStripMenuItem.Text = "Send Rates";
            this.sendRatesToolStripMenuItem.Click += new System.EventHandler(this.sendRatesToolStripMenuItem_Click);
            // 
            // sendCommandToolStripMenuItem
            // 
            this.sendCommandToolStripMenuItem.Name = "sendCommandToolStripMenuItem";
            this.sendCommandToolStripMenuItem.Size = new System.Drawing.Size(105, 20);
            this.sendCommandToolStripMenuItem.Text = "Send Command";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataLoggerToolStripMenuItem,
            this.dataForwardingToolStripMenuItem,
            this.configureWirelessSettingsViaUSBToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // dataLoggerToolStripMenuItem
            // 
            this.dataLoggerToolStripMenuItem.Name = "dataLoggerToolStripMenuItem";
            this.dataLoggerToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.dataLoggerToolStripMenuItem.Text = "Data Logger";
            this.dataLoggerToolStripMenuItem.Click += new System.EventHandler(this.dataLoggerToolStripMenuItem_Click);
            // 
            // dataForwardingToolStripMenuItem
            // 
            this.dataForwardingToolStripMenuItem.Name = "dataForwardingToolStripMenuItem";
            this.dataForwardingToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.dataForwardingToolStripMenuItem.Text = "Data Forwarding";
            this.dataForwardingToolStripMenuItem.Click += new System.EventHandler(this.dataForwardingToolStripMenuItem_Click);
            // 
            // configureWirelessSettingsViaUSBToolStripMenuItem
            // 
            this.configureWirelessSettingsViaUSBToolStripMenuItem.Name = "configureWirelessSettingsViaUSBToolStripMenuItem";
            this.configureWirelessSettingsViaUSBToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.configureWirelessSettingsViaUSBToolStripMenuItem.Text = "Configure Wireless Settings Via USB";
            this.configureWirelessSettingsViaUSBToolStripMenuItem.Click += new System.EventHandler(this.ConfigureWirelessSettingsViaUSBToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // timerFast
            // 
            this.timerFast.Enabled = true;
            this.timerFast.Interval = 50;
            this.timerFast.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Device
            // 
            this.Device.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Device.HeaderText = "Device";
            this.Device.MinimumWidth = 100;
            this.Device.Name = "Device";
            // 
            // Master
            // 
            this.Master.HeaderText = "Master";
            this.Master.MinimumWidth = 60;
            this.Master.Name = "Master";
            this.Master.ReadOnly = true;
            this.Master.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Master.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Master.Width = 60;
            // 
            // MessagesReceived
            // 
            this.MessagesReceived.HeaderText = "Received";
            this.MessagesReceived.MinimumWidth = 90;
            this.MessagesReceived.Name = "MessagesReceived";
            this.MessagesReceived.ReadOnly = true;
            this.MessagesReceived.Width = 90;
            // 
            // MessagesSent
            // 
            this.MessagesSent.HeaderText = "Sent";
            this.MessagesSent.MinimumWidth = 90;
            this.MessagesSent.Name = "MessagesSent";
            this.MessagesSent.ReadOnly = true;
            this.MessagesSent.Width = 90;
            // 
            // BatteryPercentage
            // 
            this.BatteryPercentage.HeaderText = "Battery";
            this.BatteryPercentage.MinimumWidth = 120;
            this.BatteryPercentage.Name = "BatteryPercentage";
            this.BatteryPercentage.ReadOnly = true;
            this.BatteryPercentage.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.BatteryPercentage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.BatteryPercentage.Width = 120;
            // 
            // Signal
            // 
            this.Signal.HeaderText = "Signal";
            this.Signal.MinimumWidth = 120;
            this.Signal.Name = "Signal";
            this.Signal.ReadOnly = true;
            this.Signal.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Signal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Signal.Width = 120;
            // 
            // Identify
            // 
            this.Identify.HeaderText = "";
            this.Identify.MinimumWidth = 60;
            this.Identify.Name = "Identify";
            this.Identify.ReadOnly = true;
            this.Identify.Text = "Send";
            this.Identify.Width = 60;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "NGIMU Synchronised Logger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem connectionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchForConnectionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectAllToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Timer timerFast;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataLoggerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendCommandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendRatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel synchronisationMasterTimeLabel;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureWirelessSettingsViaUSBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataForwardingToolStripMenuItem;
        private NgimuForms.Controls.TextAndIconColumn Device;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Master;
        private System.Windows.Forms.DataGridViewTextBoxColumn MessagesReceived;
        private System.Windows.Forms.DataGridViewTextBoxColumn MessagesSent;
        private Controls.DataGridViewProgressColumn BatteryPercentage;
        private Controls.DataGridViewProgressColumn Signal;
        private System.Windows.Forms.DataGridViewButtonColumn Identify;
    }
}

