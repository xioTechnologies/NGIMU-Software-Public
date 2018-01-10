using System;
using System.Windows.Forms;
using NgimuForms.DialogsAndWindows;

namespace NgimuSynchronisedNetworkManager.DialogsAndWindows
{
    public partial class WifiSettingsDialog : BaseForm
    {
        public WifiSettingsDialog()
        {
            InitializeComponent();
        }

        private void Dialog_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (e.CloseReason != CloseReason.UserClosing &&
                e.CloseReason != CloseReason.None)
            {
                return;
            }

            if (this.DialogResult != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            if (ValidateValues() == false)
            {
                e.Cancel = true;

                System.Media.SystemSounds.Exclamation.Play();

                FlashingDialogHelper.FlashWindowEx(this);

                return;
            }

            MainForm.DefaultWifiClientSSID = ssidTextBox.Text;
            MainForm.DefaultWifiClientPassword = passwordTextBox.Text;
        }

        private void Dialog_Load(object sender, EventArgs e)
        {
            ssidTextBox.Text = MainForm.DefaultWifiClientSSID;
            passwordTextBox.Text = MainForm.DefaultWifiClientPassword;
        }

        private bool ValidateValues()
        {
            if (string.IsNullOrWhiteSpace(ssidTextBox.Text))
            {
                return false;
            }

            return true;
        }
    }
}