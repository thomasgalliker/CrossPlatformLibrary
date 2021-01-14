using CrossPlatformLibrary.Forms.Controls;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Behaviors
{
    internal static class VisualElementExtensions
    {
        internal static Entry AsEntry(this VisualElement bindable)
        {
            if (bindable is Entry entry)
            {
                return entry;
            }
            else if (bindable is ValidatableEntry validatableEntry)
            {
                return validatableEntry.Entry;
            }

            return null;
        }

        internal static VisualElement AsVisualElement(this VisualElement bindable)
        {
            if (bindable is VisualElement visualElement)
            {
                return visualElement;
            }
            else if (bindable is ValidatableEntry validatableEntry)
            {
                return validatableEntry.Entry;
            }

            return null;
        }
    }
}
