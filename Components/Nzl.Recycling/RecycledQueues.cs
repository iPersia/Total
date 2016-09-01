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
        private static Dictionary<Type, Queue<IRecycled>> _dictRecycledQueues = new Dictionary<Type, Queue<IRecycled>>();

        /// <summary>
        /// 
        /// </summary>
        private static Queue<IRecycled> GetQueue(Type type)
        {
            if (type != null)
            {
                lock(_dictRecycledQueues)
                {
                    if (_dictRecycledQueues.ContainsKey(type))
                    {
                        return _dictRecycledQueues[type];
                    }

                    Queue<IRecycled> queue = new Queue<IRecycled>();
                    _dictRecycledQueues.Add(type, queue);
                    return queue;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T GetRecycled<T>()
            where T : class
        {
            Queue<IRecycled> queue = GetQueue(typeof(T));
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
            where T : class, IRecycled
        {
            if (obj != null)
            {
                Queue<IRecycled> queue = GetQueue(typeof(T));
                lock(queue)
                {
                    if (queue.Contains(obj) == false)
                    {
                        obj.Recycling();
                        queue.Enqueue(obj);
                        obj.Status = RecycledStatus.Recycled;
                    }
                }
#if (DEBUG)
                System.Diagnostics.Debug.WriteLine("RecycledQueues - AddRecycled - Type is " + obj.GetType().ToString() + "\tQueue size is " + queue.Count);
#endif
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetStatistics()
        {
            string msg = "Recycled object list:\n";
            foreach (KeyValuePair<Type, Queue<IRecycled>> pair in _dictRecycledQueues)
            {
                msg += "\t" + pair.Key.ToString() + "\t" + pair.Value.Count + "\n";
            }

            return msg.TrimEnd('\n');
        }
    }
}
