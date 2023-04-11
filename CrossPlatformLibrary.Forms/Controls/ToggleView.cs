using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public class ToggleView : ContentView
    {
        private const string TapGestureRecognizerId = "ToggleCommandTapGestureRecognizer";
        private ICommand viewTappedCommand;

        protected override void OnParentSet()
        {
            base.OnParentSet();

            this.SetView(this.IsToggled, this.IsTapToToggleEnabled);
        }

        private void SetView(bool isToggled, bool isTapToToggleEnabled)
        {
            this.Content = isToggled ? this.TrueView : this.FalseView;

            if (this.Content is View content)
            {
                var tapGestureRecognizer = content.GestureRecognizers
                    .OfType<TapGestureRecognizer>()
                    .SingleOrDefault(r => r.AutomationId == TapGestureRecognizerId);

                if (tapGestureRecognizer != null)
                {
                    if (isTapToToggleEnabled == false)
                    {
                        content.GestureRecognizers.Remove(tapGestureRecognizer);
                    }
                }
                else
                {
                    if (isTapToToggleEnabled == true)
                    {
                        content.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            AutomationId = TapGestureRecognizerId,
                            Command = this.InternalViewTappedCommand
                        });
                    }
                }
            }
        }

        public static readonly BindableProperty IsToggledProperty =
            BindableProperty.Create(
                propertyName: nameof(IsToggled),
                returnType: typeof(bool),
                declaringType: typeof(ToggleView),
                defaultValue: false,
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: OnIsToggledPropertyChanged);

        public bool IsToggled
        {
            get => (bool)this.GetValue(IsToggledProperty);
            set => this.SetValue(IsToggledProperty, value);
        }

        private static void OnIsToggledPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is ToggleView view))
            {
                return;
            }

            if (!(newValue is bool isToggled))
            {
                return;
            }

            view.SetView(isToggled, view.IsTapToToggleEnabled);
        }

        public static readonly BindableProperty IsTapToToggleEnabledProperty =
            BindableProperty.Create(
                propertyName: nameof(IsTapToToggleEnabled),
                returnType: typeof(bool),
                declaringType: typeof(ToggleView),
                defaultValue: false,
                defaultBindingMode: BindingMode.OneWay,
                propertyChanged: OnIsTapToToggleEnabledPropertyChanged);

        public bool IsTapToToggleEnabled
        {
            get => (bool)this.GetValue(IsTapToToggleEnabledProperty);
            set => this.SetValue(IsTapToToggleEnabledProperty, value);
        }

        private static void OnIsTapToToggleEnabledPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is ToggleView view))
            {
                return;
            }

            if (!(newValue is bool isTapToToggleEnabled))
            {
                return;
            }

            view.SetView(view.IsToggled, isTapToToggleEnabled);
        }

        public static readonly BindableProperty ToggleCommandProperty =
            BindableProperty.Create(
                propertyName: nameof(ToggleCommand),
                returnType: typeof(ICommand),
                declaringType: typeof(ToggleView),
                defaultValue: default(ICommand));

        public ICommand ToggleCommand
        {
            get => (ICommand)this.GetValue(ToggleCommandProperty);
            set => this.SetValue(ToggleCommandProperty, value);
        }

        public View TrueView { get; set; }

        public View FalseView { get; set; }

        private ICommand InternalViewTappedCommand => this.viewTappedCommand ?? (this.viewTappedCommand = new Command(() =>
        {
            var isToggled = !this.IsToggled;
            this.IsToggled = isToggled;

            if (this.IsTapToToggleEnabled)
            {
                this.ToggleCommand?.Execute(isToggled);
            }
        }));
    }
}