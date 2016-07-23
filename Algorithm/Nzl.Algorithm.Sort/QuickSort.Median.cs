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
        /// Media sort method internal.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="p"></param>
        /// <param name="r"></param>
        private static void MedianInternal(T[] array, int p, int r)
        {
            if (p < r)
            {
                if (p != 0 && r != (array.Length - 1) && r - p + 1 < 20)
                {
                    InsertionSort<T>.Sort(array, p, r);
                    return;
                }

                int pivot = MedianPartition(array, p, r);                     
                MedianInternal(array, p, pivot - 1);
                MedianInternal(array, pivot + 1, r);
            }
        }
        
        /// <summary>
        /// DoubleIndexed sort method partition.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="p"></param>
        /// <param name="r"></param>
        private static int MedianPartition(T[] array, int p, int r)
        {
            int len = r - p + 1;
            int m = p + (len >> 1);
            if (len > 7)
            {
                int l = p;
                int n = p + len - 1;
                if (len > 40)
                { 
                    int s = len / 8;
                    l = Median3(array, l, l + s, l + 2 * s);    // 取样左端点3个数并得出中数
                    m = Median3(array, m - s, m, m + s);        // 取样中点3个数并得出中数
                    n = Median3(array, n - 2 * s, n - s, n);    // 取样右端点3个数并得出中数
                }

                m = Median3(array, l, m, n);
            }
            T tmp = array[p];
            array[p] = array[m];
            array[m] = tmp;

            m = p;
            int j = r + 1;
            T x = array[p];
            while (true)
            {
                do
                {
                    m++;
                } while (m <= r && array[m].CompareTo(x) < 0);

                do
                {
                    j--;
                } while (array[j].CompareTo(x) > 0);

                if (j < m)
                {
                    break;
                }

                tmp = array[m];
                array[m] = array[j];
                array[j] = tmp;
            }

            tmp = array[p];
            array[p] = array[j];
            array[j] = tmp;
            return j;
        }
    }
}
