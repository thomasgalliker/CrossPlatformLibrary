﻿using CrossPlatformLibrary.Forms.Themes;
using CrossPlatformLibrary.Forms.Themes.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatformLibrary.Forms.Resources
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThemeColorResources : ResourceDictionary
    {
        internal ThemeColorResources(IColorConfiguration colorConfiguration)
        {
            this.InitializeComponent();

            this.SetThemeColors(colorConfiguration);
            this.SetButtonColors(colorConfiguration);
            this.SetDrilldownButtonColors(colorConfiguration);
            this.SetCardViewColors(colorConfiguration);
        }

        private void SetButtonColors(IColorConfiguration colorConfiguration)
        {
            this[ThemeConstants.CustomButtonStyle.TextColor] = colorConfiguration.TextColor;
            this[ThemeConstants.CustomButtonStyle.BorderColorEnabled] = colorConfiguration.TextColor;
            this[ThemeConstants.CustomButtonStyle.BorderColorDisabled] = Color.DarkGray;
            this[ThemeConstants.CustomButtonStyle.BorderColorPressed] = colorConfiguration.TextColor;
            this[ThemeConstants.CustomButtonStyle.BackgroundColorEnabled] = Color.White;
            this[ThemeConstants.CustomButtonStyle.BackgroundColorDisabled] = colorConfiguration.TextColorBright;
            this[ThemeConstants.CustomButtonStyle.BackgroundColorPressed] = colorConfiguration.TextColorBright;

            this[ThemeConstants.CustomButtonPrimaryStyle.TextColor] = colorConfiguration.OnPrimary;
            this[ThemeConstants.CustomButtonPrimaryStyle.BorderColorEnabled] = colorConfiguration.Primary;
            this[ThemeConstants.CustomButtonPrimaryStyle.BorderColorDisabled] = colorConfiguration.PrimaryVariant;
            this[ThemeConstants.CustomButtonPrimaryStyle.BorderColorPressed] = colorConfiguration.Primary;
            this[ThemeConstants.CustomButtonPrimaryStyle.BackgroundColorEnabled] = colorConfiguration.Primary;
            this[ThemeConstants.CustomButtonPrimaryStyle.BackgroundColorDisabled] = colorConfiguration.PrimaryVariant;
            this[ThemeConstants.CustomButtonPrimaryStyle.BackgroundColorPressed] = colorConfiguration.PrimaryVariant;

            this[ThemeConstants.CustomButtonSecondaryStyle.TextColor] = colorConfiguration.Secondary;
            this[ThemeConstants.CustomButtonSecondaryStyle.BorderColorEnabled] = colorConfiguration.Secondary;
            this[ThemeConstants.CustomButtonSecondaryStyle.BorderColorDisabled] = colorConfiguration.SecondaryVariant;
            this[ThemeConstants.CustomButtonSecondaryStyle.BorderColorPressed] = colorConfiguration.Secondary;
            this[ThemeConstants.CustomButtonSecondaryStyle.BackgroundColorEnabled] = colorConfiguration.OnSecondary;
            this[ThemeConstants.CustomButtonSecondaryStyle.BackgroundColorDisabled] = colorConfiguration.SecondaryVariant;
            this[ThemeConstants.CustomButtonSecondaryStyle.BackgroundColorPressed] = colorConfiguration.SecondaryVariant;
        }

        private void SetDrilldownButtonColors(IColorConfiguration colorConfiguration)
        {
            this[ThemeConstants.DrilldownButtonStyle.TextColor] = colorConfiguration.TextColor;
            this[ThemeConstants.DrilldownButtonStyle.BorderColorEnabled] = Color.Transparent;
            this[ThemeConstants.DrilldownButtonStyle.BorderColorDisabled] = Color.Transparent;
            this[ThemeConstants.DrilldownButtonStyle.BorderColorPressed] = Color.Transparent;
            this[ThemeConstants.DrilldownButtonStyle.BackgroundColorEnabled] = Color.Transparent;
            this[ThemeConstants.DrilldownButtonStyle.BackgroundColorDisabled] = colorConfiguration.SemiTransparentBright;
            this[ThemeConstants.DrilldownButtonStyle.BackgroundColorPressed] = colorConfiguration.SemiTransparentBright;
        }

        private void SetCardViewColors(IColorConfiguration colorConfiguration)
        {
            this[ThemeConstants.CardViewStyle.TextColor] = Color.FromHex("#6D6D72");
            this[ThemeConstants.CardViewStyle.BackgroundColor] = colorConfiguration.CardViewBackgroundColor;
            this[ThemeConstants.CardViewStyle.HeaderBackgroundColor] = colorConfiguration.CardViewHeaderBackgroundColor;
            this[ThemeConstants.CardViewStyle.HeaderDividerColor] = colorConfiguration.CardViewDividerColor;
            this[ThemeConstants.CardViewStyle.FooterDividerColor] = colorConfiguration.CardViewDividerColor;

        }

        private void SetThemeColors(IColorConfiguration colorConfiguration)
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
            this.TryAddColorResource(ThemeConstants.Color.ErrorBackground, colorConfiguration.ErrorBackground);
        }
    }
}