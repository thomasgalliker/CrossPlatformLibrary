using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Effects
{
    public class BorderEffect : RoutingEffect
    {
        public BorderEffect()
            : base($"{Effects.Prefix}.{nameof(BorderEffect)}")
        {
        }
    }
}
