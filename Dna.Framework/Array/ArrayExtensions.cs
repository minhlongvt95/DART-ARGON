using System.Collections.Generic;

namespace Dna
{
    /// <summary>
    /// Extensions method for arrays
    /// <summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Append the give objects to the original source array
        /// </summary>
        /// <typeparam name="T">type of array</typeparam>
        /// <param name="source">The orignal array of values</param>
        /// <param name="toAdd">The values to append to the source</param>
        /// <returns></returns>
        public static T[] Append<T>(this T[] source, params T[] toAdd)
        {
            // Create a list of the original items
            var list = new List<T>(source);

            // Append the new items
            list.AddRange(toAdd);

            return list.ToArray();
        }

        /// <summary>
        /// Prepend the give objects to the original source array
        /// </summary>
        /// <typeparam name="T">type of array</typeparam>
        /// <param name="source">The orignal array of values</param>
        /// <param name="toAdd">The values to prepend to the source</param>
        /// <returns></returns>
        public static T[] Prepend<T>(this T[] source, params T[] toAdd)
        {
            // Create a list of the original items
            var list = new List<T>(source);

            // Append the source items
            list.AddRange(toAdd);

            // Return the new array
            return list.ToArray();
        }
    }
}
// have fun :)
