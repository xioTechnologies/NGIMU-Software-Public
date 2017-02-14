using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NgimuApi;
using NgimuForms.DialogsAndWindows;
using NgimuSynchronisedNetworkManager.Controls;

namespace NgimuSynchronisedNetworkManager.DialogsAndWindows
{
    public partial class SendRatesWindow : BaseForm
    {
        private readonly Settings settings = new Settings();
        private readonly object[] rowTemplate = null;

        private readonly Dictionary<string, DataGridViewSettingsValueColumn> settingsToColumnLookup = new Dictionary<string, DataGridViewSettingsValueColumn>();
        private readonly Dictionary<Connection, DataGridViewRow> connectionToRowLookup = new Dictionary<Connection, DataGridViewRow>();

        private System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle() { NullValue = "Unknown" };

        public List<Connection> ActiveConnections { get; } = new List<Connection>();

        public SendRatesWindow()
        {
            InitializeComponent();

            rowTemplate = new object[settings.SendRates.Values.Count() + 1];
        }

        public void UpdateRows()
        {
            sendRatesGrid.Rows.Clear();
            connectionToRowLookup.Clear();

            foreach (Connection connection in ActiveConnections)
            {
                rowTemplate[0] = connection.Settings.GetDeviceDescriptor();

                DataGridViewRow row = sendRatesGrid.Rows[sendRatesGrid.Rows.Add(rowTemplate)];

                row.Tag = connection;

                connectionToRowLookup.Add(connection, row);
            }

            CopyValuesToGrid();
        }

        private void SendRates_Load(object sender, EventArgs e)
        {
            int templateIndex = 0;

            rowTemplate[templateIndex++] = string.Empty;

            foreach (ISettingValue value in settings.SendRates.Values)
            {
                rowTemplate[templateIndex++] = 0f;

                DataGridViewSettingsValueColumn column = new DataGridViewSettingsValueColumn
                {
                    DefaultCellStyle = dataGridViewCellStyle,

                    HeaderText = value.Name,
                    Tag = value.OscAddress,
                    //AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                    Width = 80,
                    MinimumWidth = 80,
                    ValueType = typeof(float),
                };

                settingsToColumnLookup.Add(value.OscAddress, column);

                sendRatesGrid.Columns.Add(column);
            }

            UpdateRows();
        }

        private void readAllButton_Click(object sender, EventArgs e)
        {
            this.ReadSettings(ActiveConnections.SelectMany(connection => connection.Settings.SendRates.Values));

            CopyValuesToGrid();
        }

        private void writeAllButton_Click(object sender, EventArgs e)
        {
            CopyValuesFromGrid();

            this.WriteSettings(ActiveConnections.SelectMany(connection => connection.Settings.SendRates.Values));

            CopyValuesToGrid();
        }

        private void CopyValuesToGrid()
        {
            foreach (Connection connection in ActiveConnections)
            {
                DataGridViewRow row;
                if (connectionToRowLookup.TryGetValue(connection, out row) == false)
                {
                    continue;
                }

                foreach (ISettingValue settingValue in connection.Settings.SendRates.Values)
                {
                    object value = settingValue.GetValue();

                    if (settingValue.HasRemoteValue == false)
                    {
                        value = null;
                    }

                    row.Cells[settingsToColumnLookup[settingValue.OscAddress].Index].Value = value;
                }
            }
        }

        private void CopyValuesFromGrid()
        {
            foreach (Connection connection in ActiveConnections)
            {
                DataGridViewRow row;
                if (connectionToRowLookup.TryGetValue(connection, out row) == false)
                {
                    continue;
                }

                foreach (ISettingValue settingValue in connection.Settings.SendRates.Values)
                {
                    object value = row.Cells[settingsToColumnLookup[settingValue.OscAddress].Index].Value;

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
