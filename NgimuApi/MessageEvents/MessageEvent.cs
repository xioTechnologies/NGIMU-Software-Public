using Rug.Osc;

namespace NgimuApi
{
    public delegate void MessageEvent(Connection source, MessageDirection direction, OscMessage message);
}
