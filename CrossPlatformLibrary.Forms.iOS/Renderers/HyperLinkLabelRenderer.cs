using System;
using System.ComponentModel;
using CrossPlatformLibrary.Forms.Controls;
using CrossPlatformLibrary.Forms.iOS.Renderers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(HyperLinkLabel), typeof(HyperLinkLabelRenderer))]

namespace CrossPlatformLibrary.Forms.iOS.Renderers
{
    [Preserve(AllMembers = true)]
    public class HyperLinkLabelRenderer : LabelRenderer
    {
        public static async void Init()
        {
            var now = DateTime.Now;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                if (this.Element is HyperLinkLabel hyperLinkLabel)
                {
                    this.UpdateTintColor(hyperLinkLabel);

                    this.Control.BackgroundColor = UIColor.Clear;
                    this.Control.UserInteractionEnabled = true;

                    var tapGestureRecognizer = new UITapGestureRecognizer();
                    tapGestureRecognizer.AddTarget(
                        () =>
                        {
                            if (!string.IsNullOrEmpty(hyperLinkLabel.NavigateUri))
                            {
                                UIApplication.SharedApplication.OpenUrl(new NSUrl(this.GetNavigationUri(hyperLinkLabel.NavigateUri)));
                            }

                            if (hyperLinkLabel.NavigateCommand != null)
                            {
                                hyperLinkLabel.NavigateCommand.Execute(null);
                            }
                        });

                    tapGestureRecognizer.NumberOfTapsRequired = 1;
                    tapGestureRecognizer.DelaysTouchesBegan = true;
                    this.Control.AddGestureRecognizer(tapGestureRecognizer);
                }
            }
        }

        private string GetNavigationUri(string uri)
        {
            if (uri.Contains("@") && !uri.StartsWith("mailto:"))
            {
                return $"mailto:{uri}";
            }

            if (uri.StartsWith("www.")) //TODO would it be better to do a !starts with http:// and https://???
            {
                return $"{@"http://"}{uri}";
            }

            return uri;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == HyperLinkLabel.TintColorProperty.PropertyName)
            {
                var hyperLinkLabel = (HyperLinkLabel)this.Element;
                this.UpdateTintColor(hyperLinkLabel);
            }
        }

        private void UpdateTintColor(HyperLinkLabel hyperLinkLabel)
        {
            if (this.Control != null)
            {
                this.Control.TextColor = hyperLinkLabel.TintColor.ToUIColor();
            }
        }
    }
}