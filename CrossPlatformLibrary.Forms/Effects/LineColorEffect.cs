using System;
using System.Linq;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Effects
{
    public static class LineColorEffect
    {
        public static readonly BindableProperty ApplyLineColorProperty =
            BindableProperty.CreateAttached(
                "ApplyLineColor",
                typeof(bool),
                typeof(LineColorEffect),
                false,
                propertyChanged: OnApplyLineColorChanged);

        public static bool GetApplyLineColor(BindableObject view)
        {
            return (bool)view.GetValue(ApplyLineColorProperty);
        }

        public static void SetApplyLineColor(BindableObject view, bool value)
        {
            view.SetValue(ApplyLineColorProperty, value);
        }

        private static void OnApplyLineColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as View;
            if (view == null)
            {
                return;
            }

            var hasLineColor = (bool)newValue;
            if (hasLineColor)
            {
                if (bindable is Entry)
                {
                    if (!view.Effects.Any(e => e is EntryLineColorEffect))
                    {
                        view.Effects.Add(new EntryLineColorEffect());
                    }
                }
                else if (bindable is Editor)
                {
                    if (!view.Effects.Any(e => e is EditorLineColorEffect))
                    {
                        view.Effects.Add(new EditorLineColorEffect());
                    }
                }
                else if (bindable is DatePicker)
                {
                    if (!view.Effects.Any(e => e is DatePickerLineColorEffect))
                    {
                        view.Effects.Add(new DatePickerLineColorEffect());
                    }
                }
                else if (bindable is TimePicker)
                {
                    if (!view.Effects.Any(e => e is TimePickerLineColorEffect))
                    {
                        view.Effects.Add(new TimePickerLineColorEffect());
                    }
                }
                else if (bindable is Picker)
                {
                    if (!view.Effects.Any(e => e is PickerLineColorEffect))
                    {
                        view.Effects.Add(new PickerLineColorEffect());
                    }
                }
                else
                {
                    throw new NotSupportedException($"LineColorEffect is not supported for {bindable.GetType().Name}");
                }
            }
            else
            {
                var entryLineColorEffectToRemove = view.Effects.FirstOrDefault(e => e is EntryLineColorEffect);
                if (entryLineColorEffectToRemove != null)
                {
                    view.Effects.Remove(entryLineColorEffectToRemove);
                }

                var editorLineColorEffectToRemove = view.Effects.FirstOrDefault(e => e is EditorLineColorEffect);
                if (editorLineColorEffectToRemove != null)
                {
                    view.Effects.Remove(editorLineColorEffectToRemove);
                }

                var datePickerLineColorEffectToRemove = view.Effects.FirstOrDefault(e => e is DatePickerLineColorEffect);
                if (datePickerLineColorEffectToRemove != null)
                {
                    view.Effects.Remove(datePickerLineColorEffectToRemove);
                }

                var timePickerLineColorEffectToRemove = view.Effects.FirstOrDefault(e => e is TimePickerLineColorEffect);
                if (timePickerLineColorEffectToRemove != null)
                {
                    view.Effects.Remove(timePickerLineColorEffectToRemove);
                }

                var pickerLineColorEffectToRemove = view.Effects.FirstOrDefault(e => e is PickerLineColorEffect);
                if (pickerLineColorEffectToRemove != null)
                {
                    view.Effects.Remove(pickerLineColorEffectToRemove);
                }
            }
        }

        public static readonly BindableProperty LineColorProperty =
            BindableProperty.CreateAttached(
                "LineColor",
                typeof(object),
                typeof(LineColorEffect),
                Color.Blue,
                BindingMode.OneWay);

        public static Color GetLineColor(BindableObject view)
        {
            return (Color)view.GetValue(LineColorProperty);
        }

        public static void SetLineColor(BindableObject view, Color value)
        {
            view.SetValue(LineColorProperty, value);
        }
    }
}
