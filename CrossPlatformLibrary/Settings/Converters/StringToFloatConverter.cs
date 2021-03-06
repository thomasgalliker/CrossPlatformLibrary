﻿using System;
using System.Globalization;
using CrossPlatformLibrary.Extensions;

namespace CrossPlatformLibrary.Settings.Converters
{
    public class StringToFloatConverter : IConvertible
    {
        // https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.110).aspx#RFormatString
        private static string Format => "R";

        public object Convert(object value, Type sourceType, Type targetType)
        {
            if (value is string str)
            {
                return float.Parse(str, CultureInfo.InvariantCulture);
            }

            if (value is float f)
            {
                return f.ToString(Format, CultureInfo.InvariantCulture);
            }

            throw new InvalidOperationException($"{this.GetType().GetFormattedName()} cannot convert from {sourceType.GetFormattedName()} to {targetType.GetFormattedName()}");
        }
    }
}