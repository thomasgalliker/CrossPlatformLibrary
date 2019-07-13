using CrossPlatformLibrary.Forms.Effects;
using CrossPlatformLibrary.Forms.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(LongPressedEffect), nameof(LongPressedEffect))]

namespace CrossPlatformLibrary.Forms.iOS.Effects
{
    public class LongPressedEffect : PlatformEffect
    {
        private bool attached;
        private readonly UILongPressGestureRecognizer longPressRecognizer;

        public LongPressedEffect()
        {
            this.longPressRecognizer = new UILongPressGestureRecognizer(this.HandleLongClick);
        }

        protected override void OnAttached()
        {
            // Because an effect can be detached immediately after attached (happens in ListView), only attach the handler one time
            if (!this.attached)
            {
                this.Container.AddGestureRecognizer(this.longPressRecognizer);
                this.attached = true;
            }
        }

        private void HandleLongClick()
        {
            var command = LongPressEffect.GetCommand(this.Element);
            command?.Execute(LongPressEffect.GetCommandParameter(this.Element));
        }

        protected override void OnDetached()
        {
            if (this.attached)
            {
                this.Container.RemoveGestureRecognizer(this.longPressRecognizer);
                this.attached = false;
            }
        }
    }
}