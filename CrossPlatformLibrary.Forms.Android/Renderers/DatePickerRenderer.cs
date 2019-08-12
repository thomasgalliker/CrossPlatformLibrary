using System;
using System.ComponentModel;
using Android.Content;
using Android.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;
using DatePickerRenderer = CrossPlatformLibrary.Forms.Android.Renderers.DatePickerRenderer;

[assembly: ExportRenderer(typeof(DatePicker), typeof(DatePickerRenderer))]
namespace CrossPlatformLibrary.Forms.Android.Renderers
{
    [Preserve(AllMembers = true)]
    public class DatePickerRenderer : Xamarin.Forms.Platform.Android.DatePickerRenderer
    {
        public static async void Init()
        {
            var now = DateTime.Now;
        }

        public DatePickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            var editText = this.Control;
            if (editText != null)
            {
                editText.InputType = InputTypes.TextFlagNoSuggestions;
            }

            var view = this.Element;
            if (view != null)
            {
                this.SetText(view);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = this.Element;

            if (e.PropertyName == DatePicker.DateProperty.PropertyName || e.PropertyName == DatePicker.FormatProperty.PropertyName)
            {
                this.SetText(view);
            }
        }

        private void SetText(DatePicker view)
        {
            var format = this.Element.Format;
            if (!string.IsNullOrEmpty(format))
            {
                var localDateTime = view.Date.ToLocalTime();
                this.Control.Text = localDateTime.ToString(format);
            }
            else
            {
                this.Control.Text = string.Empty;
            }
        }
    }
}