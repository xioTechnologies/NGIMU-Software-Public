using Rug.Osc;

namespace NgimuApi.Data
{
    /// <summary>
    /// RSSI data received from the device.
    /// </summary>
    public sealed class RssiData : DataBase
    {
        /// <summary>
        /// Gets the OSC address for this data object.
        /// </summary>
        public override string OscAddress { get { return "/rssi"; } }

        /// <summary>
        /// Gets the RSSI power in dBm.
        /// </summary>
        public float Power { get; private set; }

        /// <summary>
        /// Gets the RSSI in percentage.
        /// </summary>
        public float Percentage { get; private set; }

        /// <summary>
        /// Method called when an OSC message associated with this data object is received.
        /// </summary>
        /// <param name="message">Received OSC message.</param>
        /// <returns>True if the OSC message was successfully parsed.</returns>
        protected override bool OnMessageReceived(OscMessage message)
        {
            if (message.Count != 2)
            {
                return false;
            }

            if (!(message[0] is float))
            {
                return false;
            }

            if (!(message[1] is float))
            {
                return false;
            }

            Power = (float)message[0];

            Percentage = (float)message[1];

            return true;
        }

        internal override string CsvHeader
        {
            get
            {
                return Helper.ToCsv(
                        "Time (s)",
                        "Power (dBm)",
                        "Percentage (%)"
                        );
            }
        }

        internal override string ToCsv(OscTimeTag firstTimestamp)
        {
            return Helper.ToCsv(
                    CreateTimestampString(firstTimestamp),
                    Power,
                    Percentage
                    );
        }
    }
}


