using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using NgimuApi;
using NgimuApi.SearchForConnections;

namespace NgimuGui
{
    internal class WindowOptions
    {
        /// <summary>
        /// Is the window currently open.
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// The current state of the window (maximised or normal).
        /// </summary>
        public FormWindowState WindowState { get; set; }

        /// <summary>
        /// The bounds of the window on the desktop.
        /// </summary>
        public Rectangle Bounds { get; set; }
    }

    internal class WindowManager
    {
        private readonly Dictionary<string, WindowOptions> m_Windows = new Dictionary<string, WindowOptions>();

        public ICollection<string> Keys { get { return m_Windows.Keys; } }

        public void Clear()
        {
            m_Windows.Clear();
        }

        public bool Contains(string key)
        {
            return m_Windows.ContainsKey(key);
        }

        public WindowOptions this[string key]
        {
            get
            {
                if (Contains(key) == false)
                {
                    m_Windows.Add(key, new WindowOptions());
                }

                return m_Windows[key];
            }
        }
    }

    internal static class Options
    {
        /// <summary>
        /// The location of the options file
        /// </summary>
        private static string m_OptionsFileName = "~/Options.xml";

        /// <summary>
        /// The current state of the window (maximised or normal)
        /// </summary>
        public static FormWindowState WindowState { get; set; }

        /// <summary>
        /// The bounds of the window on the desktop
        /// </summary>
        public static Rectangle Bounds { get; set; }

        public static readonly WindowManager Windows = new WindowManager();

        public static bool SearchForConnectionsAtStartup { get; set; }

        public static bool OpenConnectionToFirstDeviceFound { get; set; }

        public static bool ConfigureUniqueUdpConnection { get; set; }

        public static bool ReadDeviceSettingsAfterOpeningConnection { get; set; }

        public static bool RememberWindowLayout { get; set; }

        public static bool DisconnectFromSerialAfterSleepAndResetCommands { get; set; }

        public static bool DisplayReceivedErrorMessagesInMessageBox { get; set; }

        public static ConnectionSearchTypes ConnectionSearchType { get; set; }

        public static readonly List<ConnectionManagerInfo> SerialConnections = new List<ConnectionManagerInfo>();

        public static readonly List<ConnectionManagerInfo> UdpConnections = new List<ConnectionManagerInfo>();

        public static bool AllowUploadWithoutSerialConnection { get; set; }

        public static bool SearchForConnectionsAfterSuccessfulUpload { get; set; }

        public static int MaximumNumberOfRetries { get; set; }

        public static int Timeout { get; set; }

        public static uint GraphSampleBufferSize { get; set; }

        /// <summary>
        /// Load the options
        /// </summary>
        public static void Load()
        {
            // set all options to their defaults
            SetDefaults();

            // check to see if there is a options file to load
            if (File.Exists(Helper.ResolvePath(m_OptionsFileName)) == false)
            {
                return;
            }

            // try to load the options
            Inner_Load();
        }

        public static void SetDefaults()
        {
            // set the bound to empty
            Bounds = Rectangle.Empty;

            // normal window state
            WindowState = FormWindowState.Normal;

            ConnectionSearchType = ConnectionSearchTypes.All;

            SearchForConnectionsAtStartup = true;

            OpenConnectionToFirstDeviceFound = false;

            ConfigureUniqueUdpConnection = true;

            ReadDeviceSettingsAfterOpeningConnection = true;

            RememberWindowLayout = true;

            DisconnectFromSerialAfterSleepAndResetCommands = true;

            DisplayReceivedErrorMessagesInMessageBox = true;

            AllowUploadWithoutSerialConnection = false;

            SearchForConnectionsAfterSuccessfulUpload = true;

            MaximumNumberOfRetries = 3;

            Timeout = 100;

            GraphSampleBufferSize = 24;

            Windows.Clear();
        }

        private static void Inner_Load()
        {
            try
            {
                XmlDocument doc = new XmlDocument();

                // load the options from the resolved path
                doc.Load(Helper.ResolvePath(m_OptionsFileName));

                XmlNode node = doc.DocumentElement;

                // if the node is not null
                if (node != null)
                {
                    ConnectionSearchType = Helper.GetAttributeValue(node, "ConnectionSearchType", ConnectionSearchType);

                    SearchForConnectionsAtStartup = Helper.GetAttributeValue(node, "SearchForConnectionsAtStartup", SearchForConnectionsAtStartup);

                    ConfigureUniqueUdpConnection = Helper.GetAttributeValue(node, "ConfigureUniqueUdpConnection", ConfigureUniqueUdpConnection);

                    ReadDeviceSettingsAfterOpeningConnection = Helper.GetAttributeValue(node, "ReadDeviceSettingsAfterOpeningConnection", ReadDeviceSettingsAfterOpeningConnection);

                    OpenConnectionToFirstDeviceFound = Helper.GetAttributeValue(node, "OpenConnectionToFirstDeviceFound", OpenConnectionToFirstDeviceFound);

                    RememberWindowLayout = Helper.GetAttributeValue(node, "RememberWindowLayout", RememberWindowLayout);

                    DisconnectFromSerialAfterSleepAndResetCommands = Helper.GetAttributeValue(node, "DisconnectFromSerialAfterSleepAndResetCommands", DisconnectFromSerialAfterSleepAndResetCommands);

                    DisplayReceivedErrorMessagesInMessageBox = Helper.GetAttributeValue(node, "DisplayReceivedErrorMessagesInMessageBox", DisplayReceivedErrorMessagesInMessageBox);

                    MaximumNumberOfRetries = Helper.GetAttributeValue(node, "MaximumNumberOfRetries", MaximumNumberOfRetries);

                    Timeout = Helper.GetAttributeValue(node, "Timeout", Timeout);

                    GraphSampleBufferSize = Helper.GetAttributeValue(node, "GraphSampleBufferSize", GraphSampleBufferSize);

                    AllowUploadWithoutSerialConnection = Helper.GetAttributeValue(node, "AllowUploadWithoutSerialConnection", AllowUploadWithoutSerialConnection);
                    SearchForConnectionsAfterSuccessfulUpload = Helper.GetAttributeValue(node, "SearchForConnectionsAfterSuccessfulUpload", SearchForConnectionsAfterSuccessfulUpload);

                    // get the string for the bounding rectangle
                    Bounds = CheckWindowBounds(Helper.GetAttributeValue(node, "Bounds", Bounds));

                    // get the window state
                    WindowState = Helper.GetAttributeValue(node, "WindowState", WindowState);

                    foreach (XmlNode windowNode in node.SelectNodes("Windows/Window"))
                    {
                        string name = Helper.GetAttributeValue(windowNode, "Name", null);

                        if (String.IsNullOrEmpty(name) == true)
                        {
                            continue;
                        }

                        if (Windows.Contains(name) == true)
                        {
                            continue;
                        }

                        WindowOptions options = Windows[name];

                        // get the open state
                        options.IsOpen = Helper.GetAttributeValue(windowNode, "IsOpen", options.IsOpen);

                        // get the window state
                        options.WindowState = Helper.GetAttributeValue(windowNode, "WindowState", options.WindowState);

                        // get the bounding rectangle
                        options.Bounds = CheckWindowBounds(Helper.GetAttributeValue(windowNode, "Bounds", options.Bounds));
                    }
                }
            }
            catch (Exception ex)
            {
                // something went wrong, tell the user
                MessageBox.Show(ex.Message, "Could not load options", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void Save()
        {
            try
            {
                XmlDocument doc = new XmlDocument();

                XmlElement node = Helper.CreateElement(doc, "Options");
                doc.AppendChild(node);

                Helper.AppendAttributeAndValue(node, "ConnectionSearchType", ConnectionSearchType);
                Helper.AppendAttributeAndValue(node, "SearchForConnectionsAtStartup", SearchForConnectionsAtStartup);
                Helper.AppendAttributeAndValue(node, "OpenConnectionToFirstDeviceFound", OpenConnectionToFirstDeviceFound);
                Helper.AppendAttributeAndValue(node, "ConfigureUniqueUdpConnection", ConfigureUniqueUdpConnection);
                Helper.AppendAttributeAndValue(node, "ReadDeviceSettingsAfterOpeningConnection", ReadDeviceSettingsAfterOpeningConnection);

                Helper.AppendAttributeAndValue(node, "RememberWindowLayout", RememberWindowLayout);
                Helper.AppendAttributeAndValue(node, "DisconnectFromSerialAfterSleepAndResetCommands", DisconnectFromSerialAfterSleepAndResetCommands);
                Helper.AppendAttributeAndValue(node, "DisplayReceivedErrorMessagesInMessageBox", DisplayReceivedErrorMessagesInMessageBox);

                Helper.AppendAttributeAndValue(node, "MaximumNumberOfRetries", MaximumNumberOfRetries);
                Helper.AppendAttributeAndValue(node, "Timeout", Timeout);
                Helper.AppendAttributeAndValue(node, "GraphSampleBufferSize", GraphSampleBufferSize);

                Helper.AppendAttributeAndValue(node, "AllowUploadWithoutSerialConnection", AllowUploadWithoutSerialConnection);
                Helper.AppendAttributeAndValue(node, "SearchForConnectionsAfterSuccessfulUpload", SearchForConnectionsAfterSuccessfulUpload);

                if (RememberWindowLayout == true)
                {
                    Helper.AppendAttributeAndValue(node, "Bounds", Bounds);
                    Helper.AppendAttributeAndValue(node, "WindowState", WindowState);

                    XmlElement windows = Helper.CreateElement(doc, "Windows");

                    foreach (string name in Windows.Keys)
                    {
                        WindowOptions options = Windows[name];

                        XmlElement window = Helper.CreateElement(doc, "Window");

                        Helper.AppendAttributeAndValue(window, "Name", name);

                        Helper.AppendAttributeAndValue(window, "IsOpen", options.IsOpen);

                        Helper.AppendAttributeAndValue(window, "WindowState", options.WindowState);

                        Helper.AppendAttributeAndValue(window, "Bounds", options.Bounds);

                        windows.AppendChild(window);
                    }

                    node.AppendChild(windows);
                }

                Helper.EnsurePathExists(Helper.ResolvePath(m_OptionsFileName));
                doc.Save(Helper.ResolvePath(m_OptionsFileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Could not save options", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static Rectangle CheckWindowBounds(Rectangle bounds)
        {
            // if the bounds is not empty
            if (bounds != Rectangle.Empty)
            {
                // check that the bounds is on the screen
                if (IsOnScreen(bounds) == false)
                {
                    // if the bounds is off the screen set it to empty
                    bounds = Rectangle.Empty;
                }
            }

            return bounds;
        }

        /// <summary>
        /// Check that a rectangle is fully on the screen
        /// </summary>
        /// <param name="rectangle">the rectangle to check</param>
        /// <returns>true if the rectangle is fully on a screen</returns>
        private static bool IsOnScreen(Rectangle rectangle)
        {
            Screen[] screens = Screen.AllScreens;
            foreach (Screen screen in screens)
            {
                if (screen.WorkingArea.Contains(rectangle))
                {
                    return true;
                }
            }

            return false;
        }
    }
}