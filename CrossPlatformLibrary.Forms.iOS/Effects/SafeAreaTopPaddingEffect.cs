using CrossPlatformLibrary.Forms.iOS.Effects;
using UIKit;
using Xamarin.Forms;

[assembly: ExportEffect(typeof(SafeAreaTopPaddingEffect), nameof(SafeAreaTopPaddingEffect))]

namespace CrossPlatformLibrary.Forms.iOS.Effects
{
    public class SafeAreaTopPaddingEffect : SafeAreaPaddingEffect
    {
        protected override Thickness GetPadding(Thickness padding, UIEdgeInsets insets)
        {
            return new Thickness(padding.Left + insets.Left, padding.Top + insets.Top, padding.Right + insets.Right, padding.Bottom);
        }

        protected override Thickness GetDefaultPadding(Thickness padding)
        {
            return new Thickness(padding.Left, padding.Top, padding.Right, padding.Bottom);
        }
    }
}