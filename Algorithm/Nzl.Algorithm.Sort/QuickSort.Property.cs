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
        /// The quick sort type.
        /// </summary>
        private static int _insertionSize = 20;

        /// <summary>
        /// The sort type.
        /// </summary>
        public static SortType SortType
        {
            get
            {
                return SortType.QuickSort;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static int InsertionSize
        {
            get
            {
                return _insertionSize;
            }

            set
            {
                _insertionSize = value;
            }
        }
    }
}
