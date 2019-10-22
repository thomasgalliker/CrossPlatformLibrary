﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CrossPlatformLibrary.Extensions;
using CrossPlatformLibrary.Forms.Mvvm;
using CrossPlatformLibrary.Forms.Validation;
using CrossPlatformLibrary.Tools;
using SampleApp.Model;
using SampleApp.Services;
using SampleApp.Validation;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly DisplayService displayService;
        private readonly ICountryService countryService;
        private readonly IValidationService validationService;
        private CountryViewModel country;
        private string notes;
        private string adminEmailAddress;

        private int numberOfLoads = 0;
        private ICommand saveProfileButtonCommand;
        private ICommand loadDataButtonCommand;
        private UserDto user;
        private string logContent;
        private ICommand toggleSwitchCommand;
        private bool isToggled;
        private ICommand longPressCommand;
        private ICommand normalPressCommand;
        private string countrySearchText;
        private ICommand autoCompleteSearchCommand;
        private ObservableCollection<CountryViewModel> countries;
        private int userNameMaxLength;
        private DateTime? birthdate;
        private bool isSaving;
        private ObservableCollection<ColorResource> themeColors;

        public MainViewModel(
            DisplayService displayService,
            ICountryService countryService,
            IValidationService validationService)
        {
            this.displayService = displayService;
            this.countryService = countryService;
            this.validationService = validationService;

            this.ViewModelError = ViewModelError.None;
            this.User = new UserDto();
            this.Countries = new ObservableCollection<CountryViewModel>();
            this.SuggestedCountries = new ObservableCollection<CountryViewModel>();

            this.PeriodicTask = new PeriodicTaskViewModel();
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
            get => this.User?.Id ?? 0;
            set => this.SetProperty(this.User, value, nameof(this.UserId), nameof(this.User.Id)); // Sync property value based on specified string
        }

        public string UserName
        {
            get => this.User?.UserName;
            set => this.SetProperty(this.User, value); // Sync property value based on property name
        }

        public int UserNameMaxLength
        {
            get => this.userNameMaxLength;
            set => this.SetProperty(ref this.userNameMaxLength, value, nameof(this.UserNameMaxLength));
        }

        public DateTime? Birthdate
        {
            get => this.birthdate;
            set => this.SetProperty(ref this.birthdate, value, nameof(this.Birthdate));
        }

        public ObservableCollection<CountryViewModel> Countries
        {
            get => this.countries;
            private set => this.SetProperty(ref this.countries, value, nameof(this.Countries));
        }

        public ObservableCollection<CountryViewModel> SuggestedCountries { get; }

        public CountryViewModel Country
        {
            get => this.country;
            set
            {
                this.country = value;
                this.RaisePropertyChanged(nameof(this.Country));
            }
        }

        public string CountrySearchText
        {
            get => this.countrySearchText;
            set
            {
                if (this.SetProperty(ref this.countrySearchText, value, nameof(this.CountrySearchText)))
                {
                    // Use SearchText property binding to filter local data sources...
                    Console.WriteLine($"CountrySearchText changed: {value}");
                }
            }
        }

        public ICommand AutoCompleteSearchCommand
        {
            get
            {
                return this.autoCompleteSearchCommand ??
                       (this.autoCompleteSearchCommand = new Command<string>(async (s) => await this.OnAutoCompleteSearch(s)));
            }
        }

        private async Task OnAutoCompleteSearch(string searchText)
        {
            // Use SearchCommand to run sync/async queries against a backend data source, etc...

            this.SuggestedCountries.Clear();

            if (!string.IsNullOrEmpty(searchText))
            {
                var filteredViewModels = this.Countries.Where(c => c.Name != null && c.Name.StartsWith(searchText, StringComparison.InvariantCultureIgnoreCase))
                    .OrderBy(c => c.Name)
                    .Take(10);
                this.SuggestedCountries.AddRange(filteredViewModels);
            }
        }

        public string Notes
        {
            get => this.notes;
            set
            {
                this.notes = value;
                this.RaisePropertyChanged(nameof(this.Notes));
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
                this.RaisePropertyChanged(nameof(this.AdminEmailAddress));
            }
        }

        public ICommand NormalPressCommand
        {
            get
            {
                return this.normalPressCommand ??
                       (this.normalPressCommand = new Command<string>(async (message) => await this.displayService.DisplayAlert("NormalPressCommand", message)));
            }
        }

        public ICommand LongPressCommand
        {
            get
            {
                return this.longPressCommand ??
                       (this.longPressCommand = new Command<string>(async (message) => await this.displayService.DisplayAlert("LongPressCommand", message)));
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

        public ICommand CalloutCommand => new Command<string>(this.OnCalloutCommand);

        private async void OnCalloutCommand(string parameter)
        {
            await this.displayService.DisplayAlert("CalloutCommand", $"parameter: {parameter}");
        }

        public ICommand SetFantasyLandCommand => new Command(this.OnSetFantasyLand);

        private void OnSetFantasyLand()
        {
            // Since none of the Countries are IEquitable<> to "Fantasy Land", the UI controls binding to Country
            // need to react properly: Bindable Picker switches to state 'nothing selected'.
            this.Country = new CountryViewModel(new CountryDto { Id = 99, Name = "Fantasy Land" });
        }

        public PeriodicTaskViewModel PeriodicTask { get; private set; }

        public ObservableCollection<ColorResource> ThemeColors
        {
            get => this.themeColors;
            private set => this.SetProperty(ref this.themeColors, value, nameof(this.ThemeColors));
        }


        protected override void OnBusyChanged(bool busy)
        {
            this.RaisePropertyChanged(nameof(this.CanExecuteSaveProfileButtonCommand));
            this.RaisePropertyChanged(nameof(this.CanExecuteLoadDataButtonCommand));
        }

        public bool CanExecuteSaveProfileButtonCommand => this.IsNotBusy && !this.IsSaving;

        public bool IsSaving
        {
            get => this.isSaving;
            set
            {
                if (this.SetProperty(ref this.isSaving, value, nameof(this.IsSaving)))
                {
                    this.RaisePropertyChanged(nameof(this.CanExecuteSaveProfileButtonCommand));
                    this.RaisePropertyChanged(nameof(this.CanExecuteLoadDataButtonCommand));
                }
            }
        }

        public ICommand SaveProfileButtonCommand
        {
            get
            {
                return this.saveProfileButtonCommand ??
                       (this.saveProfileButtonCommand = new Command(async () => await this.OnSaveProfile(), () => this.CanExecuteSaveProfileButtonCommand));
            }
        }

        private async Task OnSaveProfile()
        {
            this.IsSaving = true;
            await Task.Delay(1000);

            var isValid = await this.Validation.IsValidAsync();
            if (isValid)
            {
                // TODO Save...
            }

            this.IsSaving = false;
        }

        protected override async Task OnRefreshList()
        {
            await Task.Delay(1000);
        }

        public bool CanExecuteLoadDataButtonCommand => this.IsNotBusy && !this.IsSaving;

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
            await Task.Delay(3000);

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

                // Demo dynamic adjustment of MaxLength binding
                this.UserNameMaxLength = Math.Max(10, ++this.UserNameMaxLength);

                this.LogContent = this.logContent + Environment.NewLine + $"{DateTime.Now:G} LoadData called";
                if (this.numberOfLoads % 2 == 0)
                {
                    // Simulate a data load exception
                    throw new InvalidOperationException("Failed to load data. Try again.");
                }

                var defaultCountryViewModel = new CountryViewModel(new CountryDto { Name = null });
                var countryDtos = (await this.countryService.GetAllAsync()).ToList();

                // Set countries all at once
                this.Countries.Clear();
                this.Countries = new ObservableCollection<CountryViewModel>(countryDtos.Select(c => new CountryViewModel(c)).Prepend(defaultCountryViewModel));

                // Set countries one after the other
                this.Countries.Clear();
                this.Countries.AddRange(countryDtos.Select(c => new CountryViewModel(c)).Prepend(defaultCountryViewModel));

                this.ThemeColors = App.Current.Resources.MergedDictionaries.SelectMany(md => md)
                    .Where(r => r.Value is Color)
                    .Select(c => new ColorResource(c))
                    .OrderBy(c => c.Id)
                    .ToObservableCollection();

                //this.Notes = $"Test test test{Environment.NewLine}Line 2 text text text";
                this.AdminEmailAddress = "thomas@bluewin.ch";
            }
            catch (Exception ex)
            {
                this.ViewModelError = new ViewModelError(ex.Message, this.LoadDataButtonCommand);
            }

            this.IsBusy = false;
        }

        protected override ViewModelValidation SetupValidation()
        {
            var viewModelValidation = new ViewModelValidation();

            viewModelValidation.AddValidationFor(nameof(this.UserName))
                .When(() => string.IsNullOrWhiteSpace(this.UserName))
                .Show(() => $"Username must not be empty");

            viewModelValidation.AddValidationFor(nameof(this.Birthdate))
                .When(() => this.Birthdate == null)
                .Show(() => $"Birthdate must be set");

            viewModelValidation.AddDelegateValidation(nameof(this.UserName))
                .Validate(async () => (await this.validationService.ValidatePersonAsync(this.CreatePerson())).Errors, validationDelay: TimeSpan.FromMilliseconds(1000));

            return viewModelValidation;
        }

        private PersonDto CreatePerson()
        {
            return new PersonDto
            {
                UserName = this.UserName
            };
        }

        public string RefreshButtonText
        {
            get { return $"Refresh {this.numberOfLoads}"; }
        }

        public bool IsToggled
        {
            get => this.isToggled;
            set
            {
                if (this.SetProperty(ref this.isToggled, value, nameof(this.IsToggled)))
                {
                    this.RaisePropertyChanged(nameof(this.ToggleSwitchButtonText));
                }
            }
        }

        public string ToggleSwitchButtonText => this.IsToggled ? "IsToggled: Yes" : "IsToggled: No";

        public ICommand ToggleSwitchCommand
        {
            get
            {
                return this.toggleSwitchCommand ??
                       (this.toggleSwitchCommand = new Command(() => { this.IsToggled = !this.IsToggled; }));
            }
        }
    }
}