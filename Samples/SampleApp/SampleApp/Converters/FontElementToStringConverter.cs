using System;
using System.Globalization;
using CrossPlatformLibrary.Forms.Themes;
using ValueConverters;

namespace SampleApp.Converters
{
    internal class FontElementToStringConverter : ConverterBase
    {
        protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is FontElement fontElement)
            {
                var fontElementString = $"FontFamily: {fontElement.FontFamily ?? "null"}{Environment.NewLine}" +
                                        $"FontSize: {string.Format(CultureInfo.InvariantCulture, $"{fontElement.FontSize}")}{Environment.NewLine}" +
                                        $"FontAttributes: {fontElement.FontAttributes}";
                return fontElementString;
            }

            return null;
        }
    }
}