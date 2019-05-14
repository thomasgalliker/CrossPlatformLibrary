using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CrossPlatformLibrary.Forms.Controls;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class MainViewModel : BindableObject
    {
        private CountryViewModel country;
        private ViewModelError viewModelError;
        private bool isBusy;

        public MainViewModel()
        {
            this.ViewModelError = ViewModelError.None;
            this.Countries = new ObservableCollection<CountryViewModel>
            {
                new CountryViewModel{Id = 1, Name = "Switzerland"},
                new CountryViewModel{Id = 2, Name = "Germany"},
                new CountryViewModel{Id = 3, Name = "France"},
            };
        }

        public ObservableCollection<CountryViewModel> Countries { get; }

        public CountryViewModel Country
        {
            get => this.country;
            set
            {
                this.country = value;
                this.OnPropertyChanged(nameof(this.Country));
            }
        }

        public ICommand PostalCodeUnfocusedCommand => new Command(this.OnPostalCodeUnfocused);

        private void OnPostalCodeUnfocused()
        {
            Console.WriteLine("unfocused");
        }

        public ICommand SaveProfileButtonCommand => new Command(async () => await this.OnSaveProfile());

        private async Task OnSaveProfile()
        {
            this.IsBusy = true;
            await Task.Delay(1000);
            this.IsBusy = false;
        }


        public bool IsBusy
        {
            get => this.isBusy;
            set
            {
                this.isBusy = value;
                this.OnPropertyChanged(nameof(this.IsBusy));
                this.OnPropertyChanged(nameof(this.IsNotBusy));
            }
        }

        public bool IsNotBusy => !this.isBusy;

        public ViewModelError ViewModelError
        {
            get { return this.viewModelError; }
            set
            {
                this.viewModelError = value;
                this.OnPropertyChanged(nameof(this.ViewModelError));
                this.OnPropertyChanged(nameof(this.HasViewModelError));
            }
        }

        public bool HasViewModelError => this.IsNotBusy && this.viewModelError.HasError;

        protected void SetViewModelError(string text, ICommand command)
        {
            this.ViewModelError = new ViewModelError(text, command);
        }
    }
}