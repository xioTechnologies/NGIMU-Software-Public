using System.Diagnostics;

namespace NgimuApi.Bootloader
{
    public class BootloaderHelper
    {
        /// <summary>
        /// Upload firmware to a specified serial port. The device bootloader must be active before calling this function. 
        /// </summary>
        /// <param name="hexFile">Full path to a hex file to upload.</param>
        /// <param name="portName">The name of the serial port.</param>
        /// <returns>True if the upload was successful.</returns>
        public bool UploadFirmware(string hexFile, string portName, int retryLimit = 3)
        {
            do
            {
                ProcessStartInfo processInfo = new ProcessStartInfo(Helper.ResolvePath("~/Bootloader/ds30LoaderConsole.exe"));
                processInfo.Arguments = "\"-f=" + hexFile + "\"" +
                                        " -d=PIC32MX470F512L " +
                                        "\"-k=" + portName + "\"" +
                                        " -r=115200 --writef --ht=1000 --polltime=100 --timeout=500 -o";

                processInfo.UseShellExecute = false;
                processInfo.RedirectStandardOutput = true;
                processInfo.CreateNoWindow = true;

                Process process = Process.Start(processInfo);

                process.WaitForExit();

                if (process.ExitCode == 0)
                {
                    return true;
                }
            }
            while (retryLimit-- > 0);

            return false; 
        }
    }
}
