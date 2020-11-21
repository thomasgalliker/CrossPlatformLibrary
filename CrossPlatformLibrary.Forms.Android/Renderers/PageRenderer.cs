using System;
using Android.Content;
using Android.Runtime;
using CrossPlatformLibrary.Forms.Android.Renderers;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(Page), typeof(PageRenderer))]
namespace CrossPlatformLibrary.Forms.Android.Renderers
{
    public class PageRenderer : Xamarin.Forms.Platform.Android.PageRenderer
    {
        public PageRenderer(Context context) : base(context)
        {
        }

        public PageRenderer(IntPtr handle, JniHandleOwnership transfer)
        {
        }

        protected override void OnAttachedToWindow()
        {
            if (this.Element == null)
            {
                return;
            }
            base.OnAttachedToWindow();
        }

        protected override void OnDetachedFromWindow()
        {
            if (this.Element == null)
            {
                // BUG: No constructor found for ... is a known bug.
                // System.NotSupportedException: Unable to activate instance of type Xamarin.Forms.Platform.Android.PageRenderer from native handle 0x7ff6e15da4 (key_handle 0xee82b63).
                // [ERROR] FATAL UNHANDLED EXCEPTION: System.NotSupportedException: Unable to activate instance of type Xamarin.Forms.Platform.Android.PageRenderer from native handle 0x7ff6e15da4 (key_handle 0xee82b63). ---> System.MissingMethodException: No constructor found for Xamarin.Forms.Platform.Android.PageRenderer::.ctor(System.IntPtr, Android.Runtime.JniHandleOwnership) ---> Java.Interop.JavaLocationException: Exception of type 'Java.Interop.JavaLocationException' was thrown.
                // Reproduce: SettingsPage -> ProfilePage -> Logout -> LoginPage -> Press 'back' -> Crash!
                // https://github.com/xamarin/Xamarin.Forms/issues/2584

                return;
            }

            base.OnDetachedFromWindow();
        }
    }
}
