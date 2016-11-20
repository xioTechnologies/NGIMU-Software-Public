namespace NgimuGui.DialogsAndWindows
{
    partial class FirmwareUploaderWindow
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
            this.m_UploadButton = new System.Windows.Forms.Button();
            this.m_FilePathLabel = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pathSelector = new NgimuGui.Controls.PathSelector();
            this.SuspendLayout();
            // 
            // m_UploadButton
            // 
            this.m_UploadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_UploadButton.Location = new System.Drawing.Point(498, 41);
            this.m_UploadButton.Name = "m_UploadButton";
            this.m_UploadButton.Size = new System.Drawing.Size(75, 23);
            this.m_UploadButton.TabIndex = 2;
            this.m_UploadButton.Text = "Upload";
            this.m_UploadButton.UseVisualStyleBackColor = true;
            this.m_UploadButton.Click += new System.EventHandler(this.m_UploadButton_Click);
            // 
            // m_FilePathLabel
            // 
            this.m_FilePathLabel.AutoSize = true;
            this.m_FilePathLabel.Location = new System.Drawing.Point(12, 17);
            this.m_FilePathLabel.Name = "m_FilePathLabel";
            this.m_FilePathLabel.Size = new System.Drawing.Size(48, 13);
            this.m_FilePathLabel.TabIndex = 4;
            this.m_FilePathLabel.Text = "File Path";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "HEX files|*.hex|All Files|*.*";
            this.openFileDialog1.Title = "Select a HEX file to uplaod";
            // 
            // pathSelector
            // 
            this.pathSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pathSelector.DialogTitle = "Select a HEX file to uplaod";
            this.pathSelector.Filter = "HEX files|*.hex|All Files|*.*";
            this.pathSelector.HelpText = "Select a HEX file to upload";
            this.pathSelector.HelpTextColor = System.Drawing.Color.Gray;
            this.pathSelector.Location = new System.Drawing.Point(66, 11);
            this.pathSelector.Margin = new System.Windows.Forms.Padding(0);
            this.pathSelector.Name = "pathSelector";
            this.pathSelector.SelectedPath = "";
            this.pathSelector.Size = new System.Drawing.Size(506, 23);
            this.pathSelector.TabIndex = 5;
            // 
            // FirmwareUploaderWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 73);
            this.Controls.Add(this.m_FilePathLabel);
            this.Controls.Add(this.m_UploadButton);
            this.Controls.Add(this.pathSelector);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1920, 112);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(260, 112);
            this.Name = "FirmwareUploaderWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Firmware Uploader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UploadFirmwareDialog_FormClosing);
            this.Load += new System.EventHandler(this.UploadFirmwareDialog_Load);
            this.ResizeBegin += new System.EventHandler(this.Form_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.Form_ResizeEnd);
            this.Resize += new System.EventHandler(this.Form_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button m_UploadButton;
        private System.Windows.Forms.Label m_FilePathLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Controls.PathSelector pathSelector;
    }
}