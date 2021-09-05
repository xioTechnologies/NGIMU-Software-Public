using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NgimuApi;

namespace NgimuSynchronisedNetworkManager.DialogsAndWindows
{
    public partial class TimestampWindow : Form
    {
        public List<Connection> Connections { get; } = new List<Connection>();

        public TimestampWindow()
        {
            InitializeComponent();
        }

        private void TimestampWindow_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.MinValue;

            foreach (Connection connection in Connections)
            {
                dateTime = Max(dateTime, connection.Altitude.Timestamp.ToDataTime());
                dateTime = Max(dateTime, connection.AnalogueInputs.Timestamp.ToDataTime());
                dateTime = Max(dateTime, connection.AuxiliarySerial.Timestamp.ToDataTime());
                dateTime = Max(dateTime, connection.AuxiliarySerialCts.Timestamp.ToDataTime());
                dateTime = Max(dateTime, connection.Battery.Timestamp.ToDataTime());
                dateTime = Max(dateTime, connection.Button.Timestamp.ToDataTime());
                dateTime = Max(dateTime, connection.EarthAcceleration.Timestamp.ToDataTime());
                dateTime = Max(dateTime, connection.EulerAngles.Timestamp.ToDataTime());
                dateTime = Max(dateTime, connection.Humidity.Timestamp.ToDataTime());
                dateTime = Max(dateTime, connection.LinearAcceleration.Timestamp.ToDataTime());
                dateTime = Max(dateTime, connection.Magnitudes.Timestamp.ToDataTime());
                dateTime = Max(dateTime, connection.Quaternion.Timestamp.ToDataTime());
                dateTime = Max(dateTime, connection.RotationMatrix.Timestamp.ToDataTime());
                dateTime = Max(dateTime, connection.Rssi.Timestamp.ToDataTime());
                dateTime = Max(dateTime, connection.Sensors.Timestamp.ToDataTime());
                dateTime = Max(dateTime, connection.SerialCts.Timestamp.ToDataTime());
                dateTime = Max(dateTime, connection.Temperature.Timestamp.ToDataTime());

                m_ClockPanel.CurrentTime = dateTime - dateTime.Date;
            }

            m_ClockPanel.Invalidate();
        }

        private DateTime Max(DateTime a, DateTime b)
        {
            return a > b ? a : b;
        }
    }
}
