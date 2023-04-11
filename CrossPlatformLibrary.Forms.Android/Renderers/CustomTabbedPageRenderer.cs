using System.ComponentModel;
using Android.Content;
using Android.Widget;
using CrossPlatformLibrary.Forms.Android.Renderers;
using CrossPlatformLibrary.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(CustomTabbedPage), typeof(CustomTabbedPageRenderer))]
namespace CrossPlatformLibrary.Forms.Android.Renderers
{
    public class CustomTabbedPageRenderer : TabbedPageRenderer
    {
        private FrameLayout bottomNavigationView;

        public CustomTabbedPageRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is CustomTabbedPage tabbedPage)
            {
                this.bottomNavigationView = (this.GetChildAt(0) as global::Android.Widget.RelativeLayout).GetChildAt(1) as FrameLayout;

                this.UpdateBottomNavigationVisibility();

                tabbedPage.PropertyChanged += this.ElementPropertyHasChanged;
            }

            if (e.OldElement != null)
            {
                this.Element.PropertyChanged -= this.OnElementPropertyChanged;
            }
        }

        private void ElementPropertyHasChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CustomTabbedPage.IsHidden))
            {
                this.UpdateBottomNavigationVisibility();
            }
        }

        private void UpdateBottomNavigationVisibility()
        {
            var customTabbedPage = (CustomTabbedPage)this.Element;

            if (customTabbedPage.IsHidden)
            {
                var parameters = this.bottomNavigationView.LayoutParameters;
                parameters.Height = 0;
                this.bottomNavigationView.LayoutParameters = parameters;
            }
            else
            {
                var parameters = this.bottomNavigationView.LayoutParameters;
                parameters.Height = LayoutParams.WrapContent;
                this.bottomNavigationView.LayoutParameters = parameters;
            }
        }
    }
}