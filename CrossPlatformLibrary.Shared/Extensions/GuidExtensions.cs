using System;

namespace CrossPlatformLibrary.Extensions
{
    public static class GuidExtensions
    {
        private static readonly int[] GuidByteOrder = { 15, 14, 13, 12, 11, 10, 9, 8, 6, 7, 4, 5, 0, 1, 2, 3 };

        /// <summary>
        ///     Increments the given <paramref name="guid" /> to by one.
        ///     Original source: http://stackoverflow.com/questions/30404965/increment-guid-in-c-sharp#30405028
        /// </summary>
        /// <example>
        ///     Guid.Increment can be helpful for generating human-readable Guids like "00000000-0000-0000-0000-000000000001"
        ///     which can be incremented to "00000000-0000-0000-0000-000000000002".
        /// </example>
        /// <param name="guid">The input Guid.</param>
        /// <returns>The incremented output Guid.</returns>
        public static Guid Increment(this Guid guid)
        {
            var bytes = guid.ToByteArray();
            var carry = true;
            for (var i = 0; i < GuidByteOrder.Length && carry; i++)
            {
                var index = GuidByteOrder[i];
                var oldValue = bytes[index]++;
                carry = oldValue > bytes[index];
            }

            return new Guid(bytes);
        }
    }
}