using CrossPlatformLibrary.Forms.Controls;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Behaviors
{
    public class MaxLengthTextBehavior : Behavior<ValidatableEntry>
    {
        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(
            nameof(MaxLength),
            typeof(int),
            typeof(MaxLengthTextBehavior),
            int.MaxValue);

        public int MaxLength
        {
            get { return (int)this.GetValue(MaxLengthProperty); }
            set { this.SetValue(MaxLengthProperty, value); }
        }

        protected override void OnAttachedTo(ValidatableEntry bindable)
        {
            bindable.TextChanged += this.BindableTextChanged;
        }

        private void BindableTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is Entry entry && !string.IsNullOrEmpty(e.NewTextValue) && e.NewTextValue.Length > this.MaxLength)
            {
                entry.Text = e.NewTextValue.Substring(0, this.MaxLength);
            }
        }

        protected override void OnDetachingFrom(ValidatableEntry bindable)
        {
            bindable.TextChanged -= this.BindableTextChanged;
        }
    }
}