
using CrossPlatformLibrary.Forms.Validation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatformLibrary.Forms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ValidationErrorBox : Frame
    {
        public ValidationErrorBox()
        {
            this.InitializeComponent();
        }

        public static readonly BindableProperty ValidationProperty =
            BindableProperty.Create(
                nameof(Validation),
                typeof(ViewModelValidation),
                typeof(ValidationErrorBox),
                default(ViewModelValidation));

        public ViewModelValidation Validation
        {
            get => (ViewModelValidation)this.GetValue(ValidationProperty);
            set => this.SetValue(ValidationProperty, value);
        }
    }
}