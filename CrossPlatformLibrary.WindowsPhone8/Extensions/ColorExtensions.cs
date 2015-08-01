using CrossPlatformLibrary.Media;
using SystemWindowsMediaColor = System.Windows.Media.Color;

namespace CrossPlatformLibrary.Extensions
{
    public static class ColorExtensions
    {
        public static Color ToXamarinColor(this SystemWindowsMediaColor color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public static SystemWindowsMediaColor ToWindowsColor(this Color color)
        {
            return SystemWindowsMediaColor.FromArgb(color.A, color.R, color.G, color.B);
        }

    }
}