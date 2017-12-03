using System;
using Rug.Osc;

namespace NgimuApi.Data
{
    /// <summary>
    /// Battery data received from the device.
    /// </summary>
    public sealed class BatteryData : DataBase
    {
        /// <summary>
        /// Gets the OSC address for this data object.
        /// </summary>
        public override string OscAddress { get { return "/battery"; } }

        /// <summary>
        /// Gets the battery percentage.
        /// </summary>
        public float Percentage { get; private set; }

        /// <summary>
        /// Gets the battery voltage in volts (V).
        /// </summary>
        public float Voltage { get; private set; }

        /// <summary>
        /// Gets the time to empty in minutes.
        /// </summary>
        public TimeSpan TimeToEmpty { get; private set; }

        /// <summary>
        /// Gets the battery current in milliamps (mA).
        /// </summary>
        public float Current { get; private set; }

        /// <summary>
        /// Gets the charger state.  This is a string received from the device.
        /// </summary>
        public string ChargerState { get; private set; }

        /// <summary>
        /// Gets a flag indicated if the charger is connected.  This state is set to true if the time to empty is infinite.
        /// </summary>
        public bool IsChargerConnected { get; private set; }

        /// <summary>
        /// Method called when an OSC message associated with this data object is received.
        /// </summary>
        /// <param name="message">Received OSC message.</param>
        /// <returns>True if the OSC message was successfully parsed.</returns>
        protected override bool OnMessageReceived(OscMessage message)
        {
            if (message.Count != 5)
            {
                return false;
            }

            for (int i = 0; i < 4; i++)
            {
                if (!(message[i] is float))
                {
                    return false;
                }
            }

            if (!(message[4] is string))
            {
                return false;
            }

            int argumentIndex = 0;
            Percentage = (float)message[argumentIndex++];

            float timeToEmpty = (float)message[argumentIndex++];

            Voltage = (float)message[argumentIndex++];

            Current = (float)message[argumentIndex++];

            ChargerState = (string)message[argumentIndex++];

            IsChargerConnected = false;

            if (float.IsPositiveInfinity(timeToEmpty) == true)
            {
                IsChargerConnected = true;
                TimeToEmpty = TimeSpan.MaxValue;
            }
            else if (float.IsNegativeInfinity(timeToEmpty) == true)
            {
                TimeToEmpty = TimeSpan.MinValue;
            }
            else if (float.IsNaN(timeToEmpty) == true)
            {
                TimeToEmpty = TimeSpan.Zero;
            }
            else
            {
                TimeToEmpty = TimeSpan.FromMinutes(timeToEmpty);
            }

            return true;
        }

        internal override string CsvHeader
        {
            get
            {
                return Helper.ToCsv(
                        "Time (s)",
                        "Percentage (%)",
                        "Time To Empty (minutes)",
                        "Voltage (V)",
                        "Current (mA)",
                        "Is Charger Connected (Boolean)"
                        );
            }
        }

        internal override string ToCsv(OscTimeTag firstTimestamp)
        {
            return Helper.ToCsv(
                    CreateTimestampString(firstTimestamp),
                    Percentage,
                    TimeToEmpty,
                    Voltage,
                    Current,
                    IsChargerConnected
                    );
        }
    }
}
