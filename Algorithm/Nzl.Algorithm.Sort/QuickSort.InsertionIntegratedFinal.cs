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
        /// Insertion integrated sort method internal.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="p"></param>
        /// <param name="r"></param>
        private static void InsertionIntegratedFinalInternal(T[] array, int p, int r)
        {
            if (p < r)
            {
                if (p != 0 && r != (array.Length - 1) && r - p + 1 < 20)
                {
                    return;
                }

                int pivot = DoubleIndexedPartition(array, p, r);
                InsertionIntegratedFinalInternal(array, p, pivot - 1);
                InsertionIntegratedFinalInternal(array, pivot + 1, r);

                if (p == 0 && r == (array.Length - 1))
                {
                    InsertionSort<T>.Sort(array, p, r);
                    return;
                }
            }
        }
    }
}
