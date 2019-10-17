using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    /// <summary>
    /// A <see cref="Frame"/> with zero padding, margin, no shadow and transparent background.
    /// </summary>
    public class FrameZero : Frame
    {
        public FrameZero()
        {
            this.Padding = 0;
            this.Margin = 0;
            this.HasShadow = false;
            this.BackgroundColor = Color.Transparent;
            this.OutlineColor = Color.White; // BUG: If this is not set, HasShadow has no effect
            this.BorderColor = Color.Transparent;
        }
    }
}

