using System;

using Guards;

#if __UNIFIED__
using Foundation;
#else
using MonoTouch.Foundation;
#endif

namespace CrossPlatformLibrary.Dispatching
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