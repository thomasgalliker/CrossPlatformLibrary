using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Themes
{
    public sealed class SpacingConfiguration : BindableObject, ISpacingConfiguration
    {
        private const double SmallSpacingDefault = 4d;
        private const double MediumSpacingDefault = 8d;
        private const double LargeSpacingDefault = 16d;

        public static readonly BindableProperty SmallSpacingProperty = BindableProperty.Create(
            nameof(SmallSpacing),
            typeof(double),
            typeof(SpacingConfiguration),
            SmallSpacingDefault);

        public double SmallSpacing
        {
            get => (double)this.GetValue(SmallSpacingProperty);
            set => this.SetValue(SmallSpacingProperty, value);
        }

        public static readonly BindableProperty SmallPaddingProperty = BindableProperty.Create(
            nameof(SmallPadding),
            typeof(Thickness),
            typeof(SpacingConfiguration),
            new Thickness(SmallSpacingDefault));

        public Thickness SmallPadding
        {
            get => (Thickness)this.GetValue(SmallPaddingProperty);
            set => this.SetValue(SmallPaddingProperty, value);
        }

        public static readonly BindableProperty MediumSpacingProperty = BindableProperty.Create(
            nameof(MediumSpacing),
            typeof(double),
            typeof(SpacingConfiguration),
            MediumSpacingDefault);

        public double MediumSpacing
        {
            get => (double)this.GetValue(MediumSpacingProperty);
            set => this.SetValue(MediumSpacingProperty, value);
        }

        public static readonly BindableProperty MediumPaddingProperty = BindableProperty.Create(
            nameof(MediumPadding),
            typeof(Thickness),
            typeof(SpacingConfiguration),
            new Thickness(MediumSpacingDefault));

        public Thickness MediumPadding
        {
            get => (Thickness)this.GetValue(MediumPaddingProperty);
            set => this.SetValue(MediumPaddingProperty, value);
        }

        public static readonly BindableProperty LargeSpacingProperty = BindableProperty.Create(
            nameof(LargeSpacing),
            typeof(double),
            typeof(SpacingConfiguration),
            LargeSpacingDefault);

        public double LargeSpacing
        {
            get => (double)this.GetValue(LargeSpacingProperty);
            set => this.SetValue(LargeSpacingProperty, value);
        }

        public static readonly BindableProperty LargePaddingProperty = BindableProperty.Create(
            nameof(LargePadding),
            typeof(Thickness),
            typeof(SpacingConfiguration),
            new Thickness(LargeSpacingDefault));

        public Thickness LargePadding
        {
            get => (Thickness)this.GetValue(LargePaddingProperty);
            set => this.SetValue(LargePaddingProperty, value);
        }

        #region CardView Paddings

        public static readonly BindableProperty CardViewPaddingProperty = BindableProperty.Create(
            nameof(CardViewPadding),
            typeof(Thickness),
            typeof(SpacingConfiguration),
            GetDefaultCardViewPadding());

        private static Thickness GetDefaultCardViewPadding()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                return new Thickness(MediumSpacingDefault);
            }

            if (Device.RuntimePlatform == Device.iOS)
            {
                return new Thickness(LargeSpacingDefault, MediumSpacingDefault);
            }

            return new Thickness(0);
        }

        public Thickness CardViewPadding
        {
            get => (Thickness)this.GetValue(CardViewPaddingProperty);
            set => this.SetValue(CardViewPaddingProperty, value);
        }

        public static readonly BindableProperty CardPaddingProperty = BindableProperty.Create(
            nameof(CardPadding),
            typeof(Thickness),
            typeof(SpacingConfiguration),
            GetDefaultCardPadding());

        private static Thickness GetDefaultCardPadding()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                return new Thickness(MediumSpacingDefault, LargeSpacingDefault);
            }

            if (Device.RuntimePlatform == Device.iOS)
            {
                return new Thickness(0, 0, 0, LargeSpacingDefault);
            }

            return new Thickness(0);
        }

        public Thickness CardPadding
        {
            get => (Thickness)this.GetValue(CardPaddingProperty);
            set => this.SetValue(CardPaddingProperty, value);
        }

        public static readonly BindableProperty CardSpacingProperty = BindableProperty.Create(
            nameof(CardSpacing),
            typeof(double),
            typeof(SpacingConfiguration),
            GetDefaultCardSpacing());

        private static double GetDefaultCardSpacing()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                return LargeSpacingDefault;
            }

            if (Device.RuntimePlatform == Device.iOS)
            {
                return 0;
            }

            return 0;
        }
        public double CardSpacing
        {
            get => (double)this.GetValue(CardSpacingProperty);
            set => this.SetValue(CardSpacingProperty, value);
        }

        #endregion
    }
}