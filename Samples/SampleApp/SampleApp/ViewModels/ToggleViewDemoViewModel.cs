using System.Diagnostics;
using System.Windows.Input;
using CrossPlatformLibrary.Mvvm;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class ToggleViewDemoViewModel : BindableBase
    {
        private bool isToggled;

        public ToggleViewDemoViewModel()
        {
        }

        public bool IsToggled
        {
            get => this.isToggled;
            set => this.SetProperty(ref this.isToggled, value);
        }

        public ICommand ToggleCommand => new Command<bool>(this.OnToggled);

        private void OnToggled(bool isToggled)
        {
            Debug.WriteLine($"OnToggled: isToggled={isToggled}");
        }
    }
}
