using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NgimuGui.DialogsAndWindows;

namespace NgimuSDCardFileConverter
{
    internal static class Program
    {
        public static string DestinationFolder = null;
        public static string[] SDCardFilePaths = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(params string[] args)
        {
            List<string> sdCardFilePaths = new List<string>();

            bool expectDestinationFolder = false;

            foreach (string arg in args)
            {
                if ("-d".Equals(arg, StringComparison.InvariantCultureIgnoreCase) ||
                    "-destination".Equals(arg, StringComparison.InvariantCultureIgnoreCase))
                {
                    expectDestinationFolder = true;
                    continue;
                }

                if (expectDestinationFolder == true)
                {
                    DestinationFolder = arg;
                    expectDestinationFolder = false;
                    continue;
                }

                sdCardFilePaths.Add(arg);
            }

            SDCardFilePaths = sdCardFilePaths.ToArray();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SDCardFileConverterWindow { DestinationFolder = DestinationFolder, SDCardFilePaths = SDCardFilePaths });
        }
    }
}