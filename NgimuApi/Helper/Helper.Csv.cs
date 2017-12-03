using System;
using System.Globalization;
using System.Text;
using NgimuApi.Data;
using Rug.Osc;

namespace NgimuApi
{
    public static partial class Helper
    {
        public static string UnknownMessageToCsvHeader(OscMessage message)
        {
            StringBuilder sb = new StringBuilder();

            string seperatorString = new string(',', 1);

            sb.Append("Time(s)");

            sb.Append(seperatorString);

            for (int i = 0; i < message.Count; i++)
            {
                sb.Append("Argument " + i);

                sb.Append(seperatorString);
            }

            string final = sb.ToString();

            // strip of the final separator string 
            final = final.Substring(0, final.Length - seperatorString.Length);

            return final;
        }

        public static string UnknownMessageToCsv(OscTimeTag firstTimestamp, OscMessage message)
        {
            string timeString = "";

            if (message.TimeTag.HasValue == true)
            {
                timeString = DataBase.CreateTimestampString(message.TimeTag.Value, firstTimestamp);
            }
            else
            {
                timeString = DataBase.CreateTimestampString(firstTimestamp, firstTimestamp);
            }

            StringBuilder sb = new StringBuilder();

            string seperatorString = new string(',', 1);

            sb.Append(timeString);

            sb.Append(seperatorString);

            foreach (object obj in message)
            {
                CellToString(sb, obj);

                sb.Append(seperatorString);
            }

            string final = sb.ToString();

            // strip of the final separator string 
            final = final.Substring(0, final.Length - seperatorString.Length);

            return final;
        }

        public static string ToCsv(params object[] cells)
        {
            StringBuilder sb = new StringBuilder();

            string seperatorString = new string(',', 1);

            foreach (object obj in cells)
            {
                CellToString(sb, obj);

                sb.Append(seperatorString);
            }

            string final = sb.ToString();

            // strip of the final separator string 
            final = final.Substring(0, final.Length - seperatorString.Length);

            return final;
        }

        private static void CellToString(StringBuilder sb, object obj)
        {
            if (obj is bool)
            {
                sb.Append((bool)obj == true ? "1" : "0");
            }
            else if (obj is int)
            {
                sb.Append(((int)obj).ToString(CultureInfo.InvariantCulture));
            }
            else if (obj is uint)
            {
                sb.Append(((uint)obj).ToString(CultureInfo.InvariantCulture));
            }
            else if (obj is long)
            {
                sb.Append(((long)obj).ToString(CultureInfo.InvariantCulture));
            }
            else if (obj is ulong)
            {
                sb.Append(((ulong)obj).ToString(CultureInfo.InvariantCulture));
            }
            else if (obj is short)
            {
                sb.Append(((short)obj).ToString(CultureInfo.InvariantCulture));
            }
            else if (obj is ushort)
            {
                sb.Append(((ushort)obj).ToString(CultureInfo.InvariantCulture));
            }
            else if (obj is float)
            {
                float value = (float)obj;
                if (float.IsInfinity(value) || float.IsNaN(value))
                {
                    value = 0;
                }
                sb.Append(value.ToString(CultureInfo.InvariantCulture));
            }
            else if (obj is double)
            {
                double value = (double)obj;
                if (double.IsInfinity(value) || double.IsNaN(value))
                {
                    value = 0;
                }
                sb.Append(value.ToString(CultureInfo.InvariantCulture));
            }
            else if (obj is decimal)
            {
                sb.Append(((decimal)obj).ToString(CultureInfo.InvariantCulture));
            }
            else if (obj is TimeSpan)
            {
                sb.Append(((TimeSpan)obj).TotalMinutes.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                sb.Append(obj.ToString());
            }
        }
    }
}
