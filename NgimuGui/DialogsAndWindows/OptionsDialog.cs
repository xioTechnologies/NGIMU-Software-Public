using System;
using System.Windows.Forms;
using NgimuApi.SearchForConnections;
using NgimuForms;
using NgimuForms.Controls;
using NgimuForms.DialogsAndWindows;

namespace NgimuGui.DialogsAndWindows
{
    public partial class OptionsDialog : BaseForm
    {
        public OptionsDialog()
        {
            InitializeComponent();
        }

        private void OptionsDialog_Load(object sender, EventArgs e)
        {
            searchType.Items.Add(ConnectionSearchTypes.All);
            searchType.Items.Add(ConnectionSearchTypes.Udp);
            searchType.Items.Add(ConnectionSearchTypes.Serial);

            searchForConnectionsAtStartup.Checked = Options.SearchForConnectionsAtStartup;
            openConnectionToFirstDeviceFound.Checked = Options.OpenConnectionToFirstDeviceFound;
            configureUniqueUdpConnection.Checked = Options.ConfigureUniqueUdpConnection;
            rememberWindowLayout.Checked = Options.RememberWindowLayout;
            disconnectFromSerialAfterSleepAndResetCommands.Checked = Options.DisconnectFromSerialAfterSleepAndResetCommands;
            displayReceivedErrorMessagesInMessageBox.Checked = Options.DisplayReceivedErrorMessagesInMessageBox;
            readDeviceSettingsAfterOpeningConnection.Checked = Options.ReadDeviceSettingsAfterOpeningConnection;

            allowUploadWithoutSerialConnection.Checked = Options.AllowUploadWithoutSerialConnection;
            searchForConnectionsAfterSuccessfulUpload.Checked = Options.SearchForConnectionsAfterSuccessfulUpload;

            maximumNumberOfRetries.Value = Options.MaximumNumberOfRetries;
            timeout.Value = Options.Timeout;

            graphSampleBufferSize.Value = Options.GraphSampleBufferSize;

            searchType.SelectedItem = Options.ConnectionSearchType;
        }

        private void m_OK_Click(object sender, EventArgs e)
        {
            Options.SearchForConnectionsAtStartup = searchForConnectionsAtStartup.Checked;
            Options.OpenConnectionToFirstDeviceFound = openConnectionToFirstDeviceFound.Checked;
            Options.ConfigureUniqueUdpConnection = configureUniqueUdpConnection.Checked;
            Options.ReadDeviceSettingsAfterOpeningConnection = readDeviceSettingsAfterOpeningConnection.Checked;
            Options.RememberWindowLayout = rememberWindowLayout.Checked;

            Options.DisconnectFromSerialAfterSleepAndResetCommands = disconnectFromSerialAfterSleepAndResetCommands.Checked;
            Options.DisplayReceivedErrorMessagesInMessageBox = displayReceivedErrorMessagesInMessageBox.Checked;

            Options.AllowUploadWithoutSerialConnection = allowUploadWithoutSerialConnection.Checked;
            Options.SearchForConnectionsAfterSuccessfulUpload = searchForConnectionsAfterSuccessfulUpload.Checked;

            Options.MaximumNumberOfRetries = (int)maximumNumberOfRetries.Value;
            Options.Timeout = (int)timeout.Value;

            Options.GraphSampleBufferSize = (uint)graphSampleBufferSize.Value;

            Options.ConnectionSearchType = (ConnectionSearchTypes)searchType.SelectedItem;
        }

        private void m_ResetToDefaults_Click(object sender, EventArgs e)
        {
            if (this.ShowWarning("This will reset all options to default values.", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            //Options.SetDefaults();
            //Close();

            searchType.SelectedIndex = 0;
            searchForConnectionsAtStartup.Checked = true;
            openConnectionToFirstDeviceFound.Checked = false;
            configureUniqueUdpConnection.Checked = true;
            readDeviceSettingsAfterOpeningConnection.Checked = true;
            rememberWindowLayout.Checked = true;
            disconnectFromSerialAfterSleepAndResetCommands.Checked = true;
            displayReceivedErrorMessagesInMessageBox.Checked = false;
            allowUploadWithoutSerialConnection.Checked = false;
            searchForConnectionsAfterSuccessfulUpload.Checked = true;
            maximumNumberOfRetries.Value = 3;
            timeout.Value = 100;
            graphSampleBufferSize.Value = 24;

            WindowManager.Clear();
        }

        private void m_Cancel_Click(object sender, EventArgs e)
        {

        }

    }
}
