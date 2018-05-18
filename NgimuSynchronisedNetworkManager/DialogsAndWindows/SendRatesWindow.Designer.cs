namespace NgimuSynchronisedNetworkManager.DialogsAndWindows
{
    partial class SendRatesWindow
    {
        /// <summary>
        /// Required designer variable.
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sendRatesGrid = new System.Windows.Forms.DataGridView();
            this.SettingName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.writeButton = new System.Windows.Forms.Button();
            this.readButton = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.sendRatesGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // sendRatesGrid
            // 
            this.sendRatesGrid.AllowUserToAddRows = false;
            this.sendRatesGrid.AllowUserToDeleteRows = false;
            this.sendRatesGrid.AllowUserToResizeColumns = false;
            this.sendRatesGrid.AllowUserToResizeRows = false;
            this.sendRatesGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sendRatesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sendRatesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SettingName});
            this.sendRatesGrid.Location = new System.Drawing.Point(13, 13);
            this.sendRatesGrid.Name = "sendRatesGrid";
            this.sendRatesGrid.RowHeadersVisible = false;
            this.sendRatesGrid.Size = new System.Drawing.Size(599, 327);
            this.sendRatesGrid.TabIndex = 0;
            this.sendRatesGrid.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.sendRatesGrid_CellBeginEdit);
            this.sendRatesGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.sendRatesGrid_CellEndEdit);
            // 
            // SettingName
            // 
            this.SettingName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SettingName.HeaderText = "Send Rates";
            this.SettingName.MinimumWidth = 120;
            this.SettingName.Name = "SettingName";
            this.SettingName.ReadOnly = true;
            this.SettingName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SettingName.Width = 120;
            // 
            // writeButton
            // 
            this.writeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.writeButton.Enabled = false;
            this.writeButton.Location = new System.Drawing.Point(537, 346);
            this.writeButton.Name = "writeButton";
            this.writeButton.Size = new System.Drawing.Size(75, 23);
            this.writeButton.TabIndex = 4;
            this.writeButton.Text = "Write";
            this.writeButton.UseVisualStyleBackColor = true;
            this.writeButton.Click += new System.EventHandler(this.writeAllButton_Click);
            // 
            // readButton
            // 
            this.readButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.readButton.Enabled = false;
            this.readButton.Location = new System.Drawing.Point(456, 346);
            this.readButton.Name = "readButton";
            this.readButton.Size = new System.Drawing.Size(75, 23);
            this.readButton.TabIndex = 3;
            this.readButton.Text = "Read";
            this.readButton.UseVisualStyleBackColor = true;
            this.readButton.Click += new System.EventHandler(this.readAllButton_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "Device";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 120;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // label
            // 
            this.label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(12, 351);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(188, 13);
            this.label.TabIndex = 5;
            this.label.Text = "Hold Shift or Ctrl to select multiple cells";
            // 
            // SendRatesWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 381);
            this.Controls.Add(this.label);
            this.Controls.Add(this.writeButton);
            this.Controls.Add(this.readButton);
            this.Controls.Add(this.sendRatesGrid);
            this.KeyPreview = true;
            this.Name = "SendRatesWindow";
            this.Text = "Send Rates";
            this.Load += new System.EventHandler(this.SendRates_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SendRates_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.sendRatesGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView sendRatesGrid;
        private System.Windows.Forms.Button writeButton;
        private System.Windows.Forms.Button readButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SettingName;
        private System.Windows.Forms.Label label;
    }
}