namespace Nzl.Messaging
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public static class MessageQueue
    {
        /// <summary>
        /// 
        /// </summary>
        private static Queue<Message> _messageQueue = new Queue<Message>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public static void Enqueue(Message msg)
        {
            if (msg != null)
            {
                lock (_messageQueue)
                {
                    if (_messageQueue.Count > 1000)
                    {
                        _messageQueue.Dequeue();
                    }

                    _messageQueue.Enqueue(msg);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static Message Dequeue()
        {
            try
            {
                lock (_messageQueue)
                {
                    if (_messageQueue.Count > 0)
                    {
                        return _messageQueue.Dequeue();
                    }

                    return null;
                }
            }
            catch (Exception exp)
            {
                return null;
            }
        }
    }
}
