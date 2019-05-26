using CrossPlatformLibrary.Forms.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatformLibrary.Forms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewModelErrorControl : StackLayout
    {
        public static readonly BindableProperty ViewModelErrorProperty =
            BindableProperty.Create(
                nameof(ViewModelError),
                typeof(ViewModelError),
                typeof(ViewModelErrorControl),
                ViewModelError.None);

        public ViewModelError ViewModelError
        {
            get { return (ViewModelError)this.GetValue(ViewModelErrorProperty); }
            set { this.SetValue(ViewModelErrorProperty, value); }
        }

        public ViewModelErrorControl()
        {
            this.InitializeComponent();
        }
    }
}