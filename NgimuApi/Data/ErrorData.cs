using Rug.Osc;

namespace NgimuApi.Data
{
    /// <summary>
    /// Error message received from the device.
    /// </summary>
    public class ErrorData : DataBase
    {
        /// <summary>
        /// Gets the OSC address for this data object.
        /// </summary>
        public override string OscAddress { get { return "/error"; } }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        public string Message { get; set; }

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

            if (!(message[0] is string))
            {
                return false;
            }

            Message = (string)message[0];

            return true;
        }

        internal override string CsvHeader
        {
            get
            {
                return Helper.ToCsv(
                        "Message"
                        );
            }
        }

        internal override string ToCsv(OscTimeTag firstTimestamp)
        {
            return Helper.ToCsv(
                    Message
                    );
        }
    }
}
