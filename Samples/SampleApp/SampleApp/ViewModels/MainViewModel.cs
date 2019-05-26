using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CrossPlatformLibrary.Forms.Mvvm;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private CountryViewModel country;
        private bool isBusy;
        private string notes;
        private string adminEmailAddress;

        private int numberOfLoads = 0;
        private ICommand saveProfileButtonCommand;
        private ICommand loadDataButtonCommand;

        public MainViewModel()
        {
            this.ViewModelError = ViewModelError.None;
            this.Countries = new ObservableCollection<CountryViewModel>();
            this.LoadData();
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

        public string Notes
        {
            get => this.notes;
            set
            {
                this.notes = value;
                this.OnPropertyChanged(nameof(this.Notes));
            }
        }

        public string AdminEmailAddress
        {
            get => this.adminEmailAddress;
            set
            {
                this.adminEmailAddress = value;
                this.OnPropertyChanged(nameof(this.AdminEmailAddress));
            }
        }

        public ICommand MailNavigateCommand => new Command(this.OnMailNavigate);

        private void OnMailNavigate()
        {
            Console.WriteLine($"Send mail to {this.AdminEmailAddress}");
        }

        public ICommand PostalCodeUnfocusedCommand => new Command(this.OnPostalCodeUnfocused);

        private void OnPostalCodeUnfocused()
        {
            Console.WriteLine("unfocused");
        }

        public ICommand SaveProfileButtonCommand
        {
            get
            {
                return this.saveProfileButtonCommand ??
                       (this.saveProfileButtonCommand = new Command(async () => await this.OnSaveProfile()));
            }
        }

        private async Task OnSaveProfile()
        {
            this.IsBusy = true;
            await Task.Delay(1000);
            this.IsBusy = false;
        }

        public ICommand LoadDataButtonCommand
        {
            get
            {
                return this.loadDataButtonCommand ??
                       (this.loadDataButtonCommand = new Command(async () => await this.LoadData()));
            }
        }

        private async Task LoadData()
        {
            this.IsBusy = true;
            this.ViewModelError = ViewModelError.None;
            await Task.Delay(1000);

            try
            {
                this.numberOfLoads++;

                if (this.numberOfLoads % 2 == 0)
                {
                    // Simulate a data load exception
                    throw new InvalidOperationException("Failed to load data. Try again.");
                }

                this.Countries.Clear();
                this.Countries.Add(new CountryViewModel { Id = 1, Name = "Switzerland" });
                this.Countries.Add(new CountryViewModel { Id = 2, Name = "Germany" });
                this.Countries.Add(new CountryViewModel { Id = 3, Name = "USA" });

                //this.Notes = $"Test test test{Environment.NewLine}Line 2 text text text";
                this.AdminEmailAddress = "thomas@bluewin.ch";
            }
            catch (Exception ex)
            {
                this.ViewModelError = new ViewModelError(ex.Message, this.LoadDataButtonCommand);
            }

            this.IsBusy = false;
        }
    }
}