using System.Collections.Specialized;
using CrossPlatformLibrary.Extensions;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public class NonScrollableListView : ExtendedListView
    {
        public NonScrollableListView() : base(ListViewCachingStrategy.RecycleElement)
        {
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == ItemsSourceProperty.PropertyName)
            {
                if (this.RowHeight > 0)
                {
                    this.HeightRequest = 0;
                    this.HookUp();
                }
            }
        }

        private INotifyCollectionChanged sourceCollection;

        private void HookUp()
        {
            // Remove previous collection changed event
            if (this.sourceCollection != null)
            {
                this.sourceCollection.CollectionChanged -= this.HandleSourceCollectionChanged;
            }

            this.sourceCollection = this.ItemsSource as INotifyCollectionChanged;

            // Subscribe to collection changed event
            if (this.sourceCollection != null)
            {
                this.sourceCollection.CollectionChanged += this.HandleSourceCollectionChanged;
            }

            this.HandleSourceCollectionChanged(this.sourceCollection, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        private void HandleSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.HeightRequest = this.ItemsSource.CreateList().Count * this.RowHeight;
        }
    }
}

