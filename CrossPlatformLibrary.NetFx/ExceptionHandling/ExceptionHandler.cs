
using System;
using System.Windows;
using System.Windows.Threading;

namespace CrossPlatformLibrary.ExceptionHandling
{
    /// <summary>
    /// Provides an implementation of <see cref="IExceptionHandler"/> for .Net Framework.
    /// </summary>
    public class ExceptionHandler : ExceptionHandlerBase
    {
        public ExceptionHandler(IExceptionHandlingStrategy exceptionHandlingStrategy)
            : base(exceptionHandlingStrategy)
        {
        }

        protected override void Attach()
        {
            AppDomain.CurrentDomain.UnhandledException += this.CurrentDomainUnhandledException;

            if (Application.Current != null)
            {
                Application.Current.DispatcherUnhandledException += this.CurrentOnDispatcherUnhandledException;
            }
        }

        protected override void Detach()
        {
            AppDomain.CurrentDomain.UnhandledException -= this.CurrentDomainUnhandledException;

            if (Application.Current != null)
            {
                Application.Current.DispatcherUnhandledException -= this.CurrentOnDispatcherUnhandledException;
            }
        }

        private void CurrentOnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var exception = e.Exception;
            if (exception != null)
            {
                e.Handled = this.HandleException(exception);
            }
        }

        private void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            if (exception != null)
            {
                this.HandleException(exception);
            }
        }
    }
}
