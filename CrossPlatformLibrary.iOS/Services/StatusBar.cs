using System;
using Foundation;
using UIKit;

namespace CrossPlatformLibrary.Services
{
    public class StatusBar : IStatusBar
    {
        public string GetColor()
        {
            throw new NotImplementedException();
        }

        public void SetColor(string hexColor)
        {
            var statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
            if (statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
            {
                //statusBar.BackgroundColor = new UIColor((hexColor);
            }
        }
    }
}