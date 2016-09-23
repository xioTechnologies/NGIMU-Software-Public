using System;
using Rug.Osc;

namespace NgimuApi
{
    public interface IReporter
    {
        void OnUpdated(object sender, EventArgs args);

        void OnCompleted(object sender, EventArgs args);

        void OnException(object sender, ExceptionEventArgs args);

        void OnError(object sender, MessageEventArgs args);

        void OnInfo(object sender, MessageEventArgs args);

        void OnMessage(Connection source, MessageDirection direction, OscMessage message);
    }
}
