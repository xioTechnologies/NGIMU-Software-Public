using System;
using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using NgimuApi.Maths;
using Rug.Osc;

namespace NgimuApi
{
    internal abstract class SettingValueBase : ISettingValue
    {
        private string documentationBody;

        #region IRemoteVariable Members

        public abstract SettingValueType SettingValueType { get; }

        public bool IsReadOnly { get; private set; }

        public bool IsHidden { get; private set; }

        public SettingCategrory Category { get; private set; }

        public string Name { get; private set; }

        public string DocumentationTitle { get; private set; }

        public string OscAddress { get; private set; }

        public CommunicationProcessResult? CommunicationResult { get; set; }

        public override string ToString()
        {
            return Name + "; " + Message.ToString();
        }

        public string Documentation
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                // TODO This text needs sorting out for multiligual support
                sb.AppendLine(@"{\rtf1\ansi\ansicpg1252\deff0\deflang2057{\fonttbl{\f0\fnil\fcharset0 Calibri;}{\f1\fnil\fcharset0 Courier New;}}");
                sb.AppendLine(@"\viewkind4\uc1\pard\sa200\sl276\slmult1\lang9");
                sb.AppendLine(@"{\b\f0\fs22 " + DocumentationTitle + (IsReadOnly ? " (read-only)" : "") + @"} \par");

                sb.Append(@"\f0\fs18 ");

                string[] descriptionLines = documentationBody.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                if (descriptionLines.Length > 1 || (descriptionLines.Length == 1 && String.IsNullOrEmpty(descriptionLines[0]) == false))
                {
                    foreach (string line in descriptionLines)
                    {
                        sb.AppendLine(line.TrimStart());
                    }
                    sb.AppendLine(@" \par"); // only insert new paraphraph if description was not blank
                }

                sb.AppendLine(@"\f0 OSC message to get value:\line");
                sb.AppendLine(@"\f1 " + OscAddress + @"\f0\par");

                if (IsReadOnly == false)
                {
                    sb.AppendLine(@"\f0 OSC message to set value:\line");
                    sb.AppendLine(@"\f1 " + Message.ToString() + @"\f0\par");
                }

                sb.AppendLine(@"}");

                return sb.ToString();
            }
        }

        public Type ValueType { get; private set; }

        public bool HasRemoteValue { get; protected set; }

        public bool IsValueUndefined { get; protected set; }

        public abstract OscMessage Message { get; }

        internal SettingValueBase(SettingCategrory category, string name, string title, string description, string oscAddress, Type valueType, bool isReadonly, bool isHidden)
        {
            Category = category;

            Name = name;
            DocumentationTitle = title;

            documentationBody = description;
            OscAddress = oscAddress;

            ValueType = valueType;

            IsReadOnly = isReadonly;
            IsHidden = isHidden;

            HasRemoteValue = false;
            IsValueUndefined = true;
        }

        public abstract void OnMessageReceived(OscMessage message);

        public abstract void OnLocalMessage(OscMessage message);

        public abstract void ResetRemoteValueState();

        public abstract void SetValue(object value);

        public abstract object GetValue();

        public abstract void SetRemoteValue(object value);

        public abstract object GetRemoteValue();

        #endregion

        #region Callback Members

        public bool HasCallbackCompleted
        {
            get { return HasRemoteValue; }
        }

        public void ResetCallbackState()
        {
            CommunicationResult = null;

            ResetRemoteValueState();
        }

        #endregion

        #region Read / Write Methods

        public CommunicationProcessResult Read(int timeout = 100, int retryLimit = 3)
        {
            return Read(null, timeout, retryLimit);
        }

        public CommunicationProcessResult Read(IReporter reporter, int timeout = 100, int retryLimit = 3)
        {
            return Category.Read(new ISettingItem[] { this }, reporter, timeout, retryLimit);
        }


        public void ReadAync(int timeout = 100, int retryLimit = 3)
        {
            ReadAync(null, timeout, retryLimit);
        }

        public void ReadAync(IReporter reporter = null, int timeout = 100, int retryLimit = 3)
        {
            Category.ReadAync(new ISettingItem[] { this }, reporter, timeout, retryLimit);
        }


        public CommunicationProcessResult Write(int timeout = 100, int retryLimit = 3)
        {
            return Write(null, timeout, retryLimit);
        }

        public CommunicationProcessResult Write(IReporter reporter = null, int timeout = 100, int retryLimit = 3)
        {
            return Category.Write(new ISettingItem[] { this }, reporter, timeout, retryLimit);
        }


        public void WriteAync(int timeout = 100, int retryLimit = 3)
        {
            WriteAync(null, timeout, retryLimit);
        }

        public void WriteAync(IReporter reporter = null, int timeout = 100, int retryLimit = 3)
        {
            Category.WriteAync(new ISettingItem[] { this }, reporter, timeout, retryLimit);
        }

        #endregion
    }

    internal abstract class SettingValue<T> : SettingValueBase, ISettingValue<T>
    {
        private T remoteValue;
        private T value;

        public T RemoteValue
        {
            get { return remoteValue; }
            protected set
            {
                remoteValue = value;
            }
        }

        public T Value
        {
            get { return value; }
            set
            {
                this.value = value;
                IsValueUndefined = false;
            }
        }

        public SettingValue(SettingCategrory category, string name, string title, string description, string oscAddress, bool isReadonly, bool isHidden)
            : base(category, name, title, description, oscAddress, typeof(T), isReadonly, isHidden)
        {

        }

        public override void SetValue(object value)
        {
            if (value is T)
            {
                Value = (T)value;
            }
        }

        public override object GetValue()
        {
            return Value;
        }


        public override void SetRemoteValue(object value)
        {
            if (value is T)
            {
                RemoteValue = (T)value;
            }
        }

        public override object GetRemoteValue()
        {
            return RemoteValue;
        }

        public override void ResetRemoteValueState()
        {
            HasRemoteValue = false;
        }

        protected bool ValidateMessage(OscMessage message)
        {
            if (message.Address != OscAddress)
            {
                return false;
            }

            if (message.Count <= 0)
            {
                return false;
            }

            return true;
        }
    }


    internal sealed class SettingValue_String : SettingValue<string>
    {
        public override SettingValueType SettingValueType { get { return SettingValueType.String; } }

        public override OscMessage Message { get { return new OscMessage(OscAddress, Value); } }

        public SettingValue_String(SettingCategrory category, string name, string title, string description, string oscAddress, bool isReadonly, bool isHidden)
            : base(category, name, title, description, oscAddress, isReadonly, isHidden)
        {
            Value = String.Empty;
            RemoteValue = String.Empty;
            IsValueUndefined = true;
        }

        public bool TryGetValue(OscMessage message, out string value)
        {
            value = default(string);

            if (ValidateMessage(message) == false)
            {
                return false;
            }

            if ((message[0] is string) == false)
            {
                return false;
            }

            value = (string)message[0];

            return true;
        }

        public override void OnMessageReceived(OscMessage message)
        {
            string value;

            if (TryGetValue(message, out value) == false)
            {
                return;
            }

            RemoteValue = value;
            Value = RemoteValue;

            HasRemoteValue = true;

            return;
        }

        public override void OnLocalMessage(OscMessage message)
        {
            string value;

            if (TryGetValue(message, out value) == false)
            {
                return;
            }

            Value = value;

            return;
        }
    }

    internal sealed class SettingValue_MacAddress : SettingValue<PhysicalAddress>
    {
        public override SettingValueType SettingValueType { get { return SettingValueType.MacAddress; } }

        public override OscMessage Message { get { return new OscMessage(OscAddress, Value.ToString()); } }

        public SettingValue_MacAddress(SettingCategrory category, string name, string title, string description, string oscAddress, bool isReadonly, bool isHidden)
            : base(category, name, title, description, oscAddress, isReadonly, isHidden)
        {
            Value = PhysicalAddress.None;
            RemoteValue = PhysicalAddress.None;
            IsValueUndefined = true;
        }

        public bool TryGetValue(OscMessage message, out PhysicalAddress value)
        {
            value = default(PhysicalAddress);

            if (ValidateMessage(message) == false)
            {
                return false;
            }

            if ((message[0] is string) == false)
            {
                return false;
            }

            value = PhysicalAddress.Parse((string)message[0]);

            return true;
        }

        public override void OnMessageReceived(OscMessage message)
        {
            PhysicalAddress value;

            if (TryGetValue(message, out value) == false)
            {
                return;
            }

            RemoteValue = value;
            Value = RemoteValue;

            HasRemoteValue = true;

            return;
        }

        public override void OnLocalMessage(OscMessage message)
        {
            PhysicalAddress value;

            if (TryGetValue(message, out value) == false)
            {
                return;
            }

            Value = value;

            return;
        }
    }

    internal sealed class SettingValue_IPAddress : SettingValue<IPAddress>
    {
        public override SettingValueType SettingValueType { get { return SettingValueType.IPAddress; } }

        public override OscMessage Message { get { return new OscMessage(OscAddress, Value.ToString()); } }

        public SettingValue_IPAddress(SettingCategrory category, string name, string title, string description, string oscAddress, bool isReadonly, bool isHidden)
            : base(category, name, title, description, oscAddress, isReadonly, isHidden)
        {
            Value = IPAddress.Any;
            RemoteValue = IPAddress.Any;
            IsValueUndefined = true;
        }

        public bool TryGetValue(OscMessage message, out IPAddress value)
        {
            value = default(IPAddress);

            if (ValidateMessage(message) == false)
            {
                return false;
            }

            if ((message[0] is string) == false)
            {
                return false;
            }

            value = IPAddress.Parse((string)message[0]);

            return true;
        }

        public override void OnMessageReceived(OscMessage message)
        {
            IPAddress value;

            if (TryGetValue(message, out value) == false)
            {
                return;
            }

            RemoteValue = value;
            Value = RemoteValue;

            HasRemoteValue = true;

            return;
        }

        public override void OnLocalMessage(OscMessage message)
        {
            IPAddress value;

            if (TryGetValue(message, out value) == false)
            {
                return;
            }

            Value = value;

            return;
        }
    }

    internal sealed class SettingValue_Float : SettingValue<float>
    {
        public override SettingValueType SettingValueType { get { return SettingValueType.Float; } }

        public override OscMessage Message { get { return new OscMessage(OscAddress, Value); } }

        public SettingValue_Float(SettingCategrory category, string name, string title, string description, string oscAddress, bool isReadonly, bool isHidden)
            : base(category, name, title, description, oscAddress, isReadonly, isHidden)
        {

        }

        public bool TryGetValue(OscMessage message, out float value)
        {
            value = default(float);

            if (ValidateMessage(message) == false)
            {
                return false;
            }

            if ((message[0] is float) == false)
            {
                return false;
            }

            value = (float)message[0];

            return true;
        }

        public override void OnMessageReceived(OscMessage message)
        {
            float value;

            if (TryGetValue(message, out value) == false)
            {
                return;
            }

            RemoteValue = value;
            Value = RemoteValue;

            HasRemoteValue = true;

            return;
        }

        public override void OnLocalMessage(OscMessage message)
        {
            float value;

            if (TryGetValue(message, out value) == false)
            {
                return;
            }

            Value = value;

            return;
        }
    }

    internal sealed class SettingValue_Bool : SettingValue<bool>
    {
        public override SettingValueType SettingValueType { get { return SettingValueType.Bool; } }

        public override OscMessage Message { get { return new OscMessage(OscAddress, Value); } }

        public SettingValue_Bool(SettingCategrory category, string name, string title, string description, string oscAddress, bool isReadonly, bool isHidden)
            : base(category, name, title, description, oscAddress, isReadonly, isHidden)
        {

        }

        public bool TryGetValue(OscMessage message, out bool value)
        {
            value = default(bool);

            if (ValidateMessage(message) == false)
            {
                return false;
            }

            if ((message[0] is bool) == false)
            {
                return false;
            }

            value = (bool)message[0];

            return true;
        }

        public override void OnMessageReceived(OscMessage message)
        {
            bool value;

            if (TryGetValue(message, out value) == false)
            {
                return;
            }

            RemoteValue = value;
            Value = RemoteValue;

            HasRemoteValue = true;

            return;
        }

        public override void OnLocalMessage(OscMessage message)
        {
            bool value;

            if (TryGetValue(message, out value) == false)
            {
                return;
            }

            Value = value;

            return;
        }
    }

    internal sealed class SettingValue_Int32 : SettingValue<Int32>
    {
        public override SettingValueType SettingValueType { get { return SettingValueType.Int32; } }

        public override OscMessage Message { get { return new OscMessage(OscAddress, Value); } }

        public SettingValue_Int32(SettingCategrory category, string name, string title, string description, string oscAddress, bool isReadonly, bool isHidden)
            : base(category, name, title, description, oscAddress, isReadonly, isHidden)
        {

        }

        public bool TryGetValue(OscMessage message, out int value)
        {
            value = default(int);

            if (ValidateMessage(message) == false)
            {
                return false;
            }

            if ((message[0] is int) == false)
            {
                return false;
            }

            value = (int)message[0];

            return true;
        }

        public override void OnMessageReceived(OscMessage message)
        {
            int value;

            if (TryGetValue(message, out value) == false)
            {
                return;
            }

            RemoteValue = value;
            Value = RemoteValue;

            HasRemoteValue = true;

            return;
        }

        public override void OnLocalMessage(OscMessage message)
        {
            int value;

            if (TryGetValue(message, out value) == false)
            {
                return;
            }

            Value = value;

            return;
        }
    }

    internal sealed class SettingValue_UInt32 : SettingValue<UInt32>
    {
        public override SettingValueType SettingValueType { get { return SettingValueType.UInt32; } }

        public override OscMessage Message { get { return new OscMessage(OscAddress, unchecked((int)Value)); } }

        public SettingValue_UInt32(SettingCategrory category, string name, string title, string description, string oscAddress, bool isReadonly, bool isHidden)
            : base(category, name, title, description, oscAddress, isReadonly, isHidden)
        {

        }

        public bool TryGetValue(OscMessage message, out uint value)
        {
            value = default(uint);

            if (ValidateMessage(message) == false)
            {
                return false;
            }

            if ((message[0] is int) == false)
            {
                return false;
            }

            value = unchecked((uint)(int)message[0]);

            return true;
        }

        public override void OnMessageReceived(OscMessage message)
        {
            uint value;

            if (TryGetValue(message, out value) == false)
            {
                return;
            }

            RemoteValue = value;
            Value = RemoteValue;

            HasRemoteValue = true;

            return;
        }

        public override void OnLocalMessage(OscMessage message)
        {
            uint value;

            if (TryGetValue(message, out value) == false)
            {
                return;
            }

            Value = value;

            return;
        }
    }

    internal sealed class SettingValue_UdpPort : SettingValue<UInt16>
    {
        public override SettingValueType SettingValueType { get { return SettingValueType.UdpPort; } }

        public override OscMessage Message { get { return new OscMessage(OscAddress, unchecked((int)Value)); } }

        public SettingValue_UdpPort(SettingCategrory category, string name, string title, string description, string oscAddress, bool isReadonly, bool isHidden)
            : base(category, name, title, description, oscAddress, isReadonly, isHidden)
        {

        }

        public bool TryGetValue(OscMessage message, out ushort value)
        {
            value = default(ushort);

            if (ValidateMessage(message) == false)
            {
                return false;
            }

            if ((message[0] is int) == false)
            {
                return false;
            }

            value = unchecked((ushort)(int)message[0]);

            return true;
        }

        public override void OnMessageReceived(OscMessage message)
        {
            ushort value;

            if (TryGetValue(message, out value) == false)
            {
                return;
            }

            RemoteValue = value;
            Value = RemoteValue;

            HasRemoteValue = true;

            return;
        }

        public override void OnLocalMessage(OscMessage message)
        {
            ushort value;

            if (TryGetValue(message, out value) == false)
            {
                return;
            }

            Value = value;

            return;
        }
    }


    internal sealed class SettingValue_Vector3 : SettingValue<Vector3>
    {
        public override SettingValueType SettingValueType { get { return SettingValueType.Vector3; } }

        public override OscMessage Message { get { return new OscMessage(OscAddress, Value.X, Value.Y, Value.Z); } }

        public SettingValue_Vector3(SettingCategrory category, string name, string title, string description, string oscAddress, bool isReadonly, bool isHidden)
            : base(category, name, title, description, oscAddress, isReadonly, isHidden)
        {

        }

        public bool TryGetValue(OscMessage message, out Vector3 value)
        {
            value = default(Vector3);

            if (ValidateMessage(message) == false)
            {
                return false;
            }

            if (message.Count != 3)
            {
                return false;
            }

            if ((message[0] is float) == false)
            {
                return false;
            }

            if ((message[1] is float) == false)
            {
                return false;
            }

            if ((message[2] is float) == false)
            {
                return false;
            }

            value = new Vector3((float)message[0], (float)message[1], (float)message[2]);

            return true;
        }

        public override void OnMessageReceived(OscMessage message)
        {
            Vector3 value;

            if (TryGetValue(message, out value) == false)
            {
                return;
            }

            RemoteValue = value;
            Value = RemoteValue;

            HasRemoteValue = true;

            return;
        }

        public override void OnLocalMessage(OscMessage message)
        {
            Vector3 value;

            if (TryGetValue(message, out value) == false)
            {
                return;
            }

            Value = value;

            return;
        }
    }

    internal sealed class SettingValue_RotationMatrix : SettingValue<RotationMatrix>
    {
        public override SettingValueType SettingValueType { get { return SettingValueType.RotationMatrix; } }

        public override OscMessage Message
        {
            get
            {
                return new OscMessage(OscAddress,
                                        Value.XX, Value.XY, Value.XZ,
                                        Value.YX, Value.YY, Value.YZ,
                                        Value.ZX, Value.ZY, Value.ZZ);
            }
        }

        public SettingValue_RotationMatrix(SettingCategrory category, string name, string title, string description, string oscAddress, bool isReadonly, bool isHidden)
            : base(category, name, title, description, oscAddress, isReadonly, isHidden)
        {

        }

        public bool TryGetValue(OscMessage message, out RotationMatrix value)
        {
            value = default(RotationMatrix);

            if (ValidateMessage(message) == false)
            {
                return false;
            }

            if (message.Count != 9)
            {
                return false;
            }

            float[] values = new float[9];

            for (int i = 0; i < 9; i++)
            {
                if ((message[i] is float) == false)
                {
                    return false;
                }

                values[i] = (float)message[i];
            }

            value = new RotationMatrix(values);

            return true;
        }

        public override void OnMessageReceived(OscMessage message)
        {
            RotationMatrix value;

            if (TryGetValue(message, out value) == false)
            {
                return;
            }

            RemoteValue = value;
            Value = RemoteValue;

            HasRemoteValue = true;

            return;
        }

        public override void OnLocalMessage(OscMessage message)
        {
            RotationMatrix value;

            if (TryGetValue(message, out value) == false)
            {
                return;
            }

            Value = value;

            return;
        }
    }


    internal abstract class SettingValue_Enum<T> : SettingValue<T> where T : struct, IConvertible
    {
        public override OscMessage Message { get { return new OscMessage(OscAddress, Value.ToInt32(CultureInfo.InvariantCulture)); } }

        public SettingValue_Enum(SettingCategrory category, string name, string title, string description, string oscAddress, bool isReadonly, bool isHidden)
            : base(category, name, title, description, oscAddress, isReadonly, isHidden)
        {
            Value = default(T);
            RemoteValue = default(T);
            IsValueUndefined = true;
        }

        public bool TryGetValue(OscMessage message, out T value)
        {
            value = default(T);

            if (ValidateMessage(message) == false)
            {
                return false;
            }

            if ((message[0] is string) == true)
            {
                value = (T)Enum.Parse(typeof(T), (string)message[0], true);
            }
            else if ((message[0] is int) == true)
            {
                value = (T)Enum.ToObject(typeof(T), (int)message[0]);
            }
            else
            {
                return false;
            }

            return true;
        }

        public override void OnMessageReceived(OscMessage message)
        {
            T value;

            if (TryGetValue(message, out value) == false)
            {
                return;
            }

            RemoteValue = value;
            Value = RemoteValue;

            HasRemoteValue = true;

            return;
        }

        public override void OnLocalMessage(OscMessage message)
        {
            T value;

            if (TryGetValue(message, out value) == false)
            {
                return;
            }

            Value = value;

            return;
        }
    }

    public enum WifiMode : int
    {
        [Description("AP")]
        AP = 0,

        [Description("Client")]
        Client = 1,
    }

    internal sealed class SettingValue_WifiMode : SettingValue_Enum<WifiMode>
    {
        public override SettingValueType SettingValueType { get { return SettingValueType.WifiMode; } }

        public SettingValue_WifiMode(SettingCategrory category, string name, string title, string description, string oscAddress, bool isReadonly, bool isHidden)
            : base(category, name, title, description, oscAddress, isReadonly, isHidden)
        { }
    }

    public enum WifiAntenna : int
    {
        [Description("Internal")]
        Internal = 0,

        [Description("External")]
        External = 1,
    }

    internal sealed class SettingValue_WifiAntenna : SettingValue_Enum<WifiAntenna>
    {
        public override SettingValueType SettingValueType { get { return SettingValueType.WifiAntenna; } }

        public SettingValue_WifiAntenna(SettingCategrory category, string name, string title, string description, string oscAddress, bool isReadonly, bool isHidden)
            : base(category, name, title, description, oscAddress, isReadonly, isHidden)
        { }
    }

    public enum WifiRegion : int
    {
        [Description("US")]
        WifiRegionUS = 1,

        [Description("Europe")]
        WifiRegionEurope = 2,

        [Description("Japan")]
        WifiRegionJapan = 3,
    }

    internal sealed class SettingValue_WifiRegion : SettingValue_Enum<WifiRegion>
    {
        public override SettingValueType SettingValueType { get { return SettingValueType.WifiRegion; } }

        public SettingValue_WifiRegion(SettingCategrory category, string name, string title, string description, string oscAddress, bool isReadonly, bool isHidden)
            : base(category, name, title, description, oscAddress, isReadonly, isHidden)
        { }
    }

    public enum WifiBand : int
    {
        [Description("2.4 GHz")]
        Band2GHz = 0,

        [Description("5 GHz")]
        Band5GHz = 1,
    }

    internal sealed class SettingValue_WifiBand : SettingValue_Enum<WifiBand>
    {
        public override SettingValueType SettingValueType { get { return SettingValueType.WifiBand; } }

        public SettingValue_WifiBand(SettingCategrory category, string name, string title, string description, string oscAddress, bool isReadonly, bool isHidden)
            : base(category, name, title, description, oscAddress, isReadonly, isHidden)
        { }
    }

    public enum Wifi2GHzChannel : int
    {
        [Description("1")]
        Channel1 = 1,

        [Description("2")]
        Channel2 = 2,

        [Description("3")]
        Channel3 = 3,

        [Description("4")]
        Channel4 = 4,

        [Description("5")]
        Channel5 = 5,

        [Description("6")]
        Channel6 = 6,

        [Description("7")]
        Channel7 = 7,

        [Description("8")]
        Channel8 = 8,

        [Description("9")]
        Channel9 = 9,

        [Description("10")]
        Channel10 = 10,

        [Description("11")]
        Channel11 = 11,

        [Description("12")]
        Channel12 = 12,

        [Description("13")]
        Channel13 = 13,

        [Description("14")]
        Channel14 = 14,
    }

    internal sealed class SettingValue_Wifi2GHzChannel : SettingValue_Enum<Wifi2GHzChannel>
    {
        public override SettingValueType SettingValueType { get { return SettingValueType.Wifi2GHzChannel; } }

        public SettingValue_Wifi2GHzChannel(SettingCategrory category, string name, string title, string description, string oscAddress, bool isReadonly, bool isHidden)
            : base(category, name, title, description, oscAddress, isReadonly, isHidden)
        {
            Value = Wifi2GHzChannel.Channel1;
            RemoteValue = Wifi2GHzChannel.Channel1;
            IsValueUndefined = true;
        }
    }


    public enum Wifi5GHzChannel : int
    {
        [Description("36")]
        Channel36 = 36,

        [Description("44")]
        Channel44 = 44,

        [Description("48")]
        Channel48 = 48,

        [Description("100")]
        Channel100 = 100,

        [Description("104")]
        Channel104 = 104,

        [Description("108")]
        Channel108 = 108,

        [Description("112")]
        Channel112 = 112,

        [Description("132")]
        Channel132 = 132,

        [Description("136")]
        Channel136 = 136,

        [Description("140")]
        Channel140 = 140,

        [Description("149")]
        Channel149 = 149,

        [Description("153")]
        Channel153 = 153,

        [Description("157")]
        Channel157 = 157,

        [Description("161")]
        Channel161 = 161,

        [Description("165")]
        Channel165 = 165,
    }

    internal sealed class SettingValue_Wifi5GHzChannel : SettingValue_Enum<Wifi5GHzChannel>
    {
        public override SettingValueType SettingValueType { get { return SettingValueType.Wifi5GHzChannel; } }

        public SettingValue_Wifi5GHzChannel(SettingCategrory category, string name, string title, string description, string oscAddress, bool isReadonly, bool isHidden)
            : base(category, name, title, description, oscAddress, isReadonly, isHidden)
        {
            Value = Wifi5GHzChannel.Channel36;
            RemoteValue = Wifi5GHzChannel.Channel36;
            IsValueUndefined = true;
        }
    }

    public enum OscPassthroughMode : int
    {
        [Description("Disabled")]
        Disabled = 0,

        [Description("Enabled")]
        Enabled = 1,

        [Description("Enabled with timestamp")]
        EnabledWithTimestamp = 2,
    }

    internal sealed class SettingValue_OscPassthroughMode : SettingValue_Enum<OscPassthroughMode>
    {
        public override SettingValueType SettingValueType { get { return SettingValueType.OscPassthroughMode; } }

        public SettingValue_OscPassthroughMode(SettingCategrory category, string name, string title, string description, string oscAddress, bool isReadonly, bool isHidden)
            : base(category, name, title, description, oscAddress, isReadonly, isHidden)
        { }
    }

    public enum CpuIdleMode : int
    {
        [Description("Disabled")]
        Disabled = 0,

        [Description("Enabled")]
        Enabled = 1,

        [Description("Enabled for battery only")]
        EnabledForBatteryOnly = 2,
    }

    internal sealed class SettingValue_CpuIdleMode : SettingValue_Enum<CpuIdleMode>
    {
        public override SettingValueType SettingValueType { get { return SettingValueType.CpuIdleMode; } }

        public SettingValue_CpuIdleMode(SettingCategrory category, string name, string title, string description, string oscAddress, bool isReadonly, bool isHidden)
            : base(category, name, title, description, oscAddress, isReadonly, isHidden)
        { }
    }

}
