using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using NgimuForms.Controls;

namespace NgimuSynchronisedNetworkManager.Controls
{
    public class DataGridViewSettingsValueColumn : DataGridViewColumn
    {
        public DataGridViewSettingsValueColumn() : base(new DataGridViewSettingsValueCell())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                // Ensure that the cell used for the template is a DataGridViewSettingsValueCell.
                if (value != null && value.GetType().IsAssignableFrom(typeof(DataGridViewSettingsValueCell)) == false)
                {
                    throw new InvalidCastException("Must be a DataGridViewSettingsValueCell");
                }

                base.CellTemplate = value;
            }
        }
    }

    public class DataGridViewSettingsValueCell : DataGridViewTextBoxCell
    {
        public DataGridViewSettingsValueCell()
            : base()
        {

        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            // Set the value of the editing control to the current cell value.
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            DataGridViewSettingsValueEditingControl ctl = DataGridView.EditingControl as DataGridViewSettingsValueEditingControl;

            ctl.HelpText = dataGridViewCellStyle.NullValue.ToString();

            // Use the default row value when Value property is null.
            if (this.Value == null)
            {
                ctl.Text = string.Empty;
            }
            else if (this.Value is float)
            {
                ctl.Text = ((float)this.Value).ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                ctl.Text = string.Empty;
            }
        }

        public override Type EditType
        {
            get
            {
                // Return the type of the editing control that CalendarCell uses.
                return typeof(DataGridViewSettingsValueEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                return typeof(object); // float?);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                return null;
            }
        }
    }

    class DataGridViewSettingsValueEditingControl : TextBoxWithHelpText, IDataGridViewEditingControl
    {
        DataGridView dataGridView;
        private bool valueChanged = false;
        int rowIndex;

        public DataGridViewSettingsValueEditingControl()
        {
            this.TextChanged += DataGridViewSettingsValueEditingControl_TextChanged;
            EnableUserPaintStyles = false;
        }

        private void DataGridViewSettingsValueEditingControl_TextChanged(object sender, EventArgs e)
        {
            float value;

            if (float.TryParse(Text, out value) == false)
            {
                ForeColor = Color.Red;
                Font = DefaultFont;
            }
            else
            {
                ForeColor = DefaultForeColor;
                Font = DefaultFont;
            }

            valueChanged = true;

            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
        }

        // Implements the IDataGridViewEditingControl.EditingControlFormattedValue 
        // property.
        public object EditingControlFormattedValue
        {
            get
            {
                return this.Text;
            }
            set
            {
                if (value is string)
                {
                    Text = (string)value;
                }
            }
        }

        // Implements the 
        // IDataGridViewEditingControl.GetEditingControlFormattedValue method.
        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }

        // Implements the 
        // IDataGridViewEditingControl.ApplyCellStyleToEditingControl method.
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
        }

        // Implements the IDataGridViewEditingControl.EditingControlRowIndex 
        // property.
        public int EditingControlRowIndex
        {
            get
            {
                return rowIndex;
            }
            set
            {
                rowIndex = value;
            }
        }

        // Implements the IDataGridViewEditingControl.EditingControlWantsInputKey method.
        public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
        {
            // Let the DateTimePicker handle the keys listed.
            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default:
                    return dataGridViewWantsInputKey == false;
            }
        }

        // Implements the IDataGridViewEditingControl.PrepareEditingControlForEdit method.
        public void PrepareEditingControlForEdit(bool selectAll)
        {
            if (selectAll == true)
            {
                SelectAll();
            }
        }

        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }

        public DataGridView EditingControlDataGridView
        {
            get
            {
                return dataGridView;
            }
            set
            {
                dataGridView = value;
            }
        }

        public bool EditingControlValueChanged
        {
            get
            {
                return valueChanged;
            }
            set
            {
                valueChanged = value;
            }
        }

        public Cursor EditingPanelCursor
        {
            get
            {
                return base.Cursor;
            }
        }
    }
}
