using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CrossPlatformLibrary.Collection.Generic
{
    public static class EnumerableExtensions
    {
        public static void Sort<TSource, TKey>(this ICollection<TSource> source, Func<TSource, TKey> keySelector)
        {
            IList<TSource> sortedList = source.OrderBy(keySelector).ToList();
            source.Clear();
            foreach (var sortedItem in sortedList)
            {
                source.Add(sortedItem);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            foreach (T item in source)
            {
                action(item);
            }
        }

        /// <summary>
        ///     To the observable collection.
        /// </summary>
        /// <typeparam name="T">Generic type T.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns>The resulting ObservableCollection&lt;T&gt;.</returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
        {
            return new ObservableCollection<T>(enumerable);
        }

        /// <summary>
        ///     Transforms an IEnumerable into groups of integer lists which are consecutivly ascending.
        ///     Please make sure that it - depending on the application of this method - is neccessary to pre-sort the IEnumerable
        ///     in order to retrieve the right groups.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The groups of consecutive integer lists.</returns>
        public static IEnumerable<List<int>> ToConsecutiveGroups(this IEnumerable<int> source)
        {
            using (var iterator = source.GetEnumerator())
            {
                if (!iterator.MoveNext())
                {
                    yield break;
                }
                else
                {
                    int current = iterator.Current;
                    var group = new List<int> { current };

                    while (iterator.MoveNext())
                    {
                        int next = iterator.Current;
                        if (next < current || current + 1 < next)
                        {
                            yield return group;
                            group = new List<int>();
                        }

                        current = next;
                        group.Add(current);
                    }

                    if (group.Any())
                    {
                        yield return group;
                    }
                }
            }
        }
    }
}