using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nzl.Algorithm.Sort
{
    partial class QuickSort<T>
    {
        /// <summary>
        /// QuickSort template calss.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="p"></param>
        /// <param name="r"></param>
        private static void Medianing(T[] array, int p, int r)
        {
            // 计算数组长度
            int len = r - p + 1;
            // 求出中点，大小等于7的数组选择pivot
            int m = p + (len >> 1);
            // 大小大于7
            if (len > 7)
            {
                int l = p;
                int n = p + len - 1;
                if (len > 40)
                { // 大数组，采用median-of-nine选择
                    int s = len / 8;
                    l = Median3(array, l, l + s, l + 2 * s);   // 取样左端点3个数并得出中数
                    m = Median3(array, m - s, m, m + s);       // 取样中点3个数并得出中数
                    n = Median3(array, n - 2 * s, n - s, n);   // 取样右端点3个数并得出中数
                }
                m = Median3(array, l, m, n); // 取中数中的中数
            }

            //swap(a, p, m);
            //Util.Swap<T>(array, p, m);
            T tmp = array[p];
            array[p] = array[m];
            array[m] = tmp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        private static int Median3(T[] x, int a, int b, int c)
        {
            return x[a].CompareTo(x[b]) < 0 ? (x[b].CompareTo(x[c]) < 0 ? b : x[a].CompareTo(x[c]) < 0 ? c : a)
                : x[b].CompareTo(x[c]) > 0 ? b : x[a].CompareTo(x[c]) > 0 ? c : a;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="p"></param>
        /// <param name="r"></param>
        private static void Randomizing(T[] array, int p, int r)
        {
            Util.Swap<T>(array, p, p + (new Random()).Next(r - p + 1));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="p"></param>
        /// <param name="r"></param>
        private static void DoubleIndexing(T[] array, int p, int r)
        {
            int i = p;
            int j = r + 1;
            T x = array[p];
            T tmp;
            while (true)
            {
                // 查找比x大于等于的位置
                do
                {
                    i++;
                }
                while (i <= r && array[i].CompareTo(x) < 0);

                // 查找比x小于等于的位置
                do
                {
                    j--;
                }
                while (array[j].CompareTo(x) > 0);

                if (j < i)
                {
                    break;
                }

                // 交换a[i]和a[j]
                tmp = array[i];
                array[i] = array[j];
                array[j] = tmp;
            }

            //Util.Swap<T>(array, p, j);
            tmp = array[p];
            array[p] = array[j];
            array[j] = tmp;
        }
    }
}
