using System;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public partial class ValidatableLabel : GridZero
    {
        public ValidatableLabel()
        {
            this.InitializeComponent();
            this.DebugLayoutBounds();
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(
                nameof(Text),
                typeof(string),
                typeof(ValidatableLabel),
                null,
                BindingMode.OneWay);

        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(
                nameof(Placeholder),
                typeof(string),
                typeof(ValidatableLabel),
                null,
                BindingMode.OneWay);

        public string Placeholder
        {
            get => (string)this.GetValue(PlaceholderProperty);
            set => this.SetValue(PlaceholderProperty, value);
        }

        public static readonly BindableProperty LabelStyleProperty =
            BindableProperty.Create(
                nameof(LabelStyle),
                typeof(Style),
                typeof(ValidatableLabel),
                default(Style),
                BindingMode.OneWay);

        public Style LabelStyle
        {
            get => (Style)this.GetValue(LabelStyleProperty);
            set => this.SetValue(LabelStyleProperty, value);
        }

        public static readonly BindableProperty LineBreakModeProperty =
            BindableProperty.Create(
                nameof(LineBreakMode),
                typeof(LineBreakMode),
                typeof(ValidatableLabel),
                LineBreakMode.TailTruncation,
                BindingMode.OneWay);

        [Obsolete("Use LabelStyle instead")]
        public LineBreakMode LineBreakMode
        {
            get => (LineBreakMode)this.GetValue(LineBreakModeProperty);
            set => this.SetValue(LineBreakModeProperty, value);
        }
    }
}