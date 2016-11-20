using System;
using System.Reflection;
using System.Windows.Forms;
using NgimuApi;

namespace NgimuGui.DialogsAndWindows
{
    public partial class AboutDialog : BaseForm
    {
        public AboutDialog()
        {
            InitializeComponent();
        }

        private void xioWebLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(xioWebLink.Tag.ToString());
        }

        private void xioRepoLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(xioRepoLink.Tag.ToString());
        }

        private void rugRepoLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(rugRepoLink.Tag.ToString());
        }

        private void AboutDialog_Load(object sender, EventArgs e)
        {
            AssemblyName application = Assembly.GetExecutingAssembly().GetName();
            applicationName.Text = application.Name;

            AssemblyName api = typeof(Connection).Assembly.GetName();
            softwareVersion.Text = "v" + api.Version.Major + "." + api.Version.Minor;

            firmwareVersion.Text = Settings.ExpectedFirmwareVersion;
        }
    }
}
