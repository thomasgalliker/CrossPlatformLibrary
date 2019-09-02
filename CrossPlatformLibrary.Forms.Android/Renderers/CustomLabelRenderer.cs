using Android.Content;
using CrossPlatformLibrary.Forms.Android.Renderers;
using CrossPlatformLibrary.Forms.Controls;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomLabel), typeof(CustomLabelRenderer))]

namespace CrossPlatformLibrary.Forms.Android.Renderers
{
    public class CustomLabelRenderer : Xamarin.Forms.Platform.Android.LabelRenderer
    {
        public CustomLabelRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (this.Element is CustomLabel customLabel)
            {
                this.UpdateLines(customLabel);
                this.JustifyText(customLabel);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == CustomLabel.LinesProperty.PropertyName)
            {
                if (this.Element is CustomLabel customLabel)
                {
                    this.UpdateLines(customLabel);
                }
            }
            else if (e.PropertyName == CustomLabel.JustifyTextProperty.PropertyName)
            {
                if (this.Element is CustomLabel customLabel)
                {
                    this.JustifyText(customLabel);
                }
            }
        }

        private void JustifyText(CustomLabel customLabel)
        {
            if (global::Android.OS.Build.VERSION.SdkInt < global::Android.OS.BuildVersionCodes.O)
            {
                return;
            }

            if (customLabel.JustifyText)
            {
                this.Control.JustificationMode = global::Android.Text.JustificationMode.InterWord;
            }
            else
            {
                this.Control.JustificationMode = global::Android.Text.JustificationMode.None;
            }
        }

        private void UpdateLines(CustomLabel customLabel)
        {
            if (customLabel.Lines != CustomLabel.DefaultLinesValue)
            {
                this.Control.SetSingleLine(false);
                this.Control.SetLines(customLabel.Lines);
            }
            else
            {
                // TODO How to switch back?
            }
        }
    }
}