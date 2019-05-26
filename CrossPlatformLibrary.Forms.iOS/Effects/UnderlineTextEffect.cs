using CrossPlatformLibrary.Forms.iOS.Effects;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(UnderlineTextEffect), nameof(UnderlineTextEffect))]
namespace CrossPlatformLibrary.Forms.iOS.Effects
{
    public class UnderlineTextEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var label = this.Control as UILabel;
            var element = this.Element as Label;

            if (label != null && element != null)
            {
                var attributes = new UIStringAttributes { UnderlineStyle = NSUnderlineStyle.Single };
                var attrString = new NSAttributedString(element.Text, attributes);
                label.AttributedText = attrString;
            }
        }

        protected override void OnDetached()
        {
        }
    }
}