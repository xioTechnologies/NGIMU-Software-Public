using Rug.Osc;

namespace NgimuApi.Data
{
    /// <summary>
    /// Humidity data received from the device.
    /// </summary>
    public sealed class HumidityData : DataBase
    {
        /// <summary>
        /// Gets the OSC address for this data object.
        /// </summary>
        public override string OscAddress { get { return "/humidity"; } }

        /// <summary>
        /// Gets the humidity in %.
        /// </summary>
        public float Humidity { get; private set; }

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

            Humidity = (float)message[0];

            return true;
        }

        internal override string CsvHeader
        {
            get
            {
                return Helper.ToCsv(
                        "Time (s)",
                        "Humidity (%)"
                        );
            }
        }


        internal override string ToCsv(OscTimeTag firstTimestamp)
        {
            return Helper.ToCsv(
                    CreateTimestampString(firstTimestamp),
                    Humidity
                    );
        }
    }
}


