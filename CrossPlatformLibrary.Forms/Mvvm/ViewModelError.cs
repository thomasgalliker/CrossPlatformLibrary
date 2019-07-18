using System.Windows.Input;
using CrossPlatformLibrary.Mvvm;

namespace CrossPlatformLibrary.Forms.Mvvm
{
    public class ViewModelError : BindableBase
    {
        public static readonly ViewModelError None = new ViewModelError(null, null);

        public ViewModelError(string text, ICommand command)
        {
            this.Text = text;
            this.Command = command;
        }

        public string Text { get; }

        public ICommand Command { get; }

        public bool HasError => this.Text != null;
    }
}