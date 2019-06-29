using CrossPlatformLibrary.Forms.Effects;
using CrossPlatformLibrary.Forms.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(LongPressedEffect), "LongPressEffect")]

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

        /// <summary>
        ///     Apply the handler
        /// </summary>
        protected override void OnAttached()
        {
            //because an effect can be detached immediately after attached (happens in listview), only attach the handler one time
            if (!this.attached)
            {
                this.Container.AddGestureRecognizer(this.longPressRecognizer);
                this.attached = true;
            }
        }

        /// <summary>
        ///     Invoke the command if there is one
        /// </summary>
        private void HandleLongClick()
        {
            var command = LongPressEffect.GetCommand(this.Element);
            command?.Execute(LongPressEffect.GetCommandParameter(this.Element));
        }

        /// <summary>
        ///     Clean the event handler on detach
        /// </summary>
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