using System.Windows.Forms;

namespace NgimuGui
{
    public static class ControlExtensions
    {
        public static void InvokeShowError(this Control control, string message, MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            if (control.IsValidForInvoke() == true)
            {
                try
                {
                    control.Invoke((MethodInvoker)(() =>
                    {
                        try
                        {
                            control.ShowError(message, buttons);
                        }
                        catch
                        {
                            if (control.IsValidForInvoke() == false)
                            {
                                return;
                            }

                            throw;
                        }
                    }));
                }
                catch
                {
                    if (control.IsValidForInvoke() == false)
                    {
                        return;
                    }

                    throw;
                }
            }
        }

        public static void InvokeShowWarning(this Control control, string message, MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            if (control.IsValidForInvoke() == true)
            {
                try
                {
                    control.Invoke((MethodInvoker)(() =>
                    {
                        try
                        {
                            control.ShowWarning(message, buttons);
                        }
                        catch
                        {
                            if (control.IsValidForInvoke() == false)
                            {
                                return;
                            }

                            throw;
                        }
                    }));
                }
                catch
                {
                    if (control.IsValidForInvoke() == false)
                    {
                        return;
                    }

                    throw;
                }
            }
        }

        public static bool IsValidForInvoke(this Control control)
        {
            return control.IsHandleCreated == true && control.Disposing == false && control.IsDisposed == false;
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
    }
}