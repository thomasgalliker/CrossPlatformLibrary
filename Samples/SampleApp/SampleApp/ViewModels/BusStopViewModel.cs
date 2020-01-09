using System;
using CrossPlatformLibrary.Mvvm;

namespace SampleApp.ViewModels
{
    public class BusStopViewModel : BindableBase
    {
        public BusStopViewModel(int id, string name, DateTime arrivalTime)
        {
            this.Id = id;
            this.Name = name;
            this.ArrivalTime = arrivalTime;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime ArrivalTime { get; }
    }
}