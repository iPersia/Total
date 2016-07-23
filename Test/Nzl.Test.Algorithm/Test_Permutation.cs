namespace Nzl.Test.Algorithm
{
    using System;
    using Nzl.Core;
    using Nzl.Core.Interface;
    using Nzl.DataStructure;
    using Nzl.DataStructure.Basic;

    public class Test_Permutation : ITest
    {
        /// <summary>
        /// 
        /// </summary>
        private int _Counter = 0;

        /// <summary>
        /// Implements the ITest.
        /// </summary>
        public void Test()
        {
            System.Console.WriteLine("Test Permutation");

            string chars = "1234567890";
            int count = 3;
            System.Console.WriteLine(chars + "\t" + count + "/" + chars.Length);
            System.Console.WriteLine("The total count is " + GetPermutationCount(chars.Length, count));
            _Counter = 0;
            GetPermutation(chars, "", count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        private int GetPermutationCount(int n, int m)
        {
            if (m > 0 && n > 0 && m <= n)
            {
                int count = 1;
                for (int i = n; i > n - m; i--)
                    count *= i;

                return count;
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chars"></param>
        /// <param name="prefix"></param>
        /// <param name="count"></param>
        private void GetPermutation(string chars, string prefix, int count)
        {
            if (chars.Length > 0 && chars.Length >= count)
            {
                if (count == 1)
                {
                    foreach (char c in chars.ToCharArray())
                    {
                        System.Console.WriteLine((++_Counter).ToString("0000") + "\t" + prefix + c);
                    }

                    return;
                }

                for (int i = 0; i < chars.Length; i++)
                {
                    GetPermutation(chars.Substring(0, i) + chars.Substring(i + 1), prefix + chars[i], count - 1);
                }
            }
        }
    }
}
