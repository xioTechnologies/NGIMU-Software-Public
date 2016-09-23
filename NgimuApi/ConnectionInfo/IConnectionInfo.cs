namespace NgimuApi
{
    /// <summary>
    /// Types that implement this interface can be used to establish a connection.
    /// </summary>
    public interface IConnectionInfo
    {
        /// <summary>
        /// The ID to use as a unique key.
        /// </summary>
        /// <returns>A unique key string.</returns>
        string ToIDString();
    }
}
