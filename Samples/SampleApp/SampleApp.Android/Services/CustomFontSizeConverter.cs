using System;
using System.Threading;
using CrossPlatformLibrary.Extensions;
using CrossPlatformLibrary.Forms.Services;
using Xamarin.Forms;

namespace SampleApp.Droid.Services
{
    /// <summary>
    /// Sample implementation of a custom font size converter.
    /// </summary>
    public class CustomFontSizeConverter : IFontConverter
    {
        private static readonly Random Rng = new Random(DateTime.UtcNow.Millisecond);
        private readonly Timer timer;
        private double randomScale = 0.5;
        public event EventHandler FontScalingChanged;

        public CustomFontSizeConverter()
        {
            this.timer = new Timer(this.TickTimer, null, 5000, 5000);
        }

        private void TickTimer(object state)
        {
            this.randomScale = Rng.NextDouble(0.5, 2.0);
            Device.BeginInvokeOnMainThread(() =>
            {
                this.FontScalingChanged?.Invoke(this, EventArgs.Empty);
            });
        }

        public double GetScaledFontSize(double fontSize)
        {
            return fontSize * this.randomScale;
        }

        public void Dispose()
        {
            this.timer?.Dispose();
        }
    }
}