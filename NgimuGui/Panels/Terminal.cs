using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NgimuForms;
using Rug.Cmd;
using Rug.Osc;

namespace NgimuGui.Panels
{
    public partial class Terminal : UserControl, IOscPacketReceiver
    {
        bool m_HasValidSendString = false;
        string m_HelpText = "Type OSC message here and press 'Enter' to send.";

        Font m_InputFont = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        Font m_BackupFont;

        public event OscPacketEvent PacketRecived;

        public OscCommunicationStatistics Statistics { get; set; }

        public int HistoryLength
        {
            get
            {
                return consoleControl1.Buffer.BufferHeight;
            }
            set
            {
                consoleControl1.Buffer.BufferHeight = value;
            }
        }

        public Terminal()
        {
            if ((RC.Sys is NullConsole) == false)
            {
                RC.Sys = new NullConsole();
            }

            InitializeComponent();

            RC.App = consoleControl1.Buffer;
        }

        private void Terminal_Load(object sender, EventArgs e)
        {
            m_BackupFont = m_SendMessageBox.Font;

            m_SendMessageBox.Text = m_HelpText;
            m_SendMessageBox.DropDown += SendMessageBox_DropDown;
            m_SendMessageBox.DropDownClosed += SendMessageBox_DropDownClosed;

            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Terminal_DragEnter);
            this.DragDrop += new DragEventHandler(Terminal_DragDrop);
        }

        private void Send()
        {
            if (m_HasValidSendString == false)
            {
                return;
            }

            string oscString = m_SendMessageBox.Text.Trim();

            int selectionStart = m_SendMessageBox.SelectionStart;
            int selectionLength = m_SendMessageBox.SelectionLength;

            if (m_SendMessageBox.Items.Contains(oscString) == true)
            {
                m_SendMessageBox.Items.Remove(oscString);
            }

            m_SendMessageBox.Items.Insert(0, oscString);

            m_SendMessageBox.SelectedIndex = 0;

            m_SendMessageBox.SelectionStart = selectionStart;
            m_SendMessageBox.SelectionLength = selectionLength;

            if (PacketRecived == null)
            {
                return;
            }

            if (File.Exists(NgimuApi.Helper.ResolvePath(oscString)) == true)
            {
                using (OscFileReader reader = new OscFileReader(NgimuApi.Helper.ResolvePath(oscString), OscPacketFormat.String))
                {
                    while (reader.EndOfStream == false)
                    {
                        OscPacket packet = reader.Read();

                        if (packet.Error != OscPacketError.None)
                        {
                            GuiTerminal.WriteError(packet.ErrorMessage);

                            continue;
                        }

                        PacketRecived(packet);
                    }
                }
            }
            else
            {
                PacketRecived(OscPacket.Parse(oscString));
            }
        }

        private void SendMessageBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SendMessageBox_TextChanged(object sender, EventArgs e)
        {
            if (m_SendMessageBox.Text == m_HelpText)
            {
                m_SendMessageBox.Font = m_BackupFont;
                m_SendMessageBox.ForeColor = Color.LightGray;

                m_HasValidSendString = false;

                return;
            }

            m_SendMessageBox.Font = m_InputFont;

            if (File.Exists(NgimuApi.Helper.ResolvePath(m_SendMessageBox.Text)) == true)
            {
                m_HasValidSendString = true;

                m_SendMessageBox.ForeColor = Control.DefaultForeColor;
            }
            else
            {
                OscPacket packet;

                m_HasValidSendString = OscPacket.TryParse(m_SendMessageBox.Text.Trim(), out packet);

                m_SendMessageBox.ForeColor = m_HasValidSendString ? Control.DefaultForeColor : Color.Red;
            }
        }

        private void SendMessageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter ||
                e.KeyCode == Keys.Return)
            {
                if (m_HasValidSendString == false)
                {
                    return;
                }

                Send();

                e.SuppressKeyPress = true;
            }
        }

        private void SendMessageBox_Enter(object sender, EventArgs e)
        {
            m_SendMessageBox.AutoCompleteMode = AutoCompleteMode.None;

            if (m_SendMessageBox.Text == m_HelpText)
            {
                m_SendMessageBox.Text = "";
            }
        }

        private void SendMessageBox_Leave(object sender, EventArgs e)
        {
            m_SendMessageBox.AutoCompleteMode = AutoCompleteMode.None;

            if (Helper.IsNullOrEmpty(m_SendMessageBox.Text) == true)
            {
                m_SendMessageBox.Text = m_HelpText;
            }
        }

        public void Clear()
        {
            consoleControl1.Clear();
        }

        void Terminal_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        void Terminal_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string file in files)
            {
                m_SendMessageBox.Text = file;

                Send();
            }
        }

        void SendMessageBox_DropDown(object sender, EventArgs e)
        {
            m_SendMessageBox.Font = m_InputFont;
            m_SendMessageBox.ForeColor = Control.DefaultForeColor;
        }

        void SendMessageBox_DropDownClosed(object sender, EventArgs e)
        {
            SendMessageBox_TextChanged(sender, e);
        }
    }
}
