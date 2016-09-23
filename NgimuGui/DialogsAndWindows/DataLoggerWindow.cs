using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NgimuApi;
using NgimuApi.Logging;

namespace NgimuGui.DialogsAndWindows
{
    public partial class DataLoggerWindow : BaseForm
    {
        public static string ID = "Logger";

        private SessionLogger m_Logger;

        private Connection m_ActiveConnection;

        public Connection ActiveConnection
        {
            get { return m_ActiveConnection; }
            set { m_ActiveConnection = value; }
        }

        public DataLoggerWindow()
        {
            InitializeComponent();
        }

        private void DataLoggerWindow_Load(object sender, EventArgs e)
        {
            if (Options.Windows[ID].Bounds != Rectangle.Empty)
            {
                this.DesktopBounds = Options.Windows[ID].Bounds;
            }

            WindowState = Options.Windows[ID].WindowState;
            Options.Windows[ID].IsOpen = true;

            m_StopButton.Enabled = false;

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
                MessageBox.Show(this, "Invalid session name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // check the filename is good
            if (CheckFileName(pathSelector.SelectedPath) == false)
            {
                MessageBox.Show(this, "The file name \"" + pathSelector.SelectedPath + "\" is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string fullpath = Path.Combine(pathSelector.SelectedPath, m_SessionNameBox.Text);

            // check the filename is good
            if (CheckFileName(fullpath) == false)
            {
                MessageBox.Show(this, "The session path \"" + fullpath + "\" is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            uint loggingPeriod = 0;

            // check logging period is valid 			
            if (string.IsNullOrEmpty(m_LoggingPeriodBox.Text) == false)
            {
                if (uint.TryParse(m_LoggingPeriodBox.Text, out loggingPeriod) == false)
                {
                    MessageBox.Show(this, "Period must be a unsigned integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
            if (m_ActiveConnection == null)
            {
                MessageBox.Show(this, "No connection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            if (m_Logger != null)
            {
                MessageBox.Show(this, "Already logging.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            pathSelector.Enabled = false;
            m_SessionNameBox.Enabled = false;

            m_StartButton.Enabled = false;
            m_LoggingPeriodBox.Enabled = false;
            m_StopButton.Enabled = true;

            try
            {
                m_Logger = new SessionLogger(settings, m_ActiveConnection);

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
            MessageBox.Show(this, label, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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

            m_Logger = null;

            return true;
        }

        private void CsvLoggerWindow_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void CsvLoggerWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopLogging();

            if (e.CloseReason == CloseReason.UserClosing || e.CloseReason == CloseReason.None)
            {
                Options.Windows[ID].IsOpen = false;
            }
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

        #region Window Resize / Move Events

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

        private void Form_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                Options.Windows[ID].WindowState = WindowState;
            }
        }

        #endregion

        private void SessionNameBox_TextChanged(object sender, EventArgs e)
        {
            m_SessionNameBox.ForeColor = Control.DefaultForeColor;

            if (string.IsNullOrEmpty(m_SessionNameBox.Text) == true ||
                Helper.IsInvalidFileName(m_SessionNameBox.Text) == true)
            {
                m_SessionNameBox.ForeColor = Color.Red;
            }
        }
    }
}
