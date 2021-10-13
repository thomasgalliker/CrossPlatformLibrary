using System.ComponentModel;
using CoreGraphics;
using CrossPlatformLibrary.Forms.Controls;
using CrossPlatformLibrary.Forms.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomTabbedPage), typeof(CustomTabbedPageRenderer))]
namespace CrossPlatformLibrary.Forms.iOS.Renderers
{
    /// <remarks>
    /// Helpful sources:
    /// https://github.com/stormlion227/STabbedPage.Forms/blob/638cb642a29f16205040a37ab3f01fd6be9c0248/STabbedPage/STabbedPage.iOS/STabbedPageRenderer.cs#L95
    /// </remarks>
    public class CustomTabbedPageRenderer : TabbedRenderer
    {
        private CGRect originalTabBarFrame;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                this.UpdateBottomNavigationVisibility();
                this.Element.PropertyChanged += this.OnElementPropertyChanged;
            }

            if (e.OldElement != null)
            {
                this.Element.PropertyChanged -= this.OnElementPropertyChanged;
            }
        }

        private void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CustomTabbedPage.IsHidden))
            {
                this.UpdateBottomNavigationVisibility();
            }
            else if (e.PropertyName == NavigationPage.CurrentPageProperty.PropertyName)
            {
                this.View.SetNeedsLayout();
            }
        }

        private void UpdateBottomNavigationVisibility()
        {
            var frame = this.View.Frame;
            var tabBarFrame = this.TabBar.Frame;
            if (tabBarFrame.Height > 0)
            {
                this.originalTabBarFrame = tabBarFrame;
            }

            var customTabbedPage = (CustomTabbedPage)this.Element;
            if (customTabbedPage.IsHidden)
            {
                this.TabBar.Hidden = true;
                this.TabBar.Frame = new CGRect(0, 0, 0, 0);
                customTabbedPage.ContainerArea = new Rectangle(0, 0, frame.Width, frame.Height);
            }
            else
            {
                this.TabBar.Hidden = false;
                this.TabBar.Frame = this.originalTabBarFrame;
                customTabbedPage.ContainerArea = new Rectangle(0, 0, frame.Width, frame.Height - this.originalTabBarFrame.Height);
            }
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            this.UpdateBottomNavigationVisibility();
        }
    }
}