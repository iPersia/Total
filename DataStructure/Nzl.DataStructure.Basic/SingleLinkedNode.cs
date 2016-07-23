namespace Nzl.DataStructure.Basic
{
    using System;

    /// <summary>
    /// The single linked node class.
    /// </summary>
    public class SingleLinkedNode<T> : LinkedNode<T>
    {
        /// <summary>
        /// 
        /// </summary>
        private SingleLinkedNode<T> _next;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public SingleLinkedNode(T key, object val)
        {
            this._key = key;
            this._val = val;
        }

        /// <summary>
        /// 
        /// </summary>
        public SingleLinkedNode<T> Next
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
