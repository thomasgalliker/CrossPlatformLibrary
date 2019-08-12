using System;
using System.ComponentModel;
using Android.Content;
using Android.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using TimePickerRenderer = CrossPlatformLibrary.Forms.Android.Renderers.TimePickerRenderer;

[assembly: ExportRenderer(typeof(TimePicker), typeof(TimePickerRenderer))]
namespace CrossPlatformLibrary.Forms.Android.Renderers
{
    public class TimePickerRenderer : Xamarin.Forms.Platform.Android.TimePickerRenderer
    {
        public TimePickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
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

            if (e.PropertyName == TimePicker.TimeProperty.PropertyName || e.PropertyName == TimePicker.FormatProperty.PropertyName)
            {
                this.SetText(view);
            }
        }

        private void SetText(TimePicker view)
        {
            var format = this.Element.Format;
            if (format == string.Empty)
            {
                this.Control.Text = string.Empty;
            }
            else
            {
                var today = DateTime.Today;
                today = today.Add(view.Time);

                var localDateTime = today.ToLocalTime();
                this.Control.Text = localDateTime.ToString(format);
            }
        }
    }
}