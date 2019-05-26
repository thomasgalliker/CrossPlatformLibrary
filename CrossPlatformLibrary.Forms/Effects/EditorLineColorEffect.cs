
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Effects
{
    public class EditorLineColorEffect : RoutingEffect
    {
        public EditorLineColorEffect()
            : base($"{Effects.Prefix}.{nameof(EditorLineColorEffect)}")
        {
        }
    }
}
