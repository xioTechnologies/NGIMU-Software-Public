using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Rug.Cmd;

namespace CreateDistribution
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SetupConsole();

            List<Regex> includeRegexes = new List<Regex>();

            List<string> explicitRemoves = new List<string>() { ".vshost.exe", ".vshost.exe.config" };

            string source, destination;
            int index = 0;

            // parse Source path argument
            if (TryGetPathArgument(ref index, args, "Source path", out source, true) == false)
            {
                return;
            }

            // parse Destination path argument
            if (TryGetPathArgument(ref index, args, "Destination path", out destination, false) == false)
            {
                return;
            }

            RC.WriteLine("Creating distribution for: " + source);

            // parse Remaining arguments
            for (; index < args.Length; index++)
            {
                string arg = args[index];

                string[] parts = arg.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string part in parts)
                {
                    string regexString = ConvertToRegexString(part);

                    try
                    {
                        Regex regex = new Regex(regexString, RegexOptions.IgnoreCase);

                        includeRegexes.Add(regex);

                        RC.WriteLine("Keeping: " + part + ", \"" + regexString + "\"");
                    }
                    catch (Exception ex)
                    {
                        RC.WriteException(03, "Exception parsing regex \"" + regexString + "\"", ex);

                        return;
                    }
                }
            }

            try
            {
                DirectoryInfo sourceDirectoryInfo = new DirectoryInfo(source);

                EnsurePathExists(destination);

                DirectoryInfo destinationDirectoryInfo = new DirectoryInfo(destination);

                if (destinationDirectoryInfo.Exists == true)
                {
                    destinationDirectoryInfo.Delete(true);
                }

                DirectoryCopy(sourceDirectoryInfo.FullName, destinationDirectoryInfo.FullName);

                foreach (DirectoryInfo directory in destinationDirectoryInfo.GetDirectories())
                {
                    if (IsMatch(directory.Name, includeRegexes) == false)
                    {
                        directory.Delete(true);

                        RC.WriteLine("Deleted: " + directory.FullName);

                        continue;
                    }
                }

                foreach (FileInfo file in destinationDirectoryInfo.GetFiles())
                {
                    if (IsMatch(file.Name, includeRegexes) == false ||
                        EndsWithAny(file.Name, explicitRemoves) == true)
                    {
                        file.Delete();

                        RC.WriteLine("Deleted: " + file.FullName);

                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                RC.WriteException(03, "Exception while composing distribution", ex);

                return;
            }
        }

        private static string ConvertToRegexString(string part)
        {
            string resolvedRegex = part;

            part = part.Replace("*", @"[\w\s.]+");

            return "^" + part + "$";
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);

                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            foreach (DirectoryInfo subdir in dirs)
            {
                // don't get too recursive 
                if (subdir.FullName == destDirName)
                {
                    continue;
                }

                string newSubFolderPath = Path.Combine(destDirName, subdir.Name);

                DirectoryCopy(subdir.FullName, newSubFolderPath);
            }
        }

        private static bool EndsWithAny(string name, List<string> explicitRemoves)
        {
            foreach (string explicitRemove in explicitRemoves)
            {
                if (name.EndsWith(explicitRemove, StringComparison.InvariantCultureIgnoreCase) == true)
                {
                    return true;
                }
            }

            return false;
        }

        private static void EnsurePathExists(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            if (fileInfo.Directory.Exists == false)
            {
                fileInfo.Directory.Create();
            }
        }

        private static bool IsMatch(string name, List<Regex> includeRegexes)
        {
            foreach (Regex regex in includeRegexes)
            {
                if (regex.IsMatch(name) == true)
                {
                    return true;
                }
            }

            return false;
        }

        private static void SetupConsole()
        {
            RC.App = ConsoleExt.SystemConsole;
            RC.Sys = RC.App;

            RC.IsBuildMode = true;
        }

        private static bool TryGetPathArgument(ref int index, string[] args, string argumentName, out string path, bool ensureExists)
        {
            path = string.Empty;

            if (index >= args.Length)
            {
                RC.WriteError(01, "Missing " + argumentName.ToLower() + " argument");

                return false;
            }

            path = args[index++];

            if (ensureExists == false)
            {
                return true;
            }

            if (Directory.Exists(path) == false)
            {
                RC.WriteError(02, argumentName + " does not exist. " + path);

                return false;
            }

            return true;
        }
    }
}