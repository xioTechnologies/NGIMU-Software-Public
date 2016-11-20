namespace NgimuGui.Panels
{
    partial class FilterPanel
    {
        /// <summary> 
        /// Required designer command.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.m_DataView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mute = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Solo = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Average = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.m_DataView)).BeginInit();
            this.SuspendLayout();
            // 
            // m_DataView
            // 
            this.m_DataView.AllowUserToAddRows = false;
            this.m_DataView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.m_DataView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.m_DataView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.m_DataView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.m_DataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_DataView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Mute,
            this.Solo,
            this.Address,
            this.Total,
            this.Average});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.m_DataView.DefaultCellStyle = dataGridViewCellStyle4;
            this.m_DataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_DataView.Location = new System.Drawing.Point(0, 0);
            this.m_DataView.Name = "m_DataView";
            this.m_DataView.RowHeadersVisible = false;
            this.m_DataView.RowTemplate.ReadOnly = true;
            this.m_DataView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.m_DataView.Size = new System.Drawing.Size(578, 420);
            this.m_DataView.TabIndex = 6;
            this.m_DataView.TabStop = false;
            this.m_DataView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.m_DataView_CellContentClick);
            this.m_DataView.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.DataView_UserDeletingRow);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Address";
            this.dataGridViewTextBoxColumn1.FillWeight = 19.35645F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Address";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 288;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Count";
            this.dataGridViewTextBoxColumn2.HeaderText = "Count";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 80;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 80;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Average";
            this.dataGridViewTextBoxColumn3.FillWeight = 259.3763F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Av.";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 67;
            // 
            // Mute
            // 
            this.Mute.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Mute.FillWeight = 1F;
            this.Mute.HeaderText = " ";
            this.Mute.MinimumWidth = 40;
            this.Mute.Name = "Mute";
            this.Mute.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Mute.ToolTipText = "Mute";
            this.Mute.Width = 60;
            // 
            // Solo
            // 
            this.Solo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Solo.FillWeight = 1F;
            this.Solo.HeaderText = " ";
            this.Solo.MinimumWidth = 40;
            this.Solo.Name = "Solo";
            this.Solo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Solo.ToolTipText = "Solo";
            this.Solo.Width = 60;
            // 
            // Address
            // 
            this.Address.DataPropertyName = "Address";
            this.Address.HeaderText = "Address";
            this.Address.Name = "Address";
            this.Address.ReadOnly = true;
            // 
            // Total
            // 
            this.Total.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Total.DataPropertyName = "Total";
            this.Total.FillWeight = 1F;
            this.Total.HeaderText = "Total";
            this.Total.MinimumWidth = 80;
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            this.Total.Width = 80;
            // 
            // Average
            // 
            this.Average.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Average.DataPropertyName = "Rate";
            dataGridViewCellStyle3.Format = "F0";
            dataGridViewCellStyle3.NullValue = null;
            this.Average.DefaultCellStyle = dataGridViewCellStyle3;
            this.Average.FillWeight = 1F;
            this.Average.HeaderText = "Rate";
            this.Average.MinimumWidth = 80;
            this.Average.Name = "Average";
            this.Average.ReadOnly = true;
            this.Average.Width = 80;
            // 
            // FilterPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_DataView);
            this.Name = "FilterPanel";
            this.Size = new System.Drawing.Size(578, 420);
            this.Load += new System.EventHandler(this.FilterPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_DataView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView m_DataView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Mute;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Solo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn Average;
    }
}
