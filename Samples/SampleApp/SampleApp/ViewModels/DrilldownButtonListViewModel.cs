using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CrossPlatformLibrary.Dispatching;
using CrossPlatformLibrary.Forms.Controls;
using CrossPlatformLibrary.Forms.Mvvm;
using CrossPlatformLibrary.Mvvm;
using SampleApp.Services;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class DrilldownButtonListViewModel : BaseViewModel
    {
        private int numberOfLoads = 0;
        private ICommand toggleSwitchCommand;
        private bool isToggled;

        public DrilldownButtonListViewModel(IDisplayService displayService)
        {
            this.DrilldownItems = new ObservableCollection<BindableBase>
            {
                new DrilldownSwitchViewModel{ Title = "DrilldownSwitchView 1", IsToggled = true },
                new DrilldownSwitchViewModel{ Title = "DrilldownSwitchView 2", IsToggled = false  },
                new DrilldownButtonViewModel{ Title = "DrilldownButtonView 1", Command = new Command(() => { displayService.DisplayAlert("DrilldownButtonView", "DrilldownButtonView 1 pressed"); })},
                new DrilldownButtonViewModel{ Title = "DrilldownButtonView 2", Command = new Command(() => { displayService.DisplayAlert("DrilldownButtonView", "DrilldownButtonView 2 pressed"); })},
                new CustomDrilldownViewModel{ Title = "CustomDrilldownViewModel 1", Command = new Command(() => { displayService.DisplayAlert("CustomDrilldownViewModel", "CustomDrilldownViewModel 1 pressed"); })},
                new CustomDrilldownViewModel{ Title = "CustomDrilldownViewModel 2", Command = new Command(() => { displayService.DisplayAlert("CustomDrilldownViewModel", "CustomDrilldownViewModel 2 pressed"); }), IsBusy=true},
            };
        }

        public string RefreshButtonText => $"Refresh (count: {this.numberOfLoads})";

        protected override async Task OnRefreshList()
        {
            await Task.Delay(1000);
            this.numberOfLoads++;
            this.RaisePropertyChanged(nameof(this.RefreshButtonText));
        }

        public bool IsToggled
        {
            get => this.isToggled;
            set
            {
                if (this.SetProperty(ref this.isToggled, value, nameof(this.IsToggled)))
                {
                    this.RaisePropertyChanged(nameof(this.ToggleSwitchButtonText));
                }
            }
        }

        public string ToggleSwitchButtonText => this.IsToggled ? "IsToggled: Yes" : "IsToggled: No";

        public ICommand ToggleSwitchCommand
        {
            get
            {
                return this.toggleSwitchCommand ??
                       (this.toggleSwitchCommand = new Command(() => { this.IsToggled = !this.IsToggled; }));
            }
        }

        public ObservableCollection<BindableBase> DrilldownItems { get; set; }
    }

    public abstract class TargetViewModel : BindableBase, IDrilldownView
    {
        public string Title { get; set; }

        public bool IsEnabled { get; set; } = true;

        public ICommand Command { get; set; }

        public object CommandParameter { get; set; }
    }

    public class DrilldownSwitchViewModel : TargetViewModel, IDrilldownSwitchView
    {
        private bool isToggled;

        public bool IsToggled
        {
            get => this.isToggled;
            set => this.isToggled = value;
        }
    }

    public class DrilldownButtonViewModel : TargetViewModel, IDrilldownButtonView
    {
    }

    public class CustomDrilldownViewModel : BaseViewModel
    {
        public ICommand Command { get; set; }
    }
}