namespace NgimuGui.DialogsAndWindows
{
    partial class GraphWindow
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
            this.graph = new Rug.LiteGL.Controls.TimestampGraph();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.horizontalRollToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centerTraceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cursorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.horizontalAutoscaleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.verticalAutoscaleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.horizonatalZoomOutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horizonatalZoomInMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verticalZoomOutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verticalZoomInMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // graph
            // 
            this.graph.AxisLabelFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.graph.AxisValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.graph.AxisValueMinorColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.graph.BackColor = System.Drawing.Color.Black;
            this.graph.ContextMenuStrip = this.contextMenuStrip1;
            this.graph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graph.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.graph.GraphInset = new System.Windows.Forms.Padding(64, 16, 16, 42);
            this.graph.Location = new System.Drawing.Point(0, 0);
            this.graph.LockXMouse = true;
            this.graph.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.graph.Name = "graph";
            this.graph.Size = new System.Drawing.Size(624, 441);
            this.graph.TabIndex = 0;
            this.graph.TabStop = false;
            this.graph.XAxisLabel = "Seconds (0001-01-01 00:00:00.000)";
            this.graph.ZoomFromXMin = true;
            this.graph.UserChangedViewport += new Rug.LiteGL.Controls.GraphViewPortChangedEvent(this.Graph_UserChangedViewport);
            this.graph.ViewReset += new System.EventHandler(this.graph_ViewReset);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetViewToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.toolStripSeparator2,
            this.horizontalRollToolStripMenuItem,
            this.centerTraceToolStripMenuItem,
            this.toolStripSeparator3,
            this.cursorsToolStripMenuItem,
            this.toolStripSeparator5,
            this.horizontalAutoscaleMenuItem,
            this.verticalAutoscaleMenuItem,
            this.toolStripSeparator4,
            this.horizonatalZoomOutMenuItem,
            this.horizonatalZoomInMenuItem,
            this.verticalZoomOutMenuItem,
            this.verticalZoomInMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(256, 270);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // resetViewToolStripMenuItem
            // 
            this.resetViewToolStripMenuItem.Name = "resetViewToolStripMenuItem";
            this.resetViewToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            this.resetViewToolStripMenuItem.Text = "Restore Defaults";
            this.resetViewToolStripMenuItem.Click += new System.EventHandler(this.resetViewToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(252, 6);
            // 
            // horizontalRollToolStripMenuItem
            // 
            this.horizontalRollToolStripMenuItem.CheckOnClick = true;
            this.horizontalRollToolStripMenuItem.Name = "horizontalRollToolStripMenuItem";
            this.horizontalRollToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.horizontalRollToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            this.horizontalRollToolStripMenuItem.Text = "Roll Mode";
            this.horizontalRollToolStripMenuItem.CheckedChanged += new System.EventHandler(this.horizontalRollToolStripMenuItem_CheckedChanged);
            // 
            // centerTraceToolStripMenuItem
            // 
            this.centerTraceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem1});
            this.centerTraceToolStripMenuItem.Name = "centerTraceToolStripMenuItem";
            this.centerTraceToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            this.centerTraceToolStripMenuItem.Text = "Center Trace";
            // 
            // allToolStripMenuItem1
            // 
            this.allToolStripMenuItem1.Name = "allToolStripMenuItem1";
            this.allToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.D0)));
            this.allToolStripMenuItem1.Size = new System.Drawing.Size(151, 22);
            this.allToolStripMenuItem1.Text = "All";
            this.allToolStripMenuItem1.Click += new System.EventHandler(this.CenterAll_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(252, 6);
            // 
            // cursorsToolStripMenuItem
            // 
            this.cursorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5});
            this.cursorsToolStripMenuItem.Name = "cursorsToolStripMenuItem";
            this.cursorsToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            this.cursorsToolStripMenuItem.Text = "Cursors";
            this.cursorsToolStripMenuItem.Visible = false;
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(111, 22);
            this.toolStripMenuItem2.Text = "All";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(111, 22);
            this.toolStripMenuItem3.Text = "Trace 1";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(111, 22);
            this.toolStripMenuItem4.Text = "Trace 2";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(111, 22);
            this.toolStripMenuItem5.Text = "Trace 3";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(252, 6);
            this.toolStripSeparator5.Visible = false;
            // 
            // horizontalAutoscaleMenuItem
            // 
            this.horizontalAutoscaleMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem2});
            this.horizontalAutoscaleMenuItem.Name = "horizontalAutoscaleMenuItem";
            this.horizontalAutoscaleMenuItem.Size = new System.Drawing.Size(255, 22);
            this.horizontalAutoscaleMenuItem.Text = "Horizontal Autoscale";
            // 
            // allToolStripMenuItem2
            // 
            this.allToolStripMenuItem2.Name = "allToolStripMenuItem2";
            this.allToolStripMenuItem2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D0)));
            this.allToolStripMenuItem2.Size = new System.Drawing.Size(124, 22);
            this.allToolStripMenuItem2.Text = "All";
            this.allToolStripMenuItem2.Click += new System.EventHandler(this.allHorizontalToolStripMenuItem_Click);
            // 
            // verticalAutoscaleMenuItem
            // 
            this.verticalAutoscaleMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem});
            this.verticalAutoscaleMenuItem.Name = "verticalAutoscaleMenuItem";
            this.verticalAutoscaleMenuItem.Size = new System.Drawing.Size(255, 22);
            this.verticalAutoscaleMenuItem.Text = "Vertical Autoscale";
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            this.allToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)));
            this.allToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.allToolStripMenuItem.Text = "All";
            this.allToolStripMenuItem.Click += new System.EventHandler(this.allVerticalToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(252, 6);
            // 
            // horizonatalZoomOutMenuItem
            // 
            this.horizonatalZoomOutMenuItem.Name = "horizonatalZoomOutMenuItem";
            this.horizonatalZoomOutMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)));
            this.horizonatalZoomOutMenuItem.Size = new System.Drawing.Size(255, 22);
            this.horizonatalZoomOutMenuItem.Text = "Horizonatal Zoom Out";
            this.horizonatalZoomOutMenuItem.Click += new System.EventHandler(this.horizonatalZoomOutMenuItem_Click);
            // 
            // horizonatalZoomInMenuItem
            // 
            this.horizonatalZoomInMenuItem.Name = "horizonatalZoomInMenuItem";
            this.horizonatalZoomInMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)));
            this.horizonatalZoomInMenuItem.Size = new System.Drawing.Size(255, 22);
            this.horizonatalZoomInMenuItem.Text = "Horizonatal Zoom In";
            this.horizonatalZoomInMenuItem.Click += new System.EventHandler(this.horizonatalZoomInMenuItem_Click);
            // 
            // verticalZoomOutMenuItem
            // 
            this.verticalZoomOutMenuItem.Name = "verticalZoomOutMenuItem";
            this.verticalZoomOutMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.verticalZoomOutMenuItem.Size = new System.Drawing.Size(255, 22);
            this.verticalZoomOutMenuItem.Text = "Vertical Zoom Out";
            this.verticalZoomOutMenuItem.Click += new System.EventHandler(this.verticalZoomOutMenuItem_Click);
            // 
            // verticalZoomInMenuItem
            // 
            this.verticalZoomInMenuItem.Name = "verticalZoomInMenuItem";
            this.verticalZoomInMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.verticalZoomInMenuItem.Size = new System.Drawing.Size(255, 22);
            this.verticalZoomInMenuItem.Text = "Vertical Zoom In";
            this.verticalZoomInMenuItem.Click += new System.EventHandler(this.verticalZoomInMenuItem_Click);
            // 
            // GraphWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.graph);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(320, 200);
            this.Name = "GraphWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "GraphWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form3DView_FormClosed);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResizeBegin += new System.EventHandler(this.Form_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.Form_ResizeEnd);
            this.SizeChanged += new System.EventHandler(this.GraphWindow_SizeChanged);
            this.VisibleChanged += new System.EventHandler(this.Form_VisibleChanged);
            this.Resize += new System.EventHandler(this.Form_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Rug.LiteGL.Controls.TimestampGraph graph;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem horizonatalZoomInMenuItem;
        private System.Windows.Forms.ToolStripMenuItem horizonatalZoomOutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verticalZoomInMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verticalZoomOutMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem verticalAutoscaleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem horizontalRollToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem centerTraceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem resetViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cursorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem horizontalAutoscaleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
    }
}