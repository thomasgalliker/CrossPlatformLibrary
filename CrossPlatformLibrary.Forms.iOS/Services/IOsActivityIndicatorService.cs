using System;
using CrossPlatformLibrary.Forms.Controls;
using CrossPlatformLibrary.Forms.iOS.Extensions;
using CrossPlatformLibrary.Forms.Services;
using UIKit;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.iOS.Services
{
    public class iOSActivityIndicatorService : IActivityIndicatorService
    {
        private UIView nativeView;
        private bool isInitialized;
        private IActivityIndicatorPage activityIndicatorPage;

        public void Init<T>(T activityIndicatorPage) where T : ContentPage, IActivityIndicatorPage
        {
            var mainPage = Xamarin.Forms.Application.Current.MainPage;
            if (mainPage == null)
            {
                return;
            }

            this.activityIndicatorPage = activityIndicatorPage ?? throw new ArgumentException(nameof(activityIndicatorPage));

            // check if the page parameter is available
            // build the loading page with native base
            activityIndicatorPage.Parent = mainPage;

            this.isInitialized = true;
        }


        private void RenderPage()
        {
            var mainPage = Xamarin.Forms.Application.Current.MainPage;
            if (mainPage == null)
            {
                return;
            }

            var contentPage = (ContentPage)this.activityIndicatorPage;
            contentPage.Layout(new Rectangle(0, 0, mainPage.Width, mainPage.Height));

            var renderer = contentPage.GetOrCreateRenderer();
            this.nativeView = renderer.NativeView;
        }


        public void ShowLoadingPage(string text)
        {
            // check if the user has set the page or not
            if (!this.isInitialized)
            {
                this.Init(new CustomActivityIndicatorPage()); // set the default page
            }

            if (this.nativeView == null)
            {
                this.RenderPage();
            }

            // showing the native loading page
            if (this.activityIndicatorPage != null)
            {
                this.activityIndicatorPage.SetCaption(text);
            }

            if (this.nativeView != null)
            {
                UIApplication.SharedApplication.KeyWindow.AddSubview(this.nativeView);
            }
        }

        private void XamFormsPage_Appearing(object sender, EventArgs e)
        {
            var animation = new Animation(callback: d => ((ContentPage)sender).Content.Rotation = d,
                start: ((ContentPage)sender).Content.Rotation,
                end: ((ContentPage)sender).Content.Rotation + 360,
                easing: Easing.Linear);
            animation.Commit(((ContentPage)sender).Content, "RotationLoopAnimation", 16, 800, null, null, () => true);
        }

        public void HideLoadingPage()
        {
            // Hide the page
            if (this.nativeView != null)
            {
                this.nativeView.RemoveFromSuperview();
                this.nativeView.Dispose();
                this.nativeView = null;
            }
        }
    }
}