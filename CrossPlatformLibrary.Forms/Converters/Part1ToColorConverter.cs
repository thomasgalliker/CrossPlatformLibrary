using System;
using System.Globalization;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Converters
{
    internal class Part1ToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is StatusSegment statusSegment)
            {
                if (statusSegment.IsFirstElement)
                {
                    return Color.Transparent;
                }
                else if (statusSegment.IsMiddleElement || (statusSegment.IsEndElement && !statusSegment.IsStartElement))
                {
                    return Color.Red;
                }

                return Color.LightSlateGray;
            }

            return Color.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    internal class Part2ToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is StatusSegment statusSegment)
            {
                if (statusSegment.IsStartElement || statusSegment.IsMiddleElement || statusSegment.IsEndElement)
                {
                    return Color.Red;
                }

                return Color.LightSlateGray;
            }

            return Color.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    internal class Part3ToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is StatusSegment statusSegment)
            {
                if (statusSegment.IsLastElement)
                {
                    return Color.Transparent;
                }
                else if ((statusSegment.IsStartElement && !statusSegment.IsEndElement) || statusSegment.IsMiddleElement)
                {
                    return Color.Red;
                }

                return Color.LightSlateGray;
            }

            return Color.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
