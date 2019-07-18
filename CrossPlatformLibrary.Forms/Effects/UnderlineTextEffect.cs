using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Effects
{
    public class UnderlineTextEffect : RoutingEffect
    {
        public UnderlineTextEffect()
            : base($"{Effects.Prefix}.{nameof(UnderlineTextEffect)}")
        {
        }
    }
}
