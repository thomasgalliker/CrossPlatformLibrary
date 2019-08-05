using System;
using System.Collections;
using System.Collections.Specialized;
using System.Reflection;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public class BindablePicker : Picker
    {
        private bool disableNestedCalls;

        public new static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(
                nameof(ItemsSource),
                typeof(IEnumerable),
                typeof(BindablePicker),
                null,
                propertyChanged: OnItemsSourceChanged);

        public new static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(
                nameof(SelectedItem),
                returnType: typeof(object),
                declaringType: typeof(BindablePicker),
                defaultValue: null,
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: OnSelectedItemChanged);

        public static readonly BindableProperty SelectedValueProperty =
            BindableProperty.Create(
                nameof(SelectedValue),
                typeof(object),
                typeof(BindablePicker),
                null, BindingMode.TwoWay,
                propertyChanged: OnSelectedValueChanged);

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)this.GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, value); }
        }

        public object SelectedItem
        {
            get { return this.GetValue(SelectedItemProperty); }
            set
            {
                if (this.SelectedItem != value)
                {
                    this.SetValue(SelectedItemProperty, value);
                    this.InternalSelectedItemChanged();
                }
            }
        }

        public object SelectedValue
        {
            get { return this.GetValue(SelectedValueProperty); }
            set
            {
                this.SetValue(SelectedValueProperty, value);
                this.InternalSelectedValueChanged();
            }
        }

        public static readonly BindableProperty SelectedValuePathProperty =
            BindableProperty.Create(
                nameof(SelectedValuePath),
                typeof(string),
                typeof(BindablePicker),
                null,
                BindingMode.OneWay);

        public string SelectedValuePath
        {
            get { return (string)this.GetValue(SelectedValuePathProperty); }
            set { this.SetValue(SelectedValuePathProperty, value); }
        }

        public static readonly BindableProperty DisplayMemberPathProperty =
            BindableProperty.Create(
                nameof(DisplayMemberPath),
                typeof(string),
                typeof(BindablePicker),
                null,
                BindingMode.OneWay,
                null,
                OnDisplayMemberPathChanged);

        private static void OnDisplayMemberPathChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is BindablePicker bindablePicker)
            {
                bindablePicker.InstanceOnItemsSourceChanged(bindablePicker.ItemsSource, bindablePicker.ItemsSource);
            }
        }

        public string DisplayMemberPath
        {
            get => (string)this.GetValue(DisplayMemberPathProperty);
            set => this.SetValue(DisplayMemberPathProperty, value);
        }

        public static readonly BindableProperty NullStringProperty =
            BindableProperty.Create(
                nameof(NullString),
                typeof(string),
                typeof(BindablePicker),
                "<null>");

        public string NullString
        {
            get => (string)this.GetValue(NullStringProperty);
            set => this.SetValue(NullStringProperty, value);
        }

        public BindablePicker()
        {
            this.SelectedIndexChanged += this.OnSelectedIndexChanged;
        }

        public event EventHandler<SelectedItemChangedEventArgs> ItemSelected;

        private void InstanceOnItemsSourceChanged(object oldValue, object newValue)
        {
            this.disableNestedCalls = true;
            this.Items.Clear();

            if (oldValue is INotifyCollectionChanged oldCollectionINotifyCollectionChanged)
            {
                oldCollectionINotifyCollectionChanged.CollectionChanged -= this.ItemsSource_CollectionChanged;
            }

            if (newValue is INotifyCollectionChanged newCollectionINotifyCollectionChanged)
            {
                newCollectionINotifyCollectionChanged.CollectionChanged += this.ItemsSource_CollectionChanged;
            }

            if (newValue != null)
            {
                var hasDisplayMemberPath = this.HasDisplayMemberPath;
                var displayMemberPath = this.DisplayMemberPath;

                foreach (var item in (IEnumerable)newValue)
                {
                    object propValue = null;
                    if (hasDisplayMemberPath)
                    {
                        propValue = BindingHelper.GetDisplayMember(item, displayMemberPath);
                    }
                    else
                    {
                        propValue = item;
                    }

                    this.Items.Add(propValue == null ? this.NullString : propValue.ToString());
                }

                this.SelectedIndex = -1;
                this.disableNestedCalls = false;

                if (this.SelectedItem != null)
                {
                    this.InternalSelectedItemChanged();
                }
                else if (hasDisplayMemberPath && this.SelectedValue != null)
                {
                    this.InternalSelectedValueChanged();
                }
            }
            else
            {
                this.disableNestedCalls = true;
                this.SelectedIndex = -1;
                this.SelectedItem = null;
                this.SelectedValue = null;
                this.disableNestedCalls = false;
            }
        }

        private void InternalSelectedItemChanged()
        {
            if (this.disableNestedCalls)
            {
                return;
            }

            var selectedIndex = -1;
            object selectedValue = null;
            if (this.ItemsSource != null)
            {
                var index = 0;
                var selectedValuePath = this.SelectedValuePath;
                var hasSelectedValuePath = !string.IsNullOrWhiteSpace(selectedValuePath);
                foreach (var item in this.ItemsSource)
                {
                    if (item != null && item.Equals(this.SelectedItem))
                    {
                        selectedIndex = index;
                        if (hasSelectedValuePath)
                        {
                            var type = item.GetType();
                            var prop = type.GetRuntimeProperty(selectedValuePath);
                            if (prop == null)
                            {
                                throw new InvalidOperationException($"SelectedValuePath={selectedValuePath} does not exist on item of type {type.Name}.");
                            }
                            selectedValue = prop.GetValue(item);
                        }
                        break;
                    }
                    index++;
                }
            }
            this.disableNestedCalls = true;
            this.SelectedValue = selectedValue;
            this.SelectedIndex = selectedIndex;
            this.disableNestedCalls = false;
        }

        private void InternalSelectedValueChanged()
        {
            if (this.disableNestedCalls)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(this.SelectedValuePath))
            {
                return;
            }
            var selectedIndex = -1;
            object selectedItem = null;
            var hasSelectedValuePath = !string.IsNullOrWhiteSpace(this.SelectedValuePath);
            if (this.ItemsSource != null && hasSelectedValuePath)
            {
                var index = 0;
                foreach (var item in this.ItemsSource)
                {
                    if (item != null)
                    {
                        var type = item.GetType();
                        var prop = type.GetRuntimeProperty(this.SelectedValuePath);
                        if (Equals(prop.GetValue(item), this.SelectedValue))
                        {
                            selectedIndex = index;
                            selectedItem = item;
                            break;
                        }
                    }

                    index++;
                }
            }
            this.disableNestedCalls = true;
            this.SelectedItem = selectedItem;
            this.SelectedIndex = selectedIndex;
            this.disableNestedCalls = false;
        }

        private bool HasDisplayMemberPath => !string.IsNullOrWhiteSpace(this.DisplayMemberPath);

        private void ItemsSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var displayMemberPath = this.DisplayMemberPath;
            var hasDisplayMemberPath = this.HasDisplayMemberPath;
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    if (hasDisplayMemberPath)
                    {
                        var displayMemberString = BindingHelper.GetDisplayMemberString(item, displayMemberPath);
                        this.Items.Add(displayMemberString);
                    }
                    else
                    {
                        this.Items.Add(item == null ? this.NullString : item.ToString());
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var item in e.NewItems)
                {
                    if (hasDisplayMemberPath)
                    {
                        var displayMemberString = BindingHelper.GetDisplayMemberString(item, displayMemberPath);
                        this.Items.Remove(displayMemberString);
                    }
                    else
                    {
                        this.Items.Remove(item.ToString());
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (var item in e.NewItems)
                {
                    if (hasDisplayMemberPath)
                    {
                        var displayMemberString = BindingHelper.GetDisplayMemberString(item, displayMemberPath);
                        this.Items.Remove(displayMemberString);
                    }
                    else
                    {
                        var index = this.Items.IndexOf(item.ToString());
                        if (index > -1)
                        {
                            this.Items[index] = item.ToString();
                        }
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                this.Items.Clear();
                if (e.NewItems != null)
                {
                    foreach (var item in e.NewItems)
                    {
                        if (hasDisplayMemberPath)
                        {
                            var displayMemberString = BindingHelper.GetDisplayMemberString(item, displayMemberPath);
                            this.Items.Remove(displayMemberString);
                        }
                        else
                        {
                            var index = this.Items.IndexOf(item.ToString());
                            if (index > -1)
                            {
                                this.Items[index] = item.ToString();
                            }
                        }
                    }
                }
                else
                {
                    this.disableNestedCalls = true;
                    this.SelectedItem = null;
                    this.SelectedIndex = -1;
                    this.SelectedValue = null;
                    this.disableNestedCalls = false;
                }
            }
        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null && oldValue == null)
            {
                return;
            }

            if (bindable is BindablePicker bindablePicker)
            {
                bindablePicker.InstanceOnItemsSourceChanged(oldValue, newValue);
            }
        }

        private void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.disableNestedCalls)
            {
                return;
            }

            if (this.SelectedIndex < 0 || this.ItemsSource == null || !this.ItemsSource.GetEnumerator().MoveNext())
            {
                this.disableNestedCalls = true;
                if (this.SelectedIndex != -1)
                {
                    this.SelectedIndex = -1;
                }
                this.SelectedItem = null;
                this.SelectedValue = null;
                this.disableNestedCalls = false;
                return;
            }

            this.disableNestedCalls = true;

            var index = 0;
            var selectedValuePath = this.SelectedValuePath;
            var hasSelectedValuePath = !string.IsNullOrWhiteSpace(selectedValuePath);
            foreach (var item in this.ItemsSource)
            {
                if (index == this.SelectedIndex)
                {
                    this.SelectedItem = item;
                    if (hasSelectedValuePath)
                    {
                        var type = item.GetType();
                        var prop = type.GetRuntimeProperty(selectedValuePath);
                        if (prop == null)
                        {
                            throw new InvalidOperationException($"SelectedValuePath={selectedValuePath} does not exist on item of type {type.Name}.");
                        }
                        this.SelectedValue = prop.GetValue(item);
                    }

                    break;
                }
                index++;
            }

            this.disableNestedCalls = false;
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var bindablePicker = (BindablePicker)bindable;
            bindablePicker.ItemSelected?.Invoke(bindablePicker, new SelectedItemChangedEventArgs(newValue));
            bindablePicker.InternalSelectedItemChanged();
        }

        private static void OnSelectedValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var bindablePicker = (BindablePicker)bindable;
            bindablePicker.InternalSelectedValueChanged();
        }
    }
}