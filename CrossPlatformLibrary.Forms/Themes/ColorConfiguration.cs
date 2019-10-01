using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Themes
{
    /// <summary>
    ///     Class that provides color theme configuration based on https://material.io/design/color.
    /// </summary>
    public sealed class ColorConfiguration : BindableObject, IColorConfiguration
    {
        public Color SemiTransparentBright => Color.FromHex("#7FFFFFFF");

        public Color SemiTransparentDark => Color.FromHex("#7F000000");

        public static readonly BindableProperty BackgroundProperty = BindableProperty.Create(
            nameof(Background),
            typeof(Color),
            typeof(Color),
            Color.FromHex("#EAEAEA"));

        /// <summary>
        ///     The underlying color of an app’s content.
        ///     Typically the background color of scrollable content.
        /// </summary>
        public Color Background
        {
            get => (Color)this.GetValue(BackgroundProperty);
            set => this.SetValue(BackgroundProperty, value);
        }

        public static readonly BindableProperty ErrorProperty = BindableProperty.Create(
            nameof(Error),
            typeof(Color),
            typeof(Color),
            Color.FromHex("#B00020"));

        /// <summary>
        ///     The color used to indicate error status.
        /// </summary>
        public Color Error
        {
            get => (Color)this.GetValue(ErrorProperty);
            set => this.SetValue(ErrorProperty, value);
        }

        public static readonly BindableProperty OnBackgroundProperty = BindableProperty.Create(
            nameof(OnBackground),
            typeof(Color),
            typeof(Color),
            Color.FromHex("#000000"));

        /// <summary>
        ///     A color that passes accessibility guidelines for text/iconography when drawn on top of <see cref="Background" />.
        /// </summary>
        public Color OnBackground
        {
            get => (Color)this.GetValue(OnBackgroundProperty);
            set => this.SetValue(OnBackgroundProperty, value);
        }

        public static readonly BindableProperty OnErrorProperty = BindableProperty.Create(
            nameof(OnError),
            typeof(Color),
            typeof(Color),
            Color.FromHex("#FFFFFF"));

        /// <summary>
        ///     A color that passes accessibility guidelines for text/iconography when drawn on top of <see cref="Error" />.
        /// </summary>
        public Color OnError
        {
            get => (Color)this.GetValue(OnErrorProperty);
            set => this.SetValue(OnErrorProperty, value);
        }

        /// <summary>
        ///     Backing field for the bindable property <see cref="OnPrimary" />.
        /// </summary>
        public static readonly BindableProperty OnPrimaryProperty = BindableProperty.Create(nameof(OnPrimary), typeof(Color), typeof(Color), Color.FromHex("#FFFFFF"));

        /// <summary>
        ///     Backing field for the bindable property <see cref="OnSecondary" />.
        /// </summary>
        public static readonly BindableProperty OnSecondaryProperty = BindableProperty.Create(nameof(OnSecondary), typeof(Color), typeof(Color), Color.FromHex("#FFFFFF"));

        public static readonly BindableProperty OnSurfaceProperty = BindableProperty.Create(nameof(OnSurface), typeof(Color), typeof(Color), Color.FromHex("#000000"));

        public static readonly BindableProperty PrimaryProperty = BindableProperty.Create(
            nameof(Primary),
            typeof(Color),
            typeof(Color),
            Color.FromHex("#6200EE"));

        /// <summary>
        ///     Displayed most frequently across your app.
        /// </summary>
        public Color Primary
        {
            get => (Color)this.GetValue(PrimaryProperty);
            set => this.SetValue(PrimaryProperty, value);
        }

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            nameof(TextColor),
            typeof(Color),
            typeof(Color),
            Color.FromHex("#343434"));

        /// <summary>
        ///     Displayed most frequently across your app.
        /// </summary>
        public Color TextColor
        {
            get => (Color)this.GetValue(TextColorProperty);
            set => this.SetValue(TextColorProperty, value);
        }

        public static readonly BindableProperty TextColorBrightProperty = BindableProperty.Create(
            nameof(TextColorBright),
            typeof(Color),
            typeof(Color),
            Color.FromHex("#7E8184"));

        /// <summary>
        ///     Displayed most frequently across your app.
        /// </summary>
        public Color TextColorBright
        {
            get => (Color)this.GetValue(TextColorBrightProperty);
            set => this.SetValue(TextColorBrightProperty, value);
        }

        /// <summary>
        ///     Backing field for the bindable property <see cref="PrimaryVariant" />.
        /// </summary>
        public static readonly BindableProperty PrimaryVariantProperty = BindableProperty.Create(nameof(PrimaryVariant), typeof(Color), typeof(Color), Color.FromHex("#6200EE"));

        /// <summary>
        ///     Backing field for the bindable property <see cref="Secondary" />.
        /// </summary>
        public static readonly BindableProperty SecondaryProperty = BindableProperty.Create(nameof(Secondary), typeof(Color), typeof(Color), default(Color));

        /// <summary>
        ///     Backing field for the bindable property <see cref="SecondaryVariant" />.
        /// </summary>
        public static readonly BindableProperty SecondaryVariantProperty = BindableProperty.Create(nameof(SecondaryVariant), typeof(Color), typeof(Color), Color.FromHex("#0400BA"));

        /// <summary>
        ///     Backing field for the bindable property <see cref="Surface" />.
        /// </summary>
        public static readonly BindableProperty SurfaceProperty = BindableProperty.Create(nameof(Surface), typeof(Color), typeof(Color), Color.FromHex("#FFFFFF"));

        /// <summary>
        ///     A color that passes accessibility guidelines for text/iconography when drawn on top of <see cref="Primary" />.
        /// </summary>
        public Color OnPrimary
        {
            get => (Color)this.GetValue(OnPrimaryProperty);
            set => this.SetValue(OnPrimaryProperty, value);
        }

        /// <summary>
        ///     A color that passes accessibility guidelines for text/iconography when drawn on top of <see cref="Secondary" />.
        /// </summary>
        public Color OnSecondary
        {
            get
            {
                var color = (Color)this.GetValue(OnSecondaryProperty);

                return color.IsDefault ? this.OnPrimary : color;
            }

            set => this.SetValue(OnSecondaryProperty, value);
        }

        /// <summary>
        ///     A color that passes accessibility guidelines for text/iconography when drawn on top of <see cref="Surface" />
        /// </summary>
        public Color OnSurface
        {
            get => (Color)this.GetValue(OnSurfaceProperty);
            set => this.SetValue(OnSurfaceProperty, value);
        }

        /// <summary>
        ///     A tonal variation of <see cref="Primary" />.
        /// </summary>
        public Color PrimaryVariant
        {
            get => (Color)this.GetValue(PrimaryVariantProperty);
            set => this.SetValue(PrimaryVariantProperty, value);
        }

        /// <summary>
        ///     Accents select parts of your UI.
        ///     If not provided, use <see cref="Primary" />.
        /// </summary>
        public Color Secondary
        {
            get
            {
                var color = (Color)this.GetValue(SecondaryProperty);

                if (color.IsDefault && this.Primary.IsDefault)
                {
                    return Color.Accent;
                }

                return color.IsDefault ? this.Primary : color;
            }
            set => this.SetValue(SecondaryProperty, value);
        }

        /// <summary>
        ///     A tonal variation of <see cref="Secondary" />.
        /// </summary>
        public Color SecondaryVariant
        {
            get => (Color)this.GetValue(SecondaryVariantProperty);
            set => this.SetValue(SecondaryVariantProperty, value);
        }

        /// <summary>
        ///     The color of surfaces such as cards, sheets, menus.
        /// </summary>
        public Color Surface
        {
            get => (Color)this.GetValue(SurfaceProperty);
            set => this.SetValue(SurfaceProperty, value);
        }

        #region CardView Colors

        public static readonly BindableProperty CardViewDividerColorProperty = BindableProperty.Create(
            nameof(CardViewDividerColor),
            typeof(Color),
            typeof(Color),
            GetDefaultCardViewDividerColor());

        private static Color GetDefaultCardViewDividerColor()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                return Color.FromHex("#ECECEC");
            }

            return Color.FromHex("#C8C7CC");
        }

        public Color CardViewDividerColor
        {
            get => (Color)this.GetValue(CardViewDividerColorProperty);
            set => this.SetValue(CardViewDividerColorProperty, value);
        }

        public static readonly BindableProperty CardViewHeaderBackgroundColorProperty = BindableProperty.Create(
            nameof(CardViewHeaderBackgroundColor),
            typeof(Color),
            typeof(Color),
            GetDefaultCardViewHeaderBackgroundColor());

        private static Color GetDefaultCardViewHeaderBackgroundColor()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                return Color.White;
            }

            return Color.FromHex("#EFEFF4");
        }

        public Color CardViewHeaderBackgroundColor
        {
            get => (Color)this.GetValue(CardViewHeaderBackgroundColorProperty);
            set => this.SetValue(CardViewHeaderBackgroundColorProperty, value);
        }
        public static readonly BindableProperty CardViewBackgroundColorProperty = BindableProperty.Create(
            nameof(CardViewBackgroundColor),
            typeof(Color),
            typeof(Color),
            GetDefaultCardViewBackgroundColor());

        private static Color GetDefaultCardViewBackgroundColor()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                return Color.FromHex("#F5F5F5");
            }

            return Color.FromHex("#EFEFF4");
        }

        public Color CardViewBackgroundColor
        {
            get => (Color)this.GetValue(CardViewBackgroundColorProperty);
            set => this.SetValue(CardViewBackgroundColorProperty, value);
        }

        #endregion
    }
}