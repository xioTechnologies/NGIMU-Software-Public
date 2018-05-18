using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using NgimuApi;
using NgimuApi.SearchForConnections;
using NgimuForms.Controls;
using NgimuForms.DialogsAndWindows;
using NgimuSynchronisedNetworkManager.Controls;

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
        }

        private void Dialog_Load(object sender, EventArgs e)
        {
            ssidTextBox.Text = MainForm.DefaultWifiClientSSID;
            passwordTextBox.Text = MainForm.DefaultWifiClientKey;
        }

        private bool ValidateValues()
        {
            if (string.IsNullOrWhiteSpace(ssidTextBox.Text))
            {
                return false;
            }

            return true;
        }

        private void ConfigureWirelessSettingsViaUSBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProgressDialog progress = new ProgressDialog
            {
                Text = "Configuring NGIMU Wireless Settings",
                Style = ProgressBarStyle.Marquee,
                CancelButtonEnabled = true,
                ProgressMessage = "",
                Progress = 0,
                ProgressMaximum = 1,
                DialogResult = DialogResult.Cancel,
            };

            ManualResetEvent contiueResetEvent = new ManualResetEvent(false);

            bool hasBeenCanceled = false;

            progress.OnCancel += delegate (object s, FormClosingEventArgs fce)
            {
                hasBeenCanceled = true;
                contiueResetEvent.Set();
            };

            progress.FormClosed += delegate (object s, FormClosedEventArgs fce)
            {
                contiueResetEvent.Set();
            };

            Thread configureWirelessSettingsViaUSBThread = new Thread(() =>
            {
                progress.UpdateProgress(0, "Searching for USB connection.");

                bool found = false;
                ConnectionSearchResult foundConnectionSearchResult = null;

                // search for serial only connections
                using (SearchForConnections searchForConnections = new SearchForConnections(ConnectionSearchTypes.Serial))
                {
                    searchForConnections.DeviceDiscovered += delegate (ConnectionSearchResult connectionSearchResult)
                    {
                        found = true;
                        foundConnectionSearchResult = connectionSearchResult;
                        contiueResetEvent.Set();
                    };

                    contiueResetEvent.Reset();

                    searchForConnections.BeginSearch();

                    contiueResetEvent.WaitOne();

                    searchForConnections.EndSearch();
                }

                if (hasBeenCanceled == true)
                {
                    return;
                }

                if (foundConnectionSearchResult == null)
                {
                    return;
                }

                string deviceDescriptor = foundConnectionSearchResult.DeviceDescriptor;

                // open connection to first device found 
                progress.UpdateProgress(0, $"Found device {deviceDescriptor}.");

                using (Connection connection = new Connection(foundConnectionSearchResult))
                {
                    connection.Connect();

                    progress.UpdateProgress(0, $"Restoring default settings.");
                    connection.SendCommand(Command.Default);

                    connection.Settings.WifiMode.Value = WifiMode.Client;
                    connection.Settings.WifiClientSSID.Value = MainForm.DefaultWifiClientSSID;
                    connection.Settings.WifiClientKey.Value = MainForm.DefaultWifiClientKey;
                    connection.Settings.WifiReceivePort.Value = MainForm.SynchronisationMasterPort;
                    connection.Settings.SynchronisationMasterPort.Value = MainForm.SynchronisationMasterPort;
                    connection.Settings.SendRateRssi.Value = MainForm.DefaultSendRateRssi;

                    progress.UpdateProgress(0, $"Writing wireless settings.");
                    this.WriteSettingsWithExistingProgress(progress, new List<ConnectionRow>(), true, new[] { connection.Settings });
                }

                progress.DialogResult = DialogResult.OK;

                Invoke(new MethodInvoker(() => { progress.Close(); }));
            });

            configureWirelessSettingsViaUSBThread.Name = "Configure Wireless Settings Via USB";
            configureWirelessSettingsViaUSBThread.Start();

            if (progress.ShowDialog(this) == DialogResult.OK)
            {
                this.ShowInformation($@"Configuration of device complete.");
            }
        }

        private void setButton_Click(object sender, EventArgs e)
        {
            if (ValidateValues() == false)
            {
                System.Media.SystemSounds.Exclamation.Play();

                FlashingDialogHelper.FlashWindowEx(this);

                return;
            }

            MainForm.DefaultWifiClientSSID = ssidTextBox.Text;
            MainForm.DefaultWifiClientKey = passwordTextBox.Text;

            ConfigureWirelessSettingsViaUSBToolStripMenuItem_Click(this, EventArgs.Empty);
        }
    }
}