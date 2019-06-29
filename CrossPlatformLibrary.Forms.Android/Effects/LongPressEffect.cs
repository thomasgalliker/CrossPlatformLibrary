using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using LongPressEffect = CrossPlatformLibrary.Forms.Android.Effects.LongPressEffect;
using View = Android.Views.View;

[assembly: ExportEffect(typeof(LongPressEffect), "LongPressEffect")]
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
                    // Control.SetBackgroundColor(Android.Graphics.Color.Red);
                    this.Control.LongClick += this.Control_LongClick;
                }
                else
                {
                    this.Container.LongClickable = true;
                    this.Container.LongClick += this.Control_LongClick;
                }
                // Control.SetBackgroundColor(Android.Graphics.Color.Blue);

                // }
            }
        }

        private void Control_LongClick(object sender, View.LongClickEventArgs e)
        {
            // Control.SetBackgroundColor(Android.Graphics.Color.Black);
            var command = Forms.Effects.LongPressEffect.GetCommand(this.Element);
            command?.Execute(Forms.Effects.LongPressEffect.GetCommandParameter(this.Element));
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