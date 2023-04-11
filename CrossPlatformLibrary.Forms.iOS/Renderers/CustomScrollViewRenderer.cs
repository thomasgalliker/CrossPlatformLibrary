using System.ComponentModel;
using CrossPlatformLibrary.Forms.Controls;
using CrossPlatformLibrary.Forms.iOS.Renderers;
using Foundation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomScrollView), typeof(CustomScrollViewRenderer))]

namespace CrossPlatformLibrary.Forms.iOS.Renderers
{
    [Preserve(AllMembers = true)]
    public class CustomScrollViewRenderer : ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is CustomScrollView customScrollView)
            {
                this.SetIsScrollEnabled(customScrollView);
                customScrollView.PropertyChanged += this.OnPropertyChanged;
            }
        }

        private void SetIsScrollEnabled(CustomScrollView customScrollView)
        {
            this.ScrollEnabled = customScrollView.IsScrollEnabled;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var customScrollView = (CustomScrollView)sender;

            if (e.PropertyName == CustomScrollView.IsScrollEnabledProperty.PropertyName)
            {
                this.SetIsScrollEnabled(customScrollView);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (this.Element is CustomScrollView customScrollView)
            {
                customScrollView.PropertyChanged -= this.OnPropertyChanged;
            }

            base.Dispose(disposing);
        }
    }
}
