using Xamarin.Forms;
using DatePickerLineColorEffect = CrossPlatformLibrary.Forms.iOS.Effects.DatePickerLineColorEffect;

[assembly: ExportEffect(typeof(DatePickerLineColorEffect), nameof(DatePickerLineColorEffect))]
namespace CrossPlatformLibrary.Forms.iOS.Effects
{
    public class DatePickerLineColorEffect : LineColorEffectBase
    {
    }
}