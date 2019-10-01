using CrossPlatformLibrary.Forms.iOS.Renderers;
using UIKit;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(ContentPage), typeof(PageRenderer))]

namespace CrossPlatformLibrary.Forms.iOS.Renderers
{
    public class PageRenderer : Xamarin.Forms.Platform.iOS.PageRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (this.ViewController?.NavigationController?.InteractivePopGestureRecognizer is UIGestureRecognizer uiGestureRecognizer)
            {
                uiGestureRecognizer.Enabled = true;
                uiGestureRecognizer.Delegate = new UIGestureRecognizerDelegate();
            }
        }
    }
}