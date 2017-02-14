using System;
using System.Drawing;
using System.Windows.Forms;

namespace NgimuForms.Controls
{
    public class TextBoxWithHelpText : TextBox
    {
        private string helpText = "";
        private Color helpTextColor = Color.Gray;
        private bool helpTextVisible = false;

        public string HelpText
        {
            get { return helpText; }

            set
            {
                helpText = value;

                Invalidate();
            }
        }

        public Color HelpTextColor
        {
            get { return helpTextColor; }

            set
            {
                helpTextColor = value;

                Invalidate();
            }
        }

        protected bool EnableUserPaintStyles { get; set; } = true;

        public TextBoxWithHelpText()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            TextChanged += CheckHelpText;
            Leave += CheckHelpText;
            Enter += CheckHelpText;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            CheckHelpText(null, null);
        }

        protected override void OnPaint(PaintEventArgs args)
        {
            base.OnPaint(args);

            if (Enabled == false)
            {
                args.Graphics.Clear(SystemColors.Control);
            }

            if (helpTextVisible != true)
            {
                return;
            }

            using (SolidBrush drawBrush = new SolidBrush(HelpTextColor))
            {
                args.Graphics.DrawString(HelpText, Font, drawBrush, new PointF(0.0F, 0.0F));
            }
        }

        private void CheckHelpText(object sender, EventArgs args)
        {
            if (string.IsNullOrEmpty(Text) == true)
            {
                EnableWaterMark();
            }
            else
            {
                DisbaleWaterMark();
            }
        }

        private void DisbaleWaterMark()
        {
            helpTextVisible = false;

            if (EnableUserPaintStyles == true)
            {
                this.SetStyle(ControlStyles.UserPaint, false);
            }

            Refresh();
        }

        private void EnableWaterMark()
        {
            if (EnableUserPaintStyles == true)
            {
                this.SetStyle(ControlStyles.UserPaint, true);
            }

            helpTextVisible = true;

            Refresh();
        }
    }
}