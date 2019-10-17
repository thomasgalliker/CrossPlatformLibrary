using CrossPlatformLibrary.Forms.Themes;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public class CardView : Frame
    {
        public CardView()
        {
            this.Padding = 0;
            this.BorderColor = Color.Transparent;
            this.IsClippedToBounds = true;

            if (Device.RuntimePlatform == Device.iOS)
            {
                this.HasShadow = false;
                this.CornerRadius = 0;
                this.BackgroundColor = Color.Transparent;
            }
            else
            {
                this.HasShadow = true;
                this.OutlineColor = Color.White; // BUG: If this is not set, HasShadow has no effect
                this.CornerRadius = 6;

                // Hack: OnPlatform lacks of support for DynamicResource bindings!
                this.SetDynamicResource(Frame.BackgroundColorProperty, ThemeConstants.CardViewStyle.BackgroundColor);
                this.SetDynamicResource(Frame.OutlineColorProperty, ThemeConstants.CardViewStyle.BackgroundColor);
            }
        }
    }
}

