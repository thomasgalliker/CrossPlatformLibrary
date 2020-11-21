using System;
using System.Globalization;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Converters
{
    internal class NullToBoolConverter : IValueConverter
    {
        public bool Inverted { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return true ^ this.Inverted;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
