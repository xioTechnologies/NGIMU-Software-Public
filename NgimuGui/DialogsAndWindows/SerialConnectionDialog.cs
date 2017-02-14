using System;
using System.Drawing;
using System.Windows.Forms;
using NgimuApi;
using NgimuForms.DialogsAndWindows;

namespace NgimuGui.DialogsAndWindows
{
    public partial class SerialConnectionDialog : BaseForm
    {
        private string m_HelpText = "No serial ports available";
        private string m_HelpText2 = "Select or enter a serial port";

        private string m_BaudRate_HelpText = "Select or enter a baud rate";

        public string PortName { get; set; }
        public uint BaudRate { get; set; }
        public bool RtsCtsEnabled { get; set; }

        public SerialConnectionDialog()
        {
            InitializeComponent();
        }

        private void SerialConnectionDialog_Load(object sender, EventArgs e)
        {
            m_BaudRate.SelectedIndex = 6;
            m_RtsCtsEnabled.SelectedIndex = 0;

            RescanSerialPorts();

            if (m_Port.Items.Count > 0)
            {
                m_Port.SelectedIndex = 0;
            }
            else
            {
                m_Port.Text = m_HelpText;
            }
        }

        private void SerialConnectionDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing &&
                e.CloseReason != CloseReason.None)
            {
                return;
            }

            if (this.DialogResult != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            if (ValidateValues() == true)
            {
                return;
            }

            e.Cancel = true;

            System.Media.SystemSounds.Exclamation.Play();

            FlashingDialogHelper.FlashWindowEx(this);
        }

        private bool ValidateValues()
        {
            bool valid = true;

            uint baudRate;

            valid &= uint.TryParse(m_BaudRate.Text, out baudRate);

            valid &= Helper.IsNullOrEmpty(m_Port.Text) == false;

            valid &= m_Port.Text.Trim() != m_HelpText;
            valid &= m_Port.Text.Trim() != m_HelpText2;

            if (valid == true)
            {
                PortName = m_Port.Text;
                BaudRate = baudRate;
                RtsCtsEnabled = m_RtsCtsEnabled.SelectedIndex == 1;
            }

            return valid;
        }

        private void BaudRate_TextChanged(object sender, EventArgs e)
        {
            if (m_BaudRate.Text == m_BaudRate_HelpText)
            {
                m_BaudRate.ForeColor = Color.LightGray;
                return;
            }

            if (Helper.IsNullOrEmpty(m_BaudRate.Text) == true)
            {
                m_BaudRate.ForeColor = Control.DefaultForeColor;
                return;
            }

            uint value;

            if (uint.TryParse(m_BaudRate.Text, out value) == false)
            {
                m_BaudRate.ForeColor = Color.Red;
            }
            else
            {
                m_BaudRate.ForeColor = Control.DefaultForeColor;
            }
        }

        private void BaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void BaudRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (m_BaudRate.Text == m_BaudRate_HelpText)
            {
                m_BaudRate.Text = "";
            }
        }

        private void BaudRate_Enter(object sender, EventArgs e)
        {
            m_BaudRate.AutoCompleteMode = AutoCompleteMode.None;

            BaudRate_MouseEvent(sender, null);
        }

        private void BaudRate_Leave(object sender, EventArgs e)
        {
            m_BaudRate.AutoCompleteMode = AutoCompleteMode.None;

            if (Helper.IsNullOrEmpty(m_BaudRate.Text) == true)
            {
                m_BaudRate.Text = m_BaudRate_HelpText;
            }
        }

        private void BaudRate_MouseEvent(object sender, MouseEventArgs e)
        {
            if (m_BaudRate.Text == m_BaudRate_HelpText)
            {
                m_BaudRate.Select(0, 0);
            }
        }


        private void BaudRate_DropDown(object sender, EventArgs e)
        {
            if (m_BaudRate.Text == m_BaudRate_HelpText)
            {
                m_BaudRate.Text = "";
            }
        }

        private void BaudRate_DropDownClosed(object sender, EventArgs e)
        {
            if (Helper.IsNullOrEmpty(m_BaudRate.Text) == true)
            {
                m_BaudRate.Text = m_BaudRate_HelpText;
            }
        }

        private void RtsCtsEnabled_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RescanSerialPorts()
        {
            m_Port.Items.Clear();

            m_Port.Items.AddRange(Helper.GetSerialPortNames());
        }

        private void Port_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Port_TextChanged(object sender, EventArgs e)
        {
            if (m_Port.Text == m_HelpText ||
                m_Port.Text == m_HelpText2)
            {
                m_Port.ForeColor = Color.LightGray;

                return;
            }
            else
            {
                m_Port.ForeColor = Control.DefaultForeColor;
            }
        }

        private void Port_KeyDown(object sender, KeyEventArgs e)
        {
            if (m_Port.Text == m_HelpText ||
                m_Port.Text == m_HelpText2)
            {
                m_Port.Text = "";
            }
        }

        private void Port_Enter(object sender, EventArgs e)
        {
            RescanSerialPorts();

            m_Port.AutoCompleteMode = AutoCompleteMode.None;

            if (m_Port.Items.Count == 0)
            {
                if (m_Port.Text == m_HelpText2)
                {
                    m_Port.Text = m_HelpText;
                }
            }
            else
            {
                if (m_Port.Text == m_HelpText)
                {
                    m_Port.Text = m_HelpText2;
                }
            }

            Port_MouseEvent(sender, null);
        }

        private void Port_Leave(object sender, EventArgs e)
        {
            m_Port.AutoCompleteMode = AutoCompleteMode.None;

            if (Helper.IsNullOrEmpty(m_Port.Text) == true)
            {
                if (m_Port.Items.Count > 0)
                {
                    m_Port.Text = m_HelpText2;
                }
                else
                {
                    m_Port.Text = m_HelpText;
                }
            }
        }

        private void Port_MouseEvent(object sender, MouseEventArgs e)
        {
            if (m_Port.Text == m_HelpText ||
                m_Port.Text == m_HelpText2)
            {
                m_Port.Select(0, 0);
            }
        }

        private void Port_DropDown(object sender, EventArgs e)
        {
            RescanSerialPorts();

            if (m_Port.Text == m_HelpText ||
                m_Port.Text == m_HelpText2)
            {
                m_Port.Text = "";
            }
        }

        private void Port_DropDownClosed(object sender, EventArgs e)
        {
            if (Helper.IsNullOrEmpty(m_Port.Text) == true)
            {
                if (m_Port.Items.Count > 0)
                {
                    m_Port.Text = m_HelpText2;
                }
                else
                {
                    m_Port.Text = m_HelpText;
                }
            }
        }
    }
}
