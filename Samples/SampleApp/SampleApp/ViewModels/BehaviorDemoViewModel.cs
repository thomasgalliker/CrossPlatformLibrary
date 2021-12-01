using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CrossPlatformLibrary.Forms.Mvvm;
using SampleApp.Services;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class BehaviorDemoViewModel : BaseViewModel
    {
        private readonly IDisplayService displayService;

        private bool isBoxVisible;
        private ObservableCollection<CountryViewModel> countries;
        private CountryViewModel scrollToElement;

        public BehaviorDemoViewModel(IDisplayService displayService, ObservableCollection<CountryViewModel> countries)
        {
            this.displayService = displayService;
            this.Countries = countries;

            this.IsBoxVisible = true;

            Task.Factory.StartNew(async () =>
            {
                await Task.Delay(1000);
                this.ScrollToElement = this.Countries.Single(c => c.Name == "Switzerland");
            });
        }

        public ICommand ToggleVisibilityCommand => new Command(this.OnToggleVisibility);

        private void OnToggleVisibility(object obj)
        {
            this.IsBoxVisible = !this.IsBoxVisible;
        }

        public bool IsBoxVisible
        {
            get => this.isBoxVisible;
            private set => this.SetProperty(ref this.isBoxVisible, value);
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

        public ICommand SelectCountryCommand => new Command<object>(this.OnSelectCountry);

        private async void OnSelectCountry(object parameter)
        {
            CountryViewModel selectedCountryViewModel;

            if (parameter is CountryViewModel selectedItem)
            {
                // Case 1: Returns the SelectedItem which is a CountryViewModel
                selectedCountryViewModel = selectedItem;
            }
            else if (parameter is ItemTappedEventArgs itemTappedEventArgs)
            {
                // Case 2: Returns ItemTappedEventArgs which contains the selected country in its Item property
                selectedCountryViewModel = (CountryViewModel)itemTappedEventArgs.Item;
            }
            else
            {
                throw new NotSupportedException("parameter is not supported");
            }

            await this.displayService.DisplayAlert("SelectCountryCommand", $"country: {selectedCountryViewModel.Name ?? "null"}");
        }

        public ICommand ScrollUpCommand => new Command(this.OnScrollUp);

        private void OnScrollUp(object obj)
        {
            this.ScrollToElement = this.Countries.First();
        }

        public ICommand ScrollDownCommand => new Command(this.OnScrollDown);

        private void OnScrollDown(object obj)
        {
            this.ScrollToElement = this.Countries.Last();
        }
    }
}