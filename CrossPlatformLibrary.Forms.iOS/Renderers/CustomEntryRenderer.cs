﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using CrossPlatformLibrary.Forms.Controls;
using CrossPlatformLibrary.Forms.iOS.Renderers;
using Foundation;
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
                uiTextField.SpellCheckingType = UITextSpellCheckingType.No; // No Spellchecking
                uiTextField.AutocorrectionType = UITextAutocorrectionType.No; // No Autocorrection

                if (this.Element is CustomEntry customEntry)
                {
                    this.UpdateBorder(customEntry);
                    this.AddRemoveReturnKeyToNumericInput(customEntry);
                    this.UpdateTextContentType(customEntry);
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
            var textContentType = this.MapTextContentType(customEntry.TextContentType);
            this.Control.TextContentType = textContentType;
        }

        private NSString MapTextContentType(TextContentType textContentType)
        {
            if (textContentType == TextContentType.OneTimeCode)
            {
                return UITextContentType.OneTimeCode;
            }
            else if (textContentType == TextContentType.FirstName)
            {
                return UITextContentType.GivenName;
            }
            else if (textContentType == TextContentType.LastName)
            {
                return UITextContentType.FamilyName;
            }
            else if (textContentType == TextContentType.Username)
            {
                return UITextContentType.Username;
            }
            else if (textContentType == TextContentType.EmailAddress)
            {
                return UITextContentType.EmailAddress;
            }
            else if (textContentType == TextContentType.PhoneNumber)
            {
                return UITextContentType.TelephoneNumber;
            }
            else if (textContentType == TextContentType.Password)
            {
                return UITextContentType.Password;
            }
            else if (textContentType == TextContentType.NewPassword)
            {
                return UITextContentType.NewPassword;
            }
            else
            {
                return new Foundation.NSString();
            }
        }
    }
}