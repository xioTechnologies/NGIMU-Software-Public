using System;
using System.Drawing;
using System.Windows.Forms;
using NgimuApi;

namespace NgimuGui.Panels
{
    public partial class ClockPanel : UserControl
    {
        Font m_BaseFont = new Font("Lucida Console", 15);
        Font m_ResizedFont = null;
        SizeF m_StringSize;
        int m_StringLength = 0;

        public TimeSpan CurrentTime { get; set; }

        public ClockPanel()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
        }

        public static Font AppropriateFont(Graphics g, float minFontSize, float maxFontSize, Size layoutSize, string s, Font font, out SizeF extent)
        {
            if (maxFontSize == minFontSize)
            {
                font = new Font(font.FontFamily, minFontSize, font.Style);
            }

            extent = g.MeasureString(s, font);

            if (maxFontSize <= minFontSize)
                return font;

            float hRatio = layoutSize.Height / extent.Height;
            float wRatio = layoutSize.Width / extent.Width;
            float ratio = (hRatio < wRatio) ? hRatio : wRatio;

            float newSize = font.Size * ratio;

            if (newSize < minFontSize)
                newSize = minFontSize;
            else if (newSize > maxFontSize)
                newSize = maxFontSize;

            font = new Font(font.FontFamily, newSize, font.Style);

            extent = g.MeasureString(s, font);

            return font;
        }

        private void ClockPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            string text = Helper.TimeSpanToString(CurrentTime, TimeSpanStringFormat.StopWatch);

            if (m_StringLength != text.Length || m_ResizedFont == null)
            {
                ResizeFont(e.Graphics, text);

                m_StringLength = text.Length;
            }

            /* 
            PointF p = new PointF(
                (ClientRectangle.Width - m_StringSize.Width) / 2,
                ((float)ClientRectangle.Height - (float)m_StringSize.Height) * 0f);
            */

            PointF p = new PointF(
                (ClientRectangle.Width - m_StringSize.Width) / 2,
                ((float)ClientRectangle.Height * 0.5f) - ((float)m_StringSize.Height * 0.4f));


            e.Graphics.DrawString(text, m_ResizedFont, Brushes.White, p);
        }

        private void ResizeFont(Graphics g, string text)
        {
            if (m_ResizedFont != null)
            {
                m_ResizedFont.Dispose();
                m_ResizedFont = null;
            }

            m_ResizedFont = AppropriateFont(g, 5, 5000, ClientRectangle.Size, text, m_BaseFont, out m_StringSize);
        }

        private void ClockPanel_Resize(object sender, EventArgs e)
        {
            if (m_ResizedFont != null)
            {
                m_ResizedFont.Dispose();
                m_ResizedFont = null;
            }

            Invalidate();
        }
    }
}
