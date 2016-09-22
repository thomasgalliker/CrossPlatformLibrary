
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
                ExceptionHandlingDemo.ThrowApplicationException();
            };

            var throwTaskExceptionButton = new Button
            {
                Text = "Throw Unobserved Task Exception"
            };
            throwTaskExceptionButton.Clicked += (sender, args) =>
                {
                    ExceptionHandlingDemo.ThrowUnobservedTaskException();
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
