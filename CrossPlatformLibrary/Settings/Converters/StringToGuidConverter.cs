using System;
using System.Globalization;
using CrossPlatformLibrary.Extensions;

namespace CrossPlatformLibrary.Settings.Converters
{
    public class StringToGuidConverter : IConvertible
    {
        // https://msdn.microsoft.com/de-de/library/s6tk2z69(v=vs.110).aspx
        private static string Format => "B";

        public object Convert(object value, Type sourceType, Type targetType)
        {
            if (value is string str)
            {
                return new Guid(str);
            }

            if (value is Guid guid)
            {
                return guid.ToString(Format, CultureInfo.InvariantCulture);
            }

            throw new InvalidOperationException($"{this.GetType().GetFormattedName()} cannot convert from {sourceType.GetFormattedName()} to {targetType.GetFormattedName()}");
        }
    }
}