using System.Linq;
using CrossPlatformLibrary.Internals;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Effects
{
    public static class SafeAreaPadding
    {
        public static readonly string EffectName = $"{Effects.Prefix}.{nameof(SafeAreaPaddingEffect)}";

        public static readonly BindableProperty ShouldIncludeStatusBarProperty =
            BindableProperty.CreateAttached(
                "ShouldIncludeStatusBar",
                typeof(bool),
                typeof(SafeAreaPadding),
                false);

        public static readonly BindableProperty EnableSafeAreaPaddingProperty =
            BindableProperty.CreateAttached(
                "EnableSafeAreaPadding",
                typeof(bool),
                typeof(SafeAreaPadding),
                false,
                propertyChanged: OnEnableSafeAreaPadding);

        public static readonly BindableProperty SafeAreaInsetsProperty =
            BindableProperty.CreateAttached(
                "SafeAreaInsets",
                typeof(Thickness),
                typeof(SafeAreaPadding),
                new Thickness(0, 0, 0, 0));

        public static bool GetShouldIncludeStatusBar(BindableObject element)
        {
            return (bool)element.GetValue(ShouldIncludeStatusBarProperty);
        }

        public static void SetEnableSafeAreaPadding(BindableObject element, bool value)
        {
            element.SetValue(EnableSafeAreaPaddingProperty, value);
        }

        public static Thickness GetSafeAreaInsets(BindableObject element)
        {
            return (Thickness)element.GetValue(SafeAreaInsetsProperty);
        }

        public static void SetSafeAreaInsets(BindableObject element, Thickness value)
        {
            element.SetValue(SafeAreaInsetsProperty, value);
        }

        public static bool GetEnableSafeAreaPadding(BindableObject element)
        {
            return (bool)element.GetValue(EnableSafeAreaPaddingProperty);
        }

        public static void SetShouldIncludeStatusBar(BindableObject element, bool value)
        {
            element.SetValue(ShouldIncludeStatusBarProperty, value);
        }

        private static void OnEnableSafeAreaPadding(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue is bool isEnable && isEnable)
            {
                AttachEffect(bindable);
            }
            else
            {
                DetachEffect(bindable);
            }
        }

        static void AttachEffect(BindableObject bindableObject)
        {
            Tracer.Current.Debug($"AttachEffect for {bindableObject?.GetType().Name}...");
            if (!(bindableObject is IElementController controller) || controller == null || controller.EffectIsAttached(EffectName))
            {
                return;
            }

            if (bindableObject is Element element)
            {
                element.Effects.Add(Effect.Resolve(EffectName));
                Tracer.Current.Debug($"AttachEffect added for {bindableObject?.GetType().Name}...");
            }
        }

        static void DetachEffect(BindableObject bindableObject)
        {
            Tracer.Current.Debug($"DetachEffect for {bindableObject?.GetType().Name}...");
            if (!(bindableObject is IElementController controller) || controller == null || !controller.EffectIsAttached(EffectName))
            {
                return;
            }

            if (bindableObject is Element element)
            {
                var toRemove = element.Effects.FirstOrDefault(e => e.ResolveId == Effect.Resolve(EffectName).ResolveId);
                if (toRemove != null)
                {
                    element.Effects.Remove(toRemove);
                    Tracer.Current.Debug($"DetachEffect removed for {bindableObject?.GetType().Name}...");
                }
            }
        }

        public static IPlatformElementConfiguration<Xamarin.Forms.PlatformConfiguration.iOS, Element> UseSafeArea(this IPlatformElementConfiguration<Xamarin.Forms.PlatformConfiguration.iOS, Element> config, bool includeStatusBar)
        {
            SetShouldIncludeStatusBar(config.Element, includeStatusBar);
            SetEnableSafeAreaPadding(config.Element, true);
            return config;
        }

        public static IPlatformElementConfiguration<Xamarin.Forms.PlatformConfiguration.iOS, Element> UseSafeAreaWithInsets(this IPlatformElementConfiguration<Xamarin.Forms.PlatformConfiguration.iOS, Element> config, bool includeStatusBar, Thickness padding)
        {
            SetShouldIncludeStatusBar(config.Element, includeStatusBar);
            SetEnableSafeAreaPadding(config.Element, true);
            SetSafeAreaInsets(config.Element, padding);
            return config;
        }

        public static IPlatformElementConfiguration<Xamarin.Forms.PlatformConfiguration.iOS, Element> SetSafeAreaInsets(this IPlatformElementConfiguration<Xamarin.Forms.PlatformConfiguration.iOS, Element> config, Thickness padding)
        {
            if (GetEnableSafeAreaPadding(config.Element))
            {
                SetSafeAreaInsets(config.Element, padding);
            }

            return config;
        }
    }
}