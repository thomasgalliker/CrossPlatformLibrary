using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    /// <summary>
    ///     Similar solution can be found here:
    ///     https://github.com/XamFormsExtended/Xfx.Controls
    /// </summary>
    public partial class ValidatableEditor : GridZero
    {
        public ValidatableEditor()
        {
            this.InitializeComponent();
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
                BindingMode.TwoWay);

        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(
                nameof(Placeholder),
                typeof(string),
                typeof(ValidatableEditor),
                null,
                BindingMode.OneWay);

        public string Placeholder
        {
            get => (string)this.GetValue(PlaceholderProperty);
            set => this.SetValue(PlaceholderProperty, value);
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