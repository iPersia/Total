namespace Nzl.Algorithm.Sort
{
    using System;

    /// <summary>
    /// Bubble sort.
    /// </summary>
    internal static class BubbleSort<T> 
        where T : IComparable<T>
    {
        /// <summary>
        /// The sort type.
        /// </summary>
        public static SortType SortType
        {
            get
            {
                return SortType.BubbleSort;
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
            if (array == null)
            {
                return;
            }
            
            for (int i = size - 1; i > 0; i--)
            {
                int pos= i;
                int j=0;
                while (j<i)
                {
                    if (array[pos].CompareTo(array[j++]) < 0)
                    {                        
                        pos = j-1;
                    }
                }

                T tmp = array[i];
                array[i] = array[pos];
                array[pos] = tmp;
            }
        }        
    }
}
