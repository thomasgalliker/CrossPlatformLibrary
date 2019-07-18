using Foundation;
using UIKit;

namespace CrossPlatformLibrary.Services
{
    public class ToastService : IToastService
    {
        private const double LongDelay = 3.5;
        private const double ShortDelay = 2.0;

        NSTimer alertDelay;
        UIAlertController alert;

        public void LongAlert(string message)
        {
            this.ShowAlert(message, LongDelay);
        }

        public void ShortAlert(string message)
        {
            this.ShowAlert(message, ShortDelay);
        }

        void ShowAlert(string message, double seconds)
        {
            this.alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) => { this.DismissMessage(); });
            this.alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(this.alert, true, null);
        }

        private void DismissMessage()
        {
            if (this.alert != null)
            {
                this.alert.DismissViewController(true, null);
            }

            if (this.alertDelay != null)
            {
                this.alertDelay.Dispose();
            }
        }
    }
}