namespace NgimuGui.Panels
{
    partial class ClockPanel
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
            this.SuspendLayout();
            // 
            // ClockPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Name = "ClockPanel";
            this.Size = new System.Drawing.Size(287, 107);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ClockPanel_Paint);
            this.Resize += new System.EventHandler(this.ClockPanel_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
