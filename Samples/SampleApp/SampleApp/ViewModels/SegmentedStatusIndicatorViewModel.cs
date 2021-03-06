﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CrossPlatformLibrary.Extensions;
using CrossPlatformLibrary.Mvvm;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class SegmentedStatusIndicatorViewModel : BindableBase
    {

        private ObservableCollection<BusStopViewModel> busStops;
        private ICommand selectFirstStopCommand;
        private BusStopViewModel firstStop;
        private BusStopViewModel currentStop;
        private ICommand selectCurrentStopCommand;

        public SegmentedStatusIndicatorViewModel()
        {
            this.BusStops = new List<BusStopViewModel>
            {
                new BusStopViewModel(1, "Stop 1: Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam.", new DateTime(2000, 1, 1, 10, 15, 00, DateTimeKind.Utc)),
                new BusStopViewModel(2, "Stop 2", new DateTime(2000, 1, 1, 10, 25, 00, DateTimeKind.Utc)),
                new BusStopViewModel(3, "Stop 3", new DateTime(2000, 1, 1, 10, 30, 00, DateTimeKind.Utc)),
                new BusStopViewModel(4, "Stop 4", null),
                new BusStopViewModel(5, "Stop 5", null),
            }.ToObservableCollection();

            this.FirstStop = this.BusStops.ElementAt(1);
            this.CurrentStop = this.BusStops.ElementAt(3);
            this.CurrentStop.ImageSource = ImageSource.FromResource("SampleApp.Resources.Images.chevron_right_grey_192x192.png", this.GetType());
        }

        public ObservableCollection<BusStopViewModel> BusStops
        {
            get => this.busStops;
            private set => this.SetProperty(ref this.busStops, value, nameof(this.BusStops));
        }

        public BusStopViewModel FirstStop
        {
            get => this.firstStop;
            private set => this.SetProperty(ref this.firstStop, value, nameof(this.FirstStop));
        }

        public BusStopViewModel CurrentStop
        {
            get => this.currentStop;
            private set => this.SetProperty(ref this.currentStop, value, nameof(this.CurrentStop));
        }

        public ICommand SelectFirstStopCommand
        {
            get
            {
                return this.selectFirstStopCommand ??
                       (this.selectFirstStopCommand = new Command<string>((s) => this.OnSelectFirstStop(s)));
            }
        }

        private void OnSelectFirstStop(string s)
        {
            var stopId = 0;
            try
            {
                stopId = System.Convert.ToInt32(s);
            }
            catch
            {
            }

            this.FirstStop = this.BusStops.FirstOrDefault(b => b.Id == stopId);
        }
        
        public ICommand SelectCurrentStopCommand
        {
            get
            {
                return this.selectCurrentStopCommand ??
                       (this.selectCurrentStopCommand = new Command<string>((s) => this.OnSelectCurrentStop(s)));
            }
        }

        private void OnSelectCurrentStop(string s)
        {
            var stopId = 0;
            try
            {
                stopId = System.Convert.ToInt32(s);
            }
            catch
            {
            }

            this.CurrentStop = this.BusStops.FirstOrDefault(b => b.Id == stopId);
        }
    }
}