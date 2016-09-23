using NgimuApi.Maths;
using Rug.Osc;

namespace NgimuApi.Data
{
    /// <summary>
    /// Sensor data received from the device.
    /// </summary>
    public sealed class SensorsData : DataBase
    {
        /// <summary>
        /// Gets the OSC address for this data object.
        /// </summary>
        public override string OscAddress { get { return "/sensors"; } }

        /// <summary>
        /// Gets the gyroscope measurement in degrees per second.
        /// </summary>
        public Vector3 Gyroscope { get; private set; }

        /// <summary>
        /// Gets the accelerometer measurement in g.
        /// </summary>
        public Vector3 Accelerometer { get; private set; }

        /// <summary>
        /// Gets the magnetometer measurement in microteslas (uT).
        /// </summary>
        public Vector3 Magnetometer { get; private set; }

        /// <summary>
        /// Gets the barometer measurement hectopascals (hPa).
        /// </summary>
        public float Barometer { get; private set; }

        /// <summary>
        /// Method called when an OSC message associated with this data object is received.
        /// </summary>
        /// <param name="message">Received OSC message.</param>
        /// <returns>True if the OSC message was successfully parsed.</returns>
        protected override bool OnMessageReceived(OscMessage message)
        {
            if (message.Count != 10)
            {
                return false;
            }

            for (int i = 0; i < 10; i++)
            {
                if (!(message[i] is float))
                {
                    return false;
                }
            }

            Gyroscope = new Vector3(
                (float)message[0],
                (float)message[1],
                (float)message[2]);

            Accelerometer = new Vector3(
                (float)message[3],
                (float)message[4],
                (float)message[5]);

            Magnetometer = new Vector3(
                (float)message[6],
                (float)message[7],
                (float)message[8]);

            Barometer = (float)message[9];

            return true;
        }

        internal override string CsvHeader
        {
            get
            {
                return Helper.ToCsv(
                        "Time (s)",
                        "Gyroscope X (deg/s)", "Gyroscope Y (deg/s)", "Gyroscope Z (deg/s)",
                        "Accelerometer X (g)", "Accelerometer Y (g)", "Accelerometer Z (g)",
                        "Magnetometer X (uT)", "Magnetometer Y (uT)", "Magnetometer Z (uT)",
                        "Barometer (hPa)"
                        );
            }
        }

        internal override string ToCsv(OscTimeTag firstTimestamp)
        {
            return Helper.ToCsv(
                    CreateTimestampString(firstTimestamp),

                    Gyroscope.X,
                    Gyroscope.Y,
                    Gyroscope.Z,

                    Accelerometer.X,
                    Accelerometer.Y,
                    Accelerometer.Z,

                    Magnetometer.X,
                    Magnetometer.Y,
                    Magnetometer.Z,

                    Barometer
                    );
        }
    }
}
