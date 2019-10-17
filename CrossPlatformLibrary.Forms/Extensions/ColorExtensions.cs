using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Extensions
{
    public static class ColorExtensions
    {
        public static string ToHexString(this Color color, bool outputAlpha = true)
        {
            string hex = "#";
            if (outputAlpha)
            {
                hex += DoubleToHex(color.A);
            }

            var hexString = $"{hex}{DoubleToHex(color.R)}{DoubleToHex(color.G)}{DoubleToHex(color.B)}";
            return hexString;
        }

        private static string DoubleToHex(double value)
        {
            return string.Format("{0:X2}", (int)(value * 255));
        }
    }
}
