using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CrossPlatformLibrary.Forms.Controls;
using CrossPlatformLibrary.Forms.Mvvm;
using CrossPlatformLibrary.Forms.Validation;
using SampleApp.Services;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class PickersViewModel : BaseViewModel
    {
        private readonly IDisplayService displayService;
        private string selectedString;
        private ObservableCollection<CountryViewModel> countries;
        private CountryViewModel country;
        private bool isReadonly;
        private DateTime? birthdate;
        private ICommand toggleBirthdateCommand;

        public PickersViewModel(IDisplayService displayService, ObservableCollection<CountryViewModel> countries)
        {
            this.displayService = displayService;

            this.StringValues = new ObservableCollection<string>
            {
                null,
                "String 1",
                "String 10"
            };

            this.Countries = countries;

            var referenceDate = DateTime.Now;
            this.BirthdateValidityRange = new DateRange(start: referenceDate.AddDays(-2), end: referenceDate.AddDays(2));
            this.Birthdate = referenceDate;
        }

        protected override ViewModelValidation SetupValidation()
        {
            var viewModelValidation = new ViewModelValidation();

            viewModelValidation.AddValidationFor(nameof(this.Birthdate))
                .When(() => this.Birthdate == null)
                .Show(() => $"Birthdate must be set");

            return viewModelValidation;
        }

        public ObservableCollection<string> StringValues { get; set; }

        public string SelectedString
        {
            get => this.selectedString;
            set
            {
                if (this.SetProperty(ref this.selectedString, value))
                {
                    this.displayService.DisplayAlert("SelectedString", $"value={value}");
                }
            }
        }

        public ObservableCollection<CountryViewModel> Countries
        {
            get => this.countries;
            private set => this.SetProperty(ref this.countries, value, nameof(this.Countries));
        }

        public CountryViewModel Country
        {
            get => this.country;
            set => this.SetProperty(ref this.country, value);
        }

        public bool IsReadonly
        {
            get => this.isReadonly;
            set => this.SetProperty(ref this.isReadonly, value, nameof(this.IsReadonly));
        }

        public DateTime? Birthdate
        {
            get => this.birthdate;
            set
            {
                if(this.SetProperty(ref this.birthdate, value, nameof(this.Birthdate)))
                {
                    _ = this.Validation.IsValidAsync();
                }
            }
        }

        public DateRange BirthdateValidityRange { get; }

        public ICommand ToggleBirthdateCommand
        {
            get
            {
                return this.toggleBirthdateCommand ??
                       (this.toggleBirthdateCommand = new Command(() => this.ToggleBirthdate()));
            }
        }

        private void ToggleBirthdate()
        {
            this.Birthdate = this.Birthdate == null ? DateTime.Now : (DateTime?)null;
        }
    }
}