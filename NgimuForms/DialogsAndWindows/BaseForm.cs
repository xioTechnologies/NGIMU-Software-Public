using System.Drawing;
using System.Windows.Forms;

namespace NgimuForms.DialogsAndWindows
{
    public partial class BaseForm : Form
    {
        public readonly string WindowID;

        public bool IsOpenReflectsVisiblity { get; set; } = false;

        public BaseForm()
        {
            InitializeComponent();
        }

        public BaseForm(string windowID)
        {
            this.WindowID = windowID;

            InitializeComponent();
        }

        private void BaseForm_Load(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(WindowID) == true)
            {
                return;
            }

            if (WindowManager.Get(WindowID).Bounds != Rectangle.Empty)
            {
                this.DesktopBounds = WindowManager.Get(WindowID).Bounds;
            }

            WindowState = WindowManager.Get(WindowID).WindowState;

            WindowManager.Get(WindowID).IsOpen = true;

        }

        private void BaseForm_Resize(object sender, System.EventArgs e)
        {
            if (Visible == false)
            {
                return;
            }

            if (string.IsNullOrEmpty(WindowID) == true)
            {
                return;
            }

            if (WindowState != FormWindowState.Minimized)
            {
                WindowManager.Get(WindowID).WindowState = WindowState;
            }
        }

        private void BaseForm_ResizeBegin(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(WindowID) == true)
            {
                return;
            }
        }

        private void BaseForm_ResizeEnd(object sender, System.EventArgs e)
        {
            if (Visible == false)
            {
                return;
            }

            if (string.IsNullOrEmpty(WindowID) == true)
            {
                return;
            }

            if (WindowState == FormWindowState.Normal)
            {
                WindowManager.Get(WindowID).Bounds = this.DesktopBounds;
            }
        }

        private void BaseForm_SizeChanged(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(WindowID) == true)
            {
                return;
            }

            //WindowManager.Get(WindowID).WindowState = WindowState;

            //if (WindowState == FormWindowState.Normal)
            //{
            //    WindowManager.Get(WindowID).Bounds = this.DesktopBounds;
            //}
        }

        private void BaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (string.IsNullOrEmpty(WindowID) == true)
            {
                return;
            }

            if (e.CloseReason != CloseReason.UserClosing && e.CloseReason != CloseReason.None)
            {
                return;
            }

            if (IsOpenReflectsVisiblity == true)
            {
                Hide();

                e.Cancel = true;
            }
            else
            {
                WindowManager.Get(WindowID).IsOpen = false;
            }
        }

        private void BaseForm_VisibleChanged(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(WindowID) == true)
            {
                return;
            }

            if (IsOpenReflectsVisiblity == false)
            {
                return;
            }

            WindowManager.Get(WindowID).IsOpen = this.Visible;
        }
    }
}
