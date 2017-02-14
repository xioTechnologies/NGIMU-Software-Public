using System.Reflection;
using System.Text;
using System.Windows.Forms;
using NgimuApi;

namespace NgimuForms.Controls
{
    public delegate DialogResult DialogResultMethodInvoker();

    public static class ControlExtensions
    {
        public static DialogResult InvokeShowError(this Control control, string message, MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            if (control.IsValidForInvoke() == true)
            {
                try
                {
                    return (DialogResult)control.Invoke((DialogResultMethodInvoker)(() =>
                    {
                        try
                        {
                            return control.ShowError(message, buttons);
                        }
                        catch
                        {
                            if (control.IsValidForInvoke() == false)
                            {
                                return DialogResult.None;
                            }

                            throw;
                        }
                    }));
                }
                catch
                {
                    if (control.IsValidForInvoke() == false)
                    {
                        return DialogResult.None;
                    }

                    throw;
                }
            }

            return DialogResult.None;
        }

        public static DialogResult InvokeShowWarning(this Control control, string message, MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            if (control.IsValidForInvoke() == true)
            {
                try
                {
                    return (DialogResult)control.Invoke((DialogResultMethodInvoker)(() =>
                    {
                        try
                        {
                            return control.ShowWarning(message, buttons);
                        }
                        catch
                        {
                            if (control.IsValidForInvoke() == false)
                            {
                                return DialogResult.None;
                            }

                            throw;
                        }
                    }));
                }
                catch
                {
                    if (control.IsValidForInvoke() == false)
                    {
                        return DialogResult.None;
                    }

                    throw;
                }
            }

            return DialogResult.None;
        }

        public static DialogResult ShowError(this Control control, string message, MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            return MessageBox.Show(control, message, "Error", buttons, MessageBoxIcon.Error);
        }

        public static DialogResult ShowQuestion(this Control control, string message, MessageBoxButtons buttons = MessageBoxButtons.YesNo)
        {
            return MessageBox.Show(control, message, "Question", buttons, MessageBoxIcon.Question);
        }

        public static DialogResult ShowWarning(this Control control, string message, MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            return MessageBox.Show(control, message, "Warning", buttons, MessageBoxIcon.Warning);
        }

        private static bool IsValidForInvoke(this Control control)
        {
            return control.IsHandleCreated == true && control.Disposing == false && control.IsDisposed == false;
        }

        public static void ShowIncompatableFirmwareWarning(this Control control, Settings settings)
        {
            if (settings.CheckFirmwareCompatibility() == FirmwareCompatibility.NotCompatible)
            {
                StringBuilder dialogString = new StringBuilder();

                Assembly assembly = Assembly.GetEntryAssembly();

                dialogString.AppendLine(string.Format("The detected firmware version on {0} may not be compatible with this version of the software.", settings.GetDeviceDescriptor()));
                dialogString.AppendLine("");
                dialogString.AppendLine("Please use the latest software and firmware versions available on-line.");
                dialogString.AppendLine("");
                dialogString.AppendLine("Detected firmware version: " + settings.FirmwareVersion.Value);
                dialogString.AppendLine("Expected firmware version: " + Settings.ExpectedFirmwareVersion);
                dialogString.AppendLine("Software version: v" + assembly.GetName().Version.Major + "." + assembly.GetName().Version.Minor);

                control.InvokeShowWarning(dialogString.ToString().TrimEnd());
            }
        }
    }
}