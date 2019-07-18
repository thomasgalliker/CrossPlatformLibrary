using System;
using CrossPlatformLibrary.Forms.iOS.Effects;
using Foundation;
using ObjCRuntime;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(EntrySelectAllText), nameof(EntrySelectAllText))]

namespace CrossPlatformLibrary.Forms.iOS.Effects
{
    [Preserve(AllMembers = true)]
    public class EntrySelectAllText : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (!(this.Control is UITextField editText))
            {
                return;
            }

            editText.EditingDidBegin += this.EditTextOnEditingDidBegin;
        }

        private void EditTextOnEditingDidBegin(object sender, EventArgs e)
        {
            ((UITextField)sender).PerformSelector(new Selector("selectAll"), null, 0.0f);
        }

        protected override void OnDetached()
        {
            if (!(this.Control is UITextField editText))
            {
                return;
            }

            editText.EditingDidBegin -= this.EditTextOnEditingDidBegin;
        }
    }
}