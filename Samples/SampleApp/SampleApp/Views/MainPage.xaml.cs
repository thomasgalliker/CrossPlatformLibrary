using System;
using System.Threading.Tasks;
using SampleApp.ViewModels;
using Xamarin.Forms;

namespace SampleApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            try
            {
                var displayService = new DisplayService((t, m) => this.DisplayAlert(t, m, "OK"));
                this.InitializeComponent();
                this.BindingContext = new MainViewModel(displayService);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }

    public class DisplayService : IDisplayService
    {
        private readonly Func<string, string, Task> alertAction;

        public DisplayService(Func<string, string, Task> alertAction)
        {
            this.alertAction = alertAction;
        }
        public async Task DisplayAlert(string title, string message)
        {
            await this.alertAction(title, message);
        }
    }
    public interface IDisplayService
    {
        Task DisplayAlert(string title, string message);
    }
}