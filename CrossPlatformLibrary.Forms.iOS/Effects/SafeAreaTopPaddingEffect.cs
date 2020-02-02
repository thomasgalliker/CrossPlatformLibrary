using CrossPlatformLibrary.Forms.Effects;
using CrossPlatformLibrary.Internals;
using Xamarin.Forms;
using SafeAreaTopPaddingEffect = CrossPlatformLibrary.Forms.iOS.Effects.SafeAreaTopPaddingEffect;

[assembly: ExportEffect(typeof(SafeAreaTopPaddingEffect), nameof(SafeAreaTopPaddingEffect))]

namespace CrossPlatformLibrary.Forms.iOS.Effects
{
    /// <summary>
    ///     Adds safe-area padding to the top of the UI element.
    /// </summary>
    public class SafeAreaTopPaddingEffect : SafeAreaPaddingEffect
    {
        private readonly ITracer tracer;
        private readonly SafeAreaPaddingLayout safeAreaPaddingLayout;

        public SafeAreaTopPaddingEffect()
        {
            this.tracer = Tracer.Current;
            this.safeAreaPaddingLayout = new SafeAreaPaddingLayout(SafeAreaPaddingLayout.PaddingPosition.Top);
        }

        protected override Thickness GetSafeAreaPadding(Thickness originalPadding, SafeAreaPaddingLayout _, Thickness safeAreaInsets, bool includeStatusBar)
        {
            var safeAreaPadding = base.GetSafeAreaPadding(originalPadding, this.safeAreaPaddingLayout, safeAreaInsets, includeStatusBar);

            this.tracer.Info($"SafeAreaTopPaddingEffect.GetSafeAreaPadding returns safeAreaPadding={{{safeAreaPadding.Left}, {safeAreaPadding.Top}, {safeAreaPadding.Right}, {safeAreaPadding.Bottom}}}");
            return safeAreaPadding;
        }
    }
}