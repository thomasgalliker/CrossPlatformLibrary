using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CrossPlatformLibrary.Forms.Validation.Converters
{
    /// <summary>
    /// Reads validation errors and returns the string message of the first validation error.
    /// </summary>
    public class FirstValidationErrorFilterConverter : ValidationErrorsFilterConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (base.Convert(value, targetType, parameter, culture) is IEnumerable<string> propertyErrors)
            {
                var firstPropertyError = propertyErrors.FirstOrDefault();
                return firstPropertyError;
            }

            return null;
        }
    }
}