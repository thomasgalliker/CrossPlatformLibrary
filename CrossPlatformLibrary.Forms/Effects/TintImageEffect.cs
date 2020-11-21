using System.Linq;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Effects
{
    public class TintImageEffect : RoutingEffect
    {
        public TintImageEffect()
          : base($"{Effects.Prefix}.{nameof(TintImageEffect)}")
        {
        }

        public static readonly BindableProperty TintColorProperty =
            BindableProperty.CreateAttached(
                "TintImageEffect",
                typeof(Color),
                typeof(TintImageEffect),
                default(Color),
                propertyChanged: OnTintColorChanged);

        public static void OnTintColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is View view))
            {
                return;
            }

            if (!view.Effects.Any(e => e is TintImageEffect))
            {
                view.Effects.Add(new TintImageEffect());
            }
        }

        public static Color GetTintColor(BindableObject bindable)
        {
            return (Color)bindable.GetValue(TintColorProperty);
        }

        public static void SetTintColor(BindableObject bindable, Color value)
        {
            bindable.SetValue(TintColorProperty, value);
        }
    }
}