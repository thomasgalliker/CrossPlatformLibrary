using System.Threading.Tasks;
using System.Windows.Input;
using CrossPlatformLibrary.Mvvm;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Mvvm
{
    public abstract class BaseViewModel : BindableBase
    {
        private string title = string.Empty;
        private string subTitle = string.Empty;
        private string icon = null;
        private bool isBusy = true;
        private bool isRefreshing;
        private ViewModelError viewModelError;
        private bool isLoadingMore;

        public const string TitlePropertyName = "Title";
        public const string IconPropertyName = "Icon";
        public const string IsBusyPropertyName = "IsBusy";

        protected BaseViewModel()
        {
            this.ViewModelError = ViewModelError.None;
        }

        public virtual string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value, nameof(this.Title)); }
        }

        public string Subtitle
        {
            get { return this.subTitle; }
            set { this.SetProperty(ref this.subTitle, value, nameof(this.Subtitle)); }
        }

        public string Icon
        {
            get { return this.icon; }
            set { this.SetProperty(ref this.icon, value, nameof(this.Icon)); }
        }

        public bool IsLoadingMore
        {
            get { return this.isLoadingMore; }
            set { this.SetProperty(ref this.isLoadingMore, value, nameof(this.IsLoadingMore)); }
        }

        public virtual bool IsBusy
        {
            get { return this.isBusy; }
            set
            {
                if (this.SetProperty(ref this.isBusy, value, nameof(this.IsBusy)))
                {
                    this.RaisePropertyChanged(nameof(this.IsNotBusy));
                    this.RaisePropertyChanged(nameof(this.HasViewModelError));
                    this.RaisePropertyChanged(nameof(this.IsNotBusyAndHasNoViewModelError));
                    this.OnBusyChanged(value);
                }
            }
        }

        public bool IsNotBusy
        {
            get { return !this.isBusy; }
        }

        protected virtual void OnBusyChanged(bool busy)
        {
        }

        public ICommand RefreshCommand => new Command(async () => await this.InternalRefreshList());

        private async Task InternalRefreshList()
        {
            this.IsRefreshing = true;

            await this.OnRefreshList();

            this.IsRefreshing = false;
        }

        protected virtual async Task OnRefreshList()
        {
            await Task.FromResult<object>(null);
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetProperty(ref this.isRefreshing, value, nameof(this.IsRefreshing)); }
        }

        public virtual ViewModelError ViewModelError
        {
            get { return this.viewModelError; }
            set
            {
                if (this.SetProperty(ref this.viewModelError, value, nameof(this.ViewModelError)))
                {
                    this.RaisePropertyChanged(nameof(this.HasViewModelError));
                    this.RaisePropertyChanged(nameof(this.IsNotBusyAndHasNoViewModelError));
                }
            }
        }

        public bool HasViewModelError => this.IsNotBusy && this.viewModelError.HasError;

        public bool IsNotBusyAndHasNoViewModelError => this.IsNotBusy && this.viewModelError.HasError == false;
    }
}