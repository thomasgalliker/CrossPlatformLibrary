using System.Threading;
using System.Windows;

using CrossPlatformLibrary.Utils;

namespace CrossPlatformLibrary.ExceptionHandling
{
    public class PlatformSpecificExceptionHandler : IPlatformSpecificExceptionHandler
    {
        private IExceptionHandler exceptionHandler;

        private void OnCurrentApplicationUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            e.Handled = this.exceptionHandler.HandleException(e.ExceptionObject);
        }

        public void Attach(IExceptionHandler handler)
        {
            Guard.ArgumentNotNull(() => handler);

            Interlocked.Exchange(ref this.exceptionHandler, handler);

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
            AsyncSynchronizationContext.Register(this.exceptionHandler);
        }

        public void Detach()
        {
            if (Application.Current != null)
            {
                Application.Current.UnhandledException -= this.OnCurrentApplicationUnhandledException;
            }
        }
    }
}
