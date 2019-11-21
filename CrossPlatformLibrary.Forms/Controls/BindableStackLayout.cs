using System.Collections.Generic;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    /// <summary>
    ///     A <see cref="StackLayout" /> that can be bound to a <see cref="DataTemplate" /> and data source.
    ///     <see cref="BindableStackLayout" /> is just an bindable extension to <seealso cref="StackLayout" />,
    ///     it does not support any item selection commanding (see <seealso cref="StackedList" /> if this is required).
    /// </summary>
    public class BindableStackLayout : StackLayout
    {
        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(
                propertyName: nameof(ItemTemplate),
                returnType: typeof(DataTemplate),
                declaringType: typeof(BindableStackLayout),
                defaultValue: default(DataTemplate),
                propertyChanged: OnItemTemplateChanged);

        /// <summary>
        ///     Gets or sets the <see cref="DataTemplate" />.
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)this.GetValue(ItemTemplateProperty);
            set => this.SetValue(ItemTemplateProperty, value);
        }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(
                propertyName: nameof(ItemsSource),
                returnType: typeof(IEnumerable<object>),
                declaringType: typeof(BindableStackLayout),
                propertyChanged: OnItemsSourceChanged);

        /// <summary>
        ///     Gets or sets the collection of view models to bind to the item views.
        /// </summary>
        public IEnumerable<object> ItemsSource
        {
            get => (IEnumerable<object>)this.GetValue(ItemsSourceProperty);
            set => this.SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        ///     Called when <see cref="ItemTemplate" /> changes.
        /// </summary>
        /// <param name="bindable">The <see cref="BindableObject" /> being changed.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void OnItemTemplateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var layout = (BindableStackLayout)bindable;
            if (newValue == null)
            {
                return;
            }

            layout.PopulateItems();
        }

        /// <summary>
        ///     Called when <see cref="ItemsSource" /> is changed.
        /// </summary>
        /// <param name="bindable">The <see cref="BindableObject" /> being changed.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var layout = (BindableStackLayout)bindable;
            if (newValue == null)
            {
                return;
            }

            layout.PopulateItems();
        }

        /// <summary>
        ///     Creates and binds the item views based on <see cref="ItemTemplate" />.
        /// </summary>
        private void PopulateItems()
        {
            var items = this.ItemsSource;
            if (items == null || this.ItemTemplate == null)
            {
                return;
            }

            var children = this.Children;
            children.Clear();

            foreach (var item in items)
            {
                children.Add(this.InflateView(item));
            }
        }

        /// <summary>
        ///     Inflates an item view using the correct <see cref="DataTemplate" /> for the given view model.
        /// </summary>
        /// <param name="item">The view model to bind the item view to.</param>
        /// <returns>The new view with the view model as its binding context.</returns>
        private View InflateView(object item)
        {
            var view = BindingHelper.CreateContent(this.ItemTemplate, item, this);
            view.BindingContext = item;
            return view;
        }
    }
}