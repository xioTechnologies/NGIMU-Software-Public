using System;
using NgimuApi;
using Rug.Cmd;
using Rug.Osc;

namespace NgimuForms
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
            WriteInfo(RC.App, message);
        }

        public static void WriteInfo(IConsole console, string message)
        {
            lock (m_Lock)
            {
                console.WriteLine(ConsoleVerbosity.Normal, InfoColor, message);
            }
        }

        public static void WriteInfo(string message, string detail)
        {
            WriteInfo(RC.App, message, detail);
        }

        public static void WriteInfo(IConsole console, string message, string detail)
        {
            lock (m_Lock)
            {
                if (ShowFullErrors == true &&
                    String.IsNullOrEmpty(detail) == false)
                {
                    console.WriteLine(ConsoleVerbosity.Normal, InfoColor, message);
                    //RC.WriteLine(ConsoleVerbosity.Normal, TimeTagColor, " (" + detail + ")");
                    console.WriteLine(ConsoleVerbosity.Normal, InfoColor, detail);
                }
                else
                {
                    console.WriteLine(ConsoleVerbosity.Normal, InfoColor, message);
                }
            }
        }

        public static void WriteError(string message)
        {
            WriteError(RC.App, message);
        }

        public static void WriteError(IConsole console, string message)
        {
            lock (m_Lock)
            {
                console.WriteLine(ConsoleVerbosity.Normal, ErrorColor, message);
            }
        }

        public static void WriteError(string message, string detail)
        {
            WriteError(RC.App, message, detail);
        }

        public static void WriteError(IConsole console, string message, string detail)
        {
            lock (m_Lock)
            {
                if (ShowFullErrors == true &&
                    String.IsNullOrEmpty(detail) == false)
                {
                    console.WriteLine(ConsoleVerbosity.Normal, ErrorColor, message);
                    //RC.WriteLine(ConsoleVerbosity.Normal, ErrorDetailColor, " (" + detail + ")");
                    console.WriteLine(ConsoleVerbosity.Normal, ErrorColor, detail);
                }
                else
                {
                    console.WriteLine(ConsoleVerbosity.Normal, ErrorColor, message);
                }
            }
        }

        public static void WriteException(Exception ex)
        {
            WriteException(RC.App, ex);
        }

        public static void WriteException(IConsole console, Exception ex)
        {
            if (ShowFullErrors == true)
            {
                lock (m_Lock)
                {
                    console.WriteException(01, ex);
                }
            }
            else
            {
                WriteError(console, ex.Message);
            }
        }

        public static void WriteException(string message, Exception ex)
        {
            WriteException(RC.App, message, ex);
        }

        public static void WriteException(IConsole console, string message, Exception ex)
        {
            lock (m_Lock)
            {
                WriteError(console, message);

                if (ShowFullErrors == true)
                {
                    console.WriteException(01, ex);
                }
            }
        }

        public static void WriteMessage(MessageDirection dir, OscTimeTag? timeTag, string message)
        {
            WriteMessage(RC.App, dir, timeTag, message);
        }

        public static void WriteMessage(IConsole console, MessageDirection dir, OscTimeTag? timeTag, string message)
        {
            WriteMessage(console, dir, timeTag, MessageColor, message);
        }

        public static void WriteMessage(MessageDirection dir, OscTimeTag? timeTag, ConsoleThemeColor messageColor, string message)
        {
            WriteMessage(RC.App, dir, timeTag, messageColor, message);
        }

        public static void WriteMessage(IConsole console, MessageDirection dir, OscTimeTag? timeTag, ConsoleThemeColor messageColor, string message)
        {
            lock (m_Lock)
            {
                switch (dir)
                {
                    case MessageDirection.Transmit:
                        console.Write(ConsoleVerbosity.Normal, TransmitColor, "TX ");
                        break;
                    case MessageDirection.Receive:
                        console.Write(ConsoleVerbosity.Normal, ReceiveColor, "RX ");
                        break;
                    default:
                        break;
                }

                if (timeTag != null && timeTag.Value.Value > 0)
                {
                    console.Write(ConsoleVerbosity.Normal, TimeTagColor, timeTag.ToString() + " ");
                }

                console.WriteLine(ConsoleVerbosity.Normal, messageColor, message);
            }
        }

        public static void WriteMessage(MessageDirection dir, OscTimeTag? timeTag, ConsoleColorExt messageColor, string message)
        {
            WriteMessage(RC.App, dir, timeTag, messageColor, message);
        }

        public static void WriteMessage(IConsole console, MessageDirection dir, OscTimeTag? timeTag, ConsoleColorExt messageColor, string message)
        {
            lock (m_Lock)
            {
                switch (dir)
                {
                    case MessageDirection.Transmit:
                        console.Write(ConsoleVerbosity.Normal, TransmitColor, "TX ");
                        break;
                    case MessageDirection.Receive:
                        console.Write(ConsoleVerbosity.Normal, ReceiveColor, "RX ");
                        break;
                    default:
                        break;
                }

                if (timeTag != null && timeTag.Value.Value > 0)
                {
                    console.Write(ConsoleVerbosity.Normal, TimeTagColor, timeTag.ToString() + " ");
                }

                console.WriteLine(ConsoleVerbosity.Normal, messageColor, message);
            }
        }


        public static void WriteMessage(MessageDirection dir, ConsoleColorExt messageColor, string message)
        {
            WriteMessage(RC.App, dir, messageColor, message);
        }

        public static void WriteMessage(IConsole console, MessageDirection dir, ConsoleColorExt messageColor, string message)
        {
            lock (m_Lock)
            {
                switch (dir)
                {
                    case MessageDirection.Transmit:
                        console.Write(ConsoleVerbosity.Normal, TransmitColor, "TX ");
                        break;
                    case MessageDirection.Receive:
                        console.Write(ConsoleVerbosity.Normal, ReceiveColor, "RX ");
                        break;
                    default:
                        break;
                }

                console.WriteLine(ConsoleVerbosity.Normal, messageColor, message);
            }
        }

        //public static void WriteMessage(IConsole console, MessageDirection dir, string message)
        //{
        //    lock (m_Lock)
        //    {
        //        switch (dir)
        //        {
        //            case MessageDirection.Transmit:
        //                console.Write(ConsoleVerbosity.Normal, ConsoleColorExt.Green, "TX ");
        //                break;
        //            case MessageDirection.Receive:
        //                console.Write(ConsoleVerbosity.Normal, ConsoleColorExt.Red, "RX ");
        //                break;
        //            default:
        //                break;
        //        }

        //        console.WriteLine(ConsoleVerbosity.Normal, MessageColor, message);
        //    }
        //}


        //public static void WriteMessage(IConsole console, MessageDirection dir, ConsoleThemeColor color, string message)
        //{
        //    lock (m_Lock)
        //    {
        //        switch (dir)
        //        {
        //            case MessageDirection.Transmit:
        //                console.Write(ConsoleVerbosity.Normal, ConsoleColorExt.Green, "TX ");
        //                break;
        //            case MessageDirection.Receive:
        //                console.Write(ConsoleVerbosity.Normal, ConsoleColorExt.Red, "RX ");
        //                break;
        //            default:
        //                break;
        //        }

        //        console.WriteLine(ConsoleVerbosity.Normal, color, message);
        //    }
        //}


        //public static void WriteMessage(IConsole console, MessageDirection dir, ConsoleColorExt color, string message)
        //{
        //    lock (m_Lock)
        //    {
        //        switch (dir)
        //        {
        //            case MessageDirection.Transmit:
        //                console.Write(ConsoleVerbosity.Normal, ConsoleColorExt.Green, "TX ");
        //                break;
        //            case MessageDirection.Receive:
        //                console.Write(ConsoleVerbosity.Normal, ConsoleColorExt.Red, "RX ");
        //                break;
        //            default:
        //                break;
        //        }

        //        console.WriteLine(ConsoleVerbosity.Normal, color, message);
        //    }
        //}
    }
}
