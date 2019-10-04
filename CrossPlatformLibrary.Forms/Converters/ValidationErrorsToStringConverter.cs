using System;
using System.Globalization;
using System.Linq;
using CrossPlatformLibrary.Forms.Validation;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Converters
{
    public class ValidationErrorsToStringConverter : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ViewModelValidation viewModelValidation && viewModelValidation.HasErrors)
            {
                var errorLines = viewModelValidation.GetErrors().SelectMany(e => e.Value).Select(s => $"• {s}");
                var errorString = string.Join(Environment.NewLine, errorLines);
                return errorString;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack is not supported");
        }
    }
}