namespace Nzl.Algorithm.Sort
{
    using System;

    /// <summary>
    /// Heap Sort class.
    /// </summary>
    internal static class HeapSort<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// The sort type.
        /// </summary>
        public static SortType SortType
        {
            get
            {
                return SortType.HeapSort;
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
            BuildMaxHeap(array, size);
            T tmp;
            int heapsize = size-1;
            for (int i = size-1; i > 0; i--)
            {
                tmp = array[0];
                array[0] = array[i];
                array[i] = tmp;

                MaxHeapify(array, heapsize--, 0);                
            }
        }

        /// <summary>
        /// Build max heap.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="?"></param>
        private static void BuildMaxHeap(T[] array, int size)
        {
            int heapsize = size;
            for (int i = size / 2; i >= 0; i--)
            {
                MaxHeapify(array, size, i);
            }
        }

        /// <summary>
        /// Max heapfiy method.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="i"></param>
        private static void MaxHeapify(T[] array, int heapsize, int i)
        {
            int l = Left(i);
            int r = Right(i);
            int p = Parent(i);
            int largestPos;

            if (l < heapsize && array[l].CompareTo(array[i]) > 0)
            {
                largestPos = l;
            }
            else
            {
                largestPos = i;
            }

            if (r < heapsize && array[r].CompareTo(array[largestPos]) > 0)
            {
                largestPos = r;
            }

            if (largestPos != i)
            {
                T tmp = array[i];
                array[i] = array[largestPos];
                array[largestPos] = tmp;

                MaxHeapify(array, heapsize, largestPos);
            }
        }

        /// <summary>
        /// Left child position.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private static int Left(int i)
        {
            return 2 * i + 1;
        }

        /// <summary>
        /// Right child position.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private static int Right(int i)
        {
            return 2 * i + 2;
        }

        /// <summary>
        /// Parent position.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private static int Parent(int i)
        {
            return (i - 1) / 2;
        }        
    }
}
