using System;
using System.Collections.Generic;
using System.Xml;
using Rug.Osc;

namespace NgimuApi.Logging
{
    public sealed class DeviceMetadata
    {
        public readonly ConnectionInformationNode ConnectionInformation = new ConnectionInformationNode();
        public readonly DeviceInformationNode DeviceInformation = new DeviceInformationNode();
        public readonly Dictionary<string, long> FilesCreated = new Dictionary<string, long>();
        public readonly StatisticsNode Statistics = new StatisticsNode();
        public readonly TimeStampsNode TimeStamps = new TimeStampsNode();

        public void Save(string filePath)
        {
            XmlDocument doc = new XmlDocument();

            XmlElement device = Helper.CreateElement(doc, "Device");
            doc.AppendChild(device);

            DeviceInformation.Save(device);
            ConnectionInformation.Save(device);
            TimeStamps.Save(device);

            XmlElement filesCreatedNode = Helper.CreateElement(device, "FilesCreated");
            device.AppendChild(filesCreatedNode);

            foreach (KeyValuePair<string, long> files in FilesCreated)
            {
                XmlElement fileNode = Helper.CreateElement(filesCreatedNode, "File");
                filesCreatedNode.AppendChild(fileNode);

                Helper.AppendAttributeAndValue(fileNode, "Name", files.Key);
                Helper.AppendAttributeAndValue(fileNode, "Messages", files.Value);
            }

            Statistics.Save(device);

            doc.Save(filePath);
        }

        public sealed class ConnectionInformationNode
        {
            public IConnectionInfo ConnectionInfo { get; set; }

            public ConnectionType ConnectionType { get; set; }

            internal void Save(XmlElement parent)
            {
                XmlElement node = Helper.CreateElement(parent, "ConnectionInformation");

                Helper.AppendAttributeAndValue(node, "ConnectionType", ConnectionType.ToString());

                if (ConnectionInfo is UdpConnectionInfo)
                {
                    UdpConnectionInfo info = ConnectionInfo as UdpConnectionInfo;

                    Helper.AppendAttributeAndValue(node, "SendIPAddress", info.SendIPAddress.ToString());
                    Helper.AppendAttributeAndValue(node, "SendPort", info.SendPort.ToString());

                    Helper.AppendAttributeAndValue(node, "AdapterIPAddress", info.AdapterIPAddress.ToString());
                    Helper.AppendAttributeAndValue(node, "ReceivePort", info.ReceivePort);

                    Helper.AppendAttributeAndValue(node, "AdapterName", info.AdapterName);
                }
                else if (ConnectionInfo is SerialConnectionInfo)
                {
                    SerialConnectionInfo info = ConnectionInfo as SerialConnectionInfo;

                    Helper.AppendAttributeAndValue(node, "PortName", info.PortName);
                    Helper.AppendAttributeAndValue(node, "BaudRate", info.BaudRate);
                    Helper.AppendAttributeAndValue(node, "RtsCtsEnabled", info.RtsCtsEnabled);
                }
                else if (ConnectionInfo is SDCardFileConnectionInfo)
                {
                    SDCardFileConnectionInfo info = ConnectionInfo as SDCardFileConnectionInfo;

                    Helper.AppendAttributeAndValue(node, "FilePath", info.FilePath);
                }

                parent.AppendChild(node);
            }
        }

        public sealed class DeviceInformationNode
        {
            public string BootloaderVersion { get; set; }

            public string DeviceName { get; set; }

            public string FirmwareVersion { get; set; }

            public string HardwareVersion { get; set; }

            public string SerialNumber { get; set; }

            public DeviceInformationNode()
            {
                DeviceName = "N/A";
                SerialNumber = "N/A";
                FirmwareVersion = "N/A";
                BootloaderVersion = "N/A";
                HardwareVersion = "N/A";
            }

            internal void Save(XmlElement parent)
            {
                XmlElement node = Helper.CreateElement(parent, "DeviceInformation");

                Helper.AppendAttributeAndValue(node, "DeviceName", DeviceName);
                Helper.AppendAttributeAndValue(node, "SerialNumber", SerialNumber);
                Helper.AppendAttributeAndValue(node, "FirmwareVersion", FirmwareVersion);
                Helper.AppendAttributeAndValue(node, "BootloaderVersion", BootloaderVersion);
                Helper.AppendAttributeAndValue(node, "HardwareVersion", HardwareVersion);

                parent.AppendChild(node);
            }

            internal void SetValues(SettingsCategoryTypes.DeviceInformation deviceInformation)
            {
                DeviceName = deviceInformation.DeviceName.Value;
                SerialNumber = deviceInformation.SerialNumber.Value;
                FirmwareVersion = deviceInformation.FirmwareVersion.HasRemoteValue == true ? deviceInformation.FirmwareVersion.Value : "N/A";
                BootloaderVersion = deviceInformation.BootloaderVersion.HasRemoteValue == true ? deviceInformation.BootloaderVersion.Value : "N/A";
                HardwareVersion = deviceInformation.HardwareVersion.HasRemoteValue == true ? deviceInformation.HardwareVersion.Value : "N/A";
            }
        }

        public sealed class StatisticsNode
        {
            public long BundlesReceived { get; set; }

            public long BytesReceived { get; set; }

            public long MessagesReceived { get; set; }

            public long PacketsReceived { get; set; }

            public long ReceiveErrors { get; set; }

            internal void Save(XmlElement parent)
            {
                XmlElement node = Helper.CreateElement(parent, "Statistics");

                Helper.AppendAttributeAndValue(node, "BytesReceived", BytesReceived);
                Helper.AppendAttributeAndValue(node, "PacketsReceived", PacketsReceived);
                Helper.AppendAttributeAndValue(node, "BundlesReceived", BundlesReceived);
                Helper.AppendAttributeAndValue(node, "MessagesReceived", MessagesReceived);
                Helper.AppendAttributeAndValue(node, "ReceiveErrors", ReceiveErrors);

                parent.AppendChild(node);
            }

            internal void SetValues(OscCommunicationStatistics statistics)
            {
                BytesReceived = statistics.BytesReceived.Total;
                PacketsReceived = statistics.PacketsReceived.Total;
                BundlesReceived = statistics.BundlesReceived.Total;
                MessagesReceived = statistics.MessagesReceived.Total;
                ReceiveErrors = statistics.ReceiveErrors.Total;
            }
        }

        public sealed class TimeStampsNode
        {
            public OscTimeTag FirstTimeTag { get; set; }

            public OscTimeTag LastTimeTag { get; set; }

            internal void Save(XmlElement parent)
            {
                DateTime startTime = FirstTimeTag.ToDataTime();
                DateTime endTime = LastTimeTag.ToDataTime();

                TimeSpan duration = endTime - startTime;

                XmlElement node = Helper.CreateElement(parent, "TimeStamps");

                Helper.AppendAttributeAndValue(node, "First", Helper.DateTimeToString(startTime, true));
                Helper.AppendAttributeAndValue(node, "Last", Helper.DateTimeToString(endTime, true));
                Helper.AppendAttributeAndValue(node, "TimeSpan", Helper.TimeSpanToString(duration, TimeSpanStringFormat.StopWatch));

                parent.AppendChild(node);
            }
        }
    }
}