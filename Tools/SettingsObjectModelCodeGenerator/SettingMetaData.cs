using System;

namespace SettingsObjectModelCodeGenerator
{
    //class ImuSettingMetaData
    enum SettingValueType
    {
        RotationMatrix,
        Vector3,

        String,

        Bool,
        MacAddress,
        IPAddress,

        WifiMode,
        WifiAntenna,
        WifiRegion,
        WifiBand,
        Wifi2GHzChannel,
        Wifi5GHzChannel,

        CpuIdleMode,

        UdpPort,
        UInt32,
        Float,
        Int32,
    }

    class SettingValue : ISettingMetaData
    {
        public int CategoryPrefix;

        /// <summary>
        /// Gets the .NET type of the value. 
        /// </summary>
        public string ValueType
        {
            get
            {
                switch (SettingValueType)
                {
                    case SettingValueType.RotationMatrix:
                        return "RotationMatrix"; 
                    case SettingValueType.Vector3:
                        return "Vector3"; 
                    case SettingValueType.String:
                        return "string"; 
                    case SettingValueType.Bool:
                        return "bool"; 
                    case SettingValueType.MacAddress:
                        return "PhysicalAddress";
                    case SettingValueType.IPAddress:
                        return "IPAddress"; 
                    case SettingValueType.WifiMode:
                        return "WifiMode";
                    case SettingValueType.WifiAntenna:
                        return "WifiAntenna";
                    case SettingValueType.WifiRegion:
                        return "WifiRegion";
                    case SettingValueType.WifiBand:
                        return "WifiBand"; 
                    case SettingValueType.Wifi2GHzChannel:
                        return "Wifi2GHzChannel"; 
                    case SettingValueType.Wifi5GHzChannel:
                        return "Wifi5GHzChannel";
                    case SettingValueType.CpuIdleMode:
                        return "CpuIdleMode";
                    case SettingValueType.UdpPort:
                        return "ushort"; 
                    case SettingValueType.UInt32:
                        return "uint"; 
                    case SettingValueType.Float:
                        return "float"; 
                    case SettingValueType.Int32:
                        return "int"; 
                    default:
                        throw new Exception("Unknown setting value type."); 
                }
            }
        }

        public string SettingInstanceType
        {
            get
            {
                switch (SettingValueType)
                {
                    case SettingValueType.RotationMatrix:
                        return "SettingValue_RotationMatrix";
                    case SettingValueType.Vector3:
                        return "SettingValue_Vector3";
                    case SettingValueType.String:
                        return "SettingValue_String";
                    case SettingValueType.Bool:
                        return "SettingValue_Bool";
                    case SettingValueType.MacAddress:
                        return "SettingValue_MacAddress";
                    case SettingValueType.IPAddress:
                        return "SettingValue_IPAddress";
                    case SettingValueType.WifiMode:
                        return "SettingValue_WifiMode";
                    case SettingValueType.WifiAntenna:
                        return "SettingValue_WifiAntenna";
                    case SettingValueType.WifiRegion:
                        return "SettingValue_WifiRegion";
                    case SettingValueType.WifiBand:
                        return "SettingValue_WifiBand";
                    case SettingValueType.Wifi2GHzChannel:
                        return "SettingValue_Wifi2GHzChannel";
                    case SettingValueType.Wifi5GHzChannel:
                        return "SettingValue_Wifi5GHzChannel";
                    case SettingValueType.CpuIdleMode:
                        return "SettingValue_CpuIdleMode";
                    case SettingValueType.UdpPort:
                        return "SettingValue_UdpPort";
                    case SettingValueType.UInt32:
                        return "SettingValue_UInt32";
                    case SettingValueType.Float:
                        return "SettingValue_Float";
                    case SettingValueType.Int32:
                        return "SettingValue_Int32";
                    default:
                        throw new Exception("Unknown setting value type.");
                }
            }
        }

        /// <summary>
        /// Gets the IMU value type. 
        /// </summary>
        public SettingValueType SettingValueType { get; set;  }

        /// <summary>
        /// Is this variable readonly. 
        /// </summary>
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// Is this variable hidden. 
        /// </summary>
        public bool IsHidden { get; set; }

        public bool IsGroup { get { return false; } }

        public string UIText { get; set; }

        public string DocumentationTitle { get; set; }

        /// <summary>
        /// Gets the description of this variable.
        /// </summary>
        public string DocumentationBody { get; set; }

        public string Category { get; set; }

        public string CategoryText { get; set; }

        public string CategoryFullMemberName { get; set; }

        public string OscAddress { get; set; }

        public string MemberName { get; set; }
    }
}
