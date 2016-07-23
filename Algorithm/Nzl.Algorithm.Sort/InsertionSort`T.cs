namespace Nzl.Algorithm.Sort
{
    using System;
    using Nzl.Algorithm;

    /// <summary>
    /// Insertion sort static class.
    /// </summary>
    internal static class InsertionSort<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// The sort type.
        /// </summary>
        public static SortType SortType
        {
            get
            {
                return SortType.InsertionSort;
            }
        }

        /// <summary>
        /// Sort the array.
        /// </summary>
        /// <param name="array">The array.</param>
        public static void Sort(T[] array)
        {
            if (array != null)
            {
                Sort(array, array.Length);
            }
        }

        /// <summary>
        /// Sort method.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="size"></param>
        public static void Sort(T[] array, int size)
        {
            Sort(array, 0, size - 1);
        }

        /// <summary>
        /// Sort method.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="size"></param>
        public static void Sort(T[] array, int p, int q)
        {
            if (array == null)
            {
                return;
            }


            for (int i = p; i <= q; i++)
            {
                int j = i;
                T cur = array[i];
                while (j > p && cur.CompareTo(array[j - 1]) < 0)
                {
                    array[j] = array[j - 1];
                    j--;
                }

                array[j] = cur;
            }
        }
    }
}
