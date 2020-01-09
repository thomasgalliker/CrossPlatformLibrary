using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CrossPlatformLibrary.Extensions;
using CrossPlatformLibrary.Mvvm;

namespace SampleApp.ViewModels
{
    public class SegmentedStatusIndicatorViewModel : BindableBase
    {

        private ObservableCollection<BusStopViewModel> busStops;

        public SegmentedStatusIndicatorViewModel()
        {
            this.BusStops = new List<BusStopViewModel>
            {
                new BusStopViewModel(1, "Stop 1", new DateTime(2000, 1, 1, 10, 15, 00, DateTimeKind.Utc)),
                new BusStopViewModel(2, "Stop 2", new DateTime(2000, 1, 1, 10, 25, 00, DateTimeKind.Utc)),
                new BusStopViewModel(3, "Stop 3", new DateTime(2000, 1, 1, 10, 30, 00, DateTimeKind.Utc)),
                new BusStopViewModel(4, "Stop 4", new DateTime(2000, 1, 1, 10, 30, 00, DateTimeKind.Utc)),
                new BusStopViewModel(5, "Stop 5", new DateTime(2000, 1, 1, 10, 30, 00, DateTimeKind.Utc)),
            }.ToObservableCollection();

            this.FirstStop = this.BusStops.ElementAt(0);
            this.CurrentStop = this.BusStops.ElementAt(0);
        }

        public ObservableCollection<BusStopViewModel> BusStops
        {
            get => this.busStops;
            private set => this.SetProperty(ref this.busStops, value, nameof(this.BusStops));
        }

        public BusStopViewModel FirstStop { get; set; }

        public BusStopViewModel CurrentStop { get; set; }
    }
}