using System;
using Android.Content;
using Android.Graphics;
using CrossPlatformLibrary.Forms.Android.Renderers;
using CrossPlatformLibrary.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Xamarin.Forms.Color;

[assembly: ExportRenderer(typeof(GradientStackLayout), typeof(GradientStackLayoutRenderer))]

namespace CrossPlatformLibrary.Forms.Android.Renderers
{
    public class GradientStackLayoutRenderer : VisualElementRenderer<StackLayout>
    {
        private StackOrientation gradientOrientation;
        private Color startColor;
        private Color endColor;

        public GradientStackLayoutRenderer(Context context) : base(context)
        {
        }

        protected override void DispatchDraw(Canvas canvas)
        {
            // Linear distribution is configurable:
            // https://stackoverflow.com/questions/35974463/why-doesnt-this-argument-of-the-lineargradient-constructor-seem-to-work

            var gradient = this.gradientOrientation == StackOrientation.Horizontal
                ? new LinearGradient(x0: 0, y0: 0, x1: this.Width, y1: 0,
                    color0: this.startColor.ToAndroid(),
                    color1: this.endColor.ToAndroid(),
                    tile: Shader.TileMode.Clamp)
                : new LinearGradient(x0: 0, y0: 0, x1: 0, y1: this.Height,
                    color0: this.startColor.ToAndroid(),
                    color1: this.endColor.ToAndroid(),
                    tile: Shader.TileMode.Clamp);

            var paint = new Paint
            {
                Dither = true,
            };

            paint.SetShader(gradient);
            canvas.DrawPaint(paint);
            base.DispatchDraw(canvas);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || this.Element == null)
            {
                return;
            }

            if (e.NewElement is GradientStackLayout gradientColorStack)
            {
                this.startColor = gradientColorStack.StartColor;
                this.endColor = gradientColorStack.EndColor;
                this.gradientOrientation = gradientColorStack.GradientOrientation;
            }
        }
    }
}