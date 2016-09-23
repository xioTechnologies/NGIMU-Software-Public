using System;
using Rug.Osc;

namespace NgimuApi.ConnectionImplementations
{
    /// <summary>
    /// Connection implementation that reads a SLIP data stream from a file.
    /// </summary>
    internal sealed class FileReadConnectionImplementation : ConnectionImplementation
    {
        private Connection connection;

        private OscFileReader fileReader;
        private SDCardFileConnectionInfo sdCardFileConnectionInfo;
        private OscCommunicationStatistics statistics;

        private bool shouldExit = false;

        public FileReadConnectionImplementation(Connection connection, SDCardFileConnectionInfo info, OscCommunicationStatistics statistics)
        {
            this.connection = connection;
            sdCardFileConnectionInfo = info;
            this.statistics = statistics;
        }

        public override void CheckConnectionState()
        {

        }

        public override void Connect()
        {
            fileReader = new OscFileReader(sdCardFileConnectionInfo.FilePath, OscPacketFormat.Slip);

            connection.OnInfo(string.Format(Strings.FileReadConnectionImplementation_Reading, sdCardFileConnectionInfo.FilePath));

            fileReader.PacketRecived += new OscPacketEvent(connection.PacketReceived);
            fileReader.Statistics = statistics;

            shouldExit = false;
        }

        public override void Start()
        {
            ReadLoop();
        }

        public override void Close()
        {
            shouldExit = true;
        }

        public override void Dispose()
        {
            shouldExit = true;
        }

        public override void Send(OscPacket packet)
        {
            //throw new NotImplementedException();
        }

        private void ReadLoop()
        {
            try
            {
                while (fileReader.EndOfStream == false &&
                    shouldExit == false)
                {
                    fileReader.Read();
                }
            }
            catch (Exception ex)
            {
                connection.OnException(Strings.FileReadConnectionImplementation_ErrorReading, ex);
            }
            finally
            {
                fileReader.Dispose();
            }
        }
    }
}
