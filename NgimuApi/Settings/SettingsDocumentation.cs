using System;
using System.Collections.Generic;
using System.Xml;

namespace NgimuApi
{
    /// <summary>
    /// Settings loaded from an embedded source. 
    /// </summary>
    public static class SettingsDocumentation
    {
        private static Dictionary<string, string> documentationLookup = new Dictionary<string, string>();

        /// <summary>
        /// Gets the text to display when no setting is selected. This is a candidate to be moved out of the API. 
        /// </summary>
        public static string NoItemSelectedText { get; private set; }

        public static string GetSettingDocumentation(string address)
        {
            return documentationLookup[address];
        }

        /// <summary>
        /// Load the settings from the embedded source. 
        /// </summary>
        static SettingsDocumentation()
        {
            Load(EmbeddedFiles.SettingsXml);
        }

        /// <summary>
        /// Does the settings contain a setting with a given OSC address.
        /// </summary>
        /// <param name="address">A OSC address.</param>
        /// <returns>True if a setting exists with the supplied OSC address.</returns>
        public static bool ContainsAddress(string address)
        {
            return documentationLookup.ContainsKey(address);
        }

        private static void Load(string xml)
        {
            try
            {
                XmlDocument doc = new XmlDocument();

                doc.LoadXml(xml);

                XmlNode node = doc.DocumentElement;

                if (node == null)
                {
                    throw new Exception(Strings.Settings_MissingRootNode);
                }

                NoItemSelectedText = node.SelectSingleNode("NoItemSelectedText").InnerText.Trim();

                foreach (XmlNode settingNode in node.SelectNodes("Setting"))
                {
                    documentationLookup.Add(
                        Helper.GetAttributeValue(settingNode, "Address", string.Empty),
                        settingNode.InnerText
                    );
                }
            }
            catch (Exception ex)
            {
                throw new Exception(Strings.Settings_CouldNotLoad, ex);
            }
        }
    }
}
