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
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            if (this.Element is GradientStackLayout gradientStackLayout)
            {
                var startColor = gradientStackLayout.StartColor.ToCGColor();
                var endColor = gradientStackLayout.EndColor.ToCGColor();

                var gradientLayer = new CAGradientLayer();

                if (gradientStackLayout.GradientOrientation == StackOrientation.Horizontal)
                {
                    gradientLayer.StartPoint = new CGPoint(0, 0.5);
                    gradientLayer.EndPoint = new CGPoint(1, 0.5);
                }

                gradientLayer.Frame = rect;
                gradientLayer.Colors = new[]
                {
                    startColor, endColor
                };

                this.NativeView.Layer.InsertSublayer(gradientLayer, 0);
            }
        }
    }
}