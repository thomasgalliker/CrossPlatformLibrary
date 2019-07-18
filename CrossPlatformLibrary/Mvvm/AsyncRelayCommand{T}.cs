using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CrossPlatformLibrary.Mvvm
{
    public class AsyncRelayCommand<T> : ICommand
    {
        private readonly Func<T, Task> execute;
        private readonly Func<bool> canExecute;

        public event EventHandler CanExecuteChanged;

        public AsyncRelayCommand(Func<T, Task> execute) : this(execute, null)
        {
        }

        public AsyncRelayCommand(Func<T, Task> execute, Func<bool> canExecute)
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
            await this.ExecuteAsync(parameter);
        }

        public async Task ExecuteAsync(object parameter)
        {
            await this.execute((T)parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = this.CanExecuteChanged;
            handler?.Invoke(this, EventArgs.Empty);
        }
    }
}
