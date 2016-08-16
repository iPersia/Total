namespace Nzl.Smth.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public class RecycledControlQueue<T> 
    {
        /// <summary>
        /// Recycled the unused controls.
        /// </summary>
        private static Queue<T> RecycledControls = new Queue<T>();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static T Dequeue()
        {
            lock (RecycledControls)
            {
                try
                {
                    if (RecycledControls.Count > 0)
                    {
                        return RecycledControls.Dequeue();
                    }

                    return default(T);
                }
                catch
                {
                    return default(T);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static void Enqueue(T t)
        {
            lock (RecycledControls)
            {
                RecycledControls.Enqueue(t);
            }
        }
    }
}
