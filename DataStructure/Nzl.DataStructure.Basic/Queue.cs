namespace Nzl.DataStructure.Basic
{
    using System;
    using Nzl.DataStructure;

    /// <summary>
    /// The queue class.
    /// </summary>
    public class Queue<T> : DataStructure
        where T : IComparable, new()
    {
        /// <summary>
        /// 
        /// </summary>
        private DoubleLinkedList<T> _list = new DoubleLinkedList<T>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        public void Enqueue(T val)
        {
            _list.Add(new DoubleLinkedNode<T>(val, val));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        public T Dequeue()
        {
            if (_list.Length < 1)
            {
                throw new Exception("Underflow");
            }

            T val = _list.Head.Key;
            _list.Delete(_list.Head);
            return val;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get
            {
                return this._list.Length;
            }
        }

        /// <summary>
        /// The name.
        /// </summary>
        public override string Name
        {
            get
            {
                return base.Name + " - Queue ";
            }
        }
    }
}
