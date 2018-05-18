namespace NgimuSynchronisedNetworkManager.DialogsAndWindows
{
    partial class WifiSettingsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WifiSettingsDialog));
            this.setButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.passwordTextBox = new NgimuForms.Controls.TextBoxWithHelpText();
            this.ssidTextBox = new NgimuForms.Controls.TextBoxWithHelpText();
            this.instructionsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // setButton
            // 
            this.setButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.setButton.Location = new System.Drawing.Point(377, 240);
            this.setButton.Name = "setButton";
            this.setButton.Size = new System.Drawing.Size(75, 23);
            this.setButton.TabIndex = 3;
            this.setButton.Text = "Configure";
            this.setButton.UseVisualStyleBackColor = true;
            this.setButton.Click += new System.EventHandler(this.setButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Network Key (Password):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Wi-Fi Network Name (SSID):";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.passwordTextBox.HasError = false;
            this.passwordTextBox.HelpText = "xiotechnologies";
            this.passwordTextBox.HelpTextColor = System.Drawing.Color.LightGray;
            this.passwordTextBox.HideSelection = false;
            this.passwordTextBox.Location = new System.Drawing.Point(160, 190);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(292, 20);
            this.passwordTextBox.TabIndex = 2;
            this.passwordTextBox.TextErrorColor = System.Drawing.Color.Red;
            // 
            // ssidTextBox
            // 
            this.ssidTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ssidTextBox.HasError = false;
            this.ssidTextBox.HelpText = "NGIMU Network";
            this.ssidTextBox.HelpTextColor = System.Drawing.Color.LightGray;
            this.ssidTextBox.HideSelection = false;
            this.ssidTextBox.Location = new System.Drawing.Point(160, 163);
            this.ssidTextBox.Name = "ssidTextBox";
            this.ssidTextBox.Size = new System.Drawing.Size(292, 20);
            this.ssidTextBox.TabIndex = 1;
            this.ssidTextBox.TextErrorColor = System.Drawing.Color.Red;
            // 
            // instructionsLabel
            // 
            this.instructionsLabel.Location = new System.Drawing.Point(13, 13);
            this.instructionsLabel.Name = "instructionsLabel";
            this.instructionsLabel.Size = new System.Drawing.Size(439, 127);
            this.instructionsLabel.TabIndex = 13;
            this.instructionsLabel.Text = resources.GetString("instructionsLabel.Text");
            // 
            // WifiSettingsDialog
            // 
            this.AcceptButton = this.setButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 275);
            this.Controls.Add(this.instructionsLabel);
            this.Controls.Add(this.ssidTextBox);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.setButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1280, 2000);
            this.MinimumSize = new System.Drawing.Size(480, 167);
            this.Name = "WifiSettingsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configure Wireless Settings Via USB";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Dialog_FormClosing);
            this.Load += new System.EventHandler(this.Dialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button setButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private NgimuForms.Controls.TextBoxWithHelpText passwordTextBox;
        private NgimuForms.Controls.TextBoxWithHelpText ssidTextBox;
        private System.Windows.Forms.Label instructionsLabel;
    }
}