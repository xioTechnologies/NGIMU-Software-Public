using System;
using System.Windows.Forms;

namespace NgimuGui.DialogsAndWindows
{
    partial class ProgressDialog : BaseForm
    {
        public string ProgressMessage
        {
            get
            {
                return m_ProgressMessage.Text;
            }
            set
            {
                m_ProgressMessage.Text = value;
            }
        }

        public int Progress
        {
            get
            {
                return m_ProgressBar.Value;
            }
            set
            {
                m_ProgressBar.Maximum = Math.Max(m_ProgressBar.Maximum, value);
                m_ProgressBar.Value = value;
            }
        }

        public int ProgressMaximum
        {
            get
            {
                return m_ProgressBar.Maximum;
            }
            set
            {
                m_ProgressBar.Value = Math.Min(value, m_ProgressBar.Value);
                m_ProgressBar.Maximum = value;
            }
        }

        public ProgressBarStyle Style
        {
            get
            {
                return m_ProgressBar.Style;
            }
            set
            {
                m_ProgressBar.Style = value;
            }
        }

        public bool CancelButtonEnabled
        {
            get
            {
                return m_CancelButton.Enabled;
            }
            set
            {
                m_CancelButton.Enabled = value;
            }
        }

        public event FormClosingEventHandler OnCancel;

        public ProgressDialog()
        {
            InitializeComponent();
            m_ProgressMessage.Text = "";
        }

        private void ProgressDialog_Load(object sender, EventArgs e)
        {

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                m_CancelButton.PerformClick();

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            FormClosingEventArgs args = new FormClosingEventArgs(CloseReason.UserClosing, false);

            if (OnCancel != null)
            {
                OnCancel(this, args);
            }

            if (args.Cancel == false)
            {
                Close();
            }
        }
    }
}
