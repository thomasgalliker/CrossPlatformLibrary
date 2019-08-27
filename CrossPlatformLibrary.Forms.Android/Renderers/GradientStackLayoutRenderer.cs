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
        public GradientStackLayoutRenderer(Context context) : base(context)
        {
        }

        private StackOrientation GradientOrientation { get; set; }

        private Color StartColor { get; set; }

        private Color EndColor { get; set; }

        protected override void DispatchDraw(Canvas canvas)
        {
            // Linear distribution is configurable:
            // https://stackoverflow.com/questions/35974463/why-doesnt-this-argument-of-the-lineargradient-constructor-seem-to-work

            var gradient = this.GradientOrientation == StackOrientation.Horizontal
                ? new LinearGradient(x0: 0, y0: 0, x1: this.Width, y1: 0,
                    color0: this.StartColor.ToAndroid(),
                    color1: this.EndColor.ToAndroid(),
                    tile: Shader.TileMode.Clamp)
                : new LinearGradient(x0: 0, y0: 0, x1: 0, y1: this.Height,
                    color0: this.StartColor.ToAndroid(),
                    color1: this.EndColor.ToAndroid(),
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

            try
            {
                if (e.NewElement is GradientStackLayout gradientColorStack)
                {
                    this.StartColor = gradientColorStack.StartColor;
                    this.EndColor = gradientColorStack.EndColor;
                    this.GradientOrientation = gradientColorStack.GradientOrientation;

                    //var gradient = new GradientDrawable(
                    //    GradientDrawable.Orientation.TopBottom,
                    //    new[] { this.StartColor.ToAndroid().ToArgb(), this.EndColor.ToAndroid().ToArgb() }
                    //);

                    //ViewCompat.SetBackground(this, gradient);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"ERROR:", ex.Message);
            }
        }
    }
}