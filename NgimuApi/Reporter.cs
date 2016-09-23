using System;
using Rug.Osc;

namespace NgimuApi
{
    public class Reporter : IReporter
    {
        /// <summary>
        /// Occurs when an exception is thrown.
        /// </summary>
        public event EventHandler<ExceptionEventArgs> Exception;

        /// <summary>
        /// Occurs when an error is raised.
        /// </summary>
        public event EventHandler<MessageEventArgs> Error;

        /// <summary>
        /// Occurs when an info message is raised. 
        /// </summary>
        public event EventHandler<MessageEventArgs> Info;

        /// <summary>
        /// Occurs when a message is transmitted or received. 
        /// </summary>
        public event MessageEvent Message;

        /// <summary>
        /// Occurs when updated.
        /// </summary>
        public event EventHandler Updated;

        /// <summary>
        /// Occurs apon completion.
        /// </summary>
        public event EventHandler Completed;

        public void OnUpdated(object sender, EventArgs args)
        {
            Updated?.Invoke(sender, args);
        }

        public void OnCompleted(object sender, EventArgs args)
        {
            Completed?.Invoke(sender, args);
        }

        public void OnException(object sender, ExceptionEventArgs args)
        {
            Exception?.Invoke(sender, args);
        }

        public void OnError(object sender, MessageEventArgs args)
        {
            Error?.Invoke(sender, args);
        }

        public void OnInfo(object sender, MessageEventArgs args)
        {
            Info?.Invoke(sender, args);
        }

        public void OnMessage(Connection source, MessageDirection direction, OscMessage message)
        {
            Message?.Invoke(source, direction, message);
        }

        public static IReporter WrapCompleteEvent(EventHandler completedEvent)
        {
            return new Reporter()
            {
                Completed = completedEvent,
            };
        }
    }

    public class ReporterWrapper : IReporter
    {
        public IReporter Reporter { get; private set; }

        public event EventHandler Completed;

        public ReporterWrapper(IReporter reporter)
        {
            Reporter = reporter;
        }

        public void OnUpdated(object sender, EventArgs args)
        {
            Reporter?.OnUpdated(sender, args);
        }

        public void OnCompleted(object sender, EventArgs args)
        {
            Completed?.Invoke(sender, args);

            Reporter?.OnCompleted(sender, args);
        }

        public void OnException(object sender, ExceptionEventArgs args)
        {
            Reporter?.OnException(sender, args);
        }

        public void OnError(object sender, MessageEventArgs args)
        {
            Reporter?.OnError(sender, args);
        }

        public void OnInfo(object sender, MessageEventArgs args)
        {
            Reporter?.OnInfo(sender, args);
        }

        public void OnMessage(Connection source, MessageDirection direction, OscMessage message)
        {
            Reporter?.OnMessage(source, direction, message);
        }
    }
}
