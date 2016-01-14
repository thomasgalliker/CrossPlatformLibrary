using System;
using System.Threading;

using Android.App;

using Guards;

namespace CrossPlatformLibrary.Dispatching
{
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