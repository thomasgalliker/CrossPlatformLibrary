using System;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using Android.Views;
using CrossPlatformLibrary.Forms.Android.Extensions;
using CrossPlatformLibrary.Forms.Controls;
using CrossPlatformLibrary.Forms.Services;
using Xamarin.Forms;
using Color = Android.Graphics.Color;
using View = Android.Views.View;

namespace CrossPlatformLibrary.Forms.Android.Services
{
    public class AndroidActivityIndicatorService : IActivityIndicatorService
    {
        private View nativeView;
        private Dialog dialog;
        private bool isInitialized;
        private ContentPage activityIndicatorPage;

        private static DisplayMetrics GetDisplayMetrics(Context context)
        {
            var displayMetrics = new DisplayMetrics();
            var windowManager = context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();
            windowManager.DefaultDisplay.GetMetrics(displayMetrics);

            return displayMetrics;
        }

        public void Init(ContentPage activityIndicatorPage)
        {
            this.activityIndicatorPage = activityIndicatorPage ?? throw new ArgumentException(nameof(activityIndicatorPage));

            var mainPage = Xamarin.Forms.Application.Current?.MainPage;
            if (mainPage == null)
            {
                return;
            }

            // build the loading page with native base
            activityIndicatorPage.Parent = mainPage;

            this.isInitialized = true;
        }

        private void RenderPage()
        {
            var mainPage = Xamarin.Forms.Application.Current?.MainPage;
            if (mainPage == null)
            {
                return;
            }

            var context = global::CrossPlatformLibrary.CrossPlatformLibrary.CurrentActivity;
            var displayMetrics = GetDisplayMetrics(context);

            var contentPage = this.activityIndicatorPage;
            contentPage.Layout(new Rectangle(0, 0, mainPage.Width, mainPage.Height));

            var renderer = contentPage.GetOrCreateRenderer(context);
            this.nativeView = renderer.View;

            this.dialog = new Dialog(context);
            this.dialog.RequestWindowFeature((int)WindowFeatures.NoTitle);
            this.dialog.SetCancelable(false);
            this.dialog.SetContentView(this.nativeView);

            var window = this.dialog.Window;
            window.SetLayout(displayMetrics.WidthPixels, displayMetrics.HeightPixels);
            window.SetGravity(GravityFlags.CenterHorizontal | GravityFlags.CenterVertical);
            window.ClearFlags(WindowManagerFlags.DimBehind);
            window.SetBackgroundDrawable(new ColorDrawable(Color.Transparent));
        }

        public void ShowLoadingPage(string text)
        {
            // check if the user has set the page or not
            if (!this.isInitialized)
            {
                this.Init(this.activityIndicatorPage ?? new CustomActivityIndicatorPage()); // set the default page
            }

            if (this.nativeView == null)
            {
                this.RenderPage();
            }

            // showing the native loading page
            if (this.activityIndicatorPage is IActivityIndicatorPage activityIndicatorPage)
            {
                activityIndicatorPage.SetCaption(text);
            }

            if (this.dialog != null && !this.dialog.IsShowing)
            {
                this.dialog.Show();
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

        public async void HideLoadingPage()
        {
            await Device.InvokeOnMainThreadAsync(() =>
            {
                if (this.dialog != null)
                {
                    this.dialog.Dismiss();
                    this.dialog.Dispose();
                    this.dialog = null;

                    this.nativeView.Dispose();
                    this.nativeView = null;
                }
            });
        }
    }
}