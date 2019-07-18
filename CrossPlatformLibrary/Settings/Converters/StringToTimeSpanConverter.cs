using System;
using System.Globalization;
using CrossPlatformLibrary.Extensions;

namespace CrossPlatformLibrary.Settings.Converters
{
    public class StringToTimeSpanConverter : IConvertible
    {
        // https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-timespan-format-strings#GeneralLong
        private static string Format => "G";

        public object Convert(object value, Type sourceType, Type targetType)
        {
            if (value is string str)
            {
                return TimeSpan.ParseExact(str, Format, CultureInfo.InvariantCulture, TimeSpanStyles.None);
            }

            if (value is TimeSpan timeSpan)
            {
                return timeSpan.ToString(Format, CultureInfo.InvariantCulture);
            }

            throw new InvalidOperationException($"{this.GetType().GetFormattedName()} cannot convert from {sourceType.GetFormattedName()} to {targetType.GetFormattedName()}");
        }
    }
}