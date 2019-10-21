using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    /// <summary>
    /// https://xamarinhelp.com/xamarin-forms-webview-executing-javascript/
    /// </summary>
    public class CustomWebView : WebView
    {
        public static BindableProperty EvaluateJavascriptProperty = BindableProperty.Create(
            nameof(EvaluateJavascript), 
            typeof(Func<string, Task<string>>),
            typeof(CustomWebView),
            null,
            BindingMode.OneWayToSource);

        public Func<string, Task<string>> EvaluateJavascript
        {
            get => (Func<string, Task<string>>)this.GetValue(EvaluateJavascriptProperty);
            set => this.SetValue(EvaluateJavascriptProperty, value);
        }
    }
}
