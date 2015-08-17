using System;

using Android.Runtime;


namespace CrossPlatformLibrary.ExceptionHandling
{
    public class PlatformSpecificExceptionHandler : ExceptionHandlerBase
    {
        protected override void Attach()
        {
            AppDomain.CurrentDomain.UnhandledException += this.CurrentDomainUnhandledException;
            AndroidEnvironment.UnhandledExceptionRaiser += this.AndroidEnvironmentUnhandledExceptionRaiser;


            // Set sync context for ui thread so that async void exceptions can be handled, keeps process alive
            // Example: Call this method with 'await'
            // private async void Test()
            // {
            //    throw new Exception("TestException");
            // }
            AsyncSynchronizationContext.Register(this.ExceptionHandler); // TODO GATH: Move to ExceptionHandlerBase?
        }

        protected override void Detach()
        {
            AppDomain.CurrentDomain.UnhandledException -= this.CurrentDomainUnhandledException;
            AndroidEnvironment.UnhandledExceptionRaiser -= this.AndroidEnvironmentUnhandledExceptionRaiser;
        }

        private void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            if (exception != null)
            {
                this.ExceptionHandler.HandleException(exception);
            }
        }

        private void AndroidEnvironmentUnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs e)
        {
            if (e.Exception != null)
            {
                e.Handled = this.ExceptionHandler.HandleException(e.Exception);
            }
        }
    }
}
