using System.Diagnostics;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    internal static class DebugHelper
    {
        public static bool Enabled = false;

        [Conditional("DEBUG")]
        internal static void DebugLayoutBounds(this ValidatableEntry element, bool debug = true)
        {
            if (!Enabled || !debug)
            {
                return;
            }

            element.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentBright");
            element.AnnotationLabel.SetValue(VisualElement.BackgroundColorProperty, Color.Yellow);
            element.Entry.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentDark");
        }

        [Conditional("DEBUG")]
        internal static void DebugLayoutBounds(this ValidatablePicker element, bool debug = true)
        {
            if (!Enabled || !debug)
            {
                return;
            }

            element.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentBright");
            element.AnnotationLabel.SetValue(VisualElement.BackgroundColorProperty, Color.Yellow);
            element.Picker.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentDark");
        }

        [Conditional("DEBUG")]
        internal static void DebugLayoutBounds(this ValidatableDatePicker element, bool debug = true)
        {
            if (!Enabled || !debug)
            {
                return;
            }

            element.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentBright");
            element.AnnotationLabel.SetValue(VisualElement.BackgroundColorProperty, Color.Yellow);
            element.DatePicker.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentDark");
        }

        [Conditional("DEBUG")]
        internal static void DebugLayoutBounds(this ValidatableDateTimePicker element, bool debug = true)
        {
            if (!Enabled || !debug)
            {
                return;
            }

            element.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentBright");
            element.AnnotationLabel.SetValue(VisualElement.BackgroundColorProperty, Color.Yellow);
            element.DatePicker.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentDark");
            element.TimePicker.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentDark");
        }
    }
}