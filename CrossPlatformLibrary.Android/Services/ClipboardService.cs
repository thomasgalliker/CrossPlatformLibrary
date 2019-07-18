using Android.Content;

namespace CrossPlatformLibrary.Services
{
    public class ClipboardService : IClipboardService
    {
        public void CopyToClipboard(string text)
        {
            var clipboardManager = (ClipboardManager)Android.App.Application.Context.GetSystemService(Context.ClipboardService);
            ClipData clip = ClipData.NewPlainText("Android Clipboard", text);
            clipboardManager.PrimaryClip = clip;
        }
    }
}