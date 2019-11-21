using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Extensions
{
    public static class ColorExtensions
    {
        public static Color Complement(this Color color)
        {
            var hue = (color.Hue * 359.0);
            var newHue = ((hue + 180) % 359.0);
            var complement = color.WithHue(newHue / 359.0);

            return complement;
        }

        /// <summary>
        /// Inverts the R-G-B parts of <paramref name="color"/> with <paramref name="alpha"/>.
        /// </summary>
        /// <param name="color">The input color.</param>
        /// <param name="alpha">The alpha value of the returned color. If null, the alpha value of <paramref name="color"/> is used.</param>
        /// <returns>An inverted color.</returns>
        public static Color Invert(this Color color, double? alpha)
        {
            var r = 255 - (int)(255 * color.R);
            var g = 255 - (int)(255 * color.G);
            var b = 255 - (int)(255 * color.B);
            var a = alpha ?? color.A;

            return Color.FromRgba(r, g, b, a);
        }

        /// <summary>
        /// Inverts the R-G-B parts of <paramref name="color"/>.
        /// </summary>
        /// <param name="color">The input color.</param>
        /// <returns>An inverted color.</returns>
        public static Color Invert(this Color color)
        {
            var r = 255 - (int)(255 * color.R);
            var g = 255 - (int)(255 * color.G);
            var b = 255 - (int)(255 * color.B);

            return Color.FromRgb(r, g, b);
        }
    }
}
