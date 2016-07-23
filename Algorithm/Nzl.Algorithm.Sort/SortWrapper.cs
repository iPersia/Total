namespace Nzl.Algorithm.Sort
{
    using System;

    /// <summary>
    /// The wrapper of sort methods.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class SortWrapper<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Sort the array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="type">The sort type.</param>
        public static void Sort(T[] array, SortType type)
        {
            if (array != null)
            {
                switch (type)
                {
                    case SortType.BubbleSort:
                        BubbleSort<T>.Sort(array);
                        break;
                    case SortType.HeapSort:
                        HeapSort<T>.Sort(array);
                        break;
                    case SortType.InsertionSort:
                        InsertionSort<T>.Sort(array);
                        break;
                    case SortType.MergeSort:
                        MergeSort<T>.Sort(array);
                        break;
                    case SortType.QuickSort:
                        QuickSort<T>.Sort(array);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Sort the array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="type">The quick sort type.</param>
        public static void Sort(T[] array, QuickSortType qSortType)
        {
            if (array != null)
            {
                QuickSort<T>.Sort(array, qSortType);
            }
        }
    }
}
