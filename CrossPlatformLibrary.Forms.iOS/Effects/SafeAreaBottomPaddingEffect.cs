using CrossPlatformLibrary.Forms.iOS.Effects;
using Xamarin.Forms;

[assembly: ExportEffect(typeof(SafeAreaBottomPaddingEffect), nameof(SafeAreaBottomPaddingEffect))]

namespace CrossPlatformLibrary.Forms.iOS.Effects
{
    /// <summary>
    ///     Adds safe-area padding to the bottom of the UI element.
    /// </summary>
    public class SafeAreaBottomPaddingEffect : SafeAreaPaddingEffect
    {
        protected override Thickness GetSafeAreaPadding(Thickness originalPadding, Thickness safeAreaInsets, bool includeStatusBar)
        {
            var safeAreaPadding = base.GetSafeAreaPadding(originalPadding, safeAreaInsets, includeStatusBar: false);
            safeAreaPadding.Top = 0;
            return safeAreaPadding;
        }
    }
}