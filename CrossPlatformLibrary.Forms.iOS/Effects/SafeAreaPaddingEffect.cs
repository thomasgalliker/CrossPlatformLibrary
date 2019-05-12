using System;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace CrossPlatformLibrary.Forms.iOS.Effects
{
    public abstract class SafeAreaPaddingEffect : PlatformEffect, IDisposable
    {
        private Thickness originalPadding;
        private NSObject orientationObserver;
        private bool disposed;

        private void DeviceOrientationDidChange(NSNotification obj)
        {
            this.UpdatePadding();
        }

        private void SetPadding(Thickness padding)
        {
            if (this.Element is Layout element)
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                {
                    var insets = UIApplication.SharedApplication.Windows[0].SafeAreaInsets; // Can't use KeyWindow this early
                    if (insets.Top > 0) // We have a notch
                    {
                        element.Padding = this.GetPadding(padding, insets);
                        return;
                    }
                }

                // Uses a default Padding of 20. Could use an property to modify if you wanted.
                element.Padding = this.GetDefaultPadding(padding);
            }
        }

        protected abstract Thickness GetPadding(Thickness padding, UIEdgeInsets insets);

        protected abstract Thickness GetDefaultPadding(Thickness padding);

        private void UpdatePadding()
        {
            this.SetPadding(this.originalPadding);
        }

        protected override void OnAttached()
        {
            this.orientationObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIApplication.DidChangeStatusBarOrientationNotification, this.DeviceOrientationDidChange);
            UIDevice.CurrentDevice.BeginGeneratingDeviceOrientationNotifications();

            if (this.Element is Layout element)
            {
                this.originalPadding = element.Padding;
            }

            this.UpdatePadding();
        }

        protected override void OnDetached()
        {
            if (this.Element is Layout element)
            {
                element.Padding = this.originalPadding;
            }

            NSNotificationCenter.DefaultCenter.RemoveObserver(this.orientationObserver);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing && this.orientationObserver != null)
                {
                    NSNotificationCenter.DefaultCenter.RemoveObserver(this.orientationObserver);
                }

                this.disposed = true;
            }
        }
    }
}