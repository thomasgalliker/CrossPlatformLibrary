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
        /// Inverts the R-G-B parts of <paramref name="color"/>.
        /// The Alpha channel remains the same as in <paramref name="color"/>.
        /// </summary>
        /// <param name="color">The input color.</param>
        /// <returns>An inverted color.</returns>
        public static Color Invert(this Color color)
        {
            var r = 255 - (int)(255 * color.R);
            var g = 255 - (int)(255 * color.G);
            var b = 255 - (int)(255 * color.B);

            return Color.FromRgba(r, g, b, color.A);
        }
    }
}
