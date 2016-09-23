using System;
using System.Globalization;
using Rug.Osc;

namespace NgimuApi
{
    public enum TimeSpanStringFormat
    {
        StopWatch, 
        Longhand, 
        Shorthand, 
    } 
    public static partial class Helper
    {
        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation.
        /// </summary>
        /// <param name="timeSpan">Time span to be represented as a string in the format hours:minutes:seconds:milliseconds.</param>
        /// <returns>Time span represented as a string.</returns>
        public static string TimeSpanToString(TimeSpan timeSpan, TimeSpanStringFormat format)
        {
            if (format == TimeSpanStringFormat.StopWatch)
            {
                return string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}",
                                     (int)Math.Floor(timeSpan.TotalHours),
                                     timeSpan.Minutes,
                                     timeSpan.Seconds,
                                     timeSpan.Milliseconds);
            }

            bool @short = format == TimeSpanStringFormat.Shorthand; 

            string text = "";

            if (Math.Truncate(timeSpan.TotalHours) != 0)
            {
                string hourString = @short ? "h, " : " hours, ";

                text += Math.Truncate(timeSpan.TotalHours).ToString(CultureInfo.InvariantCulture) + hourString;
            }

            if (Math.Truncate(timeSpan.TotalMinutes) != 0)
            {
                string minuteString = @short ? "m" : " minutes";

                text += timeSpan.Minutes.ToString(CultureInfo.InvariantCulture) + minuteString;
            }
            else
            {
                string secondsString = @short ? "s" : " seconds";

                text += timeSpan.Seconds.ToString(CultureInfo.InvariantCulture) + secondsString;
            }

            return text;
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation.
        /// </summary>
        /// <param name="dateTime">Date and time to be represented as a string in the format year-month-date hour:minute:second.millisecond.</param>
        /// <param name="includeMilliseconds"></param>
        /// <returns>Date and time represented as a string.</returns>
        public static string DateTimeToString(DateTime dateTime, bool includeMilliseconds = false)
        {
            if (includeMilliseconds)
            {
                return dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            }
            else
            {
                return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        public static string TimeTagToString(OscTimeTag timetag)
        {
            //return timetag.ToDataTime().ToLocalTime().ToString("dd-MM-yyyy HH:mm:ss.ffff");            
            return timetag.ToDataTime().ToString("dd-MM-yyyy HH:mm:ss.ffff");
        }
    }
}
