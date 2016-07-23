namespace Nzl.Algorithm.Sort
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Quick sort type enum.
    /// </summary>
    public enum QuickSortType
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("Algorithm - Sort - Quick Sort - Standard")]
        Standard = 0,

        /// <summary>
        /// 
        /// </summary>
        [Description("Algorithm - Sort - Quick Sort - Randomized")]
        Randomized = 1,

        /// <summary>
        /// C.A.R.Hoare
        /// </summary>
        [Description("Algorithm - Sort - Quick Sort - Double Indexed")]
        DoubleIndexed = 2,

        /// <summary>
        /// 
        /// </summary>
        [Description("Algorithm - Sort - Quick Sort - Parts Insertion")]
        PartsInsertion = 3,

        /// <summary>
        /// 
        /// </summary>
        [Description("Algorithm - Sort - Quick Sort - Final Insertion")]
        FinalInsertion = 4,

        /// <summary>
        /// 
        /// </summary>
        [Description("Algorithm - Sort - Quick Sort - Medianing")]
        Medianing = 5,

        /// <summary>
        /// 
        /// </summary>
        [Description("Algorithm - Sort - Quick Sort - Split End")]
        SplitEnd = 6
    }
}
