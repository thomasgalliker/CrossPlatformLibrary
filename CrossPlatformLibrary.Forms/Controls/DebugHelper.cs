using System.Diagnostics;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public static class DebugHelper
    {
        public static bool ShowLayoutBounds = true;

        [Conditional("DEBUG")]
        internal static void DebugLayoutBounds(this ValidatableEntry element, bool debug = true)
        {
            if (!ShowLayoutBounds || !debug)
            {
                return;
            }

            element.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentBright");
            element.AnnotationLabel.SetValue(VisualElement.BackgroundColorProperty, Color.Yellow);
            element.Entry.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentDark");
            element.ReadonlyLabel.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentDark");
        }
        
        [Conditional("DEBUG")]
        internal static void DebugLayoutBounds(this ValidatableAutoCompleteView element, bool debug = true)
        {
            if (!ShowLayoutBounds || !debug)
            {
                return;
            }

            element.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentBright");
            element.AnnotationLabel.SetValue(VisualElement.BackgroundColorProperty, Color.Yellow);
            element.Entry.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentDark");
            element.ReadonlyLabel.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentDark");
        }

        [Conditional("DEBUG")]
        internal static void DebugLayoutBounds(this ValidatableLabel element, bool debug = true)
        {
            if (!ShowLayoutBounds || !debug)
            {
                return;
            }

            element.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentBright");
            element.AnnotationLabel.SetValue(VisualElement.BackgroundColorProperty, Color.Yellow);
            element.ReadonlyLabel.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentDark");
        }

        [Conditional("DEBUG")]
        internal static void DebugLayoutBounds(this ValidatablePicker element, bool debug = true)
        {
            if (!ShowLayoutBounds || !debug)
            {
                return;
            }

            element.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentBright");
            element.AnnotationLabel.SetValue(VisualElement.BackgroundColorProperty, Color.Yellow);
            element.Picker.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentDark");
            element.ReadonlyLabel.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentDark");
        }

        [Conditional("DEBUG")]
        internal static void DebugLayoutBounds(this ValidatableDatePicker element, bool debug = true)
        {
            if (!ShowLayoutBounds || !debug)
            {
                return;
            }

            element.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentBright");
            element.AnnotationLabel.SetValue(VisualElement.BackgroundColorProperty, Color.Yellow);
            element.DatePicker.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentDark");
            element.ReadonlyLabel.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentDark");
        }

        [Conditional("DEBUG")]
        internal static void DebugLayoutBounds(this ValidatableDateTimePicker element, bool debug = true)
        {
            if (!ShowLayoutBounds || !debug)
            {
                return;
            }

            element.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentBright");
            element.AnnotationLabel.SetValue(VisualElement.BackgroundColorProperty, Color.Yellow);
            element.DatePicker.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentDark");
            element.TimePicker.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentDark");
            element.ReadonlyLabel.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentDark");
        }

        [Conditional("DEBUG")]
        internal static void DebugLayoutBounds(this ValidatableEditor element, bool debug = true)
        {
            if (!ShowLayoutBounds || !debug)
            {
                return;
            }

            element.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentBright");
            element.AnnotationLabel.SetValue(VisualElement.BackgroundColorProperty, Color.Yellow);
            element.Editor.SetDynamicResource(VisualElement.BackgroundColorProperty, "Theme.Color.SemiTransparentDark");
        }
    }
}