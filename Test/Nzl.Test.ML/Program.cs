using System;
using Nzl.Core;
using Nzl.Core.Interface;

namespace Nzl.Test.ML
{
    class Program
    {
        static void Main(string[] args)
        {
            ITest test = null;
            int flag = 1;
            switch (flag)
            {
                case 1:
                    test = new Test_KMeans();
                    break;
                default:
                    break;
            }

            test.Test();
            System.Console.ReadLine();
        }
    }
}
