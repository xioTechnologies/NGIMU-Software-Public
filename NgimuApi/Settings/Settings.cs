/* THIS FILE WAS GENERATED AUTOMATICALLY. DO NOT MODIFY. */ 

using System.Net;
using System.Net.NetworkInformation;
using NgimuApi.Maths;

namespace NgimuApi
{
	public sealed partial class Settings : SettingCategrory
	{
	    private Connection connection;
	
		/// <summary>
		/// Gets the Connection that this setting category is bound to or null if it is unbound.
		/// </summary>
		public override Connection Connection { get { return connection; } }
	
		/// <summary>
		/// Gets the category text including the full path from the root category.
		/// </summary>
		public override string Text { get { return "Settings"; } }
	
		/// <summary>
		/// Gets the category prefix text.
		/// </summary>
		protected override int CategoryPrefix { get { return 0; } }
	
		/// <summary>
		/// Gets the parent category of this category or null if this category is the root category.
		/// </summary>
		public override SettingCategrory Parent { get { return null; } }
	
		/// <summary>
		/// Gets a flag to indicate if this category is hidden.
		/// </summary>
		public override bool IsHidden { get { return false; } }
	
		/// <summary>
		/// Gets Calibration Parameters settings category.
		/// </summary>
		public SettingsCategoryTypes.Calibration Calibration { get; private set; }
		
		/// <summary>
		/// Gets Device Information settings category.
		/// </summary>
		public SettingsCategoryTypes.DeviceInformation DeviceInformation { get; private set; }
		
		/// <summary>
		/// Gets Wi-Fi settings category.
		/// </summary>
		public SettingsCategoryTypes.Wifi Wifi { get; private set; }
		
		/// <summary>
		/// Gets Serial settings category.
		/// </summary>
		public SettingsCategoryTypes.Serial Serial { get; private set; }
		
		/// <summary>
		/// Gets SD Card settings category.
		/// </summary>
		public SettingsCategoryTypes.SDCard SDCard { get; private set; }
		
		/// <summary>
		/// Gets AHRS settings category.
		/// </summary>
		public SettingsCategoryTypes.Ahrs Ahrs { get; private set; }
		
		/// <summary>
		/// Gets Send Rates settings category.
		/// </summary>
		public SettingsCategoryTypes.SendRates SendRates { get; private set; }
		
		/// <summary>
		/// Gets Auxiliary Serial settings category.
		/// </summary>
		public SettingsCategoryTypes.AuxiliarySerial AuxiliarySerial { get; private set; }
		
		/// <summary>
		/// Gets Power Management settings category.
		/// </summary>
		public SettingsCategoryTypes.PowerManagement PowerManagement { get; private set; }
		
		/// <summary>
		/// Gets Crystal Trim setting.
		/// </summary>
		public ISettingValue<float> CrystalTrim { get; private set; }
		
		/// <summary>
		/// Gets Gyroscope Misalignment setting.
		/// </summary>
		public ISettingValue<RotationMatrix> GyroscopeMisalignment { get; private set; }
		
		/// <summary>
		/// Gets Gyroscope Sensitivity setting.
		/// </summary>
		public ISettingValue<Vector3> GyroscopeSensitivity { get; private set; }
		
		/// <summary>
		/// Gets Gyroscope Bias setting.
		/// </summary>
		public ISettingValue<Vector3> GyroscopeBias { get; private set; }
		
		/// <summary>
		/// Gets Accelerometer Misalignment setting.
		/// </summary>
		public ISettingValue<RotationMatrix> AccelerometerMisalignment { get; private set; }
		
		/// <summary>
		/// Gets Accelerometer Sensitivity setting.
		/// </summary>
		public ISettingValue<Vector3> AccelerometerSensitivity { get; private set; }
		
		/// <summary>
		/// Gets Accelerometer Bias setting.
		/// </summary>
		public ISettingValue<Vector3> AccelerometerBias { get; private set; }
		
		/// <summary>
		/// Gets Soft Iron Matrix setting.
		/// </summary>
		public ISettingValue<RotationMatrix> SoftIronMatrix { get; private set; }
		
		/// <summary>
		/// Gets Hard Iron Bias setting.
		/// </summary>
		public ISettingValue<Vector3> HardIronBias { get; private set; }
		
		/// <summary>
		/// Gets Calibration Date setting.
		/// </summary>
		public ISettingValue<string> CalibrationDate { get; private set; }
		
		/// <summary>
		/// Gets Calibration Temperature setting.
		/// </summary>
		public ISettingValue<float> CalibrationTemperature { get; private set; }
		
		/// <summary>
		/// Gets Device Name setting.
		/// </summary>
		public ISettingValue<string> DeviceName { get; private set; }
		
		/// <summary>
		/// Gets Serial Number setting.
		/// </summary>
		public ISettingValue<string> SerialNumber { get; private set; }
		
		/// <summary>
		/// Gets Firmware Version setting.
		/// </summary>
		public ISettingValue<string> FirmwareVersion { get; private set; }
		
		/// <summary>
		/// Gets Bootloader Version setting.
		/// </summary>
		public ISettingValue<string> BootloaderVersion { get; private set; }
		
		/// <summary>
		/// Gets Hardware Version setting.
		/// </summary>
		public ISettingValue<string> HardwareVersion { get; private set; }
		
		/// <summary>
		/// Gets Wi-Fi Enabled setting.
		/// </summary>
		public ISettingValue<bool> WifiEnabled { get; private set; }
		
		/// <summary>
		/// Gets Wi-Fi Firmware Version setting.
		/// </summary>
		public ISettingValue<string> WifiFirmwareVersion { get; private set; }
		
		/// <summary>
		/// Gets Wi-Fi MAC Address setting.
		/// </summary>
		public ISettingValue<PhysicalAddress> WifiMacAddress { get; private set; }
		
		/// <summary>
		/// Gets Wi-Fi IP Address setting.
		/// </summary>
		public ISettingValue<IPAddress> WifiIPAddress { get; private set; }
		
		/// <summary>
		/// Gets Wi-Fi Mode setting.
		/// </summary>
		public ISettingValue<WifiMode> WifiMode { get; private set; }
		
		/// <summary>
		/// Gets Wi-Fi Antenna setting.
		/// </summary>
		public ISettingValue<WifiAntenna> WifiAntenna { get; private set; }
		
		/// <summary>
		/// Gets Wi-Fi Region setting.
		/// </summary>
		public ISettingValue<WifiRegion> WifiRegion { get; private set; }
		
		/// <summary>
		/// Gets Wi-Fi AP Band setting.
		/// </summary>
		public ISettingValue<WifiBand> WifiAPBand { get; private set; }
		
		/// <summary>
		/// Gets Wi-Fi AP 2.4 GHz Channel setting.
		/// </summary>
		public ISettingValue<Wifi2GHzChannel> WifiAP2GHzChannel { get; private set; }
		
		/// <summary>
		/// Gets Wi-Fi AP 5 GHz Channel setting.
		/// </summary>
		public ISettingValue<Wifi5GHzChannel> WifiAP5GHzChannel { get; private set; }
		
		/// <summary>
		/// Gets Wi-Fi Client SSID setting.
		/// </summary>
		public ISettingValue<string> WifiClientSSID { get; private set; }
		
		/// <summary>
		/// Gets Wi-Fi Client Key setting.
		/// </summary>
		public ISettingValue<string> WifiClientKey { get; private set; }
		
		/// <summary>
		/// Gets Wi-Fi Client DHCP Enabled setting.
		/// </summary>
		public ISettingValue<bool> WifiClientDhcpEnabled { get; private set; }
		
		/// <summary>
		/// Gets Wi-Fi Client IP Address setting.
		/// </summary>
		public ISettingValue<IPAddress> WifiClientIPAddress { get; private set; }
		
		/// <summary>
		/// Gets Wi-Fi Client Subnet setting.
		/// </summary>
		public ISettingValue<IPAddress> WifiClientSubnet { get; private set; }
		
		/// <summary>
		/// Gets Wi-Fi Client Gateway setting.
		/// </summary>
		public ISettingValue<IPAddress> WifiClientGateway { get; private set; }
		
		/// <summary>
		/// Gets Wi-Fi Client Low Power Mode setting.
		/// </summary>
		public ISettingValue<bool> WifiClientLowPower { get; private set; }
		
		/// <summary>
		/// Gets UDP Send IP Address setting.
		/// </summary>
		public ISettingValue<IPAddress> WifiSendIPAddress { get; private set; }
		
		/// <summary>
		/// Gets UDP Send Port setting.
		/// </summary>
		public ISettingValue<ushort> WifiSendPort { get; private set; }
		
		/// <summary>
		/// Gets UDP Receive Port setting.
		/// </summary>
		public ISettingValue<ushort> WifiReceivePort { get; private set; }
		
		/// <summary>
		/// Gets UDP Maximise Throughput setting.
		/// </summary>
		public ISettingValue<bool> WifiMaximiseThroughput { get; private set; }
		
		/// <summary>
		/// Gets Synchronisation Master Enabled setting.
		/// </summary>
		public ISettingValue<bool> SynchronisationMasterEnabled { get; private set; }
		
		/// <summary>
		/// Gets Synchronisation Master Send Port setting.
		/// </summary>
		public ISettingValue<ushort> SynchronisationMasterPort { get; private set; }
		
		/// <summary>
		/// Gets Synchronisation Network Latency setting.
		/// </summary>
		public ISettingValue<uint> SynchronisationMasterLatency { get; private set; }
		
		/// <summary>
		/// Gets Serial Enabled setting.
		/// </summary>
		public ISettingValue<bool> SerialEnabled { get; private set; }
		
		/// <summary>
		/// Gets Serial Baud Rate setting.
		/// </summary>
		public ISettingValue<uint> SerialBaudRate { get; private set; }
		
		/// <summary>
		/// Gets Serial Baud Rate Error setting.
		/// </summary>
		public ISettingValue<float> SerialBaudRateError { get; private set; }
		
		/// <summary>
		/// Gets Serial RTS/CTS Enabled setting.
		/// </summary>
		public ISettingValue<bool> SerialRtsCtsEnabled { get; private set; }
		
		/// <summary>
		/// Gets Serial Invert Data Lines setting.
		/// </summary>
		public ISettingValue<bool> SerialInvertDataLines { get; private set; }
		
		/// <summary>
		/// Gets SD Card File Name Prefix setting.
		/// </summary>
		public ISettingValue<string> sdCardFileNamePrefix { get; private set; }
		
		/// <summary>
		/// Gets SD Card File Number setting.
		/// </summary>
		public ISettingValue<uint> sdCardFileNumber { get; private set; }
		
		/// <summary>
		/// Gets SD Card Maximum File Size setting.
		/// </summary>
		public ISettingValue<uint> sdCardMaxFileSize { get; private set; }
		
		/// <summary>
		/// Gets SD Card Maximum File Period setting.
		/// </summary>
		public ISettingValue<uint> sdCardMaxFilePeriod { get; private set; }
		
		/// <summary>
		/// Gets AHRS Gain setting.
		/// </summary>
		public ISettingValue<float> AhrsGain { get; private set; }
		
		/// <summary>
		/// Gets AHRS Gyroscope Bias Correction setting.
		/// </summary>
		public ISettingValue<bool> AhrsGyroscopeBiasCorrection { get; private set; }
		
		/// <summary>
		/// Gets AHRS Ignore Magnetometer setting.
		/// </summary>
		public ISettingValue<bool> AhrsIgnoreMagnetometer { get; private set; }
		
		/// <summary>
		/// Gets AHRS Minimum Magnetic Field setting.
		/// </summary>
		public ISettingValue<float> AhrsMinimumMagneticField { get; private set; }
		
		/// <summary>
		/// Gets AHRS Maximum Magnetic Field setting.
		/// </summary>
		public ISettingValue<float> AhrsMaximumMagneticField { get; private set; }
		
		/// <summary>
		/// Gets Sensors Send Rate setting.
		/// </summary>
		public ISettingValue<float> SendRateSensors { get; private set; }
		
		/// <summary>
		/// Gets Magnitudes Send Rate setting.
		/// </summary>
		public ISettingValue<float> SendRateMagnitudes { get; private set; }
		
		/// <summary>
		/// Gets Quaternion Send Rate setting.
		/// </summary>
		public ISettingValue<float> SendRateQuaternion { get; private set; }
		
		/// <summary>
		/// Gets Rotation Matrix Send Rate setting.
		/// </summary>
		public ISettingValue<float> SendRateRotationMatrix { get; private set; }
		
		/// <summary>
		/// Gets Euler Angles Send Rate setting.
		/// </summary>
		public ISettingValue<float> SendRateEulerAngles { get; private set; }
		
		/// <summary>
		/// Gets Linear Acceleration Send Rate setting.
		/// </summary>
		public ISettingValue<float> SendRateLinearAcceleration { get; private set; }
		
		/// <summary>
		/// Gets Earth Acceleration Send Rate setting.
		/// </summary>
		public ISettingValue<float> SendRateEarthAcceleration { get; private set; }
		
		/// <summary>
		/// Gets Altitude Send Rate setting.
		/// </summary>
		public ISettingValue<float> SendRateAltitude { get; private set; }
		
		/// <summary>
		/// Gets Temperature Send Rate setting.
		/// </summary>
		public ISettingValue<float> SendRateTemperature { get; private set; }
		
		/// <summary>
		/// Gets Humidity Send Rate setting.
		/// </summary>
		public ISettingValue<float> SendRateHumidity { get; private set; }
		
		/// <summary>
		/// Gets Battery Send Rate setting.
		/// </summary>
		public ISettingValue<float> SendRateBattery { get; private set; }
		
		/// <summary>
		/// Gets Analogue Inputs Send Rate setting.
		/// </summary>
		public ISettingValue<float> SendRateAnalogueInputs { get; private set; }
		
		/// <summary>
		/// Gets RSSI Send Rate setting.
		/// </summary>
		public ISettingValue<float> SendRateRssi { get; private set; }
		
		/// <summary>
		/// Gets Auxiliary Serial Enabled setting.
		/// </summary>
		public ISettingValue<bool> AuxSerialEnabled { get; private set; }
		
		/// <summary>
		/// Gets Auxiliary Serial Baud Rate setting.
		/// </summary>
		public ISettingValue<uint> AuxSerialBaudRate { get; private set; }
		
		/// <summary>
		/// Gets Auxiliary Serial Baud Rate Error setting.
		/// </summary>
		public ISettingValue<float> AuxSerialBaudRateError { get; private set; }
		
		/// <summary>
		/// Gets Auxiliary Serial RTS/CTS Enabled setting.
		/// </summary>
		public ISettingValue<bool> AuxSerialRtsCtsEnabled { get; private set; }
		
		/// <summary>
		/// Gets Auxiliary Serial Invert Data Lines setting.
		/// </summary>
		public ISettingValue<bool> AuxSerialInvertDataLines { get; private set; }
		
		/// <summary>
		/// Gets Auxiliary Serial OSC Passthrough setting.
		/// </summary>
		public ISettingValue<bool> AuxSerialOscPassthrough { get; private set; }
		
		/// <summary>
		/// Gets Auxiliary Serial Send As String setting.
		/// </summary>
		public ISettingValue<bool> AuxSerialSendAsString { get; private set; }
		
		/// <summary>
		/// Gets Auxiliary Serial Send Buffer Size setting.
		/// </summary>
		public ISettingValue<uint> AuxSerialBufferSize { get; private set; }
		
		/// <summary>
		/// Gets Auxiliary Serial Send Timeout setting.
		/// </summary>
		public ISettingValue<uint> AuxSerialTimeout { get; private set; }
		
		/// <summary>
		/// Gets Auxiliary Serial Send Framing Character setting.
		/// </summary>
		public ISettingValue<int> AuxSerialFramingCharacter { get; private set; }
		
		/// <summary>
		/// Gets CPU Idle Mode setting.
		/// </summary>
		public ISettingValue<CpuIdleMode> CpuIdleMode { get; private set; }
		
		/// <summary>
		/// Gets LEDs Enabled setting.
		/// </summary>
		public ISettingValue<bool> LedsEnabled { get; private set; }
		
		/// <summary>
		/// Gets External Current Limit setting.
		/// </summary>
		public ISettingValue<bool> ExternalCurrentLimit { get; private set; }
		
		/// <summary>
		/// Gets Sleep Timer setting.
		/// </summary>
		public ISettingValue<uint> SleepTimer { get; private set; }
		
		/// <summary>
		/// Gets Wakeup Timer setting.
		/// </summary>
		public ISettingValue<uint> WakeupTimer { get; private set; }
		
		/// <summary>
		/// Gets Motion Trigger Wakeup setting.
		/// </summary>
		public ISettingValue<bool> MotionTriggerWakeup { get; private set; }
		
		/// <summary>
		/// Gets External Power Wakeup setting.
		/// </summary>
		public ISettingValue<bool> ExternalPowerWakeup { get; private set; }
		
		/// <summary>
		/// Gets Muted On Startup setting.
		/// </summary>
		public ISettingValue<bool> MutedOnStartup { get; private set; }
		
		/// <summary>
		/// Gets Battery Minimum Voltage setting.
		/// </summary>
		public ISettingValue<float> BatteryMinimumVoltage { get; private set; }
		
		/// <summary>
		/// Gets Battery Capacity setting.
		/// </summary>
		public ISettingValue<float> BatteryCapacity { get; private set; }
		
		/// <summary>
		/// Gets Battery Health setting.
		/// </summary>
		public ISettingValue<float> BatteryHealth { get; private set; }
		
		/// <summary>
		/// Gets Battery Charge/Discharge Cycles setting.
		/// </summary>
		public ISettingValue<float> BatteryChargeDischargeCycles { get; private set; }
	
	    public Settings()
	    {
			Calibration = new SettingsCategoryTypes.Calibration(this);
			DeviceInformation = new SettingsCategoryTypes.DeviceInformation(this);
			Wifi = new SettingsCategoryTypes.Wifi(this);
			Serial = new SettingsCategoryTypes.Serial(this);
			SDCard = new SettingsCategoryTypes.SDCard(this);
			Ahrs = new SettingsCategoryTypes.Ahrs(this);
			SendRates = new SettingsCategoryTypes.SendRates(this);
			AuxiliarySerial = new SettingsCategoryTypes.AuxiliarySerial(this);
			PowerManagement = new SettingsCategoryTypes.PowerManagement(this);
			
			CrystalTrim = new SettingValue_Float(Calibration, "Crystal Trim", "Crystal Trim", LookupDocumentation("/crystal"), "/crystal", false, false);
			GyroscopeMisalignment = new SettingValue_RotationMatrix(Calibration, "Gyroscope Misalignment", "Gyroscope Misalignment", LookupDocumentation("/gyroscope/misalignment"), "/gyroscope/misalignment", false, false);
			GyroscopeSensitivity = new SettingValue_Vector3(Calibration, "Gyroscope Sensitivity", "Gyroscope Sensitivity", LookupDocumentation("/gyroscope/sensitivity"), "/gyroscope/sensitivity", false, false);
			GyroscopeBias = new SettingValue_Vector3(Calibration, "Gyroscope Bias", "Gyroscope Bias", LookupDocumentation("/gyroscope/bias"), "/gyroscope/bias", false, false);
			AccelerometerMisalignment = new SettingValue_RotationMatrix(Calibration, "Accelerometer Misalignment", "Accelerometer Misalignment", LookupDocumentation("/accelerometer/misalignment"), "/accelerometer/misalignment", false, false);
			AccelerometerSensitivity = new SettingValue_Vector3(Calibration, "Accelerometer Sensitivity", "Accelerometer Sensitivity", LookupDocumentation("/accelerometer/sensitivity"), "/accelerometer/sensitivity", false, false);
			AccelerometerBias = new SettingValue_Vector3(Calibration, "Accelerometer Bias", "Accelerometer Bias", LookupDocumentation("/accelerometer/bias"), "/accelerometer/bias", false, false);
			SoftIronMatrix = new SettingValue_RotationMatrix(Calibration, "Soft Iron Matrix", "Soft Iron Matrix", LookupDocumentation("/softiron"), "/softiron", false, false);
			HardIronBias = new SettingValue_Vector3(Calibration, "Hard Iron Bias", "Hard Iron Bias", LookupDocumentation("/hardiron"), "/hardiron", false, false);
			CalibrationDate = new SettingValue_String(Calibration, "Calibration Date", "Calibration Date", LookupDocumentation("/calibration/date"), "/calibration/date", false, false);
			CalibrationTemperature = new SettingValue_Float(Calibration, "Calibration Temperature", "Calibration Temperature", LookupDocumentation("/calibration/temperature"), "/calibration/temperature", false, false);
			DeviceName = new SettingValue_String(DeviceInformation, "Device Name", "Device Name", LookupDocumentation("/name"), "/name", false, false);
			SerialNumber = new SettingValue_String(DeviceInformation, "Serial Number", "Serial Number", LookupDocumentation("/serialnumber"), "/serialnumber", true, false);
			FirmwareVersion = new SettingValue_String(DeviceInformation, "Firmware Version", "Firmware Version", LookupDocumentation("/firmware"), "/firmware", true, false);
			BootloaderVersion = new SettingValue_String(DeviceInformation, "Bootloader Version", "Bootloader Version", LookupDocumentation("/bootloader"), "/bootloader", true, false);
			HardwareVersion = new SettingValue_String(DeviceInformation, "Hardware Version", "Hardware Version", LookupDocumentation("/hardware"), "/hardware", true, false);
			WifiEnabled = new SettingValue_Bool(Wifi, "Enabled", "Wi-Fi Enabled", LookupDocumentation("/wifi/enabled"), "/wifi/enabled", false, false);
			WifiFirmwareVersion = new SettingValue_String(Wifi, "Firmware Version", "Wi-Fi Firmware Version", LookupDocumentation("/wifi/firmware"), "/wifi/firmware", true, false);
			WifiMacAddress = new SettingValue_MacAddress(Wifi, "MAC Address", "Wi-Fi MAC Address", LookupDocumentation("/wifi/mac"), "/wifi/mac", true, false);
			WifiIPAddress = new SettingValue_IPAddress(Wifi, "IP Address", "Wi-Fi IP Address", LookupDocumentation("/wifi/ip"), "/wifi/ip", true, false);
			WifiMode = new SettingValue_WifiMode(Wifi, "Mode", "Wi-Fi Mode", LookupDocumentation("/wifi/mode"), "/wifi/mode", false, false);
			WifiAntenna = new SettingValue_WifiAntenna(Wifi, "Antenna", "Wi-Fi Antenna", LookupDocumentation("/wifi/antenna"), "/wifi/antenna", false, false);
			WifiRegion = new SettingValue_WifiRegion(Wifi, "Region", "Wi-Fi Region", LookupDocumentation("/wifi/region"), "/wifi/region", false, false);
			WifiAPBand = new SettingValue_WifiBand(Wifi.AP, "Band", "Wi-Fi AP Band", LookupDocumentation("/wifi/ap/band"), "/wifi/ap/band", false, false);
			WifiAP2GHzChannel = new SettingValue_Wifi2GHzChannel(Wifi.AP, "2.4 GHz Channel", "Wi-Fi AP 2.4 GHz Channel", LookupDocumentation("/wifi/ap/2ghzchannel"), "/wifi/ap/2ghzchannel", false, false);
			WifiAP5GHzChannel = new SettingValue_Wifi5GHzChannel(Wifi.AP, "5 GHz Channel", "Wi-Fi AP 5 GHz Channel", LookupDocumentation("/wifi/ap/5ghzchannel"), "/wifi/ap/5ghzchannel", false, false);
			WifiClientSSID = new SettingValue_String(Wifi.Client, "SSID", "Wi-Fi Client SSID", LookupDocumentation("/wifi/client/ssid"), "/wifi/client/ssid", false, false);
			WifiClientKey = new SettingValue_String(Wifi.Client, "Key", "Wi-Fi Client Key", LookupDocumentation("/wifi/client/key"), "/wifi/client/key", false, false);
			WifiClientDhcpEnabled = new SettingValue_Bool(Wifi.Client, "DHCP Enabled", "Wi-Fi Client DHCP Enabled", LookupDocumentation("/wifi/client/dhcp"), "/wifi/client/dhcp", false, false);
			WifiClientIPAddress = new SettingValue_IPAddress(Wifi.Client, "IP Address", "Wi-Fi Client IP Address", LookupDocumentation("/wifi/client/ip"), "/wifi/client/ip", false, false);
			WifiClientSubnet = new SettingValue_IPAddress(Wifi.Client, "Subnet", "Wi-Fi Client Subnet", LookupDocumentation("/wifi/client/subnet"), "/wifi/client/subnet", false, false);
			WifiClientGateway = new SettingValue_IPAddress(Wifi.Client, "Gateway", "Wi-Fi Client Gateway", LookupDocumentation("/wifi/client/gateway"), "/wifi/client/gateway", false, false);
			WifiClientLowPower = new SettingValue_Bool(Wifi.Client, "Low Power Mode", "Wi-Fi Client Low Power Mode", LookupDocumentation("/wifi/client/lowpower"), "/wifi/client/lowpower", false, false);
			WifiSendIPAddress = new SettingValue_IPAddress(Wifi.Udp, "Send IP Address", "UDP Send IP Address", LookupDocumentation("/wifi/send/ip"), "/wifi/send/ip", false, false);
			WifiSendPort = new SettingValue_UdpPort(Wifi.Udp, "Send Port", "UDP Send Port", LookupDocumentation("/wifi/send/port"), "/wifi/send/port", false, false);
			WifiReceivePort = new SettingValue_UdpPort(Wifi.Udp, "Receive Port", "UDP Receive Port", LookupDocumentation("/wifi/receive/port"), "/wifi/receive/port", false, false);
			WifiMaximiseThroughput = new SettingValue_Bool(Wifi.Udp, "Maximise Throughput", "UDP Maximise Throughput", LookupDocumentation("/wifi/throughput"), "/wifi/throughput", false, false);
			SynchronisationMasterEnabled = new SettingValue_Bool(Wifi.SynchronisationMaster, "Enabled", "Synchronisation Master Enabled", LookupDocumentation("/wifi/synchronisation/enabled"), "/wifi/synchronisation/enabled", false, false);
			SynchronisationMasterPort = new SettingValue_UdpPort(Wifi.SynchronisationMaster, "Send Port", "Synchronisation Master Send Port", LookupDocumentation("/wifi/synchronisation/port"), "/wifi/synchronisation/port", false, false);
			SynchronisationMasterLatency = new SettingValue_UInt32(Wifi.SynchronisationMaster, "Network Latency (ms)", "Synchronisation Network Latency", LookupDocumentation("/wifi/synchronisation/latency"), "/wifi/synchronisation/latency", false, false);
			SerialEnabled = new SettingValue_Bool(Serial, "Enabled", "Serial Enabled", LookupDocumentation("/serial/enabled"), "/serial/enabled", false, false);
			SerialBaudRate = new SettingValue_UInt32(Serial, "Baud Rate", "Serial Baud Rate", LookupDocumentation("/serial/baud"), "/serial/baud", false, false);
			SerialBaudRateError = new SettingValue_Float(Serial, "Baud Rate Error (%)", "Serial Baud Rate Error", LookupDocumentation("/serial/error"), "/serial/error", true, false);
			SerialRtsCtsEnabled = new SettingValue_Bool(Serial, "RTS/CTS Enabled", "Serial RTS/CTS Enabled", LookupDocumentation("/serial/rtscts"), "/serial/rtscts", false, false);
			SerialInvertDataLines = new SettingValue_Bool(Serial, "Invert Data Lines", "Serial Invert Data Lines", LookupDocumentation("/serial/invert"), "/serial/invert", false, false);
			sdCardFileNamePrefix = new SettingValue_String(SDCard, "File Name Prefix", "SD Card File Name Prefix", LookupDocumentation("/sd/prefix"), "/sd/prefix", false, false);
			sdCardFileNumber = new SettingValue_UInt32(SDCard, "File Number", "SD Card File Number", LookupDocumentation("/sd/number"), "/sd/number", false, false);
			sdCardMaxFileSize = new SettingValue_UInt32(SDCard, "Maximum File Size (MB)", "SD Card Maximum File Size", LookupDocumentation("/sd/size"), "/sd/size", false, false);
			sdCardMaxFilePeriod = new SettingValue_UInt32(SDCard, "Maximum File Period (s)", "SD Card Maximum File Period", LookupDocumentation("/sd/period"), "/sd/period", false, false);
			AhrsGain = new SettingValue_Float(Ahrs, "Gain", "AHRS Gain", LookupDocumentation("/ahrs/gain"), "/ahrs/gain", false, false);
			AhrsGyroscopeBiasCorrection = new SettingValue_Bool(Ahrs, "Gyroscope Bias Correction", "AHRS Gyroscope Bias Correction", LookupDocumentation("/ahrs/gyroscope"), "/ahrs/gyroscope", false, false);
			AhrsIgnoreMagnetometer = new SettingValue_Bool(Ahrs, "Ignore Magnetometer", "AHRS Ignore Magnetometer", LookupDocumentation("/ahrs/magnetometer"), "/ahrs/magnetometer", false, false);
			AhrsMinimumMagneticField = new SettingValue_Float(Ahrs.MagneticFieldRejection, "Minimum Magnetic Field (uT)", "AHRS Minimum Magnetic Field", LookupDocumentation("/ahrs/magnetic/min"), "/ahrs/magnetic/min", false, false);
			AhrsMaximumMagneticField = new SettingValue_Float(Ahrs.MagneticFieldRejection, "Maximum Magnetic Field (uT)", "AHRS Maximum Magnetic Field", LookupDocumentation("/ahrs/magnetic/max"), "/ahrs/magnetic/max", false, false);
			SendRateSensors = new SettingValue_Float(SendRates, "Sensors (Hz)", "Sensors Send Rate", LookupDocumentation("/rate/sensors"), "/rate/sensors", false, false);
			SendRateMagnitudes = new SettingValue_Float(SendRates, "Magnitudes (Hz)", "Magnitudes Send Rate", LookupDocumentation("/rate/magnitudes"), "/rate/magnitudes", false, false);
			SendRateQuaternion = new SettingValue_Float(SendRates, "Quaternion (Hz)", "Quaternion Send Rate", LookupDocumentation("/rate/quaternion"), "/rate/quaternion", false, false);
			SendRateRotationMatrix = new SettingValue_Float(SendRates, "Rotation Matrix (Hz)", "Rotation Matrix Send Rate", LookupDocumentation("/rate/matrix"), "/rate/matrix", false, false);
			SendRateEulerAngles = new SettingValue_Float(SendRates, "Euler Angles (Hz)", "Euler Angles Send Rate", LookupDocumentation("/rate/euler"), "/rate/euler", false, false);
			SendRateLinearAcceleration = new SettingValue_Float(SendRates, "Linear Acceleration (Hz)", "Linear Acceleration Send Rate", LookupDocumentation("/rate/linear"), "/rate/linear", false, false);
			SendRateEarthAcceleration = new SettingValue_Float(SendRates, "Earth Acceleration (Hz)", "Earth Acceleration Send Rate", LookupDocumentation("/rate/earth"), "/rate/earth", false, false);
			SendRateAltitude = new SettingValue_Float(SendRates, "Altitude (Hz)", "Altitude Send Rate", LookupDocumentation("/rate/altitude"), "/rate/altitude", false, false);
			SendRateTemperature = new SettingValue_Float(SendRates, "Temperature (Hz)", "Temperature Send Rate", LookupDocumentation("/rate/temperature"), "/rate/temperature", false, false);
			SendRateHumidity = new SettingValue_Float(SendRates, "Humidity (Hz)", "Humidity Send Rate", LookupDocumentation("/rate/humidity"), "/rate/humidity", false, false);
			SendRateBattery = new SettingValue_Float(SendRates, "Battery (Hz)", "Battery Send Rate", LookupDocumentation("/rate/battery"), "/rate/battery", false, false);
			SendRateAnalogueInputs = new SettingValue_Float(SendRates, "Analogue Inputs (Hz)", "Analogue Inputs Send Rate", LookupDocumentation("/rate/analogue"), "/rate/analogue", false, false);
			SendRateRssi = new SettingValue_Float(SendRates, "RSSI (Hz)", "RSSI Send Rate", LookupDocumentation("/rate/rssi"), "/rate/rssi", false, false);
			AuxSerialEnabled = new SettingValue_Bool(AuxiliarySerial, "Enabled", "Auxiliary Serial Enabled", LookupDocumentation("/auxserial/enabled"), "/auxserial/enabled", false, false);
			AuxSerialBaudRate = new SettingValue_UInt32(AuxiliarySerial, "Baud Rate", "Auxiliary Serial Baud Rate", LookupDocumentation("/auxserial/baud"), "/auxserial/baud", false, false);
			AuxSerialBaudRateError = new SettingValue_Float(AuxiliarySerial, "Baud Rate Error (%)", "Auxiliary Serial Baud Rate Error", LookupDocumentation("/auxserial/error"), "/auxserial/error", true, false);
			AuxSerialRtsCtsEnabled = new SettingValue_Bool(AuxiliarySerial, "RTS/CTS Enabled", "Auxiliary Serial RTS/CTS Enabled", LookupDocumentation("/auxserial/rtscts"), "/auxserial/rtscts", false, false);
			AuxSerialInvertDataLines = new SettingValue_Bool(AuxiliarySerial, "Invert Data Lines", "Auxiliary Serial Invert Data Lines", LookupDocumentation("/auxserial/invert"), "/auxserial/invert", false, false);
			AuxSerialOscPassthrough = new SettingValue_Bool(AuxiliarySerial, "OSC Passthrough", "Auxiliary Serial OSC Passthrough", LookupDocumentation("/auxserial/passthrough"), "/auxserial/passthrough", false, false);
			AuxSerialSendAsString = new SettingValue_Bool(AuxiliarySerial, "Send As String", "Auxiliary Serial Send As String", LookupDocumentation("/auxserial/string"), "/auxserial/string", false, false);
			AuxSerialBufferSize = new SettingValue_UInt32(AuxiliarySerial.SendCondition, "Buffer Size (Bytes)", "Auxiliary Serial Send Buffer Size", LookupDocumentation("/auxserial/buffer"), "/auxserial/buffer", false, false);
			AuxSerialTimeout = new SettingValue_UInt32(AuxiliarySerial.SendCondition, "Timeout (ms)", "Auxiliary Serial Send Timeout", LookupDocumentation("/auxserial/timeout"), "/auxserial/timeout", false, false);
			AuxSerialFramingCharacter = new SettingValue_Int32(AuxiliarySerial.SendCondition, "Framing Character", "Auxiliary Serial Send Framing Character", LookupDocumentation("/auxserial/framing"), "/auxserial/framing", false, false);
			CpuIdleMode = new SettingValue_CpuIdleMode(PowerManagement, "CPU Idle Mode", "CPU Idle Mode", LookupDocumentation("/idle"), "/idle", false, false);
			LedsEnabled = new SettingValue_Bool(PowerManagement, "LEDs Enabled", "LEDs Enabled", LookupDocumentation("/leds"), "/leds", false, false);
			ExternalCurrentLimit = new SettingValue_Bool(PowerManagement, "External Current Limit", "External Current Limit", LookupDocumentation("/currentlimit"), "/currentlimit", false, false);
			SleepTimer = new SettingValue_UInt32(PowerManagement, "Sleep Timer (s)", "Sleep Timer", LookupDocumentation("/sleeptimer"), "/sleeptimer", false, false);
			WakeupTimer = new SettingValue_UInt32(PowerManagement, "Wakeup Timer (s)", "Wakeup Timer", LookupDocumentation("/wakeuptimer"), "/wakeuptimer", false, false);
			MotionTriggerWakeup = new SettingValue_Bool(PowerManagement, "Motion Trigger Wakeup", "Motion Trigger Wakeup", LookupDocumentation("/motiontrigger"), "/motiontrigger", false, false);
			ExternalPowerWakeup = new SettingValue_Bool(PowerManagement, "External Power Wakeup", "External Power Wakeup", LookupDocumentation("/powerwakeup"), "/powerwakeup", false, false);
			MutedOnStartup = new SettingValue_Bool(PowerManagement, "Muted On Startup", "Muted On Startup", LookupDocumentation("/muted"), "/muted", false, false);
			BatteryMinimumVoltage = new SettingValue_Float(PowerManagement.Battery, "Minimum Voltage (V)", "Battery Minimum Voltage", LookupDocumentation("/battery/voltage"), "/battery/voltage", false, false);
			BatteryCapacity = new SettingValue_Float(PowerManagement.Battery, "Capacity (mAh)", "Battery Capacity", LookupDocumentation("/battery/capacity"), "/battery/capacity", false, false);
			BatteryHealth = new SettingValue_Float(PowerManagement.Battery, "Health (%)", "Battery Health", LookupDocumentation("/battery/health"), "/battery/health", true, false);
			BatteryChargeDischargeCycles = new SettingValue_Float(PowerManagement.Battery, "Charge/Discharge Cycles", "Battery Charge/Discharge Cycles", LookupDocumentation("/battery/cycles"), "/battery/cycles", true, false);
			
			AttachSettings(this);
	
			Finalise();
	    }
	
		internal override void AttachSettings(Settings settings)
		{
			Calibration.AttachSettings(settings);
			DeviceInformation.AttachSettings(settings);
			Wifi.AttachSettings(settings);
			Serial.AttachSettings(settings);
			SDCard.AttachSettings(settings);
			Ahrs.AttachSettings(settings);
			SendRates.AttachSettings(settings);
			AuxiliarySerial.AttachSettings(settings);
			PowerManagement.AttachSettings(settings);
			
			Add(Calibration);
			Add(DeviceInformation);
			Add(Wifi);
			Add(Serial);
			Add(SDCard);
			Add(Ahrs);
			Add(SendRates);
			Add(AuxiliarySerial);
			Add(PowerManagement);
		}
	
	    internal Settings(Connection connection) : this() 
	    {
	        this.connection = connection;
	
			foreach (ISettingValue value in AllValues) 
			{
				Connection.Attach(value.OscAddress, value.OnMessageReceived);
			}
	    }
	
		/// <summary>
		/// Copy the values of all the settings in this category to another
		/// </summary>
		/// <param name="other">Another category</param>
	    public void CopyTo(Settings other)
	    {
			Calibration.CopyTo(other.Calibration);
			DeviceInformation.CopyTo(other.DeviceInformation);
			Wifi.CopyTo(other.Wifi);
			Serial.CopyTo(other.Serial);
			SDCard.CopyTo(other.SDCard);
			Ahrs.CopyTo(other.Ahrs);
			SendRates.CopyTo(other.SendRates);
			AuxiliarySerial.CopyTo(other.AuxiliarySerial);
			PowerManagement.CopyTo(other.PowerManagement);
	    }
	} 

	/// <summary>
	/// Contains all concrete category types.
	/// </summary>
    public sealed class SettingsCategoryTypes
    {
		public sealed class Calibration : SettingCategrory
		{
		    private Settings parent; 
		
			/// <summary>
			/// Gets the Connection that this setting category is bound to or null if it is unbound.
			/// </summary>
			public override Connection Connection { get { return parent.Connection; } }
		
			/// <summary>
			/// Gets the category text including the full path from the root category.
			/// </summary>
			public override string Text { get { return "Calibration Parameters"; } }
		
			/// <summary>
			/// Gets the category prefix text.
			/// </summary>
			protected override int CategoryPrefix { get { return 10; } }
		
			/// <summary>
			/// Gets the parent category of this category or null if this category is the root category.
			/// </summary>
			public override SettingCategrory Parent { get { return parent; } }
		
			/// <summary>
			/// Gets a flag to indicate if this category is hidden.
			/// </summary>
			public override bool IsHidden { get { return true; } }
		
			/// <summary>
			/// Gets Crystal Trim setting.
			/// </summary>
			public ISettingValue<float> CrystalTrim { get; private set; }
			
			/// <summary>
			/// Gets Gyroscope Misalignment setting.
			/// </summary>
			public ISettingValue<RotationMatrix> GyroscopeMisalignment { get; private set; }
			
			/// <summary>
			/// Gets Gyroscope Sensitivity setting.
			/// </summary>
			public ISettingValue<Vector3> GyroscopeSensitivity { get; private set; }
			
			/// <summary>
			/// Gets Gyroscope Bias setting.
			/// </summary>
			public ISettingValue<Vector3> GyroscopeBias { get; private set; }
			
			/// <summary>
			/// Gets Accelerometer Misalignment setting.
			/// </summary>
			public ISettingValue<RotationMatrix> AccelerometerMisalignment { get; private set; }
			
			/// <summary>
			/// Gets Accelerometer Sensitivity setting.
			/// </summary>
			public ISettingValue<Vector3> AccelerometerSensitivity { get; private set; }
			
			/// <summary>
			/// Gets Accelerometer Bias setting.
			/// </summary>
			public ISettingValue<Vector3> AccelerometerBias { get; private set; }
			
			/// <summary>
			/// Gets Soft Iron Matrix setting.
			/// </summary>
			public ISettingValue<RotationMatrix> SoftIronMatrix { get; private set; }
			
			/// <summary>
			/// Gets Hard Iron Bias setting.
			/// </summary>
			public ISettingValue<Vector3> HardIronBias { get; private set; }
			
			/// <summary>
			/// Gets Calibration Date setting.
			/// </summary>
			public ISettingValue<string> CalibrationDate { get; private set; }
			
			/// <summary>
			/// Gets Calibration Temperature setting.
			/// </summary>
			public ISettingValue<float> CalibrationTemperature { get; private set; }
		
		    internal Calibration(Settings parent)
		    {
		        this.parent = parent; 
		
		    }
		
			internal override void AttachSettings(Settings settings)
			{
				CrystalTrim = settings.CrystalTrim;
				GyroscopeMisalignment = settings.GyroscopeMisalignment;
				GyroscopeSensitivity = settings.GyroscopeSensitivity;
				GyroscopeBias = settings.GyroscopeBias;
				AccelerometerMisalignment = settings.AccelerometerMisalignment;
				AccelerometerSensitivity = settings.AccelerometerSensitivity;
				AccelerometerBias = settings.AccelerometerBias;
				SoftIronMatrix = settings.SoftIronMatrix;
				HardIronBias = settings.HardIronBias;
				CalibrationDate = settings.CalibrationDate;
				CalibrationTemperature = settings.CalibrationTemperature;
				
				Add(settings.CrystalTrim);
				Add(settings.GyroscopeMisalignment);
				Add(settings.GyroscopeSensitivity);
				Add(settings.GyroscopeBias);
				Add(settings.AccelerometerMisalignment);
				Add(settings.AccelerometerSensitivity);
				Add(settings.AccelerometerBias);
				Add(settings.SoftIronMatrix);
				Add(settings.HardIronBias);
				Add(settings.CalibrationDate);
				Add(settings.CalibrationTemperature);
				Finalise();
			}
		
			/// <summary>
			/// Copy the values of all the settings in this category to another
			/// </summary>
			/// <param name="other">Another category</param>
		    public void CopyTo(Calibration other)
		    {
				if (CrystalTrim.IsValueUndefined == false) { other.CrystalTrim.Value = CrystalTrim.Value; }
				if (GyroscopeMisalignment.IsValueUndefined == false) { other.GyroscopeMisalignment.Value = GyroscopeMisalignment.Value; }
				if (GyroscopeSensitivity.IsValueUndefined == false) { other.GyroscopeSensitivity.Value = GyroscopeSensitivity.Value; }
				if (GyroscopeBias.IsValueUndefined == false) { other.GyroscopeBias.Value = GyroscopeBias.Value; }
				if (AccelerometerMisalignment.IsValueUndefined == false) { other.AccelerometerMisalignment.Value = AccelerometerMisalignment.Value; }
				if (AccelerometerSensitivity.IsValueUndefined == false) { other.AccelerometerSensitivity.Value = AccelerometerSensitivity.Value; }
				if (AccelerometerBias.IsValueUndefined == false) { other.AccelerometerBias.Value = AccelerometerBias.Value; }
				if (SoftIronMatrix.IsValueUndefined == false) { other.SoftIronMatrix.Value = SoftIronMatrix.Value; }
				if (HardIronBias.IsValueUndefined == false) { other.HardIronBias.Value = HardIronBias.Value; }
				if (CalibrationDate.IsValueUndefined == false) { other.CalibrationDate.Value = CalibrationDate.Value; }
				if (CalibrationTemperature.IsValueUndefined == false) { other.CalibrationTemperature.Value = CalibrationTemperature.Value; }
		    }
		}
		
		public sealed class DeviceInformation : SettingCategrory
		{
		    private Settings parent; 
		
			/// <summary>
			/// Gets the Connection that this setting category is bound to or null if it is unbound.
			/// </summary>
			public override Connection Connection { get { return parent.Connection; } }
		
			/// <summary>
			/// Gets the category text including the full path from the root category.
			/// </summary>
			public override string Text { get { return "Device Information"; } }
		
			/// <summary>
			/// Gets the category prefix text.
			/// </summary>
			protected override int CategoryPrefix { get { return 9; } }
		
			/// <summary>
			/// Gets the parent category of this category or null if this category is the root category.
			/// </summary>
			public override SettingCategrory Parent { get { return parent; } }
		
			/// <summary>
			/// Gets a flag to indicate if this category is hidden.
			/// </summary>
			public override bool IsHidden { get { return false; } }
		
			/// <summary>
			/// Gets Device Name setting.
			/// </summary>
			public ISettingValue<string> DeviceName { get; private set; }
			
			/// <summary>
			/// Gets Serial Number setting.
			/// </summary>
			public ISettingValue<string> SerialNumber { get; private set; }
			
			/// <summary>
			/// Gets Firmware Version setting.
			/// </summary>
			public ISettingValue<string> FirmwareVersion { get; private set; }
			
			/// <summary>
			/// Gets Bootloader Version setting.
			/// </summary>
			public ISettingValue<string> BootloaderVersion { get; private set; }
			
			/// <summary>
			/// Gets Hardware Version setting.
			/// </summary>
			public ISettingValue<string> HardwareVersion { get; private set; }
		
		    internal DeviceInformation(Settings parent)
		    {
		        this.parent = parent; 
		
		    }
		
			internal override void AttachSettings(Settings settings)
			{
				DeviceName = settings.DeviceName;
				SerialNumber = settings.SerialNumber;
				FirmwareVersion = settings.FirmwareVersion;
				BootloaderVersion = settings.BootloaderVersion;
				HardwareVersion = settings.HardwareVersion;
				
				Add(settings.DeviceName);
				Add(settings.SerialNumber);
				Add(settings.FirmwareVersion);
				Add(settings.BootloaderVersion);
				Add(settings.HardwareVersion);
				Finalise();
			}
		
			/// <summary>
			/// Copy the values of all the settings in this category to another
			/// </summary>
			/// <param name="other">Another category</param>
		    public void CopyTo(DeviceInformation other)
		    {
				if (DeviceName.IsValueUndefined == false) { other.DeviceName.Value = DeviceName.Value; }
				if (SerialNumber.IsValueUndefined == false) { other.SerialNumber.Value = SerialNumber.Value; }
				if (FirmwareVersion.IsValueUndefined == false) { other.FirmwareVersion.Value = FirmwareVersion.Value; }
				if (BootloaderVersion.IsValueUndefined == false) { other.BootloaderVersion.Value = BootloaderVersion.Value; }
				if (HardwareVersion.IsValueUndefined == false) { other.HardwareVersion.Value = HardwareVersion.Value; }
		    }
		}
		
		public sealed class AP : SettingCategrory
		{
		    private Wifi parent; 
		
			/// <summary>
			/// Gets the Connection that this setting category is bound to or null if it is unbound.
			/// </summary>
			public override Connection Connection { get { return parent.Connection; } }
		
			/// <summary>
			/// Gets the category text including the full path from the root category.
			/// </summary>
			public override string Text { get { return "AP"; } }
		
			/// <summary>
			/// Gets the category prefix text.
			/// </summary>
			protected override int CategoryPrefix { get { return 2; } }
		
			/// <summary>
			/// Gets the parent category of this category or null if this category is the root category.
			/// </summary>
			public override SettingCategrory Parent { get { return parent; } }
		
			/// <summary>
			/// Gets a flag to indicate if this category is hidden.
			/// </summary>
			public override bool IsHidden { get { return false; } }
		
			/// <summary>
			/// Gets Wi-Fi AP Band setting.
			/// </summary>
			public ISettingValue<WifiBand> WifiAPBand { get; private set; }
			
			/// <summary>
			/// Gets Wi-Fi AP 2.4 GHz Channel setting.
			/// </summary>
			public ISettingValue<Wifi2GHzChannel> WifiAP2GHzChannel { get; private set; }
			
			/// <summary>
			/// Gets Wi-Fi AP 5 GHz Channel setting.
			/// </summary>
			public ISettingValue<Wifi5GHzChannel> WifiAP5GHzChannel { get; private set; }
		
		    internal AP(Wifi parent)
		    {
		        this.parent = parent; 
		
		    }
		
			internal override void AttachSettings(Settings settings)
			{
				WifiAPBand = settings.WifiAPBand;
				WifiAP2GHzChannel = settings.WifiAP2GHzChannel;
				WifiAP5GHzChannel = settings.WifiAP5GHzChannel;
				
				Add(settings.WifiAPBand);
				Add(settings.WifiAP2GHzChannel);
				Add(settings.WifiAP5GHzChannel);
				Finalise();
			}
		
			/// <summary>
			/// Copy the values of all the settings in this category to another
			/// </summary>
			/// <param name="other">Another category</param>
		    public void CopyTo(AP other)
		    {
				if (WifiAPBand.IsValueUndefined == false) { other.WifiAPBand.Value = WifiAPBand.Value; }
				if (WifiAP2GHzChannel.IsValueUndefined == false) { other.WifiAP2GHzChannel.Value = WifiAP2GHzChannel.Value; }
				if (WifiAP5GHzChannel.IsValueUndefined == false) { other.WifiAP5GHzChannel.Value = WifiAP5GHzChannel.Value; }
		    }
		}
		
		public sealed class Client : SettingCategrory
		{
		    private Wifi parent; 
		
			/// <summary>
			/// Gets the Connection that this setting category is bound to or null if it is unbound.
			/// </summary>
			public override Connection Connection { get { return parent.Connection; } }
		
			/// <summary>
			/// Gets the category text including the full path from the root category.
			/// </summary>
			public override string Text { get { return "Client"; } }
		
			/// <summary>
			/// Gets the category prefix text.
			/// </summary>
			protected override int CategoryPrefix { get { return 2; } }
		
			/// <summary>
			/// Gets the parent category of this category or null if this category is the root category.
			/// </summary>
			public override SettingCategrory Parent { get { return parent; } }
		
			/// <summary>
			/// Gets a flag to indicate if this category is hidden.
			/// </summary>
			public override bool IsHidden { get { return false; } }
		
			/// <summary>
			/// Gets Wi-Fi Client SSID setting.
			/// </summary>
			public ISettingValue<string> WifiClientSSID { get; private set; }
			
			/// <summary>
			/// Gets Wi-Fi Client Key setting.
			/// </summary>
			public ISettingValue<string> WifiClientKey { get; private set; }
			
			/// <summary>
			/// Gets Wi-Fi Client DHCP Enabled setting.
			/// </summary>
			public ISettingValue<bool> WifiClientDhcpEnabled { get; private set; }
			
			/// <summary>
			/// Gets Wi-Fi Client IP Address setting.
			/// </summary>
			public ISettingValue<IPAddress> WifiClientIPAddress { get; private set; }
			
			/// <summary>
			/// Gets Wi-Fi Client Subnet setting.
			/// </summary>
			public ISettingValue<IPAddress> WifiClientSubnet { get; private set; }
			
			/// <summary>
			/// Gets Wi-Fi Client Gateway setting.
			/// </summary>
			public ISettingValue<IPAddress> WifiClientGateway { get; private set; }
			
			/// <summary>
			/// Gets Wi-Fi Client Low Power Mode setting.
			/// </summary>
			public ISettingValue<bool> WifiClientLowPower { get; private set; }
		
		    internal Client(Wifi parent)
		    {
		        this.parent = parent; 
		
		    }
		
			internal override void AttachSettings(Settings settings)
			{
				WifiClientSSID = settings.WifiClientSSID;
				WifiClientKey = settings.WifiClientKey;
				WifiClientDhcpEnabled = settings.WifiClientDhcpEnabled;
				WifiClientIPAddress = settings.WifiClientIPAddress;
				WifiClientSubnet = settings.WifiClientSubnet;
				WifiClientGateway = settings.WifiClientGateway;
				WifiClientLowPower = settings.WifiClientLowPower;
				
				Add(settings.WifiClientSSID);
				Add(settings.WifiClientKey);
				Add(settings.WifiClientDhcpEnabled);
				Add(settings.WifiClientIPAddress);
				Add(settings.WifiClientSubnet);
				Add(settings.WifiClientGateway);
				Add(settings.WifiClientLowPower);
				Finalise();
			}
		
			/// <summary>
			/// Copy the values of all the settings in this category to another
			/// </summary>
			/// <param name="other">Another category</param>
		    public void CopyTo(Client other)
		    {
				if (WifiClientSSID.IsValueUndefined == false) { other.WifiClientSSID.Value = WifiClientSSID.Value; }
				if (WifiClientKey.IsValueUndefined == false) { other.WifiClientKey.Value = WifiClientKey.Value; }
				if (WifiClientDhcpEnabled.IsValueUndefined == false) { other.WifiClientDhcpEnabled.Value = WifiClientDhcpEnabled.Value; }
				if (WifiClientIPAddress.IsValueUndefined == false) { other.WifiClientIPAddress.Value = WifiClientIPAddress.Value; }
				if (WifiClientSubnet.IsValueUndefined == false) { other.WifiClientSubnet.Value = WifiClientSubnet.Value; }
				if (WifiClientGateway.IsValueUndefined == false) { other.WifiClientGateway.Value = WifiClientGateway.Value; }
				if (WifiClientLowPower.IsValueUndefined == false) { other.WifiClientLowPower.Value = WifiClientLowPower.Value; }
		    }
		}
		
		public sealed class Udp : SettingCategrory
		{
		    private Wifi parent; 
		
			/// <summary>
			/// Gets the Connection that this setting category is bound to or null if it is unbound.
			/// </summary>
			public override Connection Connection { get { return parent.Connection; } }
		
			/// <summary>
			/// Gets the category text including the full path from the root category.
			/// </summary>
			public override string Text { get { return "UDP"; } }
		
			/// <summary>
			/// Gets the category prefix text.
			/// </summary>
			protected override int CategoryPrefix { get { return 2; } }
		
			/// <summary>
			/// Gets the parent category of this category or null if this category is the root category.
			/// </summary>
			public override SettingCategrory Parent { get { return parent; } }
		
			/// <summary>
			/// Gets a flag to indicate if this category is hidden.
			/// </summary>
			public override bool IsHidden { get { return false; } }
		
			/// <summary>
			/// Gets UDP Send IP Address setting.
			/// </summary>
			public ISettingValue<IPAddress> WifiSendIPAddress { get; private set; }
			
			/// <summary>
			/// Gets UDP Send Port setting.
			/// </summary>
			public ISettingValue<ushort> WifiSendPort { get; private set; }
			
			/// <summary>
			/// Gets UDP Receive Port setting.
			/// </summary>
			public ISettingValue<ushort> WifiReceivePort { get; private set; }
			
			/// <summary>
			/// Gets UDP Maximise Throughput setting.
			/// </summary>
			public ISettingValue<bool> WifiMaximiseThroughput { get; private set; }
		
		    internal Udp(Wifi parent)
		    {
		        this.parent = parent; 
		
		    }
		
			internal override void AttachSettings(Settings settings)
			{
				WifiSendIPAddress = settings.WifiSendIPAddress;
				WifiSendPort = settings.WifiSendPort;
				WifiReceivePort = settings.WifiReceivePort;
				WifiMaximiseThroughput = settings.WifiMaximiseThroughput;
				
				Add(settings.WifiSendIPAddress);
				Add(settings.WifiSendPort);
				Add(settings.WifiReceivePort);
				Add(settings.WifiMaximiseThroughput);
				Finalise();
			}
		
			/// <summary>
			/// Copy the values of all the settings in this category to another
			/// </summary>
			/// <param name="other">Another category</param>
		    public void CopyTo(Udp other)
		    {
				if (WifiSendIPAddress.IsValueUndefined == false) { other.WifiSendIPAddress.Value = WifiSendIPAddress.Value; }
				if (WifiSendPort.IsValueUndefined == false) { other.WifiSendPort.Value = WifiSendPort.Value; }
				if (WifiReceivePort.IsValueUndefined == false) { other.WifiReceivePort.Value = WifiReceivePort.Value; }
				if (WifiMaximiseThroughput.IsValueUndefined == false) { other.WifiMaximiseThroughput.Value = WifiMaximiseThroughput.Value; }
		    }
		}
		
		public sealed class SynchronisationMaster : SettingCategrory
		{
		    private Wifi parent; 
		
			/// <summary>
			/// Gets the Connection that this setting category is bound to or null if it is unbound.
			/// </summary>
			public override Connection Connection { get { return parent.Connection; } }
		
			/// <summary>
			/// Gets the category text including the full path from the root category.
			/// </summary>
			public override string Text { get { return "Synchronisation Master"; } }
		
			/// <summary>
			/// Gets the category prefix text.
			/// </summary>
			protected override int CategoryPrefix { get { return 2; } }
		
			/// <summary>
			/// Gets the parent category of this category or null if this category is the root category.
			/// </summary>
			public override SettingCategrory Parent { get { return parent; } }
		
			/// <summary>
			/// Gets a flag to indicate if this category is hidden.
			/// </summary>
			public override bool IsHidden { get { return false; } }
		
			/// <summary>
			/// Gets Synchronisation Master Enabled setting.
			/// </summary>
			public ISettingValue<bool> SynchronisationMasterEnabled { get; private set; }
			
			/// <summary>
			/// Gets Synchronisation Master Send Port setting.
			/// </summary>
			public ISettingValue<ushort> SynchronisationMasterPort { get; private set; }
			
			/// <summary>
			/// Gets Synchronisation Network Latency setting.
			/// </summary>
			public ISettingValue<uint> SynchronisationMasterLatency { get; private set; }
		
		    internal SynchronisationMaster(Wifi parent)
		    {
		        this.parent = parent; 
		
		    }
		
			internal override void AttachSettings(Settings settings)
			{
				SynchronisationMasterEnabled = settings.SynchronisationMasterEnabled;
				SynchronisationMasterPort = settings.SynchronisationMasterPort;
				SynchronisationMasterLatency = settings.SynchronisationMasterLatency;
				
				Add(settings.SynchronisationMasterEnabled);
				Add(settings.SynchronisationMasterPort);
				Add(settings.SynchronisationMasterLatency);
				Finalise();
			}
		
			/// <summary>
			/// Copy the values of all the settings in this category to another
			/// </summary>
			/// <param name="other">Another category</param>
		    public void CopyTo(SynchronisationMaster other)
		    {
				if (SynchronisationMasterEnabled.IsValueUndefined == false) { other.SynchronisationMasterEnabled.Value = SynchronisationMasterEnabled.Value; }
				if (SynchronisationMasterPort.IsValueUndefined == false) { other.SynchronisationMasterPort.Value = SynchronisationMasterPort.Value; }
				if (SynchronisationMasterLatency.IsValueUndefined == false) { other.SynchronisationMasterLatency.Value = SynchronisationMasterLatency.Value; }
		    }
		}
		
		public sealed class Wifi : SettingCategrory
		{
		    private Settings parent; 
		
			/// <summary>
			/// Gets the Connection that this setting category is bound to or null if it is unbound.
			/// </summary>
			public override Connection Connection { get { return parent.Connection; } }
		
			/// <summary>
			/// Gets the category text including the full path from the root category.
			/// </summary>
			public override string Text { get { return "Wi-Fi"; } }
		
			/// <summary>
			/// Gets the category prefix text.
			/// </summary>
			protected override int CategoryPrefix { get { return 8; } }
		
			/// <summary>
			/// Gets the parent category of this category or null if this category is the root category.
			/// </summary>
			public override SettingCategrory Parent { get { return parent; } }
		
			/// <summary>
			/// Gets a flag to indicate if this category is hidden.
			/// </summary>
			public override bool IsHidden { get { return false; } }
		
			/// <summary>
			/// Gets Wi-Fi Enabled setting.
			/// </summary>
			public ISettingValue<bool> WifiEnabled { get; private set; }
			
			/// <summary>
			/// Gets Wi-Fi Firmware Version setting.
			/// </summary>
			public ISettingValue<string> WifiFirmwareVersion { get; private set; }
			
			/// <summary>
			/// Gets Wi-Fi MAC Address setting.
			/// </summary>
			public ISettingValue<PhysicalAddress> WifiMacAddress { get; private set; }
			
			/// <summary>
			/// Gets Wi-Fi IP Address setting.
			/// </summary>
			public ISettingValue<IPAddress> WifiIPAddress { get; private set; }
			
			/// <summary>
			/// Gets Wi-Fi Mode setting.
			/// </summary>
			public ISettingValue<WifiMode> WifiMode { get; private set; }
			
			/// <summary>
			/// Gets Wi-Fi Antenna setting.
			/// </summary>
			public ISettingValue<WifiAntenna> WifiAntenna { get; private set; }
			
			/// <summary>
			/// Gets Wi-Fi Region setting.
			/// </summary>
			public ISettingValue<WifiRegion> WifiRegion { get; private set; }
			
			/// <summary>
			/// Gets AP settings category.
			/// </summary>
			public SettingsCategoryTypes.AP AP { get; private set; }
			
			/// <summary>
			/// Gets Client settings category.
			/// </summary>
			public SettingsCategoryTypes.Client Client { get; private set; }
			
			/// <summary>
			/// Gets UDP settings category.
			/// </summary>
			public SettingsCategoryTypes.Udp Udp { get; private set; }
			
			/// <summary>
			/// Gets Synchronisation Master settings category.
			/// </summary>
			public SettingsCategoryTypes.SynchronisationMaster SynchronisationMaster { get; private set; }
		
		    internal Wifi(Settings parent)
		    {
		        this.parent = parent; 
				AP = new SettingsCategoryTypes.AP(this);
				Client = new SettingsCategoryTypes.Client(this);
				Udp = new SettingsCategoryTypes.Udp(this);
				SynchronisationMaster = new SettingsCategoryTypes.SynchronisationMaster(this);
		    }
		
			internal override void AttachSettings(Settings settings)
			{
				WifiEnabled = settings.WifiEnabled;
				WifiFirmwareVersion = settings.WifiFirmwareVersion;
				WifiMacAddress = settings.WifiMacAddress;
				WifiIPAddress = settings.WifiIPAddress;
				WifiMode = settings.WifiMode;
				WifiAntenna = settings.WifiAntenna;
				WifiRegion = settings.WifiRegion;
				AP.AttachSettings(settings);
				Client.AttachSettings(settings);
				Udp.AttachSettings(settings);
				SynchronisationMaster.AttachSettings(settings);
				
				Add(settings.WifiEnabled);
				Add(settings.WifiFirmwareVersion);
				Add(settings.WifiMacAddress);
				Add(settings.WifiIPAddress);
				Add(settings.WifiMode);
				Add(settings.WifiAntenna);
				Add(settings.WifiRegion);
				Add(AP);
				Add(Client);
				Add(Udp);
				Add(SynchronisationMaster);
				Finalise();
			}
		
			/// <summary>
			/// Copy the values of all the settings in this category to another
			/// </summary>
			/// <param name="other">Another category</param>
		    public void CopyTo(Wifi other)
		    {
				if (WifiEnabled.IsValueUndefined == false) { other.WifiEnabled.Value = WifiEnabled.Value; }
				if (WifiFirmwareVersion.IsValueUndefined == false) { other.WifiFirmwareVersion.Value = WifiFirmwareVersion.Value; }
				if (WifiMacAddress.IsValueUndefined == false) { other.WifiMacAddress.Value = WifiMacAddress.Value; }
				if (WifiIPAddress.IsValueUndefined == false) { other.WifiIPAddress.Value = WifiIPAddress.Value; }
				if (WifiMode.IsValueUndefined == false) { other.WifiMode.Value = WifiMode.Value; }
				if (WifiAntenna.IsValueUndefined == false) { other.WifiAntenna.Value = WifiAntenna.Value; }
				if (WifiRegion.IsValueUndefined == false) { other.WifiRegion.Value = WifiRegion.Value; }
				AP.CopyTo(other.AP);
				Client.CopyTo(other.Client);
				Udp.CopyTo(other.Udp);
				SynchronisationMaster.CopyTo(other.SynchronisationMaster);
		    }
		}
		
		public sealed class Serial : SettingCategrory
		{
		    private Settings parent; 
		
			/// <summary>
			/// Gets the Connection that this setting category is bound to or null if it is unbound.
			/// </summary>
			public override Connection Connection { get { return parent.Connection; } }
		
			/// <summary>
			/// Gets the category text including the full path from the root category.
			/// </summary>
			public override string Text { get { return "Serial"; } }
		
			/// <summary>
			/// Gets the category prefix text.
			/// </summary>
			protected override int CategoryPrefix { get { return 7; } }
		
			/// <summary>
			/// Gets the parent category of this category or null if this category is the root category.
			/// </summary>
			public override SettingCategrory Parent { get { return parent; } }
		
			/// <summary>
			/// Gets a flag to indicate if this category is hidden.
			/// </summary>
			public override bool IsHidden { get { return false; } }
		
			/// <summary>
			/// Gets Serial Enabled setting.
			/// </summary>
			public ISettingValue<bool> SerialEnabled { get; private set; }
			
			/// <summary>
			/// Gets Serial Baud Rate setting.
			/// </summary>
			public ISettingValue<uint> SerialBaudRate { get; private set; }
			
			/// <summary>
			/// Gets Serial Baud Rate Error setting.
			/// </summary>
			public ISettingValue<float> SerialBaudRateError { get; private set; }
			
			/// <summary>
			/// Gets Serial RTS/CTS Enabled setting.
			/// </summary>
			public ISettingValue<bool> SerialRtsCtsEnabled { get; private set; }
			
			/// <summary>
			/// Gets Serial Invert Data Lines setting.
			/// </summary>
			public ISettingValue<bool> SerialInvertDataLines { get; private set; }
		
		    internal Serial(Settings parent)
		    {
		        this.parent = parent; 
		
		    }
		
			internal override void AttachSettings(Settings settings)
			{
				SerialEnabled = settings.SerialEnabled;
				SerialBaudRate = settings.SerialBaudRate;
				SerialBaudRateError = settings.SerialBaudRateError;
				SerialRtsCtsEnabled = settings.SerialRtsCtsEnabled;
				SerialInvertDataLines = settings.SerialInvertDataLines;
				
				Add(settings.SerialEnabled);
				Add(settings.SerialBaudRate);
				Add(settings.SerialBaudRateError);
				Add(settings.SerialRtsCtsEnabled);
				Add(settings.SerialInvertDataLines);
				Finalise();
			}
		
			/// <summary>
			/// Copy the values of all the settings in this category to another
			/// </summary>
			/// <param name="other">Another category</param>
		    public void CopyTo(Serial other)
		    {
				if (SerialEnabled.IsValueUndefined == false) { other.SerialEnabled.Value = SerialEnabled.Value; }
				if (SerialBaudRate.IsValueUndefined == false) { other.SerialBaudRate.Value = SerialBaudRate.Value; }
				if (SerialBaudRateError.IsValueUndefined == false) { other.SerialBaudRateError.Value = SerialBaudRateError.Value; }
				if (SerialRtsCtsEnabled.IsValueUndefined == false) { other.SerialRtsCtsEnabled.Value = SerialRtsCtsEnabled.Value; }
				if (SerialInvertDataLines.IsValueUndefined == false) { other.SerialInvertDataLines.Value = SerialInvertDataLines.Value; }
		    }
		}
		
		public sealed class SDCard : SettingCategrory
		{
		    private Settings parent; 
		
			/// <summary>
			/// Gets the Connection that this setting category is bound to or null if it is unbound.
			/// </summary>
			public override Connection Connection { get { return parent.Connection; } }
		
			/// <summary>
			/// Gets the category text including the full path from the root category.
			/// </summary>
			public override string Text { get { return "SD Card"; } }
		
			/// <summary>
			/// Gets the category prefix text.
			/// </summary>
			protected override int CategoryPrefix { get { return 6; } }
		
			/// <summary>
			/// Gets the parent category of this category or null if this category is the root category.
			/// </summary>
			public override SettingCategrory Parent { get { return parent; } }
		
			/// <summary>
			/// Gets a flag to indicate if this category is hidden.
			/// </summary>
			public override bool IsHidden { get { return false; } }
		
			/// <summary>
			/// Gets SD Card File Name Prefix setting.
			/// </summary>
			public ISettingValue<string> sdCardFileNamePrefix { get; private set; }
			
			/// <summary>
			/// Gets SD Card File Number setting.
			/// </summary>
			public ISettingValue<uint> sdCardFileNumber { get; private set; }
			
			/// <summary>
			/// Gets SD Card Maximum File Size setting.
			/// </summary>
			public ISettingValue<uint> sdCardMaxFileSize { get; private set; }
			
			/// <summary>
			/// Gets SD Card Maximum File Period setting.
			/// </summary>
			public ISettingValue<uint> sdCardMaxFilePeriod { get; private set; }
		
		    internal SDCard(Settings parent)
		    {
		        this.parent = parent; 
		
		    }
		
			internal override void AttachSettings(Settings settings)
			{
				sdCardFileNamePrefix = settings.sdCardFileNamePrefix;
				sdCardFileNumber = settings.sdCardFileNumber;
				sdCardMaxFileSize = settings.sdCardMaxFileSize;
				sdCardMaxFilePeriod = settings.sdCardMaxFilePeriod;
				
				Add(settings.sdCardFileNamePrefix);
				Add(settings.sdCardFileNumber);
				Add(settings.sdCardMaxFileSize);
				Add(settings.sdCardMaxFilePeriod);
				Finalise();
			}
		
			/// <summary>
			/// Copy the values of all the settings in this category to another
			/// </summary>
			/// <param name="other">Another category</param>
		    public void CopyTo(SDCard other)
		    {
				if (sdCardFileNamePrefix.IsValueUndefined == false) { other.sdCardFileNamePrefix.Value = sdCardFileNamePrefix.Value; }
				if (sdCardFileNumber.IsValueUndefined == false) { other.sdCardFileNumber.Value = sdCardFileNumber.Value; }
				if (sdCardMaxFileSize.IsValueUndefined == false) { other.sdCardMaxFileSize.Value = sdCardMaxFileSize.Value; }
				if (sdCardMaxFilePeriod.IsValueUndefined == false) { other.sdCardMaxFilePeriod.Value = sdCardMaxFilePeriod.Value; }
		    }
		}
		
		public sealed class MagneticFieldRejection : SettingCategrory
		{
		    private Ahrs parent; 
		
			/// <summary>
			/// Gets the Connection that this setting category is bound to or null if it is unbound.
			/// </summary>
			public override Connection Connection { get { return parent.Connection; } }
		
			/// <summary>
			/// Gets the category text including the full path from the root category.
			/// </summary>
			public override string Text { get { return "Magnetic Field Rejection"; } }
		
			/// <summary>
			/// Gets the category prefix text.
			/// </summary>
			protected override int CategoryPrefix { get { return 2; } }
		
			/// <summary>
			/// Gets the parent category of this category or null if this category is the root category.
			/// </summary>
			public override SettingCategrory Parent { get { return parent; } }
		
			/// <summary>
			/// Gets a flag to indicate if this category is hidden.
			/// </summary>
			public override bool IsHidden { get { return false; } }
		
			/// <summary>
			/// Gets AHRS Minimum Magnetic Field setting.
			/// </summary>
			public ISettingValue<float> AhrsMinimumMagneticField { get; private set; }
			
			/// <summary>
			/// Gets AHRS Maximum Magnetic Field setting.
			/// </summary>
			public ISettingValue<float> AhrsMaximumMagneticField { get; private set; }
		
		    internal MagneticFieldRejection(Ahrs parent)
		    {
		        this.parent = parent; 
		
		    }
		
			internal override void AttachSettings(Settings settings)
			{
				AhrsMinimumMagneticField = settings.AhrsMinimumMagneticField;
				AhrsMaximumMagneticField = settings.AhrsMaximumMagneticField;
				
				Add(settings.AhrsMinimumMagneticField);
				Add(settings.AhrsMaximumMagneticField);
				Finalise();
			}
		
			/// <summary>
			/// Copy the values of all the settings in this category to another
			/// </summary>
			/// <param name="other">Another category</param>
		    public void CopyTo(MagneticFieldRejection other)
		    {
				if (AhrsMinimumMagneticField.IsValueUndefined == false) { other.AhrsMinimumMagneticField.Value = AhrsMinimumMagneticField.Value; }
				if (AhrsMaximumMagneticField.IsValueUndefined == false) { other.AhrsMaximumMagneticField.Value = AhrsMaximumMagneticField.Value; }
		    }
		}
		
		public sealed class Ahrs : SettingCategrory
		{
		    private Settings parent; 
		
			/// <summary>
			/// Gets the Connection that this setting category is bound to or null if it is unbound.
			/// </summary>
			public override Connection Connection { get { return parent.Connection; } }
		
			/// <summary>
			/// Gets the category text including the full path from the root category.
			/// </summary>
			public override string Text { get { return "AHRS"; } }
		
			/// <summary>
			/// Gets the category prefix text.
			/// </summary>
			protected override int CategoryPrefix { get { return 5; } }
		
			/// <summary>
			/// Gets the parent category of this category or null if this category is the root category.
			/// </summary>
			public override SettingCategrory Parent { get { return parent; } }
		
			/// <summary>
			/// Gets a flag to indicate if this category is hidden.
			/// </summary>
			public override bool IsHidden { get { return false; } }
		
			/// <summary>
			/// Gets AHRS Gain setting.
			/// </summary>
			public ISettingValue<float> AhrsGain { get; private set; }
			
			/// <summary>
			/// Gets AHRS Gyroscope Bias Correction setting.
			/// </summary>
			public ISettingValue<bool> AhrsGyroscopeBiasCorrection { get; private set; }
			
			/// <summary>
			/// Gets AHRS Ignore Magnetometer setting.
			/// </summary>
			public ISettingValue<bool> AhrsIgnoreMagnetometer { get; private set; }
			
			/// <summary>
			/// Gets Magnetic Field Rejection settings category.
			/// </summary>
			public SettingsCategoryTypes.MagneticFieldRejection MagneticFieldRejection { get; private set; }
		
		    internal Ahrs(Settings parent)
		    {
		        this.parent = parent; 
				MagneticFieldRejection = new SettingsCategoryTypes.MagneticFieldRejection(this);
		    }
		
			internal override void AttachSettings(Settings settings)
			{
				AhrsGain = settings.AhrsGain;
				AhrsGyroscopeBiasCorrection = settings.AhrsGyroscopeBiasCorrection;
				AhrsIgnoreMagnetometer = settings.AhrsIgnoreMagnetometer;
				MagneticFieldRejection.AttachSettings(settings);
				
				Add(settings.AhrsGain);
				Add(settings.AhrsGyroscopeBiasCorrection);
				Add(settings.AhrsIgnoreMagnetometer);
				Add(MagneticFieldRejection);
				Finalise();
			}
		
			/// <summary>
			/// Copy the values of all the settings in this category to another
			/// </summary>
			/// <param name="other">Another category</param>
		    public void CopyTo(Ahrs other)
		    {
				if (AhrsGain.IsValueUndefined == false) { other.AhrsGain.Value = AhrsGain.Value; }
				if (AhrsGyroscopeBiasCorrection.IsValueUndefined == false) { other.AhrsGyroscopeBiasCorrection.Value = AhrsGyroscopeBiasCorrection.Value; }
				if (AhrsIgnoreMagnetometer.IsValueUndefined == false) { other.AhrsIgnoreMagnetometer.Value = AhrsIgnoreMagnetometer.Value; }
				MagneticFieldRejection.CopyTo(other.MagneticFieldRejection);
		    }
		}
		
		public sealed class SendRates : SettingCategrory
		{
		    private Settings parent; 
		
			/// <summary>
			/// Gets the Connection that this setting category is bound to or null if it is unbound.
			/// </summary>
			public override Connection Connection { get { return parent.Connection; } }
		
			/// <summary>
			/// Gets the category text including the full path from the root category.
			/// </summary>
			public override string Text { get { return "Send Rates"; } }
		
			/// <summary>
			/// Gets the category prefix text.
			/// </summary>
			protected override int CategoryPrefix { get { return 4; } }
		
			/// <summary>
			/// Gets the parent category of this category or null if this category is the root category.
			/// </summary>
			public override SettingCategrory Parent { get { return parent; } }
		
			/// <summary>
			/// Gets a flag to indicate if this category is hidden.
			/// </summary>
			public override bool IsHidden { get { return false; } }
		
			/// <summary>
			/// Gets Sensors Send Rate setting.
			/// </summary>
			public ISettingValue<float> SendRateSensors { get; private set; }
			
			/// <summary>
			/// Gets Magnitudes Send Rate setting.
			/// </summary>
			public ISettingValue<float> SendRateMagnitudes { get; private set; }
			
			/// <summary>
			/// Gets Quaternion Send Rate setting.
			/// </summary>
			public ISettingValue<float> SendRateQuaternion { get; private set; }
			
			/// <summary>
			/// Gets Rotation Matrix Send Rate setting.
			/// </summary>
			public ISettingValue<float> SendRateRotationMatrix { get; private set; }
			
			/// <summary>
			/// Gets Euler Angles Send Rate setting.
			/// </summary>
			public ISettingValue<float> SendRateEulerAngles { get; private set; }
			
			/// <summary>
			/// Gets Linear Acceleration Send Rate setting.
			/// </summary>
			public ISettingValue<float> SendRateLinearAcceleration { get; private set; }
			
			/// <summary>
			/// Gets Earth Acceleration Send Rate setting.
			/// </summary>
			public ISettingValue<float> SendRateEarthAcceleration { get; private set; }
			
			/// <summary>
			/// Gets Altitude Send Rate setting.
			/// </summary>
			public ISettingValue<float> SendRateAltitude { get; private set; }
			
			/// <summary>
			/// Gets Temperature Send Rate setting.
			/// </summary>
			public ISettingValue<float> SendRateTemperature { get; private set; }
			
			/// <summary>
			/// Gets Humidity Send Rate setting.
			/// </summary>
			public ISettingValue<float> SendRateHumidity { get; private set; }
			
			/// <summary>
			/// Gets Battery Send Rate setting.
			/// </summary>
			public ISettingValue<float> SendRateBattery { get; private set; }
			
			/// <summary>
			/// Gets Analogue Inputs Send Rate setting.
			/// </summary>
			public ISettingValue<float> SendRateAnalogueInputs { get; private set; }
			
			/// <summary>
			/// Gets RSSI Send Rate setting.
			/// </summary>
			public ISettingValue<float> SendRateRssi { get; private set; }
		
		    internal SendRates(Settings parent)
		    {
		        this.parent = parent; 
		
		    }
		
			internal override void AttachSettings(Settings settings)
			{
				SendRateSensors = settings.SendRateSensors;
				SendRateMagnitudes = settings.SendRateMagnitudes;
				SendRateQuaternion = settings.SendRateQuaternion;
				SendRateRotationMatrix = settings.SendRateRotationMatrix;
				SendRateEulerAngles = settings.SendRateEulerAngles;
				SendRateLinearAcceleration = settings.SendRateLinearAcceleration;
				SendRateEarthAcceleration = settings.SendRateEarthAcceleration;
				SendRateAltitude = settings.SendRateAltitude;
				SendRateTemperature = settings.SendRateTemperature;
				SendRateHumidity = settings.SendRateHumidity;
				SendRateBattery = settings.SendRateBattery;
				SendRateAnalogueInputs = settings.SendRateAnalogueInputs;
				SendRateRssi = settings.SendRateRssi;
				
				Add(settings.SendRateSensors);
				Add(settings.SendRateMagnitudes);
				Add(settings.SendRateQuaternion);
				Add(settings.SendRateRotationMatrix);
				Add(settings.SendRateEulerAngles);
				Add(settings.SendRateLinearAcceleration);
				Add(settings.SendRateEarthAcceleration);
				Add(settings.SendRateAltitude);
				Add(settings.SendRateTemperature);
				Add(settings.SendRateHumidity);
				Add(settings.SendRateBattery);
				Add(settings.SendRateAnalogueInputs);
				Add(settings.SendRateRssi);
				Finalise();
			}
		
			/// <summary>
			/// Copy the values of all the settings in this category to another
			/// </summary>
			/// <param name="other">Another category</param>
		    public void CopyTo(SendRates other)
		    {
				if (SendRateSensors.IsValueUndefined == false) { other.SendRateSensors.Value = SendRateSensors.Value; }
				if (SendRateMagnitudes.IsValueUndefined == false) { other.SendRateMagnitudes.Value = SendRateMagnitudes.Value; }
				if (SendRateQuaternion.IsValueUndefined == false) { other.SendRateQuaternion.Value = SendRateQuaternion.Value; }
				if (SendRateRotationMatrix.IsValueUndefined == false) { other.SendRateRotationMatrix.Value = SendRateRotationMatrix.Value; }
				if (SendRateEulerAngles.IsValueUndefined == false) { other.SendRateEulerAngles.Value = SendRateEulerAngles.Value; }
				if (SendRateLinearAcceleration.IsValueUndefined == false) { other.SendRateLinearAcceleration.Value = SendRateLinearAcceleration.Value; }
				if (SendRateEarthAcceleration.IsValueUndefined == false) { other.SendRateEarthAcceleration.Value = SendRateEarthAcceleration.Value; }
				if (SendRateAltitude.IsValueUndefined == false) { other.SendRateAltitude.Value = SendRateAltitude.Value; }
				if (SendRateTemperature.IsValueUndefined == false) { other.SendRateTemperature.Value = SendRateTemperature.Value; }
				if (SendRateHumidity.IsValueUndefined == false) { other.SendRateHumidity.Value = SendRateHumidity.Value; }
				if (SendRateBattery.IsValueUndefined == false) { other.SendRateBattery.Value = SendRateBattery.Value; }
				if (SendRateAnalogueInputs.IsValueUndefined == false) { other.SendRateAnalogueInputs.Value = SendRateAnalogueInputs.Value; }
				if (SendRateRssi.IsValueUndefined == false) { other.SendRateRssi.Value = SendRateRssi.Value; }
		    }
		}
		
		public sealed class SendCondition : SettingCategrory
		{
		    private AuxiliarySerial parent; 
		
			/// <summary>
			/// Gets the Connection that this setting category is bound to or null if it is unbound.
			/// </summary>
			public override Connection Connection { get { return parent.Connection; } }
		
			/// <summary>
			/// Gets the category text including the full path from the root category.
			/// </summary>
			public override string Text { get { return "Send Condition"; } }
		
			/// <summary>
			/// Gets the category prefix text.
			/// </summary>
			protected override int CategoryPrefix { get { return 2; } }
		
			/// <summary>
			/// Gets the parent category of this category or null if this category is the root category.
			/// </summary>
			public override SettingCategrory Parent { get { return parent; } }
		
			/// <summary>
			/// Gets a flag to indicate if this category is hidden.
			/// </summary>
			public override bool IsHidden { get { return false; } }
		
			/// <summary>
			/// Gets Auxiliary Serial Send Buffer Size setting.
			/// </summary>
			public ISettingValue<uint> AuxSerialBufferSize { get; private set; }
			
			/// <summary>
			/// Gets Auxiliary Serial Send Timeout setting.
			/// </summary>
			public ISettingValue<uint> AuxSerialTimeout { get; private set; }
			
			/// <summary>
			/// Gets Auxiliary Serial Send Framing Character setting.
			/// </summary>
			public ISettingValue<int> AuxSerialFramingCharacter { get; private set; }
		
		    internal SendCondition(AuxiliarySerial parent)
		    {
		        this.parent = parent; 
		
		    }
		
			internal override void AttachSettings(Settings settings)
			{
				AuxSerialBufferSize = settings.AuxSerialBufferSize;
				AuxSerialTimeout = settings.AuxSerialTimeout;
				AuxSerialFramingCharacter = settings.AuxSerialFramingCharacter;
				
				Add(settings.AuxSerialBufferSize);
				Add(settings.AuxSerialTimeout);
				Add(settings.AuxSerialFramingCharacter);
				Finalise();
			}
		
			/// <summary>
			/// Copy the values of all the settings in this category to another
			/// </summary>
			/// <param name="other">Another category</param>
		    public void CopyTo(SendCondition other)
		    {
				if (AuxSerialBufferSize.IsValueUndefined == false) { other.AuxSerialBufferSize.Value = AuxSerialBufferSize.Value; }
				if (AuxSerialTimeout.IsValueUndefined == false) { other.AuxSerialTimeout.Value = AuxSerialTimeout.Value; }
				if (AuxSerialFramingCharacter.IsValueUndefined == false) { other.AuxSerialFramingCharacter.Value = AuxSerialFramingCharacter.Value; }
		    }
		}
		
		public sealed class AuxiliarySerial : SettingCategrory
		{
		    private Settings parent; 
		
			/// <summary>
			/// Gets the Connection that this setting category is bound to or null if it is unbound.
			/// </summary>
			public override Connection Connection { get { return parent.Connection; } }
		
			/// <summary>
			/// Gets the category text including the full path from the root category.
			/// </summary>
			public override string Text { get { return "Auxiliary Serial"; } }
		
			/// <summary>
			/// Gets the category prefix text.
			/// </summary>
			protected override int CategoryPrefix { get { return 3; } }
		
			/// <summary>
			/// Gets the parent category of this category or null if this category is the root category.
			/// </summary>
			public override SettingCategrory Parent { get { return parent; } }
		
			/// <summary>
			/// Gets a flag to indicate if this category is hidden.
			/// </summary>
			public override bool IsHidden { get { return false; } }
		
			/// <summary>
			/// Gets Auxiliary Serial Enabled setting.
			/// </summary>
			public ISettingValue<bool> AuxSerialEnabled { get; private set; }
			
			/// <summary>
			/// Gets Auxiliary Serial Baud Rate setting.
			/// </summary>
			public ISettingValue<uint> AuxSerialBaudRate { get; private set; }
			
			/// <summary>
			/// Gets Auxiliary Serial Baud Rate Error setting.
			/// </summary>
			public ISettingValue<float> AuxSerialBaudRateError { get; private set; }
			
			/// <summary>
			/// Gets Auxiliary Serial RTS/CTS Enabled setting.
			/// </summary>
			public ISettingValue<bool> AuxSerialRtsCtsEnabled { get; private set; }
			
			/// <summary>
			/// Gets Auxiliary Serial Invert Data Lines setting.
			/// </summary>
			public ISettingValue<bool> AuxSerialInvertDataLines { get; private set; }
			
			/// <summary>
			/// Gets Auxiliary Serial OSC Passthrough setting.
			/// </summary>
			public ISettingValue<bool> AuxSerialOscPassthrough { get; private set; }
			
			/// <summary>
			/// Gets Auxiliary Serial Send As String setting.
			/// </summary>
			public ISettingValue<bool> AuxSerialSendAsString { get; private set; }
			
			/// <summary>
			/// Gets Send Condition settings category.
			/// </summary>
			public SettingsCategoryTypes.SendCondition SendCondition { get; private set; }
		
		    internal AuxiliarySerial(Settings parent)
		    {
		        this.parent = parent; 
				SendCondition = new SettingsCategoryTypes.SendCondition(this);
		    }
		
			internal override void AttachSettings(Settings settings)
			{
				AuxSerialEnabled = settings.AuxSerialEnabled;
				AuxSerialBaudRate = settings.AuxSerialBaudRate;
				AuxSerialBaudRateError = settings.AuxSerialBaudRateError;
				AuxSerialRtsCtsEnabled = settings.AuxSerialRtsCtsEnabled;
				AuxSerialInvertDataLines = settings.AuxSerialInvertDataLines;
				AuxSerialOscPassthrough = settings.AuxSerialOscPassthrough;
				AuxSerialSendAsString = settings.AuxSerialSendAsString;
				SendCondition.AttachSettings(settings);
				
				Add(settings.AuxSerialEnabled);
				Add(settings.AuxSerialBaudRate);
				Add(settings.AuxSerialBaudRateError);
				Add(settings.AuxSerialRtsCtsEnabled);
				Add(settings.AuxSerialInvertDataLines);
				Add(settings.AuxSerialOscPassthrough);
				Add(settings.AuxSerialSendAsString);
				Add(SendCondition);
				Finalise();
			}
		
			/// <summary>
			/// Copy the values of all the settings in this category to another
			/// </summary>
			/// <param name="other">Another category</param>
		    public void CopyTo(AuxiliarySerial other)
		    {
				if (AuxSerialEnabled.IsValueUndefined == false) { other.AuxSerialEnabled.Value = AuxSerialEnabled.Value; }
				if (AuxSerialBaudRate.IsValueUndefined == false) { other.AuxSerialBaudRate.Value = AuxSerialBaudRate.Value; }
				if (AuxSerialBaudRateError.IsValueUndefined == false) { other.AuxSerialBaudRateError.Value = AuxSerialBaudRateError.Value; }
				if (AuxSerialRtsCtsEnabled.IsValueUndefined == false) { other.AuxSerialRtsCtsEnabled.Value = AuxSerialRtsCtsEnabled.Value; }
				if (AuxSerialInvertDataLines.IsValueUndefined == false) { other.AuxSerialInvertDataLines.Value = AuxSerialInvertDataLines.Value; }
				if (AuxSerialOscPassthrough.IsValueUndefined == false) { other.AuxSerialOscPassthrough.Value = AuxSerialOscPassthrough.Value; }
				if (AuxSerialSendAsString.IsValueUndefined == false) { other.AuxSerialSendAsString.Value = AuxSerialSendAsString.Value; }
				SendCondition.CopyTo(other.SendCondition);
		    }
		}
		
		public sealed class Battery : SettingCategrory
		{
		    private PowerManagement parent; 
		
			/// <summary>
			/// Gets the Connection that this setting category is bound to or null if it is unbound.
			/// </summary>
			public override Connection Connection { get { return parent.Connection; } }
		
			/// <summary>
			/// Gets the category text including the full path from the root category.
			/// </summary>
			public override string Text { get { return "Battery"; } }
		
			/// <summary>
			/// Gets the category prefix text.
			/// </summary>
			protected override int CategoryPrefix { get { return 2; } }
		
			/// <summary>
			/// Gets the parent category of this category or null if this category is the root category.
			/// </summary>
			public override SettingCategrory Parent { get { return parent; } }
		
			/// <summary>
			/// Gets a flag to indicate if this category is hidden.
			/// </summary>
			public override bool IsHidden { get { return false; } }
		
			/// <summary>
			/// Gets Battery Minimum Voltage setting.
			/// </summary>
			public ISettingValue<float> BatteryMinimumVoltage { get; private set; }
			
			/// <summary>
			/// Gets Battery Capacity setting.
			/// </summary>
			public ISettingValue<float> BatteryCapacity { get; private set; }
			
			/// <summary>
			/// Gets Battery Health setting.
			/// </summary>
			public ISettingValue<float> BatteryHealth { get; private set; }
			
			/// <summary>
			/// Gets Battery Charge/Discharge Cycles setting.
			/// </summary>
			public ISettingValue<float> BatteryChargeDischargeCycles { get; private set; }
		
		    internal Battery(PowerManagement parent)
		    {
		        this.parent = parent; 
		
		    }
		
			internal override void AttachSettings(Settings settings)
			{
				BatteryMinimumVoltage = settings.BatteryMinimumVoltage;
				BatteryCapacity = settings.BatteryCapacity;
				BatteryHealth = settings.BatteryHealth;
				BatteryChargeDischargeCycles = settings.BatteryChargeDischargeCycles;
				
				Add(settings.BatteryMinimumVoltage);
				Add(settings.BatteryCapacity);
				Add(settings.BatteryHealth);
				Add(settings.BatteryChargeDischargeCycles);
				Finalise();
			}
		
			/// <summary>
			/// Copy the values of all the settings in this category to another
			/// </summary>
			/// <param name="other">Another category</param>
		    public void CopyTo(Battery other)
		    {
				if (BatteryMinimumVoltage.IsValueUndefined == false) { other.BatteryMinimumVoltage.Value = BatteryMinimumVoltage.Value; }
				if (BatteryCapacity.IsValueUndefined == false) { other.BatteryCapacity.Value = BatteryCapacity.Value; }
				if (BatteryHealth.IsValueUndefined == false) { other.BatteryHealth.Value = BatteryHealth.Value; }
				if (BatteryChargeDischargeCycles.IsValueUndefined == false) { other.BatteryChargeDischargeCycles.Value = BatteryChargeDischargeCycles.Value; }
		    }
		}
		
		public sealed class PowerManagement : SettingCategrory
		{
		    private Settings parent; 
		
			/// <summary>
			/// Gets the Connection that this setting category is bound to or null if it is unbound.
			/// </summary>
			public override Connection Connection { get { return parent.Connection; } }
		
			/// <summary>
			/// Gets the category text including the full path from the root category.
			/// </summary>
			public override string Text { get { return "Power Management"; } }
		
			/// <summary>
			/// Gets the category prefix text.
			/// </summary>
			protected override int CategoryPrefix { get { return 2; } }
		
			/// <summary>
			/// Gets the parent category of this category or null if this category is the root category.
			/// </summary>
			public override SettingCategrory Parent { get { return parent; } }
		
			/// <summary>
			/// Gets a flag to indicate if this category is hidden.
			/// </summary>
			public override bool IsHidden { get { return false; } }
		
			/// <summary>
			/// Gets CPU Idle Mode setting.
			/// </summary>
			public ISettingValue<CpuIdleMode> CpuIdleMode { get; private set; }
			
			/// <summary>
			/// Gets LEDs Enabled setting.
			/// </summary>
			public ISettingValue<bool> LedsEnabled { get; private set; }
			
			/// <summary>
			/// Gets External Current Limit setting.
			/// </summary>
			public ISettingValue<bool> ExternalCurrentLimit { get; private set; }
			
			/// <summary>
			/// Gets Sleep Timer setting.
			/// </summary>
			public ISettingValue<uint> SleepTimer { get; private set; }
			
			/// <summary>
			/// Gets Wakeup Timer setting.
			/// </summary>
			public ISettingValue<uint> WakeupTimer { get; private set; }
			
			/// <summary>
			/// Gets Motion Trigger Wakeup setting.
			/// </summary>
			public ISettingValue<bool> MotionTriggerWakeup { get; private set; }
			
			/// <summary>
			/// Gets External Power Wakeup setting.
			/// </summary>
			public ISettingValue<bool> ExternalPowerWakeup { get; private set; }
			
			/// <summary>
			/// Gets Muted On Startup setting.
			/// </summary>
			public ISettingValue<bool> MutedOnStartup { get; private set; }
			
			/// <summary>
			/// Gets Battery settings category.
			/// </summary>
			public SettingsCategoryTypes.Battery Battery { get; private set; }
		
		    internal PowerManagement(Settings parent)
		    {
		        this.parent = parent; 
				Battery = new SettingsCategoryTypes.Battery(this);
		    }
		
			internal override void AttachSettings(Settings settings)
			{
				CpuIdleMode = settings.CpuIdleMode;
				LedsEnabled = settings.LedsEnabled;
				ExternalCurrentLimit = settings.ExternalCurrentLimit;
				SleepTimer = settings.SleepTimer;
				WakeupTimer = settings.WakeupTimer;
				MotionTriggerWakeup = settings.MotionTriggerWakeup;
				ExternalPowerWakeup = settings.ExternalPowerWakeup;
				MutedOnStartup = settings.MutedOnStartup;
				Battery.AttachSettings(settings);
				
				Add(settings.CpuIdleMode);
				Add(settings.LedsEnabled);
				Add(settings.ExternalCurrentLimit);
				Add(settings.SleepTimer);
				Add(settings.WakeupTimer);
				Add(settings.MotionTriggerWakeup);
				Add(settings.ExternalPowerWakeup);
				Add(settings.MutedOnStartup);
				Add(Battery);
				Finalise();
			}
		
			/// <summary>
			/// Copy the values of all the settings in this category to another
			/// </summary>
			/// <param name="other">Another category</param>
		    public void CopyTo(PowerManagement other)
		    {
				if (CpuIdleMode.IsValueUndefined == false) { other.CpuIdleMode.Value = CpuIdleMode.Value; }
				if (LedsEnabled.IsValueUndefined == false) { other.LedsEnabled.Value = LedsEnabled.Value; }
				if (ExternalCurrentLimit.IsValueUndefined == false) { other.ExternalCurrentLimit.Value = ExternalCurrentLimit.Value; }
				if (SleepTimer.IsValueUndefined == false) { other.SleepTimer.Value = SleepTimer.Value; }
				if (WakeupTimer.IsValueUndefined == false) { other.WakeupTimer.Value = WakeupTimer.Value; }
				if (MotionTriggerWakeup.IsValueUndefined == false) { other.MotionTriggerWakeup.Value = MotionTriggerWakeup.Value; }
				if (ExternalPowerWakeup.IsValueUndefined == false) { other.ExternalPowerWakeup.Value = ExternalPowerWakeup.Value; }
				if (MutedOnStartup.IsValueUndefined == false) { other.MutedOnStartup.Value = MutedOnStartup.Value; }
				Battery.CopyTo(other.Battery);
		    }
		}         
    }
}
