using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public class CustomScrollView : ScrollView
    {
        public static readonly BindableProperty IsScrollEnabledProperty =
            BindableProperty.Create(
                nameof(IsScrollEnabled),
                typeof(bool),
                typeof(CustomScrollView),
                default(bool));

        /// <summary>
        /// Enables or disables the scrollability via input gestures of this ScrollView.
        /// </summary>
        public bool IsScrollEnabled
        {
            get => (bool)this.GetValue(IsScrollEnabledProperty);
            set => this.SetValue(IsScrollEnabledProperty, value);
        }
    }
}
