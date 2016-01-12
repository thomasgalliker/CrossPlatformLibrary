using System;

using CrossPlatformLibrary.IoC;

using Xamarin.Forms;

namespace Sample
{
    class DemoPage : ContentPage
    {
        public DemoPage()
        {
            var titleLabel = new Label { Text = "CrossPlatformLibrary Demo", FontSize = 24};

            var button = new Button
            {
                Text = "Throw InvalidOperationException"
            };
            button.Clicked += (sender, args) =>
            {
               throw new InvalidOperationException("Some exception text...");
            };
            
            var stackPanel = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = { titleLabel, button }
            };

            this.Content = stackPanel;
        }
    }
}
