using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using CrossPlatformLibrary.Forms.Android.Renderers;
using CrossPlatformLibrary.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]

namespace CrossPlatformLibrary.Forms.Android.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        private Drawable originalBackground;
        private Thickness? originalPadding;

        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
            {
                return;
            }

            var formsEditText = this.Control;
            if (formsEditText != null)
            {
                if (this.Element is CustomEntry customEntry)
                {
                    this.HideBorder(customEntry);
                    this.RemovePadding(customEntry);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == CustomEntry.HideBorderProperty.PropertyName)
            {
                if (this.Element is CustomEntry customEntry)
                {
                    this.HideBorder(customEntry);
                }
            }
            else if (e.PropertyName == CustomEntry.RemovePaddingProperty.PropertyName)
            {
                if (this.Element is CustomEntry customEntry)
                {
                    this.RemovePadding(customEntry);
                }
            }
        }

        private void HideBorder(CustomEntry customEntry)
        {
            if (customEntry.HideBorder)
            {
                this.originalBackground = this.Control.Background;
                this.Control.Background = null;
            }
            else
            {
                if (this.originalBackground != null)
                {
                    this.Control.Background = this.originalBackground;
                    this.originalBackground = null;
                }
            }
        }

        private void RemovePadding(CustomEntry customEntry)
        {
            if (customEntry.RemovePadding)
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