using System;
using CrossPlatformLibrary.Extensions;

namespace CrossPlatformLibrary.Settings.Converters
{
    public class StringToBoolConverter : IConvertible
    {
        public object Convert(object value, Type sourceType, Type targetType)
        {
            if (value is string str)
            {
                return bool.Parse(str);
            }

            if (value is bool b)
            {
                return b.ToString();
            }

            throw new InvalidOperationException($"{this.GetType().GetFormattedName()} cannot convert from {sourceType.GetFormattedName()} to {targetType.GetFormattedName()}");
        }
    }
}