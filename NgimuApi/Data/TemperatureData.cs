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
        /// Gets the processor temperature in degrees Celsius.
        /// </summary>
        public float ProcessorTemperature { get; private set; }

        /// <summary>
        /// Gets the accelerometer temperature in degrees Celsius.
        /// </summary>
        public float SensorTemperature { get; private set; }

        /// <summary>
        /// Gets the barometer temperature in degrees Celsius.
        /// </summary>
        public float BarometerTemperature { get; private set; }

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

            ProcessorTemperature = (float)message[0];
            SensorTemperature = (float)message[1];
            BarometerTemperature = (float)message[2];

            return true;
        }

        internal override string CsvHeader
        {
            get
            {
                return Helper.ToCsv(
                        "Time (s)",
                        "Processor Temperature (degC)",
                        "Sensor Temperature (degC)",
                        "Barometer Temperature (degC)"
                        );
            }
        }

        internal override string ToCsv(OscTimeTag firstTimestamp)
        {
            return Helper.ToCsv(
                    CreateTimestampString(firstTimestamp),

                    ProcessorTemperature,
                    SensorTemperature,
                    BarometerTemperature
                    );
        }
    }
}
