using System;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Sample.UWP
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            throw new InvalidOperationException("Some exception text...");
        }
    }
}