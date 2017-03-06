using System;
using System.Collections.Generic;
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
        private readonly Dictionary<Connection, DataGridViewSettingsValueColumn> connectionToColumnLookup = new Dictionary<Connection, DataGridViewSettingsValueColumn>();

        private readonly DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle() { NullValue = "Unknown" };

        public List<Connection> ActiveConnections { get; } = new List<Connection>();

        public SendRatesWindow()
        {
            InitializeComponent();
        }

        public void UpdateColumns()
        {
            foreach (DataGridViewSettingsValueColumn column in connectionToColumnLookup.Values)
            {
                sendRatesGrid.Columns.Remove(column);
            }

            connectionToColumnLookup.Clear();

            foreach (Connection connection in ActiveConnections)
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(connection.Settings.DeviceInformation.DeviceName.Value);
                sb.Append(connection.Settings.DeviceInformation.SerialNumber.Value);

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

            CopyValuesToGrid();
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
            this.ReadSettings(ActiveConnections.Select(connection => connection.Settings.SendRates));

            CopyValuesToGrid();
        }

        private void writeAllButton_Click(object sender, EventArgs e)
        {
            CopyValuesFromGrid();

            this.WriteSettings(ActiveConnections.Select(connection => connection.Settings.SendRates));

            CopyValuesToGrid();
        }

        private void CopyValuesToGrid()
        {
            foreach (Connection connection in ActiveConnections)
            {
                DataGridViewSettingsValueColumn column;
                if (connectionToColumnLookup.TryGetValue(connection, out column) == false)
                {
                    continue;
                }

                int cellIndex = column.Index; 

                foreach (ISettingValue settingValue in connection.Settings.SendRates.Values)
                {
                    object value = settingValue.GetValue();

                    if (settingValue.HasRemoteValue == false)
                    {
                        value = null;
                    }

                    DataGridViewRow row = settingsToRowLookup[settingValue.OscAddress];

                    row.Cells[cellIndex].Value = value;
                }
            }
        }

        private void CopyValuesFromGrid()
        {
            foreach (Connection connection in ActiveConnections)
            {
                DataGridViewSettingsValueColumn column;
                if (connectionToColumnLookup.TryGetValue(connection, out column) == false)
                {
                    continue;
                }

                int cellIndex = column.Index;

                foreach (ISettingValue settingValue in connection.Settings.SendRates.Values)
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
    }
}
