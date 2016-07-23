namespace Nzl.Algorithm
{
    using System;

    /// <summary>
    /// Util class.
    /// </summary>
    public class Util
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        public static void Swap<T>(ref T t1, ref T t2)
        {
            T t = t1;
            t1 = t2;
            t2 = t;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        public static void Swap<T>(T[] array, int i, int j)
        {
            T t = array[i];
            array[i] = array[j];
            array[j] = t;
        }
    }
}
