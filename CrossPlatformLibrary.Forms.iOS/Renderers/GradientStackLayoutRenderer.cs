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
            var stack = (GradientStackLayout)this.Element;
            var startColor = stack.StartColor.ToCGColor();
            var endColor = stack.EndColor.ToCGColor();


            var gradientLayer = new CAGradientLayer();


            //var gradientLayer = new CAGradientLayer()
            //{
            //  StartPoint = new CGPoint(0, 0.5),
            //  EndPoint = new CGPoint(1, 0.5)
            //};

            gradientLayer.Frame = rect;
            gradientLayer.Colors = new[]
            {
                startColor, endColor
            };

            this.NativeView.Layer.InsertSublayer(gradientLayer, 0);
        }
    }
}