
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace NgimuApi
{
    public static partial class Helper
    {
        #region String Helpers

        public static bool IsNullOrEmpty(string str)
        {
            if (str == null)
            {
                return true;
            }

            if (str.Trim().Length == 0)
            {
                return true;
            }

            return false;
        }

        public static bool IsNotNullOrEmpty(string str)
        {
            if (str == null)
            {
                return false;
            }

            if (str.Trim().Length == 0)
            {
                return false;
            }

            return true;
        }

        private static Regex AlphaNumericFilter = new Regex("[^A-Z0-9]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static string ToLowerCamelCase(string original)
        {
            string str = AlphaNumericFilter.Replace(original, " ");

            //str = str.Replace("   ", " ").Replace("  ", " ");

            StringBuilder sb = new StringBuilder(str.Length);
            bool first = true;

            foreach (string part in str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (first == true)
                {
                    first = false;
                    sb.Append(part.ToLowerInvariant());
                }
                else
                {
                    sb.Append(char.ToUpperInvariant(part[0]));
                    sb.Append(part.Substring(1));
                }
            }

            return sb.ToString();
        }


        public static bool IsInvalidFileName(string filename)
        {
            return filename.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 0;
        }

        #endregion
    }
}
