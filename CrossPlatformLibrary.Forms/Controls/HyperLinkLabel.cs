using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public class HyperLinkLabel : Label
    {
        public static readonly BindableProperty SubjectProperty =
            BindableProperty.Create(
                nameof(Subject),
                typeof(string),
                typeof(HyperLinkLabel),
                string.Empty);

        public static readonly BindableProperty NavigateUriProperty =
            BindableProperty.Create(
                nameof(NavigateUri),
                typeof(string),
                typeof(HyperLinkLabel),
                string.Empty);

        public static readonly BindableProperty NavigateCommandProperty =
            BindableProperty.Create(
                nameof(NavigateCommand),
                typeof(ICommand),
                typeof(HyperLinkLabel));

        public static readonly BindableProperty TintColorProperty =
            BindableProperty.Create(
                nameof(TintColor),
                typeof(Color),
                typeof(HyperLinkLabel),
                Color.Accent);

        private TapGestureRecognizer tapGestureRecognizer;

        public HyperLinkLabel()
        {
            this.NavigateCommand = new Command(() =>
            {
                if (this.NavigateUri != null)
                {
                    Device.OpenUri(new Uri(this.NavigateUri));
                }
            });
            this.tapGestureRecognizer = new TapGestureRecognizer { Command = this.NavigateCommand };
            this.GestureRecognizers.Add(this.tapGestureRecognizer);
        }

        public string Subject
        {
            get { return (string)this.GetValue(SubjectProperty); }
            set { this.SetValue(SubjectProperty, value); }
        }

        public string NavigateUri
        {
            get { return (string)this.GetValue(NavigateUriProperty); }
            set { this.SetValue(NavigateUriProperty, value); }
        }

        public ICommand NavigateCommand
        {
            get { return (ICommand)this.GetValue(NavigateCommandProperty); }
            set { this.SetValue(NavigateCommandProperty, value); }
        }

        public Color TintColor
        {
            get { return (Color)this.GetValue(TintColorProperty); }
            set { this.SetValue(TintColorProperty, value); }
        }

        #region Overrides of BindableObject

        /// <param name="propertyName">The name of the property that changed.</param>
        /// <summary>
        ///     Call this method from a child class to notify that a change happened on a property.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         A <see cref="T:Xamarin.Forms.BindableProperty" /> triggers this by itself. An inheritor only needs to call this
        ///         for properties without <see cref="T:Xamarin.Forms.BindableProperty" /> as the backend store.
        ///     </para>
        /// </remarks>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(this.NavigateCommand))
            {
                this.GestureRecognizers.Remove(this.tapGestureRecognizer);

                this.tapGestureRecognizer = new TapGestureRecognizer { Command = this.NavigateCommand };

                this.GestureRecognizers.Add(this.tapGestureRecognizer);
            }
        }

        #endregion
    }
}