using Rug.Osc;

namespace NgimuApi.Data
{
    /// <summary>
    /// Altitude data received from the device.
    /// </summary>
    public sealed class AltitudeData : DataBase
    {
        /// <summary>
        /// Gets the OSC address for this data object.
        /// </summary>
        public override string OscAddress { get { return "/altitude"; } }

        /// <summary>
        /// Gets the altitude in meters.
        /// </summary>
        public float Altitude { get; private set; }

        /// <summary>
        /// Method called when an OSC message associated with this data object is received.
        /// </summary>
        /// <param name="message">Received OSC message.</param>
        /// <returns>True if the OSC message was successfully parsed.</returns>
        protected override bool OnMessageReceived(OscMessage message)
        {
            if (message.Count != 1)
            {
                return false;
            }

            if (!(message[0] is float))
            {
                return false;
            }

            Altitude = (float)message[0];

            return true;
        }

        internal override string CsvHeader
        {
            get
            {
                return Helper.ToCsv(
                        "Time (s)",
                        "Altitude (m)"
                        );
            }
        }


        internal override string ToCsv(OscTimeTag firstTimestamp)
        {
            return Helper.ToCsv(
                    CreateTimestampString(firstTimestamp),
                    Altitude
                    );
        }
    }
}


