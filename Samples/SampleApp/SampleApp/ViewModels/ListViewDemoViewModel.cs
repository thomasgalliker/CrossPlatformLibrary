using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CrossPlatformLibrary.Forms.Mvvm;
using SampleApp.Services;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class ListViewDemoViewModel : BaseViewModel
    {
        private readonly IDisplayService displayService;
        private ObservableCollection<CountryViewModel> countries;

        public ListViewDemoViewModel(IDisplayService displayService, ObservableCollection<CountryViewModel> countries)
        {
            this.displayService = displayService;
            this.Countries = countries;
        }

        public ObservableCollection<CountryViewModel> Countries
        {
            get => this.countries;
            private set => this.SetProperty(ref this.countries, value);
        }

        public ICommand SelectCountryCommand => new Command<CountryViewModel>(this.OnSelectCountry);

        private async void OnSelectCountry(CountryViewModel parameter)
        {
            await this.displayService.DisplayAlert("SelectCountryCommand", $"country: {parameter.Name ?? "null"}");
        }
    }
}
