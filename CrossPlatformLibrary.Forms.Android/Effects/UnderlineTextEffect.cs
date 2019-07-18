using Android.Widget;
using CrossPlatformLibrary.Forms.Android.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(UnderlineTextEffect), nameof(UnderlineTextEffect))]
namespace CrossPlatformLibrary.Forms.Android.Effects
{
    public class UnderlineTextEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var label = this.Control as TextView;

            if (label != null)
            {
                label.PaintFlags |= global::Android.Graphics.PaintFlags.UnderlineText;
            }
        }

        protected override void OnDetached()
        {
        }
    }
}