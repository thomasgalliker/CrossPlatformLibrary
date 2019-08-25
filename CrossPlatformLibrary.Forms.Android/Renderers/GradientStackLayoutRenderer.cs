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
            var gradient = this.GradientOrientation == StackOrientation.Horizontal
                ? new LinearGradient(0, 0, this.Width, 0,
                    this.StartColor.ToAndroid(),
                    this.EndColor.ToAndroid(),
                    Shader.TileMode.Clamp)
                : new LinearGradient(0, 0, 0, this.Height,
                    this.StartColor.ToAndroid(),
                    this.EndColor.ToAndroid(),
                    Shader.TileMode.Clamp);

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