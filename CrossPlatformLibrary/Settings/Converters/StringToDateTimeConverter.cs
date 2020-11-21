using System;
using System.Globalization;
using CrossPlatformLibrary.Extensions;

namespace CrossPlatformLibrary.Settings.Converters
{
    public class StringToDateTimeConverter : StringToNullableDateTimeConverter
    {
        public override object Convert(object value, Type sourceType, Type targetType)
        {
            var converted = base.Convert(value, sourceType, targetType);
            if (converted == null)
            {
                throw new InvalidOperationException($"{this.GetType().GetFormattedName()} cannot convert from {sourceType.GetFormattedName()} to {targetType.GetFormattedName()}");
            }

            return converted;
        }
    }

    public class StringToNullableDateTimeConverter : IConvertible
    {
        // https://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#Roundtrip
        private static string Format => "O";

        public virtual object Convert(object value, Type sourceType, Type targetType)
        {
            if (value is string str && DateTime.TryParseExact(str, Format, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var result))
            {
                return result;
            }

            if (value is DateTime dateTime)
            {
                return dateTime.ToString(Format, CultureInfo.InvariantCulture);
            }

            return null;
        }
    }
}