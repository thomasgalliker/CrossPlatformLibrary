using System;
using System.Threading;
using System.Threading.Tasks;
using Android.Content;
using Android.Webkit;
using CrossPlatformLibrary.Forms.Android.Renderers;
using CrossPlatformLibrary.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRender))]
namespace CrossPlatformLibrary.Forms.Android.Renderers
{
    public class CustomWebViewRender : WebViewRenderer
    {
        public CustomWebViewRender(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is CustomWebView webView)
                webView.EvaluateJavascript = async (js) =>
                {
                    var reset = new ManualResetEvent(false);
                    var response = string.Empty;
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        this.Control?.EvaluateJavascript(js, new JavascriptCallback((r) =>
                        {
                            response = r;
                            reset.Set();
                        }));
                    });
                    await Task.Run(() => { reset.WaitOne(); });
                    return response;
                };
        }
    }

    internal class JavascriptCallback : Java.Lang.Object, IValueCallback
    {
        private readonly Action<string> callback;

        public JavascriptCallback(Action<string> callback)
        {
            this.callback = callback;
        }

        public void OnReceiveValue(Java.Lang.Object value)
        {
            this.callback?.Invoke(Convert.ToString(value));
        }
    }
}