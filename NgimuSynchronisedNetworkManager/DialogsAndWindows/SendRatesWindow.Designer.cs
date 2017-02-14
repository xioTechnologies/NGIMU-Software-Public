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
            this.writeButton = new System.Windows.Forms.Button();
            this.readButton = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Device = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.sendRatesGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // sendRatesGrid
            // 
            this.sendRatesGrid.AllowUserToAddRows = false;
            this.sendRatesGrid.AllowUserToDeleteRows = false;
            this.sendRatesGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sendRatesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sendRatesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Device});
            this.sendRatesGrid.Location = new System.Drawing.Point(13, 13);
            this.sendRatesGrid.Name = "sendRatesGrid";
            this.sendRatesGrid.RowHeadersVisible = false;
            this.sendRatesGrid.Size = new System.Drawing.Size(599, 327);
            this.sendRatesGrid.TabIndex = 0;
            // 
            // writeButton
            // 
            this.writeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.writeButton.Location = new System.Drawing.Point(517, 346);
            this.writeButton.Name = "writeButton";
            this.writeButton.Size = new System.Drawing.Size(95, 23);
            this.writeButton.TabIndex = 4;
            this.writeButton.Text = "Write All (F6)";
            this.writeButton.UseVisualStyleBackColor = true;
            this.writeButton.Click += new System.EventHandler(this.writeAllButton_Click);
            // 
            // readButton
            // 
            this.readButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.readButton.Location = new System.Drawing.Point(416, 346);
            this.readButton.Name = "readButton";
            this.readButton.Size = new System.Drawing.Size(95, 23);
            this.readButton.TabIndex = 3;
            this.readButton.Text = "Read All (F5)";
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
            // Device
            // 
            this.Device.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Device.HeaderText = "Device";
            this.Device.MinimumWidth = 120;
            this.Device.Name = "Device";
            this.Device.ReadOnly = true;
            // 
            // SendRates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 381);
            this.Controls.Add(this.writeButton);
            this.Controls.Add(this.readButton);
            this.Controls.Add(this.sendRatesGrid);
            this.KeyPreview = true;
            this.Name = "SendRates";
            this.Text = "Send Rates";
            this.Load += new System.EventHandler(this.SendRates_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SendRates_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.sendRatesGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView sendRatesGrid;
        private System.Windows.Forms.Button writeButton;
        private System.Windows.Forms.Button readButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Device;
    }
}