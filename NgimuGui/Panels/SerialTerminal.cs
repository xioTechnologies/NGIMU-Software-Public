using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Rug.Cmd;
using Rug.Cmd.Colors;
using Rug.Osc;

namespace NgimuGui.Panels
{
    public partial class SerialTerminal : UserControl, IOscPacketReceiver
    {
        bool m_HasValidSendString = false;
        string m_HelpText = "Type characters here and press 'Enter' to send.";

        Font m_InputFont = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        Font m_BackupFont;

        IConsole m_Console;

        public event OscPacketEvent PacketRecived;

        public OscCommunicationStatistics Statistics { get; set; }

        public SerialTerminal()
        {
            if ((RC.Sys is NullConsole) == false)
            {
                RC.Sys = new NullConsole();
            }

            ConsoleColorState state = RC.ColorState;
            try
            {
                RC.BackgroundColor = ConsoleColorExt.Black;

                InitializeComponent();

                m_Console = consoleControl1.Buffer;
            }
            finally
            {
                RC.ColorState = state;
            }
        }

        private void Terminal_Load(object sender, EventArgs e)
        {
            m_Console.Theme = ConsoleColorTheme.Load(ConsoleColor.White, ConsoleColor.Black, ConsoleColorDefaultThemes.Colorful);

            m_BackupFont = m_SendMessageBox.Font;

            m_SendMessageBox.Text = m_HelpText;
            m_SendMessageBox.DropDown += SendMessageBox_DropDown;
            m_SendMessageBox.DropDownClosed += SendMessageBox_DropDownClosed;

            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Terminal_DragEnter);
            this.DragDrop += new DragEventHandler(Terminal_DragDrop);
        }

        public void OnData(byte[] data)
        {
            GuiTerminal.WriteMessage(m_Console, NgimuApi.MessageDirection.Receive, ConsoleColorExt.White, OscHelper.Escape(data));
        }

        public void OnData(string data)
        {
            GuiTerminal.WriteMessage(m_Console, NgimuApi.MessageDirection.Receive, ConsoleColorExt.White, OscHelper.EscapeString(data));
        }

        public void OnMessage(OscMessage message)
        {
            if (message.Count > 0 && message[0] is byte[])
            {
                GuiTerminal.WriteMessage(m_Console, NgimuApi.MessageDirection.Receive, ConsoleColorExt.White, OscHelper.Escape((byte[])message[0]));
            }
            else
            {
                GuiTerminal.WriteMessage(m_Console, NgimuApi.MessageDirection.Receive, ConsoleColorExt.White, OscHelper.EscapeString(message[0].ToString()));
            }
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
                foreach (string line in File.ReadAllLines(NgimuApi.Helper.ResolvePath(oscString)))
                {
                    byte[] bytes = OscHelper.Unescape(line);

                    PacketRecived(new OscMessage("/auxserial", bytes));

                    GuiTerminal.WriteMessage(m_Console, NgimuApi.MessageDirection.Transmit, ConsoleColorExt.White, OscHelper.Escape(bytes));
                }
            }
            else
            {
                byte[] bytes = OscHelper.Unescape(oscString);

                PacketRecived(new OscMessage("/auxserial", bytes));

                GuiTerminal.WriteMessage(m_Console, NgimuApi.MessageDirection.Transmit, ConsoleColorExt.White, OscHelper.Escape(bytes));
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

            if (m_SendMessageBox.Text == string.Empty)
            {
                m_HasValidSendString = false;
            }
            else if (File.Exists(NgimuApi.Helper.ResolvePath(m_SendMessageBox.Text)) == true)
            {
                m_HasValidSendString = true;
            }
            else
            {
                m_HasValidSendString = OscHelper.IsValidEscape(m_SendMessageBox.Text);
            }

            m_SendMessageBox.ForeColor = m_HasValidSendString ? Control.DefaultForeColor : Color.Red;
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
