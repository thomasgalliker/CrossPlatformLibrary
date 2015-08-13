using System;
using System.Threading;

using Android.Runtime;

using CrossPlatformLibrary.Utils;

namespace CrossPlatformLibrary.ExceptionHandling
{
    public class PlatformSpecificExceptionHandler : IPlatformSpecificExceptionHandler
    {
        private IExceptionHandler exceptionHandler;

        public void Attach(IExceptionHandler handler)
        {
            Guard.ArgumentNotNull(() => handler);

            Interlocked.Exchange(ref this.exceptionHandler, handler);

            AppDomain.CurrentDomain.UnhandledException += this.CurrentDomainUnhandledException;
            AndroidEnvironment.UnhandledExceptionRaiser += this.AndroidEnvironmentUnhandledExceptionRaiser;


            // Set sync context for ui thread so that async void exceptions can be handled, keeps process alive
            // Example: Call this method with 'await'
            // private async void Test()
            // {
            //    throw new Exception("TestException");
            // }
            AsyncSynchronizationContext.Register(this.exceptionHandler);
        }

        public void Detach()
        {
            AppDomain.CurrentDomain.UnhandledException -= this.CurrentDomainUnhandledException;
            AndroidEnvironment.UnhandledExceptionRaiser -= this.AndroidEnvironmentUnhandledExceptionRaiser;
        }

        private void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            if (exception != null)
            {
                this.exceptionHandler.HandleException(exception);
            }
        }

        private void AndroidEnvironmentUnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs e)
        {
            if (e.Exception != null)
            {
                e.Handled = this.exceptionHandler.HandleException(e.Exception);
            }
        }
    }
}
