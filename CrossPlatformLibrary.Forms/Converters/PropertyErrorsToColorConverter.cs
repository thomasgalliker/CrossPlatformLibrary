using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Converters
{
    internal class PropertyErrorsToColorConverter : BindableObject, IValueConverter
    {
        public static readonly BindableProperty NormalColorProperty = BindableProperty.Create(
            nameof(NormalColor),
            typeof(Color),
            typeof(PropertyErrorsToColorConverter),
            default(Color));

        public static readonly BindableProperty ErrorColorProperty = BindableProperty.Create(
            nameof(ErrorColor),
            typeof(Color),
            typeof(PropertyErrorsToColorConverter),
            default(Color));

        public Color NormalColor
        {
            get { return (Color)this.GetValue(NormalColorProperty); }
            set { this.SetValue(NormalColorProperty, value); }
        }

        public Color ErrorColor
        {
            get { return (Color)this.GetValue(ErrorColorProperty); }
            set { this.SetValue(ErrorColorProperty, value); }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var propertyErrors = (ReadOnlyCollection<string>)value;
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
