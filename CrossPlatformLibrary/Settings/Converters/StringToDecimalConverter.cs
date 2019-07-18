using System;
using System.Globalization;
using CrossPlatformLibrary.Extensions;

namespace CrossPlatformLibrary.Settings.Converters
{
    public class StringToDecimalConverter : IConvertible
    {
        // https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.110).aspx#GFormatString
        private static string Format => "G";

        public object Convert(object value, Type sourceType, Type targetType)
        {
            if (value is string str)
            {
                return decimal.Parse(str, CultureInfo.InvariantCulture);
            }

            if (value is decimal d)
            {
                return d.ToString(Format, CultureInfo.InvariantCulture);
            }

            throw new InvalidOperationException($"{this.GetType().GetFormattedName()} cannot convert from {sourceType.GetFormattedName()} to {targetType.GetFormattedName()}");
        }
    }
}