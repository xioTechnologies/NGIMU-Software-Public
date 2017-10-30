using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NgimuApi;
using NgimuForms.DialogsAndWindows;
using Rug.LiteGL.Controls;

namespace NgimuGui.DialogsAndWindows
{
    public partial class GraphWindow : BaseForm
    {
        public enum TrackingMode : int
        {
            None = -1, 
            All = int.MaxValue, 
        }

        public readonly string ID;

        private GraphSettings defaultGraphSettings;
        private GraphSettings graphSettings;

        private List<ToolStripMenuItem> horizontalTrackItems = new List<ToolStripMenuItem>();
        private List<ToolStripMenuItem> verticalTrackItems = new List<ToolStripMenuItem>();

        private bool paused = false; 

        private string GraphSettingsFilePath { get { return Helper.ResolvePath("~/Graph Settings/" + defaultGraphSettings.Title + ".xml"); } }

        public GraphWindow(GraphSettings defaultGraphSettings) : base(defaultGraphSettings.Title)
        {
            this.defaultGraphSettings = defaultGraphSettings;

            ID = defaultGraphSettings.Title;

            if (GraphSettings.FromFile(GraphSettingsFilePath, out graphSettings) == false)
            {
                graphSettings = defaultGraphSettings.Clone();
            }

            InitializeComponent();

            Name = defaultGraphSettings.Title;
            Text = defaultGraphSettings.Title;

            graph.ShowLegend = graphSettings.ShowLegend;
            graph.AxesRange = graphSettings.AxesRange;
            graph.YAxisLabel = graphSettings.YAxisLabel;
            graph.LockXMouse = false;
            graph.Rolling = true;

            graph.Traces.AddRange(defaultGraphSettings.Traces);
        }

        public void AddData(DateTime timestamp, int index, float value)
        {
            if (paused)
            {
                return; 
            }

            graph.AddScopeData(timestamp, index, value);
        }

        public void AddData(DateTime timestamp, params float[] values)
        {
            if (paused)
            {
                return; 
            }

            graph.AddScopeData(timestamp, values);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((keyData & Keys.Control) == Keys.Control)
            {
                keyData = keyData & ~(Keys.Control | Keys.Alt);

                if (keyData == Keys.Back)
                {
                    graph.ResetView();
                }

                if (keyData == Keys.L)
                {
                    graph.Clear();
                }

                if (keyData == Keys.Space)
                {
                    pauseToolStripMenuItem.PerformClick(); 
                }

                if (keyData == Keys.Right)
                {
                    graph.ZoomGraph(0.1f, 0);
                }

                if (keyData == Keys.Left)
                {
                    graph.ZoomGraph(-0.1f, 0);
                }

                if (keyData == Keys.Down)
                {
                    graph.ZoomGraph(0, 0.1f);
                }

                if (keyData == Keys.Up)
                {
                    graph.ZoomGraph(0, -0.1f);
                }

                for (int i = 0; i < graph.Traces.Count + 1; i++)
                {
                    if (keyData == (Keys)((int)Keys.D0 + i))
                    {
                        SetVerticalAutoscaleIndex(i == 0 ? TrackingMode.All : (TrackingMode)(i - 1), false);
                    }
                }
            }
            else if ((keyData & Keys.Alt) == Keys.Alt)
            {
                keyData = keyData & ~(Keys.Control | Keys.Alt);

                if (keyData == Keys.Left)
                {
                    graph.ScrollGraph(-0.1f, 0);
                }

                if (keyData == Keys.Right)
                {
                    graph.ScrollGraph(0.1f, 0);
                }

                if (keyData == Keys.Down)
                {
                    graph.ScrollGraph(0, 0.1f);
                }

                if (keyData == Keys.Up)
                {
                    graph.ScrollGraph(0, -0.1f);
                }

                for (int i = 0; i < graph.Traces.Count + 1; i++)
                {
                    if (keyData == (Keys)((int)Keys.D0 + i))
                    {
                        SetHorizontalAutoscaleIndex(i == 0 ? TrackingMode.All : (TrackingMode)(i - 1), false);
                    }
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void allHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetHorizontalAutoscaleIndex(TrackingMode.All, false);
        }

        private void allVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetVerticalAutoscaleIndex(TrackingMode.All, false);
        }

        private void CenterAll_Click(object sender, EventArgs e)
        {
            CenterOnTrace(TrackingMode.All);
        }

        private void CenterItem_Click(object sender, EventArgs e)
        {
            CenterOnTrace((TrackingMode)(int)(sender as ToolStripMenuItem).Tag);
        }

        private void CenterOnTrace(TrackingMode modeOrIndex)
        {
            SetVerticalAutoscaleIndex(TrackingMode.None, false);
            graph.CenterOnTrace((int)modeOrIndex);
        }

        /// <summary>
        /// Form closing event to minimise form instead of close.
        /// </summary>
        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        public void SaveSettings()
        {
            graphSettings.AxesRange = graph.AxesRange;

            GraphSettings.SaveToFile(GraphSettingsFilePath, graphSettings);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            verticalTrackItems.Add(verticalAutoscaleMenuItem.DropDownItems[0] as ToolStripMenuItem);
            horizontalTrackItems.Add(horizontalAutoscaleMenuItem.DropDownItems[0] as ToolStripMenuItem);

            int index = 0;
            foreach (Trace trace in graph.Traces)
            {
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

            graph.Clear();
            graph_ViewReset(null, EventArgs.Empty);
        }

        private void Graph_UserChangedViewport(Rug.LiteGL.Controls.GraphBase graph, bool xChanged, bool yChanged)
        {
            if (yChanged == true)
            {
                SetVerticalAutoscaleIndex(TrackingMode.None, true);
            }

            if (xChanged == true)
            {
                SetHorizontalAutoscaleIndex(TrackingMode.None, true);
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

        private void HorizontalTrackItem_Click(object sender, EventArgs e)
        {
            SetHorizontalAutoscaleIndex((TrackingMode)(int)(sender as ToolStripMenuItem).Tag, false);
        }

        private void resetViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graphSettings = defaultGraphSettings.Clone();

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

        private void SetHorizontalAutoscaleIndex(TrackingMode modeOrIndex, bool @override)
        {
            if (@override == false && graph.TrackHorizontalTraceIndex == (int)modeOrIndex && graph.TrackHorizontalTrace == true)
            {
                modeOrIndex = TrackingMode.None;
            }

            bool anyChecked = false;

            for (int i = 0; i < horizontalTrackItems.Count; i++)
            {
                if (i == 0)
                {
                    horizontalTrackItems[i].Checked = (int) modeOrIndex >= horizontalTrackItems.Count;
                }
                else
                {
                    horizontalTrackItems[i].Checked = i - 1 == (int)modeOrIndex;
                }

                anyChecked |= horizontalTrackItems[i].Checked;
            }

            horizontalAutoscaleMenuItem.Checked = anyChecked;
            graph.LockXZoom = anyChecked;
            horizonatalZoomOutMenuItem.Enabled = !anyChecked;
            horizonatalZoomInMenuItem.Enabled = !anyChecked;

            graph.TrackHorizontalTraceIndex = (int)modeOrIndex;
            graph.TrackHorizontalTrace = modeOrIndex >= 0;

            graphSettings.HorizontalAutoscaleIndex = (int)modeOrIndex;
        }

        private void SetVerticalAutoscaleIndex(TrackingMode modeOrIndex, bool @override)
        {
            if (@override == false && graph.TrackVerticalTraceIndex == (int)modeOrIndex && graph.TrackVerticalTrace == true)
            {
                modeOrIndex = TrackingMode.None;
            }

            bool anyChecked = false;

            for (int i = 0; i < verticalTrackItems.Count; i++)
            {
                if (i == 0)
                {
                    verticalTrackItems[i].Checked = (int)modeOrIndex >= verticalTrackItems.Count;
                }
                else
                {
                    verticalTrackItems[i].Checked = i - 1 == (int)modeOrIndex;
                }

                anyChecked |= verticalTrackItems[i].Checked;
            }

            verticalAutoscaleMenuItem.Checked = anyChecked;

            verticalZoomOutMenuItem.Enabled = !anyChecked;
            verticalZoomInMenuItem.Enabled = !anyChecked;
            verticalScrollUpToolStripMenuItem.Enabled = !anyChecked;
            verticalScrollDownToolStripMenuItem.Enabled = !anyChecked;

            graph.LockYMouse = anyChecked; 
            graph.LockYZoom = anyChecked;
            graph.TrackVerticalTraceIndex = (int)modeOrIndex;
            graph.TrackVerticalTrace = modeOrIndex >= 0;

            graphSettings.VerticalAutoscaleIndex = (int)modeOrIndex;
        }

        private void VerticalTrackItem_Click(object sender, EventArgs e)
        {
            SetVerticalAutoscaleIndex((TrackingMode)(int)(sender as ToolStripMenuItem).Tag, false);
        }

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
            graphSettings = defaultGraphSettings.Clone();

            AxesRange defaultRange = graphSettings.AxesRange;
            AxesRange currentRange = graph.AxesRange;

            currentRange.XMin = defaultRange.XMin;
            currentRange.XMax = defaultRange.XMax;
            currentRange.YMin = defaultRange.YMin;
            currentRange.YMax = defaultRange.YMax;
            currentRange.XOffset = 0;
            currentRange.YOffset = 0;

            graph.AxesRange = currentRange;

            graph.Rolling = true; 

            SetHorizontalAutoscaleIndex((TrackingMode)graphSettings.HorizontalAutoscaleIndex, true);
            SetVerticalAutoscaleIndex((TrackingMode)graphSettings.VerticalAutoscaleIndex, true);
        }

        public void SetSampleBufferSize(uint graphSampleBufferSize)
        {
            lock (graph.Traces.SyncRoot)
            {
                foreach (Trace trace in graph.Traces)
                {
                    trace.MaxDataPoints = graphSampleBufferSize;
                }
            }

            foreach (Trace trace in defaultGraphSettings.Traces)
            {
                trace.MaxDataPoints = graphSampleBufferSize;
            }

            foreach (Trace trace in graphSettings.Traces)
            {
                trace.MaxDataPoints = graphSampleBufferSize;
            }

            graph.Clear();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graph.Clear();
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paused = pauseToolStripMenuItem.Checked;
            graph.LockXMouse = false;
        }

        private void horizontalScrollLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graph.ScrollGraph(-0.1f, 0);
        }

        private void horizontalScrollRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graph.ScrollGraph(0.1f, 0);
        }

        private void verticalScrollDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graph.ScrollGraph(0, 0.1f);
        }

        private void verticalScrollUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graph.ScrollGraph(0, -0.1f);
        }
    }
}