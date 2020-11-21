using UIKit;

namespace CrossPlatformLibrary.Services
{
    public class ClipboardService : IClipboardService
    {
        public void SetText(string text)
        {
            UIPasteboard.General.String = text;
        }

        public string GetText()
        {
            return UIPasteboard.General.String;
        }
    }
}