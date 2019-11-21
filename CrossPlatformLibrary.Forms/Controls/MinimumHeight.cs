using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
            Log(bindable, $"OnMinimumHeightPropertyChanged: oldValue={oldValue}, newValue={newValue}");

            if (bindable is Layout layout)
            {
                if (oldValue != null)
                {
                    layout.LayoutChanged -= MinimumHeight.OnLayoutChanged;
                }

                if (newValue != null)
                {
                    layout.LayoutChanged += MinimumHeight.OnLayoutChanged;
                }
            }

            if (bindable is VisualElement visualElement)
            {
                if (oldValue != null)
                {
                    visualElement.SizeChanged -= MinimumHeight.OnLayoutChanged;
                }

                if (newValue != null)
                {
                    visualElement.SizeChanged += MinimumHeight.OnLayoutChanged;
                }
            }

            if (bindable is INotifyPropertyChanged notifyPropertyChanged)
            {
                if (oldValue != null)
                {
                    notifyPropertyChanged.PropertyChanged -= MinimumHeight.OnPropertyChanged;
                }

                if (newValue != null)
                {
                    notifyPropertyChanged.PropertyChanged += MinimumHeight.OnPropertyChanged;
                }
            }
        }

        private static void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Text" && sender is VisualElement visualElement && visualElement.HeightRequest > 0)
            {
                // This code makes sure all VisualElements with a property update 'Text' and HeightRequest>0
                // can grow their content (regardless of the MinimumHeight value).
                Log(sender, $"OnPropertyChanged -> PropertyName={e.PropertyName}");
                visualElement.HeightRequest = -1;
                RequestMinimumHeight(sender);
            }
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
            if (height <= 0)
            {
                return;
            }

            var minimumHeight = MinimumHeight.GetValue(layout);
            if (minimumHeight <= 0 || height == minimumHeight)
            {
                return;
            }

            if (height < minimumHeight)
            {
                Log(sender, $"Height={height} -> HeightRequest={minimumHeight}");
                layout.HeightRequest = minimumHeight;
            }
            else
            {
                Log(sender, $"Height={height} -> HeightRequest reset");
                layout.HeightRequest = -1;
            }
        }

        [Conditional("DEBUG")]
        private static void Log(object element, string message)
        {
            Debug.WriteLine($"MinimumHeight for {type.GetFormattedName()}: {message}");
        }
    }
}
