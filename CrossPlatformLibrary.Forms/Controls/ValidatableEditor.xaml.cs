using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public partial class ValidatableEditor : GridZero
    {
        public ValidatableEditor()
        {
            this.InitializeComponent();
            this.DebugLayoutBounds();
        }

        public new void Focus()
        {
            base.Focus();
            this.Editor.Focus();
        }

        public new void Unfocus()
        {
            this.Editor.Unfocus();
            //this.Entry.SendCompleted();
            base.Unfocus();
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(
                nameof(Text),
                typeof(string),
                typeof(ValidatableEditor),
                null,
                BindingMode.TwoWay,
                propertyChanged: OnTextPropertyChanged);

        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        private static void OnTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var validatableEditor = (ValidatableEditor)bindable;
            validatableEditor.OnPropertyChanged(nameof(validatableEditor.AnnotationText));
        }

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(
                nameof(Placeholder),
                typeof(string),
                typeof(ValidatableEditor),
                null,
                BindingMode.OneWay,
                propertyChanged: OnPlaceholderPropertyChanged);

        public string Placeholder
        {
            get => (string)this.GetValue(PlaceholderProperty);
            set => this.SetValue(PlaceholderProperty, value);
        }

        private static void OnPlaceholderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var entry = (ValidatableEditor)bindable;
            entry.OnPropertyChanged(nameof(entry.AnnotationText));
        }

        public static readonly BindableProperty HidePlaceholderProperty =
            BindableProperty.Create(
                nameof(HidePlaceholder),
                typeof(bool),
                typeof(ValidatableEditor),
                true,
                BindingMode.OneWay,
                propertyChanged: OnHidePlaceholderPropertyChanged);

        public bool HidePlaceholder
        {
            get => (bool)this.GetValue(HidePlaceholderProperty);
            set => this.SetValue(HidePlaceholderProperty, value);
        }

        private static void OnHidePlaceholderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var entry = (ValidatableEditor)bindable;
            entry.OnPropertyChanged(nameof(entry.AnnotationText));
        }

        public string AnnotationText
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Text))
                {
                    return this.Placeholder;
                }

                // If HidePlaceholder is true, the returned AnnotationText is null
                // which means, the Placeholder label is not visible if Text is empty
                return this.HidePlaceholder ? null : " ";
            }
        }

        public static readonly BindableProperty KeyboardProperty =
            BindableProperty.Create(
                nameof(Keyboard),
                typeof(Keyboard),
                typeof(ValidatableEditor),
                Keyboard.Default,
                BindingMode.OneWay);

        public Keyboard Keyboard
        {
            get => (Keyboard)this.GetValue(KeyboardProperty);
            set => this.SetValue(KeyboardProperty, value);
        }

        public static readonly BindableProperty EditorStyleProperty =
            BindableProperty.Create(
                nameof(EditorStyle),
                typeof(Style),
                typeof(ValidatableEditor),
                default(Style));

        public Style EditorStyle
        {
            get => (Style)this.GetValue(EditorStyleProperty);
            set => this.SetValue(EditorStyleProperty, value);
        }

        public static readonly BindableProperty MaxLengthProperty =
            BindableProperty.Create(
                nameof(MaxLength),
                typeof(int),
                typeof(ValidatableEditor),
                int.MaxValue,
                BindingMode.OneWay);

        public int MaxLength
        {
            get => (int)this.GetValue(MaxLengthProperty);
            set => this.SetValue(MaxLengthProperty, value);
        }

        public static readonly BindableProperty MaxLinesProperty =
            BindableProperty.Create(
                nameof(MaxLines),
                typeof(int),
                typeof(ValidatableEditor),
                int.MaxValue,
                BindingMode.OneWay);

        public int MaxLines
        {
            get => (int)this.GetValue(MaxLinesProperty);
            set => this.SetValue(MaxLinesProperty, value);
        }

        public static readonly BindableProperty ValidationErrorsProperty =
            BindableProperty.Create(
                nameof(ValidationErrors),
                typeof(IEnumerable<string>),
                typeof(ValidatableEditor),
                default(IEnumerable<string>),
                BindingMode.OneWay);

        public IEnumerable<string> ValidationErrors
        {
            get => (IEnumerable<string>)this.GetValue(ValidationErrorsProperty);
            set => this.SetValue(ValidationErrorsProperty, value);
        }

        public event EventHandler Completed
        {
            add => this.Editor.Completed += value;
            remove => this.Editor.Completed -= value;
        }

        public new event EventHandler<FocusEventArgs> Focused
        {
            add => this.Editor.Focused += value;
            remove => this.Editor.Focused -= value;
        }

        public new event EventHandler<FocusEventArgs> Unfocused
        {
            add => this.Editor.Unfocused += value;
            remove => this.Editor.Unfocused -= value;
        }

        public event EventHandler<TextChangedEventArgs> TextChanged
        {
            add => this.Editor.TextChanged += value;
            remove => this.Editor.TextChanged -= value;
        }
    }
}