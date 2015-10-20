
using System;

namespace CrossPlatformLibrary.ExceptionHandling
{
    public class PlatformSpecificExceptionHandler : ExceptionHandlerBase
    {
        protected override void Attach()
        {
            AppDomain.CurrentDomain.UnhandledException += this.CurrentDomainUnhandledException;
        }

        protected override void Detach()
        {
            AppDomain.CurrentDomain.UnhandledException -= this.CurrentDomainUnhandledException;
        }

        private void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            if (exception != null)
            {
                this.ExceptionHandler.HandleException(exception);
            }
        }
    }
}
