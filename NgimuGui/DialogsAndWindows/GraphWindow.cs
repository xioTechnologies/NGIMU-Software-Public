using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Rug.LiteGL.Controls;

namespace NgimuGui.DialogsAndWindows
{
    public partial class GraphWindow : BaseForm
    {
        public readonly string ID;

        private List<ToolStripMenuItem> horizontalTrackItems = new List<ToolStripMenuItem>();
        private Graph.Trace[] traces;
        private List<ToolStripMenuItem> verticalTrackItems = new List<ToolStripMenuItem>();

        public GraphWindow(string ident, string yAxisLabel, AxesRange axesRange, bool showLegend, params Graph.Trace[] traces)
        {
            ID = ident;

            InitializeComponent();

            Name = ident;
            Text = ident;

            graph.ShowLegend = showLegend;
            graph.AxesRange = axesRange;
            graph.YAxisLabel = yAxisLabel;

            graph.AddTraces(traces);

            this.traces = traces;
        }

        public void AddData(DateTime timestamp, int index, float beam)
        {
            graph.AddScopeData(timestamp, index, beam);
        }

        public void AddData(DateTime timestamp, params float[] beams)
        {
            graph.AddScopeData(timestamp, beams);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((keyData & Keys.Control) == Keys.Control && (keyData & Keys.Alt) == Keys.Alt)
            {
                for (int i = 0; i < traces.Length + 1; i++)
                {
                    if ((keyData & (Keys)((int)Keys.D0 + i)) == (Keys)((int)Keys.D0 + i))
                    {
                        CenterOnTrace(i - 1);
                    }
                }
            }
            else if ((keyData & Keys.Control) == Keys.Control)
            {
                if ((keyData & Keys.Back) == Keys.Back)
                {
                    SetActiveVerticalTrackIndex(-2);
                    graph.ResetView();
                }

                for (int i = 0; i < traces.Length + 1; i++)
                {
                    if ((keyData & (Keys)((int)Keys.D0 + i)) == (Keys)((int)Keys.D0 + i))
                    {
                        SetActiveVerticalTrackIndex(i - 1);
                    }
                }
            }
            else if ((keyData & Keys.Alt) == Keys.Alt)
            {
                for (int i = 0; i < traces.Length + 1; i++)
                {
                    if ((keyData & (Keys)((int)Keys.D0 + i)) == (Keys)((int)Keys.D0 + i))
                    {
                        SetActiveHorizontalTrackIndex(i - 1);
                    }
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void allHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetActiveHorizontalTrackIndex(-1);
        }

        private void allVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetActiveVerticalTrackIndex(-1);
        }

        private void CenterAll_Click(object sender, EventArgs e)
        {
            CenterOnTrace(-1);
        }

        private void CenterItem_Click(object sender, EventArgs e)
        {
            CenterOnTrace((int)(sender as ToolStripMenuItem).Tag);
        }

        private void CenterOnTrace(int index)
        {
            SetActiveVerticalTrackIndex(-2);
            graph.CenterOnTrace(index);
        }

        /// <summary>
        /// Form closing event to minimise form instead of close.
        /// </summary>
        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                Options.Windows[ID].Bounds = this.DesktopBounds;
            }

            if (e.CloseReason == CloseReason.UserClosing ||
                e.CloseReason == CloseReason.None)
            {
                Options.Windows[ID].IsOpen = false;

                Hide();

                e.Cancel = true;
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            if (Options.Windows[ID].Bounds != Rectangle.Empty)
            {
                this.DesktopBounds = Options.Windows[ID].Bounds;
            }

            WindowState = Options.Windows[ID].WindowState;

            verticalTrackItems.Add(verticalAutoscaleMenuItem.DropDownItems[0] as ToolStripMenuItem);
            horizontalTrackItems.Add(horizontalAutoscaleMenuItem.DropDownItems[0] as ToolStripMenuItem);

            int index = 0;
            foreach (Graph.Trace trace in traces)
            {
                ToolStripMenuItem centerItem = new ToolStripMenuItem(trace.Name, null, CenterItem_Click, (Keys.Control | Keys.Alt | (Keys)(int)Keys.D1 + index))
                {
                    Tag = index,
                };

                centerTraceToolStripMenuItem.DropDownItems.Add(centerItem);

                ToolStripMenuItem verticalTrackItem = new ToolStripMenuItem(trace.Name, null, VerticalTrackItem_Click, (Keys)((int)Shortcut.Ctrl1 + index))
                {
                    Tag = index,
                };

                verticalTrackItems.Add(verticalTrackItem);
                verticalAutoscaleMenuItem.DropDownItems.Add(verticalTrackItem);

                ToolStripMenuItem horizontalTrackItem = new ToolStripMenuItem(trace.Name, null, HorizontalTrackItem_Click, (Keys)((int)Shortcut.Alt1 + index))
                {
                    Tag = index,
                };

                horizontalTrackItems.Add(horizontalTrackItem);
                horizontalAutoscaleMenuItem.DropDownItems.Add(horizontalTrackItem);

                index++;
            }

            SetActiveVerticalTrackIndex(-1);
        }

        /// <summary>
        /// Form visible changed event to start/stop form update formUpdateTimer.
        /// </summary>
        private void Form_VisibleChanged(object sender, EventArgs e)
        {
            Options.Windows[ID].IsOpen = this.Visible;

            //if (this.Visible)
            //{
            //    formUpdateTimer.Start();
            //}
            //else
            //{
            //    formUpdateTimer.Stop();
            //}
        }

        private void Graph_UserChangedViewport(Rug.LiteGL.Controls.GraphBase graph, bool xChanged, bool yChanged)
        {
            if (yChanged == true)
            {
                SetActiveVerticalTrackIndex(-2);
            }

            if (xChanged == true)
            {
                SetActiveHorizontalTrackIndex(-2);
            }
        }

        private void GraphWindow_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                Options.Windows[ID].Bounds = this.DesktopBounds;
            }
        }

        private void horizonatalZoomInMenuItem_Click(object sender, EventArgs e)
        {
            graph.ZoomGraph(-0.1f, 0);
        }

        private void horizonatalZoomOutMenuItem_Click(object sender, EventArgs e)
        {
            graph.ZoomGraph(0.1f, 0);
        }

        private void horizontalRollToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (graph.Rolling == horizontalRollToolStripMenuItem.Checked)
            {
                return; 
            }

            graph.Rolling = horizontalRollToolStripMenuItem.Checked;

            graph.Clear();
            graph.AxesRange = graph.DefaultAxesRange;
        }

        private void HorizontalTrackItem_Click(object sender, EventArgs e)
        {
            SetActiveHorizontalTrackIndex((int)(sender as ToolStripMenuItem).Tag);
        }

        private void resetViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graph.ResetView();
        }

        private void scrollDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graph.ScrollGraph(0, -0.1f);
        }

        private void scrollLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graph.ScrollGraph(-0.1f, 0f);
        }

        private void scrollUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graph.ScrollGraph(0, 0.1f);
        }

        private void SetActiveHorizontalTrackIndex(int index)
        {
            if (graph.TrackHorizontalTraceIndex == index && graph.TrackHorizontalTrace == true)
            {
                index = -2;
            }

            bool anyChecked = false;

            for (int i = 0; i < horizontalTrackItems.Count; i++)
            {
                horizontalTrackItems[i].Checked = i == index + 1;

                anyChecked |= horizontalTrackItems[i].Checked;
            }

            horizontalAutoscaleMenuItem.Checked = anyChecked;

            graph.TrackHorizontalTraceIndex = index;
            graph.TrackHorizontalTrace = index > -2;
        }

        private void SetActiveVerticalTrackIndex(int index)
        {
            if (graph.TrackVerticalTraceIndex == index && graph.TrackVerticalTrace == true)
            {
                index = -2;
            }

            bool anyChecked = false;

            for (int i = 0; i < verticalTrackItems.Count; i++)
            {
                verticalTrackItems[i].Checked = i == index + 1;

                anyChecked |= verticalTrackItems[i].Checked;
            }

            verticalAutoscaleMenuItem.Checked = anyChecked;

            graph.TrackVerticalTraceIndex = index;
            graph.TrackVerticalTrace = index > -2;
        }

        private void VerticalTrackItem_Click(object sender, EventArgs e)
        {
            SetActiveVerticalTrackIndex((int)(sender as ToolStripMenuItem).Tag);
        }

        #region Window Resize / Move Events

        private void Form_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                Options.Windows[ID].WindowState = WindowState;
            }
        }

        private void Form_ResizeBegin(object sender, EventArgs e)
        {
        }

        private void Form_ResizeEnd(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                Options.Windows[ID].Bounds = this.DesktopBounds;
            }
        }

        private void Form3DView_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        #endregion Window Resize / Move Events

        private void verticalZoomInMenuItem_Click(object sender, EventArgs e)
        {
            graph.ZoomGraph(0, -0.1f);
        }

        private void verticalZoomOutMenuItem_Click(object sender, EventArgs e)
        {
            graph.ZoomGraph(0, 0.1f);
        }

        private void xScrollRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graph.ScrollGraph(0.1f, 0f);
        }

        private void graph_ViewReset(object sender, EventArgs e)
        {
            horizontalRollToolStripMenuItem.Checked = false; 

            SetActiveVerticalTrackIndex(-1);
            SetActiveHorizontalTrackIndex(-2);

            graph.AxesRange.MinX = graph.DefaultAxesRange.MinX;
            graph.AxesRange.MaxX = graph.DefaultAxesRange.MaxX;
            graph.AxesRange.OffsetX = 0;
            graph.AxesRange.OffsetY = 0;
        }
    }
}