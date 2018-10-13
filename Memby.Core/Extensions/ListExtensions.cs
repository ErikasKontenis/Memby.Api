using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Memby.Core.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Determines whether the collection is null or contains no elements.
        /// </summary>
        /// <typeparam name="T">The IEnumerable type.</typeparam>
        /// <param name="enumerable">The enumerable, which may be null or empty.</param>
        /// <returns>
        ///     true if the IEnumerable is null or empty; otherwise, false.
        /// </returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                return true;

            return !enumerable.Any();
        }

        public static bool IsEnumerable<T>(this T source)
        {
            return typeof(IEnumerable).IsAssignableFrom(source.GetType());
        }
    }
}
