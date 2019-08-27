using System.Collections.Generic;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class ColorResource : BindableObject
    {
        public ColorResource(KeyValuePair<string, object> colorResource)
        {
            this.Id = colorResource.Key;
            this.Color = colorResource.Value as Color?;
        }

        public string Id { get; set; }

        public Color? Color { get; set; }
    }
}