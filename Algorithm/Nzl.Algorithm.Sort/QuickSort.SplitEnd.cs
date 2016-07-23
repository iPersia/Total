namespace Nzl.Algorithm.Sort
{
    using System;

    /// <summary>
    /// QuickSort template calss.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    partial class QuickSort<T>
    {
        /// <summary>
        /// Split edit sort method internal.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="p"></param>
        /// <param name="r"></param>
        private static void SplitEndInternal(T[] array, int p, int r)
        {
            if (p < r)
            {
                if (p != 0 && r != (array.Length - 1) && r - p + 1 < 20)
                {
                    InsertionSort<T>.Sort(array, p, r);
                    return;
                }

                int len = r - p + 1;
                int m = p + (len >> 1);
                if (len > 7)
                {
                    int l = p;
                    int n = p + len - 1;
                    if (len > 40)
                    {
                        int s = len / 8;
                        l = Median3(array, l, l + s, l + 2 * s);
                        m = Median3(array, m - s, m, m + s);
                        n = Median3(array, n - 2 * s, n - s, n);
                    }
                    m = Median3(array, l, m, n);
                }

                T v = array[m];
                T tmp;

                // a,b进行左端扫描，c,d进行右端扫描
                int a = p, b = a, c = p + len - 1, d = c;
                while (true)
                {
                    // 尝试找到大于pivot的元素
                    while (b <= c && array[b].CompareTo(v) <= 0)
                    {
                        // 与pivot相同的交换到左端
                        if (array[b].CompareTo(v) == 0)
                        {
                            tmp = array[a];
                            array[a++] = array[b];
                            array[b] = tmp;
                        }

                        b++;
                    }
                    // 尝试找到小于pivot的元素
                    while (c >= b && array[c].CompareTo(v) >= 0)
                    {
                        // 与pivot相同的交换到右端
                        if (array[c].CompareTo(v) == 0)
                        {
                            tmp = array[c];
                            array[c] = array[d];
                            array[d--] = tmp;
                        }

                        c--;
                    }

                    if (b > c)
                    {
                        break;
                    }

                    // 交换找到的元素
                    tmp = array[b];
                    array[b++] = array[c];
                    array[c--] = tmp;
                }

                // 将相同的元素交换到中间
                {
                    int s, n = p + len;
                    s = Math.Min(a - p, b - a);
                    //Swaps(array, p, b - s, s);
                    {
                        int start = p;
                        int end = b - s;
                        int count = s;
                        for (int i = 0; i < count; i++, start++, end++)
                        {
                            tmp = array[start];
                            array[start] = array[end];
                            array[end] = tmp;
                        }
                    }
                    s = Math.Min(d - c, n - d - 1);
                    //Swaps(array, b, n - s, s);
                    {
                        int start = b;
                        int end = n - s;
                        int count = s;
                        for (int i = 0; i < count; i++, start++, end++)
                        {
                            tmp = array[start];
                            array[start] = array[end];
                            array[end] = tmp;
                        }
                    }

                    // 递归调用子序列
                    if ((s = b - a) > 1)
                    {
                        SplitEndInternal(array, p, s + p - 1);
                    }

                    if ((s = d - c) > 1)
                    {
                        SplitEndInternal(array, n - s, n - 1);
                    }
                }
            }
        }

        /// <summary>
        /// Split-edit sort method internal.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="p"></param>
        /// <param name="r"></param>
        private static int SplitEndPartition(T[] array, int p, int r)
        {
            return 0;
        }

        /// <summary>
        /// Swap values.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="n"></param>
        internal static void Swaps(T[] array, int a, int b, int n)
        {
            T tmp;
            for (int i = 0; i < n; i++, a++, b++)
            {
                tmp = array[a];
                array[a] = array[b];
                array[b] = tmp;
            }
        }
    }
}
