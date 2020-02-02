using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public class ContentControl : ContentView
    {
        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(
                nameof(ItemTemplate),
                typeof(DataTemplate),
                typeof(ContentControl),
                null,
                propertyChanged: OnItemTemplateChanged);

        private static void OnItemTemplateChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var contentControl = (ContentControl)bindable;
            var view = BindingHelper.CreateContent(contentControl.ItemTemplate, newvalue, contentControl);
            contentControl.Content = view;
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)this.GetValue(ItemTemplateProperty);
            set => this.SetValue(ItemTemplateProperty, value);
        }
    }
}