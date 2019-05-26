using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public class GridZero : Grid
    {
        public GridZero()
        {
            this.Padding = 0;
            this.Margin = 0;
            this.RowSpacing = 0;
            this.ColumnSpacing = 0;
        }
    }
}