using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NgimuSynchronisedNetworkManager.Controls
{
    public struct DataGridViewProgressValue : IComparable<DataGridViewProgressValue>, IComparable
    {
        public int Value;
        public string Text;

        public int CompareTo(DataGridViewProgressValue other)
        {
            return Value - other.Value;
        }

        public int CompareTo(object obj)
        {
            if (obj is DataGridViewProgressValue)
            {
                return CompareTo((DataGridViewProgressValue)obj);
            }

            return Value - obj.GetHashCode();
        }
    }

    public class DataGridViewProgressColumn : DataGridViewImageColumn
    {
        public DataGridViewProgressColumn()
        {
            CellTemplate = new DataGridViewProgressCell();
        }
    }

    internal class DataGridViewProgressCell : DataGridViewImageCell
    {
        // Used to make custom cell consistent with a DataGridViewImageCell
        private static Image emptyImage;

        static DataGridViewProgressCell()
        {
            emptyImage = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }

        public DataGridViewProgressCell()
        {
            this.ValueType = typeof(DataGridViewProgressValue);
        }

        // Method required to make the Progress Cell consistent with the default Image Cell.
        // The default Image Cell assumes an Image as a value, although the value of the Progress Cell is an int.
        protected override object GetFormattedValue(object value,
                            int rowIndex, ref DataGridViewCellStyle cellStyle,
                            TypeConverter valueTypeConverter,
                            TypeConverter formattedValueTypeConverter,
                            DataGridViewDataErrorContexts context)
        {
            return emptyImage;
        }

        protected override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            try
            {
                DataGridViewProgressValue dataGridViewProgressValue = (DataGridViewProgressValue)value;
                int progressVal = dataGridViewProgressValue.Value;

                float percentage = ((float)progressVal / 100.0f); // Need to convert to float before division; otherwise C# returns int which is 0 for anything but 100%.

                Brush backColorBrush = new SolidBrush(cellStyle.BackColor);
                Brush foreColorBrush = new SolidBrush(cellStyle.ForeColor);

                // Draws the cell grid
                base.Paint(g, clipBounds, cellBounds,
                            rowIndex, cellState, value, formattedValue, errorText,
                            cellStyle, advancedBorderStyle, (paintParts & ~DataGridViewPaintParts.ContentForeground));

                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                stringFormat.Trimming = StringTrimming.EllipsisCharacter;

                RectangleF layoutRectange = new RectangleF(cellBounds.X + 2, cellBounds.Y + 2, cellBounds.Width - 4, cellBounds.Height - 4);

                if (percentage > 0.0)
                {
                    // Draw the progress bar and the text
                    g.FillRectangle(new SolidBrush(Color.FromArgb(203, 235, 108)), cellBounds.X + 2, cellBounds.Y + 2, Convert.ToInt32((percentage * cellBounds.Width - 4)), cellBounds.Height - 4);
                    g.DrawString(dataGridViewProgressValue.Text, cellStyle.Font, foreColorBrush, layoutRectange, stringFormat);
                }
                else
                {
                    // draw the text
                    if (this.DataGridView.CurrentRow.Index == rowIndex)
                    {
                        g.DrawString(dataGridViewProgressValue.Text, cellStyle.Font, new SolidBrush(cellStyle.SelectionForeColor), layoutRectange, stringFormat);
                    }
                    else
                    {
                        g.DrawString(dataGridViewProgressValue.Text, cellStyle.Font, foreColorBrush, layoutRectange, stringFormat);
                    }
                }
            }
            catch (Exception e) { }
        }
    }
}