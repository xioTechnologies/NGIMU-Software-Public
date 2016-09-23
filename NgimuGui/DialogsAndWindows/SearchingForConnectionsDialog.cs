using System;
using System.Windows.Forms;
using NgimuApi.SearchForConnections;

namespace NgimuGui.DialogsAndWindows
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

        public ConnectionSearchInfo Info { get; private set; }

        public SearchingForConnectionsDialog()
        {
            InitializeComponent();
        }

        private void AutoConnector_DeviceDiscovered(ConnectionSearchInfo obj)
        {
            if (InvokeRequired == true)
            {
                BeginInvoke(new Action<ConnectionSearchInfo>(OnDeviceDiscovered), obj);
            }
            else
            {
                OnDeviceDiscovered(obj);
            }
        }


        private void AutoConnector_DeviceExpired(ConnectionSearchInfo obj)
        {
            if (InvokeRequired == true)
            {
                BeginInvoke(new Action<ConnectionSearchInfo>(OnDeviceExpired), obj);
            }
            else
            {
                OnDeviceExpired(obj);
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
            autoConnector = new SearchForConnections(Options.ConnectionSearchType);

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
            if (m_Connections.SelectedRows.Count == 0)
            {
                System.Media.SystemSounds.Exclamation.Play();

                FlashingDialogHelper.FlashWindowEx(this);

                return;
            }

            DataGridViewRow row = m_Connections.SelectedRows[0];

            Info = row.Tag as ConnectionSearchInfo;

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

        private void OnDeviceDiscovered(ConnectionSearchInfo info)
        {
            DataGridViewRow row = new DataGridViewRow() { Tag = info };

            row.CreateCells(m_Connections, info.DeviceDescriptor, info.ConnectionInfo.ToString());

            m_Connections.Rows.Add(row);

            if (m_Connections.Rows.Count == 1)
            {
                m_Connections.ClearSelection();
                m_Connections.Rows[0].Selected = true;
            }

            if (info.ConnectionType == NgimuApi.ConnectionType.Udp)
            {
                numberOfUdpConnections++;
            }
            else
            {
                numberOfSerialConnections++;
            }

            numberOfConnectionsLabel.Text = string.Format(numberOfConnectionsLabel.Tag.ToString(), numberOfSerialConnections, numberOfUdpConnections);

            if (Options.OpenConnectionToFirstDeviceFound == true)
            {
                Connect_Click(null, EventArgs.Empty);
            }
        }

        private void OnDeviceExpired(ConnectionSearchInfo info)
        {
            DataGridViewRow foundRow = null;

            foreach (DataGridViewRow row in m_Connections.Rows)
            {
                if (info.Equals(row.Tag as ConnectionSearchInfo) == false)
                {
                    continue;
                }

                foundRow = row;
            }

            if (foundRow == null)
            {
                return;
            }

            m_Connections.Rows.Remove(foundRow);

            if (foundRow.Selected == true)
            {
                if (m_Connections.Rows.Count > 0)
                {
                    m_Connections.ClearSelection();
                    m_Connections.Rows[0].Selected = true;
                }
            }

            if (info.ConnectionType == NgimuApi.ConnectionType.Udp)
            {
                numberOfUdpConnections--;
            }
            else
            {
                numberOfSerialConnections--;
            }

            numberOfConnectionsLabel.Text = string.Format(numberOfConnectionsLabel.Tag.ToString(), numberOfSerialConnections, numberOfUdpConnections);
        }
    }
}