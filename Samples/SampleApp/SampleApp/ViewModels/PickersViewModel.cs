using System.Collections.ObjectModel;
using CrossPlatformLibrary.Forms.Mvvm;
using SampleApp.Services;

namespace SampleApp.ViewModels
{
    public class PickersViewModel : BaseViewModel
    {
        public PickersViewModel(IDisplayService displayService)
        {
            this.StringValues = new ObservableCollection<string>
            {
                "String 1",
                "String 2"
            };
        }

        public ObservableCollection<string> StringValues { get; set; }
    }
}