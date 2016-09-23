using Rug.Osc;

namespace NgimuApi.Data
{
    /// <summary>
    /// Auxiliary serial data received from the device.
    /// </summary>
    public sealed class AuxiliarySerialData : DataBase
    {
        /// <summary>
        /// Gets the OSC address for this data object.
        /// </summary>
        public override string OscAddress { get { return "/auxserial"; } }

        /// <summary>
        /// True if the received OSC message represented the data as an OSC string instead of an OSC blob.
        /// </summary>
        public bool IsDataAString { get; private set; }

        /// <summary>
        /// Gets the auxiliary serial data as a byte array.  This data will be null if the received OSC message represented the data as an OSC string.
        /// </summary>
        public byte[] Data { get; private set; }

        /// <summary>
        /// Gets the auxiliary serial data as a string.  This data will be null if the received OSC message represented the data as an OSC blob.
        /// </summary>
        public string StringData { get; private set; }

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

            if (message[0] is byte[])
            {
                Data = (byte[])message[0];
                StringData = string.Empty;
                IsDataAString = false;

                return true;
            }
            else if (message[0] is string)
            {
                Data = new byte[0];
                StringData = (string)message[0];
                IsDataAString = true;

                return true;
            }
            else
            {
                return false;
            }
        }

        internal override string CsvHeader
        {
            get
            {
                return Helper.ToCsv(
                        "Time (s)",
                        "Data"
                        );
            }
        }

        internal override string ToCsv(OscTimeTag firstTimestamp)
        {
            if (IsDataAString == false)
            {
                return Helper.ToCsv(
                        CreateTimestampString(firstTimestamp),
                        OscHelper.ToStringBlob(Data)
                        );
            }
            else
            {
                return Helper.ToCsv(
                        CreateTimestampString(firstTimestamp),
                        OscHelper.EscapeString(StringData)
                        );
            }
        }
    }
}
