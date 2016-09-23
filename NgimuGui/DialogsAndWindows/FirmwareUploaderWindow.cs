using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using NgimuApi;
using NgimuApi.Bootloader;

namespace NgimuGui.DialogsAndWindows
{
    public partial class FirmwareUploaderWindow : BaseForm
    {
        public static readonly string ID = "UploadFirmware";

        private string m_FilePath = string.Empty;
        private Thread m_Thread;
        private ProgressDialog m_Progress = new ProgressDialog();

        private bool m_Cancel;
        private bool m_Successful = false;

        public bool IsRunning { get; private set; }

        public NgimuApi.Connection ActiveConnection { get; set; }

        public event EventHandler AutoConnectRequest;

        public FirmwareUploaderWindow()
        {
            InitializeComponent();

            m_Progress.Style = ProgressBarStyle.Marquee;
            m_Progress.Text = "Uploading Firmware";
            m_Progress.CancelButtonEnabled = true;
            m_Progress.OnCancel += new FormClosingEventHandler(Progress_OnCancel);
        }

        private void OnAutoConnectRequest()
        {
            if (AutoConnectRequest != null)
            {
                AutoConnectRequest(this, EventArgs.Empty);
            }
        }

        private void UploadFirmwareDialog_Load(object sender, EventArgs e)
        {
            if (Options.Windows[ID].Bounds != Rectangle.Empty)
            {
                this.DesktopBounds = Options.Windows[ID].Bounds;
            }

            WindowState = Options.Windows[ID].WindowState;

            Options.Windows[ID].IsOpen = true;

            pathSelector.SelectedPath = m_FilePath;
        }

        private void m_UploadButton_Click(object sender, EventArgs e)
        {
            string filepath = pathSelector.SelectedPath;

            if (File.Exists(filepath) == false)
            {
                ShowErrorMessage("The selected file does not exist.", "Error");
                return;
            }

            // Check the current connection 
            if (ActiveConnection == null ||
                ActiveConnection.IsConnected == false ||
                ActiveConnection.ConnectionType != NgimuApi.ConnectionType.Serial)
            {
                if (Options.AllowUploadWithoutSerialConnection == false)
                {
                    ShowErrorMessage("The device is not connected via serial.", "Error");
                    return;
                }
            }

            m_Progress.ProgressMessage = "";
            m_Progress.Progress = 0;
            m_Progress.ProgressMaximum = 100;
            m_Progress.Style = ProgressBarStyle.Marquee;

            m_FilePath = filepath;

            m_Thread = new Thread(RunThread);
            m_Thread.Name = "Firmware Uploader";

            m_UploadButton.Enabled = false;
            pathSelector.Enabled = false;

            IsRunning = true;
            m_Cancel = false;
            m_Successful = false;
            m_Thread.Start();

            m_Progress.ShowDialog(this);
        }

        void Progress_OnCancel(object sender, FormClosingEventArgs e)
        {
            m_Cancel = true;

            e.Cancel = IsRunning;
        }

        private string GetResultText(CommunicationProcessResult result)
        {
            switch (result)
            {
                case CommunicationProcessResult.Running:
                    return "Process is still running.";
                case CommunicationProcessResult.Success:
                    return "Process completed successfully.";
                case CommunicationProcessResult.ConnectionError:
                    return "Messages could not be sent.";
                case CommunicationProcessResult.RetryLimitExceeded:
                    return "Maximum retry limit exceeded.";
                default:
                    return "Unknown state.";
            }
        }

        private void RunThread()
        {
            try
            {
                List<string> excludedSerialPortNames = new List<string>();

                BootloaderHelper bootloader = new BootloaderHelper();

                // send upload and disconnect
                if (ActiveConnection != null &&
                    ActiveConnection.IsConnected == true &&
                    ActiveConnection.ConnectionType == NgimuApi.ConnectionType.Serial)
                {
                    // get all the serial ports 
                    excludedSerialPortNames.AddRange(Helper.GetSerialPortNames());

                    CommunicationProcessResult resetResult;

                    this.Invoke(new Action<string>(OnInfo), "Sending upload command");

                    resetResult = Commands.Send(ActiveConnection, Command.Upload, Options.Timeout, Options.MaximumNumberOfRetries);

                    if (m_Cancel == true) { return; }

                    if (ActiveConnection != null && ActiveConnection.IsConnected == true)
                    {
                        this.Invoke(new Action<string>(OnInfo), "Closing connection");

                        // disconnect 			
                        this.Invoke(new Action(ActiveConnection.Close));
                    }

                    if (m_Cancel == true) { return; }

                    this.Invoke(new Action<string>(OnInfo), "Waiting for device to restart in bootloader mode");

                    // wait 5 seconds 			
                    Thread.CurrentThread.Join(5000);

                    if (m_Cancel == true) { return; }
                }

                try
                {
                    bool success = false;
                    foreach (string port in Helper.GetSerialPortNames())
                    {
                        if (excludedSerialPortNames.Contains(port) == true)
                        {
                            continue;
                        }

                        this.Invoke(new Action<string>(OnInfo), "Uploading firmware on " + port);

                        if (bootloader.UploadFirmware(m_FilePath, port) == true)
                        {
                            success = true;
                            break;
                        }
                    }

                    if (success == false)
                    {
                        this.Invoke(new Action<string, string>(ShowErrorMessage), "Failed to upload firmware.", "Error");

                        return;
                    }

                    m_Successful = success;
                }
                catch (Exception ex)
                {
                    this.Invoke(new Action<string, string>(ShowErrorMessage), ex.ToString(), "Exception");
                }

                if (m_Cancel == true) { return; }

                if (Options.SearchForConnectionsAfterSuccessfulUpload == false)
                {
                    return;
                }

                this.Invoke(new Action<string>(OnInfo), "Waiting for device to restart");

                // This is to allow the device to come out of bootloader mode
                Thread.CurrentThread.Join(1000);
            }
            catch (Exception ex)
            {
                this.Invoke(new Action<string, string>(ShowErrorMessage), ex.ToString(), "Exception");
            }
            finally
            {
                IsRunning = false;

                this.Invoke(new Action(Done));
            }
        }

        void OnInfo(string message)
        {
            m_Progress.ProgressMessage = message;
        }

        private void ShowErrorMessage(string text, string caption)
        {
            MessageBox.Show(this, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Done()
        {
            // we have to hide the dialog first otherwise it will stick around until the auto connect has completed
            m_Progress.Hide();
            m_Progress.Close();

            if (m_Successful == true && Options.SearchForConnectionsAfterSuccessfulUpload == true)
            {
                OnAutoConnectRequest();
            }

            m_UploadButton.Enabled = true;
            pathSelector.Enabled = true;
        }

        private void UploadFirmwareDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = IsRunning;

            if (e.Cancel == true)
            {
                FlashingDialogHelper.FlashWindowEx(this);
                return;
            }

            if (e.CloseReason == CloseReason.UserClosing || e.CloseReason == CloseReason.None)
            {
                Options.Windows[ID].IsOpen = false;
            }
        }

        public void Stop()
        {
            m_Cancel = true;

            if (m_Thread != null && m_Thread.IsAlive == true)
            {
                m_Thread.Join();
            }
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
    }
}
