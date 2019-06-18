using System;
using System.Globalization;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Converters
{
    public class NullableDateTimeToFormatConverter : BindableObject, IValueConverter
    {
        public static readonly BindableProperty DateFormatProperty =
            BindableProperty.Create(
                nameof(DateFormat),
                typeof(string),
                typeof(NullableDateTimeToFormatConverter),
                defaultValue: null);

        public static readonly BindableProperty TimeFormatProperty =
            BindableProperty.Create(
                nameof(TimeFormat),
                typeof(string),
                typeof(NullableDateTimeToFormatConverter),
                defaultValue: null);

        public static readonly BindableProperty NullableFormatProperty =
            BindableProperty.Create(
                nameof(NullableFormat),
                typeof(string),
                typeof(NullableDateTimeToFormatConverter),
                "");

        public string DateFormat
        {
            get
            {
                return (string)this.GetValue(DateFormatProperty);
            }
            set
            {
                this.SetValue(DateFormatProperty, value);
            }
        }

        public string TimeFormat
        {
            get
            {
                return (string)this.GetValue(TimeFormatProperty);
            }
            set
            {
                this.SetValue(TimeFormatProperty, value);
            }
        }

        public string NullableFormat
        {
            get
            {
                return (string)this.GetValue(NullableFormatProperty);
            }
            set
            {
                this.SetValue(NullableFormatProperty, value);
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dateTime = value as DateTime?;
            if (dateTime != null)
            {
                return this.DateFormat ?? CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern;
            }

            var timeSpan = value as TimeSpan?;
            if (timeSpan != null)
            {
                return this.TimeFormat ?? CultureInfo.CurrentUICulture.DateTimeFormat.ShortTimePattern;
            }

            return this.NullableFormat;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}