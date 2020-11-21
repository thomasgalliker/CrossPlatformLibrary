using System;
using System.Globalization;
using System.Linq;
using CrossPlatformLibrary.Forms.Validation;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Converters
{
    public class ValidationErrorsToStringConverter : BindableObject, IValueConverter
    {
        private const string DefaultBullet = "• ";

        public static readonly BindableProperty BulletProperty =
            BindableProperty.Create(
                nameof(Bullet),
                typeof(string),
                typeof(ValidationErrorsToStringConverter),
                defaultValue: DefaultBullet);

        public string Bullet
        {
            get => (string)this.GetValue(BulletProperty);
            set => this.SetValue(BulletProperty, value);
        }
        
        public static readonly BindableProperty ShowBulletsProperty =
            BindableProperty.Create(
                nameof(ShowBullets),
                typeof(bool),
                typeof(ValidationErrorsToStringConverter),
                defaultValue: true);

        public bool ShowBullets
        {
            get => (bool)this.GetValue(ShowBulletsProperty);
            set => this.SetValue(ShowBulletsProperty, value);
        }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ViewModelValidation viewModelValidation && viewModelValidation.HasErrors)
            {
                string bulletString = null;
                var showBullets = this.ShowBullets;
                if (showBullets)
                {
                    bulletString = this.Bullet;
                }

                var errorLines = viewModelValidation.GetErrors().SelectMany(e => e.Value).Select(s => $"{bulletString}{s}");
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