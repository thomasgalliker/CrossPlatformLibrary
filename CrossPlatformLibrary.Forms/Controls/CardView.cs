using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public class CardView : Frame
    {
        public CardView()
        {
            this.Padding = 0;
            if (Device.RuntimePlatform == Device.iOS)
            {
                this.HasShadow = false;
                this.CornerRadius = 0;
                this.BorderColor = Color.Transparent;
                this.BackgroundColor = Color.Transparent;
            }
            else
            {
                this.HasShadow = true;
                this.BorderColor = Color.Gray;
                this.CornerRadius = 6;
            }
        }
    }
}

