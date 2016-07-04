using System;

using Android.Runtime;

namespace CrossPlatformLibrary.ExceptionHandling
{
    public class ExceptionHandler : ExceptionHandlerBase
    {
        public ExceptionHandler(IExceptionHandlingStrategy exceptionHandlingStrategy)
            : base(exceptionHandlingStrategy)
        {
        }

        protected override void Attach()
        {
            AppDomain.CurrentDomain.UnhandledException += this.CurrentDomainUnhandledException;
            AndroidEnvironment.UnhandledExceptionRaiser += this.AndroidEnvironmentUnhandledExceptionRaiser;
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
                this.HandleException(exception);
            }
        }

        private void AndroidEnvironmentUnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs e)
        {
            if (e.Exception != null)
            {
                e.Handled = this.HandleException(e.Exception);
            }
        }
    }
}
