using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public class DrilldownCellTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DrilldownButtonCellTemplate { get; set; }

        public DataTemplate DrilldownSwitchCellTemplate { get; set; }
        
        public DataTemplate CustomDrilldownCellTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is IDrilldownButtonView)
            {
                return this.DrilldownButtonCellTemplate;
            }

            if (item is IDrilldownSwitchView)
            {
                return this.DrilldownSwitchCellTemplate;
            }

            return this.CustomDrilldownCellTemplate;
        }
    }
}