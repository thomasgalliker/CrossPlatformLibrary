using System;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Sample
{
    class DemoPage : ContentPage
    {
        public DemoPage()
        {
            var titleLabel = new Label { Text = "CrossPlatformLibrary Demo", FontSize = 32, };

            var throwExceptionButton = new Button
            {
                Text = "Throw Unhandled Application Exception"
            };
            throwExceptionButton.Clicked += (sender, args) =>
            {
                throw new InvalidOperationException("Some exception text...");
            };

            var throwTaskExceptionButton = new Button
            {
                Text = "Throw Unobserved Task Exception"
            };
            throwTaskExceptionButton.Clicked += (sender, args) =>
                {
                    Task.Factory.StartNew(() =>
                    {
                        throw new InvalidOperationException("Some unobserved task exception text...");
                    });

                    Task.Delay(2000).ContinueWith(
                        ct =>
                            {
                                // We need to enforce GC manually
                                // so that the GC pushed the unobserved task exceptions to
                                // ExceptionHandlerBase.OnTaskSchedulerUnobservedTaskException
                                GC.Collect();
                                GC.WaitForPendingFinalizers();
                            });
          
                };

            var stackPanel = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children =
                    {
                        titleLabel,
                        throwExceptionButton,
                        throwTaskExceptionButton
                    }
            };

            this.Content = stackPanel;
        }
    }
}
