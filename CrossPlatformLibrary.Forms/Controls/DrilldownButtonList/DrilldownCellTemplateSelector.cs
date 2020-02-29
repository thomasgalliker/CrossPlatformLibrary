using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public class DrilldownCellTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DrilldownButtonCellTemplate { get; set; }

        public DataTemplate DrilldownSwitchCellTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is DrilldownButtonView)
            {
                return this.DrilldownButtonCellTemplate;
            }

            if (item is DrilldownSwitchView)
            {
                return this.DrilldownSwitchCellTemplate;
            }

            return null;
        }
    }
}