using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public class CustomEntry : Entry
    {
        public static BindableProperty CornerRadiusProperty =
             BindableProperty.Create(
                 nameof(CornerRadius),
                 typeof(int),
                 typeof(CustomEntry),
                 0);

        public int CornerRadius
        {
            get => (int)this.GetValue(CornerRadiusProperty);
            set => this.SetValue(CornerRadiusProperty, value);
        }

        public static BindableProperty BorderThicknessProperty =
            BindableProperty.Create(
                nameof(BorderThickness),
                typeof(int),
                typeof(CustomEntry),
                0);

        public int BorderThickness
        {
            get => (int)this.GetValue(BorderThicknessProperty);
            set => this.SetValue(BorderThicknessProperty, value);
        }

        public static BindableProperty BorderColorProperty =
            BindableProperty.Create(
                nameof(BorderColor),
                typeof(Color),
                typeof(CustomEntry),
                Color.Transparent);

        public Color BorderColor
        {
            get => (Color)this.GetValue(BorderColorProperty);
            set => this.SetValue(BorderColorProperty, value);
        }

        public static BindableProperty PaddingProperty =
            BindableProperty.Create(
                nameof(Padding),
                typeof(Thickness),
                typeof(CustomEntry),
                default(Thickness));

        /// <remarks>
        /// This property cannot be changed at runtime in iOS.
        /// </remarks>
        public Thickness Padding
        {
            get => (Thickness)this.GetValue(PaddingProperty);
            set => this.SetValue(PaddingProperty, value);
        }

        public static readonly BindableProperty HideBorderProperty =
            BindableProperty.Create(
                nameof(HideBorder),
                typeof(bool),
                typeof(CustomEntry),
                default(bool),
                BindingMode.OneWay);

        public bool HideBorder
        {
            get => (bool)this.GetValue(HideBorderProperty);
            set => this.SetValue(HideBorderProperty, value);
        }

        public static readonly BindableProperty RemovePaddingProperty =
            BindableProperty.Create(
                nameof(RemovePadding),
                typeof(bool),
                typeof(CustomEntry),
                false,
                BindingMode.OneWay
            );

        public bool RemovePadding
        {
            get => (bool)this.GetValue(RemovePaddingProperty);
            set => this.SetValue(RemovePaddingProperty, value);
        }

        public static readonly BindableProperty TextContentTypeProperty =
            BindableProperty.Create(
                nameof(TextContentType),
                typeof(TextContentType),
                typeof(CustomEntry),
                TextContentType.Default,
                BindingMode.OneWay);

        public TextContentType TextContentType
        {
            get => (TextContentType)this.GetValue(TextContentTypeProperty);
            set => this.SetValue(TextContentTypeProperty, value);
        }
    }
}