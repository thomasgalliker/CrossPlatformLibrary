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

        private Color StartColor { get; set; }

        private Color EndColor { get; set; }

        protected override void DispatchDraw(Canvas canvas)
        {
            //var gradient = new Android.Graphics.LinearGradient(0, 0, 0, Height,
            var gradient = new LinearGradient(0, 0, this.Width, 0,
                this.StartColor.ToAndroid(),
                this.EndColor.ToAndroid(),
                Shader.TileMode.Mirror);

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
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"ERROR:", ex.Message);
            }
        }
    }
}