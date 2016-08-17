namespace Nzl.Recycling
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public static class RecycledQueues
    {
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<Type, Queue<Object>> _dictRecycledQueues = new Dictionary<Type, Queue<object>>();

        /// <summary>
        /// 
        /// </summary>
        public static Queue<Object> GetQueue(Type type)
        {
            if (type != null)
            {
                if (_dictRecycledQueues.ContainsKey(type))
                {
                    return _dictRecycledQueues[type];
                }

                Queue<object> queue = new Queue<object>();
                _dictRecycledQueues.Add(type, queue);
                return queue;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T GetRecycled<T>()
            where T : class
        {
            Queue<object> queue = GetQueue(typeof(T));
            lock (queue)
            {
                try
                {
                    if (queue.Count > 0)
                    {
#if (DEBUG)
                        System.Diagnostics.Debug.WriteLine("RecycledQueues - Before GetRecycled - Type is " + typeof(T).ToString() + "\tQueue size is " + queue.Count);
#endif
                        return queue.Dequeue() as T;
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
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static void AddRecycled<T>(T obj)
            where T : class
        {
            if (obj != null)
            {
                Queue<object> queue = GetQueue(typeof(T));
                queue.Enqueue(obj);
#if (DEBUG)
//                System.Diagnostics.Debug.WriteLine("RecycledQueues - AddRecycled - Type is " + obj.GetType().ToString() + "\tQueue size is " + queue.Count);
#endif
            }
        }
    }
}
