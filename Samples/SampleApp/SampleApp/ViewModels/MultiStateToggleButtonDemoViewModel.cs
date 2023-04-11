using CrossPlatformLibrary.Mvvm;

namespace SampleApp.ViewModels
{
    public class MultiStateToggleButtonDemoViewModel : BindableBase
    {
        private bool isMultiToggleButtonOn = true;
        private bool isMultiToggleButtonOff;

        public MultiStateToggleButtonDemoViewModel()
        {
        }

        public bool IsMultiToggleButtonOn
        {
            get => this.isMultiToggleButtonOn;
            set => this.SetProperty(ref this.isMultiToggleButtonOn, value, nameof(this.IsMultiToggleButtonOn));
        }

        public bool IsMultiToggleButtonOff
        {
            get => this.isMultiToggleButtonOff;
            set => this.SetProperty(ref this.isMultiToggleButtonOff, value, nameof(this.IsMultiToggleButtonOff));
        }
    }
}