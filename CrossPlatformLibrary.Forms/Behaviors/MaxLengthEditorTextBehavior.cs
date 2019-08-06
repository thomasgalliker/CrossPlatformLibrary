using System.Diagnostics;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Behaviors
{
    public class MaxLengthEditorTextBehavior : BehaviorBase<Editor>
    {
        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(
            nameof(MaxLength),
            typeof(int),
            typeof(MaxLengthEditorTextBehavior),
            int.MaxValue,
            BindingMode.OneWay,
            null,
            MaxLengthPropertyChanged);

        private static void MaxLengthPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            Debug.WriteLine($"MaxLengthEditorTextBehavior.MaxLength={newvalue}");
        }

        public int MaxLength
        {
            get => (int)this.GetValue(MaxLengthProperty);
            set => this.SetValue(MaxLengthProperty, value);
        }

        protected override void OnAttachedTo(Editor bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += this.BindableTextChanged;
        }

        private void BindableTextChanged(object sender, TextChangedEventArgs e)
        {
            var maxLength = this.MaxLength;
            if (sender is Editor entry && !string.IsNullOrEmpty(e.NewTextValue) && e.NewTextValue.Length > maxLength)
            {
                entry.Text = e.NewTextValue.Substring(0, maxLength);
            }
        }

        protected override void OnDetachingFrom(Editor bindable)
        {
            bindable.TextChanged -= this.BindableTextChanged;
            base.OnDetachingFrom(bindable);
        }
    }
}