using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CrossPlatformLibrary.Extensions
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        ///     Returns all elements of the given <seealso cref="source" /> distinct by <seealso cref="keySelector" />.
        /// </summary>
        /// <typeparam name="TSource">Type of the source sequence.</typeparam>
        /// <typeparam name="TKey">Type of the selected element.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <param name="keySelector">Key selector.</param>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.DistinctBy(keySelector, null);
        }

        /// <summary>
        ///     Returns all elements of the given <seealso cref="source" /> distinct by <seealso cref="keySelector" />.
        /// </summary>
        /// <typeparam name="TSource">Type of the source sequence.</typeparam>
        /// <typeparam name="TKey">Type of the selected element.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <param name="keySelector">Key selector.</param>
        /// <param name="comparer">
        ///     The equality comparer to use to determine whether or not keys are equal. If null, the default equality comparer for
        ///     <c>TSource</c> is used.
        /// </param>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            return DistinctByInternal(source, keySelector, comparer);
        }

        private static IEnumerable<TSource> DistinctByInternal<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            var knownKeys = new HashSet<TKey>(comparer);
            foreach (var element in source)
            {
                if (knownKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        ///     Returns all elements of the given <seealso cref="source" /> distinct by <seealso cref="keySelector" />.
        /// </summary>
        /// <typeparam name="TSource">Type of the source sequence.</typeparam>
        /// <typeparam name="TKey">Type of the selected element.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <param name="keySelector">Key selector.</param>
        public static IQueryable<TSource> DistinctBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IEqualityComparer<TKey> comparer = null)
        {
            return Queryable.Select(Queryable.GroupBy(source, keySelector, comparer), g => g.First());
        }
    }
}