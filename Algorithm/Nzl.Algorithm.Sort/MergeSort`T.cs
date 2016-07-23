namespace Nzl.Algorithm.Sort
{
    using System;

    /// <summary>
    /// Merge sort class.
    /// </summary>
    internal static class MergeSort<T>
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
            MergeSort<T>.MergeSortInternal(array, 0, size - 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <param name="r"></param>
        private static void MergeSortInternal(T[] array, int p, int r)
        {
            if (p < r)
            {                
                MergeSortInternal(array, p, (r + p) / 2);
                MergeSortInternal(array, (r + p) / 2 + 1, r);
                Merge(array, p, (p + r) / 2, r);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <param name="r"></param>
        private static void Merge(T[] array, int p, int q, int r)
        {
            T[] tmpArray = new T[r - p + 1];
            for (int i = 0; i < r - p + 1; i++)
            {
                tmpArray[i] = array[p + i];
            }

            int f = p;
            int s = (p + r) / 2 + 1;
            int d = 0;
            while (f <= (p + r) / 2 && s <= r)
            {
                if (array[f].CompareTo(array[s]) < 0)
                {
                    tmpArray[d++] = array[f++];
                }
                else
                {
                    tmpArray[d++] = array[s++];
                }
            }

            while (f <= (p + r) / 2)
                tmpArray[d++] = array[f++];

            while (s <= r)
                tmpArray[d++] = array[s++];

            for (int i = 0; i < r - p + 1; i++)
            {
                array[p + i] = tmpArray[i];
            }
        }
    }
}
