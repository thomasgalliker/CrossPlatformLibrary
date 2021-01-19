using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CrossPlatformLibrary.Extensions;
using CrossPlatformLibrary.Forms.Mvvm;
using CrossPlatformLibrary.Forms.Validation;
using SampleApp.Model;
using SampleApp.Services;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class AutoCompleteViewModel : BaseViewModel
    {
        private readonly IDisplayService displayService;
        private ObservableCollection<CountryViewModel> countries;
        private CountryViewModel country;
        private bool isReadonly;
        private string countrySearchText;
        private ICommand autoCompleteSearchCommand;

        public AutoCompleteViewModel(IDisplayService displayService, ObservableCollection<CountryViewModel> countries)
        {
            this.displayService = displayService;
            this.Countries = countries;
            this.SuggestedCountries = new ObservableCollection<CountryViewModel>();
        }

        protected override ViewModelValidation SetupValidation()
        {
            var viewModelValidation = new ViewModelValidation();

            viewModelValidation.AddValidationFor(nameof(this.Country))
                .When(() => this.Country == null)
                .Show(() => $"Country must be set");

            return viewModelValidation;
        }

        public string CountrySearchText
        {
            get => this.countrySearchText;
            set
            {
                if (this.SetProperty(ref this.countrySearchText, value, nameof(this.CountrySearchText)))
                {
                    // Use SearchText property binding to filter local data sources...
                    Console.WriteLine($"CountrySearchText changed: {value ?? "null"}");
                }
            }
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
            set => this.SetProperty(ref this.country, value);
        }

        public ICommand SetFantasyLandCommand => new Command(this.OnSetFantasyLand);

        private void OnSetFantasyLand()
        {
            // Since none of the Countries are IEquitable<> to "Fantasy Land", the UI controls binding to Country
            // need to react properly: Bindable Picker switches to state 'nothing selected'.
            this.Country = new CountryViewModel(new CountryDto { Id = 99, Name = "Fantasy Land" });
            this.Validation.AddErrorMessageForProperty(nameof(this.Country), "Fantasy Land does not exist, it's fiction!");
        }
        
        public ICommand SetCountryCommand => new Command(this.OnSetCountry);

        private void OnSetCountry()
        {
            this.Validation.ClearErrorMessages();
            this.Country = this.Countries.First(c => c.Name == "French Guiana");
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


        public bool IsReadonly
        {
            get => this.isReadonly;
            set => this.SetProperty(ref this.isReadonly, value, nameof(this.IsReadonly));
        }
    }
}