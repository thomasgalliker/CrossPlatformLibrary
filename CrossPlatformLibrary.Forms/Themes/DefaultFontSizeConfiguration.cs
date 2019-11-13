using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Themes
{
    public class DefaultFontSizeConfiguration : BindableObject, IFontSizeConfiguration
    {
        public static readonly BindableProperty MicroProperty =
            BindableProperty.Create(
                nameof(Micro),
                typeof(double),
                typeof(DefaultFontSizeConfiguration),
                defaultValue: 10.0d);

        [TypeConverter(typeof(FontSizeConverter))]
        public double Micro
        {
            get => (double)this.GetValue(MicroProperty);
            set => this.SetValue(MicroProperty, value);
        }

        public static readonly BindableProperty XSmallProperty =
            BindableProperty.Create(
                nameof(XSmall),
                typeof(double),
                typeof(DefaultFontSizeConfiguration),
                defaultValue: 12.0d);

        [TypeConverter(typeof(FontSizeConverter))]
        public double XSmall
        {
            get => (double)this.GetValue(XSmallProperty);
            set => this.SetValue(XSmallProperty, value);
        }

        public static readonly BindableProperty SmallProperty =
            BindableProperty.Create(
                nameof(Small),
                typeof(double),
                typeof(DefaultFontSizeConfiguration),
                defaultValue: 14.0d);

        [TypeConverter(typeof(FontSizeConverter))]
        public double Small
        {
            get => (double)this.GetValue(SmallProperty);
            set => this.SetValue(SmallProperty, value);
        }

        public static readonly BindableProperty MidMediumProperty =
            BindableProperty.Create(
                nameof(MidMedium),
                typeof(double),
                typeof(DefaultFontSizeConfiguration),
                defaultValue: 16.0d);

        [TypeConverter(typeof(FontSizeConverter))]
        public double MidMedium
        {
            get => (double)this.GetValue(MidMediumProperty);
            set => this.SetValue(MidMediumProperty, value);
        }

        public static readonly BindableProperty MediumProperty =
            BindableProperty.Create(
                nameof(Medium),
                typeof(double),
                typeof(DefaultFontSizeConfiguration),
                defaultValue: 18.0d);

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
                defaultValue: 22.0d);

        [TypeConverter(typeof(FontSizeConverter))]
        public double Large
        {
            get => (double)this.GetValue(LargeProperty);
            set => this.SetValue(LargeProperty, value);
        }

        public static readonly BindableProperty XLargeProperty =
            BindableProperty.Create(
                nameof(XLarge),
                typeof(double),
                typeof(DefaultFontSizeConfiguration),
                defaultValue: 26.0d);

        [TypeConverter(typeof(FontSizeConverter))]
        public double XLarge
        {
            get => (double)this.GetValue(XLargeProperty);
            set => this.SetValue(XLargeProperty, value);
        }

        public static readonly BindableProperty XXLargeProperty =
            BindableProperty.Create(
                nameof(XXLarge),
                typeof(double),
                typeof(DefaultFontSizeConfiguration),
                defaultValue: 30.0d);

        [TypeConverter(typeof(FontSizeConverter))]
        public double XXLarge
        {
            get => (double)this.GetValue(XXLargeProperty);
            set => this.SetValue(XXLargeProperty, value);
        }
    }
}