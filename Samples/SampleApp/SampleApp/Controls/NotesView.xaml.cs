﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesView : ContentView
    {
        public NotesView()
        {
            this.InitializeComponent();
        }
    }
}