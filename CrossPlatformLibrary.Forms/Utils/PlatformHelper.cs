using System.Linq;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Utils
{
    public static class PlatformHelper
    {
        public static T OnPlatform<T>(T ios, T android, T uwp)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    return ios;

                case Device.Android:
                    return android;

                case Device.UWP:
                    return uwp;
            }

            return default(T);
        }

        public static T GetValue<T>(object res)
        {
            var onPlatform = (OnPlatform<T>)res;
            var value = onPlatform.Platforms.FirstOrDefault(p => p.Platform[0] == Device.RuntimePlatform)?.Value;
            if (value != null)
            {
                return (T)System.Convert.ChangeType(value, typeof(T));
            }

            return default(T);
        }
    }
}
