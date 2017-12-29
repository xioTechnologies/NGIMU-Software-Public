using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Rug.Cmd;
using Rug.Osc;

namespace NgimuGui.Panels
{
    public partial class FilterPanel : UserControl
    {
        private readonly List<string> m_Muted = new List<string>();
        private readonly List<string> m_Soloed = new List<string>();

        private readonly CheckBox MuteAll = new CheckBox();
        private readonly CheckBox SoloAll = new CheckBox();

        private readonly Dictionary<string, AddressInstance> m_AddressLookup = new Dictionary<string, AddressInstance>();

        private readonly ManualResetEvent m_AddNewAddress = new ManualResetEvent(false);
        private DateTime m_LastUpdate;

        public FilterPanel()
        {
            InitializeComponent();
        }

        #region Load Event

        private void FilterPanel_Load(object sender, EventArgs e)
        {
            m_LastUpdate = DateTime.Now;

            SetupCheckBox(MuteAll, "Mute");
            SetupCheckBox(SoloAll, "Solo");

            m_DataView_ColumnWidthChanged(this, null);

            MuteAll.CheckedChanged += MuteAll_CheckedChanged;
            SoloAll.CheckedChanged += SoloAll_CheckedChanged;

            m_DataView.ColumnWidthChanged += m_DataView_ColumnWidthChanged;

            // Add the CheckBox into the DataGridView
            m_DataView.Controls.Add(MuteAll);
            m_DataView.Controls.Add(SoloAll);

            DoubleBuffered = true;
        }

        private void SetupCheckBox(CheckBox checkBox, string name)
        {
            checkBox.AutoEllipsis = true;
            checkBox.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            checkBox.Location = new System.Drawing.Point(299, 195);
            checkBox.Name = name;
            checkBox.Size = new System.Drawing.Size(85, 19);
            checkBox.TabIndex = 1;
            checkBox.Text = name;
            checkBox.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            //checkBox.AutoCheck = false; 
            //checkBox.UseVisualStyleBackColor = true;
        }

        internal void InvalidateAndAlign()
        {
            //MuteAll.Visible = false;
            //SoloAll.Visible = false; 

            Invalidate(true);

            m_DataView_ColumnWidthChanged(this, null);
        }

        void m_DataView_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            AlignCheckBox(MuteAll, 0);
            AlignCheckBox(SoloAll, 1);
        }

        private void AlignCheckBox(CheckBox checkBox, int column)
        {
            // Get the column header cell bounds
            Rectangle rect = this.m_DataView.GetCellDisplayRectangle(column, -1, true);

            checkBox.Size = new Size(Math.Min(rect.Width - 10, 50), 18); //);
            checkBox.Padding = new System.Windows.Forms.Padding(0);
            checkBox.Margin = new System.Windows.Forms.Padding(0);

            // Change the location of the CheckBox to make it stay on the header
            checkBox.Location = new Point(
                rect.X + 2, //((rect.Width - checkBox.Width) / 2) + rect.X + 2,
                ((rect.Height - checkBox.Height) / 2) + rect.Y + 1
                );

            checkBox.UseVisualStyleBackColor = false;
            checkBox.BackColor = Color.Transparent;

            checkBox.Invalidate();
        }

        void MuteAll_CheckedChanged(object sender, EventArgs e)
        {
            List<string> allAddresses = new List<string>(m_AddressLookup.Keys);

            foreach (string address in allAddresses)
            {
                SetMute(address, MuteAll.Checked);
            }
        }

        void SoloAll_CheckedChanged(object sender, EventArgs e)
        {
            List<string> allAddresses = new List<string>(m_AddressLookup.Keys);

            foreach (string address in allAddresses)
            {
                SetSolo(address, SoloAll.Checked);
            }
        }

        #endregion

        public void CalculateRate()
        {
            DateTime date = DateTime.Now;

            TimeSpan span = date - m_LastUpdate;

            foreach (AddressInstance instance in m_AddressLookup.Values)
            {
                instance.CalculateRate(span);
            }

            m_LastUpdate = date;
        }

        #region Add Filter

        public void AddFilter(OscMessage message)
        {
            try
            {
                AddressInstance instance;

                if (m_AddressLookup.TryGetValue(message.Address, out instance) == true)
                {
                    instance.OnMessage(message);
                    return;
                }

                if (this.InvokeRequired == true)
                {
                    m_AddNewAddress.Reset();

                    this.Invoke(new Action<string>(AddFilter), message.Address);

                    m_AddNewAddress.WaitOne();
                }
                else
                {
                    AddFilter(message.Address);
                }
            }
            catch (Exception ex)
            {
                // explicitly tell the user why the address failed to parse
                RC.WriteException(001, "Error parsing OSC message", ex);
            }
        }

        public void AddFilter(string address)
        {
            try
            {
                AddressInstance instance;
                if (m_AddressLookup.TryGetValue(address, out instance) == true)
                {
                    return;
                }

                DataGridViewRow row = m_DataView.Rows[m_DataView.Rows.Add(false, false, address, 0L, 0d)];

                instance = new AddressInstance(address, row);

                m_AddressLookup.Add(address, instance);

                // set initial mute solo state
                SetMute(address, false);
                SetSolo(address, false);
            }
            catch (Exception ex)
            {
                // explicitly tell the user why the address failed to parse
                RC.WriteException(002, "Error parsing OSC address", ex);
            }
            finally
            {
                m_AddNewAddress.Set();
            }
        }

        #endregion

        #region Data Grid Events

        private void m_DataView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            string address = m_DataView.Rows[e.RowIndex].Cells["Address"].Value.ToString();

            DataGridViewColumn column = m_DataView.Columns[e.ColumnIndex];

            if (column.Name == "Mute")
            {
                ToggleMute(address);
            }
            else if (column.Name == "Solo")
            {
                ToggleSolo(address);
            }
        }

        private void SetSolo(string address, bool value)
        {
            AddressInstance instance = m_AddressLookup[address];

            instance.Solo = value;

            if (instance.Solo == true)
            {
                if (m_Soloed.Contains(address) == false)
                {
                    m_Soloed.Add(address);
                }
            }
            else
            {
                m_Soloed.Remove(address);
            }
        }

        private void ToggleSolo(string address)
        {
            AddressInstance instance = m_AddressLookup[address];

            instance.Solo = !instance.Solo;

            if (instance.Solo == true)
            {
                if (m_Soloed.Contains(address) == false)
                {
                    m_Soloed.Add(address);
                }
            }
            else
            {
                m_Soloed.Remove(address);
            }
        }

        private void SetMute(string address, bool value)
        {
            AddressInstance instance = m_AddressLookup[address];

            instance.Mute = value;

            if (instance.Mute == true)
            {
                if (m_Muted.Contains(address) == false)
                {
                    m_Muted.Add(address);
                }
            }
            else
            {
                m_Muted.Remove(address);
            }
        }

        private void ToggleMute(string address)
        {
            AddressInstance instance = m_AddressLookup[address];

            instance.Mute = !instance.Mute;

            if (instance.Mute == true)
            {
                if (m_Muted.Contains(address) == false)
                {
                    m_Muted.Add(address);
                }
            }
            else
            {
                m_Muted.Remove(address);
            }
        }

        private void DataView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            string address = e.Row.Cells["Address"].Value.ToString();

            Delete(address);
        }

        private void Delete(string address)
        {
            AddressInstance instance = m_AddressLookup[address];

            m_Muted.Remove(address);
            m_Soloed.Remove(address);

            m_AddressLookup.Remove(address);
        }

        #endregion

        internal void Clear()
        {
            m_DataView.Rows.Clear();

            List<string> allAddresses = new List<string>(m_AddressLookup.Keys);

            foreach (string address in allAddresses)
            {
                Delete(address);
            }
        }

        internal bool ShouldWriteMessage(string address)
        {
            if (m_Soloed.Count > 0)
            {
                return m_Soloed.Contains(address);
            }
            else if (m_Muted.Count > 0 && m_Muted.Contains(address) == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
