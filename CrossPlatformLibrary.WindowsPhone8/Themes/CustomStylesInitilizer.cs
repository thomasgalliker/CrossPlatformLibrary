using System.Windows;
using System.Windows.Media;

using CrossPlatformLibrary.Extensions;
using CrossPlatformLibrary.Media;

namespace CrossPlatformLibrary.Themes
{
    public static class CustomStylesInitilizer
    {
        public static void OverrideStyles(this Application currentApplication)
        {
            var phoneAccentBrushBrighter = currentApplication.Resources["PhoneAccentBrushBrighter"] as SolidColorBrush;
            if (phoneAccentBrushBrighter != null)
            {
                phoneAccentBrushBrighter.Color = phoneAccentBrushBrighter.Color.ToXamarinColor().ChangeSaturation(0.5d).ToWindowsColor();
                phoneAccentBrushBrighter.Color = phoneAccentBrushBrighter.Color.ToXamarinColor().ChangeBrightness(255).ToWindowsColor();
            }

            var phoneAccentBrushBright = currentApplication.Resources["PhoneAccentBrushBright"] as SolidColorBrush;
            if (phoneAccentBrushBright != null)
            {
                phoneAccentBrushBright.Color = phoneAccentBrushBright.Color.ToXamarinColor().ChangeSaturation(0.2d).ToWindowsColor();
                phoneAccentBrushBright.Color = phoneAccentBrushBright.Color.ToXamarinColor().ChangeBrightness(191).ToWindowsColor();
            }

            var phoneAccentBrushDark = currentApplication.Resources["PhoneAccentBrushDark"] as SolidColorBrush;
            if (phoneAccentBrushDark != null)
            {
                phoneAccentBrushDark.Color = phoneAccentBrushDark.Color.ToXamarinColor().ChangeSaturation(0.8d).ToWindowsColor();
                phoneAccentBrushDark.Color = phoneAccentBrushDark.Color.ToXamarinColor().ChangeBrightness(127).ToWindowsColor();
            }

            var phoneAccentBrushDarker = currentApplication.Resources["PhoneAccentBrushDarker"] as SolidColorBrush;
            if (phoneAccentBrushDarker != null)
            {
                phoneAccentBrushDarker.Color = phoneAccentBrushDarker.Color.ToXamarinColor().ChangeSaturation(0.8d).ToWindowsColor();
                phoneAccentBrushDarker.Color = phoneAccentBrushDarker.Color.ToXamarinColor().ChangeBrightness(63).ToWindowsColor();
            }
        }
    }
}
