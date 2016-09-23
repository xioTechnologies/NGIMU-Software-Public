
namespace NgimuApi.ConnectionImplementations
{
    /// <summary>
    /// Abstract connection implementation
    /// </summary>
    internal abstract class ConnectionImplementation
    {
        /// <summary>
        /// Check the state of the connection, this is pumped by the application.
        /// </summary>
        public abstract void CheckConnectionState();

        /// <summary>
        /// Connect.
        /// </summary>
        public abstract void Connect();

        /// <summary>
        /// Start listening.
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// Close. 
        /// </summary>
        public abstract void Close();

        /// <summary>
        /// Close and dispose.
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// Send a osc packet.
        /// </summary>
        /// <param name="packet"></param>
        public abstract void Send(Rug.Osc.OscPacket packet);
    }
}
