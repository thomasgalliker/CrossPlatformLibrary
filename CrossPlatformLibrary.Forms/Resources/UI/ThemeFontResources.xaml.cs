using System;
using CrossPlatformLibrary.Forms.Extensions;
using CrossPlatformLibrary.Forms.Themes;
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
            this[ThemeConstants.FontSize.Medium] = fontConfiguration.FontSizes.Medium;
            this[ThemeConstants.FontSize.Large] = fontConfiguration.FontSizes.Large;
            this[ThemeConstants.FontSize.XLarge] = fontConfiguration.FontSizes.XLarge;
            this[ThemeConstants.FontSize.XXLarge] = fontConfiguration.FontSizes.XXLarge;
        }

        private void SetFonts(IFontConfiguration fontConfiguration)
        {
            this[ThemeConstants.FontFamily.Default] = fontConfiguration.DefaultFontFamily;

            this[ThemeConstants.FontFamily.Body1] = TryGetFontFamily(fontConfiguration.Body1, fontConfiguration.DefaultFontFamily);
            this[ThemeConstants.FontSize.Body1] = TryGetFontSize(fontConfiguration.Body1, fontConfiguration.FontSizes.Medium);
            this[ThemeConstants.FontAttributes.Body1] = TryGetFontAttributes(fontConfiguration.Body1);

            this[ThemeConstants.FontFamily.Body2] = TryGetFontFamily(fontConfiguration.Body2, fontConfiguration.DefaultFontFamily);
            this[ThemeConstants.FontSize.Body2] = TryGetFontSize(fontConfiguration.Body2, fontConfiguration.FontSizes.Small);
            this[ThemeConstants.FontAttributes.Body2] = TryGetFontAttributes(fontConfiguration.Body2);

            this[ThemeConstants.FontFamily.Button] = TryGetFontFamily(fontConfiguration.Button, fontConfiguration.DefaultFontFamily);
            this[ThemeConstants.FontSize.Button] = TryGetFontSize(fontConfiguration.Button, fontConfiguration.FontSizes.Medium);
            this[ThemeConstants.FontAttributes.Button] = TryGetFontAttributes(fontConfiguration.Button);

            this[ThemeConstants.FontFamily.Caption] = TryGetFontFamily(fontConfiguration.Caption, fontConfiguration.DefaultFontFamily);
            this[ThemeConstants.FontSize.Caption] = TryGetFontSize(fontConfiguration.Caption, fontConfiguration.FontSizes.Micro);
            this[ThemeConstants.FontAttributes.Caption] = TryGetFontAttributes(fontConfiguration.Caption);

            this[ThemeConstants.FontFamily.H1] = TryGetFontFamily(fontConfiguration.H1, fontConfiguration.DefaultFontFamily);
            this[ThemeConstants.FontSize.H1] = TryGetFontSize(fontConfiguration.H1, 96d);
            this[ThemeConstants.FontAttributes.H1] = TryGetFontAttributes(fontConfiguration.H1);

            this[ThemeConstants.FontFamily.H2] = TryGetFontFamily(fontConfiguration.H2, fontConfiguration.DefaultFontFamily);
            this[ThemeConstants.FontSize.H2] = TryGetFontSize(fontConfiguration.H2, 60d);
            this[ThemeConstants.FontAttributes.H2] = TryGetFontAttributes(fontConfiguration.H2);

            this[ThemeConstants.FontFamily.H3] = TryGetFontFamily(fontConfiguration.H3, fontConfiguration.DefaultFontFamily);
            this[ThemeConstants.FontSize.H3] = TryGetFontSize(fontConfiguration.H3, 48d);
            this[ThemeConstants.FontAttributes.H3] = TryGetFontAttributes(fontConfiguration.H3);

            this[ThemeConstants.FontFamily.H4] = TryGetFontFamily(fontConfiguration.H4, fontConfiguration.DefaultFontFamily);
            this[ThemeConstants.FontSize.H4] = TryGetFontSize(fontConfiguration.H4, 34d);
            this[ThemeConstants.FontAttributes.H4] = TryGetFontAttributes(fontConfiguration.H4);

            this[ThemeConstants.FontFamily.H5] = TryGetFontFamily(fontConfiguration.H5, fontConfiguration.DefaultFontFamily);
            this[ThemeConstants.FontSize.H5] = TryGetFontSize(fontConfiguration.H5, 24d);
            this[ThemeConstants.FontAttributes.H5] = TryGetFontAttributes(fontConfiguration.H5);

            this[ThemeConstants.FontFamily.H6] = TryGetFontFamily(fontConfiguration.H6, fontConfiguration.DefaultFontFamily);
            this[ThemeConstants.FontSize.H6] = TryGetFontSize(fontConfiguration.H6, 20d);
            this[ThemeConstants.FontAttributes.H6] = TryGetFontAttributes(fontConfiguration.H6);

            this[ThemeConstants.FontFamily.Overline] = TryGetFontFamily(fontConfiguration.Overline, fontConfiguration.DefaultFontFamily);
            this[ThemeConstants.FontSize.Overline] = TryGetFontSize(fontConfiguration.Overline, fontConfiguration.FontSizes.Micro);
            this[ThemeConstants.FontAttributes.Overline] = TryGetFontAttributes(fontConfiguration.Overline);

            this[ThemeConstants.FontFamily.Subtitle1] = TryGetFontFamily(fontConfiguration.Subtitle1, fontConfiguration.DefaultFontFamily);
            this[ThemeConstants.FontSize.Subtitle1] = TryGetFontSize(fontConfiguration.Subtitle1, fontConfiguration.FontSizes.Medium);
            this[ThemeConstants.FontAttributes.Subtitle1] = TryGetFontAttributes(fontConfiguration.Subtitle1);

            this[ThemeConstants.FontFamily.Subtitle2] = TryGetFontFamily(fontConfiguration.Subtitle2, fontConfiguration.DefaultFontFamily);
            this[ThemeConstants.FontSize.Subtitle2] = TryGetFontSize(fontConfiguration.Subtitle2, fontConfiguration.FontSizes.Small);
            this[ThemeConstants.FontAttributes.Subtitle2] = TryGetFontAttributes(fontConfiguration.Subtitle2);
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

        private static FontAttributes TryGetFontAttributes(FontElement fontElement)
        {
            return fontElement?.FontAttributes ?? FontAttributes.None;
        }
    }
}