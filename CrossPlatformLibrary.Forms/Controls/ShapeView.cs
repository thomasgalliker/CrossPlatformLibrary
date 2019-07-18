using System;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    /// <summary>
    /// Source: https://github.com/chrispellett/Xamarin-Forms-Shape
    /// TODO: Replace with RoundedBoxView ?
    /// </summary>
    public class ShapeView : BoxView
    {
        public static readonly BindableProperty ShapeTypeProperty = BindableProperty.Create<ShapeView, ShapeType>(s => s.ShapeType, ShapeType.Box);

        public static readonly BindableProperty StrokeColorProperty = BindableProperty.Create<ShapeView, Color>(s => s.StrokeColor, Color.Default);

        public static readonly BindableProperty StrokeWidthProperty = BindableProperty.Create<ShapeView, float>(s => s.StrokeWidth, 1f);

        public static readonly BindableProperty IndicatorPercentageProperty = BindableProperty.Create<ShapeView, float>(s => s.IndicatorPercentage, 0f);

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create<ShapeView, float>(s => s.CornerRadius, 0f);

        public static readonly BindableProperty PaddingProperty = BindableProperty.Create<ShapeView, Thickness>(s => s.Padding, default(Thickness));

        public ShapeType ShapeType
        {
            get
            {
                return (ShapeType)this.GetValue(ShapeTypeProperty);
            }
            set
            {
                this.SetValue(ShapeTypeProperty, value);
            }
        }

        public Color StrokeColor
        {
            get
            {
                return (Color)this.GetValue(StrokeColorProperty);
            }
            set
            {
                this.SetValue(StrokeColorProperty, value);
            }
        }

        public float StrokeWidth
        {
            get
            {
                return (float)this.GetValue(StrokeWidthProperty);
            }
            set
            {
                this.SetValue(StrokeWidthProperty, value);
            }
        }

        public float IndicatorPercentage
        {
            get
            {
                return (float)this.GetValue(IndicatorPercentageProperty);
            }
            set
            {
                if (this.ShapeType != ShapeType.CircleIndicator) throw new ArgumentException("Can only specify this property with CircleIndicator");
                this.SetValue(IndicatorPercentageProperty, value);
            }
        }

        public float CornerRadius
        {
            get
            {
                return (float)this.GetValue(CornerRadiusProperty);
            }
            set
            {
                if (this.ShapeType != ShapeType.Box) throw new ArgumentException("Can only specify this property with Box");
                this.SetValue(CornerRadiusProperty, value);
            }
        }

        public Thickness Padding
        {
            get
            {
                return (Thickness)this.GetValue(PaddingProperty);
            }
            set
            {
                this.SetValue(PaddingProperty, value);
            }
        }
    }
}