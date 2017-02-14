using System;
using System.Windows.Forms;
using Rug.Cmd;

namespace NgimuSynchronisedNetworkManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ConsoleColorState state = RC.ColorState;

            try
            {
                RC.Sys = new NullConsole();
                RC.App = RC.Sys;

                //Options.Load();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            finally
            {
                //Options.Save();

                RC.ColorState = state;
            }
        }
    }
}
