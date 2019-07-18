using System;
using CrossPlatformLibrary.Extensions;

namespace CrossPlatformLibrary.Settings.Converters
{
    public class StringToIntegerConverter : IConvertible
    {
        public object Convert(object value, Type sourceType, Type targetType)
        {
            if (value is string str)
            {
                return int.Parse(str);
            }

            if (value is int i)
            {
                return i.ToString();
            }

            throw new InvalidOperationException($"{this.GetType().GetFormattedName()} cannot convert from {sourceType.GetFormattedName()} to {targetType.GetFormattedName()}");
        }
    }
}