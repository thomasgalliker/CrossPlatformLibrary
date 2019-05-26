using System;
using System.ComponentModel;
using Android.Content;
using Android.Runtime;
using Android.Text.Util;
using CrossPlatformLibrary.Forms.Android.Renderers;
using CrossPlatformLibrary.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(HyperLinkLabel), typeof(HyperLinkLabelRenderer))]

namespace CrossPlatformLibrary.Forms.Android.Renderers
{
    [Preserve(AllMembers = true)]
    public class HyperLinkLabelRenderer : Xamarin.Forms.Platform.Android.LabelRenderer
    {
        public static async void Init()
        {
            var now = DateTime.Now;
        }

        public HyperLinkLabelRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null)
            {
                Linkify.AddLinks(this.Control, MatchOptions.All);

                if (this.Element is HyperLinkLabel hyperLinkLabel)
                {
                    this.UpdateTintColor(hyperLinkLabel);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == HyperLinkLabel.NavigateUriProperty.PropertyName)
            {
                Linkify.AddLinks(this.Control, MatchOptions.All);
            }
            else if (e.PropertyName == HyperLinkLabel.TintColorProperty.PropertyName)
            {
                var hyperLinkLabel = (HyperLinkLabel)this.Element;
                this.UpdateTintColor(hyperLinkLabel);
            }
        }

        private void UpdateTintColor(HyperLinkLabel hyperLinkLabel)
        {
            if (this.Control != null)
            {
                this.Control.SetLinkTextColor(hyperLinkLabel.TintColor.ToAndroid());
            }
        }
    }
}