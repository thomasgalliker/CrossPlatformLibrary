using System.Collections;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatformLibrary.Forms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrilldownButtonList : GridZero
    {
        public DrilldownButtonList()
        {
            this.InitializeComponent();
        }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(
                nameof(ItemsSource),
                typeof(IEnumerable),
                typeof(DrilldownButtonList),
                null,
                BindingMode.OneWay);

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)this.GetValue(ItemsSourceProperty);
            set => this.SetValue(ItemsSourceProperty, value);
        }

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(
                nameof(ItemTemplate),
                typeof(DataTemplate),
                typeof(DrilldownButtonList),
                null,
                propertyChanged: OnItemTemplateChanged);

        private static void OnItemTemplateChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)this.GetValue(ItemTemplateProperty);
            set => this.SetValue(ItemTemplateProperty, value);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var bc = this.BindingContext;
            foreach (var child in this.ItemsSource.OfType<BindableObject>())
            {
                SetInheritedBindingContext(child, bc);
            }
        }
    }
}