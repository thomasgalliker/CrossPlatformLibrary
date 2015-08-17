using System.Threading.Tasks;

using CrossPlatformLibrary.Utils;

namespace CrossPlatformLibrary.ExceptionHandling
{
    public abstract class ExceptionHandlerBase : IPlatformSpecificExceptionHandler
    {
        protected IExceptionHandler ExceptionHandler;

        public void RegisterExceptionHandler(IExceptionHandler targetExceptionHandler)
        {
            Guard.ArgumentNotNull(() => targetExceptionHandler);

            this.ExceptionHandler = targetExceptionHandler;

            // Before attaching we want to make sure, we only have one subscription at the time
            this.InternalDetach();

            // Call Attach in order to wire up platform-specific crash observation
            this.InternalAttach();
        }

        private void InternalAttach()
        {
            this.Attach();

            TaskScheduler.UnobservedTaskException += this.OnTaskSchedulerUnobservedTaskException;
        }

        protected abstract void Attach();

        private void InternalDetach()
        {
            this.Detach();

            TaskScheduler.UnobservedTaskException -= this.OnTaskSchedulerUnobservedTaskException;
        }

        protected abstract void Detach();

        // Ensure unobserved task exceptions (unawaited async methods returning Task or Task<T>) are handled
        // Example: Call this method without 'await'
        // private async Task<string> TestStringTask()
        // {
        //    throw new Exception("TestStringTaskException");
        // }
        private void OnTaskSchedulerUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            if (e.Exception != null)
            {
                var isHandled = this.ExceptionHandler.HandleException(e.Exception);
                if (isHandled)
                {
                    e.SetObserved();
                }
            }
        }
    }
}