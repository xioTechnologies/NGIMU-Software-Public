using NgimuApi.Maths;
using Rug.Osc;

namespace NgimuApi.Data
{
    /// <summary>
    /// Rotation matrix data received from the device.
    /// </summary>
    public sealed class RotationMatrixData : DataBase
    {
        /// <summary>
        /// Gets the OSC address for this data object.
        /// </summary>
        public override string OscAddress { get { return "/matrix"; } }

        /// <summary>
        /// Rotation matrix describing the orientation of the device relative to the Earth.
        /// </summary>
        public RotationMatrix RotationMatrix { get; private set; }

        /// <summary>
        /// Method called when an OSC message associated with this data object is received.
        /// </summary>
        /// <param name="message">Received OSC message.</param>
        /// <returns>True if the OSC message was successfully parsed.</returns>
        protected override bool OnMessageReceived(OscMessage message)
        {
            if (message.Count != 9)
            {
                return false;
            }

            for (int i = 0; i < 9; i++)
            {
                if (!(message[i] is float))
                {
                    return false;
                }
            }

            RotationMatrix = new RotationMatrix(
                (float)message[0], (float)message[1], (float)message[2],
                (float)message[3], (float)message[4], (float)message[5],
                (float)message[6], (float)message[7], (float)message[8]);

            return true;
        }

        internal override string CsvHeader
        {
            get
            {
                return Helper.ToCsv(
                        "Time (s)",
                        "XX", "XY", "XZ",
                        "YX", "YY", "YZ",
                        "ZX", "ZY", "ZZ"
                        );
            }
        }

        internal override string ToCsv(OscTimeTag firstTimestamp)
        {
            return Helper.ToCsv(
                    CreateTimestampString(firstTimestamp),
                    RotationMatrix.XX, RotationMatrix.XY, RotationMatrix.XZ,
                    RotationMatrix.YX, RotationMatrix.YY, RotationMatrix.YZ,
                    RotationMatrix.ZX, RotationMatrix.ZY, RotationMatrix.ZZ
                    );
        }
    }
}
