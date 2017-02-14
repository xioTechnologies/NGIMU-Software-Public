using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NgimuForms.DialogsAndWindows
{
    // http://stackoverflow.com/questions/6942150/why-folderbrowserdialog-dialog-does-not-scroll-to-selected-folder
    public static class FolderBrowserLauncher
    {
        /// <summary>
        /// Using title text to look for the top level dialog window is fragile.
        /// In particular, this will fail in non-English applications.
        /// </summary>
        private const string TopLevelSearchString = "Browse For Folder";

        /// <summary>
        /// These should be more robust.  We find the correct child controls in the dialog
        /// by using the GetDlgItem method, rather than the FindWindow(Ex) method,
        /// because the dialog item IDs should be constant.
        /// </summary>
        private const int DialogItemBrowseControl = 0;

        private const int DialogItemTreeView = 100;

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern IntPtr GetDlgItem(IntPtr hDlg, int nIDDlgItem);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Some of the callbacks that the Tree View control will respond to
        /// </summary>
        private const int TV_FIRST = 0x1100;
        private const int TVM_SELECTITEM = (TV_FIRST + 11);
        private const int TVM_GETNEXTITEM = (TV_FIRST + 10);
        private const int TVM_GETITEM = (TV_FIRST + 12);
        private const int TVM_ENSUREVISIBLE = (TV_FIRST + 20);

        /// <summary>
        /// Constants used to identity specific items in the Tree View control
        /// </summary>
        private const int TVGN_ROOT = 0x0;
        private const int TVGN_NEXT = 0x1;
        private const int TVGN_CHILD = 0x4;
        private const int TVGN_FIRSTVISIBLE = 0x5;
        private const int TVGN_NEXTVISIBLE = 0x6;
        private const int TVGN_CARET = 0x9;


        /// <summary>
        /// Calling this method is identical to calling the ShowDialog method of the provided
        /// FolderBrowserDialog, except that an attempt will be made to scroll the Tree View
        /// to make the currently selected folder visible in the dialog window.
        /// </summary>
        /// <param name="dlg"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static DialogResult ShowFolderBrowser(FolderBrowserDialog dlg, IWin32Window parent = null)
        {
            DialogResult result;
            int retries = 10;

            using (Timer timer = new Timer())
            {
                timer.Tick += (s, a) =>
                {
                    if (retries <= 0)
                    {
                        //
                        //  We failed to find the Tree View control.
                        //
                        //  As a fall back (and this is an UberUgly hack), we will send
                        //  some fake keystrokes to the application in an attempt to force
                        //  the Tree View to scroll to the selected item.
                        //
                        timer.Stop();

                        SendKeys.Send("{TAB}{TAB}{DOWN}{DOWN}{UP}{UP}");

                        return;
                    }

                    --retries;

                    IntPtr hwndDlg = FindWindow((string)null, TopLevelSearchString);
                    if (hwndDlg == IntPtr.Zero)
                    {
                        return;
                    }

                    IntPtr hwndFolderCtrl = GetDlgItem(hwndDlg, DialogItemBrowseControl);
                    if (hwndFolderCtrl == IntPtr.Zero)
                    {
                        return;
                    }

                    IntPtr hwndTV = GetDlgItem(hwndFolderCtrl, DialogItemTreeView);
                    if (hwndTV == IntPtr.Zero)
                    {
                        return;
                    }

                    IntPtr item = SendMessage(hwndTV, (uint)TVM_GETNEXTITEM, new IntPtr(TVGN_CARET), IntPtr.Zero);
                    if (item == IntPtr.Zero)
                    {
                        return;
                    }

                    SendMessage(hwndTV, TVM_ENSUREVISIBLE, IntPtr.Zero, item);

                    retries = 0;

                    timer.Stop();
                };

                timer.Interval = 10;
                timer.Start();

                result = dlg.ShowDialog(parent);
            }

            return result;
        }
    }
}
