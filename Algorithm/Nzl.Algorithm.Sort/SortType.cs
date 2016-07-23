namespace Nzl.Algorithm.Sort
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Sort type enum.
    /// </summary>
    public enum SortType
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("Algorithm - Sort - Insertion Sort")]
        InsertionSort = 0,

        /// <summary>
        /// 
        /// </summary>
        [Description("Algorithm - Sort - Bubble Sort")]
        BubbleSort = 1,

        /// <summary>
        /// 
        /// </summary>
        [Description("Algorithm - Sort - Quick Sort")]
        QuickSort = 2,

        /// <summary>
        /// 
        /// </summary>
        [Description("Algorithm - Sort - Merge Sort")]
        MergeSort = 3,

        /// <summary>
        /// 
        /// </summary>
        [Description("Algorithm - Sort - Heap Sort")]
        HeapSort = 4
    }
}
