using System;
using System.Drawing;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows.Forms;
using NgimuApi;
using NgimuForms.DialogsAndWindows;

namespace NgimuGui.DialogsAndWindows
{
    public partial class UdpConnectionDialog : BaseForm
    {
        private string m_AddressHelpText = "Device IP address";
        private string m_SendPortHelpText = "Send port";
        private string m_ReceivePortHelpText = "Receive port";

        private bool m_SendIPAddress_HasError;
        private bool m_SendPort_HasError;
        private bool m_ReceivePort_HasError;

        public bool OnlyIpV4 { get; set; }

        struct IntefaceInfo
        {
            public string String;

            public string InterfaceName;

            public NetworkInterface NetworkInterface;

            public IPAddress IpAddress;

            public override string ToString()
            {
                return String;
            }
        }

        public IPAddress SendIPAddress { get; set; }
        public int SendPort { get; set; }

        public string AdapterName { get; set; }

        public NetworkInterface NetworkAdapter { get; set; }
        public IPAddress AdapterIPAddress { get; set; }
        public int ReceivePort { get; set; }

        public UdpConnectionDialog()
        {
            OnlyIpV4 = true;

            InitializeComponent();
        }

        private void UdpConnectionDialog_Load(object sender, EventArgs e)
        {
            m_SendIPAddress.HelpTextColor = Color.LightGray;
            m_SendIPAddress.HelpText = m_AddressHelpText;
            m_SendPort.HelpTextColor = Color.LightGray;
            m_SendPort.HelpText = m_SendPortHelpText;
            m_ReceivePort.HelpTextColor = Color.LightGray;
            m_ReceivePort.HelpText = m_ReceivePortHelpText;

            UdpConnectionInfo defaults = new UdpConnectionInfo();

            m_AdapterIPAddress.Items.Add(new IntefaceInfo() { NetworkInterface = defaults.NetworkAdapter, String = defaults.AdapterName, InterfaceName = defaults.AdapterName, IpAddress = defaults.AdapterIPAddress });

            if (OnlyIpV4 == false)
            {
                UdpConnectionInfo defaultsIPv6 = UdpConnectionInfo.DefaultIPv6;

                m_AdapterIPAddress.Items.Add(new IntefaceInfo() { NetworkInterface = defaultsIPv6.NetworkAdapter, String = defaultsIPv6.AdapterName, InterfaceName = defaultsIPv6.AdapterName, IpAddress = defaultsIPv6.AdapterIPAddress });
            }

            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface adapter in interfaces)
            {
                var ipProps = adapter.GetIPProperties();

                foreach (var ip in ipProps.UnicastAddresses)
                {
                    if (// (adapter.OperationalStatus == OperationalStatus.Up) &&
                        ((ip.Address.AddressFamily == AddressFamily.InterNetwork) ||
                        (OnlyIpV4 == false && ip.Address.AddressFamily == AddressFamily.InterNetworkV6)))
                    {
                        m_AdapterIPAddress.Items.Add(new IntefaceInfo() { NetworkInterface = adapter, String = adapter.Description.ToString() + " (" + ip.Address.ToString() + ")", InterfaceName = adapter.Description.ToString(), IpAddress = ip.Address });
                    }
                }
            }

            m_AdapterIPAddress.SelectedIndex = 0;

            NetworkAdapter = null;

            m_SendPort.Tag = m_SendPortHelpText;
            m_SendPort.HideSelection = true;

            m_ReceivePort.Tag = m_ReceivePortHelpText;
            m_ReceivePort.HideSelection = true;

            m_SendIPAddress.Tag = m_AddressHelpText;
            m_SendIPAddress.HideSelection = true;
        }

        private void Broadcast_CheckedChanged(object sender, EventArgs e)
        {
            if (m_Broadcast.Checked == true)
            {
                m_SendIPAddress.Text = "255.255.255.255";
                m_SendIPAddress.Enabled = false;
            }
            else
            {
                m_SendIPAddress.Text = "";
                m_SendIPAddress.Enabled = true;
            }
        }

        private void SendIPAddress_TextChanged(object sender, EventArgs e)
        {
            SendIPAddress = null;
            m_SendIPAddress_HasError = false;

            if (Helper.IsNullOrEmpty(m_SendIPAddress.Text) == true)
            {
                //m_SendIPAddress.ForeColor = Color.LightGray; 

                return;
            }

            m_SendIPAddress.ForeColor = Control.DefaultForeColor;

            IPAddress address;

            if (IPAddress.TryParse(m_SendIPAddress.Text, out address) == false ||
                address.AddressFamily == AddressFamily.InterNetworkV6)
            {
                m_SendIPAddress.ForeColor = Color.Red;
                m_SendIPAddress_HasError = true;
            }
            else
            {
                SendIPAddress = address;
            }
        }

        private void SendPort_TextChanged(object sender, EventArgs e)
        {
            SendPort = -1;
            m_SendPort_HasError = false;

            //if (m_SendPort.Text == m_SendPortHelpText)
            if (Helper.IsNullOrEmpty(m_SendPort.Text) == true)
            {
                //m_SendPort.ForeColor = Color.LightGray;

                return;
            }

            m_SendPort.ForeColor = Control.DefaultForeColor;

            ushort port;

            if (ushort.TryParse(m_SendPort.Text, out port) == false ||
                port == 0)
            {
                m_SendPort.ForeColor = Color.Red;
                m_SendPort_HasError = true;
            }
            else
            {
                SendPort = (int)port;
            }
        }

        private void AdapterIPAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            AdapterIPAddress = null;
            NetworkAdapter = null;

            IntefaceInfo info = (IntefaceInfo)m_AdapterIPAddress.SelectedItem;

            AdapterIPAddress = info.IpAddress;
            AdapterName = info.InterfaceName;
            NetworkAdapter = info.NetworkInterface;
        }

        private void ReceivePort_TextChanged(object sender, EventArgs e)
        {
            ReceivePort = -1;
            m_ReceivePort_HasError = false;

            if (Helper.IsNullOrEmpty(m_ReceivePort.Text) == true)
            {
                return;
            }

            m_ReceivePort.ForeColor = Control.DefaultForeColor;

            ushort port;

            if (ushort.TryParse(m_ReceivePort.Text, out port) == false ||
                port == 0)
            {
                m_ReceivePort.ForeColor = Color.Red;
                m_ReceivePort_HasError = true;
            }
            else
            {
                ReceivePort = (int)port;
            }
        }

        private void BoxWithHelper_Leave(object sender, EventArgs e)
        {
            TextBox textbox = sender as TextBox;

            if (Helper.IsNullOrEmpty(textbox.Text) == true)
            {
                textbox.HideSelection = true;
            }
        }

        private void BoxWithHelper_Enter(object sender, EventArgs e)
        {
            TextBox textbox = sender as TextBox;

            if (Helper.IsNullOrEmpty(textbox.Text) == true)
            {
            }
        }

        private void BoxWithHelper_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox textbox = sender as TextBox;
        }

        private void BoxWithHelper_MouseEvent(object sender, MouseEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            if (Helper.IsNullOrEmpty(textbox.Text) == true)
            {
            }
        }

        private void UdpConnectionDialog_FormClosing(object sender, FormClosingEventArgs e)
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

            if (ValidateValues() == true)
            {
                return;
            }

            e.Cancel = true;

            System.Media.SystemSounds.Exclamation.Play();

            FlashingDialogHelper.FlashWindowEx(this);
        }

        private bool ValidateValues()
        {
            if (m_SendIPAddress_HasError == true ||
                m_SendPort_HasError == true ||
                m_ReceivePort_HasError == true)
            {
                return false;
            }

            if (SendIPAddress == null ||
                SendPort == -1 ||
                ReceivePort == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}


