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
        private Drawable originalBackground = null;

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
                    this.UpdatePadding(customEntry);
                    this.UpdateBorder(customEntry);
                }
            }
        }

        private void UpdatePadding(CustomEntry customEntry)
        {
            var padding = customEntry.Padding;
            this.Control.SetPadding((int)padding.Left, (int)padding.Top, (int)padding.Right, (int)padding.Bottom);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (this.Element is CustomEntry customEntry)
            {
                if (e.PropertyName == CustomEntry.PaddingProperty.PropertyName)
                {
                    this.UpdatePadding(customEntry);
                }
                else if (e.PropertyName == CustomEntry.HideBorderProperty.PropertyName)
                {
                    this.UpdateBorder(customEntry);
                }
            }
        }

        private void UpdateBorder(CustomEntry customEntry)
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
    }
}