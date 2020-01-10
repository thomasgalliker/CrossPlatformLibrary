using System;
using System.Diagnostics;
using CrossPlatformLibrary.Mvvm;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    [DebuggerDisplay("BusStop: {this.Id}")]
    public class BusStopViewModel : BindableBase
    {
        public BusStopViewModel(int id, string title, DateTime arrivalTime)
        {
            this.Id = id;
            this.Title = title;
            this.Description = $"{arrivalTime:t}";
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ImageSource ImageSource { get; set; }
    }
}