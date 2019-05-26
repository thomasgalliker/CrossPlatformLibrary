using CrossPlatformLibrary.Forms.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace CrossPlatformLibrary.Forms.iOS.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            var uiTextField = this.Control;
            if (uiTextField != null)
            {
                uiTextField.SpellCheckingType = UITextSpellCheckingType.No;             // No Spellchecking
                uiTextField.AutocorrectionType = UITextAutocorrectionType.No;           // No Autocorrection
                ////uiTextField.AutocapitalizationType = UITextAutocapitalizationType.None; // No Autocapitalization

                ////var borderLayer = new CALayer();
                ////borderLayer.MasksToBounds = true;
                ////borderLayer.Frame = new CGRect(0f, this.Frame.Height, this.Frame.Width, 0f);
                ////borderLayer.BorderColor = UIColor.White.CGColor;
                ////borderLayer.BorderWidth = 2.0f;
                ////uiTextField.Layer.AddSublayer(borderLayer);
                ////uiTextField.BorderStyle = UITextBorderStyle.None;
            }
        }
    }
}