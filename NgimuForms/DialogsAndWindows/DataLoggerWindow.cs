using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NgimuApi;
using NgimuApi.Logging;
using NgimuForms.Controls;

namespace NgimuForms.DialogsAndWindows
{
    public partial class DataLoggerWindow : BaseForm
    {
        public static string ID = "Logger";

        private SessionLogger m_Logger;

        private string lastSessionDirectory;

        public List<Connection> ActiveConnections { get; } = new List<Connection>();

        public DataLoggerWindow() : base(ID)
        {
            InitializeComponent();
        }

        private void DataLoggerWindow_Load(object sender, EventArgs e)
        {
            //if (WindowManager.Get(ID).Bounds != Rectangle.Empty)
            //{
            //    this.DesktopBounds = WindowManager.Get(ID).Bounds;
            //}

            //WindowState = WindowManager.Get(ID).WindowState;
            //WindowManager.Get(ID).IsOpen = true;

            m_StopButton.Enabled = false;
            openDirectory.Enabled = false;
            pathSelector.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        private bool CheckFileName(string fileName)
        {
            try
            {
                new System.IO.FileInfo(fileName);

                return System.IO.Path.IsPathRooted(fileName);
            }
            catch
            {
                return false;
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(m_SessionNameBox.Text) == true ||
                Helper.IsInvalidFileName(m_SessionNameBox.Text) == true)
            {
                this.ShowError("Invalid session name.");
                return;
            }

            // check the filename is good
            if (CheckFileName(pathSelector.SelectedPath) == false)
            {
                this.ShowError("The file name \"" + pathSelector.SelectedPath + "\" is not valid.");
                return;
            }

            string fullpath = Path.Combine(pathSelector.SelectedPath, m_SessionNameBox.Text);

            // check the filename is good
            if (CheckFileName(fullpath) == false)
            {
                this.ShowError("The session path \"" + fullpath + "\" is not valid.");
                return;
            }

            uint loggingPeriod = 0;

            // check logging period is valid 			
            if (string.IsNullOrEmpty(m_LoggingPeriodBox.Text) == false)
            {
                if (uint.TryParse(m_LoggingPeriodBox.Text, out loggingPeriod) == false)
                {
                    this.ShowError("Period must be a unsigned integer.");
                    return;
                }
            }

            SessionSettings settings = new SessionSettings()
            {
                RootDirectory = NgimuApi.Helper.ResolvePath(pathSelector.SelectedPath),
                SessionName = m_SessionNameBox.Text,
                LoggingPeriod = loggingPeriod,
            };

            /* 
            // the readme file exists
            //if (File.Exists(settings.ReadmePath) == true)
            if (Directory.Exists(settings.RootDirectory) == true && 
                (Directory.GetFiles(settings.RootDirectory).Length > 0 || 
                Directory.GetDirectories(settings.RootDirectory).Length > 0)) 
            {
                MessageBox.Show(this, "The destination directory must be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // do confirm box here
                // also delete folder 

                return; 
            }
            */

            // create logger 
            StartLogging(settings);
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            if (StopLogging() == false)
            {
                return;
            }
        }

        private bool StartLogging(SessionSettings settings)
        {
            if (ActiveConnections.Count == 0)
            {
                this.ShowError("No connection.");

                return false;
            }

            if (m_Logger != null)
            {
                this.ShowError("Already logging.");

                return false;
            }

            pathSelector.Enabled = false;
            m_SessionNameBox.Enabled = false;

            m_StartButton.Enabled = false;
            m_LoggingPeriodBox.Enabled = false;
            m_StopButton.Enabled = true;
            openDirectory.Enabled = false;

            try
            {
                m_Logger = new SessionLogger(settings, ActiveConnections.ToArray());

                m_Logger.Exception += DisplayException;
                m_Logger.Stopped += m_Logger_Stopped;

                m_Logger.Start();

                timer1.Enabled = true;

                return true;
            }
            catch (Exception ex)
            {
                StopLogging();

                DisplayException(ex);

                return false;
            }
        }

        void DisplayException(Exception ex)
        {
            if (this.InvokeRequired == true)
            {
                this.BeginInvoke(new Action<string, string>(DisplayException_Inner), new object[] { ex.Message, ex.ToString() });
            }
            else
            {
                DisplayException_Inner(ex.Message, ex.ToString());
            }
        }

        void DisplayException(object sender, ExceptionEventArgs args)
        {
            if (this.InvokeRequired == true)
            {
                this.BeginInvoke(new Action<string, string>(DisplayException_Inner), new object[] { args.Message, args.Exception.ToString() });
            }
            else
            {
                DisplayException_Inner(args.Message, args.Exception.ToString());
            }
        }

        void DisplayException_Inner(string label, string detail)
        {
            this.ShowError(label);
            /* 
            using (ExceptionDialog dialog = new ExceptionDialog())
            {
                dialog.Title = "An Exception Occurred";

                dialog.Label = label;
                dialog.Detail = detail;

                dialog.ShowDialog(this);
            }
            */
        }

        void m_Logger_Stopped(object sender, EventArgs e)
        {
            if (this.InvokeRequired == true)
            {
                this.Invoke(new Func<bool>(StopLogging));
            }
            else
            {
                StopLogging();
            }
        }

        private bool StopLogging()
        {
            timer1.Enabled = false;

            pathSelector.Enabled = true;
            m_SessionNameBox.Enabled = true;

            m_StartButton.Enabled = true;
            m_LoggingPeriodBox.Enabled = true;
            m_StopButton.Enabled = false;

            if (m_Logger == null)
            {
                return false;
            }

            m_Logger.Exception -= DisplayException;
            m_Logger.Stopped -= m_Logger_Stopped;

            m_Logger.Stop();

            lastSessionDirectory = m_Logger.SessionDirectory;

            TimeSpan span = m_Logger.LoggingTime;

            m_ClockPanel.CurrentTime = span;

            m_ClockPanel.Invalidate();

            m_Logger = null;

            openDirectory.Enabled = true;

            return true;
        }

        private void CsvLoggerWindow_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void CsvLoggerWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopLogging();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (m_Logger == null)
            {
                return;
            }

            TimeSpan span = m_Logger.LoggingTime;

            m_ClockPanel.CurrentTime = span;

            m_ClockPanel.Invalidate();
        }

        public void Stop()
        {
            StopLogging();
        }

        private void SessionNameBox_TextChanged(object sender, EventArgs e)
        {
            m_SessionNameBox.HasError = string.IsNullOrEmpty(m_SessionNameBox.Text) == true ||
                                        Helper.IsInvalidFileName(m_SessionNameBox.Text) == true;
        }

        private void openDirectory_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(lastSessionDirectory) == false)
            {
                this.ShowError("Directory does not exist. \"" + lastSessionDirectory + "\"");
                return;
            }

            Process.Start(lastSessionDirectory);
        }
    }
}
