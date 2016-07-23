using System;
using Nzl.Algorithm;
using Nzl.Algorithm.Sort;
using Nzl.Core;
using Nzl.Core.Interface;

namespace Nzl.Test.Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            ITest test = null;
            int flag = 9;
            switch (flag)
            {
                case 1:
                    test = new Test_Sort();
                    break;
                case 2:
                    test = new Test_BinarySearchTree();
                    break;
                case 3:
                    test = new Test_RedBlackTree();
                    break;
                case 4:
                    test = new Test_Basic();
                    break;
                case 5:
                    test = new Test_OrderStatistics();
                    break;
                case 6:
                    test = new Test_DynamicProgramming();
                    break;
                case 7:
                    test = new Test_Combination();
                    break;
                case 8:
                    test = new Test_Permutation();
                    break;
                case 9:
                    test = new Test_DualSum();
                    break;                    
                default:
                    break;
            }

            test.Test();
            System.Console.ReadLine();
        }
    }
}
