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
        public readonly string ID;

        private GraphSettings defaultGraphSettings;
        private GraphSettings graphSettings;

        private List<ToolStripMenuItem> horizontalTrackItems = new List<ToolStripMenuItem>();
        private List<ToolStripMenuItem> verticalTrackItems = new List<ToolStripMenuItem>();

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
            graph.Rolling = graphSettings.RollMode;

            graph.Traces.AddRange(defaultGraphSettings.Traces);
        }

        public void AddData(DateTime timestamp, int index, float value)
        {
            graph.AddScopeData(timestamp, index, value);
        }

        public void AddData(DateTime timestamp, params float[] values)
        {
            graph.AddScopeData(timestamp, values);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((keyData & Keys.Control) == Keys.Control && (keyData & Keys.Alt) == Keys.Alt)
            {
                keyData = keyData & ~(Keys.Control | Keys.Alt);

                for (int i = 0; i < graph.Traces.Count + 1; i++)
                {
                    if ((keyData & (Keys)((int)Keys.D0 + i)) == (Keys)((int)Keys.D0 + i))
                    {
                        CenterOnTrace(i - 1);
                    }
                }
            }
            else if ((keyData & Keys.Control) == Keys.Control)
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

                for (int i = 0; i < graph.Traces.Count + 1; i++)
                {
                    if (keyData == (Keys)((int)Keys.D0 + i))
                    {
                        SetVerticalAutoscaleIndex(i - 1, false);
                    }
                }
            }
            else if ((keyData & Keys.Alt) == Keys.Alt)
            {
                keyData = keyData & ~(Keys.Control | Keys.Alt);

                for (int i = 0; i < graph.Traces.Count + 1; i++)
                {
                    if (keyData == (Keys)((int)Keys.D0 + i))
                    {
                        SetHorizontalAutoscaleIndex(i - 1, false);
                    }
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void allHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetHorizontalAutoscaleIndex(int.MaxValue, false);
        }

        private void allVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetVerticalAutoscaleIndex(int.MaxValue, false);
        }

        private void CenterAll_Click(object sender, EventArgs e)
        {
            CenterOnTrace(int.MaxValue);
        }

        private void CenterItem_Click(object sender, EventArgs e)
        {
            CenterOnTrace((int)(sender as ToolStripMenuItem).Tag);
        }

        private void CenterOnTrace(int index)
        {
            SetVerticalAutoscaleIndex(-1, false);
            graph.CenterOnTrace(index);
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

            graph.Clear();
            graph_ViewReset(null, EventArgs.Empty);
        }

        private void Graph_UserChangedViewport(Rug.LiteGL.Controls.GraphBase graph, bool xChanged, bool yChanged)
        {
            if (yChanged == true)
            {
                SetVerticalAutoscaleIndex(-1, true);
            }

            if (xChanged == true)
            {
                SetHorizontalAutoscaleIndex(-1, true);
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

            graphSettings.RollMode = graph.Rolling;
            graphSettings.AxesRange = graph.AxesRange;
        }

        private void HorizontalTrackItem_Click(object sender, EventArgs e)
        {
            SetHorizontalAutoscaleIndex((int)(sender as ToolStripMenuItem).Tag, false);
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

        private void SetHorizontalAutoscaleIndex(int index, bool @override)
        {
            if (@override == false && graph.TrackHorizontalTraceIndex == index && graph.TrackHorizontalTrace == true)
            {
                index = -1;
            }

            bool anyChecked = false;

            for (int i = 0; i < horizontalTrackItems.Count; i++)
            {
                if (i == 0)
                {
                    horizontalTrackItems[i].Checked = index >= horizontalTrackItems.Count;
                }
                else
                {
                    horizontalTrackItems[i].Checked = i - 1 == index;
                }

                anyChecked |= horizontalTrackItems[i].Checked;
            }

            horizontalAutoscaleMenuItem.Checked = anyChecked;

            graph.TrackHorizontalTraceIndex = index;
            graph.TrackHorizontalTrace = index >= 0;

            graphSettings.HorizontalAutoscaleIndex = index;
        }

        private void SetVerticalAutoscaleIndex(int index, bool @override)
        {
            if (@override == false && graph.TrackVerticalTraceIndex == index && graph.TrackVerticalTrace == true)
            {
                index = -1;
            }

            bool anyChecked = false;

            for (int i = 0; i < verticalTrackItems.Count; i++)
            {
                if (i == 0)
                {
                    verticalTrackItems[i].Checked = index >= verticalTrackItems.Count;
                }
                else
                {
                    verticalTrackItems[i].Checked = i - 1 == index;
                }

                anyChecked |= verticalTrackItems[i].Checked;
            }

            verticalAutoscaleMenuItem.Checked = anyChecked;

            graph.TrackVerticalTraceIndex = index;
            graph.TrackVerticalTrace = index >= 0;

            graphSettings.VerticalAutoscaleIndex = index;
        }

        private void VerticalTrackItem_Click(object sender, EventArgs e)
        {
            SetVerticalAutoscaleIndex((int)(sender as ToolStripMenuItem).Tag, false);
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

            bool rollModeChanged = graph.Rolling != graphSettings.RollMode;
            graph.Rolling = graphSettings.RollMode;

            horizontalRollToolStripMenuItem.Checked = graphSettings.RollMode;

            if (rollModeChanged == true)
            {
                graph.Clear();
            }

            SetHorizontalAutoscaleIndex(graphSettings.HorizontalAutoscaleIndex, true);
            SetVerticalAutoscaleIndex(graphSettings.VerticalAutoscaleIndex, true);
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
    }
}