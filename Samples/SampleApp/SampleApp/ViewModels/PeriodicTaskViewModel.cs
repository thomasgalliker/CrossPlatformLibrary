using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Input;
using CrossPlatformLibrary.Forms.Mvvm;
using CrossPlatformLibrary.Tools;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class PeriodicTaskViewModel : BaseViewModel
    {
        private DateTime timerStartDate;
        private DateTime countdownEndDate;
        private ICommand timerStartStopCommand;
        private ICommand countdownStartStopCommand;
        private CancellationTokenSource timerCts = new CancellationTokenSource();
        private CancellationTokenSource countdownCts = new CancellationTokenSource();
        private Guid timerInstance;
        private Guid countdownInstance;
        private bool isTimerRunning;

        public PeriodicTaskViewModel()
        {
        }

        public string TimerString
        {
            get
            {
                TimeSpan elapsedTime;
                if (!this.IsTimerRunning && this.TimerStartDate == DateTime.MinValue)
                {
                    elapsedTime = TimeSpan.Zero;
                }
                else
                {
                    elapsedTime = DateTime.UtcNow - this.TimerStartDate;
                }

                var timerString = $"{elapsedTime.TotalSeconds:F2}s";
                return timerString;
            }
        }

        private DateTime TimerStartDate
        {
            get => this.timerStartDate;
            set
            {
                if (this.SetProperty(ref this.timerStartDate, value, nameof(this.TimerStartDate)))
                {
                    this.RaisePropertyChanged(nameof(this.TimerButtonText));
                    this.RaisePropertyChanged(nameof(this.TimerString));
                }
            }
        }
        
        private bool IsTimerRunning
        {
            get => this.isTimerRunning;
            set
            {
                if (this.SetProperty(ref this.isTimerRunning, value, nameof(this.IsTimerRunning)))
                {
                    this.RaisePropertyChanged(nameof(this.TimerButtonText));
                }
            }
        }

        public string TimerButtonText
        {
            get
            {
                if (this.IsTimerRunning)
                {
                    return "Stop";
                }

                return "Start";
            }
        }

        public ICommand TimerStartStopCommand
        {
            get
            {
                return this.timerStartStopCommand ??
                       (this.timerStartStopCommand = new Command(() => this.ToggleTimer()));
            }
        }

        private void ToggleTimer()
        {
            if (this.IsTimerRunning)
            {
                this.StopTimer();
            }
            else
            {
                this.StartTimer();
            }
        }

        private void StopTimer()
        {
            try
            {
                // Cancel running timer if there is one...
                this.IsTimerRunning = false;
                Interlocked.Exchange(ref this.timerCts, new CancellationTokenSource()).Cancel();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Periodic updates cancellation failed: {ex}");
            }
        }

        private void StartTimer()
        {
            // Reset previous timer (if exists)
            this.TimerStartDate = DateTime.MinValue;

            // Start new periodic timer
            this.timerInstance = Guid.NewGuid();
            Debug.WriteLine($"Periodic updates {{{this.timerInstance}}} started");
            this.TimerStartDate = DateTime.UtcNow;

            try
            {
                this.IsTimerRunning = true;
                PeriodicTaskFactory.Start(() =>
                {
                    this.RaisePropertyChanged(nameof(this.TimerString));
                }, intervalInMilliseconds: 33, cancelToken: this.timerCts.Token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Periodic updates {{{this.timerInstance}}} failed: {ex}");
                throw;
            }
        }

        public string CountdownString
        {
            get
            {
                TimeSpan remainingTime;
                if (!this.IsCountdownRunning())
                {
                    remainingTime = TimeSpan.Zero;
                }
                else
                {
                    remainingTime = this.CountdownEndDate - DateTime.UtcNow;
                }

                var countdownString = $"{remainingTime.TotalSeconds:F0}s";
                return countdownString;
            }
        }

        private bool IsCountdownRunning()
        {
            return this.CountdownEndDate != DateTime.MinValue;
        }

        private DateTime CountdownEndDate
        {
            get => this.countdownEndDate;
            set
            {
                if (this.SetProperty(ref this.countdownEndDate, value, nameof(this.CountdownEndDate)))
                {
                    this.RaisePropertyChanged(nameof(this.CountdownButtonText));
                    this.RaisePropertyChanged(nameof(this.CountdownString));
                }
            }
        }

        public string CountdownButtonText
        {
            get
            {
                if (this.IsCountdownRunning())
                {
                    return "Stop";
                }

                return "Start";
            }
        }

        public ICommand CountdownStartStopCommand
        {
            get
            {
                return this.countdownStartStopCommand ??
                       (this.countdownStartStopCommand = new Command(() => this.ToggleCountdown()));
            }
        }

        private void ToggleCountdown()
        {
            if (this.IsCountdownRunning())
            {
                this.StopCountdown();
            }
            else
            {
                this.StartCountdown();
            }
        }

        private void StopCountdown()
        {
            try
            {
                // Cancel running timer if there is one...
                this.CountdownEndDate = DateTime.MinValue;
                Interlocked.Exchange(ref this.countdownCts, new CancellationTokenSource()).Cancel();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Periodic updates cancellation failed: {ex}");
            }
        }

        private void StartCountdown()
        {
            // Start new periodic timer
            this.countdownInstance = Guid.NewGuid();
            Debug.WriteLine($"Periodic updates {{{this.countdownInstance}}} started");

            var countdownInSeconds = 10;
            this.CountdownEndDate = DateTime.UtcNow.AddSeconds(countdownInSeconds);

            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                PeriodicTaskFactory.Start(() =>
                {
                    this.RaisePropertyChanged(nameof(this.CountdownString));
                    Debug.WriteLine($"Countdown Elapsed: {stopwatch.Elapsed}");
                }, intervalInMilliseconds: 1000, delayInMilliseconds: 1000, cancelToken: this.countdownCts.Token, maxIterations: countdownInSeconds);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Periodic updates {{{this.countdownInstance}}} failed: {ex}");
                throw;
            }
        }
    }


}