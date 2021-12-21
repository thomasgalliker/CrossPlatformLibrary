using System.ComponentModel;
using Android.Content;
using Android.Views;
using CrossPlatformLibrary.Forms.Android.Renderers;
using CrossPlatformLibrary.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomScrollView), typeof(CustomScrollViewRenderer))]
namespace CrossPlatformLibrary.Forms.Android.Renderers
{
    public class CustomScrollViewRenderer : ScrollViewRenderer
    {
        private bool scrollEnabled;

        public CustomScrollViewRenderer(Context context) : base(context)
        {
        }

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
            this.scrollEnabled = customScrollView.IsScrollEnabled;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var customScrollView = (CustomScrollView)sender;

            if (e.PropertyName == CustomScrollView.IsScrollEnabledProperty.PropertyName)
            {
                this.SetIsScrollEnabled(customScrollView);
            }
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            if (this.scrollEnabled)
            {
                return base.OnInterceptTouchEvent(ev);
            }
            else
            {
                return false;
            }
        }

        public override bool OnTouchEvent(MotionEvent ev)
        {
            if (this.scrollEnabled)
            {
                return base.OnTouchEvent(ev);
            }
            else
            {
                return false;
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
