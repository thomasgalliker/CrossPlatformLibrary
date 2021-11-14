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
        private CountryViewModel scrollToElement;

        public ListViewDemoViewModel(IDisplayService displayService, ObservableCollection<CountryViewModel> countries)
        {
            this.displayService = displayService;
            this.Countries = countries;

            Task.Factory.StartNew(async () =>
            {
                await Task.Delay(1000);
                this.ScrollToElement = this.Countries.Single(c => c.Name == "Switzerland");
            });
        }

        public ObservableCollection<CountryViewModel> Countries
        {
            get => this.countries;
            private set => this.SetProperty(ref this.countries, value);
        }

        public CountryViewModel ScrollToElement
        {
            get => this.scrollToElement;
            private set => this.SetProperty(ref this.scrollToElement, value);
        }

        public ICommand SelectCountryCommand => new Command<CountryViewModel>(this.OnSelectCountry);

        private async void OnSelectCountry(CountryViewModel parameter)
        {
            await this.displayService.DisplayAlert("SelectCountryCommand", $"country: {parameter.Name ?? "null"}");
        }
    }
}
