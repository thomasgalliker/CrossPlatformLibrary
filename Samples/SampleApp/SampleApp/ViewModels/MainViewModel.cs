﻿using System;
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
        private string notes;
        private string adminEmailAddress;

        private int numberOfLoads = 0;
        private ICommand saveProfileButtonCommand;
        private ICommand loadDataButtonCommand;
        private UserDto user;
        private string logContent;

        public MainViewModel()
        {
            this.ViewModelError = ViewModelError.None;
            this.User = new UserDto();
            this.Countries = new ObservableCollection<CountryViewModel>();
            this.LoadData();
        }

        private UserDto User
        {
            get => this.user;
            set
            {
                if (this.SetProperty(ref this.user, value, nameof(this.User)))
                {
                    this.RaisePropertyChanged(nameof(this.UserId));
                    this.RaisePropertyChanged(nameof(this.UserName));
                }
            }
        }

        public int UserId
        {
            get => this.User.Id;
            set => this.SetProperty(this.User, value, nameof(this.UserId), nameof(this.User.Id));
        }

        public string UserName
        {
            get => this.User.UserName;
            set => this.SetProperty(this.User, value);
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

        public string LogContent
        {
            get => this.logContent;
            set => this.SetProperty(ref this.logContent, value, nameof(this.LogContent));
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
                this.User = new UserDto
                {
                    Id = 1,
                    UserName = "thomasgalliker"
                };
                this.UserId = 2;

                this.numberOfLoads++;
                this.RaisePropertyChanged(nameof(this.RefreshButtonText));

                this.LogContent = this.logContent + Environment.NewLine + $"{DateTime.Now:G} LoadData called";
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

        public string RefreshButtonText
        {
            get { return $"Refresh {this.numberOfLoads}"; }
        }
    }
}