namespace NgimuGui.DialogsAndWindows
{
    partial class OptionsDialog
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
            this.rememberWindowLayout = new System.Windows.Forms.CheckBox();
            this.searchForConnections = new System.Windows.Forms.GroupBox();
            this.configureUniqueUdpConnection = new System.Windows.Forms.CheckBox();
            this.openConnectionToFirstDeviceFound = new System.Windows.Forms.CheckBox();
            this.searchTypeLabel = new System.Windows.Forms.Label();
            this.searchForConnectionsAtStartup = new System.Windows.Forms.CheckBox();
            this.searchType = new System.Windows.Forms.ComboBox();
            this.readDeviceSettingsAfterOpeningConnection = new System.Windows.Forms.CheckBox();
            this.m_OK = new System.Windows.Forms.Button();
            this.m_Cancel = new System.Windows.Forms.Button();
            this.disconnectFromSerialAfterSleepAndResetCommands = new System.Windows.Forms.CheckBox();
            this.displayReceivedErrorMessagesInMessageBox = new System.Windows.Forms.CheckBox();
            this.maximumNumberOfRetries = new System.Windows.Forms.NumericUpDown();
            this.timeout = new System.Windows.Forms.NumericUpDown();
            this.maximumNumberOfRetriesLabel = new System.Windows.Forms.Label();
            this.timeoutLabel = new System.Windows.Forms.Label();
            this.m_ResetToDefaults = new System.Windows.Forms.Button();
            this.firmwareUploader = new System.Windows.Forms.GroupBox();
            this.searchForConnectionsAfterSuccessfulUpload = new System.Windows.Forms.CheckBox();
            this.allowUploadWithoutSerialConnection = new System.Windows.Forms.CheckBox();
            this.connectDisconnectBehaviour = new System.Windows.Forms.GroupBox();
            this.commandAndSettingsConfirmation = new System.Windows.Forms.GroupBox();
            this.misc = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.graphSampleBufferSize = new System.Windows.Forms.NumericUpDown();
            this.searchForConnections.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maximumNumberOfRetries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeout)).BeginInit();
            this.firmwareUploader.SuspendLayout();
            this.connectDisconnectBehaviour.SuspendLayout();
            this.commandAndSettingsConfirmation.SuspendLayout();
            this.misc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.graphSampleBufferSize)).BeginInit();
            this.SuspendLayout();
            // 
            // rememberWindowLayout
            // 
            this.rememberWindowLayout.AutoSize = true;
            this.rememberWindowLayout.Location = new System.Drawing.Point(10, 42);
            this.rememberWindowLayout.Name = "rememberWindowLayout";
            this.rememberWindowLayout.Size = new System.Drawing.Size(147, 17);
            this.rememberWindowLayout.TabIndex = 1;
            this.rememberWindowLayout.Text = "Remember window layout";
            this.rememberWindowLayout.UseVisualStyleBackColor = true;
            // 
            // searchForConnections
            // 
            this.searchForConnections.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchForConnections.Controls.Add(this.configureUniqueUdpConnection);
            this.searchForConnections.Controls.Add(this.openConnectionToFirstDeviceFound);
            this.searchForConnections.Controls.Add(this.searchTypeLabel);
            this.searchForConnections.Controls.Add(this.searchForConnectionsAtStartup);
            this.searchForConnections.Controls.Add(this.searchType);
            this.searchForConnections.Location = new System.Drawing.Point(12, 12);
            this.searchForConnections.Name = "searchForConnections";
            this.searchForConnections.Size = new System.Drawing.Size(457, 118);
            this.searchForConnections.TabIndex = 0;
            this.searchForConnections.TabStop = false;
            this.searchForConnections.Text = "Search For Connections";
            // 
            // configureUniqueUdpConnection
            // 
            this.configureUniqueUdpConnection.AutoSize = true;
            this.configureUniqueUdpConnection.Location = new System.Drawing.Point(10, 69);
            this.configureUniqueUdpConnection.Name = "configureUniqueUdpConnection";
            this.configureUniqueUdpConnection.Size = new System.Drawing.Size(332, 17);
            this.configureUniqueUdpConnection.TabIndex = 0;
            this.configureUniqueUdpConnection.Text = "Configure a unique UDP connection when opening a connection";
            this.configureUniqueUdpConnection.UseVisualStyleBackColor = true;
            // 
            // openConnectionToFirstDeviceFound
            // 
            this.openConnectionToFirstDeviceFound.AutoSize = true;
            this.openConnectionToFirstDeviceFound.Location = new System.Drawing.Point(10, 46);
            this.openConnectionToFirstDeviceFound.Name = "openConnectionToFirstDeviceFound";
            this.openConnectionToFirstDeviceFound.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.openConnectionToFirstDeviceFound.Size = new System.Drawing.Size(204, 17);
            this.openConnectionToFirstDeviceFound.TabIndex = 1;
            this.openConnectionToFirstDeviceFound.Text = "Open connection to first device found";
            this.openConnectionToFirstDeviceFound.UseVisualStyleBackColor = true;
            // 
            // searchTypeLabel
            // 
            this.searchTypeLabel.AutoSize = true;
            this.searchTypeLabel.Location = new System.Drawing.Point(7, 22);
            this.searchTypeLabel.Name = "searchTypeLabel";
            this.searchTypeLabel.Size = new System.Drawing.Size(71, 13);
            this.searchTypeLabel.TabIndex = 2;
            this.searchTypeLabel.Text = "Search Type:";
            // 
            // searchForConnectionsAtStartup
            // 
            this.searchForConnectionsAtStartup.AutoSize = true;
            this.searchForConnectionsAtStartup.Location = new System.Drawing.Point(10, 92);
            this.searchForConnectionsAtStartup.Name = "searchForConnectionsAtStartup";
            this.searchForConnectionsAtStartup.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.searchForConnectionsAtStartup.Size = new System.Drawing.Size(183, 17);
            this.searchForConnectionsAtStartup.TabIndex = 2;
            this.searchForConnectionsAtStartup.Text = "Search for connections at startup";
            this.searchForConnectionsAtStartup.UseVisualStyleBackColor = true;
            // 
            // searchType
            // 
            this.searchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchType.FormattingEnabled = true;
            this.searchType.Location = new System.Drawing.Point(84, 19);
            this.searchType.Name = "searchType";
            this.searchType.Size = new System.Drawing.Size(73, 21);
            this.searchType.TabIndex = 0;
            // 
            // readDeviceSettingsAfterOpeningConnection
            // 
            this.readDeviceSettingsAfterOpeningConnection.AutoSize = true;
            this.readDeviceSettingsAfterOpeningConnection.Location = new System.Drawing.Point(10, 42);
            this.readDeviceSettingsAfterOpeningConnection.Name = "readDeviceSettingsAfterOpeningConnection";
            this.readDeviceSettingsAfterOpeningConnection.Size = new System.Drawing.Size(247, 17);
            this.readDeviceSettingsAfterOpeningConnection.TabIndex = 1;
            this.readDeviceSettingsAfterOpeningConnection.Text = "Read device settings after opening connection";
            this.readDeviceSettingsAfterOpeningConnection.UseVisualStyleBackColor = true;
            // 
            // m_OK
            // 
            this.m_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_OK.Location = new System.Drawing.Point(313, 469);
            this.m_OK.Name = "m_OK";
            this.m_OK.Size = new System.Drawing.Size(75, 23);
            this.m_OK.TabIndex = 5;
            this.m_OK.Text = "OK";
            this.m_OK.UseVisualStyleBackColor = true;
            this.m_OK.Click += new System.EventHandler(this.m_OK_Click);
            // 
            // m_Cancel
            // 
            this.m_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_Cancel.Location = new System.Drawing.Point(394, 469);
            this.m_Cancel.Name = "m_Cancel";
            this.m_Cancel.Size = new System.Drawing.Size(75, 23);
            this.m_Cancel.TabIndex = 6;
            this.m_Cancel.Text = "Cancel";
            this.m_Cancel.UseVisualStyleBackColor = true;
            this.m_Cancel.Click += new System.EventHandler(this.m_Cancel_Click);
            // 
            // disconnectFromSerialAfterSleepAndResetCommands
            // 
            this.disconnectFromSerialAfterSleepAndResetCommands.AutoSize = true;
            this.disconnectFromSerialAfterSleepAndResetCommands.Location = new System.Drawing.Point(10, 19);
            this.disconnectFromSerialAfterSleepAndResetCommands.Name = "disconnectFromSerialAfterSleepAndResetCommands";
            this.disconnectFromSerialAfterSleepAndResetCommands.Size = new System.Drawing.Size(290, 17);
            this.disconnectFromSerialAfterSleepAndResetCommands.TabIndex = 2;
            this.disconnectFromSerialAfterSleepAndResetCommands.Text = "Disconnect from serial after Sleep and Reset commands";
            this.disconnectFromSerialAfterSleepAndResetCommands.UseVisualStyleBackColor = true;
            // 
            // displayReceivedErrorMessagesInMessageBox
            // 
            this.displayReceivedErrorMessagesInMessageBox.AutoSize = true;
            this.displayReceivedErrorMessagesInMessageBox.Location = new System.Drawing.Point(10, 19);
            this.displayReceivedErrorMessagesInMessageBox.Name = "displayReceivedErrorMessagesInMessageBox";
            this.displayReceivedErrorMessagesInMessageBox.Size = new System.Drawing.Size(254, 17);
            this.displayReceivedErrorMessagesInMessageBox.TabIndex = 0;
            this.displayReceivedErrorMessagesInMessageBox.Text = "Display received error messages in message box";
            this.displayReceivedErrorMessagesInMessageBox.UseVisualStyleBackColor = true;
            // 
            // maximumNumberOfRetries
            // 
            this.maximumNumberOfRetries.Location = new System.Drawing.Point(10, 19);
            this.maximumNumberOfRetries.Name = "maximumNumberOfRetries";
            this.maximumNumberOfRetries.Size = new System.Drawing.Size(51, 20);
            this.maximumNumberOfRetries.TabIndex = 0;
            // 
            // timeout
            // 
            this.timeout.Location = new System.Drawing.Point(10, 45);
            this.timeout.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.timeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.timeout.Name = "timeout";
            this.timeout.Size = new System.Drawing.Size(51, 20);
            this.timeout.TabIndex = 1;
            this.timeout.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // maximumNumberOfRetriesLabel
            // 
            this.maximumNumberOfRetriesLabel.AutoSize = true;
            this.maximumNumberOfRetriesLabel.Location = new System.Drawing.Point(67, 22);
            this.maximumNumberOfRetriesLabel.Name = "maximumNumberOfRetriesLabel";
            this.maximumNumberOfRetriesLabel.Size = new System.Drawing.Size(132, 13);
            this.maximumNumberOfRetriesLabel.TabIndex = 11;
            this.maximumNumberOfRetriesLabel.Text = "Maximum number of retries";
            // 
            // timeoutLabel
            // 
            this.timeoutLabel.AutoSize = true;
            this.timeoutLabel.Location = new System.Drawing.Point(67, 48);
            this.timeoutLabel.Name = "timeoutLabel";
            this.timeoutLabel.Size = new System.Drawing.Size(67, 13);
            this.timeoutLabel.TabIndex = 12;
            this.timeoutLabel.Text = "Timeout (ms)";
            // 
            // m_ResetToDefaults
            // 
            this.m_ResetToDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_ResetToDefaults.Location = new System.Drawing.Point(12, 469);
            this.m_ResetToDefaults.Name = "m_ResetToDefaults";
            this.m_ResetToDefaults.Size = new System.Drawing.Size(75, 23);
            this.m_ResetToDefaults.TabIndex = 7;
            this.m_ResetToDefaults.Text = "Default";
            this.m_ResetToDefaults.UseVisualStyleBackColor = true;
            this.m_ResetToDefaults.Click += new System.EventHandler(this.m_ResetToDefaults_Click);
            // 
            // firmwareUploader
            // 
            this.firmwareUploader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.firmwareUploader.Controls.Add(this.searchForConnectionsAfterSuccessfulUpload);
            this.firmwareUploader.Controls.Add(this.allowUploadWithoutSerialConnection);
            this.firmwareUploader.Location = new System.Drawing.Point(12, 291);
            this.firmwareUploader.Name = "firmwareUploader";
            this.firmwareUploader.Size = new System.Drawing.Size(457, 71);
            this.firmwareUploader.TabIndex = 3;
            this.firmwareUploader.TabStop = false;
            this.firmwareUploader.Text = "Firmware Uploader";
            // 
            // searchForConnectionsAfterSuccessfulUpload
            // 
            this.searchForConnectionsAfterSuccessfulUpload.AutoSize = true;
            this.searchForConnectionsAfterSuccessfulUpload.Location = new System.Drawing.Point(10, 43);
            this.searchForConnectionsAfterSuccessfulUpload.Name = "searchForConnectionsAfterSuccessfulUpload";
            this.searchForConnectionsAfterSuccessfulUpload.Size = new System.Drawing.Size(248, 17);
            this.searchForConnectionsAfterSuccessfulUpload.TabIndex = 1;
            this.searchForConnectionsAfterSuccessfulUpload.Text = "Search for connections after successful upload";
            this.searchForConnectionsAfterSuccessfulUpload.UseVisualStyleBackColor = true;
            // 
            // allowUploadWithoutSerialConnection
            // 
            this.allowUploadWithoutSerialConnection.AutoSize = true;
            this.allowUploadWithoutSerialConnection.Location = new System.Drawing.Point(10, 20);
            this.allowUploadWithoutSerialConnection.Name = "allowUploadWithoutSerialConnection";
            this.allowUploadWithoutSerialConnection.Size = new System.Drawing.Size(206, 17);
            this.allowUploadWithoutSerialConnection.TabIndex = 0;
            this.allowUploadWithoutSerialConnection.Text = "Allow upload without serial connection";
            this.allowUploadWithoutSerialConnection.UseVisualStyleBackColor = true;
            // 
            // connectDisconnectBehaviour
            // 
            this.connectDisconnectBehaviour.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.connectDisconnectBehaviour.Controls.Add(this.readDeviceSettingsAfterOpeningConnection);
            this.connectDisconnectBehaviour.Controls.Add(this.disconnectFromSerialAfterSleepAndResetCommands);
            this.connectDisconnectBehaviour.Location = new System.Drawing.Point(12, 136);
            this.connectDisconnectBehaviour.Name = "connectDisconnectBehaviour";
            this.connectDisconnectBehaviour.Size = new System.Drawing.Size(457, 69);
            this.connectDisconnectBehaviour.TabIndex = 1;
            this.connectDisconnectBehaviour.TabStop = false;
            this.connectDisconnectBehaviour.Text = "Connect/Disconnect Behaviour";
            // 
            // commandAndSettingsConfirmation
            // 
            this.commandAndSettingsConfirmation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.commandAndSettingsConfirmation.Controls.Add(this.maximumNumberOfRetries);
            this.commandAndSettingsConfirmation.Controls.Add(this.timeout);
            this.commandAndSettingsConfirmation.Controls.Add(this.maximumNumberOfRetriesLabel);
            this.commandAndSettingsConfirmation.Controls.Add(this.timeoutLabel);
            this.commandAndSettingsConfirmation.Location = new System.Drawing.Point(12, 211);
            this.commandAndSettingsConfirmation.Name = "commandAndSettingsConfirmation";
            this.commandAndSettingsConfirmation.Size = new System.Drawing.Size(457, 74);
            this.commandAndSettingsConfirmation.TabIndex = 2;
            this.commandAndSettingsConfirmation.TabStop = false;
            this.commandAndSettingsConfirmation.Text = "Command And Settings Confirmation";
            // 
            // misc
            // 
            this.misc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.misc.Controls.Add(this.label1);
            this.misc.Controls.Add(this.graphSampleBufferSize);
            this.misc.Controls.Add(this.rememberWindowLayout);
            this.misc.Controls.Add(this.displayReceivedErrorMessagesInMessageBox);
            this.misc.Location = new System.Drawing.Point(12, 368);
            this.misc.Name = "misc";
            this.misc.Size = new System.Drawing.Size(457, 94);
            this.misc.TabIndex = 4;
            this.misc.TabStop = false;
            this.misc.Text = "Misc";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Graph sample buffer (kSa)";
            // 
            // graphSampleBufferSize
            // 
            this.graphSampleBufferSize.Location = new System.Drawing.Point(10, 65);
            this.graphSampleBufferSize.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.graphSampleBufferSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.graphSampleBufferSize.Name = "graphSampleBufferSize";
            this.graphSampleBufferSize.Size = new System.Drawing.Size(51, 20);
            this.graphSampleBufferSize.TabIndex = 2;
            this.graphSampleBufferSize.Value = new decimal(new int[] {
            24,
            0,
            0,
            0});
            // 
            // OptionsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_Cancel;
            this.ClientSize = new System.Drawing.Size(481, 501);
            this.Controls.Add(this.misc);
            this.Controls.Add(this.commandAndSettingsConfirmation);
            this.Controls.Add(this.connectDisconnectBehaviour);
            this.Controls.Add(this.firmwareUploader);
            this.Controls.Add(this.m_ResetToDefaults);
            this.Controls.Add(this.m_Cancel);
            this.Controls.Add(this.m_OK);
            this.Controls.Add(this.searchForConnections);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(326, 278);
            this.Name = "OptionsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.OptionsDialog_Load);
            this.searchForConnections.ResumeLayout(false);
            this.searchForConnections.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maximumNumberOfRetries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeout)).EndInit();
            this.firmwareUploader.ResumeLayout(false);
            this.firmwareUploader.PerformLayout();
            this.connectDisconnectBehaviour.ResumeLayout(false);
            this.connectDisconnectBehaviour.PerformLayout();
            this.commandAndSettingsConfirmation.ResumeLayout(false);
            this.commandAndSettingsConfirmation.PerformLayout();
            this.misc.ResumeLayout(false);
            this.misc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.graphSampleBufferSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox rememberWindowLayout;
        private System.Windows.Forms.GroupBox searchForConnections;
        private System.Windows.Forms.CheckBox openConnectionToFirstDeviceFound;
        private System.Windows.Forms.Label searchTypeLabel;
        private System.Windows.Forms.CheckBox searchForConnectionsAtStartup;
        private System.Windows.Forms.ComboBox searchType;
        private System.Windows.Forms.Button m_OK;
        private System.Windows.Forms.Button m_Cancel;
        private System.Windows.Forms.CheckBox disconnectFromSerialAfterSleepAndResetCommands;
        private System.Windows.Forms.CheckBox displayReceivedErrorMessagesInMessageBox;
        private System.Windows.Forms.NumericUpDown maximumNumberOfRetries;
        private System.Windows.Forms.NumericUpDown timeout;
        private System.Windows.Forms.Label maximumNumberOfRetriesLabel;
        private System.Windows.Forms.Label timeoutLabel;
        private System.Windows.Forms.Button m_ResetToDefaults;
        private System.Windows.Forms.GroupBox firmwareUploader;
        private System.Windows.Forms.CheckBox searchForConnectionsAfterSuccessfulUpload;
        private System.Windows.Forms.CheckBox allowUploadWithoutSerialConnection;
        private System.Windows.Forms.CheckBox configureUniqueUdpConnection;
        private System.Windows.Forms.CheckBox readDeviceSettingsAfterOpeningConnection;
        private System.Windows.Forms.GroupBox connectDisconnectBehaviour;
        private System.Windows.Forms.GroupBox commandAndSettingsConfirmation;
        private System.Windows.Forms.GroupBox misc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown graphSampleBufferSize;
    }
}