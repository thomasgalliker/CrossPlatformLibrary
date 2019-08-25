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