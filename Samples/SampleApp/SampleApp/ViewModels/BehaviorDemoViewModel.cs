using System.Windows.Input;
using CrossPlatformLibrary.Forms.Mvvm;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class BehaviorDemoViewModel : BaseViewModel
    {
        private bool isBoxVisible;

        public BehaviorDemoViewModel()
        {
            this.IsBoxVisible = true;
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
    }
}