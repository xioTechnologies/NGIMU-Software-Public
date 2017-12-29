using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NgimuApi;
using NgimuForms.Controls;
using NgimuForms.DialogsAndWindows;
using System.Linq; 

namespace NgimuSynchronisedNetworkManager.Controls
{
    public static class ConnectionGuiExtentions
    {
        public static void SendCommand(this Control control, ConnectionRow connection, bool errorsInRow, Command command, params object[] args)
        {
            control.SendCommand(new List<ConnectionRow> { connection }, errorsInRow, Commands.GetCommandMetaData(command), args);
        }

        public static void SendCommand(this Control control, List<ConnectionRow> connections, bool errorsInRow, Command command, params object[] args)
        {
            control.SendCommand(connections, errorsInRow, Commands.GetCommandMetaData(command), args);
        }

        public static void SendCommand(this Control control, List<ConnectionRow> connections, bool errorsInRow, CommandMetaData commandMetaData, params object[] args)
        {
            using (ProgressDialog dialog = new ProgressDialog()
            {
                ProgressMessage = $"Sending command to {connections.Count} devices.",
                Style = ProgressBarStyle.Marquee,
                CancelButtonEnabled = false,
            })
            {
                Thread thread = new Thread(() =>
                {
                    Parallel.ForEach(connections,
                        (connection) =>
                        {
                            string messageString = "";
                            CommunicationProcessResult result;

                            do
                            {
                                Reporter reporter = new Reporter();
                                CommandProcess process = new CommandProcess(connection.Connection, reporter, new CommandCallback(commandMetaData.GetMessage(args)), 100, 3);

                                result = process.Send();

                                StringBuilder fullMessageString = new StringBuilder();

                                fullMessageString.AppendLine("Error while communicating with " + connection.Connection.Settings.GetDeviceDescriptor() + ".");
                                fullMessageString.AppendLine();

                                fullMessageString.Append("Failed to confirm command: " + commandMetaData.OscAddress);

                                messageString = fullMessageString.ToString();

                                if (result != CommunicationProcessResult.Success && errorsInRow == true)
                                {
                                    connection.SetError(messageString); 
                                }
                            }
                            while (result != CommunicationProcessResult.Success &&
                                   errorsInRow == false && dialog.InvokeShowError(messageString, MessageBoxButtons.RetryCancel) == DialogResult.Retry);
                        });

                    control.Invoke(new MethodInvoker(dialog.Close));
                });

                thread.Start();

                dialog.ShowDialog(control);
            }
        }

        public static void SetSettingValueOnAllDevices<SettingValueType>(this Control control, IEnumerable<ISettingValue<SettingValueType>> settingItems, SettingValueType value)
        {
            Parallel.ForEach(settingItems, (settingItem) =>
            {
                settingItem.Value = value;
            });
        }

        public static void WriteSettings(this Control control, List<ConnectionRow> connectionRows, bool errorsInRow, IEnumerable<ISettingValue> settingValues)
        {
            using (ProgressDialog dialog = new ProgressDialog()
            {
                ProgressMessage = "Writing settings to all devices.",
                Style = ProgressBarStyle.Marquee,
                CancelButtonEnabled = false,
            })
            {
                Thread thread = new Thread(() =>
                {
                    Parallel.ForEach(settingValues,
                        (settingValue) =>
                        {
                            string messageString = "";
                            CommunicationProcessResult result;

                            ConnectionRow row = connectionRows.FirstOrDefault(connectionRow => connectionRow.Connection == settingValue.Category.Connection);
                            
                            do
                            {
                                result = settingValue.Write();

                                bool allValuesFailed;
                                bool allValuesSucceeded;

                                messageString = Settings.GetCommunicationFailureString(new ISettingValue[] { settingValue }, 20, out allValuesFailed, out allValuesSucceeded);

                                StringBuilder fullMessageString = new StringBuilder();

                                fullMessageString.AppendLine("Error while communicating with " + settingValue.Category.Connection.Settings.GetDeviceDescriptor() + ".");
                                fullMessageString.AppendLine();

                                fullMessageString.Append("Failed to confirm write of following setting:" + messageString);

                                messageString = fullMessageString.ToString();

                                if (result != CommunicationProcessResult.Success && errorsInRow == true)
                                {
                                    row?.SetError(messageString); 
                                }
                            }
                            while (result != CommunicationProcessResult.Success &&
                                   errorsInRow == false && 
                                   dialog.InvokeShowError(messageString, MessageBoxButtons.RetryCancel) == DialogResult.Retry);
                        });

                    control.Invoke(new MethodInvoker(dialog.Close));
                });

                thread.Start();

                dialog.ShowDialog(control);
            }
        }

        //public static void WriteSettingsUsingProgress(this Control control, ProgressDialog dialog, IEnumerable<ISettingValue> settingValues)
        //{
        //    Parallel.ForEach(settingValues,
        //        (settingValue) =>
        //        {
        //            string messageString = "";
        //            CommunicationProcessResult result;

        //            do
        //            {
        //                result = settingValue.Write();

        //                bool allValuesFailed;
        //                bool allValuesSucceeded;

        //                messageString = Settings.GetCommunicationFailureString(new ISettingValue[] { settingValue }, 20, out allValuesFailed, out allValuesSucceeded);

        //                StringBuilder fullMessageString = new StringBuilder();

        //                fullMessageString.AppendLine("Error while communicating with " + settingValue.Category.Connection.Settings.GetDeviceDescriptor() + ".");
        //                fullMessageString.AppendLine();

        //                fullMessageString.Append("Failed to confirm write of following setting:" + messageString);

        //                messageString = fullMessageString.ToString();
        //            }
        //            while (result != CommunicationProcessResult.Success &&
        //                   dialog.InvokeShowError(messageString, MessageBoxButtons.RetryCancel) == DialogResult.Retry);
        //        });
        //}


        public static void WriteSettings(this Control control, List<ConnectionRow> connectionRows, bool errorsInRow, IEnumerable<SettingCategrory> settingCategrories)
        {
            using (ProgressDialog dialog = new ProgressDialog()
            {
                ProgressMessage = "Writing settings to all devices.",
                Style = ProgressBarStyle.Marquee,
                CancelButtonEnabled = false,
            })
            {
                Thread thread = new Thread(() =>
                {
                    control.WriteSettingsWithExistingProgress(dialog, connectionRows, errorsInRow, settingCategrories);

                    control.Invoke(new MethodInvoker(dialog.Close));
                });

                thread.Start();

                dialog.ShowDialog(control);
            }
        }

        public static void WriteSettingsWithExistingProgress(this Control control, ProgressDialog dialog, List<ConnectionRow> connectionRows, bool errorsInRow, IEnumerable<SettingCategrory> settingCategrories)
        {
            Parallel.ForEach(settingCategrories,
                (settingCategrory) =>
                {
                    string messageString = "";
                    CommunicationProcessResult result;

                    ConnectionRow row = connectionRows.FirstOrDefault(connectionRow => connectionRow.Connection == settingCategrory.Connection);

                    do
                    {
                        result = settingCategrory.Write();

                        bool allValuesFailed;
                        bool allValuesSucceeded;

                        messageString = Settings.GetCommunicationFailureString(settingCategrory.Values, 20, out allValuesFailed, out allValuesSucceeded);

                        StringBuilder fullMessageString = new StringBuilder();

                        fullMessageString.AppendLine("Error while communicating with " + settingCategrory.Connection.Settings.GetDeviceDescriptor() + ".");
                        fullMessageString.AppendLine();

                        fullMessageString.Append("Failed to confirm write of following settings:" + messageString);

                        messageString = fullMessageString.ToString();

                        if (result != CommunicationProcessResult.Success && errorsInRow == true)
                        {
                            row?.SetError(messageString);
                        }
                    }
                    while (result != CommunicationProcessResult.Success &&
                           errorsInRow == false && 
                           dialog.InvokeShowError(messageString, MessageBoxButtons.RetryCancel) == DialogResult.Retry);
                });
        }

        public static void ReadSettings(this Control control, List<ConnectionRow> connectionRows, bool errorsInRow, IEnumerable<ISettingValue> settingValues)
        {
            using (ProgressDialog dialog = new ProgressDialog()
            {
                ProgressMessage = "Reading settings from all devices.",
                Style = ProgressBarStyle.Marquee,
                CancelButtonEnabled = false,
            })
            {
                Thread thread = new Thread(() =>
                {
                    Parallel.ForEach(settingValues,
                        (settingValue) =>
                        {
                            string messageString = "";
                            CommunicationProcessResult result;

                            ConnectionRow row = connectionRows.FirstOrDefault(connectionRow => connectionRow.Connection == settingValue.Category.Connection);

                            do
                            {
                                result = settingValue.Read();

                                bool allValuesFailed;
                                bool allValuesSucceeded;

                                messageString = Settings.GetCommunicationFailureString(new ISettingValue[] { settingValue }, 20, out allValuesFailed, out allValuesSucceeded);

                                StringBuilder fullMessageString = new StringBuilder();

                                fullMessageString.AppendLine("Error while communicating with " + settingValue.Category.Connection.Settings.GetDeviceDescriptor() + ".");
                                fullMessageString.AppendLine();

                                fullMessageString.Append("Failed to read the following setting:" + messageString);

                                messageString = fullMessageString.ToString();

                                if (result != CommunicationProcessResult.Success && errorsInRow == true)
                                {
                                    row?.SetError(messageString);
                                }
                            }
                            while (result != CommunicationProcessResult.Success &&
                                   errorsInRow == false && 
                                   dialog.InvokeShowError(messageString, MessageBoxButtons.RetryCancel) == DialogResult.Retry);
                        });

                    control.Invoke(new MethodInvoker(dialog.Close));
                });

                thread.Start();

                dialog.ShowDialog(control);
            }
        }

        public static void ReadSettings(this Control control, List<ConnectionRow> connectionRows, bool errorsInRow, IEnumerable<SettingCategrory> settingCategrories)
        {
            using (ProgressDialog dialog = new ProgressDialog()
            {
                ProgressMessage = "Reading settings from all devices.",
                Style = ProgressBarStyle.Marquee,
                CancelButtonEnabled = false,
            })
            {
                Thread thread = new Thread(() =>
                {
                    Parallel.ForEach(settingCategrories,
                        (settingCategrory) =>
                        {
                            string messageString = "";
                            CommunicationProcessResult result;

                            ConnectionRow row = connectionRows.FirstOrDefault(connectionRow => connectionRow.Connection == settingCategrory.Connection);

                            do
                            {
                                result = settingCategrory.Read();

                                bool allValuesFailed;
                                bool allValuesSucceeded;

                                messageString = Settings.GetCommunicationFailureString(settingCategrory.Values, 20, out allValuesFailed, out allValuesSucceeded);

                                StringBuilder fullMessageString = new StringBuilder();

                                fullMessageString.AppendLine("Error while communicating with " + settingCategrory.Connection.Settings.GetDeviceDescriptor() + ".");
                                fullMessageString.AppendLine();

                                fullMessageString.Append("Failed to read the following settings:" + messageString);

                                messageString = fullMessageString.ToString();

                                if (result != CommunicationProcessResult.Success && errorsInRow == true)
                                {
                                    row?.SetError(messageString);
                                }
                            }
                            while (result != CommunicationProcessResult.Success &&
                                   errorsInRow == false && 
                                   dialog.InvokeShowError(messageString, MessageBoxButtons.RetryCancel) == DialogResult.Retry);
                        });

                    control.Invoke(new MethodInvoker(dialog.Close));
                });

                thread.Start();

                dialog.ShowDialog(control);
            }
        }
    }
}
