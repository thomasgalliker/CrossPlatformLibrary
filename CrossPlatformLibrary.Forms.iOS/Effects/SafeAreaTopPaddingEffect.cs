using CrossPlatformLibrary.Forms.iOS.Effects;
using Xamarin.Forms;

[assembly: ExportEffect(typeof(SafeAreaTopPaddingEffect), nameof(SafeAreaTopPaddingEffect))]

namespace CrossPlatformLibrary.Forms.iOS.Effects
{
    /// <summary>
    ///     Adds safe-area padding to the top of the UI element.
    /// </summary>
    public class SafeAreaTopPaddingEffect : SafeAreaPaddingEffect
    {
        protected override Thickness GetSafeAreaPadding(Thickness originalPadding, Thickness safeAreaInsets, bool includeStatusBar)
        {
            var safeAreaPadding = base.GetSafeAreaPadding(originalPadding, safeAreaInsets, includeStatusBar);
            safeAreaPadding.Bottom = 0;
            return safeAreaPadding;
        }
    }
}