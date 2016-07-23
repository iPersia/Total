namespace Nzl.Test.Algorithm
{
    using System;
    using Nzl.Core;
    using Nzl.Core.Interface;
    using Nzl.DataStructure;
    using Nzl.DataStructure.Basic;

    public class Test_Combination : ITest
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
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int count = 2;
            System.Console.WriteLine(chars + "\t" + count + "/" + chars.Length);
            System.Console.WriteLine("The total count is " + GetCombinationCount(chars.Length, count));
            _Counter = 0;
            GetCombinations(chars, "", count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        private int GetCombinationCount(int n, int m)
        {
            if (m > 0 && n > 0 && m <= n)
            {
                int count = 1;
                for (int i = n; i > n - m; i--)
                    count *= i;
                for (int i = 1; i <= m; i++)
                    count /= i;

                return count;
            }

            return 0;
        }

        private void GetCombinations(string chars, string prefix, int count)
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

                prefix = prefix + chars.Substring(0, 1);
                GetCombinations(chars.Substring(1), prefix, count - 1);
                GetCombinations(chars.Substring(1), prefix.Substring(0, prefix.Length - 1), count);
                
            }
        }
    }
}
