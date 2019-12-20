using System.Windows.Input;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public class MultiToggleButtonView : BindableObject
    {
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(
                nameof(Title),
                typeof(string),
                typeof(MultiToggleButtonView));

        public string Title
        {
            get => (string)this.GetValue(TitleProperty);
            set => this.SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty ImageSourceProperty =
            BindableProperty.Create(
                nameof(ImageSource),
                typeof(ImageSource),
                typeof(MultiToggleButtonView));

        public ImageSource ImageSource
        {
            get => (ImageSource)this.GetValue(ImageSourceProperty);
            set => this.SetValue(ImageSourceProperty, value);
        }

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(
                nameof(Command),
                typeof(ICommand),
                typeof(MultiToggleButtonView));

        public ICommand Command
        {
            get => (ICommand)this.GetValue(CommandProperty);
            set => this.SetValue(CommandProperty, value);
        }

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(
                nameof(CommandParameter),
                typeof(object),
                typeof(MultiToggleButtonView),
                null,
                BindingMode.OneWay);

        public object CommandParameter
        {
            get => this.GetValue(CommandParameterProperty);
            set => this.SetValue(CommandParameterProperty, value);
        }

        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create(
                nameof(IsSelected),
                typeof(bool),
                typeof(MultiToggleButtonView),
                false,
                BindingMode.TwoWay);

        public bool IsSelected
        {
            get => (bool)this.GetValue(IsSelectedProperty);
            set => this.SetValue(IsSelectedProperty, value);
        }
    }
}