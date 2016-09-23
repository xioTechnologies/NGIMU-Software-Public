using System;
using System.Threading;
using Rug.Osc;

namespace NgimuApi.ConnectionImplementations
{
    /// <summary>
    /// Connection implementation over UDP 
    /// </summary>
    internal sealed class UdpConnectionImplementation : ConnectionImplementation
    {
        private Connection connection;

        private OscReceiver oscReceiver;
        private OscSender oscSender;
        private UdpConnectionInfo udpConnectionInfo;
        private Thread thread;

        public UdpConnectionImplementation(Connection connection, UdpConnectionInfo info, OscCommunicationStatistics statistics)
        {
            this.connection = connection;
            udpConnectionInfo = info;

            oscReceiver = new OscReceiver(info.AdapterIPAddress, info.ReceivePort, OscReceiver.DefaultMessageBufferSize, OscReceiver.DefaultPacketSize);
            oscReceiver.Statistics = statistics;

            oscSender = new OscSender(info.AdapterIPAddress, 0, info.SendIPAddress, info.SendPort,
                OscSender.DefaultMulticastTimeToLive, OscSender.DefaultMessageBufferSize, OscSender.DefaultPacketSize);
            oscSender.Statistics = statistics;
        }

        public override void CheckConnectionState()
        {
            if (oscReceiver.State != OscSocketState.Connected)
            {
                return;
            }

            if (udpConnectionInfo.HasAdapterChanged == false &&
                oscSender.State != OscSocketState.Closed)
            {
                return;
            }

            udpConnectionInfo.HasAdapterChanged = false;

            connection.OnError(Strings.UdpConnectionImplementation_IPAddressChanged);

            connection.Close();
        }

        public override void Connect()
        {
            oscReceiver.Connect();
            oscSender.Connect();

            thread = new Thread(new ThreadStart(ReceiveLoop));
            thread.Name = "Udp Connection Implementation Receive Loop" + udpConnectionInfo.ToString();

            if (oscReceiver.State != OscSocketState.Connected &&
                oscSender.State != OscSocketState.Connected)
            {
                throw new Exception(string.Format(Strings.UdpConnectionImplementation_FailedToConnect, udpConnectionInfo.ToString()));
            }

            connection.OnInfo(string.Format(Strings.UdpConnectionImplementation_Connected, udpConnectionInfo.ToString()));
        }

        public override void Start()
        {
            thread.Start();
        }

        public override void Close()
        {
            try { oscReceiver.Close(); } catch { }

            try { oscSender.Close(); } catch { }
        }

        public override void Dispose()
        {
            oscReceiver.Dispose();

            oscSender.Dispose();
        }

        public override void Send(OscPacket packet)
        {
            oscSender.Send(packet);
        }

        private void ReceiveLoop()
        {
            try
            {
                while (oscReceiver.State != OscSocketState.Closed)
                {
                    // if we are in a state to receive
                    if (oscReceiver.State == OscSocketState.Connected)
                    {
                        // get the next callback 
                        // this will block until one arrives or the socket is closed
                        OscPacket packet = oscReceiver.Receive();

                        connection.PacketReceived(packet);
                    }
                }
            }
            catch (Exception ex)
            {
                if (oscReceiver.State != OscSocketState.Closed &&
                    oscReceiver.State != OscSocketState.Closing)
                {
                    connection.OnException(Strings.UdpConnectionImplementation_ErrorReceiving, ex);
                }
            }
        }
    }
}
