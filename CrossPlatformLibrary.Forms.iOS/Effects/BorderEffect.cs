using System;
using CrossPlatformLibrary.Forms.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(BorderEffect), nameof(BorderEffect))]
namespace CrossPlatformLibrary.Forms.iOS.Effects
{
    public class BorderEffect : PlatformEffect
	{
		protected override void OnAttached()
		{
			try
			{
				this.Control.Layer.BorderColor = UIColor.Red.CGColor;
				this.Control.Layer.BorderWidth = 1;
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
				this.Control.Layer.BorderWidth = 0;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
			}
		}
	}
}
