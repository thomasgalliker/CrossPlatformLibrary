using Windows.UI.Xaml;

namespace CrossPlatformLibrary.ExceptionHandling
{
    public class PlatformSpecificExceptionHandler : ExceptionHandlerBase
    {
        private void OnCurrentApplicationUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            e.Handled = this.ExceptionHandler.HandleException(e.Exception);
        }

        protected override void Attach()
        {
            if (Application.Current != null)
            {
                Application.Current.UnhandledException += this.OnCurrentApplicationUnhandledException;
            }

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
            if (Application.Current != null)
            {
                Application.Current.UnhandledException -= this.OnCurrentApplicationUnhandledException;
            }
        }
    }
}
