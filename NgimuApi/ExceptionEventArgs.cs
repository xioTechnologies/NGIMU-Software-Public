using System;

namespace NgimuApi
{
    public class ExceptionEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public Exception Exception { get; private set; }

        public ExceptionEventArgs(string message, Exception exception)
        {
            Message = message;
            Exception = exception;
        }
    }
}
