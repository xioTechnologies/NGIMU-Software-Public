namespace NgimuGui
{
    partial class Form3DView
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

        #region m_Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.modelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imuBoardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imuHousingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadOBJToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wigitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imuModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.earthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imuAxesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eulerAnglesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modelToolStripMenuItem,
            this.wigitsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(584, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // modelToolStripMenuItem
            // 
            this.modelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imuHousingToolStripMenuItem,
            this.imuBoardToolStripMenuItem,
            this.loadOBJToolStripMenuItem});
            this.modelToolStripMenuItem.Name = "modelToolStripMenuItem";
            this.modelToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.modelToolStripMenuItem.Text = "Model";
            // 
            // imuBoardToolStripMenuItem
            // 
            this.imuBoardToolStripMenuItem.Name = "imuBoardToolStripMenuItem";
            this.imuBoardToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.imuBoardToolStripMenuItem.Text = "IMU Board";
            this.imuBoardToolStripMenuItem.Click += new System.EventHandler(this.IMUBoardToolStripMenuItem_Click);
            // 
            // imuHousingToolStripMenuItem
            // 
            this.imuHousingToolStripMenuItem.Checked = true;
            this.imuHousingToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.imuHousingToolStripMenuItem.Name = "imuHousingToolStripMenuItem";
            this.imuHousingToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.imuHousingToolStripMenuItem.Text = "IMU Housing";
            this.imuHousingToolStripMenuItem.Click += new System.EventHandler(this.IMUHousingToolStripMenuItem_Click);
            // 
            // loadOBJToolStripMenuItem
            // 
            this.loadOBJToolStripMenuItem.Name = "loadOBJToolStripMenuItem";
            this.loadOBJToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadOBJToolStripMenuItem.Text = "Custom";
            this.loadOBJToolStripMenuItem.Click += new System.EventHandler(this.loadOBJToolStripMenuItem_Click);
            // 
            // wigitsToolStripMenuItem
            // 
            this.wigitsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imuModelToolStripMenuItem,
            this.earthToolStripMenuItem,
            this.imuAxesToolStripMenuItem,
            this.eulerAnglesToolStripMenuItem});
            this.wigitsToolStripMenuItem.Name = "wigitsToolStripMenuItem";
            this.wigitsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.wigitsToolStripMenuItem.Text = "View";
            // 
            // imuModelToolStripMenuItem
            // 
            this.imuModelToolStripMenuItem.Checked = true;
            this.imuModelToolStripMenuItem.CheckOnClick = true;
            this.imuModelToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.imuModelToolStripMenuItem.Name = "imuModelToolStripMenuItem";
            this.imuModelToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.imuModelToolStripMenuItem.Text = "Model";
            // 
            // earthToolStripMenuItem
            // 
            this.earthToolStripMenuItem.Checked = true;
            this.earthToolStripMenuItem.CheckOnClick = true;
            this.earthToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.earthToolStripMenuItem.Name = "earthToolStripMenuItem";
            this.earthToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.earthToolStripMenuItem.Text = "Earth";
            // 
            // imuAxesToolStripMenuItem
            // 
            this.imuAxesToolStripMenuItem.Checked = true;
            this.imuAxesToolStripMenuItem.CheckOnClick = true;
            this.imuAxesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.imuAxesToolStripMenuItem.Name = "imuAxesToolStripMenuItem";
            this.imuAxesToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.imuAxesToolStripMenuItem.Text = "XYZ Axes";
            // 
            // eulerAnglesToolStripMenuItem
            // 
            this.eulerAnglesToolStripMenuItem.Checked = true;
            this.eulerAnglesToolStripMenuItem.CheckOnClick = true;
            this.eulerAnglesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.eulerAnglesToolStripMenuItem.Name = "eulerAnglesToolStripMenuItem";
            this.eulerAnglesToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.eulerAnglesToolStripMenuItem.Text = "Euler Angles";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Wavefront Object Files|*.obj";
            this.openFileDialog1.Title = "Select Wavefront Object FIle";
            // 
            // Form3DView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 362);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(320, 240);
            this.Name = "Form3DView";
            this.Text = "3D View";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form3DView_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form3DView_FormClosed);
            this.Load += new System.EventHandler(this.Form3DView_Load);
            this.ResizeBegin += new System.EventHandler(this.Form_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.Form_ResizeEnd);
            this.VisibleChanged += new System.EventHandler(this.Form3DView_VisibleChanged);
            this.Resize += new System.EventHandler(this.Form_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem modelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadOBJToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wigitsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imuModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imuAxesToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem imuBoardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imuHousingToolStripMenuItem;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.ToolStripMenuItem earthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eulerAnglesToolStripMenuItem;
    }
}