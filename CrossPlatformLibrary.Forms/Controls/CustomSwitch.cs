using System;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    [Obsolete("Use Xamarin.Forms.Switch instead")]
    public class CustomSwitch : Switch
    {
        public static readonly BindableProperty TintColorProperty =
            BindableProperty.Create(
                nameof(TintColor),
                typeof(Color),
                typeof(CustomSwitch),
                Color.White);

        [Obsolete("Use Xamarin.Forms.Switch.ThumbColor instead")]
        public Color TintColor
        {
            get => (Color)this.GetValue(TintColorProperty);
            set => this.SetValue(TintColorProperty, value);
        }
    }
}
