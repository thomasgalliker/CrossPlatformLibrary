using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CrossPlatformLibrary.Extensions;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Converters
{
    public class SegmentedItemsWrapperConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable enumerable)
            {
                var segmentedItems = new List<StatusSegment>();
                foreach (var item in enumerable)
                {
                    segmentedItems.Add(new StatusSegment(item));
                }

                var first = segmentedItems.FirstOrDefault();
                if (first != null)
                {
                    first.IsFirstElement = true;
                }

                var last = segmentedItems.LastOrDefault();
                if (last != null)
                {
                    last.IsLastElement = true;
                }

                return segmentedItems;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
