using System;
using System.Windows;

namespace CrossPlatformLibrary.ExceptionHandling
{
    /// <summary>
    ///     Provides an implementation of <see cref="IExceptionHandler" /> for Windows Phone 8 (SL).
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

            this.CheckBeginInvokeOnUI(
                 () =>
                 {
                     if (Application.Current != null)
                     {
                         Application.Current.UnhandledException += this.OnCurrentApplicationUnhandledException;
                     }
                 });
        }

        protected override void Detach()
        {
            AppDomain.CurrentDomain.UnhandledException -= this.CurrentDomainUnhandledException;

            this.CheckBeginInvokeOnUI(
                () =>
                    {
                        if (Application.Current != null)
                        {
                            Application.Current.UnhandledException -= this.OnCurrentApplicationUnhandledException;
                        }
                    });
        }

        private void OnCurrentApplicationUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            e.Handled = this.HandleException(e.ExceptionObject);
        }

        private void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            if (exception != null)
            {
                this.HandleException(exception);
            }
        }

        private void CheckBeginInvokeOnUI(Action action)
        {
            var dispatcher = Deployment.Current.Dispatcher;
            if (dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                dispatcher.BeginInvoke(action);
            }
        }
    }
}