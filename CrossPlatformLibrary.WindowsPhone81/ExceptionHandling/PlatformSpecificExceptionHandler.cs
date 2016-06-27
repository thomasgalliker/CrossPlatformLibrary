using Windows.UI.Xaml;

namespace CrossPlatformLibrary.ExceptionHandling
{
    public class PlatformSpecificExceptionHandler : ExceptionHandlerBase
    {
        public PlatformSpecificExceptionHandler(IExceptionHandlingStrategy exceptionHandlingStrategy)
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
