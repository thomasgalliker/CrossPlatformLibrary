using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CrossPlatformLibrary.Forms.Resources;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Converters
{
    internal class PropertyErrorsToColorConverter : BindableObject, IValueConverter
    {
        public object NormalColor => (Color)Application.Current.Resources[ThemeConstants.Color.TextColor];

        public object ErrorColor => (Color)Application.Current.Resources[ThemeConstants.Color.ERROR];

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var propertyErrors = (IEnumerable<string>)value;
            if (propertyErrors != null && propertyErrors.Any())
            {
                return this.ErrorColor;
            }

            return this.NormalColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}