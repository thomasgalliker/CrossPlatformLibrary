using System;

using CrossPlatformLibrary.Internals;

#if __UNIFIED__
using Foundation;
#else
using MonoTouch.Foundation;
#endif

namespace CrossPlatformLibrary.Dispatching
{
    /// <summary>
    /// Provides an implementation of <see cref="IDispatcherService"/> for iOS.
    /// </summary>
    public class DispatcherService : IDispatcherService
    {
        private readonly NSObject dispatcher = new NSObject();

        public void CheckBeginInvokeOnUI(Action action)
        {
            Guard.ArgumentNotNull(action, nameof(action));

            if (NSThread.Current.IsMainThread)
            {
                action();
            }
            else
            {
                this.dispatcher.BeginInvokeOnMainThread(() => action());
            }
        }
    }
}