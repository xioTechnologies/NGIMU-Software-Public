using Rug.Osc;

namespace NgimuApi.Data
{
    /// <summary>
    /// Magnitudes data received from the device.
    /// </summary>
    public sealed class MagnitudesData : DataBase
    {
        /// <summary>
        /// Gets the OSC address for this data object.
        /// </summary>
        public override string OscAddress { get { return "/magnitudes"; } }

        /// <summary>
        /// Gets the gyroscope magnitude in degrees per second.
        /// </summary>
        public float Gyroscope { get; private set; }

        /// <summary>
        /// Gets the accelerometer magnitude in g.
        /// </summary>
        public float Accelerometer { get; private set; }

        /// <summary>
        /// Gets the magnetometer magnitude in microteslas (uT).
        /// </summary>
        public float Magnetometer { get; private set; }

        /// <summary>
        /// Method called when an OSC message associated with this data object is received.
        /// </summary>
        /// <param name="message">Received OSC message.</param>
        /// <returns>True if the OSC message was successfully parsed.</returns>
        protected override bool OnMessageReceived(OscMessage message)
        {
            if (message.Count != 3)
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

            if (!(message[2] is float))
            {
                return false;
            }

            Gyroscope = (float)message[0];

            Accelerometer = (float)message[1];

            Magnetometer = (float)message[2];

            return true;
        }

        internal override string CsvHeader
        {
            get
            {
                return Helper.ToCsv(
                        "Time (s)",
                        "Gyroscope (deg/s)", "Accelerometer (g)", "Magnetometer (uT)"
                        );
            }
        }

        internal override string ToCsv(OscTimeTag firstTimestamp)
        {
            return Helper.ToCsv(
                    CreateTimestampString(firstTimestamp),
                    Gyroscope, Accelerometer, Magnetometer
                    );
        }
    }
}
