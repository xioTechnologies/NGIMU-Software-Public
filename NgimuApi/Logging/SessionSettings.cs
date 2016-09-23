using System.IO;

namespace NgimuApi.Logging
{
    /// <summary>
    /// Logging settings are used to configure ImuLogger instances. 
    /// </summary>
    public sealed class SessionSettings
    {
        /// <summary>
        /// Gets or sets the root directory path. 
        /// </summary>
        public string RootDirectory { get; set; }

        /// <summary>
        /// Gets or sets the session directory name. This maybe the original file name if this logging session is from a file.
        /// </summary>
        public string SessionName { get; set; }

        /// <summary>
        /// Gets or sets the desired logging period in seconds. 
        /// </summary>
        public uint LoggingPeriod { get; set; }

        /// <summary>
        /// Creates an instance using the default values.
        /// </summary>
        public SessionSettings()
        {
            RootDirectory = string.Empty;
            SessionName = "Unnamed Session";
            LoggingPeriod = 0;
        }

        public SessionSettings Clone()
        {
            return new SessionSettings()
            {
                RootDirectory = this.RootDirectory,
                SessionName = this.SessionName,
                LoggingPeriod = this.LoggingPeriod,
            };
        }

        /// <summary>
        /// Helper method to create logger settings for the contents of a file. 
        /// </summary>
        /// <param name="file">The file that contains OSC messages in SLIP format.</param>
        /// <returns>Logger settings that will read from a file.</returns>
        public static SessionSettings CreateForFile(FileInfo file)
        {
            return new SessionSettings()
            {
                RootDirectory = file.Directory.FullName,
                SessionName = file.Name.Substring(0, file.Name.Length - file.Extension.Length),
            };
        }

        /// <summary>
        /// Helper method to create logger settings for the contents of a file. 
        /// </summary>
        /// <param name="rootDirectory">Destination root directory.</param>
        /// <param name="file">The file that contains OSC messages in SLIP format.</param>
        /// <returns>Logger settings that will read from a file.</returns>
        public static SessionSettings CreateForFileAndPath(string rootDirectory, FileInfo file)
        {
            return new SessionSettings()
            {
                RootDirectory = rootDirectory,
                SessionName = file.Name.Substring(0, file.Name.Length - file.Extension.Length),
            };
        }
    }
}
