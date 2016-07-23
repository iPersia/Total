namespace Nzl.Test.Algorithm
{
    using System;
    using Nzl.Core;
    using Nzl.Core.Interface;
    using Nzl.DataStructure;
    using Nzl.DataStructure.Basic;

    /// <summary>
    /// Give an array which is sorted.
    /// Please find the dual elem whose sum is given number.
    /// </summary>
    public class Test_DualSum : ITest
    {
        /// <summary>
        /// Implements the ITest.
        /// </summary>
        public void Test()
        {
            System.Console.WriteLine("Test Dual Sum");

            int[] arr = new int[] { 1, 2, 4, 6, 9, 11, 14, 15 };
            int sum = 15;
            GetDual(arr, sum);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="sum"></param>
        private void GetDual(int[] arr, int sum)
        {
            if (arr != null && arr.Length > 1)
            {
                int i = 0;
                int j = arr.Length - 1;
                
                while(i < j)
                {
                    if (arr[i] + arr[j] == sum)
                    {
                        System.Console.WriteLine("The dual can be " + arr[i++] + " + " + arr[j--]);
                    }
                    else if (arr[i] + arr[j] < sum)
                    {
                        i++;
                    }
                    else
                    {
                        j--;
                    }
                }
            }
        }
    }
}
