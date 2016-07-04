using System.Windows;

namespace CrossPlatformLibrary.ExceptionHandling
{
    /// <summary>
    /// Provides an implementation of <see cref="IExceptionHandler"/> for Windows Phone 8 (SL).
    /// </summary>
    public class ExceptionHandler : ExceptionHandlerBase
    {
        public ExceptionHandler(IExceptionHandlingStrategy exceptionHandlingStrategy)
            : base(exceptionHandlingStrategy)
        {
        }

        private void OnCurrentApplicationUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            e.Handled = this.HandleException(e.ExceptionObject);
        }

        protected override void Attach()
        {
            if (Application.Current != null)
            {
                Application.Current.UnhandledException += this.OnCurrentApplicationUnhandledException;
            }
        }

        protected override void Detach()
        {
            if (Application.Current != null)
            {
                Application.Current.UnhandledException -= this.OnCurrentApplicationUnhandledException;
            }
        }
    }
}
