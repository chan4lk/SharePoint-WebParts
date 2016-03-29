using System;
using System.Collections.Generic;

namespace SuperMarketSystem.Common
{
    /// <summary>
    /// Extension methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Determines whether the specified list is empty.
        /// </summary>
        /// <typeparam name="T">
        /// The list Type.
        /// </typeparam>
        /// <param name="list">The list.</param>
        /// <returns>
        /// <c>true</c> if empty.
        /// </returns>
        public static bool IsEmpty<T>(this List<T> list)
        {
            if (list == null)
            {
                throw new ArgumentException(SupermarketResources.ArgumentNullError);               
            }

            return list.Count == 0;
        }

        /// <summary>
        /// Determines whether [is not empty] [the specified list].
        /// </summary>
        /// <typeparam name="T">
        /// The list Type.
        /// </typeparam>
        /// <param name="list">The list.</param>
        /// <returns>
        /// <c>true</c> if not empty.
        /// </returns>
        public static bool IsNotEmpty<T>(this List<T> list)
        {
            return !IsEmpty(list);
        }
    }
}
