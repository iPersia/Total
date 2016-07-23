namespace Nzl.Algorithm.Sort
{
    using System;

    /// <summary>
    /// QuickSort template calss.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    partial class QuickSort<T>
    {
        /// <summary>
        /// Standard sort method internal.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="p"></param>
        /// <param name="r"></param>
        private static void StandardInternal(T[] array, int p, int r)
        {
            if (p < r)
            {
                int pivot = StandardPartition(array, p, r);
                StandardInternal(array, p, pivot - 1);
                StandardInternal(array, pivot + 1, r);
            }
        }

        /// <summary>
        /// Standard sort method partition.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="p"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        private static int StandardPartition(T[] array, int p, int r)
        {
            T tmp;
            T pivot = array[p];
            int cursor = p + 1;
            int partitionpos = p;
            while (cursor <= r)
            {
                if (pivot.CompareTo(array[cursor]) > 0)
                {
                    partitionpos++;
                    tmp = array[cursor];
                    array[cursor] = array[partitionpos];
                    array[partitionpos] = tmp;
                }

                cursor++;
            }

            tmp = array[p];
            array[p] = array[partitionpos];
            array[partitionpos] = tmp;

            return partitionpos;
        }
    }
}
