namespace NgimuApi
{
    /// <summary>
    /// Firmware compatibility result.
    /// </summary>
    public enum FirmwareCompatibility
    {
        /// <summary>
        /// The detected firmware version is compatible.
        /// </summary>
        Compatible,

        /// <summary>
        /// The detected firmware version is incompatible.
        /// </summary>
        NotCompatible,

        /// <summary>
        /// The firmware version is unknown.
        /// </summary>
        Unknown,
    }

    partial class Settings
    {
        /// <summary>
        /// The expected firmware version.
        /// </summary>
        public const string ExpectedFirmwareVersion = "v1.5 (May 19 2017 15:24:30)";

        /// <summary>
        /// Checks the firmware compatibility.
        /// </summary>
        /// <returns>FirmwareCompatibility.</returns>
        public FirmwareCompatibility CheckFirmwareCompatibility()
        {
            if (FirmwareVersion.HasRemoteValue == false)
            {
                return FirmwareCompatibility.Unknown;
            }

            if (ExpectedFirmwareVersion.Equals(FirmwareVersion.Value) == false)
            {
                return FirmwareCompatibility.NotCompatible;
            }

            return FirmwareCompatibility.Compatible;
        }
    }
}