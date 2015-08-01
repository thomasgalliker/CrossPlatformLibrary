using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CrossPlatformLibrary.Converters
{
    public class BoolToValueConverter<T> : IValueConverter
    {
        public T FalseValue { get; set; }

        public T TrueValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return this.FalseValue;
            }
            
            return (bool)value ? this.TrueValue : this.FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && value.Equals(this.TrueValue);
        }
    }

    public class BoolToImageSourceConverter : BoolToValueConverter<ImageSource>
    {
    }
}