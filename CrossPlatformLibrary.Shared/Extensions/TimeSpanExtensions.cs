using System;
using System.Globalization;

namespace CrossPlatformLibrary.Extensions
{
    public static class TimeSpanExtensions
    {
        public static string UnitSeconds = "s";

        /// <summary>
        /// Returns the formatted string for a given <paramref name="timeSpan"/>.
        /// </summary>
        public static string ToSecondsString(this TimeSpan timeSpan)
        {
            if (timeSpan.TotalMilliseconds >= 100)
            {
                return $"{timeSpan.TotalSeconds:F1}{UnitSeconds}";
            }

            if (timeSpan.TotalMilliseconds >= 10)
            {
                return $"{timeSpan.TotalSeconds:F2}{UnitSeconds}";
            }

            if (timeSpan.TotalMilliseconds >= 1)
            {
                return $"{timeSpan.TotalSeconds:F3}{UnitSeconds}";
            }

            return $"{timeSpan.TotalSeconds:F1}{UnitSeconds}";
        }

        /// <summary>
        /// Returns the formatted string for a given <paramref name="timeSpan"/>.
        /// </summary>
        public static string ToSecondsString(this TimeSpan? timeSpan)
        {
            return timeSpan == null ? "" : timeSpan.Value.ToSecondsString();
        }

        public static string ToDurationString(this TimeSpan? timeSpan)
        {
            return timeSpan?.ToDurationString();
        }

        public static string ToDurationString(this TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.MinValue)
            {
                return "";
            }

            if (timeSpan == TimeSpan.Zero)
            {
                return "0s";
            }

            var absoluteTimeSpan = timeSpan.Duration();
            var daysString = AddDaysIfNotZero(absoluteTimeSpan);
            if (daysString != null)
            {
                return daysString;
            }

            var hoursString = GetHours(absoluteTimeSpan);
            if (hoursString != null)
            {
                return hoursString;
            }

            var minutesString = GetMinutes(absoluteTimeSpan);
            if (minutesString != null)
            {
                return minutesString;
            }

            var secondsString = GetSeconds(absoluteTimeSpan);
            if (secondsString != null)
            {
                return secondsString;
            }

            var microsecondsString = GetMicroseconds(absoluteTimeSpan);
            if (microsecondsString != null)
            {
                return microsecondsString;
            }

            return "";
        }

        private static string GetMicroseconds(TimeSpan timeSpan)
        {
            var num1 = timeSpan.Ticks % 10000L;
            if (num1 <= 0L)
            {
                return null;
            }

            var num2 = num1 / 10000.0 * 1000.0;
            return num2.ToString("0.0", CultureInfo.InvariantCulture) + "µs";
        }

        private static string GetSeconds(TimeSpan timeSpan)
        {
            if (timeSpan.Seconds <= 0 && timeSpan.Milliseconds <= 0)
            {
                return null;
            }

            var seconds = timeSpan.Seconds;
            var secondsString = seconds.ToString(CultureInfo.InvariantCulture);
            if (timeSpan.Milliseconds > 0)
            {
                secondsString = $"{secondsString}.{timeSpan.Milliseconds.ToString("000", CultureInfo.InvariantCulture)}";
            }

            return secondsString + "s";
        }

        private static string GetMinutes(TimeSpan timeSpan)
        {
            if (timeSpan.Minutes <= 0)
            {
                return null;
            }

            return timeSpan.Minutes.ToString(CultureInfo.InvariantCulture) + "m";
        }

        private static string GetHours(TimeSpan timeSpan)
        {
            if (timeSpan.Hours <= 0)
            {
                return null;
            }

            return timeSpan.Hours.ToString(CultureInfo.InvariantCulture) + "h";
        }

        private static string AddDaysIfNotZero(TimeSpan timeSpan)
        {
            if (timeSpan.Days <= 0)
            {
                return null;
            }

            return timeSpan.Days.ToString(CultureInfo.InvariantCulture) + "d";
        }
    }
}