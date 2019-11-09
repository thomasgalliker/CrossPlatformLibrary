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

            this.SetFontFamilies(fontConfiguration);
            this.SetFontSizes(fontConfiguration);
            this.SetFontAttributes(fontConfiguration);
        }

        private void SetFontFamilies(IFontConfiguration fontConfiguration)
        {
            this[ThemeConstants.FontFamily.Default] = TryGetFontFamily(fontConfiguration.Default);
            this[ThemeConstants.FontFamily.Body] = TryGetFontFamily(fontConfiguration.Default);
            this[ThemeConstants.FontFamily.Title] = TryGetFontFamily(fontConfiguration.Title);
            this[ThemeConstants.FontFamily.Button] = TryGetFontFamily(fontConfiguration.Button);
            this[ThemeConstants.FontFamily.Entry] = TryGetFontFamily(fontConfiguration.Default);
        }

        private static string TryGetFontFamily(FontElement fontElement)
        {
            return fontElement?.FontFamily ?? Font.Default.FontFamily;
        }

        private void SetFontSizes(IFontConfiguration fontConfiguration)
        {
            this[ThemeConstants.FontSize.Default] = TryGetFontSize(fontConfiguration.Default);
            this[ThemeConstants.FontSize.Body] = TryGetFontSize(fontConfiguration.Default);
            this[ThemeConstants.FontSize.Title] = TryGetFontSize(fontConfiguration.Title);
            this[ThemeConstants.FontSize.Button] = TryGetFontSize(fontConfiguration.Button);
            this[ThemeConstants.FontSize.Entry] = TryGetFontSize(fontConfiguration.Default);
        }

        private static double? TryGetFontSize(FontElement fontElement)
        {
            return fontElement?.FontSize ?? Font.Default.FontSize;
        }

        private void SetFontAttributes(IFontConfiguration fontConfiguration)
        {
            this[ThemeConstants.FontAttributes.Default] = TryGetFontAttributes(fontConfiguration.Default);
            this[ThemeConstants.FontAttributes.Body] = TryGetFontAttributes(fontConfiguration.Default);
            this[ThemeConstants.FontAttributes.Title] = TryGetFontAttributes(fontConfiguration.Title);
            this[ThemeConstants.FontAttributes.Button] = TryGetFontAttributes(fontConfiguration.Button);
            this[ThemeConstants.FontAttributes.Entry] = TryGetFontAttributes(fontConfiguration.Default);
        }

        private static FontAttributes TryGetFontAttributes(FontElement fontElement)
        {
            return fontElement?.FontAttributes ?? Font.Default.FontAttributes;
        }
    }
}