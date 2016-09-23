using System;

namespace NgimuApi.SearchForConnections
{
    /// <summary>
    /// Connection search information. Describes a detected possible connection. 
    /// </summary>
    public sealed class ConnectionSearchInfo : IComparable<ConnectionSearchInfo>, IEquatable<ConnectionSearchInfo>
    {
        /// <summary>
        /// Gets the serial number of the detected device. 
        /// </summary>
        public string DeviceDescriptor { get; private set; }

        /// <summary>
        /// Gets the type of the connection.
        /// </summary>
        public ConnectionType ConnectionType { get; private set; }

        /// <summary>
        /// Gets the IConnectionInfo that can be used to create a connection.
        /// </summary>
        public IConnectionInfo ConnectionInfo { get; private set; }

        /// <summary>
        /// Creates a AutoConnectionInfo object. 
        /// </summary>
        /// <param name="serialNumber">The serial number of the device.</param>
        /// <param name="info">A IConnectionInfo that can be used to create a connection.</param>
        internal ConnectionSearchInfo(string serialNumber, IConnectionInfo info)
        {
            DeviceDescriptor = serialNumber;

            ConnectionInfo = info;

            if (info is UdpConnectionInfo)
            {
                ConnectionType = ConnectionType.Udp;
            }
            else if (info is SerialConnectionInfo)
            {
                ConnectionType = ConnectionType.Serial;
            }
            else
            {
                throw new Exception(Strings.Exception_UnknownConnectionType);
            }
        }

        public int CompareTo(ConnectionSearchInfo other)
        {
            return DeviceDescriptor.CompareTo(other.DeviceDescriptor);
        }

        public bool Equals(ConnectionSearchInfo other)
        {
            return DeviceDescriptor.Equals(other.DeviceDescriptor) && ConnectionInfo.ToIDString().Equals(other.ConnectionInfo.ToIDString());
        }
    }
}
