using System.Threading.Tasks;
using System.Windows.Input;
using CrossPlatformLibrary.Forms.Mvvm;
using CrossPlatformLibrary.Mvvm;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class DrilldownButtonListViewModel : BaseViewModel
    {
        private int numberOfLoads = 0;
        private ICommand toggleSwitchCommand;
        private bool isToggled;

        public DrilldownButtonListViewModel()
        {
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
    }
}