using Android.Runtime;
using Android.Widget;
using CrossPlatformLibrary.Forms.Android.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(EntrySelectAllText), nameof(EntrySelectAllText))]
namespace CrossPlatformLibrary.Forms.Android.Effects
{
    [Preserve(AllMembers = true)]
    public class EntrySelectAllText : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (!(this.Control is EditText editText))
            {
                return;
            }

            editText.SetSelectAllOnFocus(true);
        }

        protected override void OnDetached()
        {
            if (!(this.Control is EditText editText))
            {
                return;
            }

            editText.SetSelectAllOnFocus(false);
        }
    }
}