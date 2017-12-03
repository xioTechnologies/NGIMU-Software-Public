using Rug.Osc;

namespace NgimuApi.Data
{
    public abstract class CtsDataBase : DataBase
    {
        /// <summary>
        /// Gets the serial CTS input state.
        /// </summary>
        public bool State { get; set; }

        internal override string CsvHeader
        {
            get
            {
                return Helper.ToCsv(
                        "Time (s)",
                        "State (Boolean)");
            }
        }

        internal override string ToCsv(OscTimeTag firstTimestamp)
        {
            return Helper.ToCsv(
                    CreateTimestampString(firstTimestamp),
                    State
                    );
        }

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

            if (!(message[0] is bool))
            {
                return false;
            }

            State = (bool)message[0];

            return true;
        }
    }
}