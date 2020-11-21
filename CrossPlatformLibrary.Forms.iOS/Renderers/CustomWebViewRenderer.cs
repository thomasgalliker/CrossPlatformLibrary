using System.Threading.Tasks;
using CrossPlatformLibrary.Forms.Controls;
using CrossPlatformLibrary.Forms.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRender))]

namespace CrossPlatformLibrary.Forms.iOS.Renderers
{
    public class CustomWebViewRender : WebViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is CustomWebView webView)
            {
                webView.EvaluateJavascript = (js) =>
                {
                    return Task.FromResult(this.EvaluateJavascript(js));
                };
            }
        }
    }
}