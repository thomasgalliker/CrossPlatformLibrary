using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    /// <summary>
    /// A <see cref="StackLayout"/> with zero padding, margin, spacing and transparent background.
    /// </summary>
    public class StackLayoutZero : StackLayout
    {
        public StackLayoutZero()
        {
            this.Padding = 0;
            this.Margin = 0;
            this.Spacing = 0;
            this.BackgroundColor = Color.Transparent;
        }
    }
}

