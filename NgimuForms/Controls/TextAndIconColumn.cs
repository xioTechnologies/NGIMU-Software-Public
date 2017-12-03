using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NgimuForms.Controls
{
    public class IconInfo
    {
        public MessageBoxIcon MessageIcon { get; set; }

        public Image Image { get; set; }

        public string Message { get; set; }

        public bool Visible { get; set; }
    }

    public class TextAndIconColumn : DataGridViewTextBoxColumn
    {
        public TextAndIconColumn()
        {
            this.CellTemplate = new TextAndIconCell();
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }

    public class TextAndIconCell : DataGridViewTextBoxCell
    {
        private readonly List<IconInfo> icons = new List<IconInfo>();
        private Rectangle cellBounds;

        public override object Clone()
        {
            TextAndIconCell c = base.Clone() as TextAndIconCell;

            c.icons.AddRange(this.icons);

            return c;
        }

        public int Count => icons.Count; 

        public IconInfo this[int index] => icons[index];

        public void Add(IconInfo icon)
        {
            icons.Add(icon); 
        }

        //public Image Image
        //{
        //    get
        //    {
        //        if (this.OwningColumn == null ||
        //            this.OwningTextAndImageColumn == null)
        //        {

        //            return imageValue;
        //        }
        //        else if (this.imageValue != null)
        //        {
        //            return this.imageValue;
        //        }
        //        else
        //        {
        //            return this.OwningTextAndImageColumn.Image;
        //        }
        //    }
        //    set
        //    {
        //        if (this.imageValue == value)
        //        {
        //            return;
        //        }

        //        this.imageValue = value;
        //        this.imageSize = value.Size;

        //        Padding inheritedPadding = this.InheritedStyle.Padding;

        //        this.Style.Padding = new Padding(inheritedPadding.Left,
        //            inheritedPadding.Top, inheritedPadding.Right - (imageSize.Width + 4),
        //            inheritedPadding.Bottom);
        //    }
        //}

        protected override void Paint(Graphics graphics, Rectangle clipBounds,
                                        Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState,
                                        object value, object formattedValue, string errorText,
                                        DataGridViewCellStyle cellStyle,
                                        DataGridViewAdvancedBorderStyle advancedBorderStyle,
                                        DataGridViewPaintParts paintParts)
        {
            this.cellBounds = cellBounds; 

            int offset = 4; 

            foreach (IconInfo info in icons)
            {
                if (info.Visible == false)
                {
                    continue; 
                }

                offset += info.Image.Width + 4; 
            }

            Padding inheritedPadding = this.InheritedStyle.Padding;

            this.Style.Padding = new Padding(inheritedPadding.Left,
                inheritedPadding.Top, inheritedPadding.Right - offset,
                inheritedPadding.Bottom);

            // Paint the base content
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState,
               value, formattedValue, errorText, cellStyle,
               advancedBorderStyle, paintParts);

            // Draw the image clipped to the cell.
            System.Drawing.Drawing2D.GraphicsContainer container = graphics.BeginContainer();

            graphics.SetClip(cellBounds);

            int x = cellBounds.Right - offset; 

            foreach (IconInfo info in icons)
            {
                if (info.Visible == false)
                {
                    continue; 
                }

                float vOffset = (cellBounds.Top + (cellBounds.Height * 0.5f)) - (info.Image.Height * 0.5f);

                graphics.DrawImageUnscaled(info.Image, x, (int) vOffset);

                x += info.Image.Width + 4; 
            }

            graphics.EndContainer(container);
        }

        public bool TryClick(Point mouse, out IconInfo iconInfo)
        {
            int offset = 4;

            foreach (IconInfo info in icons)
            {
                if (info.Visible == false)
                {
                    continue;
                }

                offset += info.Image.Width + 4;
            }

            int x = cellBounds.Right - offset;

            foreach (IconInfo info in icons)
            {
                if (info.Visible == false)
                {
                    continue;
                }

                float vOffset = (cellBounds.Top + (cellBounds.Height * 0.5f)) - (info.Image.Height * 0.5f);

                Rectangle imageRect = new Rectangle(x, (int)vOffset, info.Image.Width, info.Image.Height);

                if (imageRect.Contains(mouse) == true)
                {
                    iconInfo = info;

                    return true; 
                }
                
                x += info.Image.Width + 4;
            }

            iconInfo = null;

            return false; 
        }

        private TextAndIconColumn OwningTextAndImageColumn => this.OwningColumn as TextAndIconColumn;
    }
}
