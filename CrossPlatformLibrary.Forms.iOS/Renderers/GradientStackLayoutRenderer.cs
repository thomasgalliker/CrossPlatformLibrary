using CoreAnimation;
using CoreGraphics;
using CrossPlatformLibrary.Forms.Controls;
using CrossPlatformLibrary.Forms.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(GradientStackLayout), typeof(GradientStackLayoutRenderer))]

namespace CrossPlatformLibrary.Forms.iOS.Renderers
{
    public class GradientStackLayoutRenderer : VisualElementRenderer<StackLayout>
    {
        private CAGradientLayer gradientLayer;

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            if (this.Element is GradientStackLayout gradientStackLayout)
            {
                var startColor = gradientStackLayout.StartColor.ToCGColor();
                var endColor = gradientStackLayout.EndColor.ToCGColor();

                this.gradientLayer = new CAGradientLayer();

                if (gradientStackLayout.GradientOrientation == StackOrientation.Horizontal)
                {
                    this.gradientLayer.StartPoint = new CGPoint(0, 0.5);
                    this.gradientLayer.EndPoint = new CGPoint(1, 0.5);
                }

                this.gradientLayer.Frame = rect;
                this.gradientLayer.Colors = new[]
                {
                    startColor, endColor
                };

                this.NativeView.Layer.InsertSublayer(this.gradientLayer, 0);
            }
        }

        public override void LayoutSublayersOfLayer(CALayer layer)
        {
            base.LayoutSublayersOfLayer(layer);

            if (this.gradientLayer != null)
            {
                // Adjust the frame size of the gradient layer
                // e.g. in case of an orientation change
                this.gradientLayer.Frame = layer.Bounds;
            }
        }
    }
}