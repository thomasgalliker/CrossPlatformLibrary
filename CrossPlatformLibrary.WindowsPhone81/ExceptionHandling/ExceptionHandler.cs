using Windows.UI.Xaml;

namespace CrossPlatformLibrary.ExceptionHandling
{
    /// <summary>
    /// Provides an implementation of <see cref="IExceptionHandler"/> for Windows Phone 8.1, Windows Store Apps and Universal Windows Platform.
    /// </summary>
    public class ExceptionHandler : ExceptionHandlerBase
    {
        public ExceptionHandler(IExceptionHandlingStrategy exceptionHandlingStrategy)
            : base(exceptionHandlingStrategy)
        {
        }

        private void OnCurrentApplicationUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            e.Handled = this.HandleException(e.Exception);
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
