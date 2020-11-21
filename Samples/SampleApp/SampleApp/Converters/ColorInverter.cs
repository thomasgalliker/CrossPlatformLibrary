using System;
using System.Globalization;
using CrossPlatformLibrary.Forms.Extensions;
using ValueConverters;
using Xamarin.Forms;

namespace SampleApp.Converters
{
    internal class ColorInverter : ConverterBase
    {
        protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is Color color)
            {
                var invertedColor = color.Invert();
                return invertedColor;
            }

            return null;
        }
    }
}