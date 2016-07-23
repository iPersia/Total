namespace Nzl.Algorithm.Sort
{
    using System;

    /// <summary>
    /// QuickSort template calss.
    /// </summary>
    internal static partial class QuickSort<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Sort the array.
        /// </summary>
        /// <param name="array">The array.</param>
        public static void Sort(T[] array)
        {
            if (array != null)
            {
                Sort(array, array.Length, QuickSortType.SplitEnd);
            }
        }

        /// <summary>
        /// Sort the array.
        /// </summary>
        /// <param name="array">The array.</param>
        public static void Sort(T[] array, QuickSortType qSortType)
        {
            if (array != null)
            {
                Sort(array, array.Length, qSortType);
            }
        }

        /// <summary>
        /// Sort method.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="size"></param>
        public static void Sort(T[] array, int size, QuickSortType qSortType)
        {
            if (array == null)
            {
                return;
            }

            switch (qSortType)
            {
                case QuickSortType.Standard:
                    {
                        StandardInternal(array, 0, size - 1);
                    }
                    break;
                case QuickSortType.Randomized:
                    {
                        RandomizedInternal(array, 0, size - 1);
                    }
                    break;
                case QuickSortType.DoubleIndexed:
                    {
                        DoubleIndexedInternal(array, 0, size - 1);
                    }
                    break;
                case QuickSortType.PartsInsertion:
                    {
                        InsertionIntegratedPartsInternal(array, 0, size - 1);
                    }
                    break;
                case QuickSortType.FinalInsertion:
                    {
                        InsertionIntegratedFinalInternal(array, 0, size - 1);
                    }
                    break;
                case QuickSortType.Medianing:
                    {
                        MedianInternal(array, 0, size - 1);
                    }
                    break;
                case QuickSortType.SplitEnd:
                    {
                        SplitEndInternal(array, 0, size - 1);
                    }
                    break;
                default:
                    {
                        SplitEndInternal(array, 0, size - 1);
                    }
                    break;
            }
        }
    }
}
