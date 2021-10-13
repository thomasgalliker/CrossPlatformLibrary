using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public class CustomTabbedPage : TabbedPage
    {
        public static readonly BindableProperty IsHiddenProperty =
           BindableProperty.Create(
               nameof(IsHidden),
               typeof(bool),
               typeof(CustomTabbedPage),
               false);

        public bool IsHidden
        {
            get => (bool)this.GetValue(IsHiddenProperty);
            set => this.SetValue(IsHiddenProperty, value);
        }
    }
}
