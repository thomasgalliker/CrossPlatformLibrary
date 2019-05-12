using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatformLibrary.Forms.Controls
{
    [ContentProperty("Source")]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (this.Source == null)
            {
                return null;
            }
            var imageSource = ImageSource.FromResource(this.Source);
            return imageSource;
        }
    }
}