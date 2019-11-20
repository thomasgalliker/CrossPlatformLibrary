using System;
using System.Globalization;
using System.Linq;
using CrossPlatformLibrary.Extensions;
using ValueConverters;

namespace SampleApp.Converters
{
    internal class RandomValidationErrorsConverter : ConverterBase
    {
        private static readonly string[] ErrorMessages =
        {
            null,
            "Validation Error 1: Long lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam.",
            "Validation Error 2: Medium lorem ipsum dolor sit amet.",
            "Validation Error 3: Very short.",
        };

        protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var randomErrorMessages = ErrorMessages.Shuffle();
            var first = randomErrorMessages.First();
            if (first == null)
            {
                return Enumerable.Empty<string>();
            }

            return randomErrorMessages;
        }
    }
}