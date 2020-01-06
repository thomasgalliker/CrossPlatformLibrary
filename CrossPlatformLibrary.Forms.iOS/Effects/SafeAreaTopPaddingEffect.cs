using CrossPlatformLibrary.Forms.iOS.Effects;
using CrossPlatformLibrary.Internals;
using Xamarin.Forms;

[assembly: ExportEffect(typeof(SafeAreaTopPaddingEffect), nameof(SafeAreaTopPaddingEffect))]

namespace CrossPlatformLibrary.Forms.iOS.Effects
{
    /// <summary>
    ///     Adds safe-area padding to the top of the UI element.
    /// </summary>
    public class SafeAreaTopPaddingEffect : SafeAreaPaddingEffect
    {
        private readonly ITracer tracer;

        public SafeAreaTopPaddingEffect()
        {
            this.tracer = Tracer.Current;
        }

        protected override Thickness GetSafeAreaPadding(Thickness originalPadding, Thickness safeAreaInsets, bool includeStatusBar)
        {
            var safeAreaPadding = base.GetSafeAreaPadding(originalPadding, safeAreaInsets, includeStatusBar);
            safeAreaPadding.Bottom = 0;

            this.tracer.Info($"SafeAreaTopPaddingEffect.GetSafeAreaPadding returns safeAreaPadding={{{safeAreaPadding.Left}, {safeAreaPadding.Top}, {safeAreaPadding.Right}, {safeAreaPadding.Bottom}}}");
            return safeAreaPadding;
        }
    }
}