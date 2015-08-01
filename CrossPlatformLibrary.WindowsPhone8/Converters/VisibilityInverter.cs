using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CrossPlatformLibrary.Converters
{
    public class VisibilityInverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.ToggleVisibility(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.ToggleVisibility(value);
        }

        private object ToggleVisibility(object value)
        {
            if (value is Visibility)
            {
                var visibility = (Visibility)value;

                switch (visibility)
                {
                    case Visibility.Visible:
                        return Visibility.Collapsed;
                    case Visibility.Collapsed:
                        return Visibility.Visible;
                }
            }

            return DependencyProperty.UnsetValue;
        }
    }
}