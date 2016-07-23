namespace Nzl.DataStructure.Basic
{
    using System;

    /// <summary>
    /// The double linked node class.
    /// </summary>
    public class DoubleLinkedNode<T> : LinkedNode<T>
    {
        /// <summary>
        /// 
        /// </summary>
        private DoubleLinkedNode<T> _last;

        /// <summary>
        /// 
        /// </summary>
        private DoubleLinkedNode<T> _next;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public DoubleLinkedNode(T key, object val)
        {
            this._key = key;
            this._val = val;
        }

        /// <summary>
        /// 
        /// </summary>
        public DoubleLinkedNode<T> Last
        {
            get
            {
                return this._last;
            }

            set
            {
                this._last = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DoubleLinkedNode<T> Next
        {
            get
            {
                return this._next;
            }

            set
            {
                this._next = value;
            }
        }
    }
}
