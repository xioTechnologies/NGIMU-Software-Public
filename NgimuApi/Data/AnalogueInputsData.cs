using Rug.Osc;

namespace NgimuApi.Data
{
    /// <summary>
    /// Analogue input data received from the device.
    /// </summary>
    public sealed class AnalogueInputsData : DataBase
    {
        /// <summary>
        /// Gets the OSC address for this data object.
        /// </summary>
        public override string OscAddress { get { return "/analogue"; } }

        /// <summary>
        /// Gets the analogue input measurements as an array in volts (V).
        /// </summary>
        public float[] Array { get; private set; }

        /// <summary>
        /// Gets the analogue input channel 1 measurement in volts (V).
        /// </summary>
        public float Channel1 { get { return Array[0]; } }

        /// <summary>
        /// Gets the analogue input channel 2 measurement in volts (V).
        /// </summary>
        public float Channel2 { get { return Array[1]; } }

        /// <summary>
        /// Gets the analogue input channel 3 measurement in volts (V).
        /// </summary>
        public float Channel3 { get { return Array[2]; } }

        /// <summary>
        /// Gets the analogue input channel 4 measurement in volts (V).
        /// </summary>
        public float Channel4 { get { return Array[3]; } }

        /// <summary>
        /// Gets the analogue input channel 5 measurement in volts (V).
        /// </summary>
        public float Channel5 { get { return Array[4]; } }

        /// <summary>
        /// Gets the analogue input channel 6 measurement in volts (V).
        /// </summary>
        public float Channel6 { get { return Array[5]; } }

        /// <summary>
        /// Gets the analogue input channel 7 measurement in volts (V).
        /// </summary>
        public float Channel7 { get { return Array[6]; } }

        /// <summary>
        /// Gets the analogue input channel 8 measurement in volts (V).
        /// </summary>
        public float Channel8 { get { return Array[7]; } }

        /// <summary>
        /// Class constructor necessary to initialise the array member of this class.
        /// </summary>
        public AnalogueInputsData()
        {
            Array = new float[8];
        }

        /// <summary>
        /// Method called when an OSC message associated with this data object is received.
        /// </summary>
        /// <param name="message">Received OSC message.</param>
        /// <returns>True if the OSC message was successfully parsed.</returns>
        protected override bool OnMessageReceived(OscMessage message)
        {
            if (message.Count != 8)
            {
                return false;
            }

            for (int i = 0; i < 8; i++)
            {
                if (!(message[i] is float))
                {
                    return false;
                }

                Array[i] = (float)message[i];
            }

            return true;
        }

        internal override string CsvHeader
        {
            get
            {
                return Helper.ToCsv(
                        "Time (s)",
                        "Channel 1 (V)",
                        "Channel 2 (V)",
                        "Channel 3 (V)",
                        "Channel 4 (V)",
                        "Channel 5 (V)",
                        "Channel 6 (V)",
                        "Channel 7 (V)",
                        "Channel 8 (V)"
                        );
            }
        }

        internal override string ToCsv(OscTimeTag firstTimestamp)
        {
            return Helper.ToCsv(
                    CreateTimestampString(firstTimestamp),
                    Channel1,
                    Channel2,
                    Channel3,
                    Channel4,
                    Channel5,
                    Channel6,
                    Channel7,
                    Channel8
                    );
        }
    }
}
