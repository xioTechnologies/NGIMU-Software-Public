namespace NgimuGui.DialogsAndWindows
{
    partial class SerialConnectionDialog
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
            this.m_OKButton = new System.Windows.Forms.Button();
            this.m_Cancel = new System.Windows.Forms.Button();
            this.m_Port = new System.Windows.Forms.ComboBox();
            this.m_BaudRate = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_RtsCtsEnabled = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // m_OKButton
            // 
            this.m_OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_OKButton.Location = new System.Drawing.Point(296, 99);
            this.m_OKButton.Name = "m_OKButton";
            this.m_OKButton.Size = new System.Drawing.Size(75, 23);
            this.m_OKButton.TabIndex = 3;
            this.m_OKButton.Text = "OK";
            this.m_OKButton.UseVisualStyleBackColor = true;
            // 
            // m_Cancel
            // 
            this.m_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_Cancel.Location = new System.Drawing.Point(378, 99);
            this.m_Cancel.Name = "m_Cancel";
            this.m_Cancel.Size = new System.Drawing.Size(75, 23);
            this.m_Cancel.TabIndex = 4;
            this.m_Cancel.Text = "Cancel";
            this.m_Cancel.UseVisualStyleBackColor = true;
            // 
            // m_Port
            // 
            this.m_Port.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_Port.FormattingEnabled = true;
            this.m_Port.Location = new System.Drawing.Point(82, 12);
            this.m_Port.Name = "m_Port";
            this.m_Port.Size = new System.Drawing.Size(371, 21);
            this.m_Port.TabIndex = 0;
            this.m_Port.DropDown += new System.EventHandler(this.Port_DropDown);
            this.m_Port.SelectedIndexChanged += new System.EventHandler(this.Port_SelectedIndexChanged);
            this.m_Port.DropDownClosed += new System.EventHandler(this.Port_DropDownClosed);
            this.m_Port.TextChanged += new System.EventHandler(this.Port_TextChanged);
            this.m_Port.Enter += new System.EventHandler(this.Port_Enter);
            this.m_Port.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Port_KeyDown);
            this.m_Port.Leave += new System.EventHandler(this.Port_Leave);
            this.m_Port.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Port_MouseEvent);
            this.m_Port.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Port_MouseEvent);
            this.m_Port.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Port_MouseEvent);
            // 
            // m_BaudRate
            // 
            this.m_BaudRate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_BaudRate.FormattingEnabled = true;
            this.m_BaudRate.Items.AddRange(new object[] {
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200",
            "230400",
            "460800",
            "921600"});
            this.m_BaudRate.Location = new System.Drawing.Point(82, 39);
            this.m_BaudRate.Name = "m_BaudRate";
            this.m_BaudRate.Size = new System.Drawing.Size(371, 21);
            this.m_BaudRate.TabIndex = 1;
            this.m_BaudRate.DropDown += new System.EventHandler(this.BaudRate_DropDown);
            this.m_BaudRate.SelectedIndexChanged += new System.EventHandler(this.BaudRate_SelectedIndexChanged);
            this.m_BaudRate.DropDownClosed += new System.EventHandler(this.BaudRate_DropDownClosed);
            this.m_BaudRate.TextChanged += new System.EventHandler(this.BaudRate_TextChanged);
            this.m_BaudRate.Enter += new System.EventHandler(this.BaudRate_Enter);
            this.m_BaudRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BaudRate_KeyDown);
            this.m_BaudRate.Leave += new System.EventHandler(this.BaudRate_Leave);
            this.m_BaudRate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BaudRate_MouseEvent);
            this.m_BaudRate.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.BaudRate_MouseEvent);
            this.m_BaudRate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BaudRate_MouseEvent);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Serial Port:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Baud Rate:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "RTS / CTS:";
            // 
            // m_RtsCtsEnabled
            // 
            this.m_RtsCtsEnabled.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_RtsCtsEnabled.FormattingEnabled = true;
            this.m_RtsCtsEnabled.Items.AddRange(new object[] {
            "Disabled",
            "Enabled"});
            this.m_RtsCtsEnabled.Location = new System.Drawing.Point(82, 67);
            this.m_RtsCtsEnabled.Name = "m_RtsCtsEnabled";
            this.m_RtsCtsEnabled.Size = new System.Drawing.Size(71, 21);
            this.m_RtsCtsEnabled.TabIndex = 2;
            this.m_RtsCtsEnabled.SelectedIndexChanged += new System.EventHandler(this.RtsCtsEnabled_SelectedIndexChanged);
            // 
            // SerialConnectionDialog
            // 
            this.AcceptButton = this.m_OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_Cancel;
            this.ClientSize = new System.Drawing.Size(464, 132);
            this.Controls.Add(this.m_RtsCtsEnabled);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_BaudRate);
            this.Controls.Add(this.m_Port);
            this.Controls.Add(this.m_OKButton);
            this.Controls.Add(this.m_Cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(1080, 166);
            this.MinimumSize = new System.Drawing.Size(302, 166);
            this.Name = "SerialConnectionDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setup Serial Connection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SerialConnectionDialog_FormClosing);
            this.Load += new System.EventHandler(this.SerialConnectionDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button m_OKButton;
		private System.Windows.Forms.Button m_Cancel;
		private System.Windows.Forms.ComboBox m_Port;
		private System.Windows.Forms.ComboBox m_BaudRate;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox m_RtsCtsEnabled;
	}
}