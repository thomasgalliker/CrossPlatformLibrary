﻿using CrossPlatformLibrary.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CountryViewCell : ExtendedViewCell
    {
        public CountryViewCell()
        {
            this.InitializeComponent();
        }
    }
}