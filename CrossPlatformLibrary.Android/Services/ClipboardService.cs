using System.Threading.Tasks;
using Android.Content;

namespace CrossPlatformLibrary.Services
{
    public class ClipboardService : IClipboardService
    {
        public void SetText(string text)
        {
            var clipboardManager = (ClipboardManager)Android.App.Application.Context.GetSystemService(Context.ClipboardService);
            clipboardManager.Text = text;
        }

        public string GetText()
        {
            var clipboardManager = (ClipboardManager)Android.App.Application.Context.GetSystemService(Context.ClipboardService);
            return clipboardManager.Text;
        }
    }
}