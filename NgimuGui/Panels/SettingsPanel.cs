using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using NgimuApi;
using NgimuGui.DialogsAndWindows;
using NgimuGui.TypeDescriptors;

namespace NgimuGui.Panels
{
    public partial class SettingsPanel : UserControl
    {
        private delegate void DoubleStringEvent(string label, string detail);

        private NgimuApi.Settings m_BackupSettings;
        private Reporter m_Reporter = new Reporter();
        private NgimuApi.Settings m_Settings;
        private SettingsTypeDescriptor m_SettingsTypeDescriptor;

        public bool NeedsWrite { get; private set; }

        public NgimuApi.Settings Settings { get { return m_Settings; } }

        public event EventHandler ReadWriteStateChanged;

        public SettingsPanel()
        {
            InitializeComponent();

            propertyGrid1.SelectedGridItemChanged += new SelectedGridItemChangedEventHandler(propertyGrid1_SelectedGridItemChanged);
            propertyGrid1.PropertySort = PropertySort.Categorized;
            propertyGrid1.PropertyValueChanged += new PropertyValueChangedEventHandler(propertyGrid1_PropertyValueChanged);

            try
            {
                m_BackupSettings = new NgimuApi.Settings();
                m_Settings = m_BackupSettings;

                SetPropertyGridObject(m_Settings);
            }
            catch (Exception ex)
            {
                DisplayException(this, new ExceptionEventArgs("Failed to load settings.", ex));
            }
        }

        public void OnConnect(Connection comms)
        {
            m_BackupSettings.CopyTo(comms.Settings);
            m_Settings = comms.Settings;
            SetPropertyGridObject(m_Settings);
            CheckChangedState();

            m_Reporter.Updated += Process_Updated;
            m_Reporter.Exception += DisplayException;
            m_Reporter.Error += Process_Error;
            m_Reporter.Info += Process_Info;
        }

        public void OnDisconnect()
        {
            m_Settings.Stop();
            m_Reporter.Updated -= Process_Updated;
            m_Reporter.Exception -= DisplayException;
            m_Reporter.Error -= Process_Error;
            m_Reporter.Info -= Process_Info;

            m_Settings.CopyTo(m_BackupSettings);
            m_Settings = m_BackupSettings;
            SetPropertyGridObject(m_Settings);
            CheckChangedState();
        }

        public void OnLoadedFromFile()
        {
            SetPropertyGridObject(m_Settings);
            CheckChangedState();
        }

        public void ReadFromDevice()
        {
            ReporterWrapper reporter = new ReporterWrapper(m_Reporter);

            reporter.Completed += (sender, e) =>
            {
                bool allValuesFailed;
                bool allValuesSucceeded;
                string messageString = GetCommunicationFailureString(20, out allValuesFailed, out allValuesSucceeded);

                if (allValuesSucceeded == true)
                {
                    return;
                }

                if (allValuesFailed == true)
                {
                    messageString = "Failed to read all settings.";
                }
                else
                {
                    messageString = "Failed to read the following settings:" + messageString;
                }

                Invoke((MethodInvoker)(() => { MessageBox.Show(this, messageString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }));
            };

            try
            {
                m_Settings.ReadAync(reporter, Options.Timeout, Options.MaximumNumberOfRetries);
            }
            catch
            {
            }
        }

        public void WriteToDevice()
        {
            ReporterWrapper reporter = new ReporterWrapper(m_Reporter);

            reporter.Completed += (sender, e) =>
            {
                bool allValuesFailed;
                bool allValuesSucceeded;
                string messageString = GetCommunicationFailureString(20, out allValuesFailed, out allValuesSucceeded);

                if (allValuesSucceeded == true)
                {
                    return;
                }

                if (allValuesFailed == true)
                {
                    messageString = "Failed to confirm write of all settings.";
                }
                else
                {
                    messageString = "Failed to confirm write of following settings:" + messageString;
                }

                Invoke((MethodInvoker)(() => { MessageBox.Show(this, messageString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }));
            };

            try
            {
                m_Settings.WriteAync(reporter, Options.Timeout, Options.MaximumNumberOfRetries);
                m_Settings.Connection?.SendCommand(Command.Apply);
            }
            catch
            {
            }
        }

        private void CheckChangedState()
        {
            bool changed = false;

            foreach (ISettingValue var in m_Settings.Values)
            {
                changed |= var.GetRemoteValue().Equals(var.GetValue()) == false;
            }

            NeedsWrite = changed;

            ReadWriteStateChanged?.Invoke(this, EventArgs.Empty);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.SelectedText);
        }

        private void DisplayException(object sender, ExceptionEventArgs args)
        {
            if (this.InvokeRequired == true)
            {
                this.BeginInvoke(new DoubleStringEvent(DisplayException_Inner), new object[] { args.Message, args.Exception.ToString() });
            }
            else
            {
                DisplayException_Inner(args.Message, args.Exception.ToString());
            }
        }

        private void DisplayException_Inner(string label, string detail)
        {
            using (ExceptionDialog dialog = new ExceptionDialog())
            {
                dialog.Title = "An Exception Occurred";

                dialog.Label = label;
                dialog.Detail = detail;

                dialog.ShowDialog(this);
            }
        }

        private string GetCommunicationFailureString(int max, out bool allValuesFailed, out bool allValuesSucceeded)
        {
            allValuesFailed = true;
            allValuesSucceeded = true;
            StringBuilder sb = new StringBuilder();
            int count = 0;
            int truncatedCount = 0;

            sb.AppendLine();
            sb.AppendLine();

            foreach (ISettingValue value in m_Settings.Values)
            {
                if (value.CommunicationResult.HasValue && value.CommunicationResult.Value == CommunicationProcessResult.Success)
                {
                    allValuesFailed = false;

                    continue;
                }

                if (value.CommunicationResult.HasValue == false)
                {
                    continue;
                }

                allValuesSucceeded = false;

                if (count++ >= max)
                {
                    truncatedCount++;
                    continue;
                }

                //sb.AppendLine(); 
                //sb.AppendLine(GetName(value));
                sb.AppendLine(value.OscAddress);
            }

            if (truncatedCount > 0)
            {
                //sb.AppendLine();
                sb.Append("... (" + truncatedCount + " more)");
            }

            return sb.ToString();
        }

        private string GetName(ISettingValue value)
        {
            string name = value.Name.Trim();

            var category = value.Category;

            while (category != null)
            {
                name = category.CategoryText.Trim() + " - " + name;

                category = category.Parent;

                if (category.Parent == null)
                {
                    break;
                }
            }

            return name.Trim();
        }

        private void Process_Error(object sender, MessageEventArgs e)
        {
            GuiTerminal.WriteError(e.Message, e.Detail);
        }

        private void Process_Info(object sender, MessageEventArgs e)
        {
            GuiTerminal.WriteInfo(e.Message, e.Detail);
        }

        private void Process_Updated(object sender, EventArgs e)
        {
            if (propertyGrid1.InvokeRequired)
            {
                propertyGrid1.BeginInvoke(new EventHandler(Process_Updated), new object[] { sender, e });
            }
            else
            {
                propertyGrid1.SelectedObject = null;
                propertyGrid1.SelectedObject = m_SettingsTypeDescriptor;
                propertyGrid1.ExpandAllGridItems();

                CheckChangedState();
            }
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = e.ChangedItem.PropertyDescriptor;

            UpdateDescription(propertyDescriptor);

            CheckChangedState();
        }

        private void propertyGrid1_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = e.NewSelection.PropertyDescriptor;

            UpdateDescription(propertyDescriptor);
        }

        private void SetPropertyGridObject(NgimuApi.Settings settings)
        {
            m_SettingsTypeDescriptor = new SettingsTypeDescriptor(settings);

            propertyGrid1.SelectedObject = m_SettingsTypeDescriptor;

            propertyGrid1.ExpandAllGridItems();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
        }

        private void UpdateDescription(PropertyDescriptor propertyDescriptor)
        {
            if (propertyDescriptor != null &&
                String.IsNullOrEmpty(propertyDescriptor.Description) == false)
            {
                richTextBox1.Rtf = propertyDescriptor.Description;
            }
            else
            {
                richTextBox1.Text = SettingsDocumentation.NoItemSelectedText;
            }
        }
    }
}