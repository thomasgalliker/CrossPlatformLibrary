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

        public static readonly BindableProperty SpacingProperty =
            BindableProperty.Create(
                nameof(Spacing),
                typeof(double),
                typeof(StackedList),
                6.0d,
                BindingMode.OneWay,propertyChanged: OnSpacingPropertyChanged);

        private static void OnSpacingPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is StackedList stackedList && stackedList.itemsStackLayout != null && newvalue is double newValue)
            {
                stackedList.itemsStackLayout.Spacing = newValue;
            }
        }

        public double Spacing
        {
            get => (double)this.GetValue(SpacingProperty);
            set => this.SetValue(SpacingProperty, value);
        }

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
            get => (ICommand)this.GetValue(SelectedCommandProperty);
            set => this.SetValue(SelectedCommandProperty, value);
        }

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)this.GetValue(ItemsSourceProperty);
            set => this.SetValue(ItemsSourceProperty, value);
        }

        public object SelectedItem
        {
            get => this.GetValue(SelectedItemProperty);
            set => this.SetValue(SelectedItemProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)this.GetValue(ItemTemplateProperty);
            set => this.SetValue(ItemTemplateProperty, value);
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsLayout = (StackedList)bindable;
            itemsLayout.HookUp();
        }

        public static readonly BindableProperty SelectionModeProperty =
            BindableProperty.Create(
                nameof(SelectionMode),
                typeof(SelectionMode),
                typeof(StackedList),
                SelectionMode.None);

        public SelectionMode SelectionMode
        {
            get => (SelectionMode)this.GetValue(SelectionModeProperty);
            set => this.SetValue(SelectionModeProperty, value);
        }

        public StackedList()
        {
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
            try
            {
                this.itemsStackLayout.Children.Clear();
                this.itemsStackLayout.Spacing = this.Spacing;

                this.innerSelectedCommand = new Command<View>(
                    view =>
                    {
                        this.SelectedItem = view.BindingContext;

                        if (this.SelectionMode == SelectionMode.None)
                        {
                            this.SelectedItem = null; // Allowing item second time selection
                        }
                    });

                var listOrientation = this.ListOrientation;
                this.itemsStackLayout.Orientation = listOrientation;
                this.scrollView.Orientation = listOrientation == StackOrientation.Horizontal ? ScrollOrientation.Horizontal : ScrollOrientation.Vertical;

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
            catch (Exception ex)
            {
                Console.WriteLine($"SetItems failed with exception: {ex}");
                throw;
            }
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