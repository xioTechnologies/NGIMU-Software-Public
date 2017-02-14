using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using NgimuApi;
using NgimuApi.SearchForConnections;
using NgimuForms;

namespace NgimuGui
{
    internal static class Options
    {
        /// <summary>
        /// The location of the options file
        /// </summary>
        private static string m_OptionsFileName = "~/Options.xml";

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

            WindowManager.Clear();
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

                    WindowManager.Load(node.SelectSingleNode("Windows"));
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
                    WindowManager.Save(node);
                }

                Helper.EnsurePathExists(Helper.ResolvePath(m_OptionsFileName));

                doc.Save(Helper.ResolvePath(m_OptionsFileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Could not save options", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}