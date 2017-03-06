using System;
using Rug.Osc;

namespace NgimuApi
{
    public enum SettingValueType
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

        OscPassthroughMode,

        CpuIdleMode,

        UdpPort,
        UInt32,
        Float,
        Int32,
    }

    public interface ISettingItem
    {
        bool IsHidden { get; }

        CommunicationProcessResult Read(int timeout = 100, int retryLimit = 3);
        CommunicationProcessResult Read(IReporter reporter, int timeout = 100, int retryLimit = 3);

        void ReadAync(int timeout = 100, int retryLimit = 3);
        void ReadAync(IReporter reporter, int timeout = 100, int retryLimit = 3);

        CommunicationProcessResult Write(int timeout = 100, int retryLimit = 3);
        CommunicationProcessResult Write(IReporter reporter, int timeout = 100, int retryLimit = 3);

        void WriteAync(int timeout = 100, int retryLimit = 3);
        void WriteAync(IReporter reporter, int timeout = 100, int retryLimit = 3);
    }

    public interface ISettingValue : ISettingItem
    {
        /// <summary>
        /// Gets the name of the setting.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the .NET type of the value. 
        /// </summary>
        Type ValueType { get; }

        /// <summary>
        /// Gets the IMU value type. 
        /// </summary>
        SettingValueType SettingValueType { get; }

        /// <summary>
        /// Is this variable read-only. 
        /// </summary>
        bool IsReadOnly { get; }

        /// <summary>
        /// Gets the category that this setting value belongs to. 
        /// </summary>
        SettingCategrory Category { get; }

        /// <summary>
        /// Gets the description of this variable.
        /// </summary>
        string Documentation { get; }

        /// <summary>
        /// Has this instance received a value from the remote source. 
        /// </summary>
        bool HasRemoteValue { get; }

        /// <summary>
        /// Gets or sets the result from the most recent communication.
        /// </summary>
        /// <value>The communication result.</value>
        CommunicationProcessResult? CommunicationResult { get; set; }

        /// <summary>
        /// Is there a valid value to write to the device.
        /// </summary>
        bool IsValueUndefined { get; }

        /// <summary>
        /// Reset the state of the HasRemoteValue flag. 
        /// </summary>				
        void ResetRemoteValueState();

        /// <summary>
        /// Set the local value. 
        /// </summary>
        /// <param name="value">The local value.</param>
        void SetValue(object value);

        /// <summary>
        /// Get the local value. 
        /// </summary>
        /// <returns>The current local value.</returns>
        object GetValue();

        /// <summary>
        /// Set the remote value. 
        /// </summary>
        /// <param name="value">The remote value.</param>
        void SetRemoteValue(object value);

        /// <summary>
        /// Get the remote value. 
        /// </summary>
        /// <returns>The current remote value.</returns>
        object GetRemoteValue();

        /// <summary>
        /// Gets OSC address of the callback. 
        /// </summary>
        string OscAddress { get; }

        /// <summary>
        /// Gets OSC callback to be sent. 
        /// </summary>
        OscMessage Message { get; }

        /// <summary>
        /// True if the callback has completed.
        /// </summary>
        bool HasCallbackCompleted { get; }

        /// <summary>
        /// Reset the callback to non-completed. 
        /// </summary>
        void ResetCallbackState();

        /// <summary>
        /// Call this method with the returning callback. 
        /// </summary>
        /// <param name="message">An OSC callback.</param>
        void OnMessageReceived(Rug.Osc.OscMessage message);

        /// <summary>
        /// Call this method with a local value. 
        /// </summary>
        /// <param name="message">An OSC callback.</param>
        void OnLocalMessage(Rug.Osc.OscMessage message);
    }

    public interface ISettingValue<T> : ISettingValue
    {
        T RemoteValue { get; }

        T Value { get; set; }
    }
}
