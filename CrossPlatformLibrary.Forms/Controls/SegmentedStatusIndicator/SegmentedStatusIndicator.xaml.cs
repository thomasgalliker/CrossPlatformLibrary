using System;
using System.Collections;
using System.Linq;
using CrossPlatformLibrary.Extensions;
using CrossPlatformLibrary.Forms.Converters;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatformLibrary.Forms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SegmentedStatusIndicator : GridZero
    {
        public SegmentedStatusIndicator()
        {
            this.InitializeComponent();
        }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(
                nameof(ItemsSource),
                typeof(IEnumerable),
                typeof(SegmentedStatusIndicator),
                null,
                BindingMode.OneWay);

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)this.GetValue(ItemsSourceProperty);
            set => this.SetValue(ItemsSourceProperty, value);
        }

        public static readonly BindableProperty SelectionStartProperty =
            BindableProperty.Create(
                nameof(SelectionStart),
                typeof(object),
                typeof(SegmentedStatusIndicator),
                null,
                BindingMode.OneWay,
                propertyChanged: OnSelectionStartPropertyChanged);

        private static void OnSelectionStartPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (SegmentedStatusIndicator)bindable;

            foreach (var item in control.ItemsSource)
            {

                if (item is StatusSegment statusSegment)
                {
                    statusSegment.IsStartElement = statusSegment.Payload == newvalue;
                }
            }

            if (control.SelectionEnd != null)
            {
                UpdateMiddleElement(control);
            }
            else
            {
                ResetMiddleElement(control);
            }

            control.ItemsSource = control.ItemsSource.CreateList();
        }

        public object SelectionStart
        {
            get => (object)this.GetValue(SelectionStartProperty);
            set => this.SetValue(SelectionStartProperty, value);
        }

        public static readonly BindableProperty SelectionEndProperty =
            BindableProperty.Create(
                nameof(SelectionEnd),
                typeof(object),
                typeof(SegmentedStatusIndicator),
                null,
                BindingMode.OneWay,
                propertyChanged: OnSelectionEndPropertyChanged);

        private static void OnSelectionEndPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (SegmentedStatusIndicator)bindable;

            foreach (var statusSegment in control.ItemsSource.OfType<StatusSegment>())
            {
                statusSegment.IsEndElement = statusSegment.Payload == newvalue;
            }

            if (newvalue != null)
            {
                UpdateMiddleElement(control);
            }
            else
            {
                ResetMiddleElement(control);
            }
        

            control.ItemsSource = control.ItemsSource.CreateList();
        }

        private static void ResetMiddleElement(SegmentedStatusIndicator control)
        {
            foreach (var statusSegment in control.ItemsSource.OfType<StatusSegment>())
            {
                statusSegment.IsMiddleElement = false;
            }
        }

        private static void UpdateMiddleElement(SegmentedStatusIndicator control)
        {
            ResetMiddleElement(control);

            var hasStartElement = false;
            var hasEndElement = false;
            foreach (var statusSegment in control.ItemsSource.OfType<StatusSegment>())
            {
                if (statusSegment.IsStartElement)
                {
                    if (hasEndElement)
                    {
                        // If the StartElement comes after the EndElement,
                        // we stop any further marking of MiddleElements as this is an inconsistent situation
                        //throw new InvalidOperationException($"End element must not be before start element.");
                        break;
                    }
                    if (statusSegment.IsEndElement)
                    {
                        // If StartElement is the same as EndElement,
                        // we don't have to mark any MiddleElements
                        break;
                    }

                    statusSegment.IsMiddleElement = false;
                    hasStartElement = true;
                    continue;
                }

                if (statusSegment.IsEndElement)
                {
                    statusSegment.IsMiddleElement = false;
                    hasEndElement = true;
                    continue;
                }

                if (hasStartElement && !hasEndElement)
                {
                    statusSegment.IsMiddleElement = true;
                }
                else
                {
                    statusSegment.IsMiddleElement = false;
                }
            }
        }

        public object SelectionEnd
        {
            get => (object)this.GetValue(SelectionEndProperty);
            set => this.SetValue(SelectionEndProperty, value);
        }

        public static readonly BindableProperty IndicatorForegroundColorProperty =
            BindableProperty.Create(
                nameof(IndicatorForegroundColor),
                typeof(Color),
                typeof(SegmentedStatusIndicator),
                Color.Default);

        public Color IndicatorForegroundColor
        {
            get => (Color)this.GetValue(IndicatorForegroundColorProperty);
            set => this.SetValue(IndicatorForegroundColorProperty, value);
        }

        public static readonly BindableProperty IndicatorBackgroundColorProperty =
            BindableProperty.Create(
                nameof(IndicatorBackgroundColor),
                typeof(Color),
                typeof(SegmentedStatusIndicator),
                Color.Default);

        public Color IndicatorBackgroundColor
        {
            get => (Color)this.GetValue(IndicatorBackgroundColorProperty);
            set => this.SetValue(IndicatorBackgroundColorProperty, value);
        }
    }
}