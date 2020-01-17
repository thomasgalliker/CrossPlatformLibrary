using CrossPlatformLibrary.Mvvm;

namespace SampleApp.ViewModels
{
    public class CardViewViewModel : BindableBase
    {
        public CardViewViewModel()
        {
            this.PeriodicTask = new PeriodicTaskViewModel();
        }

        public PeriodicTaskViewModel PeriodicTask { get; private set; }
    }
}