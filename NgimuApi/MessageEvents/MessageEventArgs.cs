using System;

namespace NgimuApi
{
    public class MessageEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public string Detail { get; private set; }

        public MessageEventArgs(string message)
        {
            Message = message;
            Detail = null;
        }

        public MessageEventArgs(string message, string detail)
        {
            Message = message;
            Detail = detail;
        }
    }
}
