using Xamarin.Forms;
using TimePickerLineColorEffect = CrossPlatformLibrary.Forms.iOS.Effects.TimePickerLineColorEffect;

[assembly: ExportEffect(typeof(TimePickerLineColorEffect), nameof(TimePickerLineColorEffect))]
namespace CrossPlatformLibrary.Forms.iOS.Effects
{
    public class TimePickerLineColorEffect : LineColorEffectBase
    {

    }
}