
namespace NgimuSynchronisedNetworkManager.DialogsAndWindows
{
    partial class TimestampWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimestampWindow));
            this.m_ClockPanel = new NgimuForms.Panels.ClockPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // m_ClockPanel
            // 
            this.m_ClockPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ClockPanel.BackColor = System.Drawing.Color.Black;
            this.m_ClockPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_ClockPanel.CurrentTime = System.TimeSpan.Parse("00:00:00");
            this.m_ClockPanel.Location = new System.Drawing.Point(13, 13);
            this.m_ClockPanel.Margin = new System.Windows.Forms.Padding(4);
            this.m_ClockPanel.Name = "m_ClockPanel";
            this.m_ClockPanel.Size = new System.Drawing.Size(560, 114);
            this.m_ClockPanel.TabIndex = 1;
            this.m_ClockPanel.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // TimestampWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 140);
            this.Controls.Add(this.m_ClockPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(290, 179);
            this.Name = "TimestampWindow";
            this.Text = "Timestamp";
            this.Load += new System.EventHandler(this.TimestampWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private NgimuForms.Panels.ClockPanel m_ClockPanel;
        private System.Windows.Forms.Timer timer1;
    }
}