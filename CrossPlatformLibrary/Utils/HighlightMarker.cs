using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using CrossPlatformLibrary.Collection;
using CrossPlatformLibrary.Extensions;

namespace CrossPlatformLibrary.Utils
{
    [DebuggerDisplay("FromIndex = {FromIndex}, Length = {Length}, IsHighlighted = {IsHighlighted}")]
    public class HighlightIndex
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HighlightIndex" /> class.
        /// </summary>
        /// <param name="fromIndex">The from index.</param>
        /// <param name="length">The length.</param>
        /// <param name="isHighlighted">The is highlighted.</param>
        public HighlightIndex(int fromIndex, int length, bool isHighlighted)
        {
            this.FromIndex = fromIndex;
            this.Length = length;
            this.IsHighlighted = isHighlighted;
        }

        /// <summary>
        ///     Gets the from index.
        /// </summary>
        /// <value>From index.</value>
        public int FromIndex { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether is highlighted.
        /// </summary>
        /// <value><c>true</c> if this instance is highlighted; otherwise, <c>false</c>.</value>
        public bool IsHighlighted { get; private set; }

        /// <summary>
        ///     Gets the length.
        /// </summary>
        /// <value>The length.</value>
        public int Length { get; private set; }
    }

    public class HighlightMarker
    {
        private string searchText;

        public HighlightMarker(string fullText, string searchText)
        {
            this.Index = new List<Range>();

            this.FullText = fullText;
            this.SearchText = searchText;
        }
        public string FullText { get; private set; }

        public IList<Range> Index { get; private set; }

        public string SearchText
        {
            get
            {
                return this.searchText;
            }

            private set
            {
                if (value != this.searchText)
                {
                    this.CreateIndex(value);
                    this.searchText = value;
                }
            }
        }

        /// <summary>
        ///     Gets the enumerator.
        ///     The enumerator returns all HighlightIndex items which indicate at which position in string FullText
        ///     parts of the given SearchText were found and how many characters long they were.
        /// </summary>
        /// <returns>The IEnumerator&lt;HighlightIndex&gt;.</returns>
        public IEnumerator<HighlightIndex> GetEnumerator()
        {
            if (!this.Index.Any())
            {
                yield return new HighlightIndex(0, this.FullText.Length, false);
                yield break;
            }

            IEnumerable<int> realRange = new List<int>();
            foreach (Range i in this.Index)
            {
                realRange = realRange.Union(Enumerable.Range(i.LowerBound, i.UpperBound - i.LowerBound));
            }

            IEnumerable<IList<int>> consecutiveGroups = realRange.OrderBy(x => x).ToConsecutiveGroups();
            IList<Range> consecutiveRanges = new List<Range>();
            foreach (var group in consecutiveGroups)
            {
                consecutiveRanges.Add(new Range(group.Min(), group.Max()));
            }

            var lastItem = new Tuple<Range, bool>(new Range(0, 0), false);
            foreach (Range currentItem in consecutiveRanges.OrderBy(x => x.LowerBound).ThenBy(y => y.UpperBound))
            {
                if (currentItem.LowerBound > lastItem.Item1.UpperBound)
                {
                    yield return new HighlightIndex(lastItem.Item1.UpperBound, currentItem.LowerBound - lastItem.Item1.UpperBound, false);
                }

                yield return new HighlightIndex(currentItem.LowerBound, currentItem.UpperBound - currentItem.LowerBound + 1, true);
                lastItem = new Tuple<Range, bool>(new Range(currentItem.LowerBound + 1, currentItem.UpperBound + 1), true);
            }

            if (this.Index.Max(x => x.UpperBound) < this.FullText.Length)
            {
                yield return new HighlightIndex(lastItem.Item1.UpperBound, this.FullText.Length - lastItem.Item1.UpperBound, false);
            }
        }

        private void CreateIndex(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            string[] searchStrings = text.Trim().Split(new[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries);

            this.Index.Clear();

            var fullTextList = new List<string> { this.FullText };
            if (this.FullText.HasDiacritics())
            {
                fullTextList.Add(this.FullText.RemoveDiacritics());
            }

            foreach (string searchString in searchStrings)
            {
                int length = searchString.Length;

                foreach (var fulltext in fullTextList)
                {
                    int searchStringIndex = fulltext.IndexOf(searchString, 0, StringComparison.CurrentCultureIgnoreCase);

                    while (searchStringIndex >= 0)
                    {
                        this.Index.Add(new Range(searchStringIndex, searchStringIndex + length));

                        int lastIndex = searchStringIndex + length;
                        searchStringIndex = fulltext.IndexOf(searchString, lastIndex, StringComparison.CurrentCultureIgnoreCase);
                    }
                }
            }
        }
    }
}