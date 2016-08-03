using System;

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
                Text = "Throw InvalidOperationException"
            };
            throwExceptionButton.Clicked += (sender, args) =>
            {
                throw new InvalidOperationException("Some exception text...");
            };

            var stackPanel = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = { titleLabel, throwExceptionButton }
            };

            this.Content = stackPanel;
        }
    }
}
