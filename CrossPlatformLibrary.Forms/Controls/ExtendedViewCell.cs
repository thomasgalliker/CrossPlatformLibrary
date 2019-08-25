using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatformLibrary.Forms.Controls
{
    /// <summary>
    ///     Will be swapped out by a Custom Renderer which will override/reset
    ///     the background colour of the cell based on whether it's (un)selected.
    ///     Source: https://github.com/wislon/xfdemos/tree/master/src/xamformsdemo/xamformsdemo/CustomControls
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class ExtendedViewCell : ViewCell
    {
        public static readonly BindableProperty SelectedBackgroundColorProperty =
            BindableProperty.Create(
                nameof(SelectedBackgroundColor),
                typeof(Color),
                typeof(ExtendedViewCell),
                Color.Default);

        public Color SelectedBackgroundColor
        {
            get => (Color)this.GetValue(SelectedBackgroundColorProperty);
            set => this.SetValue(SelectedBackgroundColorProperty, value);
        }

        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create(
                nameof(IsSelected),
                typeof(bool),
                typeof(ExtendedViewCell),
                false);

        public bool IsSelected
        {
            get => (bool)this.GetValue(IsSelectedProperty);
            set => this.SetValue(IsSelectedProperty, value);
        }
    }
}