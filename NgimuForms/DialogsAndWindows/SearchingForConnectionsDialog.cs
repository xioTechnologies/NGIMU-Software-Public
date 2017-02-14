using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NgimuApi.SearchForConnections;

namespace NgimuForms.DialogsAndWindows
{
    public partial class SearchingForConnectionsDialog : BaseForm
    {
        private static readonly string[] ellipseAnimationFrames = new string[]
        {
            "    ",
            ".   ",
            "..  ",
            "... ",
            " ...",
            "  ..",
            "   .",
            "    "
        };

        private SearchForConnections autoConnector;
        private int ellipseAnimationFrameIndex = 0;
        private int numberOfSerialConnections, numberOfUdpConnections;

        public List<ConnectionSearchResult> ConnectionSearchResults { get; } = new List<ConnectionSearchResult>();

        public ConnectionSearchTypes ConnectionSearchType { get; set; } = ConnectionSearchTypes.All;

        public bool OpenConnectionToFirstDeviceFound { get; set; } = false;

        public bool AllowMultipleConnections { get; set; } = false;

        public SearchingForConnectionsDialog()
        {
            InitializeComponent();
        }

        private void AutoConnector_DeviceDiscovered(ConnectionSearchResult connectionSearchResult)
        {
            if (InvokeRequired == true)
            {
                BeginInvoke(new Action<ConnectionSearchResult>(OnDeviceDiscovered), connectionSearchResult);
            }
            else
            {
                OnDeviceDiscovered(connectionSearchResult);
            }
        }

        private void AutoConnector_DeviceExpired(ConnectionSearchResult connectionSearchResult)
        {
            if (InvokeRequired == true)
            {
                BeginInvoke(new Action<ConnectionSearchResult>(OnDeviceExpired), connectionSearchResult);
            }
            else
            {
                OnDeviceExpired(connectionSearchResult);
            }
        }

        private void SearchingForConnectionsDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            // make sure you disconnect the event to avoid erroneous rows getting added to the disposed grid view  
            autoConnector.DeviceDiscovered -= AutoConnector_DeviceDiscovered;
            autoConnector.DeviceExpired -= AutoConnector_DeviceExpired;

            ellipseAnimationTimer_Tick(null, EventArgs.Empty);

            autoConnector.Dispose();
        }

        private void SearchingForConnectionsDialog_Load(object sender, EventArgs e)
        {
            SetNumberOfConnectionsText();

            Selected.Visible = AllowMultipleConnections;

            autoConnector = new SearchForConnections(ConnectionSearchType);

            autoConnector.DeviceDiscovered += AutoConnector_DeviceDiscovered;
            autoConnector.DeviceExpired += AutoConnector_DeviceExpired;

            ellipseAnimationTimer.Enabled = true;

            autoConnector.BeginSearch();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;

            Close();
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            List<ConnectionSearchResult> connectionSearchResults = new List<ConnectionSearchResult>();

            foreach (DataGridViewRow row in m_Connections.Rows)
            {
                if ((bool)row.Cells[0].Value == false)
                {
                    continue;
                }

                connectionSearchResults.Add(row.Tag as ConnectionSearchResult);
            }

            if (connectionSearchResults.Count == 0)
            {
                System.Media.SystemSounds.Exclamation.Play();

                FlashingDialogHelper.FlashWindowEx(this);

                return;
            }

            ConnectionSearchResults.Clear();
            ConnectionSearchResults.AddRange(connectionSearchResults);

            DialogResult = System.Windows.Forms.DialogResult.OK;

            Close();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter ||
                e.KeyCode == Keys.Return)
            {
                Connect_Click(sender, EventArgs.Empty);
            }
        }

        private void ellipseAnimationTimer_Tick(object sender, EventArgs e)
        {
            ellipseAnimationFrameIndex = ++ellipseAnimationFrameIndex % ellipseAnimationFrames.Length;

            Text = "Searching For Connections " + ellipseAnimationFrames[ellipseAnimationFrameIndex];
        }

        private void Connections_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            Connect_Click(sender, EventArgs.Empty);
        }

        private void OnDeviceDiscovered(ConnectionSearchResult connectionSearchResult)
        {
            DataGridViewRow row = new DataGridViewRow() { Tag = connectionSearchResult };

            row.CreateCells(m_Connections, AllowMultipleConnections, connectionSearchResult.DeviceDescriptor, connectionSearchResult.ConnectionInfo.ToString());

            m_Connections.Rows.Add(row);

            if (m_Connections.Rows.Count == 1)
            {
                m_Connections.ClearSelection();
                row.Selected = true;
            }

            if (connectionSearchResult.ConnectionType == NgimuApi.ConnectionType.Udp)
            {
                numberOfUdpConnections++;
            }
            else
            {
                numberOfSerialConnections++;
            }

            SetNumberOfConnectionsText();

            if (OpenConnectionToFirstDeviceFound == true)
            {
                Connect_Click(null, EventArgs.Empty);
            }
        }

        private void m_Connections_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != Selected.Index)
            {
                return;
            }

            if (e.RowIndex < 0)
            {
                return;
            }

            m_Connections.Rows[e.RowIndex].Cells[0].Value = !(bool)m_Connections.Rows[e.RowIndex].Cells[0].Value;
        }

        private void OnDeviceExpired(ConnectionSearchResult connectionSearchResult)
        {
            DataGridViewRow foundRow = null;

            foreach (DataGridViewRow row in m_Connections.Rows)
            {
                if (connectionSearchResult.Equals(row.Tag as ConnectionSearchResult) == false)
                {
                    continue;
                }

                foundRow = row;
            }

            if (foundRow == null)
            {
                return;
            }

            ConnectionSearchResults.Remove(connectionSearchResult);

            m_Connections.Rows.Remove(foundRow);

            if (foundRow.Selected == true)
            {
                if (m_Connections.Rows.Count > 0)
                {
                    m_Connections.ClearSelection();
                    m_Connections.Rows[0].Selected = true;
                }
            }

            if (connectionSearchResult.ConnectionType == NgimuApi.ConnectionType.Udp)
            {
                numberOfUdpConnections--;
            }
            else
            {
                numberOfSerialConnections--;
            }

            SetNumberOfConnectionsText();
        }

        private void m_Connections_SelectionChanged(object sender, EventArgs e)
        {
            if (AllowMultipleConnections == true)
            {
                return;
            }

            DataGridViewRow selectedRow = m_Connections.SelectedRows.Count > 0 ? m_Connections.SelectedRows[0] : null;

            // uncheck all and check the selected row 
            foreach (DataGridViewRow row in m_Connections.Rows)
            {
                row.Cells[Selected.Index].Value = row == selectedRow;
            }
        }

        private void SetNumberOfConnectionsText()
        {
            switch (ConnectionSearchType)
            {
                case ConnectionSearchTypes.None:
                    numberOfConnectionsLabel.Text = string.Empty;
                    break;
                case ConnectionSearchTypes.Udp:
                    numberOfConnectionsLabel.Text = $"Found {numberOfUdpConnections} UDP connections";
                    break;
                case ConnectionSearchTypes.Serial:
                    numberOfConnectionsLabel.Text = $"Found {numberOfSerialConnections} serial connections";
                    break;
                case ConnectionSearchTypes.All:
                    numberOfConnectionsLabel.Text = $"Found {numberOfSerialConnections} serial connections and {numberOfUdpConnections} UDP connections";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}