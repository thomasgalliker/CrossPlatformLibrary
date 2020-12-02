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
    public class EntryViewModel : BaseViewModel
    {
        private readonly IDisplayService displayService;
        private bool isReadonly;
        private string userName;

        public EntryViewModel(IDisplayService displayService, ObservableCollection<CountryViewModel> countries)
        {
            this.displayService = displayService;

        }

        protected override ViewModelValidation SetupValidation()
        {
            var viewModelValidation = new ViewModelValidation();
     
            viewModelValidation.AddValidationFor(nameof(this.UserName))
                         .When(() => string.IsNullOrWhiteSpace(this.UserName))
                         .Show(() => $"Username must not be empty");


            return viewModelValidation;
        }

        public string UserName
        {
            get => this.userName;
            set => this.SetProperty(this.userName, value);
        }

        public ICommand CalloutCommand => new Command<string>(this.OnCalloutCommand);

        private async void OnCalloutCommand(string parameter)
        {
            await this.displayService.DisplayAlert("CalloutCommand", $"parameter: {parameter}");
        }

        public bool IsReadonly
        {
            get => this.isReadonly;
            set => this.SetProperty(ref this.isReadonly, value, nameof(this.IsReadonly));
        }

    }
}