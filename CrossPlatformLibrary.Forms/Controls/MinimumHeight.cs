using System;
using System.ComponentModel;
using System.Diagnostics;
using CrossPlatformLibrary.Extensions;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    /// <summary>
    /// Attached BindableProperty which sizes a <seealso cref="Layout"/> or <seealso cref="VisualElement"/>
    /// to a defined minimum height. <seealso cref="MinimumHeight"/> ensures the Layout resp. VisualElement will always have the configured minimum height.
    /// </summary>
    public static class MinimumHeight
    {
        public static readonly BindableProperty ValueProperty =
            BindableProperty.CreateAttached(
                "Value",
                typeof(double),
                typeof(MinimumHeight),
                0d,
                propertyChanged: OnMinimumHeightPropertyChanged);

        public static double GetValue(BindableObject bindable)
        {
            return (double)bindable.GetValue(ValueProperty);
        }

        public static void SetValue(BindableObject bindable, double value)
        {
            bindable.SetValue(ValueProperty, value);
        }

        private static void OnMinimumHeightPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            Log($"OnMinimumHeightPropertyChanged: bindable={bindable.GetType().GetFormattedName()}");

            var oldMinimumHeight = oldValue as double?;

            if (bindable is Layout layout)
            {
                if (oldMinimumHeight != null)
                {
                    layout.LayoutChanged -= MinimumHeight.OnLayoutChanged;
                    layout.PropertyChanged -= MinimumHeight.OnPropertyChanged;
                }

                if (newValue != null)
                {
                    layout.LayoutChanged += MinimumHeight.OnLayoutChanged;
                    layout.PropertyChanged += MinimumHeight.OnPropertyChanged;
                }
            }

            if (bindable is VisualElement visualElement)
            {
                if (oldMinimumHeight != null)
                {
                    visualElement.SizeChanged -= MinimumHeight.OnLayoutChanged;
                    visualElement.PropertyChanged -= MinimumHeight.OnPropertyChanged;
                }

                if (newValue != null)
                {
                    visualElement.SizeChanged += MinimumHeight.OnLayoutChanged;
                    visualElement.PropertyChanged += MinimumHeight.OnPropertyChanged;
                }
            }
        }

        private static void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //if (e.PropertyName == "Height")
            //{
            //    RequestMinimumHeight(sender);
            //}
        }

        private static void OnLayoutChanged(object sender, EventArgs e)
        {
            RequestMinimumHeight(sender);
        }

        private static void RequestMinimumHeight(object sender)
        {
            var layout = sender as Layout ?? sender as VisualElement;
            if (layout == null)
            {
                throw new InvalidCastException();
            }

            var height = layout.Height;
            var minimumHeight = MinimumHeight.GetValue(layout);
            if (minimumHeight <= 0 || height <= 0)
            {
                return;
            }

            if (height < minimumHeight)
            {
                Log($"RequestMinimumHeight: Requesting MinimumHeight={minimumHeight}");
                layout.HeightRequest = minimumHeight;
            }
#if DEBUG
            else
            {
                Log($"RequestMinimumHeight: HeightRequest not required (MinimumHeight={minimumHeight}, Height={height})");
            }
#endif
        }

        [Conditional("DEBUG")]
        private static void Log(string message)
        {
            Debug.WriteLine($"MinimumHeight: RequestMinimumHeight: {message}");
        }
    }
}
