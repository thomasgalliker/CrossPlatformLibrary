using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
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
        private bool returnButtonAdded;
        private nfloat? originalBorderWidth;
        private UITextBorderStyle? originalBorderStyle;

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            var uiTextField = this.Control;
            if (uiTextField != null)
            {
                //uiTextField.SizeToFit();
                uiTextField.SpellCheckingType = UITextSpellCheckingType.No; // No Spellchecking
                uiTextField.AutocorrectionType = UITextAutocorrectionType.No; // No Autocorrection

                if (this.Element is CustomEntry customEntry)
                {
                    this.UpdateBorder(customEntry);
                    this.AddRemoveReturnKeyToNumericInput(customEntry);
                    this.UpdateTextContentType(customEntry);
                    this.SizeToFit(customEntry);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);


            if (e.PropertyName == CustomEntry.HideBorderProperty.PropertyName)
            {
                if (this.Element is CustomEntry customEntry)
                {
                    this.UpdateBorder(customEntry);
                }
            }
            else if (e.PropertyName == nameof(Entry.Keyboard))
            {
                if (this.Element is CustomEntry customEntry)
                {
                    this.AddRemoveReturnKeyToNumericInput(customEntry);
                }
            }
            else if (e.PropertyName == nameof(CustomEntry.TextContentType))
            {
                if (this.Element is CustomEntry customEntry)
                {
                    this.UpdateTextContentType(customEntry);
                }
            }
            else if (e.PropertyName == nameof(CustomEntry.HeightRequest))
            {
                if (this.Element is CustomEntry customEntry)
                {
                    this.SizeToFit(customEntry);
                }
            }
        }

        private void SizeToFit(CustomEntry customEntry)
        {
            Debug.WriteLine($"CustomEntryRenderer: SizeToFit --> HeightRequest={customEntry.HeightRequest}");
            if (customEntry.HeightRequest < 0)
            {
                //this.Control.SizeToFit();
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

        private void AddRemoveReturnKeyToNumericInput(CustomEntry customEntry)
        {
            if (customEntry.Keyboard == Keyboard.Numeric || customEntry.Keyboard == Keyboard.Telephone)
            {
                UIToolbar toolbar = null;

                if (!this.returnButtonAdded)
                {
                    toolbar = new UIToolbar(new RectangleF(0.0f, 0.0f, 50.0f, 44.0f));

                    var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
                    {
                        this.Control.ResignFirstResponder();
                        this.Element.SendCompleted();
                    });

                    toolbar.Items = new[] { new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace), doneButton };

                    this.returnButtonAdded = true;
                }

                this.Control.InputAccessoryView = toolbar;
            }

            else
            {
                this.Control.InputAccessoryView = null;
            }
        }

        private void UpdateTextContentType(CustomEntry customEntry)
        {
            if (customEntry.TextContentType == TextContentType.OneTimeCode)
            {
                this.Control.TextContentType = UITextContentType.OneTimeCode;
            }
            else if (customEntry.TextContentType == TextContentType.Name)
            {
                this.Control.TextContentType = UITextContentType.Name;
            }
            else if (customEntry.TextContentType == TextContentType.Username)
            {
                this.Control.TextContentType = UITextContentType.Username;
            }
            else if (customEntry.TextContentType == TextContentType.Password)
            {
                this.Control.TextContentType = UITextContentType.Password;
            }
            else if (customEntry.TextContentType == TextContentType.NewPassword)
            {
                this.Control.TextContentType = UITextContentType.NewPassword;
            }
            else
            {
                //this.Control.TextContentType = new Foundation.NSString();
            }
        }
    }
}