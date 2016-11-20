using System.IO;

namespace SettingsObjectModelCodeGenerator
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

        #endregion
    }
}
