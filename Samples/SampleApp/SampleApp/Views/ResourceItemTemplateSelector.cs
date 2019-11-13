using System.Collections.Generic;
using CrossPlatformLibrary.Forms.Themes;
using SampleApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace SampleApp.Views
{
    public class ResourceItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ColorTemplate { get; set; }

        public DataTemplate FontTemplate { get; set; }

        public DataTemplate ThicknessTemplate { get; set; }

        public DataTemplate GenericTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is ResourceViewModel resource && resource.Value != null)
            {
                var type = resource.Value.GetType();
                if (type == typeof(Color))
                {
                    return this.ColorTemplate;
                }
                
                if (type == typeof(FontElement))
                {
                    return this.FontTemplate;
                }
                
                if (type == typeof(Thickness))
                {
                    return this.ThicknessTemplate;
                }
            }

            return this.GenericTemplate;
        }
    }
}
