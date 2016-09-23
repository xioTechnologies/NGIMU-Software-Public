namespace NgimuGui.DialogsAndWindows
{
    partial class SearchingForConnectionsDialog
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
            this.components = new System.ComponentModel.Container();
            this.m_CancelButton = new System.Windows.Forms.Button();
            this.m_Connect = new System.Windows.Forms.Button();
            this.m_Connections = new System.Windows.Forms.DataGridView();
            this.Device = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Connection = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numberOfConnectionsLabel = new System.Windows.Forms.Label();
            this.ellipseAnimationTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.m_Connections)).BeginInit();
            this.SuspendLayout();
            // 
            // m_CancelButton
            // 
            this.m_CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_CancelButton.Location = new System.Drawing.Point(377, 239);
            this.m_CancelButton.Name = "m_CancelButton";
            this.m_CancelButton.Size = new System.Drawing.Size(75, 23);
            this.m_CancelButton.TabIndex = 2;
            this.m_CancelButton.Text = "Cancel";
            this.m_CancelButton.UseVisualStyleBackColor = true;
            this.m_CancelButton.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // m_Connect
            // 
            this.m_Connect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_Connect.Location = new System.Drawing.Point(296, 239);
            this.m_Connect.Name = "m_Connect";
            this.m_Connect.Size = new System.Drawing.Size(75, 23);
            this.m_Connect.TabIndex = 1;
            this.m_Connect.Text = "Connect";
            this.m_Connect.UseVisualStyleBackColor = true;
            this.m_Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // m_Connections
            // 
            this.m_Connections.AllowUserToAddRows = false;
            this.m_Connections.AllowUserToDeleteRows = false;
            this.m_Connections.AllowUserToResizeRows = false;
            this.m_Connections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_Connections.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_Connections.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Device,
            this.Connection});
            this.m_Connections.Location = new System.Drawing.Point(12, 12);
            this.m_Connections.MultiSelect = false;
            this.m_Connections.Name = "m_Connections";
            this.m_Connections.ReadOnly = true;
            this.m_Connections.RowHeadersVisible = false;
            this.m_Connections.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.m_Connections.ShowCellErrors = false;
            this.m_Connections.ShowCellToolTips = false;
            this.m_Connections.ShowEditingIcon = false;
            this.m_Connections.ShowRowErrors = false;
            this.m_Connections.Size = new System.Drawing.Size(440, 221);
            this.m_Connections.TabIndex = 0;
            this.m_Connections.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Connections_CellMouseDoubleClick);
            this.m_Connections.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // Device
            // 
            this.Device.FillWeight = 1F;
            this.Device.HeaderText = "Device";
            this.Device.MinimumWidth = 100;
            this.Device.Name = "Device";
            this.Device.ReadOnly = true;
            this.Device.Width = 175;
            // 
            // Connection
            // 
            this.Connection.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Connection.HeaderText = "Connection";
            this.Connection.Name = "Connection";
            this.Connection.ReadOnly = true;
            // 
            // numberOfConnectionsLabel
            // 
            this.numberOfConnectionsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numberOfConnectionsLabel.AutoSize = true;
            this.numberOfConnectionsLabel.Location = new System.Drawing.Point(12, 244);
            this.numberOfConnectionsLabel.Name = "numberOfConnectionsLabel";
            this.numberOfConnectionsLabel.Size = new System.Drawing.Size(251, 13);
            this.numberOfConnectionsLabel.TabIndex = 5;
            this.numberOfConnectionsLabel.Tag = "Found {0} serial connections and {1} UDP connections";
            this.numberOfConnectionsLabel.Text = "Found 0 serial connections and 0 UDP connections";
            // 
            // ellipseAnimationTimer
            // 
            this.ellipseAnimationTimer.Tick += new System.EventHandler(this.ellipseAnimationTimer_Tick);
            // 
            // SearchingForConnectionsDialog
            // 
            this.AcceptButton = this.m_Connect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_CancelButton;
            this.ClientSize = new System.Drawing.Size(464, 274);
            this.Controls.Add(this.numberOfConnectionsLabel);
            this.Controls.Add(this.m_Connections);
            this.Controls.Add(this.m_Connect);
            this.Controls.Add(this.m_CancelButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(480, 137);
            this.Name = "SearchingForConnectionsDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Available Connections";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SearchingForConnectionsDialog_FormClosing);
            this.Load += new System.EventHandler(this.SearchingForConnectionsDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_Connections)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button m_CancelButton;
        private System.Windows.Forms.Button m_Connect;
        private System.Windows.Forms.DataGridView m_Connections;
        private System.Windows.Forms.DataGridViewTextBoxColumn Device;
        private System.Windows.Forms.DataGridViewTextBoxColumn Connection;
        private System.Windows.Forms.Label numberOfConnectionsLabel;
        private System.Windows.Forms.Timer ellipseAnimationTimer;
    }
}