using System;
using System.Globalization;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Converters
{
    internal class DoubleToGridLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double d)
            {
                return new GridLength(d, GridUnitType.Absolute);
            }

            return new GridLength(0, GridUnitType.Absolute);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
