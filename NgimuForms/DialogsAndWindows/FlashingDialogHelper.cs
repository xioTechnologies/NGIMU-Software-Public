using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NgimuForms.DialogsAndWindows
{
    public class FlashingDialogHelper
    {
        #region Flashing

        // To support flashing.
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        private enum FlashWindowFlags : uint
        {
            // stop flashing
            FLASHW_STOP = 0,

            // flash the window title
            FLASHW_CAPTION = 1,

            // flash the taskbar button
            FLASHW_TRAY = 2,

            // 1 | 2
            FLASHW_ALL = 3,

            // flash continuously
            FLASHW_TIMER = 4,

            // flash until the window comes to the foreground
            FLASHW_TIMERNOFG = 12
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            public uint cbSize;
            public IntPtr hwnd;
            public uint dwFlags;
            public uint uCount;
            public uint dwTimeout;
        }

        // Do the flashing - this does not involve a raincoat.
        public static bool FlashWindowEx(Form form)
        {
            IntPtr hWnd = form.Handle;
            FLASHWINFO fInfo = new FLASHWINFO();

            fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
            fInfo.hwnd = hWnd;
            fInfo.dwFlags = (uint)FlashWindowFlags.FLASHW_ALL | (uint)FlashWindowFlags.FLASHW_TIMERNOFG;
            fInfo.uCount = uint.MaxValue;
            fInfo.dwTimeout = 0;

            return FlashWindowEx(ref fInfo);
        }

        #endregion
    }
}
