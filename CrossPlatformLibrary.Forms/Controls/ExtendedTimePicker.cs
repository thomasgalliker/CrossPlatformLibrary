using System;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    /// <summary>
    ///  Extended TimePicker for Nullable Values
    ///  Via: https://forums.xamarin.com/discussion/20028/datepicker-possible-to-bind-to-nullable-date-value
    ///  Via: https://github.com/XLabs/Xamarin-Forms-Labs/wiki/ExtendedEntry
    /// </summary>
    public class ExtendedTimePicker : TimePicker
    {
        /// <summary>
        /// The font property
        /// </summary>
        public static readonly BindableProperty FontProperty =
            BindableProperty.Create("Font", typeof(Font), typeof(ExtendedTimePicker), new Font());

        /// <summary>
        /// The NullableTime property
        /// </summary>
        public static readonly BindableProperty NullableTimeProperty =
            BindableProperty.Create("NullableTime", typeof(TimeSpan?), typeof(ExtendedTimePicker), null, BindingMode.TwoWay);

        /// <summary>
        /// The XAlign property
        /// </summary>
        public static readonly BindableProperty XAlignProperty =
            BindableProperty.Create("XAlign", typeof(TextAlignment), typeof(ExtendedTimePicker),
            TextAlignment.Start);

        /// <summary>
        /// The HasBorder property
        /// </summary>
        public static readonly BindableProperty HasBorderProperty =
            BindableProperty.Create("HasBorder", typeof(bool), typeof(ExtendedTimePicker), true);

        /// <summary>
        /// The Placeholder property
        /// </summary>
        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(
                nameof(Placeholder),
                typeof(string), 
                typeof(ExtendedTimePicker),
                null,
                BindingMode.OneWay);

        /// <summary>
        /// The PlaceholderTextColor property
        /// </summary>
        public static readonly BindableProperty PlaceholderTextColorProperty =
            BindableProperty.Create("PlaceholderTextColor", typeof(Color), typeof(ExtendedTimePicker), Color.Default);

        /// <summary>
		/// The MinimumTime property
		/// </summary>
		public static readonly BindableProperty MinimumTimeProperty =
            BindableProperty.Create("MinimumTime", typeof(TimeSpan), typeof(ExtendedTimePicker), new TimeSpan(0, 0, 0));

        /// <summary>
        /// The MaximumTime property
        /// </summary>
        public static readonly BindableProperty MaximumTimeProperty =
            BindableProperty.Create("MaximumTime", typeof(TimeSpan), typeof(ExtendedTimePicker), new TimeSpan(24, 0, 0));

        /// <summary>
        /// Get or sets the NullableTime
        /// </summary>
        public TimeSpan? NullableTime
        {
            get => (TimeSpan?)this.GetValue(NullableTimeProperty);
            set
            {
                if (value != this.NullableTime)
                {
                    this.SetValue(NullableTimeProperty, value);
                    this.UpdateTime();
                }
            }
        }

        /// <summary>
        /// Gets or sets the X alignment of the text
        /// </summary>
        public TextAlignment XAlign
        {
            get => (TextAlignment)this.GetValue(XAlignProperty);
            set => this.SetValue(XAlignProperty, value);
        }


        /// <summary>
        /// Gets or sets if the border should be shown or not
        /// </summary>
        public bool HasBorder
        {
            get => (bool)this.GetValue(HasBorderProperty);
            set => this.SetValue(HasBorderProperty, value);
        }

        /// <summary>
        /// Get or sets the PlaceHolder
        /// </summary>
        public string Placeholder
        {
            get => (string)this.GetValue(PlaceholderProperty);
            set => this.SetValue(PlaceholderProperty, value);
        }

        /// <summary>
        /// Sets color for placeholder text
        /// </summary>
        public Color PlaceholderTextColor
        {
            get => (Color)this.GetValue(PlaceholderTextColorProperty);
            set => this.SetValue(PlaceholderTextColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the minimum time
        /// </summary>
        /// <value>The minimum time.</value>
        public TimeSpan MinimumTime
        {
            get => (TimeSpan)this.GetValue(MinimumTimeProperty);
            set => this.SetValue(MinimumTimeProperty, value);
        }

        /// <summary>
        /// Gets or sets the maximum time
        /// </summary>
        /// <value>The maximum time.</value>
        public TimeSpan MaximumTime
        {
            get => (TimeSpan)this.GetValue(MaximumTimeProperty);
            set => this.SetValue(MaximumTimeProperty, value);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            this.UpdateTime();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            Device.OnPlatform(() =>
            {
                if (propertyName == IsFocusedProperty.PropertyName)
                {
                    if (this.IsFocused)
                    {
                        if (!this.NullableTime.HasValue)
                        {
                            this.Time = (TimeSpan)TimeProperty.DefaultValue;
                        }
                    }
                    else
                    {
                        this.OnPropertyChanged(TimeProperty.PropertyName);
                    }
                }
            });

            if (propertyName == TimeProperty.PropertyName)
            {
                this.NullableTime = this.Time;
            }

            if (propertyName == NullableTimeProperty.PropertyName)
            {
                if (this.NullableTime.HasValue)
                {
                    this.Time = this.NullableTime.Value;
                }
            }
        }

        private void UpdateTime()
        {


            if (this.NullableTime.HasValue)
            {

                if (this.NullableTime.Value < this.MinimumTime)
                {
                    this.Time = this.MinimumTime;
                }
                else if (this.NullableTime.Value > this.MaximumTime)
                {
                    this.Time = this.MaximumTime;
                }
                else
                {
                    this.Time = this.NullableTime.Value;
                }

                
            }
            else
            {
                this.Time = (TimeSpan)TimeProperty.DefaultValue;
            }
        }
    }
}

