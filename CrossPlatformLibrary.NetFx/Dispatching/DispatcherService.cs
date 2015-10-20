using System;
using System.Windows.Threading;

namespace CrossPlatformLibrary.Dispatching
{
    public class DispatcherService : IDispatcherService
    {
        private readonly Dispatcher dispatcher;

        public DispatcherService()
        {
            this.dispatcher = Dispatcher.CurrentDispatcher;
        }

        public void CheckBeginInvokeOnUI(Action action)
        {
            if (this.dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                this.dispatcher.BeginInvoke(action);
            }
        }
    }
}
