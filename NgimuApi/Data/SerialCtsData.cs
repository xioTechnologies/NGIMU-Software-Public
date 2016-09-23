namespace NgimuApi.Data
{
    /// <summary>
    /// Serial CTS input data received from the device.
    /// </summary>
    public sealed class SerialCtsData : CtsDataBase
    {
        /// <summary>
        /// Gets the OSC address for this data object.
        /// </summary>
        public override string OscAddress { get { return "/serial/cts"; } }
    }
}
