using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Themes
{
    public class DefaultFontSizeConfiguration : BindableObject, IFontSizeConfiguration
    {
        /*
         *
         *   <OnPlatform x:Key="LittleSize" x:TypeArguments="x:Double">
        <On Platform="iOS" Value="11.0"/>
        <On Platform="Android" Value="12.0"/>
    </OnPlatform>

    <OnPlatform x:Key="MidMediumSize" x:TypeArguments="x:Double">
        <On Platform="iOS" Value="12.0"/>
        <On Platform="Android" Value="14.0"/>
    </OnPlatform>

    <OnPlatform x:Key="MediumSize" x:TypeArguments="x:Double">
        <On Platform="iOS" Value="14.0"/>
        <On Platform="Android" Value="16.0"/>
    </OnPlatform>

    <OnPlatform x:Key="LargeSize" x:TypeArguments="x:Double">
        <On Platform="iOS" Value="16.0"/>
        <On Platform="Android" Value="18.0"/>
    </OnPlatform>

    <OnPlatform x:Key="LargerSize" x:TypeArguments="x:Double">
        <On Platform="iOS" Value="18.0"/>
        <On Platform="Android" Value="20.0"/>
    </OnPlatform>

    <OnPlatform x:Key="BigSize" x:TypeArguments="x:Double">
        <On Platform="iOS" Value="20.0"/>
        <On Platform="Android" Value="24.0"/>
    </OnPlatform>

    <OnPlatform x:Key="ExtraBigSize" x:TypeArguments="x:Double">
        <On Platform="iOS" Value="24.0"/>
        <On Platform="Android" Value="32.0"/>
    </OnPlatform>

    <OnPlatform x:Key="HugeSize" x:TypeArguments="x:Double">
        <On Platform="iOS" Value="32.0"/>
        <On Platform="Android" Value="48.0"/>
    </OnPlatform>
         *
         * */
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

        public static readonly BindableProperty XLargeProperty =
            BindableProperty.Create(
                nameof(XLarge),
                typeof(double),
                typeof(DefaultFontSizeConfiguration),
                GetDefaultXLarge());

        private static double GetDefaultXLarge()
        {
            var mediumSize = Device.GetNamedSize(NamedSize.Medium, typeof(DefaultFontSizeConfiguration));
            var largeSize = Device.GetNamedSize(NamedSize.Large, typeof(DefaultFontSizeConfiguration));

            var xLargeSize = largeSize + (largeSize - mediumSize);
            return xLargeSize;
        }

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
                GetDefaultXXLarge());

        private static double GetDefaultXXLarge()
        {
            var mediumSize = Device.GetNamedSize(NamedSize.Medium, typeof(DefaultFontSizeConfiguration));
            var largeSize = Device.GetNamedSize(NamedSize.Large, typeof(DefaultFontSizeConfiguration));

            var xLargeSize = largeSize + 2 * (largeSize - mediumSize);
            return xLargeSize;
        }

        [TypeConverter(typeof(FontSizeConverter))]
        public double XXLarge
        {
            get => (double)this.GetValue(XXLargeProperty);
            set => this.SetValue(XXLargeProperty, value);
        }
    }
}