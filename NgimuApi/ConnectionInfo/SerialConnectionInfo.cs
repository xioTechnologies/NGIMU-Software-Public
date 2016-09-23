using System;

namespace NgimuApi
{
    /// <summary>
    /// Connection info that describes a connection via a serial port.
    /// </summary>
    public sealed class SerialConnectionInfo : IEquatable<SerialConnectionInfo>, NgimuApi.IConnectionInfo
    {
        /// <summary>
        /// Gets or sets the name of the serial port. 
        /// </summary>
        public string PortName { get; set; }

        /// <summary>
        /// Gets or sets the baud rate.
        /// </summary>
        public uint BaudRate { get; set; }

        /// <summary>
        /// Gets or sets the RTS/CTS enabled flag.
        /// </summary>
        public bool RtsCtsEnabled { get; set; }

        /// <summary>
        /// Creates an instance using the default values.
        /// </summary>
        public SerialConnectionInfo()
        {
            PortName = "";
            BaudRate = 115200;
            RtsCtsEnabled = false;
        }

        public override string ToString()
        {
            return PortName.ToString() + ", " + BaudRate + (RtsCtsEnabled ? ", RTC/CTS Enabled" : "");
        }

        /// <summary>
        /// The ID to use as a unique key.
        /// </summary>
        /// <returns>A unique key string.</returns>
        public string ToIDString()
        {
            return ToString();
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
            else if (obj is SerialConnectionInfo)
            {
                return Equals(obj as SerialConnectionInfo);
            }

            return base.Equals(obj);
        }

        public bool Equals(SerialConnectionInfo other)
        {
            if (other == null)
            {
                return false;
            }

            return
                this.PortName == other.PortName &&
                this.BaudRate == other.BaudRate &&
                this.RtsCtsEnabled == other.RtsCtsEnabled;
        }
    }
}

