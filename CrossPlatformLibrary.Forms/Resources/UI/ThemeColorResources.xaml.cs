using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatformLibrary.Forms.Resources
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThemeColorResources : ResourceDictionary
    {
        internal ThemeColorResources(CrossPlatformLibraryColorConfiguration colorConfiguration)
        {
            this.InitializeComponent();

            this.SetThemeColors(colorConfiguration);
            this.SetButtonColors(colorConfiguration);
        }

        private void SetButtonColors(CrossPlatformLibraryColorConfiguration colorConfiguration)
        {
            this[ThemeConstants.CustomButtonPrimaryStyle.TextColor] = colorConfiguration.OnPrimary;
            this[ThemeConstants.CustomButtonPrimaryStyle.BorderColorEnabled] = colorConfiguration.Primary;
            this[ThemeConstants.CustomButtonPrimaryStyle.BorderColorDisabled] = colorConfiguration.PrimaryVariant;
            this[ThemeConstants.CustomButtonPrimaryStyle.BorderColorPressed] = colorConfiguration.Primary;
            this[ThemeConstants.CustomButtonPrimaryStyle.BackgroundColorEnabled] = colorConfiguration.Primary;
            this[ThemeConstants.CustomButtonPrimaryStyle.BackgroundColorDisabled] = colorConfiguration.PrimaryVariant;
            this[ThemeConstants.CustomButtonPrimaryStyle.BackgroundColorPressed] = colorConfiguration.Primary;

            this[ThemeConstants.CustomButtonSecondaryStyle.TextColor] = colorConfiguration.Secondary;
            this[ThemeConstants.CustomButtonSecondaryStyle.BorderColorEnabled] = colorConfiguration.Secondary;
            this[ThemeConstants.CustomButtonSecondaryStyle.BorderColorDisabled] = colorConfiguration.SecondaryVariant;
            this[ThemeConstants.CustomButtonSecondaryStyle.BorderColorPressed] = colorConfiguration.Secondary;
            this[ThemeConstants.CustomButtonSecondaryStyle.BackgroundColorEnabled] = colorConfiguration.OnSecondary;
            this[ThemeConstants.CustomButtonSecondaryStyle.BackgroundColorDisabled] = colorConfiguration.SecondaryVariant;
            this[ThemeConstants.CustomButtonSecondaryStyle.BackgroundColorPressed] = colorConfiguration.Secondary;
        }

        private void SetThemeColors(CrossPlatformLibraryColorConfiguration colorConfiguration)
        {
            this.TryAddColorResource(ThemeConstants.Color.TextColor, colorConfiguration.TextColor);
            this.TryAddColorResource(ThemeConstants.Color.TextColorBright, colorConfiguration.TextColorBright);
            this.TryAddColorResource(ThemeConstants.Color.PRIMARY, colorConfiguration.Primary);
            this.TryAddColorResource(ThemeConstants.Color.PRIMARY_VARIANT, colorConfiguration.PrimaryVariant);
            this.TryAddColorResource(ThemeConstants.Color.ON_PRIMARY, colorConfiguration.OnPrimary);
            this.TryAddColorResource(ThemeConstants.Color.SECONDARY, colorConfiguration.Secondary);
            this.TryAddColorResource(ThemeConstants.Color.SECONDARY_VARIANT, colorConfiguration.SecondaryVariant);
            this.TryAddColorResource(ThemeConstants.Color.ON_SECONDARY, colorConfiguration.OnSecondary);
            this.TryAddColorResource(ThemeConstants.Color.BACKGROUND, colorConfiguration.Background);
            this.TryAddColorResource(ThemeConstants.Color.ON_BACKGROUND, colorConfiguration.OnBackground);
            this.TryAddColorResource(ThemeConstants.Color.SURFACE, colorConfiguration.Surface);
            this.TryAddColorResource(ThemeConstants.Color.ON_SURFACE, colorConfiguration.OnSurface);
            this.TryAddColorResource(ThemeConstants.Color.ERROR, colorConfiguration.Error);
            this.TryAddColorResource(ThemeConstants.Color.ON_ERROR, colorConfiguration.OnError);
        }
    }
}