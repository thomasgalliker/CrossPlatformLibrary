using System.Diagnostics;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    internal static class RowDefinitionExtensions
    {
        internal static void Resize(this RowDefinition targetRow, VisualElement sourceElement, double heightOffset)
        {
            Debug.WriteLine($"RowDefinitionExtensions.Resize: Height={sourceElement.Height}, Width={sourceElement.Width}");
            if (sourceElement.Height > targetRow.Height.Value)
            {
                var newRowHeight = sourceElement.Height - heightOffset;
                if (newRowHeight <= 0)
                {
                    return;
                }

                Debug.WriteLine($"RowDefinitionExtensions.Resize: targetRow.Height={targetRow.Height.Value} -> newRowHeight={newRowHeight}");
                targetRow.Height = newRowHeight;
            }
        }
    }
}