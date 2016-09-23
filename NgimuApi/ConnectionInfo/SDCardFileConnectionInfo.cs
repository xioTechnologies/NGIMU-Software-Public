using System.IO;

namespace NgimuApi
{
    /// <summary>
    /// Connection info that describes a pseudo connection to read from a file. 
    /// </summary>
    public sealed class SDCardFileConnectionInfo : IConnectionInfo
    {
        /// <summary>
        /// Gets or sets the path to the file. 
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Does the file exist. 
        /// </summary>
        public bool FileExists { get { return File.Exists(FilePath); } }

        /// <summary>
        /// The ID to use as a unique key.
        /// </summary>
        /// <returns>A unique key string.</returns>
        public string ToIDString()
        {
            return "SD Card File: " + FilePath;
        }

        /// <summary>
        /// Creates an instance using the default values.
        /// </summary>
        public SDCardFileConnectionInfo()
        {
            FilePath = string.Empty;
        }
    }
}
