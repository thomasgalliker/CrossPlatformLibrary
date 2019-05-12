using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public class CustomSwitch : Switch
    {
        public static readonly BindableProperty TintColorProperty = 
            BindableProperty.Create(
                nameof(TintColor), 
                typeof(Color), 
                typeof(CustomSwitch),
                Color.White);

        public Color TintColor
        {
            get { return (Color)this.GetValue(TintColorProperty); }
            set { this.SetValue(TintColorProperty, value); }
        }
    }
}
