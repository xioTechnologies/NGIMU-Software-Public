using NgimuApi.Maths;
using Rug.Osc;

namespace NgimuApi.Data
{
    /// <summary>
    /// Euler angle data received from the device.
    /// </summary>
    public sealed class EulerAnglesData : DataBase
    {
        /// <summary>
        /// Gets the OSC address for this data object.
        /// </summary>
        public override string OscAddress { get { return "/euler"; } }

        /// <summary>
        /// Euler angles describing the orientation of the device relative to the Earth.
        /// </summary>
        public EulerAngles EulerAngles { get; private set; }

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

            EulerAngles = new EulerAngles((float)message[0], (float)message[1], (float)message[2]);

            return true;
        }

        internal override string CsvHeader
        {
            get
            {
                return Helper.ToCsv(
                        "Time (s)",
                        "Roll (deg)", "Pitch (deg)", "Yaw (deg)"
                        );
            }
        }

        internal override string ToCsv(OscTimeTag firstTimestamp)
        {
            return Helper.ToCsv(
                    CreateTimestampString(firstTimestamp),
                    EulerAngles.Roll, EulerAngles.Pitch, EulerAngles.Yaw
                    );
        }
    }
}
