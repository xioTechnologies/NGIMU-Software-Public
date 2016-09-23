using System.IO;

namespace NgimuApi
{
    public static partial class Helper
    {
        #region Path Helpers

        public static string ApplicationRootPath { get { return new FileInfo(System.Windows.Forms.Application.ExecutablePath).DirectoryName + Path.DirectorySeparatorChar; } }

        public static string ResolvePath(string path)
        {
            string p = path;

            p = p.Replace("~/", ApplicationRootPath);

            p = p.Replace('/', Path.DirectorySeparatorChar);

            return p;
        }

        public static string UnResolvePath(string path)
        {
            string p = path;

            p = p.Replace(ApplicationRootPath, "~/");

            p = p.Replace(Path.DirectorySeparatorChar, '/');

            return p;
        }

        public static void EnsurePathExists(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            if (fileInfo.Directory.Exists == false)
            {
                fileInfo.Directory.Create();
            }
        }

        public static string CleanFileName(string filename)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                filename = filename.Replace(c, '_');
            }

            return filename;
        }

        #endregion

        internal static void MoveDirectory(string directory, string resolvedDirectory, bool deleteOriginal)
        {
            if (directory.Equals(resolvedDirectory) == false)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directory);
                DirectoryInfo resolvedDirectoryInfo = new DirectoryInfo(resolvedDirectory);

                if (resolvedDirectoryInfo.Exists == false)
                {
                    resolvedDirectoryInfo.Create();
                }

                foreach (FileInfo source in directoryInfo.GetFiles())
                {
                    FileInfo destination = new FileInfo(Path.Combine(resolvedDirectoryInfo.FullName, source.Name));

                    if (destination.Exists == true)
                    {
                        destination.Delete();
                    }

                    source.MoveTo(destination.FullName);
                }

                if (deleteOriginal == true)
                {
                    directoryInfo.Delete(true);
                }
            }
        }
    }
}
