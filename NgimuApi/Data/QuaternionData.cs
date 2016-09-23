using NgimuApi.Maths;
using Rug.Osc;

namespace NgimuApi.Data
{
    /// <summary>
    /// Quaternion data received from the device.
    /// </summary>
    public sealed class QuaternionData : DataBase
    {
        /// <summary>
        /// Gets the OSC address for this data object.
        /// </summary>
        public override string OscAddress { get { return "/quaternion"; } }

        /// <summary>
        /// Quaternion describing the orientation of the device relative to the Earth.
        /// </summary>
        public Quaternion Quaternion { get; private set; }

        /// <summary>
        /// Method called when an OSC message associated with this data object is received.
        /// </summary>
        /// <param name="message">Received OSC message.</param>
        /// <returns>True if the OSC message was successfully parsed.</returns>
        protected override bool OnMessageReceived(OscMessage message)
        {
            if (message.Count != 4)
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

            if (!(message[3] is float))
            {
                return false;
            }

            Quaternion = new Quaternion((float)message[0], (float)message[1], (float)message[2], (float)message[3]);

            return true;
        }

        internal override string CsvHeader
        {
            get
            {
                return Helper.ToCsv(
                        "Time (s)",
                        "W", "X", "Y", "Z"
                        );
            }
        }

        internal override string ToCsv(OscTimeTag firstTimestamp)
        {
            return Helper.ToCsv(
                    CreateTimestampString(firstTimestamp),
                    Quaternion.W, Quaternion.X, Quaternion.Y, Quaternion.Z
                    );
        }
    }
}
