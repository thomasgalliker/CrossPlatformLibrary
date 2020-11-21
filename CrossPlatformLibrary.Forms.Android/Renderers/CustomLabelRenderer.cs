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
        private Thickness? originalPadding;

        public CustomLabelRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
            {
                return;
            }

            var textView = this.Control;
            if (textView != null)
            {
                if (this.Element is CustomLabel customLabel)
                {
                    this.UpdateLines(customLabel);
                    this.JustifyText(customLabel); 
                    this.RemovePadding(customLabel);
                }
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
            else if (e.PropertyName == CustomLabel.RemovePaddingProperty.PropertyName)
            {
                if (this.Element is CustomLabel customLabel)
                {
                    this.RemovePadding(customLabel);
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

        private void RemovePadding(CustomLabel customLabel)
        {
            if (customLabel.RemovePadding)
            {
                this.originalPadding = new Thickness(left: this.Control.PaddingLeft, top: this.Control.PaddingTop, right: this.Control.PaddingRight, bottom: this.Control.PaddingBottom);
                this.Control.SetPadding(0, 0, 0, 0);
                this.Control.SetIncludeFontPadding(false);
            }
            else
            {
                if (this.originalPadding != null)
                {
                    var p = this.originalPadding.Value;
                    var left = (int)p.Left;
                    var top = (int)p.Top;
                    var right = (int)p.Right;
                    var bottom = (int)p.Bottom;
                    this.Control.SetPadding(left, top, right, bottom);
                }

                this.Control.SetIncludeFontPadding(true);
            }
        }
    }
}