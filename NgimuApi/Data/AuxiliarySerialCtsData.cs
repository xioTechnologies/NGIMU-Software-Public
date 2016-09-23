namespace NgimuApi.Data
{
    /// <summary>
    /// Auxiliary serial CTS input data received from the device.
    /// </summary>
    public sealed class AuxiliarySerialCtsData : CtsDataBase
    {
        /// <summary>
        /// Gets the OSC address for this data object.
        /// </summary>
        public override string OscAddress { get { return "/auxserial/cts"; } }
    }
}
