using System;
using System.Globalization;
using Rug.Osc;

namespace NgimuApi.Data
{
    /// <summary>
    /// Abstract class for all types of data can be received from the device.
    /// </summary>
    public abstract class DataBase
    {
        /// <summary>
        /// Gets the connection that this data object is associated with.
        /// </summary>
        protected Connection Connection { get; private set; }

        /// <summary>
        /// True if this data object has received a valid message. 
        /// </summary>
        public bool HasValidValues { get; private set; }

        /// <summary>
        /// Gets the OSC address for this data object.
        /// </summary>
        public abstract string OscAddress { get; }

        /// <summary>
        /// Gets the timestamp of the most recently parsed message.
        /// </summary>
        public OscTimeTag Timestamp { get; private set; }

        /// <summary>
        /// Gets the header line for the CSV log file.
        /// </summary>
        internal abstract string CsvHeader { get; }

        /// <summary>
        /// Returns a CSV string of the current state of the data object.
        /// </summary>
        /// <returns></returns>
        internal abstract string ToCsv(OscTimeTag firstTimestamp);

        /// <summary>
        /// Raised whenever a valid message is received.
        /// </summary>
        public event EventHandler Received;

        internal DataBase() { }

        /// <summary>
        /// Method called when an OSC message associated with this data object is received.  The inheriting classes should parse messages in this method. 
        /// </summary>
        /// <param name="message">Received OSC message.</param>
        /// <returns>True if the OSC message was successfully parsed.</returns>
        protected abstract bool OnMessageReceived(OscMessage message);

        #region Internal Workings

        internal void Attach(Connection connection)
        {
            if (Connection != null)
            {
                throw new Exception(Strings.Data_AlreadyConnected);
            }

            connection.Attach(OscAddress, OnMessage_Inner);

            Connection = connection;
        }

        internal void Detach()
        {
            if (Connection == null)
            {
                throw new Exception(Strings.Data_NotConnected);
            }

            Connection.Detach(OscAddress, OnMessage_Inner);

            Connection = null;
        }

        internal void OnMessage_Inner(OscMessage message)
        {
            if (message.TimeTag != null)
            {
                Timestamp = (OscTimeTag)message.TimeTag;
            }

            HasValidValues = OnMessageReceived(message);

            if (HasValidValues == true && Received != null)
            {
                Received(this, EventArgs.Empty);
            }
        }

        internal string CreateTimestampString(OscTimeTag firstTimestamp)
        {
            return CreateTimestampString(Timestamp, firstTimestamp);
        }

        internal static string CreateTimestampString(OscTimeTag timestamp, OscTimeTag firstTimestamp)
        {
            return (timestamp.SecondsDecimal - firstTimestamp.SecondsDecimal).ToString("F9", CultureInfo.InvariantCulture);
        }

        #endregion
    }
}
