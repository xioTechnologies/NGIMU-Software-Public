using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text.RegularExpressions;

namespace NgimuApi
{
    public static partial class Helper
    {
        private static Regex validWindowsSerialPortRegex = new Regex(@"^COM[0-9]+", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

        private struct ComPortInfo : IComparable<ComPortInfo>
        {
            public string Name;
            public int Order;

            public int CompareTo(ComPortInfo other)
            {
                return Order - other.Order;
            }
        }

        public static string[] GetSerialPortNames()
        {
            string[] ports = SerialPort.GetPortNames();

            List<ComPortInfo> portInfos = new List<ComPortInfo>(ports.Length);

            for (int i = 0; i < ports.Length; i++)
            {
                ComPortInfo port = CleanSerialPortName(ports[i]);

                if (String.IsNullOrEmpty(port.Name) == true)
                {
                    continue;
                }

                portInfos.Add(port);
            }

            portInfos.Sort();

            string[] finalPorts = new string[portInfos.Count];

            for (int i = 0; i < finalPorts.Length; i++)
            {
                finalPorts[i] = portInfos[i].Name;
            }

            return finalPorts;
        }

        private static ComPortInfo CleanSerialPortName(string name)
        {
            ComPortInfo result = new ComPortInfo()
            {
                Name = null,
                Order = 0,
            };

            // Handle weird linux COM port names
            if (name.StartsWith("/") == true)
            {
                if (name.StartsWith("/dev/tty.") == false)
                {
                    return result;
                }

                result.Name = name;
                result.Order = name.GetHashCode();

                return result;
            }

            // Handle windows COM ports
            if (validWindowsSerialPortRegex.IsMatch(name) == true)
            {
                result.Name = validWindowsSerialPortRegex.Match(name).Value;
                result.Order = int.Parse(result.Name.Substring(3));
            }

            return result;
        }
    }
}
