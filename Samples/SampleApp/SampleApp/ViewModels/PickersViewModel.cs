using System.Collections.ObjectModel;
using CrossPlatformLibrary.Forms.Mvvm;
using SampleApp.Services;

namespace SampleApp.ViewModels
{
    public class PickersViewModel : BaseViewModel
    {
        private readonly IDisplayService displayService;
        private string selectedString;

        public PickersViewModel(IDisplayService displayService)
        {
            this.displayService = displayService;

            this.StringValues = new ObservableCollection<string>
            {
                null,
                "String 1",
                "String 10"
            };
        }

        public ObservableCollection<string> StringValues { get; set; }

        public string SelectedString
        {
            get => this.selectedString;
            set
            {
                if(this.SetProperty(ref this.selectedString, value))
                {
                    this.displayService.DisplayAlert("SelectedString", $"value={value}");
                }
            }
        }
    }
}