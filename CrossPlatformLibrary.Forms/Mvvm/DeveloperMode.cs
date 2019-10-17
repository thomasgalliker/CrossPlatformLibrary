using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Mvvm
{
    /// <summary>
    /// DeveloperMode allows to unlock a hidden developer mode.
    /// </summary>
    public class DeveloperMode : BindableObject, IDeveloperMode
    {
        private const int UnlockCounterMax = 3;
        private static readonly TimeSpan UnlockCounterTimeSpan = TimeSpan.FromMilliseconds(2000);
        private int unlockCounter;
        private bool unlocked;

        public DeveloperMode()
        {
            this.UnlockCommand = new Command(this.UnlockDeveloperMode, () => true);
        }

        public ICommand UnlockCommand { get; }

        private void UnlockDeveloperMode()
        {
            Task.Factory.StartNew(async () =>
            {
                await Task.Delay(UnlockCounterTimeSpan);
                this.unlockCounter = 0;
            });

            if (this.unlockCounter <= UnlockCounterMax)
            {
                this.unlockCounter++;
            }
            else
            {
                this.Unlocked = true;
                this.UnlockedEvent?.Invoke(this, new EventArgs());
            }
        }

        public bool Unlocked
        {
            get => this.unlocked;
            set
            {
                this.unlocked = value;
                this.OnPropertyChanged(nameof(this.Unlocked));
            }
        }

        public event EventHandler<EventArgs> UnlockedEvent;
    }

    public interface IDeveloperMode
    {
        ICommand UnlockCommand { get; }

        bool Unlocked { get; set; }

        event EventHandler<EventArgs> UnlockedEvent;
    }
}