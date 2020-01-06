using Xamarin.Forms;
using EntryLineColorEffect = CrossPlatformLibrary.Forms.iOS.Effects.EntryLineColorEffect;

[assembly: ExportEffect(typeof(EntryLineColorEffect), nameof(EntryLineColorEffect))]
namespace CrossPlatformLibrary.Forms.iOS.Effects
{
    public class EntryLineColorEffect : LineColorEffectBase
    {
    }
}