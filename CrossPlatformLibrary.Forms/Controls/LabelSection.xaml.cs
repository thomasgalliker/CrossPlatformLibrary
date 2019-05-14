﻿using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public partial class LabelSection : ContentView
    {
        public LabelSection()
        {
            this.InitializeComponent();
        }

        public static readonly BindableProperty TextProperty = 
            BindableProperty.Create(nameof(Text), typeof(string), typeof(LabelSection), string.Empty);

        public string Text
        {
            get { return (string)this.GetValue(TextProperty); }
            set { this.SetValue(TextProperty, value); }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == TextProperty.PropertyName)
            {
                this.Section.Text = Device.RuntimePlatform == Device.iOS ? this.Text.ToUpperInvariant() : this.Text;
            }
        }
    }
}
