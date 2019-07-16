using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Effects
{
    public class EntrySelectAllText : RoutingEffect
    {
        public EntrySelectAllText()
            : base($"{Effects.Prefix}.{nameof(EntrySelectAllText)}")
        {
        }
    }
}