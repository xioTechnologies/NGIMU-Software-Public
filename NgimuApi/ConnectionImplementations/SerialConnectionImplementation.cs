using System;
using System.Collections.Generic;
using System.Threading;
using Rug.Osc;

namespace NgimuApi.ConnectionImplementations
{
    /// <summary>
    /// IMU connection implementation over serial COM portName
    /// </summary>
    internal sealed class SerialConnectionImplementation : ConnectionImplementation
    {
        private static readonly List<string> openPorts = new List<string>();
        private static readonly object openPortsSyncLock = new object();
        private Connection connection;

        private SerialConnectionInfo serialConnectionInfo;

        private OscSerial oscSerial;

        public SerialConnectionImplementation(Connection conn, SerialConnectionInfo info, OscCommunicationStatistics statistics)
        {
            connection = conn;
            serialConnectionInfo = info;

            // We set the buffer size to double that of the DefaultPacketSize to hold the maximum number of SLIP escaped bytes
            oscSerial = new OscSerial(info.PortName, (int)info.BaudRate, info.RtsCtsEnabled,
                System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One, OscReceiver.DefaultPacketSize * 2);
            oscSerial.Statistics = statistics;
        }

        public override void CheckConnectionState()
        {
            // TODO: Find a way of checking if the portName is dead
        }

        public override void Close()
        {
            try { oscSerial.Close(); }
            catch { }
            finally
            {
                lock (openPortsSyncLock)
                {
                    openPorts.Remove(serialConnectionInfo.PortName);
                }
            }
        }

        public override void Connect()
        {
            WaitForPortToClose(serialConnectionInfo.PortName, 500);

            lock (openPortsSyncLock)
            {
                openPorts.Add(serialConnectionInfo.PortName);
            }

            oscSerial.Connect();

            connection.OnInfo(string.Format(Strings.SerialConnectionImplementation_Connected, serialConnectionInfo.ToString()));

            oscSerial.PacketRecived += new OscPacketEvent(connection.PacketReceived);
        }

        public override void Dispose()
        {
            oscSerial.Dispose();
        }

        public override void Send(OscPacket packet)
        {
            try
            {
                oscSerial.Send(packet);
            }
            catch (TimeoutException ex)
            {

            }
        }

        public override void Start()
        {
        }

        private static void WaitForPortToClose(string portName, int timeout)
        {
            DateTime startTime = DateTime.Now;

            while (openPorts.Contains(portName) == true)
            {
                if ((DateTime.Now - startTime).TotalMilliseconds >= timeout)
                {
                    return;
                }

                Thread.CurrentThread.Join(10);
            }
        }
    }
}