using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Validation.Converters
{
    /// <summary>
    /// Reads validation errors and returns the string messages of all errors.
    /// </summary>
    public class ValidationErrorsFilterConverter : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ViewModelValidation validation)
            {
                var parameterString = parameter as string;
                if (!string.IsNullOrEmpty(parameterString))
                {
                    var properties = parameterString.Split('|');

                    var propertyErrors = properties.SelectMany(p => validation.Errors[p]).ToList();
                    return propertyErrors;
                }

                throw new InvalidOperationException("ConverterParameter must a pipe-delimited string");
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack is not supported");
        }
    }
}