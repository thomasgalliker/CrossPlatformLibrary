using System;
using System.Threading;

namespace CrossPlatformLibrary.Dispatching
{
    public static class DispatcherExtensions
    {
        public static void Invoke(this IDispatcherService dispatcher, Action action) // TODO GATH: Write tests! Create base class with common functionality
        {
            Exception exception = null;
            var waitEvent = new ManualResetEvent(false);

            dispatcher.CheckBeginInvokeOnUI(
                () =>
                {
                    try
                    {
                        action();
                    }
                    catch (Exception ex)
                    {
                        exception = ex;
                    }
                    waitEvent.Set();
                });

            waitEvent.WaitOne();
            if (exception != null)
            {
                throw exception;
            }
        }
    }
}