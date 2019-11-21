using System;
using System.Globalization;
using ValueConverters;
using Xamarin.Forms;

namespace SampleApp.Converters
{
    internal class ColorToHexStringConverter : ConverterBase
    {
        protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                var hexColor = color.ToHex();
                return hexColor;
            }

            return null;
        }
    }
}
