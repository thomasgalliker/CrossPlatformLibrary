


using Android.Graphics;
using Android.Text;
using Android.Text.Style;
using Android.Widget;

using CrossPlatformLibrary.System.Text;

namespace CrossPlatformLibrary.Droid.Extensions
{
    public static class TextViewExtensions
    {
        public static void HighlightText(this TextView textView, string searchText, Color foregroundColor, Color? backgroundColor = null)
        {
            var highlightMarker = new HighlightMarker(textView.Text, searchText);
            using (var enumerator = highlightMarker.GetEnumerator())
            {
                var spannableStringBuilder = new SpannableStringBuilder(textView.Text);

                while (enumerator.MoveNext())
                {
                    int fromIndex = enumerator.Current.FromIndex;
                    int endIndex = fromIndex + enumerator.Current.Length;
                    bool isHighlighted = enumerator.Current.IsHighlighted;

                    if (isHighlighted)
                    {
                        spannableStringBuilder.SetSpan(new ForegroundColorSpan(foregroundColor), fromIndex, endIndex, SpanTypes.ExclusiveExclusive);
                        
                        if (backgroundColor.HasValue)
                        {
                            spannableStringBuilder.SetSpan(new BackgroundColorSpan(backgroundColor.Value), fromIndex, endIndex, SpanTypes.ExclusiveExclusive);
                        }
                    }
                }

                textView.TextFormatted = spannableStringBuilder;
            }
        }
    }
}