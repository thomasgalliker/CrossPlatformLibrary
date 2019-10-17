using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public class CustomButton : Button
    {
        public static readonly BindableProperty BackgroundColorPressedProperty = BindableProperty.Create(
                nameof(BackgroundColorPressed),
                typeof(Color),
                typeof(CustomButton),
                Color.White);

        public Color BackgroundColorPressed
        {
            get => (Color)this.GetValue(BackgroundColorPressedProperty);
            set => this.SetValue(BackgroundColorPressedProperty, value);
        }

        public static readonly BindableProperty BorderColorPressedProperty = BindableProperty.Create(
                nameof(BorderColorPressed),
                typeof(Color),
                typeof(CustomButton),
                Color.White);

        public Color BorderColorPressed
        {
            get => (Color)this.GetValue(BorderColorPressedProperty);
            set => this.SetValue(BorderColorPressedProperty, value);
        }

        public static readonly BindableProperty VerticalContentAlignmentProperty = BindableProperty.Create(
            nameof(VerticalContentAlignment),
            typeof(TextAlignment),
            typeof(CustomButton),
            TextAlignment.Center);

        public TextAlignment VerticalContentAlignment
        {
            get => (TextAlignment)this.GetValue(VerticalContentAlignmentProperty);
            set => this.SetValue(VerticalContentAlignmentProperty, value);
        }

        public static readonly BindableProperty HorizontalContentAlignmentProperty = BindableProperty.Create(
            nameof(HorizontalContentAlignment),
            typeof(TextAlignment),
            typeof(CustomButton),
            TextAlignment.Center);

        public TextAlignment HorizontalContentAlignment
        {
            get => (TextAlignment)this.GetValue(HorizontalContentAlignmentProperty);
            set => this.SetValue(HorizontalContentAlignmentProperty, value);
        }

        public static readonly BindableProperty AllCapsProperty = BindableProperty.Create(
                nameof(AllCaps),
                typeof(bool),
                typeof(CustomButton),
                GetDefaultAllCaps());

        private static object GetDefaultAllCaps()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                return true;
            }

            return false;
        }

        public bool AllCaps
        {
            get => (bool)this.GetValue(AllCapsProperty);
            set => this.SetValue(AllCapsProperty, value);
        }
    }
}