using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace NgimuApi
{
    /// <summary>
    /// Connection info that describes a connection via UDP.
    /// </summary>
    public sealed class UdpConnectionInfo : IConnectionInfo, IEquatable<UdpConnectionInfo>
    {
        public static UdpConnectionInfo DefaultIPv6
        {
            get
            {
                return new UdpConnectionInfo()
                {
                    AdapterName = "All Adapters IPv6",
                    SendIPAddress = IPAddress.IPv6Any,
                };
            }
        }

        /// <summary>
        /// Gets or sets the IP address of the device.
        /// </summary>
        public IPAddress SendIPAddress { get; set; }

        /// <summary>
        /// Gets or sets the port of the device.
        /// </summary>
        public int SendPort { get; set; }

        /// <summary>
        /// Gets or sets the IP address of the local network adapter.
        /// </summary>
        public IPAddress AdapterIPAddress { get; set; }

        /// <summary>
        /// Gets or sets the local port number that we will receive on.
        /// </summary>
        public int ReceivePort { get; set; }

        /// <summary>
        /// Gets or sets the name of the local network adapter.
        /// </summary>
        public string AdapterName { get; set; }

        /// <summary>
        /// Gets or sets the network adapter interface instance. 
        /// </summary>
        public NetworkInterface NetworkAdapter { get; set; }

        /// <summary>
        /// Is true if a change in the local adapters state has been detected.
        /// </summary>
        public bool HasAdapterChanged { get; internal set; }

        /// <summary>
        /// Creates an instance using the default values.
        /// </summary>
        public UdpConnectionInfo()
        {
            SendIPAddress = IPAddress.Broadcast;
            SendPort = 9000;
            AdapterIPAddress = IPAddress.Any;
            ReceivePort = 8000;
            AdapterName = "All Adapters";
            NetworkAdapter = null;
            HasAdapterChanged = false;
        }

        /// <summary>
        /// Check the adapters to see if the adapter we are using has changed IP address. 
        /// </summary>
        /// <param name="interfaces">An array of interfaces to check against.</param>
        public void RefreshAdapterAddress(NetworkInterface[] interfaces)
        {
            if (NetworkAdapter != null)
            {
                foreach (NetworkInterface @interface in interfaces)
                {
                    if (@interface.Description == NetworkAdapter.Description)
                    {
                        NetworkAdapter = @interface;
                        break;
                    }
                }

                var ipProps = NetworkAdapter.GetIPProperties();

                foreach (var ip in ipProps.UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        if (AdapterIPAddress.Equals(ip.Address) == false)
                        {
                            HasAdapterChanged = true;
                        }

                        AdapterIPAddress = ip.Address;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Get a regional string format for this connection (Don't use this string as a key). 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format(Strings.UdpConnectionInfo_StringFormat,
                SendIPAddress.ToString(), SendPort,
                AdapterIPAddress.ToString(), ReceivePort,
                AdapterName
                );
        }

        /// <summary>
        /// The ID to use as a unique key.
        /// </summary>
        /// <returns>A unique key string.</returns>
        public string ToIDString()
        {
            return SendIPAddress.ToString() + ", " + SendPort + "; " + ReceivePort + "; " + AdapterName;
        }

        public override int GetHashCode()
        {
            return ToIDString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is string)
            {
                return obj as string == ToIDString();
            }
            else if (obj is UdpConnectionInfo)
            {
                return Equals(obj as UdpConnectionInfo);
            }

            return base.Equals(obj);
        }

        public bool Equals(UdpConnectionInfo other)
        {
            return
                this.SendIPAddress.Equals(other.SendIPAddress) &&
                this.SendPort.Equals(other.SendPort) &&
                this.AdapterIPAddress.Equals(other.AdapterIPAddress) &&
                this.ReceivePort.Equals(other.ReceivePort);
        }

        public static UdpConnectionInfo CreateUniqueConnectionInfo(UdpConnectionInfo connectionInfo)
        {
            ushort port = (ushort)(8000 + connectionInfo.SendIPAddress.GetAddressBytes()[3]);

            UdpConnectionInfo newInfo = new UdpConnectionInfo()
            {
                AdapterName = connectionInfo.AdapterName,
                AdapterIPAddress = connectionInfo.AdapterIPAddress,
                ReceivePort = port,
                SendIPAddress = connectionInfo.SendIPAddress,
                SendPort = connectionInfo.SendPort,
            };

            return newInfo;
        }
    }
}
