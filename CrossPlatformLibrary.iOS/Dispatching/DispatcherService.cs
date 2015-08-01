

using System;

using CrossPlatformLibrary.Dispatching;
using CrossPlatformLibrary.Utils;
#if __UNIFIED__
using Foundation;
#else
using MonoTouch.Foundation;
#endif

namespace Xamarin.Dispatching
{
    public class DispatcherService : IDispatcherService
    {
        private readonly NSObject dispatcher = new NSObject();

        public void CheckBeginInvokeOnUI(Action action)
        {
            Guard.ArgumentNotNull(() => action);

            this.dispatcher.BeginInvokeOnMainThread(() => action());
        }
    }
}