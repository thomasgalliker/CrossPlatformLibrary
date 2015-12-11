using Guards;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CrossPlatformLibrary.Extensions
{
    public static class EnumerableExtensions
    {
        public static void Sort<TSource, TKey>(this ICollection<TSource> source, Func<TSource, TKey> keySelector)
        {
            Guard.ArgumentNotNull(() => source);
            Guard.ArgumentNotNull(() => keySelector);

            IList<TSource> sortedList = source.OrderBy(keySelector).ToList();
            source.Clear();
            foreach (var sortedItem in sortedList)
            {
                source.Add(sortedItem);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            Guard.ArgumentNotNull(() => source);
            Guard.ArgumentNotNull(() => action);

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
        /// Updates all items in the specified source which match with selectorFunc with the specified updateAction.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="selectorFunc">The selector function.</param>
        /// <param name="updateAction">The update action.</param>
        public static void Update<T>(this IEnumerable<T> source, Func<T, bool> selectorFunc, Action<T> updateAction)
        {
            Guard.ArgumentNotNull(() => source);
            Guard.ArgumentNotNull(() => selectorFunc);
            Guard.ArgumentNotNull(() => updateAction);

            foreach (var item in source.Where(selectorFunc))
            {
                updateAction(item);
            }
        }

        /// <summary>
        /// Updates a single item in the given source using the selector function to find the item
        /// and the update action to update it accordingly.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="selectorFunc">The selector function.</param>
        /// <param name="updateAction">The update action.</param>
        public static void UpdateSingle<T>(this IEnumerable<T> source, Func<T, bool> selectorFunc, Action<T> updateAction)
        {
            Guard.ArgumentNotNull(() => source);
            Guard.ArgumentNotNull(() => selectorFunc);
            Guard.ArgumentNotNull(() => updateAction);

            var selected = source.Single(selectorFunc);
            updateAction(selected);
        }

        /// <summary>
        /// Determines whether the specified search list contains duplicates.
        /// </summary>
        /// <typeparam name="T">The generic type T.</typeparam>
        /// <typeparam name="TResult">The type of the T result.</typeparam>
        /// <param name="searchList">The search list.</param>
        /// <param name="selectionCriteria">The selection criteria.</param>
        /// <returns><c>true</c> if the specified search list contains duplicates; otherwise, <c>false</c>.</returns>
        public static bool AnyDuplicates<T, TResult>(this IEnumerable<T> searchList, Func<T, TResult> selectionCriteria)
        {
            Guard.ArgumentNotNull(() => searchList);

            return searchList.Select(selectionCriteria)
                    .GroupBy(x => x)
                    .Where(y => y.Count() > 1)
                    .Select(z => z.Key)
                    .Any();
        }

        /// <summary>
        /// Returns the last object of source enumerable.
        /// </summary>
        /// <exception cref="ArgumentNullException">The source enumerable is null.</exception>
        /// <exception cref="System.InvalidOperationException">The source enumerable does not contain any elements.</exception>
        public static object Last(this IEnumerable source)
        {
            Guard.ArgumentNotNull(() => source);

            var lastOrDefault = source.LastOrDefault();
            if (lastOrDefault != null)
            {
                return lastOrDefault;
            }

            throw new InvalidOperationException("The source enumerable does not contain any elements.");
        }

        /// <summary>
        /// Returns the last object of source enumerable.
        /// If there are no items in source enumerable, it returns null.
        /// </summary>
        /// <exception cref="ArgumentNullException">The source enumerable is null.</exception>
        /// <exception cref="System.InvalidOperationException">The source enumerable does not contain any elements.</exception>
        public static object LastOrDefault(this IEnumerable source)
        {
            Guard.ArgumentNotNull(() => source);

            var list = source as IList;
            if (list != null)
            {
                int count = list.Count;
                if (count > 0)
                {
                    return list[count - 1];
                }
            }
            else
            {
                var e = source.GetEnumerator();
                using (e as IDisposable)
                {
                    if (e.MoveNext())
                    {
                        object result;
                        do
                        {
                            result = e.Current;
                        } while (e.MoveNext());
                        return result;
                    }
                }
            }
            return null;
        }
    }
}