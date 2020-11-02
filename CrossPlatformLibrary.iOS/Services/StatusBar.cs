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
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                // If VS has updated to the latest version , you can use StatusBarManager , else use the first line code
                // UIView statusBar = new UIView(UIApplication.SharedApplication.StatusBarFrame);
                UIView statusBar = new UIView(UIApplication.SharedApplication.KeyWindow.WindowScene.StatusBarManager.StatusBarFrame);
                statusBar.BackgroundColor = UIColor.Red;
                UIApplication.SharedApplication.KeyWindow.AddSubview(statusBar);
            }
            else
            {
                UIView statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
                if (statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
                {
                    statusBar.BackgroundColor = UIColor.Red;
                    UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.BlackOpaque;
                }
            }
        }
    }
}