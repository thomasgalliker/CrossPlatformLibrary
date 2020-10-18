using System;
using CrossPlatformLibrary.Extensions;

namespace CrossPlatformLibrary.Settings.Converters
{
    public class StringToIntConverter : StringToNullableIntConverter
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

    public class StringToNullableIntConverter : IConvertible
    {
        public virtual object Convert(object value, Type sourceType, Type targetType)
        {
            if (value is string str)
            {
                return int.Parse(str);
            }

            if (value is int i)
            {
                return i.ToString();
            }

            return null;
        }
    }
}