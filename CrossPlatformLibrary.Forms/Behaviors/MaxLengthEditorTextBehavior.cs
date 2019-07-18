using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Behaviors
{
    public class MaxLengthEditorTextBehavior : Behavior<Editor>
    {
        public static readonly BindableProperty MaxLengthProperty =  BindableProperty.Create(
            nameof(MaxLength),
            typeof(int), 
            typeof(MaxLengthEditorTextBehavior),
            0);

        public int MaxLength
        {
            get { return (int)this.GetValue(MaxLengthProperty); }
            set { this.SetValue(MaxLengthProperty, value); }
        }

        protected override void OnAttachedTo(Editor bindable)
        {
            bindable.TextChanged += this.BindableTextChanged;
        }

        private void BindableTextChanged(object sender, TextChangedEventArgs e)
        {
            var editor = sender as Editor;
            if (editor != null && !string.IsNullOrEmpty(e.NewTextValue) && e.NewTextValue.Length > this.MaxLength)
            {
                editor.Text = e.NewTextValue.Substring(0, this.MaxLength);
            }
        }

        protected override void OnDetachingFrom(Editor bindable)
        {
            bindable.TextChanged -= this.BindableTextChanged;
        }
    }
}