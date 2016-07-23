using System;
using System.Collections.Generic;
using System.Text;

namespace Nzl.Test.Bag
{
    /// <summary>
    /// The bag problem util class.
    /// </summary>
    public static class BagUtil
    {
        /// <summary>
        /// To run.
        /// </summary>
        public static void Run(int[] weights, int[] values, int bagCapacity)
        {
            ///Basic constraints.
            if (weights != null && weights.Length > 0 
                && values != null && values.Length > 0 
                && weights.Length == values.Length
                && bagCapacity > 0)
            {
                int[,] f = new int[weights.Length,bagCapacity];
                for (int i = 0; i < bagCapacity; i++)
                {
                    for (int j = 0; j < weights.Length; j++)
                    {

                    }
                }
            }
        }
    }
}
