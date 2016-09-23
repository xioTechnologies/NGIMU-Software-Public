using System;
using System.Drawing;
using System.Windows.Forms;

namespace NgimuGui.Controls
{
    class TextBoxWithHelpText : TextBox
    {
        private bool m_HelpTextVisible = false;

        private Color m_HelpTextColor = Color.Gray;
        private string m_HelpText = "";

        public Color HelpTextColor
        {
            get { return m_HelpTextColor; }
            set
            {
                m_HelpTextColor = value;
                Invalidate();
            }
        }

        public string HelpText
        {
            get { return m_HelpText; }
            set
            {
                m_HelpText = value;
                Invalidate();
            }
        }

        public TextBoxWithHelpText()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            this.TextChanged += new System.EventHandler(this.CheckHelpText);
            this.Leave += new System.EventHandler(this.CheckHelpText);
            this.Enter += new System.EventHandler(this.CheckHelpText);
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

            if (m_HelpTextVisible == true)
            {
                using (SolidBrush drawBrush = new SolidBrush(HelpTextColor))
                {
                    args.Graphics.DrawString(HelpText, Font, drawBrush, new PointF(0.0F, 0.0F));
                }
            }
        }

        private void CheckHelpText(object sender, EventArgs args)
        {
            if (String.IsNullOrEmpty(Text) == true)
            {
                EnableWaterMark();
            }
            else
            {
                DisbaleWaterMark();
            }
        }

        private void EnableWaterMark()
        {
            this.SetStyle(ControlStyles.UserPaint, true);

            this.m_HelpTextVisible = true;

            Refresh();
        }

        private void DisbaleWaterMark()
        {
            this.m_HelpTextVisible = false;

            this.SetStyle(ControlStyles.UserPaint, false);

            Refresh();
        }
    }
}
