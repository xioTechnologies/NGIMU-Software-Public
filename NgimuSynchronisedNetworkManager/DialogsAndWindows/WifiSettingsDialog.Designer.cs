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
            this.cancelButton = new System.Windows.Forms.Button();
            this.setButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.passwordTextBox = new NgimuForms.Controls.TextBoxWithHelpText();
            this.ssidTextBox = new NgimuForms.Controls.TextBoxWithHelpText();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(377, 93);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // setButton
            // 
            this.setButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.setButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.setButton.Location = new System.Drawing.Point(295, 93);
            this.setButton.Name = "setButton";
            this.setButton.Size = new System.Drawing.Size(75, 23);
            this.setButton.TabIndex = 3;
            this.setButton.Text = "Set";
            this.setButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Password:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "SSID:";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.passwordTextBox.HasError = false;
            this.passwordTextBox.HelpText = "Network Password";
            this.passwordTextBox.HelpTextColor = System.Drawing.Color.LightGray;
            this.passwordTextBox.HideSelection = false;
            this.passwordTextBox.Location = new System.Drawing.Point(69, 33);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(383, 20);
            this.passwordTextBox.TabIndex = 2;
            this.passwordTextBox.TextErrorColor = System.Drawing.Color.Red;
            // 
            // ssidTextBox
            // 
            this.ssidTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ssidTextBox.HasError = false;
            this.ssidTextBox.HelpText = "Network SSID";
            this.ssidTextBox.HelpTextColor = System.Drawing.Color.LightGray;
            this.ssidTextBox.HideSelection = false;
            this.ssidTextBox.Location = new System.Drawing.Point(69, 6);
            this.ssidTextBox.Name = "ssidTextBox";
            this.ssidTextBox.Size = new System.Drawing.Size(383, 20);
            this.ssidTextBox.TabIndex = 1;
            this.ssidTextBox.TextErrorColor = System.Drawing.Color.Red;
            // 
            // WifiSettingsDialog
            // 
            this.AcceptButton = this.setButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(464, 128);
            this.Controls.Add(this.ssidTextBox);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.setButton);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1280, 167);
            this.MinimumSize = new System.Drawing.Size(480, 167);
            this.Name = "WifiSettingsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Default Wifi Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Dialog_FormClosing);
            this.Load += new System.EventHandler(this.Dialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button setButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private NgimuForms.Controls.TextBoxWithHelpText passwordTextBox;
        private NgimuForms.Controls.TextBoxWithHelpText ssidTextBox;
    }
}