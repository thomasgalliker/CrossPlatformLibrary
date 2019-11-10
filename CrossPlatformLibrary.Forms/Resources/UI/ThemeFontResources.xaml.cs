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
            this[ThemeConstants.FontSizes.Micro] = fontConfiguration.FontSizes.Micro;
            this[ThemeConstants.FontSizes.Small] = fontConfiguration.FontSizes.Small;
            this[ThemeConstants.FontSizes.Medium] = fontConfiguration.FontSizes.Medium;
            this[ThemeConstants.FontSizes.Large] = fontConfiguration.FontSizes.Large;
        }

        private void SetFonts(IFontConfiguration fontConfiguration)
        {
            this[ThemeConstants.FontFamilies.Body1] = this.TryGetFontFamily(fontConfiguration.Body1);
            this[ThemeConstants.FontSizes.Body1] = this.TryGetFontSize(fontConfiguration.Body1);
            this[ThemeConstants.FontAttributes.Body1] = TryGetFontAttributes(fontConfiguration.Body1);

            this[ThemeConstants.FontFamilies.Body2] = this.TryGetFontFamily(fontConfiguration.Body2);
            this[ThemeConstants.FontSizes.Body2] = this.TryGetFontSize(fontConfiguration.Body2);
            this[ThemeConstants.FontAttributes.Body2] = TryGetFontAttributes(fontConfiguration.Body2);

            this[ThemeConstants.FontFamilies.Button] = this.TryGetFontFamily(fontConfiguration.Button);
            this[ThemeConstants.FontSizes.Button] = this.TryGetFontSize(fontConfiguration.Button);
            this[ThemeConstants.FontAttributes.Button] = TryGetFontAttributes(fontConfiguration.Button);

            this[ThemeConstants.FontFamilies.Caption] = this.TryGetFontFamily(fontConfiguration.Caption);
            this[ThemeConstants.FontSizes.Caption] = this.TryGetFontSize(fontConfiguration.Caption);
            this[ThemeConstants.FontAttributes.Caption] = TryGetFontAttributes(fontConfiguration.Caption);

            this[ThemeConstants.FontFamilies.H1] = this.TryGetFontFamily(fontConfiguration.H1);
            this[ThemeConstants.FontSizes.H1] = this.TryGetFontSize(fontConfiguration.H1);
            this[ThemeConstants.FontAttributes.H1] = TryGetFontAttributes(fontConfiguration.H1);

            this[ThemeConstants.FontFamilies.H2] = this.TryGetFontFamily(fontConfiguration.H2);
            this[ThemeConstants.FontSizes.H2] = this.TryGetFontSize(fontConfiguration.H2);
            this[ThemeConstants.FontAttributes.H2] = TryGetFontAttributes(fontConfiguration.H2);

            this[ThemeConstants.FontFamilies.H3] = this.TryGetFontFamily(fontConfiguration.H3);
            this[ThemeConstants.FontSizes.H3] = this.TryGetFontSize(fontConfiguration.H3);
            this[ThemeConstants.FontAttributes.H3] = TryGetFontAttributes(fontConfiguration.H3);

            this[ThemeConstants.FontFamilies.H4] = this.TryGetFontFamily(fontConfiguration.H4);
            this[ThemeConstants.FontSizes.H4] = this.TryGetFontSize(fontConfiguration.H4);
            this[ThemeConstants.FontAttributes.H4] = TryGetFontAttributes(fontConfiguration.H4);

            this[ThemeConstants.FontFamilies.H5] = this.TryGetFontFamily(fontConfiguration.H5);
            this[ThemeConstants.FontSizes.H5] = this.TryGetFontSize(fontConfiguration.H5);
            this[ThemeConstants.FontAttributes.H5] = TryGetFontAttributes(fontConfiguration.H5);

            this[ThemeConstants.FontFamilies.H6] = this.TryGetFontFamily(fontConfiguration.H6);
            this[ThemeConstants.FontSizes.H6] = this.TryGetFontSize(fontConfiguration.H6);
            this[ThemeConstants.FontAttributes.H6] = TryGetFontAttributes(fontConfiguration.H6);

            this[ThemeConstants.FontFamilies.Overline] = this.TryGetFontFamily(fontConfiguration.Overline);
            this[ThemeConstants.FontSizes.Overline] = this.TryGetFontSize(fontConfiguration.Overline);
            this[ThemeConstants.FontAttributes.Overline] = TryGetFontAttributes(fontConfiguration.Overline);

            this[ThemeConstants.FontFamilies.Subtitle1] = this.TryGetFontFamily(fontConfiguration.Subtitle1);
            this[ThemeConstants.FontSizes.Subtitle1] = this.TryGetFontSize(fontConfiguration.Subtitle1);
            this[ThemeConstants.FontAttributes.Subtitle1] = TryGetFontAttributes(fontConfiguration.Subtitle1);

            this[ThemeConstants.FontFamilies.Subtitle2] = this.TryGetFontFamily(fontConfiguration.Subtitle2);
            this[ThemeConstants.FontSizes.Subtitle2] = this.TryGetFontSize(fontConfiguration.Subtitle2);
            this[ThemeConstants.FontAttributes.Subtitle2] = TryGetFontAttributes(fontConfiguration.Subtitle2);
        }

        private string TryGetFontFamily(FontElement fontElement)
        {
            var fontFamily = fontElement?.FontFamily;
            if (fontFamily != null)
            {
                return fontFamily;
            }

            return PlatformHelper.GetValue<string>(this[ThemeConstants.FontFamilies.Default]);
        }

        private double? TryGetFontSize(FontElement fontElement)
        {
            var fontSize = fontElement?.FontSize;
            if (fontSize != null && fontSize > 0)
            {
                return fontElement.FontSize;
            }

            return (double)this[ThemeConstants.FontSizes.Medium];
        }

        private static FontAttributes TryGetFontAttributes(FontElement fontElement)
        {
            return fontElement?.FontAttributes ?? Font.Default.FontAttributes;
        }
    }
}