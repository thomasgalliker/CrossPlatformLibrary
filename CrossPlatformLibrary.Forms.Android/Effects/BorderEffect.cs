using System;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using CrossPlatformLibrary.Forms.Android.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(BorderEffect), "BorderEffect")]
namespace CrossPlatformLibrary.Forms.Android.Effects
{
    public class BorderEffect : PlatformEffect
	{
		Drawable originalBackground;

		protected override void OnAttached()
		{
			try
			{
				this.originalBackground = this.Control.Background;

				var shape = new ShapeDrawable(new RectShape());
				shape.Paint.Color = global::Android.Graphics.Color.Red;
				shape.Paint.StrokeWidth = 5;
				shape.Paint.SetStyle(Paint.Style.Stroke);
				this.Control.SetBackground(shape);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
			}
		}

		protected override void OnDetached()
		{
			try
			{
				this.Control.Background = this.originalBackground;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
			}
		}
	}
}
