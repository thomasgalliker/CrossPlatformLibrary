using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using CrossPlatformLibrary.Forms.Android.Renderers;
using CrossPlatformLibrary.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]

namespace CrossPlatformLibrary.Forms.Android.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        private static readonly string[] AutofillHintOneTimeCode = { "otp", "one-time-code" };
        private static readonly string[] AutofillHintFirstName = { "firstname", "first-name", "givenname", "given-name", "cc-given-name" };
        private static readonly string[] AutofillHintLastName = { "lastname", "last-name", "familyname", "family-name", "cc-family-name" };
        private new static readonly string[] AutofillHintUsername = { View.AutofillHintUsername };
        private new static readonly string[] AutofillHintEmailAddress = { View.AutofillHintEmailAddress, "email" };
        private new static readonly string[] AutofillHintPhone = { View.AutofillHintPhone, "tel" };
        private new static readonly string[] AutofillHintPassword = { View.AutofillHintPassword };
        private static readonly string[] AutofillHintNewPassword = { "new-password" };

        private Drawable originalBackground;
        private Thickness? originalPadding;

        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
            {
                return;
            }

            var formsEditText = this.Control;
            if (formsEditText != null)
            {
                if (this.Element is CustomEntry customEntry)
                {
                    this.HideBorder(customEntry);
                    this.RemovePadding(customEntry);
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
                    this.HideBorder(customEntry);
                }
            }
            else if (e.PropertyName == CustomEntry.RemovePaddingProperty.PropertyName)
            {
                if (this.Element is CustomEntry customEntry)
                {
                    this.RemovePadding(customEntry);
                }
            }
            else if (e.PropertyName == nameof(CustomEntry.TextContentTypeProperty.PropertyName))
            {
                if (this.Element is CustomEntry customEntry)
                {
                    this.UpdateTextContentType(customEntry);
                }
            }
        }

        private void HideBorder(CustomEntry customEntry)
        {
            if (customEntry.HideBorder)
            {
                this.originalBackground = this.Control.Background;
                this.Control.Background = null;
            }
            else
            {
                if (this.originalBackground != null)
                {
                    this.Control.Background = this.originalBackground;
                    this.originalBackground = null;
                }
            }
        }

        private void RemovePadding(CustomEntry customEntry)
        {
            if (customEntry.RemovePadding)
            {
                this.originalPadding = new Thickness(left: this.Control.PaddingLeft, top: this.Control.PaddingTop, right: this.Control.PaddingRight, bottom: this.Control.PaddingBottom);
                this.Control.SetPadding(0, 0, 0, 0);
                this.Control.SetIncludeFontPadding(false);
            }
            else
            {
                if (this.originalPadding != null)
                {
                    var p = this.originalPadding.Value;
                    var left = (int)p.Left;
                    var top = (int)p.Top;
                    var right = (int)p.Right;
                    var bottom = (int)p.Bottom;
                    this.Control.SetPadding(left, top, right, bottom);
                }

                this.Control.SetIncludeFontPadding(true);
            }
        }

        private void UpdateTextContentType(CustomEntry customEntry)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var textView = this.Control;
                if (customEntry.TextContentType == TextContentType.Default)
                {
                    textView.SetAutofillHints(autofillHints: null);
                    textView.ImportantForAutofill = ImportantForAutofill.No;
                }
                else
                {
                    var autofillHints = MapTextContentType(customEntry.TextContentType);
                    textView.SetAutofillHints(autofillHints);
                    textView.ImportantForAutofill = ImportantForAutofill.Yes;
                }
            }
        }

        private static string[] MapTextContentType(TextContentType textContentType)
        {
            // Some mapping names are taken von Android's View constants while others come from here:
            // https://developer.mozilla.org/en-US/docs/Web/HTML/Attributes/autocomplete

            if (textContentType == TextContentType.OneTimeCode)
            {
                return AutofillHintOneTimeCode;
            }
            else if (textContentType == TextContentType.FirstName)
            {
                return AutofillHintFirstName;
            }
            else if (textContentType == TextContentType.LastName)
            {
                return AutofillHintLastName;
            }
            else if (textContentType == TextContentType.Username)
            {
                return AutofillHintUsername;
            }
            else if (textContentType == TextContentType.EmailAddress)
            {
                return AutofillHintEmailAddress;
            }
            else if (textContentType == TextContentType.PhoneNumber)
            {
                return AutofillHintPhone;
            }
            else if (textContentType == TextContentType.Password)
            {
                return AutofillHintPassword;
            }
            else if (textContentType == TextContentType.NewPassword)
            {
                return AutofillHintNewPassword;
            }

            return new[] { string.Empty };
        }
    }
}