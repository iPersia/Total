namespace Nzl.Algorithms.Sort
{
    using System;

    /// <summary>
    /// QuickSort template calss.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    partial class QuickSort<T>
    {
        /// <summary>
        /// Insertion integrated sort method internal.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="p"></param>
        /// <param name="r"></param>
        private static void InsertionIntegratedPartsInternal(T[] array, int p, int r)
        {
            if (p < r)
            {
                if (p != 0 && r != (array.Length - 1) && r - p + 1 < 20)
                {
                    InsertionSort<T>.Sort(array, p, r);
                    return;
                }

                int pivot = DoubleIndexedPartition(array, p, r);
                InsertionIntegratedPartsInternal(array, p, pivot - 1);
                InsertionIntegratedPartsInternal(array, pivot + 1, r);     
            }
        }
    }
}
