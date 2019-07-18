using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Effects
{
    public class SafeAreaBottomPaddingEffect : RoutingEffect
    {
        public SafeAreaBottomPaddingEffect()
            : base($"{Effects.Prefix}.{nameof(SafeAreaBottomPaddingEffect)}")
        {
        }
    }
}