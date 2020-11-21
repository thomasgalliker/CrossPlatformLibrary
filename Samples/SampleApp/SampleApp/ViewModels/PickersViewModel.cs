using System.Collections.ObjectModel;
using CrossPlatformLibrary.Forms.Mvvm;
using SampleApp.Services;

namespace SampleApp.ViewModels
{
    public class PickersViewModel : BaseViewModel
    {
        private readonly IDisplayService displayService;
        private string selectedString;
        private ObservableCollection<CountryViewModel> countries;
        private CountryViewModel country;
        private bool isReadonly;

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
    }
}