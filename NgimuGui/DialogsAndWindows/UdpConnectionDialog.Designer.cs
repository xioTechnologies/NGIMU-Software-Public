namespace NgimuGui.DialogsAndWindows
{
    partial class UdpConnectionDialog
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
            this.m_Cancel = new System.Windows.Forms.Button();
            this.m_OKButton = new System.Windows.Forms.Button();
            this.m_AdapterIPAddress = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_SendPort = new NgimuForms.Controls.TextBoxWithHelpText();
            this.m_ReceivePort = new NgimuForms.Controls.TextBoxWithHelpText();
            this.label3 = new System.Windows.Forms.Label();
            this.m_Broadcast = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();  
            // 
            // m_SendIPAddress
            // 
            this.m_SendIPAddress.HelpText = "Device IP address";
            this.m_SendIPAddress.HelpTextColor = System.Drawing.Color.Gray;
            this.m_SendIPAddress.Location = new System.Drawing.Point(153, 32);
            this.m_SendIPAddress.Name = "m_SendIPAddress";
            this.m_SendIPAddress.Size = new System.Drawing.Size(156, 20);
            this.m_SendIPAddress.TabIndex = 2;
            this.m_SendIPAddress.TextChanged += new System.EventHandler(this.SendIPAddress_TextChanged);
            // 
            // m_Cancel
            // 
            this.m_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_Cancel.Location = new System.Drawing.Point(378, 88);
            this.m_Cancel.Name = "m_Cancel";
            this.m_Cancel.Size = new System.Drawing.Size(75, 23);
            this.m_Cancel.TabIndex = 6;
            this.m_Cancel.Text = "Cancel";
            this.m_Cancel.UseVisualStyleBackColor = true;
            // 
            // m_OKButton
            // 
            this.m_OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_OKButton.Location = new System.Drawing.Point(296, 88);
            this.m_OKButton.Name = "m_OKButton";
            this.m_OKButton.Size = new System.Drawing.Size(75, 23);
            this.m_OKButton.TabIndex = 5;
            this.m_OKButton.Text = "OK";
            this.m_OKButton.UseVisualStyleBackColor = true;
            // 
            // m_AdapterIPAddress
            // 
            this.m_AdapterIPAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_AdapterIPAddress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_AdapterIPAddress.FormattingEnabled = true;
            this.m_AdapterIPAddress.Location = new System.Drawing.Point(69, 6);
            this.m_AdapterIPAddress.Name = "m_AdapterIPAddress";
            this.m_AdapterIPAddress.Size = new System.Drawing.Size(383, 21);
            this.m_AdapterIPAddress.TabIndex = 0;
            this.m_AdapterIPAddress.SelectedIndexChanged += new System.EventHandler(this.AdapterIPAddress_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Send:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Receive:";
            // 
            // m_SendPort
            // 
            this.m_SendPort.HelpText = "Send port";
            this.m_SendPort.HelpTextColor = System.Drawing.Color.Gray;
            this.m_SendPort.Location = new System.Drawing.Point(69, 32);
            this.m_SendPort.Name = "m_SendPort";
            this.m_SendPort.Size = new System.Drawing.Size(78, 20);
            this.m_SendPort.TabIndex = 1;
            this.m_SendPort.TextChanged += new System.EventHandler(this.SendPort_TextChanged);
            // 
            // m_ReceivePort
            // 
            this.m_ReceivePort.HelpText = "Receive port";
            this.m_ReceivePort.HelpTextColor = System.Drawing.Color.Gray;
            this.m_ReceivePort.Location = new System.Drawing.Point(69, 58);
            this.m_ReceivePort.Name = "m_ReceivePort";
            this.m_ReceivePort.Size = new System.Drawing.Size(78, 20);
            this.m_ReceivePort.TabIndex = 4;
            this.m_ReceivePort.TextChanged += new System.EventHandler(this.ReceivePort_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Adapter:";
            // 
            // m_Broadcast
            // 
            this.m_Broadcast.AutoSize = true;
            this.m_Broadcast.Location = new System.Drawing.Point(315, 34);
            this.m_Broadcast.Name = "m_Broadcast";
            this.m_Broadcast.Size = new System.Drawing.Size(74, 17);
            this.m_Broadcast.TabIndex = 3;
            this.m_Broadcast.Text = "Broadcast";
            this.m_Broadcast.UseVisualStyleBackColor = true;
            this.m_Broadcast.CheckedChanged += new System.EventHandler(this.Broadcast_CheckedChanged);
            // 
            // UdpConnectionDialog
            // 
            this.AcceptButton = this.m_OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_Cancel;
            this.ClientSize = new System.Drawing.Size(464, 120);
            this.Controls.Add(this.m_Broadcast);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_ReceivePort);
            this.Controls.Add(this.m_SendPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_AdapterIPAddress);
            this.Controls.Add(this.m_OKButton);
            this.Controls.Add(this.m_Cancel);
            this.Controls.Add(this.m_SendIPAddress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1280, 154);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(396, 154);
            this.Name = "UdpConnectionDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setup UDP Connection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UdpConnectionDialog_FormClosing);
            this.Load += new System.EventHandler(this.UdpConnectionDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NgimuForms.Controls.TextBoxWithHelpText m_SendIPAddress;
        private System.Windows.Forms.Button m_Cancel;
        private System.Windows.Forms.Button m_OKButton;
        private System.Windows.Forms.ComboBox m_AdapterIPAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private NgimuForms.Controls.TextBoxWithHelpText m_SendPort;
        private NgimuForms.Controls.TextBoxWithHelpText m_ReceivePort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox m_Broadcast;
    }
}