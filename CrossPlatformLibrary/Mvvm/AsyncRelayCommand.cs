using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CrossPlatformLibrary.Mvvm
{
    public class AsyncRelayCommand : ICommand
    {
        private readonly Func<Task> execute;
        private readonly Func<bool> canExecute;

        public event EventHandler CanExecuteChanged;

        public AsyncRelayCommand(Func<Task> execute) : this(execute, null)
        {
        }

        public AsyncRelayCommand(Func<Task> execute, Func<bool> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute();
        }

        public async void Execute(object parameter)
        {
            await this.ExecuteAsync();
        }

        public async void Execute()
        {
            await this.ExecuteAsync();
        }


        public async Task ExecuteAsync()
        {
            await this.execute();
        }

        public void RaiseCanExecuteChange()
        {
            var handler = this.CanExecuteChanged;
            handler?.Invoke(this, EventArgs.Empty);
        }
    }
}
