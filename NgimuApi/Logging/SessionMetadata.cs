using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace NgimuApi.Logging
{
    public sealed class SessionMetadata
    {
        public readonly ApplicationInformationNode ApplicationInformation = new ApplicationInformationNode();
        public readonly List<DeviceMetadata> Devices = new List<DeviceMetadata>();

        public readonly SessionInformationNode SessionInformation = new SessionInformationNode();
        public readonly UserInformationNode UserInformation = new UserInformationNode();

        public SessionMetadata()
        {
            ApplicationInformation.ApplicationName = Assembly.GetEntryAssembly().GetName().Name;
            ApplicationInformation.ApplicationVersion = Assembly.GetEntryAssembly().GetName().Version.ToString();
            ApplicationInformation.ApiVersion = typeof(DeviceMetadata).Assembly.GetName().Version.ToString();

            UserInformation.UserName = Environment.UserName;
            UserInformation.MachineName = Environment.MachineName;
        }

        public void Save(string filepath)
        {
            try
            {
                XmlDocument doc = new XmlDocument();

                XmlElement node = Helper.CreateElement(doc, "Session");
                doc.AppendChild(node);

                SessionInformation.Save(node);

                ApplicationInformation.Save(node);

                UserInformation.Save(node);

                SaveDeviceInformation(node);

                doc.Save(filepath);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not save session meta data.", ex);
            }
        }

        private void SaveDeviceInformation(XmlElement parent)
        {
            XmlElement node = Helper.CreateElement(parent, "Devices");

            foreach (DeviceMetadata device in Devices)
            {
                SaveDeviceInformation(node, device);
            }

            parent.AppendChild(node);
        }

        private void SaveDeviceInformation(XmlElement parent, DeviceMetadata device)
        {
            XmlElement node = Helper.CreateElement(parent, "Device");

            Helper.AppendAttributeAndValue(node, "SerialNumber", device.DeviceInformation.SerialNumber);
            Helper.AppendAttributeAndValue(node, "DeviceName", device.DeviceInformation.DeviceName);
            Helper.AppendAttributeAndValue(node, "ConnectionType", device.ConnectionInformation.ConnectionType.ToString());
            Helper.AppendAttributeAndValue(node, "DirectoryName", device.DirectoryName);

            parent.AppendChild(node);
        }

        public sealed class ApplicationInformationNode
        {
            public string ApiVersion { get; set; }

            public string ApplicationName { get; set; }

            public string ApplicationVersion { get; set; }

            public void Save(XmlElement parent)
            {
                XmlElement node = Helper.CreateElement(parent, "ApplicationInformation");

                Helper.AppendAttributeAndValue(node, "ApplicationName", ApplicationName);
                Helper.AppendAttributeAndValue(node, "ApplicationVersion", ApplicationVersion);
                Helper.AppendAttributeAndValue(node, "ApiVersion", ApiVersion);

                parent.AppendChild(node);
            }
        }

        public sealed class SessionInformationNode
        {
            public DateTime Date { get; set; }

            public TimeSpan Duration { get; set; }

            public string Name { get; set; }

            public void Save(XmlElement parent)
            {
                XmlElement node = Helper.CreateElement(parent, "SessionInformation");

                Helper.AppendAttributeAndValue(node, "Name", Name);
                Helper.AppendAttributeAndValue(node, "Date", Helper.DateTimeToString(Date));
                Helper.AppendAttributeAndValue(node, "Duration", Helper.TimeSpanToString(Duration, TimeSpanStringFormat.StopWatch));

                parent.AppendChild(node);
            }
        }

        public sealed class UserInformationNode
        {
            public string MachineName { get; set; }

            public string UserName { get; set; }

            public void Save(XmlElement parent)
            {
                XmlElement node = Helper.CreateElement(parent, "UserInformation");

                Helper.AppendAttributeAndValue(node, "UserName", UserName);
                Helper.AppendAttributeAndValue(node, "MachineName", MachineName);

                parent.AppendChild(node);
            }
        }
    }
}