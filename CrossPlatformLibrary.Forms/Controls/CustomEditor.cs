using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public class CustomEditor : Editor
    {
        public static readonly BindableProperty MaxLinesProperty =
            BindableProperty.Create(
                nameof(MaxLines),
                typeof(int),
                typeof(CustomEditor),
                -1);

        public int MaxLines
        {
            get => (int)this.GetValue(MaxLinesProperty);
            set => this.SetValue(MaxLinesProperty, value);
        }

        public static readonly BindableProperty HideKeyboardProperty =
            BindableProperty.Create(
                nameof(HideKeyboard),
                typeof(bool),
                typeof(CustomEditor),
                defaultValue: false);

        public bool HideKeyboard
        {
            get { return (bool)this.GetValue(HideKeyboardProperty); }
            set { this.SetValue(HideKeyboardProperty, value); }
        }

        public CustomEditor()
        {
            this.TextChanged += this.OnTextChanged;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            this.InvalidateMeasure();
        }
    }
}
