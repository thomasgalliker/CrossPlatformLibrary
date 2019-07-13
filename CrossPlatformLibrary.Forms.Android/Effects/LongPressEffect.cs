using CrossPlatformLibrary.Forms.Android.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;

[assembly: ExportEffect(typeof(LongPressEffect), nameof(LongPressEffect))]

namespace CrossPlatformLibrary.Forms.Android.Effects
{
    public class LongPressEffect : PlatformEffect
    {
        private bool attached;

        public static void Initialize()
        {
        }

        protected override void OnAttached()
        {
            if (!this.attached)
            {
                if (this.Control != null)
                {
                    this.Control.LongClickable = true;
                    this.Control.LongClick += this.Control_LongClick;
                }
                else
                {
                    this.Container.LongClickable = true;
                    this.Container.LongClick += this.Control_LongClick;
                }
            }
        }

        private void Control_LongClick(object sender, View.LongClickEventArgs e)
        {
            var command = Forms.Effects.LongPressEffect.GetCommand(this.Element);
            if (command != null)
            {
                command.Execute(Forms.Effects.LongPressEffect.GetCommandParameter(this.Element));
            }
        }

        protected override void OnDetached()
        {
            if (this.attached)
            {
                if (this.Control != null)
                {
                    this.Control.LongClickable = true;
                    this.Control.LongClick -= this.Control_LongClick;
                }
                else
                {
                    this.Container.LongClickable = true;
                    this.Container.LongClick -= this.Control_LongClick;
                }

                this.attached = false;
            }
        }
    }
}