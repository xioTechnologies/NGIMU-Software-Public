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
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork ||
                        OnlyIpV4 == false && ip.Address.AddressFamily == AddressFamily.InterNetworkV6)
                    {
                        m_AdapterIPAddress.Items.Add(new IntefaceInfo() { NetworkInterface = adapter, String = adapter.Description + " (" + ip.Address + ")", InterfaceName = adapter.Description, IpAddress = ip.Address });
                    }
                }
            }

            m_AdapterIPAddress.SelectedIndex = 0;

            NetworkAdapter = null;
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

            if (Helper.IsNullOrEmpty(m_SendIPAddress.Text) == true)
            {
                return;
            }

            if (IPAddress.TryParse(m_SendIPAddress.Text, out IPAddress address) == false ||
                address.AddressFamily == AddressFamily.InterNetworkV6)
            {
                m_SendIPAddress.HasError = true; ;
            }
            else
            {
                m_SendIPAddress.HasError = false;
                SendIPAddress = address;
            }
        }

        private void SendPort_TextChanged(object sender, EventArgs e)
        {
            SendPort = -1;
            
            if (Helper.IsNullOrEmpty(m_SendPort.Text) == true)
            {
                return;
            }

            if (ushort.TryParse(m_SendPort.Text, out ushort port) == false ||
                port == 0)
            {
                m_SendPort.HasError = true;
            }
            else
            {
                m_SendPort.HasError = false;
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
            
            if (Helper.IsNullOrEmpty(m_ReceivePort.Text) == true)
            {
                return;
            }
            
            if (ushort.TryParse(m_ReceivePort.Text, out ushort port) == false ||
                port == 0)
            {
                m_ReceivePort.HasError = true;
            }
            else
            {
                m_ReceivePort.HasError = false;

                ReceivePort = (int)port;
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
            if (m_SendIPAddress.HasError == true ||
                m_SendPort.HasError == true ||
                m_ReceivePort.HasError == true)
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


