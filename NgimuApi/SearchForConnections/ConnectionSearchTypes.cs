
namespace NgimuApi.SearchForConnections
{
    /// <summary>
    /// Connection search type filter.
    /// </summary>
    public enum ConnectionSearchTypes
    {
        /// <summary>
        /// Don't search for anything.
        /// </summary>
        None = 0,

        /// <summary>
        /// Search for UDP connections.
        /// </summary>
        Udp = 1,

        /// <summary>
        /// Search for serial connections.
        /// </summary>
        Serial = 2,

        /// <summary>
        /// Search for any connection.
        /// </summary>
        All = Serial | Udp,
    }
}
