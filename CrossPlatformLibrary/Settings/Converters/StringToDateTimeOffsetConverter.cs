using System;
using System.Globalization;
using CrossPlatformLibrary.Extensions;

namespace CrossPlatformLibrary.Settings.Converters
{
    public class StringToDateTimeOffsetConverter : IConvertible
    {
        // https://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#Roundtrip
        private static string Format => "O";

        public object Convert(object value, Type sourceType, Type targetType)
        {
            if (value is string str)
            {
                return DateTimeOffset.ParseExact(str, Format, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            }

            if (value is DateTimeOffset dateTimeOffset)
            {
                return dateTimeOffset.ToString(Format, CultureInfo.InvariantCulture);
            }

            throw new InvalidOperationException($"{this.GetType().GetFormattedName()} cannot convert from {sourceType.GetFormattedName()} to {targetType.GetFormattedName()}");
        }
    }
}