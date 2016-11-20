namespace NgimuGui.Panels
{
    partial class SerialTerminal
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
            this.m_SendMessageBox = new System.Windows.Forms.ComboBox();
            this.consoleControl1 = new Rug.Forms.Console.ConsoleControl();
            this.SuspendLayout();
            // 
            // m_SendMessageBox
            // 
            this.m_SendMessageBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.m_SendMessageBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_SendMessageBox.FormattingEnabled = true;
            this.m_SendMessageBox.Location = new System.Drawing.Point(0, 425);
            this.m_SendMessageBox.Name = "m_SendMessageBox";
            this.m_SendMessageBox.Size = new System.Drawing.Size(565, 21);
            this.m_SendMessageBox.TabIndex = 1;
            this.m_SendMessageBox.SelectedIndexChanged += new System.EventHandler(this.SendMessageBox_SelectedIndexChanged);
            this.m_SendMessageBox.TextChanged += new System.EventHandler(this.SendMessageBox_TextChanged);
            this.m_SendMessageBox.Enter += new System.EventHandler(this.SendMessageBox_Enter);
            this.m_SendMessageBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SendMessageBox_KeyDown);
            this.m_SendMessageBox.Leave += new System.EventHandler(this.SendMessageBox_Leave);
            // 
            // consoleControl1
            // 
            this.consoleControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.consoleControl1.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consoleControl1.Input = null;
            this.consoleControl1.Location = new System.Drawing.Point(0, 0);
            this.consoleControl1.Name = "consoleControl1";
            this.consoleControl1.Size = new System.Drawing.Size(565, 425);
            this.consoleControl1.TabIndex = 0;
            this.consoleControl1.Text = "consoleControl1";
            this.consoleControl1.UpdateInterval = 10;
            this.consoleControl1.WordWrap = false;
            // 
            // SerialTerminal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.consoleControl1);
            this.Controls.Add(this.m_SendMessageBox);
            this.Name = "SerialTerminal";
            this.Size = new System.Drawing.Size(565, 446);
            this.Load += new System.EventHandler(this.Terminal_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Rug.Forms.Console.ConsoleControl consoleControl1;
        private System.Windows.Forms.ComboBox m_SendMessageBox;
    }
}
