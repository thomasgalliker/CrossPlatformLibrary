using System;

using Android.App;

using CrossPlatformLibrary.Dispatching;
using CrossPlatformLibrary.Utils;

namespace Xamarin.Dispatching
{
    public class DispatcherService : IDispatcherService
    {
        public void CheckBeginInvokeOnUI(Action action)
        {
            Guard.ArgumentNotNull(() => action);

            Application.SynchronizationContext.Post(_ => action(), null);
        }
    }
}