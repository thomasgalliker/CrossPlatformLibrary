using System;
using System.ComponentModel;
using System.Drawing;
using CrossPlatformLibrary.Forms.Controls;
using CrossPlatformLibrary.Forms.iOS.Controls;
using CrossPlatformLibrary.Forms.iOS.Renderers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]

namespace CrossPlatformLibrary.Forms.iOS.Renderers
{
    public partial class CustomEntryRenderer : EntryRenderer
    {
        private bool returnButtonAdded;
        private nfloat? originalBorderWidth;
        private UITextBorderStyle? originalBorderStyle;

        protected override UITextField CreateNativeControl()
        {
            UITextField control;

            var customEntry = (CustomEntry)this.Element;
            var propartyValue = customEntry.GetValue(CustomEntry.PaddingProperty);
            if (propartyValue is Thickness padding)
            {
                control = new UITextFieldPadding(RectangleF.Empty)
                {
                    Padding = padding,
                    BorderStyle = UITextBorderStyle.RoundedRect,
                    ClipsToBounds = true
                };
            }
            else
            {
                control = base.CreateNativeControl();
            }

            return control;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            var uiTextField = this.Control;
            if (uiTextField != null)
            {
                if (this.Element is CustomEntry customEntry)
                {
                    this.UpdateBorder(customEntry);
                    this.AddRemoveReturnKeyToNumericInput(customEntry);
                    this.UpdateTextContentType(customEntry);
                    //this.UpdatePadding(customEntry); // Not supported
                    this.UpdateBorderColor(customEntry);
                    this.UpdateBorderThickness(customEntry);
                    this.UpdateCornerRadius(customEntry);
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
            // Not supported
            //else if (e.PropertyName == CustomEntry.PaddingProperty.PropertyName)
            //{
            //}
            else if (e.PropertyName == CustomEntry.BorderColorProperty.PropertyName)
            {
                if (this.Element is CustomEntry customEntry)
                {
                    this.UpdateBorderColor(customEntry);
                }
            }
            else if (e.PropertyName == CustomEntry.BorderThicknessProperty.PropertyName)
            {
                if (this.Element is CustomEntry customEntry)
                {
                    this.UpdateBorderThickness(customEntry);
                }
            }
            else if (e.PropertyName == CustomEntry.CornerRadiusProperty.PropertyName)
            {
                if (this.Element is CustomEntry customEntry)
                {
                    this.UpdateCornerRadius(customEntry);
                }
            }
        }

        private void UpdateBorderColor(CustomEntry customEntry)
        {
            this.Control.Layer.BorderColor = customEntry.BorderColor.ToCGColor();
        }
        
        private void UpdateBorderThickness(CustomEntry customEntry)
        {
            this.Control.Layer.BorderWidth = customEntry.BorderThickness;
        }

        private void UpdateCornerRadius(CustomEntry customEntry)
        {
            this.Control.Layer.CornerRadius = customEntry.CornerRadius;
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
            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
            {
                var (textContentType, keyboardType, autocapitalizationType, autocorrectionType) = MapTextContentType(customEntry.TextContentType);
                this.Control.TextContentType = textContentType;

                if (keyboardType != null)
                {
                    this.Control.KeyboardType = keyboardType.Value;
                }

                if (autocapitalizationType != null)
                {
                    this.Control.AutocapitalizationType = autocapitalizationType.Value;
                }

                if (autocorrectionType != null)
                {
                    this.Control.AutocorrectionType = autocorrectionType.Value;
                }
            }
        }

        private static (NSString, UIKeyboardType?, UITextAutocapitalizationType?, UITextAutocorrectionType?) MapTextContentType(TextContentType textContentType)
        {
            if (textContentType == TextContentType.Default)
            {
                return (new NSString(), null, null, null);
            }
            else if (textContentType == TextContentType.OneTimeCode)
            {
                return (UITextContentType.OneTimeCode, UIKeyboardType.NumberPad, UITextAutocapitalizationType.None, UITextAutocorrectionType.No);
            }
            else if (textContentType == TextContentType.FirstName)
            {
                return (UITextContentType.GivenName, null, null, null);
            }
            else if (textContentType == TextContentType.LastName)
            {
                return (UITextContentType.FamilyName, null, null, null);
            }
            else if (textContentType == TextContentType.Username)
            {
                return (UITextContentType.Username, null, UITextAutocapitalizationType.None, UITextAutocorrectionType.No);
            }
            else if (textContentType == TextContentType.EmailAddress)
            {
                return (UITextContentType.EmailAddress, null, UITextAutocapitalizationType.None, UITextAutocorrectionType.No);
            }
            else if (textContentType == TextContentType.PhoneNumber)
            {
                return (UITextContentType.TelephoneNumber, UIKeyboardType.NumberPad, UITextAutocapitalizationType.None, UITextAutocorrectionType.No);
            }
            else if (textContentType == TextContentType.Password)
            {
                return (UITextContentType.Password, null, UITextAutocapitalizationType.None, UITextAutocorrectionType.No);
            }
            else if (textContentType == TextContentType.NewPassword)
            {
                return (UITextContentType.NewPassword, null, UITextAutocapitalizationType.None, UITextAutocorrectionType.No);
            }

            return (null, null, null, null);
        }
    }
}