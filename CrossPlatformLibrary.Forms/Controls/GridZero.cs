using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    /// <summary>
    /// A <see cref="Grid"/> with zero padding, margin, spacing and transparent background.
    /// </summary>
    public class GridZero : Grid
    {
        public GridZero()
        {
            this.Padding = 0;
            this.Margin = 0;
            this.RowSpacing = 0;
            this.ColumnSpacing = 0;
            this.BackgroundColor = Color.Transparent;
        }
    }
}