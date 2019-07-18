using System;
using CrossPlatformLibrary.Extensions;

namespace CrossPlatformLibrary.Settings.Converters
{
    public class StringToUriConverter : IConvertible
    {
        public object Convert(object value, Type sourceType, Type targetType)
        {
            if (value is string str)
            {
                return new Uri(str);
            }

            if (value is Uri uri)
            {
                return uri.AbsoluteUri;
            }

            throw new InvalidOperationException($"{this.GetType().GetFormattedName()} cannot convert from {sourceType.GetFormattedName()} to {targetType.GetFormattedName()}");
        }
    }
}