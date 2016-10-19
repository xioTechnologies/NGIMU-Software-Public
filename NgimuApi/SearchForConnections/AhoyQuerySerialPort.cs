using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using Rug.Osc;
using Rug.Osc.Ahoy;

namespace NgimuApi.SearchForConnections
{
    internal class AhoyQuerySerialPort : IAhoyQuerySerial
    {
        private readonly List<AhoyServiceInfo> ahoyServiceInfoList = new List<AhoyServiceInfo>();
        private readonly string portName;
        private readonly ManualResetEvent searchComplete = new ManualResetEvent(true);
        private readonly object searchSyncLock = new object();
        private Thread searchThread;
        private bool shouldSearch = false;

        public int Count { get { return ahoyServiceInfoList.Count; } }

        public string Namespace { get; private set; } 

        public AhoyServiceInfo this[int index] { get { return ahoyServiceInfoList[index]; } }  

        public event OscMessageEvent AnyReceived;

        public event OscMessageEvent MessageReceived;

        public event OscMessageEvent MessageSent;

        public event AhoyServiceSerialEvent SerialDeviceDiscovered;

        public event AhoyServiceSerialEvent SerialDeviceExpired;

        public event AhoyServiceEvent ServiceDiscovered;

        public event AhoyServiceEvent ServiceExpired;

        public AhoyQuerySerialPort(string portName)
        {
            this.portName = portName;
        }

        public void BeginSearch(int sendInterval = 100)
        {
            EndSearch();

            lock (searchSyncLock)
            {
                ahoyServiceInfoList.Clear();

                shouldSearch = true;
                searchComplete.Reset();

                searchThread = new Thread(delegate ()
                {
                    Connection imuConnection = null;

                    string serialNumber;
                    SerialConnectionInfo serialConnectionInfo = new SerialConnectionInfo()
                    {
                        PortName = portName,
                        BaudRate = 115200,
                        RtsCtsEnabled = false,
                    };

                    try
                    {
                        do
                        {
                            //Thread.CurrentThread.Join(100);

                            try
                            {
                                imuConnection = new Connection(serialConnectionInfo);

                                imuConnection.Connect();
                            }
                            catch
                            {
                                imuConnection?.Dispose();
                                imuConnection = null;
                            }
                        }
                        while (shouldSearch == true && imuConnection == null);

                        if (shouldSearch == false)
                        {
                            return;
                        }

                        do
                        {
                            //Thread.CurrentThread.Join(100);

                            //SerialConnectionInfo info;
                            //string serialNumber;

                            //if (TryConnection(portName, 115200, false, sendInterval, 1, out info, out serialNumber) == false)
                            //{
                            //    continue;
                            //}

                            try
                            {
                                if (Commands.Send(imuConnection, Command.Ahoy, sendInterval, 1, out serialNumber) != CommunicationProcessResult.Success)
                                {
                                    continue;
                                }
                            }
                            catch
                            {
                                continue;
                            }

                            AhoyServiceInfo serviceInfo = new AhoyServiceInfo(IPAddress.Any, IPAddress.Any, 0, 0, serialNumber, string.Empty, new object[] { serialConnectionInfo }, 0);

                            if (ahoyServiceInfoList.Contains(serviceInfo) == true)
                            {
                                continue;
                            }

                            ahoyServiceInfoList.Add(serviceInfo);

                            ServiceDiscovered?.Invoke(serviceInfo);

                            SerialDeviceDiscovered?.Invoke(serialNumber, serialConnectionInfo);

                            return;
                        }
                        while (shouldSearch == true);
                    }
                    finally
                    {
                        imuConnection?.Dispose();

                        searchComplete.Set();
                    }
                });
                searchThread.Name = "Ahoy Scan Port " + portName;
                searchThread.Start();
            }
        }

        public void Dispose()
        {
            EndSearch();
        }

        public void EndSearch()
        {
            lock (searchSyncLock)
            {
                shouldSearch = false;
                //searchComplete.WaitOne();
            }
        }

        public IEnumerator<AhoyServiceInfo> GetEnumerator()
        {
            return ahoyServiceInfoList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (ahoyServiceInfoList as System.Collections.IEnumerable).GetEnumerator();
        }

        public void Search(int sendInterval = 100, int timeout = 500)
        {
            try
            {
                BeginSearch(sendInterval);

                Thread.CurrentThread.Join(timeout);
            }
            finally
            {
                EndSearch();
            }
        }
    }
}