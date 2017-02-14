namespace NgimuGui.DialogsAndWindows
{
    partial class SDCardFileConverterWindow
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
            this.m_FileNameLabel = new System.Windows.Forms.Label();
            this.m_StartButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.sdCardFilePathSelector = new NgimuForms.Controls.PathSelector();
            this.directorySelector = new NgimuForms.Controls.PathSelector();
            this.SuspendLayout();
            // 
            // m_FileNameLabel
            // 
            this.m_FileNameLabel.AutoSize = true;
            this.m_FileNameLabel.Location = new System.Drawing.Point(12, 43);
            this.m_FileNameLabel.Name = "m_FileNameLabel";
            this.m_FileNameLabel.Size = new System.Drawing.Size(108, 13);
            this.m_FileNameLabel.TabIndex = 13;
            this.m_FileNameLabel.Text = "Destination Directory:";
            // 
            // m_StartButton
            // 
            this.m_StartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_StartButton.Location = new System.Drawing.Point(495, 67);
            this.m_StartButton.Name = "m_StartButton";
            this.m_StartButton.Size = new System.Drawing.Size(75, 23);
            this.m_StartButton.TabIndex = 4;
            this.m_StartButton.Text = "Convert";
            this.m_StartButton.UseVisualStyleBackColor = true;
            this.m_StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "SD Card File(s):";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // sdCardFilePathSelector
            // 
            this.sdCardFilePathSelector.AllowMultiplePaths = true;
            this.sdCardFilePathSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sdCardFilePathSelector.DialogTitle = "Select File(s) To Process";
            this.sdCardFilePathSelector.Filter = "XIO files|*.xio|All files|*.*";
            this.sdCardFilePathSelector.HelpText = "Select SD card file(s)";
            this.sdCardFilePathSelector.HelpTextColor = System.Drawing.Color.Gray;
            this.sdCardFilePathSelector.Location = new System.Drawing.Point(126, 12);
            this.sdCardFilePathSelector.Margin = new System.Windows.Forms.Padding(0);
            this.sdCardFilePathSelector.Name = "sdCardFilePathSelector";
            this.sdCardFilePathSelector.SelectedPath = "";
            this.sdCardFilePathSelector.Size = new System.Drawing.Size(444, 23);
            this.sdCardFilePathSelector.TabIndex = 17;
            // 
            // directorySelector
            // 
            this.directorySelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.directorySelector.DialogTitle = "Please select a destination directory.";
            this.directorySelector.HelpText = "Select destination directory";
            this.directorySelector.HelpTextColor = System.Drawing.Color.Gray;
            this.directorySelector.Location = new System.Drawing.Point(126, 39);
            this.directorySelector.Margin = new System.Windows.Forms.Padding(0);
            this.directorySelector.Name = "directorySelector";
            this.directorySelector.SelectedPath = "";
            this.directorySelector.SelectorType = NgimuForms.Controls.PathSelector.PathSelectorType.SelectDirectory;
            this.directorySelector.Size = new System.Drawing.Size(444, 23);
            this.directorySelector.TabIndex = 18;
            // 
            // SDCardFileConverterWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 107);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_FileNameLabel);
            this.Controls.Add(this.m_StartButton);
            this.Controls.Add(this.sdCardFilePathSelector);
            this.Controls.Add(this.directorySelector);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(1022, 146);
            this.MinimumSize = new System.Drawing.Size(368, 146);
            this.Name = "SDCardFileConverterWindow";
            this.Text = "SD Card File Converter";
            this.Load += new System.EventHandler(this.SDCardReaderDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_FileNameLabel;
        private System.Windows.Forms.Button m_StartButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private NgimuForms.Controls.PathSelector sdCardFilePathSelector;
        private NgimuForms.Controls.PathSelector directorySelector;
    }
}