using System;
using System.Collections.Generic;
using System.Text;
using NgimuApi;

namespace NgimuGui
{
    internal static class ReceivedErrorMessages
    {
        public delegate void ErrorMessagesEvent();

        public static int MaxMessages = 256;

        private static readonly List<ErrorMessage> errorMessages = new List<ErrorMessage>();

        private static readonly object syncLock = new object();

        public static event ErrorMessagesEvent Changed;

        public static void Add(string message)
        {
            lock (syncLock)
            {
                errorMessages.Add(new ErrorMessage() { Timestamp = DateTime.Now, Message = message });

                if (errorMessages.Count > MaxMessages)
                {
                    errorMessages.RemoveRange(0, errorMessages.Count - MaxMessages);
                }
            }

            Changed?.Invoke();
        }

        public static void Clear()
        {
            lock (syncLock)
            {
                errorMessages.Clear();
            }

            Changed?.Invoke();
        }

        public static string GetString()
        {
            lock (syncLock)
            {
                StringBuilder sb = new StringBuilder();

                for (int i = errorMessages.Count - 1; i >= 0; i--)
                {
                    sb.Append("[");
                    sb.Append(Helper.DateTimeToString(errorMessages[i].Timestamp, true));
                    sb.Append("] ");
                    sb.AppendLine(errorMessages[i].Message);
                }

                return sb.ToString();
            }
        }

        public struct ErrorMessage
        {
            public string Message;
            public DateTime Timestamp;
        }
    }
}