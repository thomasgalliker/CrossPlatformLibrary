using CrossPlatformLibrary.Forms.Effects;
using CrossPlatformLibrary.Internals;
using Xamarin.Forms;
using SafeAreaBottomPaddingEffect = CrossPlatformLibrary.Forms.iOS.Effects.SafeAreaBottomPaddingEffect;

[assembly: ExportEffect(typeof(SafeAreaBottomPaddingEffect), nameof(SafeAreaBottomPaddingEffect))]

namespace CrossPlatformLibrary.Forms.iOS.Effects
{
    /// <summary>
    ///     Adds safe-area padding to the bottom of the UI element.
    /// </summary>
    public class SafeAreaBottomPaddingEffect : SafeAreaPaddingEffect
    {
        private readonly ITracer tracer;
        private readonly SafeAreaPaddingLayout safeAreaPaddingLayout;

        public SafeAreaBottomPaddingEffect()
        {
            this.tracer = Tracer.Current;
            this.safeAreaPaddingLayout = new SafeAreaPaddingLayout(SafeAreaPaddingLayout.PaddingPosition.Bottom);
        }

        protected override Thickness GetSafeAreaPadding(Thickness originalPadding, SafeAreaPaddingLayout _, Thickness safeAreaInsets, bool includeStatusBar)
        {
            var safeAreaPadding = base.GetSafeAreaPadding(originalPadding, this.safeAreaPaddingLayout, safeAreaInsets, includeStatusBar: false);

            this.tracer.Info($"SafeAreaBottomPaddingEffect.GetSafeAreaPadding returns safeAreaPadding={{{safeAreaPadding.Left}, {safeAreaPadding.Top}, {safeAreaPadding.Right}, {safeAreaPadding.Bottom}}}");
            return safeAreaPadding;
        }
    }
}