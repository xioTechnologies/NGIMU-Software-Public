using System;
using NgimuApi;
using Rug.Cmd;
using Rug.Osc;

namespace NgimuGui
{
    public static class GuiTerminal
    {
        private static object m_Lock = new object();

        public static bool ShowFullErrors { get; set; }

        public static ConsoleThemeColor InfoColor { get; set; }
        public static ConsoleThemeColor InfoDetailColor { get; set; }

        public static ConsoleThemeColor ErrorColor { get; set; }
        public static ConsoleThemeColor ErrorDetailColor { get; set; }

        public static ConsoleThemeColor TransmitColor { get; set; }
        public static ConsoleThemeColor ReceiveColor { get; set; }
        public static ConsoleThemeColor TimeTagColor { get; set; }
        public static ConsoleThemeColor MessageColor { get; set; }

        static GuiTerminal()
        {
            ShowFullErrors = false;
            RC.Verbosity = ConsoleVerbosity.Normal;

            InfoColor = ConsoleThemeColor.SubTextGood;
            InfoDetailColor = ConsoleThemeColor.SubText;

            ErrorColor = ConsoleThemeColor.TextBad;
            ErrorDetailColor = ConsoleThemeColor.SubTextBad;

            TransmitColor = ConsoleThemeColor.SubTextGood;
            ReceiveColor = ConsoleThemeColor.SubTextBad;
            TimeTagColor = ConsoleThemeColor.SubText;
            MessageColor = ConsoleThemeColor.Text;
        }

        public static void WriteInfo(string message)
        {
            lock (m_Lock)
            {
                RC.WriteLine(ConsoleVerbosity.Normal, InfoColor, message);
            }
        }

        public static void WriteInfo(string message, string detail)
        {
            lock (m_Lock)
            {
                if (ShowFullErrors == true &&
                    String.IsNullOrEmpty(detail) == false)
                {
                    RC.WriteLine(ConsoleVerbosity.Normal, InfoColor, message);
                    //RC.WriteLine(ConsoleVerbosity.Normal, TimeTagColor, " (" + detail + ")");
                    RC.WriteLine(ConsoleVerbosity.Normal, InfoColor, detail);
                }
                else
                {
                    RC.WriteLine(ConsoleVerbosity.Normal, InfoColor, message);
                }
            }
        }

        public static void WriteError(string message)
        {
            lock (m_Lock)
            {
                RC.WriteLine(ConsoleVerbosity.Normal, ErrorColor, message);
            }
        }

        internal static void WriteError(string message, string detail)
        {
            lock (m_Lock)
            {
                if (ShowFullErrors == true &&
                    String.IsNullOrEmpty(detail) == false)
                {
                    RC.WriteLine(ConsoleVerbosity.Normal, ErrorColor, message);
                    //RC.WriteLine(ConsoleVerbosity.Normal, ErrorDetailColor, " (" + detail + ")");
                    RC.WriteLine(ConsoleVerbosity.Normal, ErrorColor, detail);
                }
                else
                {
                    RC.WriteLine(ConsoleVerbosity.Normal, ErrorColor, message);
                }
            }
        }

        public static void WriteException(Exception ex)
        {
            if (ShowFullErrors == true)
            {
                lock (m_Lock)
                {
                    RC.WriteException(01, ex);
                }
            }
            else
            {
                WriteError(ex.Message);
            }
        }

        public static void WriteException(string message, Exception ex)
        {
            lock (m_Lock)
            {
                WriteError(message);

                if (ShowFullErrors == true)
                {
                    RC.WriteException(01, ex);
                }
            }
        }

        public static void WriteMessage(MessageDirection dir, OscTimeTag? timeTag, string message)
        {
            WriteMessage(dir, timeTag, MessageColor, message);
        }

        public static void WriteMessage(MessageDirection dir, OscTimeTag? timeTag, ConsoleThemeColor messageColor, string message)
        {
            lock (m_Lock)
            {
                switch (dir)
                {
                    case MessageDirection.Transmit:
                        RC.Write(ConsoleVerbosity.Normal, TransmitColor, "TX ");
                        break;
                    case MessageDirection.Receive:
                        RC.Write(ConsoleVerbosity.Normal, ReceiveColor, "RX ");
                        break;
                    default:
                        break;
                }

                if (timeTag != null && timeTag.Value.Value > 0)
                {
                    RC.Write(ConsoleVerbosity.Normal, TimeTagColor, NgimuApi.Helper.TimeTagToString(timeTag.Value) + " ");
                }

                RC.WriteLine(ConsoleVerbosity.Normal, messageColor, message);
            }
        }

        public static void WriteMessage(MessageDirection dir, OscTimeTag? timeTag, ConsoleColorExt messageColor, string message)
        {
            lock (m_Lock)
            {
                switch (dir)
                {
                    case MessageDirection.Transmit:
                        RC.Write(ConsoleVerbosity.Normal, TransmitColor, "TX ");
                        break;
                    case MessageDirection.Receive:
                        RC.Write(ConsoleVerbosity.Normal, ReceiveColor, "RX ");
                        break;
                    default:
                        break;
                }

                if (timeTag != null && timeTag.Value.Value > 0)
                {
                    RC.Write(ConsoleVerbosity.Normal, TimeTagColor, NgimuApi.Helper.TimeTagToString(timeTag.Value) + " ");
                }

                RC.WriteLine(ConsoleVerbosity.Normal, messageColor, message);
            }
        }

        public static void WriteMessage(IConsole console, MessageDirection dir, string message)
        {
            lock (m_Lock)
            {
                switch (dir)
                {
                    case MessageDirection.Transmit:
                        console.Write(ConsoleVerbosity.Normal, ConsoleColorExt.Green, "TX ");
                        break;
                    case MessageDirection.Receive:
                        console.Write(ConsoleVerbosity.Normal, ConsoleColorExt.Red, "RX ");
                        break;
                    default:
                        break;
                }

                console.WriteLine(ConsoleVerbosity.Normal, MessageColor, message);
            }
        }


        public static void WriteMessage(IConsole console, MessageDirection dir, ConsoleThemeColor color, string message)
        {
            lock (m_Lock)
            {
                switch (dir)
                {
                    case MessageDirection.Transmit:
                        console.Write(ConsoleVerbosity.Normal, ConsoleColorExt.Green, "TX ");
                        break;
                    case MessageDirection.Receive:
                        console.Write(ConsoleVerbosity.Normal, ConsoleColorExt.Red, "RX ");
                        break;
                    default:
                        break;
                }

                console.WriteLine(ConsoleVerbosity.Normal, color, message);
            }
        }


        public static void WriteMessage(IConsole console, MessageDirection dir, ConsoleColorExt color, string message)
        {
            lock (m_Lock)
            {
                switch (dir)
                {
                    case MessageDirection.Transmit:
                        console.Write(ConsoleVerbosity.Normal, ConsoleColorExt.Green, "TX ");
                        break;
                    case MessageDirection.Receive:
                        console.Write(ConsoleVerbosity.Normal, ConsoleColorExt.Red, "RX ");
                        break;
                    default:
                        break;
                }

                console.WriteLine(ConsoleVerbosity.Normal, color, message);
            }
        }
    }
}
