﻿
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AutoCompletePage : ContentPage
    {
        public AutoCompletePage()
        {
            this.InitializeComponent();
        }

        // Subscribe TextChanged="AutoCompleteView_OnTextChanged"
        // in order to scroll the AutoCompleteView into view.
        //private void AutoCompleteView_OnTextChanged(object sender, TextChangedEventArgs e)
        //{
        //    this.ScrollView.ScrollToAsync(this.AutoCompleteView, ScrollToPosition.Start, animated: true);
        //}
    }
}