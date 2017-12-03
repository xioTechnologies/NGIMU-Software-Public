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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.resetViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.horizontalAutoscaleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.horizonatalZoomOutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horizonatalZoomInMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horizontalScrollLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horizontalScrollRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.verticalAutoscaleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verticalZoomOutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verticalZoomInMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verticalScrollUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verticalScrollDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graph = new Rug.LiteGL.Controls.TimestampGraph();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetViewToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.pauseToolStripMenuItem,
            this.toolStripSeparator5,
            this.horizontalAutoscaleMenuItem,
            this.horizonatalZoomOutMenuItem,
            this.horizonatalZoomInMenuItem,
            this.horizontalScrollLeftToolStripMenuItem,
            this.horizontalScrollRightToolStripMenuItem,
            this.toolStripSeparator4,
            this.verticalAutoscaleMenuItem,
            this.verticalZoomOutMenuItem,
            this.verticalZoomInMenuItem,
            this.verticalScrollUpToolStripMenuItem,
            this.verticalScrollDownToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(251, 302);
            // 
            // resetViewToolStripMenuItem
            // 
            this.resetViewToolStripMenuItem.Name = "resetViewToolStripMenuItem";
            this.resetViewToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.resetViewToolStripMenuItem.Text = "Restore Defaults";
            this.resetViewToolStripMenuItem.Click += new System.EventHandler(this.resetViewToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.CheckOnClick = true;
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Space)));
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.pauseToolStripMenuItem.Text = "Pause";
            this.pauseToolStripMenuItem.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(247, 6);
            // 
            // horizontalAutoscaleMenuItem
            // 
            this.horizontalAutoscaleMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem2});
            this.horizontalAutoscaleMenuItem.Name = "horizontalAutoscaleMenuItem";
            this.horizontalAutoscaleMenuItem.Size = new System.Drawing.Size(250, 22);
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
            // horizonatalZoomOutMenuItem
            // 
            this.horizonatalZoomOutMenuItem.Name = "horizonatalZoomOutMenuItem";
            this.horizonatalZoomOutMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)));
            this.horizonatalZoomOutMenuItem.Size = new System.Drawing.Size(250, 22);
            this.horizonatalZoomOutMenuItem.Text = "Horizontal Zoom Out";
            this.horizonatalZoomOutMenuItem.Click += new System.EventHandler(this.horizonatalZoomInMenuItem_Click);
            // 
            // horizonatalZoomInMenuItem
            // 
            this.horizonatalZoomInMenuItem.Name = "horizonatalZoomInMenuItem";
            this.horizonatalZoomInMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)));
            this.horizonatalZoomInMenuItem.Size = new System.Drawing.Size(250, 22);
            this.horizonatalZoomInMenuItem.Text = "Horizontal Zoom In";
            this.horizonatalZoomInMenuItem.Click += new System.EventHandler(this.horizonatalZoomOutMenuItem_Click);
            // 
            // horizontalScrollLeftToolStripMenuItem
            // 
            this.horizontalScrollLeftToolStripMenuItem.Enabled = false;
            this.horizontalScrollLeftToolStripMenuItem.Name = "horizontalScrollLeftToolStripMenuItem";
            this.horizontalScrollLeftToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Left)));
            this.horizontalScrollLeftToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.horizontalScrollLeftToolStripMenuItem.Text = "Horizontal Scroll Left";
            this.horizontalScrollLeftToolStripMenuItem.Click += new System.EventHandler(this.horizontalScrollLeftToolStripMenuItem_Click);
            // 
            // horizontalScrollRightToolStripMenuItem
            // 
            this.horizontalScrollRightToolStripMenuItem.Enabled = false;
            this.horizontalScrollRightToolStripMenuItem.Name = "horizontalScrollRightToolStripMenuItem";
            this.horizontalScrollRightToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Right)));
            this.horizontalScrollRightToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.horizontalScrollRightToolStripMenuItem.Text = "Horizontal Scroll Right";
            this.horizontalScrollRightToolStripMenuItem.Click += new System.EventHandler(this.horizontalScrollRightToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(247, 6);
            // 
            // verticalAutoscaleMenuItem
            // 
            this.verticalAutoscaleMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem});
            this.verticalAutoscaleMenuItem.Name = "verticalAutoscaleMenuItem";
            this.verticalAutoscaleMenuItem.Size = new System.Drawing.Size(250, 22);
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
            // verticalZoomOutMenuItem
            // 
            this.verticalZoomOutMenuItem.Name = "verticalZoomOutMenuItem";
            this.verticalZoomOutMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.verticalZoomOutMenuItem.Size = new System.Drawing.Size(250, 22);
            this.verticalZoomOutMenuItem.Text = "Vertical Zoom Out";
            this.verticalZoomOutMenuItem.Click += new System.EventHandler(this.verticalZoomOutMenuItem_Click);
            // 
            // verticalZoomInMenuItem
            // 
            this.verticalZoomInMenuItem.Name = "verticalZoomInMenuItem";
            this.verticalZoomInMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.verticalZoomInMenuItem.Size = new System.Drawing.Size(250, 22);
            this.verticalZoomInMenuItem.Text = "Vertical Zoom In";
            this.verticalZoomInMenuItem.Click += new System.EventHandler(this.verticalZoomInMenuItem_Click);
            // 
            // verticalScrollUpToolStripMenuItem
            // 
            this.verticalScrollUpToolStripMenuItem.Name = "verticalScrollUpToolStripMenuItem";
            this.verticalScrollUpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)));
            this.verticalScrollUpToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.verticalScrollUpToolStripMenuItem.Text = "Vertical Scroll Down";
            this.verticalScrollUpToolStripMenuItem.Click += new System.EventHandler(this.verticalScrollUpToolStripMenuItem_Click);
            // 
            // verticalScrollDownToolStripMenuItem
            // 
            this.verticalScrollDownToolStripMenuItem.Name = "verticalScrollDownToolStripMenuItem";
            this.verticalScrollDownToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Up)));
            this.verticalScrollDownToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.verticalScrollDownToolStripMenuItem.Text = "Vertical Scroll Up";
            this.verticalScrollDownToolStripMenuItem.Click += new System.EventHandler(this.verticalScrollDownToolStripMenuItem_Click);
            // 
            // graph
            // 
            this.graph.AxisLabelFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.graph.AxisValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.graph.AxisValueMinorColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.graph.BackColor = System.Drawing.Color.Black;
            this.graph.ContextMenuStrip = this.contextMenuStrip1;
            this.graph.DisableKeys = true;
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
            this.Load += new System.EventHandler(this.Form_Load);
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
        private System.Windows.Forms.ToolStripMenuItem verticalAutoscaleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem horizontalAutoscaleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem horizontalScrollLeftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem horizontalScrollRightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verticalScrollUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verticalScrollDownToolStripMenuItem;
    }
}