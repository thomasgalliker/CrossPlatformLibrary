using System;
using System.Globalization;
using ValueConverters;
using Xamarin.Forms;

namespace SampleApp.Converters
{
    internal class ThicknessToStringConverter : ConverterBase
    {
        protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Thickness thickness)
            {
                var thicknessString = $"{{{thickness.Left}, {thickness.Top}, {thickness.Right}, {thickness.Bottom}}}";
                return thicknessString;
            }

            return null;
        }
    }
}