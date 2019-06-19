using System;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms
{
    public class ExtendedListView : ListView
    {
        public static readonly BindableProperty ItemTemplateSelectorProperty = BindableProperty.Create(
            nameof(ItemTemplateSelector),
            typeof(DataTemplateSelector),
            typeof(ExtendedListView),
            null,
            propertyChanged: OnDataTemplateSelectorChanged);

        private DataTemplateSelector currentItemSelector;

        public ExtendedListView()
        {
        }

        public ExtendedListView(ListViewCachingStrategy cachingStrategy) : base(cachingStrategy)
        {
        }

        private static void OnDataTemplateSelectorChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((ExtendedListView)bindable).OnDataTemplateSelectorChanged((DataTemplateSelector)oldvalue, (DataTemplateSelector)newvalue);
        }

        protected virtual void OnDataTemplateSelectorChanged(DataTemplateSelector oldValue, DataTemplateSelector newValue)
        {
            // check to see we don't have an ItemTemplate set
            if (this.ItemTemplate != null && newValue != null)
            {
                throw new ArgumentException("Cannot set both ItemTemplate and ItemTemplateSelector", "ItemTemplateSelector");
            }

            this.currentItemSelector = newValue;
        }

        protected override Cell CreateDefault(object item)
        {
            if (this.currentItemSelector != null)
            {
                var template = this.currentItemSelector.SelectTemplate(item, this);
                if (template != null)
                {
                    var templateInstance = template.CreateContent();
                    // see if it's a view or a cell
                    var templateView = templateInstance as View;
                    var templateCell = templateInstance as Cell;
                    if (templateView == null && templateCell == null)
                    {
                        throw new InvalidOperationException("DataTemplate must be either a Cell or a View");
                    }

                    if (templateView != null) // we got a view, wrap in a cell
                    {
                        templateCell = new ViewCell { View = templateView };
                    }

                    return templateCell;
                }
            }

            return base.CreateDefault(item);
        }

        public DataTemplateSelector ItemTemplateSelector
        {
            get { return (DataTemplateSelector)this.GetValue(ItemTemplateSelectorProperty); }
            set { this.SetValue(ItemTemplateSelectorProperty, value); }
        }
    }
}