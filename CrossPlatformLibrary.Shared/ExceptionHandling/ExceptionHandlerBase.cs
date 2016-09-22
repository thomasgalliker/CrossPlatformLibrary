using System;
using System.Threading.Tasks;

using CrossPlatformLibrary.ExceptionHandling.ExceptionHandlingStrategies;

using Guards;

namespace CrossPlatformLibrary.ExceptionHandling
{
    /// <summary>
    ///     ExceptionHandlerBase is a cross-platform base for attaching to resp. detaching from application wide exception handlers.
    /// </summary>
    public abstract class ExceptionHandlerBase : IExceptionHandler
    {
        private readonly IExceptionHandlingStrategy exceptionHandlingStrategy;

        protected ExceptionHandlerBase(IExceptionHandlingStrategy exceptionHandlingStrategy)
        {
            Guard.ArgumentNotNull(exceptionHandlingStrategy, nameof(exceptionHandlingStrategy));

            this.exceptionHandlingStrategy = exceptionHandlingStrategy;

            // Before attaching we want to make sure, we only have one subscription at the time
            this.InternalDetach();

            // Call Attach in order to wire up platform-specific crash observation
            this.InternalAttach();
        }

        private void InternalAttach()
        {
            this.Attach();

            TaskScheduler.UnobservedTaskException += this.OnTaskSchedulerUnobservedTaskException;

            AsyncSynchronizationContext.Register(this.exceptionHandlingStrategy);
        }

        protected abstract void Attach();

        private void InternalDetach()
        {
            this.Detach();

            TaskScheduler.UnobservedTaskException -= this.OnTaskSchedulerUnobservedTaskException;

            AsyncSynchronizationContext.Register(new RethrowExceptionHandlingStrategy());
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
                var isHandled = this.HandleException(e.Exception);
                if (isHandled)
                {
                    e.SetObserved();
                }
            }
        }

        protected bool HandleException(Exception exception)
        {
            return this.exceptionHandlingStrategy.HandleException(exception);
        }
    }
}