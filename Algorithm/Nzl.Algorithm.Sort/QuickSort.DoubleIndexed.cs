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
        /// DoubleIndexed sort method internal.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="p"></param>
        /// <param name="r"></param>
        private static void DoubleIndexedInternal(T[] array, int p, int r)
        {
            if (p < r)
            {
                int pivot = DoubleIndexedPartition(array, p, r);
                DoubleIndexedInternal(array, p, pivot - 1);
                DoubleIndexedInternal(array, pivot + 1, r);
            }
        }
        
        /// <summary>
        /// DoubleIndexed sort method partiotion.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="p"></param>
        /// <param name="r"></param>
        private static int DoubleIndexedPartition(T[] array, int p, int r)
        {
            int i = p + (new Random()).Next(r - p + 1);
            T tmp = array[p];
            array[p] = array[i];
            array[i] = tmp;
            i = p;
            int j = r + 1;
            T x = array[p];
            while (true)
            {
                do
                {
                    i++;
                } while (i <= r && array[i].CompareTo(x) < 0);

                do
                {
                    j--;
                } while (array[j].CompareTo(x) > 0);

                if (j < i)
                {
                    break;
                }

                tmp = array[j];
                array[j] = array[i];
                array[i] = tmp;
            }

            tmp = array[p];
            array[p] = array[j];
            array[j] = tmp;
            return j;
        }
    }
}
