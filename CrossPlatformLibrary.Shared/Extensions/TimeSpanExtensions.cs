using System;

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
    }
}