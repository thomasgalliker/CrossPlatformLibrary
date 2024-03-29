﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public partial class ValidatableAutoCompleteView : GridZero
    {
        private readonly TaskDelayer taskDelayer;

        private INotifyCollectionChanged sourceCollection;
        private bool selectedItemChanging;
        private bool initialized = false;

        public ValidatableAutoCompleteView()
        {
            this.InitializeComponent();
            this.DebugLayoutBounds();
            this.taskDelayer = new TaskDelayer();
        }

        public new void Focus()
        {
            base.Focus();
            this.Entry.Focus();
        }

        public new void Unfocus()
        {
            this.Entry.Unfocus();
            base.Unfocus();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            this.initialized = this.BindingContext != null;
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(
                nameof(Text),
                typeof(string),
                typeof(ValidatableAutoCompleteView),
                null,
                BindingMode.TwoWay,
                propertyChanged: OnTextPropertyChanged);

        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        private static async void OnTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            Debug.WriteLine($"OnTextPropertyChanged: oldValue={oldValue}, newValue={newValue}");

            var validatableAutoCompleteView = (ValidatableAutoCompleteView)bindable;
            validatableAutoCompleteView.OnPropertyChanged(nameof(ValidatableAutoCompleteView.AnnotationText));

            if (validatableAutoCompleteView.SearchCommand == null)
            {
                // In case no search command is specified by the user,
                // we don't have to do anything here
                return;
            }

            if (validatableAutoCompleteView.selectedItemChanging)
            {
                return;
            }

            var newSearchText = newValue as string;

            var delay = validatableAutoCompleteView.SearchCommandDelay;
            if (delay == TimeSpan.Zero)
            {
                validatableAutoCompleteView.PerformSearchCommand(newSearchText);
            }
            else
            {
                await validatableAutoCompleteView.taskDelayer.RunWithDelay(delay, () => validatableAutoCompleteView.PerformSearchCommand(newSearchText));
            }
        }

        private void PerformSearchCommand(string searchText)
        {
            var searchCommand = this.SearchCommand;
            if (searchCommand != null && searchCommand.CanExecute(searchText))
            {
                searchCommand.Execute(searchText);
            }
        }

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(
                nameof(Placeholder),
                typeof(string),
                typeof(ValidatableAutoCompleteView),
                null,
                BindingMode.OneWay,
                propertyChanged: OnPlaceholderPropertyChanged);

        public string Placeholder
        {
            get => (string)this.GetValue(PlaceholderProperty);
            set => this.SetValue(PlaceholderProperty, value);
        }

        private static void OnPlaceholderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var validatableAutoCompleteView = (ValidatableAutoCompleteView)bindable;
            validatableAutoCompleteView.OnPropertyChanged(nameof(ValidatableAutoCompleteView.AnnotationText));
        }

        public string AnnotationText
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Text))
                {
                    return this.Placeholder;
                }

                return " ";
            }
        }

        public static readonly BindableProperty IsReadonlyProperty =
            BindableProperty.Create(
                nameof(IsReadonly),
                typeof(bool),
                typeof(ValidatableAutoCompleteView),
                false,
                BindingMode.OneWay);

        public bool IsReadonly
        {
            get => (bool)this.GetValue(IsReadonlyProperty);
            set => this.SetValue(IsReadonlyProperty, value);
        }

        public static readonly BindableProperty KeyboardProperty =
            BindableProperty.Create(
                nameof(Keyboard),
                typeof(Keyboard),
                typeof(ValidatableAutoCompleteView),
                Keyboard.Default,
                BindingMode.OneWay);

        public Keyboard Keyboard
        {
            get => (Keyboard)this.GetValue(KeyboardProperty);
            set => this.SetValue(KeyboardProperty, value);
        }

        public static readonly BindableProperty EntryStyleProperty =
            BindableProperty.Create(
                nameof(EntryStyle),
                typeof(Style),
                typeof(ValidatableAutoCompleteView),
                default(Style));

        public Style EntryStyle
        {
            get => (Style)this.GetValue(EntryStyleProperty);
            set => this.SetValue(EntryStyleProperty, value);
        }

        public static readonly BindableProperty SuggestionListStyleProperty =
            BindableProperty.Create(
                nameof(SuggestionListStyle),
                typeof(Style),
                typeof(ValidatableAutoCompleteView),
                default(Style));

        public Style SuggestionListStyle
        {
            get => (Style)this.GetValue(SuggestionListStyleProperty);
            set => this.SetValue(SuggestionListStyleProperty, value);
        }

        public static readonly BindableProperty MaxLengthProperty =
            BindableProperty.Create(
                nameof(MaxLength),
                typeof(int),
                typeof(ValidatableAutoCompleteView),
                int.MaxValue,
                BindingMode.OneWay);

        public int MaxLength
        {
            get => (int)this.GetValue(MaxLengthProperty);
            set => this.SetValue(MaxLengthProperty, value);
        }

        public static readonly BindableProperty ValidationErrorsProperty =
            BindableProperty.Create(
                nameof(ValidationErrors),
                typeof(IEnumerable<string>),
                typeof(ValidatableAutoCompleteView),
                default(IEnumerable<string>),
                BindingMode.OneWay);

        public IEnumerable<string> ValidationErrors
        {
            get => (IEnumerable<string>)this.GetValue(ValidationErrorsProperty);
            set => this.SetValue(ValidationErrorsProperty, value);
        }

        public static readonly BindableProperty SearchCommandProperty =
            BindableProperty.Create(
                nameof(SearchCommand),
                typeof(ICommand),
                typeof(ValidatableAutoCompleteView),
                null,
                BindingMode.OneWay);

        public ICommand SearchCommand
        {
            get => (ICommand)this.GetValue(SearchCommandProperty);
            set => this.SetValue(SearchCommandProperty, value);
        }

        public static readonly BindableProperty SearchCommandDelayProperty =
            BindableProperty.Create(
                nameof(SearchCommandDelay),
                typeof(TimeSpan),
                typeof(ValidatableAutoCompleteView),
                TimeSpan.Zero,
                BindingMode.OneWay);

        public TimeSpan SearchCommandDelay
        {
            get => (TimeSpan)this.GetValue(SearchCommandDelayProperty);
            set => this.SetValue(SearchCommandDelayProperty, value);
        }

        public static readonly BindableProperty SuggestedItemsSourceProperty =
            BindableProperty.Create(
                nameof(SuggestedItemsSource),
                typeof(IEnumerable),
                typeof(ValidatableAutoCompleteView),
                null,
                propertyChanged: OnSuggestionItemsSourceChanged);

        private static void OnSuggestionItemsSourceChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var autoCompleteView = (ValidatableAutoCompleteView)bindable;
            autoCompleteView.HookUp();
        }

        private void HookUp()
        {
            // Remove previous collection changed event
            if (this.sourceCollection != null)
            {
                this.sourceCollection.CollectionChanged -= this.HandleSourceCollectionChanged;
            }

            this.sourceCollection = this.SuggestedItemsSource as INotifyCollectionChanged;

            // Subscribe to collection changed event
            if (this.sourceCollection != null)
            {
                this.sourceCollection.CollectionChanged += this.HandleSourceCollectionChanged;
            }

            this.HandleSourceCollectionChanged(this.sourceCollection, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        private void HandleSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Debug.WriteLine($"HandleSourceCollectionChanged: e.Action={e.Action}, e.NewItems.Count={e.NewItems?.Count}");

            if (e.NewItems is IList newItems && newItems.Count > 0)
            {
                this.SuggestionList.IsVisible = true;
            }
            else
            {
                this.SuggestionList.IsVisible = false;
            }
        }

        public IEnumerable SuggestedItemsSource
        {
            get => (IEnumerable)this.GetValue(SuggestedItemsSourceProperty);
            set => this.SetValue(SuggestedItemsSourceProperty, value);
        }

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(
                nameof(SelectedItem),
                typeof(object),
                typeof(ValidatableAutoCompleteView),
                null,
                BindingMode.TwoWay,
                propertyChanged: OnSelectedItemChanged);

        private static void OnSelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            Debug.WriteLine($"OnSelectedItemChanged: oldvalue={oldvalue}, newvalue={newvalue}");

            if (newvalue == null)
            {
                return;
            }

            var autoCompleteView = (ValidatableAutoCompleteView)bindable;
            autoCompleteView.SuggestionList.IsVisible = false;

            autoCompleteView.selectedItemChanging = true;
            autoCompleteView.Entry.Text = autoCompleteView.GetDisplayString(newvalue);
            autoCompleteView.selectedItemChanging = false;
        }

        private string GetDisplayString(object value)
        {
            var displayMemberPath = this.DisplayMemberPath;
            if (!string.IsNullOrEmpty(displayMemberPath))
            {
                var displayMemberString = BindingHelper.GetDisplayMemberString(value, this.DisplayMemberPath);
                return displayMemberString;
            }

            return $"{value}";
        }

        public object SelectedItem
        {
            get => (object)this.GetValue(SelectedItemProperty);
            set
            {
                if (value == null && !this.initialized)
                {
                    return;
                }

                this.SetValue(SelectedItemProperty, value);
            }
        }

        public static readonly BindableProperty SuggestedItemTemplateProperty =
            BindableProperty.Create(
                nameof(SuggestedItemTemplate),
                typeof(DataTemplate),
                typeof(ValidatableAutoCompleteView),
                default(DataTemplate));

        public DataTemplate SuggestedItemTemplate
        {
            get => (DataTemplate)this.GetValue(SuggestedItemTemplateProperty);
            set => this.SetValue(SuggestedItemTemplateProperty, value);
        }

        public static readonly BindableProperty SuggestedItemsSpacingProperty =
            BindableProperty.Create(
                nameof(SuggestedItemsSpacing),
                typeof(double),
                typeof(ValidatableAutoCompleteView),
                6.0);

        public double SuggestedItemsSpacing
        {
            get => (double)this.GetValue(SuggestedItemsSpacingProperty);
            set => this.SetValue(SuggestedItemsSpacingProperty, value);
        }

        public static readonly BindableProperty DisplayMemberPathProperty =
            BindableProperty.Create(
                nameof(DisplayMemberPath),
                typeof(string),
                typeof(ValidatableAutoCompleteView));

        public string DisplayMemberPath
        {
            get => (string)this.GetValue(DisplayMemberPathProperty);
            set => this.SetValue(DisplayMemberPathProperty, value);
        }

        public event EventHandler Completed
        {
            add => this.Entry.Completed += value;
            remove => this.Entry.Completed -= value;
        }

        public new event EventHandler<FocusEventArgs> Focused
        {
            add => this.Entry.Focused += value;
            remove => this.Entry.Focused -= value;
        }

        public new event EventHandler<FocusEventArgs> Unfocused
        {
            add => this.Entry.Unfocused += value;
            remove => this.Entry.Unfocused -= value;
        }

        public event EventHandler<TextChangedEventArgs> TextChanged
        {
            add => this.Entry.TextChanged += value;
            remove => this.Entry.TextChanged -= value;
        }
    }
}