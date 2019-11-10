using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Themes
{
    public class DefaultFontSizeConfiguration : BindableObject, IFontSizeConfiguration
    {
        public DefaultFontSizeConfiguration()
        {
          
        }

        public static readonly BindableProperty MicroProperty =
            BindableProperty.Create(
                nameof(Micro),
                typeof(double),
                typeof(DefaultFontSizeConfiguration),
                Device.GetNamedSize(NamedSize.Micro, typeof(DefaultFontSizeConfiguration)));

        [TypeConverter(typeof(FontSizeConverter))]
        public double Micro
        {
            get => (double)this.GetValue(MicroProperty);
            set => this.SetValue(MicroProperty, value);
        }

        public static readonly BindableProperty SmallProperty =
            BindableProperty.Create(
                nameof(Small),
                typeof(double),
                typeof(DefaultFontSizeConfiguration),
                Device.GetNamedSize(NamedSize.Small, typeof(DefaultFontSizeConfiguration)));

        [TypeConverter(typeof(FontSizeConverter))]
        public double Small
        {
            get => (double)this.GetValue(SmallProperty);
            set => this.SetValue(SmallProperty, value);
        }

        public static readonly BindableProperty MediumProperty =
            BindableProperty.Create(
                nameof(Medium),
                typeof(double),
                typeof(DefaultFontSizeConfiguration),
                Device.GetNamedSize(NamedSize.Medium, typeof(DefaultFontSizeConfiguration)));

        [TypeConverter(typeof(FontSizeConverter))]
        public double Medium
        {
            get => (double)this.GetValue(MediumProperty);
            set => this.SetValue(MediumProperty, value);
        }

        public static readonly BindableProperty LargeProperty =
            BindableProperty.Create(
                nameof(Large),
                typeof(double),
                typeof(DefaultFontSizeConfiguration),
                Device.GetNamedSize(NamedSize.Large, typeof(DefaultFontSizeConfiguration)));

        [TypeConverter(typeof(FontSizeConverter))]
        public double Large
        {
            get => (double)this.GetValue(LargeProperty);
            set => this.SetValue(LargeProperty, value);
        }
    }
}