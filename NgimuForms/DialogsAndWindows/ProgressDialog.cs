using System;
using System.Windows.Forms;

namespace NgimuForms.DialogsAndWindows
{
    public partial class ProgressDialog : BaseForm
    {
        public string ProgressMessage
        {
            get
            {
                return progressMessage.Text;
            }
            set
            {
                progressMessage.Text = value;
            }
        }

        public int Progress
        {
            get
            {
                return progressBar.Value;
            }
            set
            {
                progressBar.Maximum = Math.Max(progressBar.Maximum, value);
                progressBar.Value = value;
            }
        }

        public int ProgressMaximum
        {
            get
            {
                return progressBar.Maximum;
            }
            set
            {
                progressBar.Value = Math.Min(value, progressBar.Value);
                progressBar.Maximum = value;
            }
        }

        public ProgressBarStyle Style
        {
            get
            {
                return progressBar.Style;
            }
            set
            {
                progressBar.Style = value;
            }
        }

        public bool CancelButtonEnabled
        {
            get
            {
                return cancelButton.Enabled;
            }
            set
            {
                cancelButton.Enabled = value;
            }
        }

        public event FormClosingEventHandler OnCancel;

        public ProgressDialog()
        {
            InitializeComponent();
            progressMessage.Text = "";
        }

        private void ProgressDialog_Load(object sender, EventArgs e)
        {

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                cancelButton.PerformClick();

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            FormClosingEventArgs args = new FormClosingEventArgs(CloseReason.UserClosing, false);

            OnCancel?.Invoke(this, args);

            if (args.Cancel == false)
            {
                Close();
            }
        }

        public void UpdateProgress(int progress, string message)
        {
            if (IsHandleCreated == true && Disposing == false && IsDisposed == false && InvokeRequired == true)
            {
                Invoke(new MethodInvoker(() =>
                {
                    Progress = progress;
                    ProgressMessage = message;
                }));
            }
            else
            {
                Progress = progress;
                ProgressMessage = message;
            }
        }

        public void UpdateProgress(string message)
        {
            if (IsHandleCreated == true && Disposing == false && IsDisposed == false && InvokeRequired == true)
            {
                Invoke(new MethodInvoker(() =>
                {
                    ProgressMessage = message;
                }));
            }
            else
            {
                ProgressMessage = message;
            }
        }

        public void UpdateProgress(int progress)
        {
            if (IsHandleCreated == true && Disposing == false && IsDisposed == false && InvokeRequired == true)
            {
                Invoke(new MethodInvoker(() =>
                {
                    Progress = progress;
                }));
            }
            else
            {
                Progress = progress;
            }
        }
    }
}
