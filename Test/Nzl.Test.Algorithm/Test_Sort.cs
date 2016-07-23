namespace Nzl.Test.Algorithm
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using Nzl.Algorithm;
    using Nzl.Algorithm.Sort;
    using Nzl.Core;
    using Nzl.Core.Interface;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Test_Sort : ITest
    {
        public void Test()
        {
            TestAllSortMethod();
        }

        /// <summary>
        /// 
        /// </summary>
        private int size = 10000;

        /// <summary>
        /// 
        /// </summary>
        private int maxReps = 5;


        /// <summary>
        /// 
        /// </summary>
        private void TestAllSortMethod()
        {
            IList<string[]> enumInfor = Util.GetEnumInfor(SortType.QuickSort.GetType());
            foreach(string[] infors in enumInfor)
            {
                TestAllConditions((SortType)(System.Convert.ToInt32(infors[1])));
            }

            TestAllQuickSort();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sort"></param>
        private void TestAllConditions(SortType type)
        {
            System.Console.WriteLine(Util.GetEnumDescription(type));
            System.Console.Write("\t Randomized array");
            TestMethod(type, Util.GetRandomizedArray(size), size);
            System.Console.Write("\t Ascending array");
            TestMethod(type, Util.GetAscArray(size), size);
            System.Console.Write("\t Desending array");
            TestMethod(type, Util.GetDscArray(size), size);
            System.Console.Write("\t Repeating array");
            TestMethod(type, Util.GetRepArray(size, maxReps), size);
        }

        private void TestAllQuickSort()
        {
            System.Console.WriteLine("Randomized array");
            TestQuickSort(Util.GetRandomizedArray(size), size);
            System.Console.WriteLine("Ascending array");
            TestQuickSort(Util.GetAscArray(size), size);
            System.Console.WriteLine("Desending array");
            TestQuickSort(Util.GetDscArray(size), size);
            System.Console.WriteLine("Repeating array");
            TestQuickSort(Util.GetRepArray(size, maxReps), size);
        }

        private void TestQuickSort(int[] array, int size)
        {
            int qSortTypeCount = typeof(QuickSortType).GetFields().Length - 1;
            int[] tempArray = new int[size];
            for (int i = 0; i < qSortTypeCount; i++)
            {
                System.Console.Write("\t" + Util.GetEnumDescription((QuickSortType)i));
                array.CopyTo(tempArray, 0);

                System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
                timer.Start();
                SortWrapper<int>.Sort(array, (QuickSortType)i);
                timer.Stop();
                Console.Write("\t\tExecution time : {0, 10:F1} microseconds.", timer.Elapsed.Ticks / 10m);

                if (CheckOrder(array, size) == false)
                {
                    System.Console.Write("\tOrder is BAD!");
                }
                else
                {
                    System.Console.WriteLine("\tOrder is GOOD!");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="del"></param>
        /// <param name="array"></param>
        /// <param name="size"></param>
        private void TestMethod(SortType type, int[] array, int size)
        {   
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();
            SortWrapper<int>.Sort(array, type);
            timer.Stop();
            Console.Write("\tExecution time : {0, 10:F1} microseconds.", timer.Elapsed.Ticks / 10m);

            if (CheckOrder(array, size) == false)
            {
                System.Console.WriteLine("\tOrder is BAD!");
            }
            else
            {
                System.Console.WriteLine("\tOrder is GOOD!");
            }
        }

        /// <summary>
        /// Check whether an array is already ordered.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private bool CheckOrder(int[] array, int size)
        {
            for (int i = 0; i < size - 1; i++)
            {
                if (array[i] > array[i + 1])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
