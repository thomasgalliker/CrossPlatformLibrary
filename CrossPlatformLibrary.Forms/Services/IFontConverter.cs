using System;

namespace CrossPlatformLibrary.Forms.Services
{
    public interface IFontConverter : IDisposable
    {
        event EventHandler FontScalingChanged;
        
        double GetScaledFontSize(double fontSize);
    }
}