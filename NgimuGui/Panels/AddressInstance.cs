using System;
using System.Windows.Forms;
using Rug.Osc;

namespace NgimuGui.Panels
{
    public sealed class AddressInstance
    {
        private bool m_Mute = false;
        private bool m_Solo = false;

        public bool HasRow { get { return Row != null; } }

        public readonly string Address;
        public readonly DataGridViewRow Row;

        private long m_LastTotal;

        public long Total { get; private set; }

        public double Rate { get; private set; }

        public bool Mute { get { return m_Mute; } set { m_Mute = value; UpdateRow(); } }

        public bool Solo { get { return m_Solo; } set { m_Solo = value; UpdateRow(); } }

        public AddressInstance(string address, DataGridViewRow row)
        {
            Address = address;
            Row = row;
            Total = 1;
        }

        public AddressInstance(AddressInstance @loadedInstance, DataGridViewRow row)
        {
            Address = @loadedInstance.Address;
            Row = row;
        }

        public void CalculateRate(TimeSpan time)
        {
            long diff = Total - m_LastTotal;

            Rate = (double)diff / time.TotalSeconds;

            m_LastTotal = Total;

            UpdateRow();
        }

        public void ResetCount()
        {
            m_LastTotal = 0;
            Total = 0;
            Rate = 0;

            UpdateRow();
        }

        public void OnMessage(OscMessage message)
        {
            Total++;

            UpdateRow();
        }

        private void UpdateRow()
        {
            if (HasRow == true)
            {
                Row.SetValues(Mute, Solo, Address, Total, Rate); // .ToString("F0", CultureInfo.InvariantCulture));
            }
        }
    }
}
