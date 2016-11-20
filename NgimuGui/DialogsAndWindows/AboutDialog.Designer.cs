namespace NgimuGui.DialogsAndWindows
{
    partial class AboutDialog
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.softwareVersion = new System.Windows.Forms.Label();
            this.firmwareVersion = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.xioWebLink = new System.Windows.Forms.LinkLabel();
            this.xioRepoLink = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.applicationName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.rugRepoLink = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::NgimuGui.Properties.Resources.x_io_rgb_black;
            this.pictureBox1.InitialImage = global::NgimuGui.Properties.Resources.x_io_rgb_black;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 256);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(275, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Application Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(275, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Software Version: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(275, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Compatable Firmware Version: ";
            // 
            // softwareVersion
            // 
            this.softwareVersion.AutoSize = true;
            this.softwareVersion.Location = new System.Drawing.Point(433, 38);
            this.softwareVersion.Name = "softwareVersion";
            this.softwareVersion.Size = new System.Drawing.Size(28, 13);
            this.softwareVersion.TabIndex = 5;
            this.softwareVersion.Text = "v1.0";
            // 
            // firmwareVersion
            // 
            this.firmwareVersion.AutoSize = true;
            this.firmwareVersion.Location = new System.Drawing.Point(433, 51);
            this.firmwareVersion.Name = "firmwareVersion";
            this.firmwareVersion.Size = new System.Drawing.Size(28, 13);
            this.firmwareVersion.TabIndex = 6;
            this.firmwareVersion.Text = "v1.0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(275, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(251, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Latest Software and firmware versions avalible from:";
            // 
            // xioWebLink
            // 
            this.xioWebLink.AutoSize = true;
            this.xioWebLink.Location = new System.Drawing.Point(275, 90);
            this.xioWebLink.Name = "xioWebLink";
            this.xioWebLink.Size = new System.Drawing.Size(113, 13);
            this.xioWebLink.TabIndex = 8;
            this.xioWebLink.TabStop = true;
            this.xioWebLink.Tag = "http://www.x-io.co.uk/ngimu";
            this.xioWebLink.Text = "www.x-io.co.uk/ngimu";
            this.xioWebLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.xioWebLink_LinkClicked);
            // 
            // xioRepoLink
            // 
            this.xioRepoLink.AutoSize = true;
            this.xioRepoLink.Location = new System.Drawing.Point(275, 129);
            this.xioRepoLink.Name = "xioRepoLink";
            this.xioRepoLink.Size = new System.Drawing.Size(259, 13);
            this.xioRepoLink.TabIndex = 11;
            this.xioRepoLink.TabStop = true;
            this.xioRepoLink.Tag = "https://github.com/xioTechnologies/NGIMU-Software-Public";
            this.xioRepoLink.Text = "github.com/xioTechnologies/NGIMU-Software-Public";
            this.xioRepoLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.xioRepoLink_LinkClicked);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(275, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Software source code repository:";
            // 
            // applicationName
            // 
            this.applicationName.AutoSize = true;
            this.applicationName.Location = new System.Drawing.Point(433, 12);
            this.applicationName.Name = "applicationName";
            this.applicationName.Size = new System.Drawing.Size(65, 13);
            this.applicationName.TabIndex = 12;
            this.applicationName.Text = "NGIMU GUI";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(275, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(300, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "This software uses Rugland Development Group components:";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::NgimuGui.Properties.Resources.Rug_Logo_256x256__Just_Rug_;
            this.pictureBox2.InitialImage = global::NgimuGui.Properties.Resources.Rug_Logo_256x256__Just_Rug_;
            this.pictureBox2.Location = new System.Drawing.Point(411, 202);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(186, 66);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // rugRepoLink
            // 
            this.rugRepoLink.AutoSize = true;
            this.rugRepoLink.Location = new System.Drawing.Point(275, 168);
            this.rugRepoLink.Name = "rugRepoLink";
            this.rugRepoLink.Size = new System.Drawing.Size(118, 13);
            this.rugRepoLink.TabIndex = 15;
            this.rugRepoLink.TabStop = true;
            this.rugRepoLink.Tag = "http://bitbucket.org/rugcode/";
            this.rugRepoLink.Text = "bitbucket.org/rugcode/";
            this.rugRepoLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.rugRepoLink_LinkClicked);
            // 
            // AboutDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 281);
            this.Controls.Add(this.rugRepoLink);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.applicationName);
            this.Controls.Add(this.xioRepoLink);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.xioWebLink);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.firmwareVersion);
            this.Controls.Add(this.softwareVersion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AboutDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.Load += new System.EventHandler(this.AboutDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label softwareVersion;
        private System.Windows.Forms.Label firmwareVersion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel xioWebLink;
        private System.Windows.Forms.LinkLabel xioRepoLink;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label applicationName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.LinkLabel rugRepoLink;
    }
}