using System;
using System.ComponentModel;
using CrossPlatformLibrary.Forms.Controls;
using CrossPlatformLibrary.Forms.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]

namespace CrossPlatformLibrary.Forms.iOS.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        private nfloat? originalBorderWidth;
        private UITextBorderStyle? originalBorderStyle;

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            var uiTextField = this.Control;
            if (uiTextField != null)
            {
                uiTextField.SpellCheckingType = UITextSpellCheckingType.No; // No Spellchecking
                uiTextField.AutocorrectionType = UITextAutocorrectionType.No; // No Autocorrection

                if (this.Element is CustomEntry customEntry)
                {
                    this.UpdateBorder(customEntry);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (this.Element is CustomEntry customEntry)
            {
                if (e.PropertyName == CustomEntry.HideBorderProperty.PropertyName)
                {
                    this.UpdateBorder(customEntry);
                }
            }
        }

        private void UpdateBorder(CustomEntry customEntry)
        {
            if (customEntry.HideBorder)
            {
                this.originalBorderWidth = this.Control.Layer.BorderWidth;
                this.originalBorderStyle = this.Control.BorderStyle;

                this.Control.Layer.BorderWidth = 0;
                this.Control.BorderStyle = UITextBorderStyle.None;
            }
            else
            {
                if (this.originalBorderWidth != null)
                {
                    this.Control.Layer.BorderWidth = this.originalBorderWidth.Value;
                    this.originalBorderWidth = null;
                }
                if (this.originalBorderStyle != null)
                {
                    this.Control.BorderStyle = this.originalBorderStyle.Value;
                    this.originalBorderStyle = null;
                }
            }
        }
    }
}