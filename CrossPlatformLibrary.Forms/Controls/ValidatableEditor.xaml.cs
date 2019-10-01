using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    /// <summary>
    /// Similar solution can be found here:
    /// https://github.com/XamFormsExtended/Xfx.Controls
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
                string.Empty,
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
                string.Empty,
                BindingMode.OneWay);

        public string Placeholder
        {
            get { return (string)this.GetValue(PlaceholderProperty); }
            set { this.SetValue(PlaceholderProperty, value); }
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
            get { return (Keyboard)this.GetValue(KeyboardProperty); }
            set { this.SetValue(KeyboardProperty, value); }
        }

        public new static readonly BindableProperty StyleProperty =
            BindableProperty.Create(
                nameof(Style),
                typeof(Style),
                typeof(ValidatableEditor),
                default(Style),
                BindingMode.OneWay);

        public new Style Style
        {
            get { return (Style)this.GetValue(StyleProperty); }
            set { this.SetValue(StyleProperty, value); }
        }

        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create(
                nameof(FontFamily),
                typeof(string),
                typeof(ValidatableEditor),
                default(string),
                BindingMode.OneWay);

        public string FontFamily
        {
            get { return (string)this.GetValue(FontFamilyProperty); }
            set { this.SetValue(FontFamilyProperty, value); }
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
            get { return (int)this.GetValue(MaxLengthProperty); }
            set { this.SetValue(MaxLengthProperty, value); }
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
            get { return (int)this.GetValue(MaxLinesProperty); }
            set { this.SetValue(MaxLinesProperty, value); }
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
            get { return (IEnumerable<string>)this.GetValue(ValidationErrorsProperty); }
            set { this.SetValue(ValidationErrorsProperty, value); }
        }

        public event EventHandler Completed
        {
            add { this.Editor.Completed += value; }
            remove { this.Editor.Completed -= value; }
        }

        public new event EventHandler<FocusEventArgs> Focused
        {
            add { this.Editor.Focused += value; }
            remove { this.Editor.Focused -= value; }
        }

        public new event EventHandler<FocusEventArgs> Unfocused
        {
            add { this.Editor.Unfocused += value; }
            remove { this.Editor.Unfocused -= value; }
        }

        public event EventHandler<TextChangedEventArgs> TextChanged
        {
            add { this.Editor.TextChanged += value; }
            remove { this.Editor.TextChanged -= value; }
        }


    }
}

