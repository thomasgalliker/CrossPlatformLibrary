using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Extensions
{
    public static class BindablePropertyExtensions
    {
        public static T GetDefaultValue<T>(this BindableProperty bindableProperty)
        {
            return (T)bindableProperty.DefaultValue;
        }
    }
}
