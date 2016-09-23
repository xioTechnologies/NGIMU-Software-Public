using System;
using System.Windows.Forms;

namespace NgimuGui.DialogsAndWindows
{
    public partial class ReceivedErrorMessagesWindow : BaseForm
    {
        public ReceivedErrorMessagesWindow()
        {
            InitializeComponent();
        }

        private void ReceivedErrorMessages_Changed()
        {
            Invoke(new Action(UpdateText));
        }

        private void ReceivedErrorMessagesWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            ReceivedErrorMessages.Changed -= ReceivedErrorMessages_Changed;
        }

        private void ReceivedErrorMessagesWindow_Load(object sender, EventArgs e)
        {
            ReceivedErrorMessages.Changed += ReceivedErrorMessages_Changed;
            UpdateText();
        }

        private void UpdateText()
        {
            textBox1.Text = ReceivedErrorMessages.GetString();
            textBox1.Select(0, 0);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}