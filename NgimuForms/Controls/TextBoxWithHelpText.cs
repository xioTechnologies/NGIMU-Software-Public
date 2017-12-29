using System;
using System.Drawing;
using System.Windows.Forms;

namespace NgimuForms.Controls
{
    public class TextBoxWithHelpText : TextBox
    {
        private string helpText = "";
        private Color helpTextColor = Color.LightGray;
        private Color textErrorColor = Color.Red;
        private bool helpTextVisible = false;
        private bool hasError = false;
        private Color foreColor = Control.DefaultForeColor;

        public string HelpText
        {
            get => helpText;

            set
            {
                helpText = value;

                Invalidate();
            }
        }

        public Color HelpTextColor
        {
            get => helpTextColor;

            set
            {
                helpTextColor = value;

                Invalidate();
            }
        }

        public Color TextErrorColor
        {
            get => textErrorColor;

            set
            {
                textErrorColor = value;

                base.ForeColor = hasError ? textErrorColor : foreColor;

                Invalidate();
            }
        }

        public bool HasError
        {
            get => hasError;

            set
            {
                hasError = value;

                base.ForeColor = hasError ? textErrorColor : foreColor;

                Invalidate();
            }
        }

        public new Color ForeColor
        {
            get => foreColor;

            set
            {
                foreColor = value;

                base.ForeColor = hasError ? textErrorColor : foreColor;

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
            base.ForeColor = hasError ? textErrorColor : foreColor; 

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

            HideSelection = true;

            Refresh();
        }

        private void EnableWaterMark()
        {
            if (EnableUserPaintStyles == true)
            {
                this.SetStyle(ControlStyles.UserPaint, true);
            }

            HideSelection = false;

            helpTextVisible = true;

            Refresh();
        }
    }
}