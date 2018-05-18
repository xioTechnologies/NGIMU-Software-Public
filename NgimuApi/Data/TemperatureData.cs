using Rug.Osc;

namespace NgimuApi.Data
{
    /// <summary>
    /// Temperature data received from the device.
    /// </summary>
    public sealed class TemperatureData : DataBase
    {
        /// <summary>
        /// Gets the OSC address for this data object.
        /// </summary>
        public override string OscAddress { get { return "/temperature"; } }

        /// <summary>
        /// Gets the temperature of the gyroscope and accelerometer in degrees Celsius.
        /// </summary>
        public float GyroscopeAndAccelerometer { get; private set; }

        /// <summary>
        /// Gets the temperature of the environmental sensor in degrees Celsius.
        /// </summary>
        public float EnvironmentalSensor { get; private set; }

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

            GyroscopeAndAccelerometer = (float)message[0];
            EnvironmentalSensor = (float)message[1];

            return true;
        }

        internal override string CsvHeader
        {
            get
            {
                return Helper.ToCsv(
                        "Time (s)",
                        "Gyroscope And Accelerometer (degC)",
                        "Environmental Sensor (degC)"
                        );
            }
        }

        internal override string ToCsv(OscTimeTag firstTimestamp)
        {
            return Helper.ToCsv(
                    CreateTimestampString(firstTimestamp),

                    GyroscopeAndAccelerometer,
                    EnvironmentalSensor
                    );
        }
    }
}
