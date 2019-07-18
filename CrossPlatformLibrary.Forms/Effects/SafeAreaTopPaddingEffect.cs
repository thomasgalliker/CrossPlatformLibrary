using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Effects
{
    public class SafeAreaTopPaddingEffect : RoutingEffect
    {
        public SafeAreaTopPaddingEffect()
            : base($"{Effects.Prefix}.{nameof(SafeAreaTopPaddingEffect)}")
        {
        }
    }
}