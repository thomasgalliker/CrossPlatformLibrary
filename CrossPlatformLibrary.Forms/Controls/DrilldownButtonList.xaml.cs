using System.Collections;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatformLibrary.Forms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrilldownButtonList : Grid
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
                BindingMode.OneWay,
                null,
                propertyChanged: OnItemsSourcePropertyChanged);

        private static void OnItemsSourcePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var drilldownButtonList = (DrilldownButtonList)bindable;

            var newItems = newvalue as IEnumerable;
            if (newItems == null)
            {
                return;
            }

            //var bc = drilldownButtonList.BindingContext;
            //foreach (var child in newItems.OfType<BindableObject>())
            //{
            //    SetInheritedBindingContext(child, bc);
            //}
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)this.GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, value); }
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