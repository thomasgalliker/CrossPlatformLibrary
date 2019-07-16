using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Input;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public class StackedList : Grid
    {
        private INotifyCollectionChanged sourceCollection;
        private ICommand innerSelectedCommand;
        private readonly NoBarsScrollViewer scrollView;
        private readonly StackLayout itemsStackLayout;

        public event EventHandler SelectedItemChanged;

        public StackOrientation ListOrientation { get; set; }

        public double Spacing { get; set; }

        public static readonly BindableProperty SelectedCommandProperty = BindableProperty.Create(
            nameof(SelectedCommand),
            typeof(ICommand),
            typeof(StackedList),
            null);

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IEnumerable),
            typeof(StackedList),
            default(IEnumerable<object>),
            BindingMode.TwoWay,
            propertyChanged: ItemsSourceChanged);

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
            nameof(SelectedItem),
            typeof(object),
            typeof(StackedList),
            null,
            BindingMode.TwoWay,
            propertyChanged: OnSelectedItemChanged);

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(StackedList),
            default(DataTemplate));

        public ICommand SelectedCommand
        {
            get { return (ICommand)this.GetValue(SelectedCommandProperty); }
            set { this.SetValue(SelectedCommandProperty, value); }
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)this.GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, value); }
        }

        public object SelectedItem
        {
            get { return (object)this.GetValue(SelectedItemProperty); }
            set { this.SetValue(SelectedItemProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)this.GetValue(ItemTemplateProperty); }
            set { this.SetValue(ItemTemplateProperty, value); }
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsLayout = (StackedList)bindable;
            itemsLayout.HookUp();
        }

        public StackedList()
        {
            this.Spacing = 5;
            this.scrollView = new NoBarsScrollViewer();
            this.itemsStackLayout = new StackLayout
            {
                Padding = this.Padding,
                Spacing = this.Spacing,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            this.scrollView.Content = this.itemsStackLayout;
            this.Children.Add(this.scrollView);
        }

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
            this.SetItems();
        }

        protected virtual void SetItems()
        {
            this.itemsStackLayout.Children.Clear();
            this.itemsStackLayout.Spacing = this.Spacing;

            this.innerSelectedCommand = new Command<View>(
                view =>
                {
                    this.SelectedItem = view.BindingContext;
                    this.SelectedItem = null; // Allowing item second time selection
                });

            this.itemsStackLayout.Orientation = this.ListOrientation;
            this.scrollView.Orientation = this.ListOrientation == StackOrientation.Horizontal ? ScrollOrientation.Horizontal : ScrollOrientation.Vertical;

            if (this.ItemsSource == null)
            {
                return;
            }

            foreach (var item in this.ItemsSource)
            {
                this.itemsStackLayout.Children.Add(this.GetItemView(item));
            }

            this.SelectedItem = null;
        }

        protected virtual View GetItemView(object item)
        {
            var content = this.ItemTemplate.CreateContent();
            View view = null;
            var viewCell = content as ViewCell;
            if (viewCell != null)
            {
                view = viewCell.View;
            }
            else
            {
                view = content as View;
                if (view == null)
                {
                    throw new Exception("ItemTemplate must either be a View or a ViewCell");
                }
            }

            view.BindingContext = item;

            var gesture = new TapGestureRecognizer { Command = this.innerSelectedCommand, CommandParameter = view };

            this.AddGesture(view, gesture);

            return view;
        }

        private void AddGesture(View view, TapGestureRecognizer gesture)
        {
            view.GestureRecognizers.Add(gesture);

            var layout = view as Layout<View>;

            if (layout == null)
            {
                return;
            }

            foreach (var child in layout.Children)
            {
                this.AddGesture(child, gesture);
            }
        }

        //protected override void OnPropertyChanged(string propertyName = null)
        //{
        //    if (propertyName == StackedList.ItemsSourceProperty.PropertyName)
        //    {
        //        this.SetItems();
        //    }
        //}

        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsView = (StackedList)bindable;
            if (newValue == oldValue || newValue == null)
            {
                return;
            }

            itemsView.SelectedItemChanged?.Invoke(itemsView, EventArgs.Empty);

            if (itemsView.SelectedCommand?.CanExecute(newValue) ?? false)
            {
                itemsView.SelectedCommand?.Execute(newValue);
            }
        }
    }
}