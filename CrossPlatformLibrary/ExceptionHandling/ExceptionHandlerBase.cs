using System;
using System.Threading.Tasks;

using CrossPlatformLibrary.Tools.PlatformSpecific;

namespace CrossPlatformLibrary.ExceptionHandling
{
    public abstract class ExceptionHandlerBase : IExceptionHandler
    {
        protected ExceptionHandlerBase()
        {
            // Subscribe to platform-specific exception handlers
            var platformSpecificExceptionHandler = PlatformAdapter.Resolve<IPlatformSpecificExceptionHandler>();
            platformSpecificExceptionHandler.Attach(this);

            // Ensure unobserved task exceptions (unawaited async methods returning Task or Task<T>) are handled
            // Example: Call this method without 'await'
            // private async Task<string> TestStringTask()
            // {
            //    throw new Exception("TestStringTaskException");
            // }
            TaskScheduler.UnobservedTaskException += this.TaskSchedulerUnobservedTaskException;
        }

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