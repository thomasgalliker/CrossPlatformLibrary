using CrossPlatformLibrary.Forms.iOS.Effects;
using UIKit;
using Xamarin.Forms;

[assembly: ExportEffect(typeof(SafeAreaBottomPaddingEffect), nameof(SafeAreaBottomPaddingEffect))]

namespace CrossPlatformLibrary.Forms.iOS.Effects
{
    public class SafeAreaBottomPaddingEffect : SafeAreaPaddingEffect
    {
        protected override Thickness GetPadding(Thickness padding, UIEdgeInsets insets)
        {
            return new Thickness(padding.Left + insets.Left, padding.Top, padding.Right, padding.Bottom + insets.Bottom);
        }

        protected override Thickness GetDefaultPadding(Thickness padding)
        {
            return new Thickness(padding.Left, padding.Top, padding.Right, padding.Bottom + 20);
        }
    }
}