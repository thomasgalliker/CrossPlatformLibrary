using System.Windows;

namespace Sample.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void ButtonThrowExceptionClicked(object sender, RoutedEventArgs e)
        {
            ExceptionHandlingDemo.ThrowApplicationException();
        }

        private void ButtonThrowTaskExceptionClicked(object sender, RoutedEventArgs e)
        {
            ExceptionHandlingDemo.ThrowUnobservedTaskException();
        }
    }
}