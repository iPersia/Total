namespace Nzl.Algorithm.OrderStatistic
{
    using System;
    using Nzl.Algorithm;

    /// <summary>
    /// The selection class based on quick sort.
    /// </summary>
    public class Selection<T> : OrderStatistic<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T Maximum(T[] array)
        {
            T max = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (max.CompareTo(array[i]) < 0)
                {
                    max = array[i];
                }
            }

            return max;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T Minimum(T[] array)
        {
            T min = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (min.CompareTo(array[i]) < 0)
                {
                    min = array[i];
                }
            }

            return min;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="k"></param>
        public static T Select(T[] array, int k)
        {
            return SelectInternal(array, 0, array.Length - 1, k);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="k"></param>
        private static T SelectInternal(T[] array, int p, int q, int k)
        {
            if (p <= q)
            {
                int pivot = Partition(array, p, q);
                if (pivot == k)
                {
                    return array[pivot];
                }

                if (pivot > k)
                {
                    return SelectInternal(array, p, pivot-1, k);
                }

                if (pivot < k)
                {
                    return SelectInternal(array, pivot+1, q, k);
                }
            }

            throw new Exception("Error");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        private static int Partition(T[] array, int p, int q)
        {
            T pivot = array[p];
            T tmp;
            int cursor = p + 1;
            int partitionpos = p;
            while (cursor <= q)
            {
                if (pivot.CompareTo(array[cursor]) > 0)
                {
                    partitionpos++;
                    //if (partitionpos < cursor)
                    //{
                    tmp = array[cursor];
                    array[cursor] = array[partitionpos];
                    array[partitionpos] = tmp;
                    //}
                }

                cursor++;
            }

            tmp = array[p];
            array[p] = array[partitionpos];
            array[partitionpos] = tmp;

            return partitionpos;
        }

        /// <summary>
        /// Algorithm name.
        /// </summary>
        public override string Name
        {
            get
            {
                return base.Name + " - Selection";
            }
        }
    }
}
