using System;
using CrossPlatformLibrary.Forms.Services;
using CrossPlatformLibrary.Forms.Themes;
using CrossPlatformLibrary.Forms.Themes.Extensions;
using CrossPlatformLibrary.Forms.Tools;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatformLibrary.Forms.Resources
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThemeFontResources : ResourceDictionary, IDisposable
    {
        private readonly IFontConfiguration fontConfiguration;
        private readonly IFontConverter fontConverter;

        internal ThemeFontResources(IFontConfiguration fontConfiguration, IFontConverter fontConverter)
        {
            this.InitializeComponent();

            this.fontConfiguration = fontConfiguration;
            this.fontConverter = fontConverter;
            this.fontConverter.FontScalingChanged += this.FontConverterUIContentSizeChanged;

            var fontSizes = this.fontConverter.GetScaledFontSizes(this.fontConfiguration.FontSizes);
            this.SetFontSizes(fontSizes);
            this.SetFonts(fontSizes);
        }

        private void FontConverterUIContentSizeChanged(object sender, EventArgs e)
        {
            var fontSizes = this.fontConverter.GetScaledFontSizes(this.fontConfiguration.FontSizes);
            this.SetFontSizes(fontSizes);
            this.SetFonts(fontSizes);
        }

        private void SetFontSizes(IFontSizeConfiguration fontSizes)
        {
            this[ThemeConstants.FontSize.Micro] = this.fontConverter.GetScaledFontSize(fontSizes.Micro);
            this[ThemeConstants.FontSize.XSmall] = this.fontConverter.GetScaledFontSize(fontSizes.XSmall);
            this[ThemeConstants.FontSize.Small] = this.fontConverter.GetScaledFontSize(fontSizes.Small);
            this[ThemeConstants.FontSize.MidMedium] = this.fontConverter.GetScaledFontSize(fontSizes.MidMedium);
            this[ThemeConstants.FontSize.Medium] = this.fontConverter.GetScaledFontSize(fontSizes.Medium);
            this[ThemeConstants.FontSize.Large] = this.fontConverter.GetScaledFontSize(fontSizes.Large);
            this[ThemeConstants.FontSize.XLarge] = this.fontConverter.GetScaledFontSize(fontSizes.XLarge);
            this[ThemeConstants.FontSize.XXLarge] = this.fontConverter.GetScaledFontSize(fontSizes.XXLarge);
            this[ThemeConstants.FontSize.XXXLarge] = this.fontConverter.GetScaledFontSize(fontSizes.XXXLarge);
        }

        private void SetFonts(IFontSizeConfiguration fontSizes)
        {
            var defaultFontFamily = TryGetFontFamily(this.fontConfiguration.Default, null);
            var defaultFontSize = TryGetFontSize(this.fontConfiguration.Default, 0d);
            var defaultFontAttributes = TryGetFontAttributes(this.fontConfiguration.Default);
            this[ThemeConstants.FontFamily.Default] = defaultFontFamily;
            this[ThemeConstants.FontSize.Default] = defaultFontSize;
            this[ThemeConstants.FontAttributes.Default] = defaultFontAttributes;

            this[ThemeConstants.FontFamily.Body1] = TryGetFontFamily(this.fontConfiguration.Body1, defaultFontFamily);
            this[ThemeConstants.FontSize.Body1] = TryGetFontSize(this.fontConfiguration.Body1, fontSizes.MidMedium);
            this[ThemeConstants.FontAttributes.Body1] = TryGetFontAttributes(this.fontConfiguration.Body1, defaultFontAttributes);

            this[ThemeConstants.FontFamily.Body2] = TryGetFontFamily(this.fontConfiguration.Body2, defaultFontFamily);
            this[ThemeConstants.FontSize.Body2] = TryGetFontSize(this.fontConfiguration.Body2, fontSizes.Small);
            this[ThemeConstants.FontAttributes.Body2] = TryGetFontAttributes(this.fontConfiguration.Body2, defaultFontAttributes);

            this[ThemeConstants.FontFamily.Button] = TryGetFontFamily(this.fontConfiguration.Button, defaultFontFamily);
            this[ThemeConstants.FontSize.Button] = TryGetFontSize(this.fontConfiguration.Button, fontSizes.Medium);
            this[ThemeConstants.FontAttributes.Button] = TryGetFontAttributes(this.fontConfiguration.Button, defaultFontAttributes);

            this[ThemeConstants.FontFamily.Input] = TryGetFontFamily(this.fontConfiguration.Input, defaultFontFamily);
            this[ThemeConstants.FontSize.Input] = TryGetFontSize(this.fontConfiguration.Input, fontSizes.Medium);
            this[ThemeConstants.FontAttributes.Input] = TryGetFontAttributes(this.fontConfiguration.Input, defaultFontAttributes);

            this[ThemeConstants.FontFamily.Caption] = TryGetFontFamily(this.fontConfiguration.Caption, defaultFontFamily);
            this[ThemeConstants.FontSize.Caption] = TryGetFontSize(this.fontConfiguration.Caption, fontSizes.XSmall);
            this[ThemeConstants.FontAttributes.Caption] = TryGetFontAttributes(this.fontConfiguration.Caption, defaultFontAttributes);

            this[ThemeConstants.FontFamily.H1] = TryGetFontFamily(this.fontConfiguration.H1, defaultFontFamily);
            this[ThemeConstants.FontSize.H1] = TryGetFontSize(this.fontConfiguration.H1, 96d);
            this[ThemeConstants.FontAttributes.H1] = TryGetFontAttributes(this.fontConfiguration.H1, defaultFontAttributes);

            this[ThemeConstants.FontFamily.H2] = TryGetFontFamily(this.fontConfiguration.H2, defaultFontFamily);
            this[ThemeConstants.FontSize.H2] = TryGetFontSize(this.fontConfiguration.H2, 60d);
            this[ThemeConstants.FontAttributes.H2] = TryGetFontAttributes(this.fontConfiguration.H2, defaultFontAttributes);

            this[ThemeConstants.FontFamily.H3] = TryGetFontFamily(this.fontConfiguration.H3, defaultFontFamily);
            this[ThemeConstants.FontSize.H3] = TryGetFontSize(this.fontConfiguration.H3, 48d);
            this[ThemeConstants.FontAttributes.H3] = TryGetFontAttributes(this.fontConfiguration.H3, defaultFontAttributes);

            this[ThemeConstants.FontFamily.H4] = TryGetFontFamily(this.fontConfiguration.H4, defaultFontFamily);
            this[ThemeConstants.FontSize.H4] = TryGetFontSize(this.fontConfiguration.H4, 34d);
            this[ThemeConstants.FontAttributes.H4] = TryGetFontAttributes(this.fontConfiguration.H4, defaultFontAttributes);

            this[ThemeConstants.FontFamily.H5] = TryGetFontFamily(this.fontConfiguration.H5, defaultFontFamily);
            this[ThemeConstants.FontSize.H5] = TryGetFontSize(this.fontConfiguration.H5, 24d);
            this[ThemeConstants.FontAttributes.H5] = TryGetFontAttributes(this.fontConfiguration.H5, defaultFontAttributes);

            this[ThemeConstants.FontFamily.H6] = TryGetFontFamily(this.fontConfiguration.H6, defaultFontFamily);
            this[ThemeConstants.FontSize.H6] = TryGetFontSize(this.fontConfiguration.H6, 20d);
            this[ThemeConstants.FontAttributes.H6] = TryGetFontAttributes(this.fontConfiguration.H6, defaultFontAttributes);

            this[ThemeConstants.FontFamily.Overline] = TryGetFontFamily(this.fontConfiguration.Overline, defaultFontFamily);
            this[ThemeConstants.FontSize.Overline] = TryGetFontSize(this.fontConfiguration.Overline, fontSizes.Micro);
            this[ThemeConstants.FontAttributes.Overline] = TryGetFontAttributes(this.fontConfiguration.Overline, defaultFontAttributes);

            this[ThemeConstants.FontFamily.Title] = TryGetFontFamily(this.fontConfiguration.Title, defaultFontFamily);
            this[ThemeConstants.FontSize.Title] = TryGetFontSize(this.fontConfiguration.Title, fontSizes.Medium);
            this[ThemeConstants.FontAttributes.Title] = TryGetFontAttributes(this.fontConfiguration.Title, FontAttributes.Bold);

            this[ThemeConstants.FontFamily.Subtitle1] = TryGetFontFamily(this.fontConfiguration.Subtitle1, defaultFontFamily);
            this[ThemeConstants.FontSize.Subtitle1] = TryGetFontSize(this.fontConfiguration.Subtitle1, fontSizes.MidMedium);
            this[ThemeConstants.FontAttributes.Subtitle1] = TryGetFontAttributes(this.fontConfiguration.Subtitle1, FontAttributes.Bold);

            this[ThemeConstants.FontFamily.Subtitle2] = TryGetFontFamily(this.fontConfiguration.Subtitle2, defaultFontFamily);
            this[ThemeConstants.FontSize.Subtitle2] = TryGetFontSize(this.fontConfiguration.Subtitle2, fontSizes.Small);
            this[ThemeConstants.FontAttributes.Subtitle2] = TryGetFontAttributes(this.fontConfiguration.Subtitle2, defaultFontAttributes);

            this[ThemeConstants.CardViewStyle.HeaderFontFamily] = TryGetFontFamily(this.fontConfiguration.SectionLabel, defaultFontFamily);
            this[ThemeConstants.CardViewStyle.HeaderFontSize] = TryGetFontSize(this.fontConfiguration.SectionLabel, PlatformHelper.OnPlatformValue((Device.iOS, () => 13d), (Device.Android, () => 18d)));
            this[ThemeConstants.CardViewStyle.HeaderFontAttributes] = TryGetFontAttributes(this.fontConfiguration.SectionLabel, defaultFontAttributes);

            this[ThemeConstants.CardViewStyle.FooterFontFamily] = TryGetFontFamily(this.fontConfiguration.FooterSection, defaultFontFamily);
            this[ThemeConstants.CardViewStyle.FooterFontSize] = TryGetFontSize(this.fontConfiguration.FooterSection, fontSizes.XSmall);
            this[ThemeConstants.CardViewStyle.FooterFontAttributes] = TryGetFontAttributes(this.fontConfiguration.FooterSection, defaultFontAttributes);
        }

        private static string TryGetFontFamily(FontElement fontElement, string @default)
        {
            var fontFamily = fontElement?.FontFamily;
            return fontFamily ?? @default;
        }

        private static double TryGetFontSize(FontElement fontElement, double @default)
        {
            var fontSize = fontElement?.FontSize;
            if (fontSize != null && fontSize > 0)
            {
                return fontElement.FontSize;
            }

            return @default;
        }

        private static FontAttributes TryGetFontAttributes(FontElement fontElement, FontAttributes @default = FontAttributes.None)
        {
            return fontElement?.FontAttributes ?? @default;
        }

        public void Dispose()
        {
            this.fontConverter?.Dispose();
        }
    }
}