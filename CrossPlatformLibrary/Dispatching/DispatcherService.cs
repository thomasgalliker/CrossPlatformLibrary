using System;
using System.Threading;
using System.Threading.Tasks;

namespace CrossPlatformLibrary.Dispatching
{
    /// <summary>
    /// Provides an implementation of <see cref="IDispatcherService"/> for .Net Standard.
    /// </summary>
    public class DispatcherService : IDispatcherService
    {
        public void CheckBeginInvokeOnUI(Action action)
        {
            Task.Factory.StartNew(action,
                CancellationToken.None, 
                TaskCreationOptions.None,
                TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
