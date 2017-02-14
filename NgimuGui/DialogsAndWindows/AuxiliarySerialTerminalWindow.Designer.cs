namespace NgimuGui.DialogsAndWindows
{
    partial class AuxiliarySerialTerminalWindow
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
            this.serialTerminal1 = new NgimuGui.Panels.SerialTerminal();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.m_TotalReceived = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_ReceiveRate = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_StatusSeparator1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_TotalSent = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_SendRate = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.RtsHigh = new NgimuGui.Controls.HiLowStatusItem();
            this.RtsLow = new NgimuGui.Controls.HiLowStatusItem();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.CtsHigh = new NgimuGui.Controls.HiLowStatusItem();
            this.CtsLow = new NgimuGui.Controls.HiLowStatusItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialTerminal1
            // 
            this.serialTerminal1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serialTerminal1.Location = new System.Drawing.Point(0, 0);
            this.serialTerminal1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.serialTerminal1.Name = "serialTerminal1";
            this.serialTerminal1.Size = new System.Drawing.Size(624, 420);
            this.serialTerminal1.Statistics = null;
            this.serialTerminal1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_TotalReceived,
            this.m_ReceiveRate,
            this.m_StatusSeparator1,
            this.m_TotalSent,
            this.m_SendRate,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.RtsHigh,
            this.RtsLow,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel3,
            this.CtsHigh,
            this.CtsLow});
            this.statusStrip1.Location = new System.Drawing.Point(0, 420);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(624, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // m_TotalReceived
            // 
            this.m_TotalReceived.Name = "m_TotalReceived";
            this.m_TotalReceived.Size = new System.Drawing.Size(63, 17);
            this.m_TotalReceived.Tag = "Total RX: {0}";
            this.m_TotalReceived.Text = "Total RX: 0";
            // 
            // m_ReceiveRate
            // 
            this.m_ReceiveRate.Name = "m_ReceiveRate";
            this.m_ReceiveRate.Size = new System.Drawing.Size(59, 17);
            this.m_ReceiveRate.Tag = "RX Rate: {0}";
            this.m_ReceiveRate.Text = "RX Rate: 0";
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
            this.m_TotalSent.Size = new System.Drawing.Size(63, 17);
            this.m_TotalSent.Tag = "Total TX: {0}";
            this.m_TotalSent.Text = "Total TX: 0";
            // 
            // m_SendRate
            // 
            this.m_SendRate.Name = "m_SendRate";
            this.m_SendRate.Size = new System.Drawing.Size(59, 17);
            this.m_SendRate.Tag = "TX Rate: {0}";
            this.m_SendRate.Text = "TX Rate: 0";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Silver;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(22, 17);
            this.toolStripStatusLabel1.Text = "  |  ";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(30, 17);
            this.toolStripStatusLabel2.Text = "RTS:";
            // 
            // RtsHigh
            // 
            this.RtsHigh.Checked = false;
            this.RtsHigh.GroupName = "RTS";
            this.RtsHigh.Name = "RtsHigh";
            this.RtsHigh.Readonly = false;
            this.RtsHigh.Size = new System.Drawing.Size(52, 20);
            this.RtsHigh.Text = "High";
            this.RtsHigh.Click += new System.EventHandler(this.RtsHigh_Click);
            // 
            // RtsLow
            // 
            this.RtsLow.Checked = false;
            this.RtsLow.GroupName = "RTS";
            this.RtsLow.Name = "RtsLow";
            this.RtsLow.Readonly = false;
            this.RtsLow.Size = new System.Drawing.Size(48, 20);
            this.RtsLow.Text = "Low";
            this.RtsLow.Click += new System.EventHandler(this.RtsLow_Click);
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.ForeColor = System.Drawing.Color.Silver;
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(22, 17);
            this.toolStripStatusLabel4.Text = "  |  ";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(31, 17);
            this.toolStripStatusLabel3.Text = "CTS:";
            // 
            // CtsHigh
            // 
            this.CtsHigh.Checked = false;
            this.CtsHigh.Enabled = false;
            this.CtsHigh.GroupName = "CTS";
            this.CtsHigh.Name = "CtsHigh";
            this.CtsHigh.Readonly = true;
            this.CtsHigh.Size = new System.Drawing.Size(52, 20);
            this.CtsHigh.Text = "High";
            // 
            // CtsLow
            // 
            this.CtsLow.Checked = false;
            this.CtsLow.Enabled = false;
            this.CtsLow.GroupName = "CTS";
            this.CtsLow.Name = "CtsLow";
            this.CtsLow.Readonly = true;
            this.CtsLow.Size = new System.Drawing.Size(48, 20);
            this.CtsLow.Text = "Low";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // AuxiliarySerialTerminal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.serialTerminal1);
            this.Controls.Add(this.statusStrip1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "AuxiliarySerialTerminal";
            this.Text = "Auxiliary Serial Terminal";
            this.Load += new System.EventHandler(this.AuxiliarySerialTerminal_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panels.SerialTerminal serialTerminal1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private Controls.HiLowStatusItem CtsHigh;
        private Controls.HiLowStatusItem CtsLow;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private Controls.HiLowStatusItem RtsLow;
        private Controls.HiLowStatusItem RtsHigh;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel m_TotalReceived;
        private System.Windows.Forms.ToolStripStatusLabel m_ReceiveRate;
        private System.Windows.Forms.ToolStripStatusLabel m_StatusSeparator1;
        private System.Windows.Forms.ToolStripStatusLabel m_TotalSent;
        private System.Windows.Forms.ToolStripStatusLabel m_SendRate;
        private System.Windows.Forms.Timer timer1;
    }
}