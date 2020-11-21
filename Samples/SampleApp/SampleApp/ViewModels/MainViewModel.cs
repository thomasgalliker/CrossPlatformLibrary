using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CrossPlatformLibrary.Extensions;
using CrossPlatformLibrary.Forms.Mvvm;
using CrossPlatformLibrary.Forms.Services;
using CrossPlatformLibrary.Forms.Validation;
using CrossPlatformLibrary.Localization;
using SampleApp.Model;
using SampleApp.Services;
using SampleApp.Validation;
using SampleApp.Views;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IDisplayService displayService;
        private readonly ICountryService countryService;
        private readonly IValidationService validationService;
        private readonly IEmailService emailService;
        private readonly ILocalizer localizer;
        private readonly IActivityIndicatorService activityIndicatorService;
        private CountryViewModel country;
        private string notes;
        private string adminEmailAddress;

        private int numberOfLoads = 0;
        private ICommand saveProfileButtonCommand;
        private ICommand loadDataButtonCommand;
        private UserDto user;
        private string logContent;
        private ICommand toggleSwitchCommand;
        private bool isReadonly;
        private ICommand longPressCommand;
        private ICommand normalPressCommand;
        private string countrySearchText;
        private ICommand autoCompleteSearchCommand;
        private ObservableCollection<CountryViewModel> countries;
        private int userNameMaxLength;
        private DateTime? birthdate;
        private bool isSaving;
        private ObservableCollection<ResourceViewModel> themeResources;
        private bool isMultiToggleButtonOn = true;
        private bool isMultiToggleButtonOff;
        private ICommand navigateToPageCommand;
        private ICommand showActivityIndicatorCommand;
        private LanguageViewModel language;

        public MainViewModel(
            INavigationService navigationService,
            IDisplayService displayService,
            ICountryService countryService,
            IValidationService validationService,
            IEmailService emailService,
            ILocalizer localizer,
            IActivityIndicatorService activityIndicatorService)
        {
            this.navigationService = navigationService;
            this.displayService = displayService;
            this.countryService = countryService;
            this.validationService = validationService;
            this.emailService = emailService;
            this.localizer = localizer;
            this.activityIndicatorService = activityIndicatorService;

            this.ViewModelError = ViewModelError.None;
            this.User = new UserDto();
            this.Countries = new ObservableCollection<CountryViewModel>();
            this.SuggestedCountries = new ObservableCollection<CountryViewModel>();

            this.Languages = new ObservableCollection<LanguageViewModel>
            {
                new LanguageViewModel(new System.Globalization.CultureInfo("en")),
                new LanguageViewModel(new System.Globalization.CultureInfo("de")),
            };
            this.language = this.Languages.First();

            this.LoadData();
        }

        public ObservableCollection<LanguageViewModel> Languages { get; }

        public LanguageViewModel Language
        {
            get
            {
                return this.language;
            }
            set
            {
                this.language = value;

                if (value != null)
                {
                    var cultureInfo = value.Dto;

                    this.localizer.SetCultureInfo(cultureInfo);

                    CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
                    CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
                }

                // Update all bindings
                this.RaisePropertyChanged("");
            }
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

        public ICommand NavigateToPageCommand
        {
            get
            {
                return this.navigateToPageCommand ??
                       (this.navigateToPageCommand = new Command<string>(async (s) => await this.OnNavigateToPage(s)));
            }
        }

        private async Task OnNavigateToPage(string pageName)
        {
            Page page = null;
            switch (pageName)
            {
                case nameof(SegmentedStatusIndicatorPage):
                    page = new SegmentedStatusIndicatorPage { BindingContext = new SegmentedStatusIndicatorViewModel() };
                    break;

                case nameof(CardViewPage):
                    page = new CardViewPage { BindingContext = new CardViewViewModel() };
                    break;

                case nameof(DrilldownButtonListPage):
                    page = new DrilldownButtonListPage { BindingContext = new DrilldownButtonListViewModel(this.displayService) };
                    break;

                case nameof(PickersPage):
                    page = new PickersPage { BindingContext = new PickersViewModel(this.displayService, this.Countries) };
                    break;

                case nameof(SwitchesPage):
                    page = new SwitchesPage { BindingContext = null };
                    break;

                default:
                    throw new NotSupportedException($"Page is not known");
            }

            await this.navigationService.PushAsync(page);
        }


        public ICommand ShowActivityIndicatorCommand
        {
            get
            {
                return this.showActivityIndicatorCommand ??
                       (this.showActivityIndicatorCommand = new Command(async () => await this.OnShowActivityIndicator()));
            }
        }

        private async Task OnShowActivityIndicator()
        {
            this.activityIndicatorService.ShowLoadingPage("Loading...");
            await Task.Delay(3000);
            this.activityIndicatorService.HideLoadingPage();
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
                    .Take(10)
                    .ToList();
                this.SuggestedCountries.AddRange(filteredViewModels);

                if (!filteredViewModels.Any())
                {
                    this.Validation.AddErrorMessageForProperty(nameof(this.Country), "No results found!");
                }
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

        public ICommand DumpResourcesCommand => new Command(this.OnDumpResources);
        private async void OnDumpResources()
        {
            try
            {
                var sb = new StringBuilder();
                foreach (var resourceViewModel in this.ThemeResources)
                {
                    sb.AppendLine($"{resourceViewModel.ResourceType};{resourceViewModel.Key};{resourceViewModel.Value}");
                }

                var resourcesDump = sb.ToString();

                await this.emailService.SendEmail("Send Mail", resourcesDump, new List<string> { this.AdminEmailAddress });
            }
            catch (Exception ex)
            {
                await this.displayService.DisplayAlert("Email Error", $"Failed to send mail: {ex}");
            }
        }

        public ICommand MailNavigateCommand => new Command(this.OnMailNavigate);

        private async void OnMailNavigate()
        {
            try
            {
                await this.emailService.SendEmail("Send Mail", "Some text....", new List<string> { this.AdminEmailAddress });
            }
            catch (Exception ex)
            {
                await this.displayService.DisplayAlert("Email Error", $"Failed to send mail: {ex}");
            }
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


        public ICommand SelectCountryCommand => new Command<CountryViewModel>(this.OnSelectCountry);

        private async void OnSelectCountry(CountryViewModel parameter)
        {
            await this.displayService.DisplayAlert("SelectCountryCommand", $"country: {parameter.Name ?? "null"}");
        }

        public ICommand SetFantasyLandCommand => new Command(this.OnSetFantasyLand);

        private void OnSetFantasyLand()
        {
            // Since none of the Countries are IEquitable<> to "Fantasy Land", the UI controls binding to Country
            // need to react properly: Bindable Picker switches to state 'nothing selected'.
            this.Country = new CountryViewModel(new CountryDto { Id = 99, Name = "Fantasy Land" });
            this.Validation.AddErrorMessageForProperty(nameof(this.Country), "Fantasy Land does not exist, it's fiction!");
        }

        public ObservableCollection<ResourceViewModel> ThemeResources
        {
            get => this.themeResources;
            private set => this.SetProperty(ref this.themeResources, value, nameof(this.ThemeResources));
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

            try
            {
                this.activityIndicatorService.ShowLoadingPage("Test loading message...");
                await Task.Delay(3000);

                this.User = new UserDto
                {
                    Id = 1,
                    UserName = "thomasgalliker"
                };
                this.UserId = 2;

                this.numberOfLoads++;

                // Demo dynamic adjustment of MaxLength binding
                this.UserNameMaxLength = Math.Max(2, ++this.UserNameMaxLength);

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

                //this.ThemeResources = Application.Current.Resources.MergedDictionaries.SelectMany(md => md)
                //    .Select(kvp => new ResourceViewModel(kvp))
                //    .OrderBy(vm => vm.Key)
                //    //.ThenBy(vm => vm.Key)
                //    .ToObservableCollection();

                //this.Notes = $"Test test test{Environment.NewLine}Line 2 text text text";
                this.AdminEmailAddress = "thomas@bluewin.ch";
            }
            catch (Exception ex)
            {
                this.ViewModelError = new ViewModelError(ex.Message, this.LoadDataButtonCommand);
            }
            finally
            {
                this.activityIndicatorService.HideLoadingPage();
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

        public bool IsReadonly
        {
            get => this.isReadonly;
            set => this.SetProperty(ref this.isReadonly, value, nameof(this.IsReadonly));
        }

        public ICommand ToggleSwitchCommand
        {
            get
            {
                return this.toggleSwitchCommand ??
                       (this.toggleSwitchCommand = new Command(() => { this.IsReadonly = !this.IsReadonly; }));
            }
        }

        public bool IsMultiToggleButtonOn
        {
            get => this.isMultiToggleButtonOn;
            set => this.SetProperty(ref this.isMultiToggleButtonOn, value, nameof(this.IsMultiToggleButtonOn));
        }

        public bool IsMultiToggleButtonOff
        {
            get => this.isMultiToggleButtonOff;
            set => this.SetProperty(ref this.isMultiToggleButtonOff, value, nameof(this.IsMultiToggleButtonOff));
        }
    }
}