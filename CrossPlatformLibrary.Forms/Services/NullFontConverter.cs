using System;

namespace CrossPlatformLibrary.Forms.Services
{
    /// <summary>
    /// <seealso cref="IFontConverter"/> which does not change the scale of font sizes.
    /// </summary>
    public class NullFontConverter : IFontConverter
    {
        public event EventHandler FontScalingChanged;

        public double GetScaledFontSize(double fontSize)
        {
            return fontSize;
        }

        public void Dispose()
        {
        }
    }
}