using System;
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
                this.InitializeComponent();
                this.BindingContext = new MainViewModel();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}