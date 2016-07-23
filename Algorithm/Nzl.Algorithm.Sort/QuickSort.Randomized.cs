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
        /// Randomized sort method internal.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="p"></param>
        /// <param name="r"></param>
        private static void RandomizedInternal(T[] array, int p, int r)
        {
            if (p < r)
            {
                int pivot = RandomizedPartition(array, p, r);                 
                RandomizedInternal(array, p, pivot - 1);
                RandomizedInternal(array, pivot + 1, r);
            }
        }
        
        /// <summary>
        /// Randomized sort method partition.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="p"></param>
        /// <param name="r"></param>
        private static int RandomizedPartition(T[] array, int p, int r)
        {
            int i = p + (new Random()).Next(r - p + 1);
            T tmp = array[p];
            array[p] = array[i];
            array[i] = tmp;
            T x = array[p];
            int j = p;
            for (i = p + 1; i <= r; i++)
            {
                if (array[i].CompareTo(x) < 0)
                {
                    tmp = array[++j];
                    array[j] = array[i];
                    array[i] = tmp;
                }
            }
         
            tmp = array[p];
            array[p] = array[j];
            array[j] = tmp;
            return j;
        }
    }
}
