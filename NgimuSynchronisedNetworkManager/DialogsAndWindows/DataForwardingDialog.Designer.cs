namespace NgimuSynchronisedNetworkManager.DialogsAndWindows
{
    partial class DataForwardingDialog
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
            this.m_SendIPAddress = new NgimuForms.Controls.TextBoxWithHelpText();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.connectButton = new System.Windows.Forms.Button();
            this.m_AdapterIPAddress = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_SendPort = new NgimuForms.Controls.TextBoxWithHelpText();
            this.label3 = new System.Windows.Forms.Label();
            this.localHost = new System.Windows.Forms.CheckBox();
            this.prefixName = new System.Windows.Forms.RadioButton();
            this.prefixSerialNumber = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_SendIPAddress
            // 
            this.m_SendIPAddress.Enabled = false;
            this.m_SendIPAddress.HelpText = "";
            this.m_SendIPAddress.HelpTextColor = System.Drawing.Color.Gray;
            this.m_SendIPAddress.Location = new System.Drawing.Point(153, 60);
            this.m_SendIPAddress.Name = "m_SendIPAddress";
            this.m_SendIPAddress.Size = new System.Drawing.Size(156, 20);
            this.m_SendIPAddress.TabIndex = 2;
            this.m_SendIPAddress.Text = "127.0.0.1";
            this.m_SendIPAddress.TextChanged += new System.EventHandler(this.SendIPAddress_TextChanged);
            this.m_SendIPAddress.Enter += new System.EventHandler(this.BoxWithHelper_Enter);
            this.m_SendIPAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BoxWithHelper_KeyDown);
            this.m_SendIPAddress.Leave += new System.EventHandler(this.BoxWithHelper_Leave);
            this.m_SendIPAddress.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BoxWithHelper_MouseEvent);
            this.m_SendIPAddress.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BoxWithHelper_MouseEvent);
            this.m_SendIPAddress.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BoxWithHelper_MouseEvent);
            // 
            // disconnectButton
            // 
            this.disconnectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.disconnectButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.disconnectButton.Enabled = false;
            this.disconnectButton.Location = new System.Drawing.Point(377, 93);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(75, 23);
            this.disconnectButton.TabIndex = 6;
            this.disconnectButton.Text = "Disconnect";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.Disconnect_Click);
            // 
            // connectButton
            // 
            this.connectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.connectButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.connectButton.Location = new System.Drawing.Point(295, 93);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 5;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.Connect_Click);
            // 
            // m_AdapterIPAddress
            // 
            this.m_AdapterIPAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_AdapterIPAddress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_AdapterIPAddress.FormattingEnabled = true;
            this.m_AdapterIPAddress.Location = new System.Drawing.Point(69, 33);
            this.m_AdapterIPAddress.Name = "m_AdapterIPAddress";
            this.m_AdapterIPAddress.Size = new System.Drawing.Size(383, 21);
            this.m_AdapterIPAddress.TabIndex = 0;
            this.m_AdapterIPAddress.SelectedIndexChanged += new System.EventHandler(this.AdapterIPAddress_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Send:";
            // 
            // m_SendPort
            // 
            this.m_SendPort.HelpText = "";
            this.m_SendPort.HelpTextColor = System.Drawing.Color.Gray;
            this.m_SendPort.Location = new System.Drawing.Point(69, 60);
            this.m_SendPort.Name = "m_SendPort";
            this.m_SendPort.Size = new System.Drawing.Size(78, 20);
            this.m_SendPort.TabIndex = 1;
            this.m_SendPort.TextChanged += new System.EventHandler(this.SendPort_TextChanged);
            this.m_SendPort.Enter += new System.EventHandler(this.BoxWithHelper_Enter);
            this.m_SendPort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BoxWithHelper_KeyDown);
            this.m_SendPort.Leave += new System.EventHandler(this.BoxWithHelper_Leave);
            this.m_SendPort.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BoxWithHelper_MouseEvent);
            this.m_SendPort.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BoxWithHelper_MouseEvent);
            this.m_SendPort.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BoxWithHelper_MouseEvent);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Adapter:";
            // 
            // localHost
            // 
            this.localHost.AutoSize = true;
            this.localHost.Checked = true;
            this.localHost.CheckState = System.Windows.Forms.CheckState.Checked;
            this.localHost.Location = new System.Drawing.Point(315, 62);
            this.localHost.Name = "localHost";
            this.localHost.Size = new System.Drawing.Size(77, 17);
            this.localHost.TabIndex = 3;
            this.localHost.Text = "Local Host";
            this.localHost.UseVisualStyleBackColor = true;
            this.localHost.CheckedChanged += new System.EventHandler(this.LocalHost_CheckedChanged);
            // 
            // prefixName
            // 
            this.prefixName.AutoSize = true;
            this.prefixName.Checked = true;
            this.prefixName.Location = new System.Drawing.Point(69, 7);
            this.prefixName.Name = "prefixName";
            this.prefixName.Size = new System.Drawing.Size(90, 17);
            this.prefixName.TabIndex = 10;
            this.prefixName.TabStop = true;
            this.prefixName.Text = "Device Name";
            this.prefixName.UseVisualStyleBackColor = true;
            this.prefixName.CheckedChanged += new System.EventHandler(this.prefixName_CheckedChanged);
            // 
            // prefixSerialNumber
            // 
            this.prefixSerialNumber.AutoSize = true;
            this.prefixSerialNumber.Location = new System.Drawing.Point(165, 7);
            this.prefixSerialNumber.Name = "prefixSerialNumber";
            this.prefixSerialNumber.Size = new System.Drawing.Size(91, 17);
            this.prefixSerialNumber.TabIndex = 11;
            this.prefixSerialNumber.Text = "Serial Number";
            this.prefixSerialNumber.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Prefix:";
            // 
            // DataForwardingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 128);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.prefixSerialNumber);
            this.Controls.Add(this.prefixName);
            this.Controls.Add(this.localHost);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_SendPort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_AdapterIPAddress);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.disconnectButton);
            this.Controls.Add(this.m_SendIPAddress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1280, 167);
            this.MinimumSize = new System.Drawing.Size(480, 167);
            this.Name = "DataForwardingDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Data Forwarding";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UdpConnectionDialog_FormClosing);
            this.Load += new System.EventHandler(this.UdpConnectionDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NgimuForms.Controls.TextBoxWithHelpText m_SendIPAddress;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.ComboBox m_AdapterIPAddress;
        private System.Windows.Forms.Label label1;
        private NgimuForms.Controls.TextBoxWithHelpText m_SendPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox localHost;
        private System.Windows.Forms.RadioButton prefixName;
        private System.Windows.Forms.RadioButton prefixSerialNumber;
        private System.Windows.Forms.Label label2;
    }
}