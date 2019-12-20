using System;
using System.Collections;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatformLibrary.Forms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MultiStateToggleButton : Frame
    {
        public MultiStateToggleButton()
        {
            this.InitializeComponent();
        }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(
                nameof(ItemsSource),
                typeof(IEnumerable),
                typeof(MultiStateToggleButton),
                null,
                BindingMode.OneWay);

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)this.GetValue(ItemsSourceProperty);
            set => this.SetValue(ItemsSourceProperty, value);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var bc = this.BindingContext;
            foreach (var child in this.ItemsSource.OfType<BindableObject>())
            {
                SetInheritedBindingContext(child, bc);
            }
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            var selectedButton = (Button)sender;
            var selectedMultiToggleButtonView = selectedButton.BindingContext as MultiToggleButtonView;

            foreach (var multiToggleButtonView in this.ItemsSource.OfType<MultiToggleButtonView>())
            {
                multiToggleButtonView.IsSelected = multiToggleButtonView == selectedMultiToggleButtonView;
            }

            //var command = selectedMultiToggleButtonView.Command;
            //var commandParameter = selectedMultiToggleButtonView.CommandParameter;
            //if (command != null && command.CanExecute(commandParameter))
            //{
            //    command.Execute(commandParameter);
            //}
        }

        public static readonly BindableProperty SpacingProperty =
            BindableProperty.Create(
                nameof(Spacing),
                typeof(double),
                typeof(MultiStateToggleButton),
                6.0d,
                BindingMode.OneWay);

        public double Spacing
        {
            get => (double)this.GetValue(SpacingProperty);
            set => this.SetValue(SpacingProperty, value);
        }

        public static readonly BindableProperty ButtonTextColorProperty =
            BindableProperty.Create(
                nameof(ButtonTextColor),
                typeof(Color),
                typeof(MultiStateToggleButton),
                Color.Default);

        public Color ButtonTextColor
        {
            get => (Color)this.GetValue(ButtonTextColorProperty);
            set => this.SetValue(ButtonTextColorProperty, value);
        }

        public static readonly BindableProperty ButtonBackgroundColorProperty =
            BindableProperty.Create(
                nameof(ButtonBackgroundColor),
                typeof(Color),
                typeof(MultiStateToggleButton),
                Color.Default);

        public Color ButtonBackgroundColor
        {
            get => (Color)this.GetValue(ButtonBackgroundColorProperty);
            set => this.SetValue(ButtonBackgroundColorProperty, value);
        }

        public static readonly BindableProperty SelectedButtonTextColorProperty =
            BindableProperty.Create(
                nameof(SelectedButtonTextColor),
                typeof(Color),
                typeof(MultiStateToggleButton),
                Color.Default);

        public Color SelectedButtonTextColor
        {
            get => (Color)this.GetValue(SelectedButtonTextColorProperty);
            set => this.SetValue(SelectedButtonTextColorProperty, value);
        }

        public static readonly BindableProperty SelectedButtonBackgroundColorProperty =
            BindableProperty.Create(
                nameof(SelectedButtonBackgroundColor),
                typeof(Color),
                typeof(MultiStateToggleButton),
                Color.Default);

        public Color SelectedButtonBackgroundColor
        {
            get => (Color)this.GetValue(SelectedButtonBackgroundColorProperty);
            set => this.SetValue(SelectedButtonBackgroundColorProperty, value);
        }
    }
}