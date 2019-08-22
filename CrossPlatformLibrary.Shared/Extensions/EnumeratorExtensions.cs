using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CrossPlatformLibrary.Extensions
{
    public static class EnumeratorExtensions
    {
        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator enumerator)
        {
            while (enumerator?.MoveNext() == true)
            {
                yield return (T)enumerator.Current;
            }
        }

        public static IEnumerable<T> ToList<T>(this IEnumerator iterator)
        {
            return iterator.ToEnumerable<T>().ToList();
        }
    }
}
