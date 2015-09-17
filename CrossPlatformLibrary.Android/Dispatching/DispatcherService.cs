using System;

using Android.App;

using Guards;

namespace CrossPlatformLibrary.Dispatching
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