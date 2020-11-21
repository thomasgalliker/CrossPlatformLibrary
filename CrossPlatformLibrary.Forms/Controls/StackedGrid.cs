using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Input;
using CrossPlatformLibrary.Extensions;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public class StackedGrid : Grid
    {
        private INotifyCollectionChanged sourceCollection;
        private ICommand innerSelectedCommand;

        public event EventHandler SelectedItemChanged;

        public static readonly BindableProperty SelectedCommandProperty = BindableProperty.Create(
            nameof(SelectedCommand),
            typeof(ICommand),
            typeof(StackedGrid), null);

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IEnumerable),
            typeof(StackedGrid),
            default(IEnumerable<object>),
            BindingMode.TwoWay,
            propertyChanged: ItemsSourceChanged);

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
            nameof(SelectedItem),
            typeof(object),
            typeof(StackedGrid),
            null,
            BindingMode.TwoWay,
            propertyChanged: OnSelectedItemChanged);

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(StackedGrid),
            default(DataTemplate));

        public virtual ICommand SelectedCommand
        {
            get => (ICommand)this.GetValue(SelectedCommandProperty);
            set => this.SetValue(SelectedCommandProperty, value);
        }

        public virtual IEnumerable ItemsSource
        {
            get => (IEnumerable)this.GetValue(ItemsSourceProperty);
            set => this.SetValue(ItemsSourceProperty, value);
        }

        public virtual object SelectedItem
        {
            get => (object)this.GetValue(SelectedItemProperty);
            set => this.SetValue(SelectedItemProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)this.GetValue(ItemTemplateProperty);
            set => this.SetValue(ItemTemplateProperty, value);
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsLayout = (StackedGrid)bindable;
            itemsLayout.HookUp();
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
            this.Children.Clear();

            this.innerSelectedCommand = new Command<View>(
                view =>
                {
                    this.SelectedItem = view.BindingContext;
                    this.SelectedItem = null; // Allowing item second time selection
                });

            if (this.ItemsSource == null)
            {
                return;
            }

            this.ColumnDefinitions.Clear();
            var numberOfColumns = this.ItemsSource.GetCount();
            for (var i = 0; i < numberOfColumns; i++)
            {
                // Item column
                this.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

                // Separator column
                if (i < numberOfColumns - 1)
                {
                    this.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1) });
                }
            }

            var x = 0;
            foreach (var item in this.ItemsSource)
            {
                // Add item
                this.Children.Add(this.GetItemView(item), x++, 0);

                // Add separator
                if (x < this.ColumnDefinitions.Count - 1)
                {
                    var separator = this.CreateSeparator();
                    this.Children.Add(separator, x++, 0);
                }
            }

            this.SelectedItem = null;
        }

        private Grid CreateSeparator()
        {
            var separatorGrid = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = 0,
                Padding = 0,
            };
            separatorGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(.025, GridUnitType.Star) });
            separatorGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(.95, GridUnitType.Star) });
            separatorGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(.025, GridUnitType.Star) });
            separatorGrid.Children.Add(new ContentView { BackgroundColor = this.BackgroundColor }, 0, 0);
            //separatorGrid.Children.Add(new ContentView { BackgroundColor = ((View)this.Parent).BackgroundColor }, 0, 1);
            separatorGrid.Children.Add(new ContentView { BackgroundColor = this.BackgroundColor }, 0, 2);
            return separatorGrid;
        }

        protected virtual View GetItemView(object item)
        {
            var view = BindingHelper.CreateContent(this.ItemTemplate, item, this);
            view.BindingContext = item;

            var gesture = new TapGestureRecognizer { Command = this.innerSelectedCommand, CommandParameter = view };

            this.AddGesture(view, gesture);

            return view;
        }

        private void AddGesture(View view, TapGestureRecognizer gesture)
        {
            var gestures = view.GestureRecognizers;
            if (!gestures.Contains(gesture))
            {
                view.GestureRecognizers.Add(gesture);
            }

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

        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsView = (StackedGrid)bindable;
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