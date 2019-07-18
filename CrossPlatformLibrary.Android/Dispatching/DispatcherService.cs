using System;
using System.Threading;

using Android.App;
using CrossPlatformLibrary.Internals;

namespace CrossPlatformLibrary.Dispatching
{
    /// <summary>
    /// Provides an implementation of <see cref="IDispatcherService"/> for Android.
    /// </summary>
    public class DispatcherService : IDispatcherService
    {
        public void CheckBeginInvokeOnUI(Action dispatchAction)
        {
            Guard.ArgumentNotNull(dispatchAction, "dispatchAction");

            if (Application.SynchronizationContext == SynchronizationContext.Current)
            {
                dispatchAction();
            }
            else
            {
                Application.SynchronizationContext.Post(_ => dispatchAction(), null);
            }
        }
    }
}