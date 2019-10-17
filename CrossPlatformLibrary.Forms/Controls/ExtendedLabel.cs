using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    //https://earthware.co.uk/blog/line-spacing-label-in-xamarin-forms
    public class ExtendedLabel : Label // TODO: Merge with CustomLabel?!
    {
        public static readonly BindableProperty LineSpacingProperty = BindableProperty.Create("LineSpacing", typeof(double), typeof(ExtendedLabel), defaultValue: default(double));

        public double LineSpacing
        {
            get => (double)this.GetValue(LineSpacingProperty);
            set => this.SetValue(LineSpacingProperty, value);
        }

        public static readonly BindableProperty CurvedCornerRadiusProperty = BindableProperty.Create(nameof(CurvedCornerRadius), typeof(double), typeof(ExtendedLabel), 12.0);

        public double CurvedCornerRadius
        {
            get => (double)this.GetValue(CurvedCornerRadiusProperty);
            set => this.SetValue(CurvedCornerRadiusProperty, value);
        }

        public static readonly BindableProperty CurvedBackgroundColorProperty = BindableProperty.Create(nameof(CurvedCornerRadius), typeof(Color), typeof(ExtendedLabel), Color.Default);

        public Color CurvedBackgroundColor
        {
            get => (Color)this.GetValue(CurvedBackgroundColorProperty);
            set => this.SetValue(CurvedBackgroundColorProperty, value);
        }
    }
}