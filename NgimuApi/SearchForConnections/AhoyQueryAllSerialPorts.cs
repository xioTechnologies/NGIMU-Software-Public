using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Rug.Osc;
using Rug.Osc.Ahoy;

namespace NgimuApi.SearchForConnections
{
    internal delegate void AhoyServiceSerialEvent(string serialNumber, SerialConnectionInfo connectionInfo);

    internal class AhoyQueryAllSerialPorts : IAhoyQuerySerial
    {
        private readonly List<AhoyServiceInfo> ahoyServiceInfoList = new List<AhoyServiceInfo>();
        private readonly object scanSyncLock = new object();
        private readonly Dictionary<string, IAhoyQuerySerial> serialPortQueriesList = new Dictionary<string, IAhoyQuerySerial>();
        private ManualResetEvent portScanComplete = new ManualResetEvent(true);
        private Thread scanForNewPortsThread;
        private bool shouldScanForPorts = false;

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

        public void BeginSearch(int sendInterval = 100)
        {
            EndSearch();

            lock (scanSyncLock)
            {
                ahoyServiceInfoList.Clear();

                shouldScanForPorts = true;
                portScanComplete.Reset();

                scanForNewPortsThread = new Thread(delegate ()
                {
                    try
                    {
                        List<string> portNames = new List<string>();
                        List<string> removed = new List<string>();

                        while (shouldScanForPorts == true)
                        {
                            portNames.Clear();
                            portNames.AddRange(Helper.GetSerialPortNames());

                            foreach (string portName in portNames)
                            {
                                if (serialPortQueriesList.ContainsKey(portName) == true)
                                {
                                    continue;
                                }

                                AhoyQuerySerialPort query = new AhoyQuerySerialPort(portName);

                                serialPortQueriesList.Add(portName, query);

                                query.AnyReceived += OnAnyReceived;
                                query.MessageReceived += OnMessageReceived;
                                query.MessageSent += OnMessageSent;
                                query.ServiceDiscovered += OnServiceDiscovered;
                                query.SerialDeviceDiscovered += OnSerialDeviceDiscovered;

                                query.BeginSearch(sendInterval);
                            }


                            removed.Clear();

                            foreach (string portName in serialPortQueriesList.Keys)
                            {
                                if (portNames.Contains(portName) == false)
                                {
                                    removed.Add(portName);
                                }
                            }

                            foreach (string portName in removed)
                            {
                                AhoyQuerySerialPort query = serialPortQueriesList[portName] as AhoyQuerySerialPort;

                                query.EndSearch();

                                serialPortQueriesList.Remove(portName);

                                foreach (AhoyServiceInfo serviceInfo in query)
                                {
                                    ServiceExpired?.Invoke(serviceInfo);

                                    SerialConnectionInfo connInfo = new SerialConnectionInfo()
                                    {
                                        PortName = portName,
                                        BaudRate = 115200,
                                        RtsCtsEnabled = false,
                                    };

                                    SerialDeviceExpired?.Invoke(serviceInfo.Descriptor, connInfo);

                                    ahoyServiceInfoList.Remove(serviceInfo);
                                }
                            }

                            Thread.CurrentThread.Join(sendInterval);
                        }
                    }
                    finally
                    {
                        portScanComplete.Set();
                    }
                });
                scanForNewPortsThread.Name = "Ahoy Scan For New Ports Thread";
                scanForNewPortsThread.Start();
            }
        }

        public void Dispose()
        {
            EndSearch();
        }

        public void EndSearch()
        {
            lock (scanSyncLock)
            {
                shouldScanForPorts = false;
                portScanComplete.WaitOne();

                foreach (IAhoyQuery query in serialPortQueriesList.Values)
                {
                    query.EndSearch();
                }

                serialPortQueriesList.Clear();
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

        private void OnAnyReceived(OscMessage message)
        {
            AnyReceived?.Invoke(message);
        }

        private void OnMessageReceived(OscMessage message)
        {
            MessageReceived?.Invoke(message);
        }

        private void OnMessageSent(OscMessage message)
        {
            MessageSent?.Invoke(message);
        }

        private void OnSerialDeviceDiscovered(string serialNumber, SerialConnectionInfo serialConnectionInfo)
        {
            SerialDeviceDiscovered?.Invoke(serialNumber, serialConnectionInfo);
        }

        private void OnServiceDiscovered(AhoyServiceInfo serviceInfo)
        {
            ahoyServiceInfoList.Add(serviceInfo);

            ServiceDiscovered?.Invoke(serviceInfo);
        }
    }

    internal interface IAhoyQuerySerial : IAhoyQuery
    {
        event AhoyServiceSerialEvent SerialDeviceDiscovered;
        event AhoyServiceSerialEvent SerialDeviceExpired;
    }
}