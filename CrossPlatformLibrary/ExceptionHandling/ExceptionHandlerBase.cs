using System;
using System.Threading.Tasks;

using CrossPlatformLibrary.Utils;

namespace CrossPlatformLibrary.ExceptionHandling
{
    public abstract class ExceptionHandlerBase : IExceptionHandler
    {
        protected ExceptionHandlerBase(IPlatformSpecificExceptionHandler platformSpecificExceptionHandler)
        {
            Guard.ArgumentNotNull(() => platformSpecificExceptionHandler);

            // Subscribe to platform-specific exception handlers
            platformSpecificExceptionHandler.Detach();
            platformSpecificExceptionHandler.Attach(this);

            TaskScheduler.UnobservedTaskException += this.TaskSchedulerUnobservedTaskException;
        }

        // Ensure unobserved task exceptions (unawaited async methods returning Task or Task<T>) are handled
        // Example: Call this method without 'await'
        // private async Task<string> TestStringTask()
        // {
        //    throw new Exception("TestStringTaskException");
        // }
        private void TaskSchedulerUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            if (e.Exception != null)
            {
                var isHandled = this.HandleException(e.Exception);
                if (isHandled)
                {
                    e.SetObserved();
                }
            }
        }

        public abstract bool HandleException(Exception exception);
    }
}