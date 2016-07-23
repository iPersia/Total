namespace Nzl.Test.Algorithm
{
    using System;
    using Nzl.Algorithm.OrderStatistic;
    using Nzl.Algorithm.Sort;
    using Nzl.Core;
    using Nzl.Core.Interface;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Test_OrderStatistics : ITest
    {
        /// <summary>
        /// 
        /// </summary>
        public void Test()
        {
            this.TestRandom();
            return;

            int size = 10000000;
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            int[] array = Util.GetRandomizedArray(size);

            System.Console.WriteLine((new Selection<int>()).Name);
            timer.Start();            
            int val = Selection<int>.Select(array, size / 2);
            timer.Stop();
            System.Console.WriteLine("Selection:\t{0}", timer.ElapsedMilliseconds);

            timer.Start();            
            SortWrapper<int>.Sort(array, SortType.QuickSort);
            timer.Stop();
            System.Console.WriteLine("Sort:\t{0}", timer.ElapsedMilliseconds);

            System.Console.WriteLine("Selection is " + (val == array[size / 2] ? "GOOD!" : "BAD!"));
        }

        public void ShuffleArray_Fisher_Yates(char[] arr, int len)
        {
            int i = len;
            int j;
            char temp;

            Random rand = new Random();
            if (i == 0) return;
            while (--i > 0)
            {
                j = rand.Next() % (i + 1);
                temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        private void ShuffleArray_Manual(char[] arr, int len)
        {
            int mid = len / 2;
            Random rand = new Random();
            for (int n = 0; n < 5; n++)
            {

                //两手洗牌
                for (int i = 1; i < mid; i += 2)
                {
                    char tmp = arr[i];
                    arr[i] = arr[mid + i];
                    arr[mid + i] = tmp;
                }

                //随机切牌
                char[] buf = new char[len];
                for (int j = 0; j < 5; j++)
                {
                    int start = rand.Next() % (len - 1) + 1;
                    int numCards = rand.Next() % (len / 2) + 1;

                    if (start + numCards > len)
                    {
                        numCards = len - start;
                    }
                                        
                    //memset(buf, 0, len);
                    for (int i = 0; i < len; i++)
                    {
                        buf[i] = '\0';
                    }

                    //strncpy(buf, arr, start);
                    for (int i = 0; i < start; i++)
                    {
                        buf[i] = arr[i];
                    }

                    //strncpy(arr, arr + start, numCards);
                    for (int i = 0; i < numCards; i++)
                    {
                        arr[i] = arr[i + start];
                    }

                    //strncpy(arr + numCards, buf, start);
                    for (int i = 0; i < start; i++)
                    {
                        arr[i + numCards] = buf[i];
                    }
                }
            }
        }

        private void SetCharsValue(char[] arr, int val, int start, int end)
        {
            if (arr != null)
            {
                for (int i = start; i < end; i++)
                {
                    arr[i] = (char)val;
                }
            }
        }

        private void CopyChars(char[] dest, char[] from, int start)
        {
            CopyChars(dest, from, start, from.Length);
        }

        private void CopyChars(char[] dest, char[] from, int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                dest[i] = from[i];
            }
        }

        int _Times = 10000000;
        int _Length = 10;

        /// <summary>
        /// 
        /// </summary>
        private void TestRandom()
        {
            char[] _Arr = new char[_Length];
            this.Reset(_Arr, _Arr.Length);

            System.Collections.Generic.Dictionary<char, int[]> dic = new System.Collections.Generic.Dictionary<char, int[]>();
            for (int i = 0; i < _Length; i++)
            {
                dic.Add((char)((int)'A' + i), new int[_Length]);
            }

            for (int i = 0; i < _Times; i++)
            {
                //this.Reset(_Arr, _Arr.Length);
                //this.ShuffleArray_Fisher_Yates(_Arr, _Arr.Length);
                this.ShuffleArray_Manual(_Arr, _Arr.Length);
                this.Stat(dic, _Arr, _Arr.Length);
                //PrintResult(dic, _Arr.Length);
                if (i % (_Times / 100) == 0)
                {
                    System.Console.WriteLine((int)(i / (_Times / 100)));
                }
            }

            PrintResult(dic, _Arr.Length);
        }

        private void Reset(char[] arr, int len)
        {
            for (int i = 0; i < len; i++)
            {
                arr[i] = (char)((int)'A' + i);
            }       
        }
                
        private void Stat(System.Collections.Generic.Dictionary<char, int[]> dic, char[] arr, int length)
        {
            for (int i = 0; i < length; i++)
            {
                dic[arr[i]][i]++;
            }
        }

        private void PrintResult(System.Collections.Generic.Dictionary<char, int[]> dic, int length)
        {
            long deviation = 0L;
            int sum = 0;
            int counter;
            for (int i = 0; i < length; i++)
            {
                char tempChar = (char)((int)'A' + i);
                for (int j = 0; j < length; j++)
                {
                    counter = dic[tempChar][j];
                    sum += counter;
                    deviation += Math.Abs(counter - _Times / (_Length * _Length));
                    System.Console.Write(counter.ToString() + " ");
                }

                System.Console.WriteLine();
            }

            System.Console.WriteLine("Summary : " + sum);
            System.Console.WriteLine("Deviation : " + deviation );
            System.Console.WriteLine("Deviation percentage : " + deviation * 100 / sum);
        }
    }
}
