using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CrossPlatformLibrary.Forms.Converters;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatformLibrary.Forms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SegmentedStatusIndicator : GridZero
    {
        private List<StatusSegment> segmentedItems = new List<StatusSegment>();

        public SegmentedStatusIndicator()
        {
            this.InitializeComponent();
        }

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(
                propertyName: nameof(ItemTemplate),
                returnType: typeof(DataTemplate),
                declaringType: typeof(SegmentedStatusIndicator),
                defaultValue: default(DataTemplate));

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)this.GetValue(ItemTemplateProperty);
            set => this.SetValue(ItemTemplateProperty, value);
        }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(
                nameof(ItemsSource),
                typeof(IEnumerable),
                typeof(SegmentedStatusIndicator),
                null,
                BindingMode.OneWay,
                propertyChanged: OnItemsSourcePropertyChanged);

        private static void OnItemsSourcePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (newvalue is IEnumerable enumerable)
            {
                var control = (SegmentedStatusIndicator)bindable;
                var segmentedItems = new List<StatusSegment>();
                foreach (var item in enumerable)
                {
                    segmentedItems.Add(new StatusSegment(item));
                }

                var first = segmentedItems.FirstOrDefault();
                if (first != null)
                {
                    first.IsFirstElement = true;
                }

                var last = segmentedItems.LastOrDefault();
                if (last != null)
                {
                    last.IsLastElement = true;
                }

                control.segmentedItems = segmentedItems;
                control.BindableStackLayout.ItemsSource = control.segmentedItems.ToList();
            }
        }

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

            foreach (var statusSegment in control.segmentedItems)
            {
                statusSegment.IsStartElement = statusSegment.Payload == newvalue;
            }

            if (control.SelectionEnd != null)
            {
                UpdateMiddleElement(control);
            }
            else
            {
                ResetMiddleElement(control);
            }

            control.BindableStackLayout.ItemsSource = control.segmentedItems.ToList();
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

            foreach (var statusSegment in control.segmentedItems)
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

            control.BindableStackLayout.ItemsSource = control.segmentedItems.ToList();
        }

        private static void ResetMiddleElement(SegmentedStatusIndicator control)
        {
            foreach (var statusSegment in control.segmentedItems)
            {
                statusSegment.IsMiddleElement = false;
            }
        }

        private static void UpdateMiddleElement(SegmentedStatusIndicator control)
        {
            ResetMiddleElement(control);

            var hasStartElement = false;
            var hasEndElement = false;
            foreach (var statusSegment in control.segmentedItems)
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

        public static readonly BindableProperty LineWidthProperty =
            BindableProperty.Create(
                nameof(LineWidth),
                typeof(double),
                typeof(SegmentedStatusIndicator),
                2.0d,
                BindingMode.OneWay);

        public double LineWidth
        {
            get => (double)this.GetValue(LineWidthProperty);
            set => this.SetValue(LineWidthProperty, value);
        }

        public static readonly BindableProperty LineLengthPart1Property =
            BindableProperty.Create(
                nameof(LineLengthPart1),
                typeof(double),
                typeof(SegmentedStatusIndicator),
                8.0d,
                BindingMode.OneWay);

        public double LineLengthPart1
        {
            get => (double)this.GetValue(LineLengthPart1Property);
            set => this.SetValue(LineLengthPart1Property, value);
        }

        public static readonly BindableProperty PointSizeProperty =
            BindableProperty.Create(
                nameof(PointSize),
                typeof(double),
                typeof(SegmentedStatusIndicator),
                16.0d,
                BindingMode.OneWay);

        public double PointSize
        {
            get => (double)this.GetValue(PointSizeProperty);
            set => this.SetValue(PointSizeProperty, value);
        }

        public static readonly BindableProperty PointCornerRadiusProperty =
            BindableProperty.Create(
                nameof(PointCornerRadius),
                typeof(double),
                typeof(SegmentedStatusIndicator),
                8.0d,
                BindingMode.OneWay);

        public double PointCornerRadius
        {
            get => (double)this.GetValue(PointCornerRadiusProperty);
            set => this.SetValue(PointCornerRadiusProperty, value);
        }

        public static readonly BindableProperty SpacingProperty =
            BindableProperty.Create(
                nameof(Spacing),
                typeof(double),
                typeof(SegmentedStatusIndicator),
                6.0d,
                BindingMode.OneWay);

        public double Spacing
        {
            get => (double)this.GetValue(SpacingProperty);
            set => this.SetValue(SpacingProperty, value);
        }
    }
}