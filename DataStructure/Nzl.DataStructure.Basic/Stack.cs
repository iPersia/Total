namespace Nzl.DataStructure.Basic
{
    using System;
    using Nzl.DataStructure;

    /// <summary>
    /// The stack data structure class.
    /// </summary>
    public class Stack<T> : DataStructure
        where T : IComparable, new()
    {
        /// <summary>
        /// The data structure in double linked list.
        /// </summary>
        private DoubleLinkedList<T> _list = new DoubleLinkedList<T>();

        /// <summary>
        /// Push a data.
        /// </summary>
        /// <param name="val">The T value to be inserted.</param>
        public void Push(T val)
        {
            _list.Add(new DoubleLinkedNode<T>(val, val));
        }

        /// <summary>
        /// Pop a data.
        /// </summary>
        /// <returns>The T value.</returns>
        public T Pop()
        {
            if (_list.Length < 1)
            {
                throw new Exception("Underflow");
            }

            T val = _list.Tail.Key;
            _list.Delete(_list.Tail);
            return val;
        }

        /// <summary>
        /// The data structure name.
        /// </summary>
        public override string Name
        {
            get
            {
                return base.Name + " - Stack ";
            }
        }           
    }
}
