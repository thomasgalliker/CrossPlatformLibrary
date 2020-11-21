using Xamarin.Forms;
using PickerLineColorEffect = CrossPlatformLibrary.Forms.iOS.Effects.PickerLineColorEffect;

[assembly: ExportEffect(typeof(PickerLineColorEffect), nameof(PickerLineColorEffect))]
namespace CrossPlatformLibrary.Forms.iOS.Effects
{
    public class PickerLineColorEffect : LineColorEffectBase
    {
    }
}