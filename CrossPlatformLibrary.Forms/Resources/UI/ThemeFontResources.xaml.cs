using System;
using CrossPlatformLibrary.Forms.Extensions;
using CrossPlatformLibrary.Forms.Themes;
using CrossPlatformLibrary.Forms.Tools;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatformLibrary.Forms.Resources
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThemeFontResources : ResourceDictionary
    {
        public ThemeFontResources(IFontConfiguration fontConfiguration)
        {
            this.InitializeComponent();

            this.SetFontSizes(fontConfiguration);
            this.SetFonts(fontConfiguration);
        }

        private void SetFontSizes(IFontConfiguration fontConfiguration)
        {
            this[ThemeConstants.FontSize.Micro] = fontConfiguration.FontSizes.Micro;
            this[ThemeConstants.FontSize.Small] = fontConfiguration.FontSizes.Small;
            this[ThemeConstants.FontSize.MidMedium] = fontConfiguration.FontSizes.MidMedium;
            this[ThemeConstants.FontSize.Medium] = fontConfiguration.FontSizes.Medium;
            this[ThemeConstants.FontSize.Large] = fontConfiguration.FontSizes.Large;
            this[ThemeConstants.FontSize.XLarge] = fontConfiguration.FontSizes.XLarge;
            this[ThemeConstants.FontSize.XXLarge] = fontConfiguration.FontSizes.XXLarge;
        }

        private void SetFonts(IFontConfiguration fontConfiguration)
        {
            var defaultFontFamily = TryGetFontFamily(fontConfiguration.Default, null);
            var defaultFontSize = TryGetFontSize(fontConfiguration.Default, 0d);
            var defaultFontAttributes = TryGetFontAttributes(fontConfiguration.Default);
            this[ThemeConstants.FontFamily.Default] = defaultFontFamily;
            this[ThemeConstants.FontSize.Default] = defaultFontSize;
            this[ThemeConstants.FontAttributes.Default] = defaultFontAttributes;

            this[ThemeConstants.FontFamily.Body1] = TryGetFontFamily(fontConfiguration.Body1, defaultFontFamily);
            this[ThemeConstants.FontSize.Body1] = TryGetFontSize(fontConfiguration.Body1, fontConfiguration.FontSizes.MidMedium);
            this[ThemeConstants.FontAttributes.Body1] = TryGetFontAttributes(fontConfiguration.Body1, defaultFontAttributes);

            this[ThemeConstants.FontFamily.Body2] = TryGetFontFamily(fontConfiguration.Body2, defaultFontFamily);
            this[ThemeConstants.FontSize.Body2] = TryGetFontSize(fontConfiguration.Body2, fontConfiguration.FontSizes.Small);
            this[ThemeConstants.FontAttributes.Body2] = TryGetFontAttributes(fontConfiguration.Body2, defaultFontAttributes);

            this[ThemeConstants.FontFamily.Button] = TryGetFontFamily(fontConfiguration.Button, defaultFontFamily);
            this[ThemeConstants.FontSize.Button] = TryGetFontSize(fontConfiguration.Button, fontConfiguration.FontSizes.Medium);
            this[ThemeConstants.FontAttributes.Button] = TryGetFontAttributes(fontConfiguration.Button, defaultFontAttributes);
            
            this[ThemeConstants.FontFamily.Input] = TryGetFontFamily(fontConfiguration.Input, defaultFontFamily);
            this[ThemeConstants.FontSize.Input] = TryGetFontSize(fontConfiguration.Input, fontConfiguration.FontSizes.Medium);
            this[ThemeConstants.FontAttributes.Input] = TryGetFontAttributes(fontConfiguration.Input, defaultFontAttributes);

            this[ThemeConstants.FontFamily.Caption] = TryGetFontFamily(fontConfiguration.Caption, defaultFontFamily);
            this[ThemeConstants.FontSize.Caption] = TryGetFontSize(fontConfiguration.Caption, fontConfiguration.FontSizes.XSmall);
            this[ThemeConstants.FontAttributes.Caption] = TryGetFontAttributes(fontConfiguration.Caption, defaultFontAttributes);

            this[ThemeConstants.FontFamily.H1] = TryGetFontFamily(fontConfiguration.H1, defaultFontFamily);
            this[ThemeConstants.FontSize.H1] = TryGetFontSize(fontConfiguration.H1, 96d);
            this[ThemeConstants.FontAttributes.H1] = TryGetFontAttributes(fontConfiguration.H1, defaultFontAttributes);

            this[ThemeConstants.FontFamily.H2] = TryGetFontFamily(fontConfiguration.H2, defaultFontFamily);
            this[ThemeConstants.FontSize.H2] = TryGetFontSize(fontConfiguration.H2, 60d);
            this[ThemeConstants.FontAttributes.H2] = TryGetFontAttributes(fontConfiguration.H2, defaultFontAttributes);

            this[ThemeConstants.FontFamily.H3] = TryGetFontFamily(fontConfiguration.H3, defaultFontFamily);
            this[ThemeConstants.FontSize.H3] = TryGetFontSize(fontConfiguration.H3, 48d);
            this[ThemeConstants.FontAttributes.H3] = TryGetFontAttributes(fontConfiguration.H3, defaultFontAttributes);

            this[ThemeConstants.FontFamily.H4] = TryGetFontFamily(fontConfiguration.H4, defaultFontFamily);
            this[ThemeConstants.FontSize.H4] = TryGetFontSize(fontConfiguration.H4, 34d);
            this[ThemeConstants.FontAttributes.H4] = TryGetFontAttributes(fontConfiguration.H4, defaultFontAttributes);

            this[ThemeConstants.FontFamily.H5] = TryGetFontFamily(fontConfiguration.H5, defaultFontFamily);
            this[ThemeConstants.FontSize.H5] = TryGetFontSize(fontConfiguration.H5, 24d);
            this[ThemeConstants.FontAttributes.H5] = TryGetFontAttributes(fontConfiguration.H5, defaultFontAttributes);

            this[ThemeConstants.FontFamily.H6] = TryGetFontFamily(fontConfiguration.H6, defaultFontFamily);
            this[ThemeConstants.FontSize.H6] = TryGetFontSize(fontConfiguration.H6, 20d);
            this[ThemeConstants.FontAttributes.H6] = TryGetFontAttributes(fontConfiguration.H6, defaultFontAttributes);

            this[ThemeConstants.FontFamily.Overline] = TryGetFontFamily(fontConfiguration.Overline, defaultFontFamily);
            this[ThemeConstants.FontSize.Overline] = TryGetFontSize(fontConfiguration.Overline, fontConfiguration.FontSizes.Micro);
            this[ThemeConstants.FontAttributes.Overline] = TryGetFontAttributes(fontConfiguration.Overline, defaultFontAttributes);

            this[ThemeConstants.FontFamily.Title] = TryGetFontFamily(fontConfiguration.Title, defaultFontFamily);
            this[ThemeConstants.FontSize.Title] = TryGetFontSize(fontConfiguration.Title, fontConfiguration.FontSizes.Medium);
            this[ThemeConstants.FontAttributes.Title] = TryGetFontAttributes(fontConfiguration.Title, FontAttributes.Bold);
            
            this[ThemeConstants.FontFamily.Subtitle1] = TryGetFontFamily(fontConfiguration.Subtitle1, defaultFontFamily);
            this[ThemeConstants.FontSize.Subtitle1] = TryGetFontSize(fontConfiguration.Subtitle1, fontConfiguration.FontSizes.MidMedium);
            this[ThemeConstants.FontAttributes.Subtitle1] = TryGetFontAttributes(fontConfiguration.Subtitle1, FontAttributes.Bold);

            this[ThemeConstants.FontFamily.Subtitle2] = TryGetFontFamily(fontConfiguration.Subtitle2, defaultFontFamily);
            this[ThemeConstants.FontSize.Subtitle2] = TryGetFontSize(fontConfiguration.Subtitle2, fontConfiguration.FontSizes.Small);
            this[ThemeConstants.FontAttributes.Subtitle2] = TryGetFontAttributes(fontConfiguration.Subtitle2, defaultFontAttributes);

            this[ThemeConstants.CardViewStyle.HeaderFontFamily] = TryGetFontFamily(fontConfiguration.SectionLabel, defaultFontFamily);
            this[ThemeConstants.CardViewStyle.HeaderFontSize] = TryGetFontSize(fontConfiguration.SectionLabel, PlatformHelper.OnPlatformValue((Device.iOS, () => 13d), (Device.Android, () => 18d)));
            this[ThemeConstants.CardViewStyle.HeaderFontAttributes] = TryGetFontAttributes(fontConfiguration.SectionLabel, defaultFontAttributes);
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
    }
}