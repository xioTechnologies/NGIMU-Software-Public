namespace NgimuGui.DialogsAndWindows
{
    partial class ProgressDialog
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
            this.m_ProgressBar = new System.Windows.Forms.ProgressBar();
            this.m_ProgressMessage = new System.Windows.Forms.Label();
            this.m_CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_ProgressBar
            // 
            this.m_ProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ProgressBar.Location = new System.Drawing.Point(13, 13);
            this.m_ProgressBar.Name = "m_ProgressBar";
            this.m_ProgressBar.Size = new System.Drawing.Size(440, 23);
            this.m_ProgressBar.TabIndex = 0;
            // 
            // m_ProgressMessage
            // 
            this.m_ProgressMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ProgressMessage.AutoEllipsis = true;
            this.m_ProgressMessage.Location = new System.Drawing.Point(13, 43);
            this.m_ProgressMessage.Name = "m_ProgressMessage";
            this.m_ProgressMessage.Size = new System.Drawing.Size(440, 13);
            this.m_ProgressMessage.TabIndex = 1;
            this.m_ProgressMessage.Text = "m_ProgressMessage";
            // 
            // m_CancelButton
            // 
            this.m_CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_CancelButton.Location = new System.Drawing.Point(378, 59);
            this.m_CancelButton.Name = "m_CancelButton";
            this.m_CancelButton.Size = new System.Drawing.Size(75, 23);
            this.m_CancelButton.TabIndex = 1;
            this.m_CancelButton.Text = "Cancel";
            this.m_CancelButton.UseVisualStyleBackColor = true;
            this.m_CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ProgressDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 94);
            this.Controls.Add(this.m_CancelButton);
            this.Controls.Add(this.m_ProgressMessage);
            this.Controls.Add(this.m_ProgressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Progress";
            this.Load += new System.EventHandler(this.ProgressDialog_Load);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ProgressBar m_ProgressBar;
		private System.Windows.Forms.Label m_ProgressMessage;
		private System.Windows.Forms.Button m_CancelButton;
	}
}