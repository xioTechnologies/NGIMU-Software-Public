using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NgimuApi;
using NgimuForms.DialogsAndWindows;
using NgimuSynchronisedNetworkManager.Controls;

namespace NgimuSynchronisedNetworkManager.DialogsAndWindows
{
    public partial class SendRatesWindow : BaseForm
    {
        private readonly Settings settings = new Settings();

        private readonly Dictionary<string, DataGridViewRow> settingsToRowLookup = new Dictionary<string, DataGridViewRow>();
        private readonly Dictionary<ConnectionRow, DataGridViewSettingsValueColumn> connectionToColumnLookup = new Dictionary<ConnectionRow, DataGridViewSettingsValueColumn>();

        private readonly DataGridViewCellStyle dataGridViewCellStyle;

        private readonly DataGridViewCellStyle dataGridViewCellStyleBold;
        private readonly List<DataGridViewCell> selectedCells = new List<DataGridViewCell>();

        public List<ConnectionRow> ActiveConnections { get; } = new List<ConnectionRow>();

        public SendRatesWindow()
        {
            InitializeComponent();

            dataGridViewCellStyle = new DataGridViewCellStyle()
            {
                Font = new Font(sendRatesGrid.Font.FontFamily, sendRatesGrid.Font.Size, FontStyle.Regular),
                NullValue = "Unknown"
            };

            dataGridViewCellStyleBold = new DataGridViewCellStyle()
            {
                Font = new Font(sendRatesGrid.Font.FontFamily, sendRatesGrid.Font.Size, FontStyle.Bold),
                NullValue = "Unknown"
            };
        }

        public void UpdateColumns()
        {   
            foreach (DataGridViewSettingsValueColumn column in connectionToColumnLookup.Values)
            {
                sendRatesGrid.Columns.Remove(column);
            }

            connectionToColumnLookup.Clear();

            sendRatesGrid.SuspendLayout(); 

            foreach (ConnectionRow connection in ActiveConnections)
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(connection.Connection.Settings.DeviceInformation.DeviceName.Value);
                sb.Append(connection.Connection.Settings.DeviceInformation.SerialNumber.Value);

                DataGridViewSettingsValueColumn column = new DataGridViewSettingsValueColumn
                {
                    DefaultCellStyle = dataGridViewCellStyle,

                    HeaderText = sb.ToString(),
                    Tag = connection,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                    Width = 80,
                    MinimumWidth = 80,
                    ValueType = typeof(float),
                };

                sendRatesGrid.Columns.Add(column);

                connectionToColumnLookup.Add(connection, column);
            }

            sendRatesGrid.ResumeLayout(true); 

            CopyValuesToGrid();

            readButton.Enabled = ActiveConnections.Count > 0;
            writeButton.Enabled = ActiveConnections.Count > 0;
        }

        private void SendRates_Load(object sender, EventArgs e)
        {
            foreach (ISettingValue value in settings.SendRates.Values)
            {
                DataGridViewRow row = sendRatesGrid.Rows[sendRatesGrid.Rows.Add(value.Name)];

                row.Tag = value.OscAddress;

                settingsToRowLookup.Add(value.OscAddress, row);
            }

            UpdateColumns();
        }

        private void readAllButton_Click(object sender, EventArgs e)
        {
            this.ReadSettings(ActiveConnections, true, ActiveConnections.Select(connection => connection.Connection.Settings.SendRates));

            CopyValuesToGrid();
        }

        private void writeAllButton_Click(object sender, EventArgs e)
        {
            CopyValuesFromGrid();

            this.WriteSettings(ActiveConnections, true, ActiveConnections.Select(connection => connection.Connection.Settings.SendRates));

            CopyValuesToGrid();
        }

        private void CopyValuesToGrid()
        {
            foreach (ConnectionRow connection in ActiveConnections)
            {
                DataGridViewSettingsValueColumn column;
                if (connectionToColumnLookup.TryGetValue(connection, out column) == false)
                {
                    continue;
                }

                int cellIndex = column.Index;

                foreach (ISettingValue settingValue in connection.Connection.Settings.SendRates.Values)
                {
                    object value = settingValue.GetValue();

                    if (settingValue.HasRemoteValue == false)
                    {
                        value = null;
                    }

                    DataGridViewRow row = settingsToRowLookup[settingValue.OscAddress];

                    row.Cells[cellIndex].Value = value;
                    row.Cells[cellIndex].Tag = settingValue;

                    if (value == null)
                    {
                        continue;
                    }

                    float floatValue = (float) value;
                    float settingRemoteValue = (float) settingValue.GetRemoteValue();

                    row.Cells[cellIndex].Style.ApplyStyle(
                        Math.Abs(floatValue - settingRemoteValue) > float.Epsilon ? 
                            dataGridViewCellStyleBold : 
                            dataGridViewCellStyle);
                }
            }
        }

        private void CopyValuesFromGrid()
        {
            foreach (ConnectionRow connection in ActiveConnections)
            {
                DataGridViewSettingsValueColumn column;
                if (connectionToColumnLookup.TryGetValue(connection, out column) == false)
                {
                    continue;
                }

                int cellIndex = column.Index;

                foreach (ISettingValue settingValue in connection.Connection.Settings.SendRates.Values)
                {
                    object value = settingsToRowLookup[settingValue.OscAddress].Cells[cellIndex].Value;

                    float floatValue = 0f;

                    if (value == null || float.TryParse(value.ToString(), out floatValue) == false)
                    {
                        floatValue = (float)settingValue.GetValue();
                    }

                    settingValue.SetValue(floatValue);
                }
            }
        }

        private void SendRates_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                readAllButton_Click(sender, e);
            }

            if (e.KeyCode == Keys.F6)
            {
                writeAllButton_Click(sender, e);
            }
        }

        private void sendRatesGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            selectedCells.Clear();

            foreach (DataGridViewCell cell in sendRatesGrid.SelectedCells)
            {
                selectedCells.Add(cell);
            }
        }

        private void sendRatesGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = sendRatesGrid.Rows[e.RowIndex];

            DataGridViewSettingsValueColumn column = sendRatesGrid.Columns[e.ColumnIndex] as DataGridViewSettingsValueColumn;

            object value = row.Cells[e.ColumnIndex].Value;

            foreach (DataGridViewCell cell in selectedCells)
            {
                ISettingValue settingValue = (ISettingValue)cell.Tag;

                if (settingValue == null)
                {
                    continue;
                }

                cell.Value = value;

                float floatValue = 0f;

                if (value == null || float.TryParse(value.ToString(), out floatValue) == false)
                {
                    floatValue = (float)settingValue.GetValue();
                }

                float settingRemoteValue = (float)settingValue.GetRemoteValue();

                if (Math.Abs(floatValue - settingRemoteValue) > float.Epsilon)
                {
                    cell.Style.ApplyStyle(dataGridViewCellStyleBold);
                }
                else
                {
                    cell.Style.ApplyStyle(dataGridViewCellStyle);
                }
            }
        }
    }
}
