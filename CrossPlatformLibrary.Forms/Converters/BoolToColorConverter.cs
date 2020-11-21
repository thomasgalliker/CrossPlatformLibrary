using System;
using System.Globalization;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Converters
{
    internal class BoolToColorConverter : BindableObject, IValueConverter
    {
        public static readonly BindableProperty TrueValueProperty =
            BindableProperty.Create(
                nameof(TrueValue),
                typeof(Color),
                typeof(BoolToColorConverter),
                defaultValue: Color.Transparent);

        public Color TrueValue
        {
            get => (Color)this.GetValue(TrueValueProperty);
            set => this.SetValue(TrueValueProperty, value);
        }

        public static readonly BindableProperty FalseValueProperty =
            BindableProperty.Create(
                nameof(FalseValue),
                typeof(Color),
                typeof(BoolToColorConverter),
                defaultValue: Color.Transparent);

        public Color FalseValue
        {
            get => (Color)this.GetValue(FalseValueProperty);
            set => this.SetValue(FalseValueProperty, value);
        }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b && b == true)
            {
                return this.TrueValue;
            }

            return FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
