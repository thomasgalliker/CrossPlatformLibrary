using System;
using System.Diagnostics;
using CrossPlatformLibrary.Forms.Services;
using Foundation;
using UIKit;

namespace CrossPlatformLibrary.Forms.iOS.Services
{
    /// <summary>
    /// iOS specific font size converter.
    /// 
    /// Subscribes to changes of the accessibility settings
    /// and is able to calculate scaled font sizes.
    /// </summary>
    public class FontConverter : IFontConverter, IDisposable
    {
        private static readonly NSString UiContentSizeCategoryDidChangeNotificationKey = (NSString)"UIContentSizeCategoryDidChangeNotification";

        public FontConverter()
        {
            NSNotificationCenter.DefaultCenter.AddObserver(UiContentSizeCategoryDidChangeNotificationKey, (n) =>
            {
                this.OnUiContentSizeChanged();
            });
        }

        public event EventHandler FontScalingChanged;

        public double GetScaledFontSize(double fontSize)
        {
            var scaledFontSize = UIFontMetrics.DefaultMetrics.GetScaledValue((nfloat)fontSize);
#if DEBUG
            Debug.WriteLine($"GetScaledFontSize: {fontSize} -> {scaledFontSize} (scale factor: {(scaledFontSize / fontSize):F2}x)");
#endif
            return scaledFontSize;
        }

        protected virtual void OnUiContentSizeChanged()
        {
            this.FontScalingChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            NSNotificationCenter.DefaultCenter.RemoveObserver(UiContentSizeCategoryDidChangeNotificationKey);
        }
    }
}