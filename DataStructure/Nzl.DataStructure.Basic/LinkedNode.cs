namespace Nzl.DataStructure.Basic
{
    using System;

    /// <summary>
    /// The linked node.
    /// </summary>
    public class LinkedNode<T>
    {
        /// <summary>
        /// 
        /// </summary>
        protected T _key;

        /// <summary>
        /// 
        /// </summary>
        protected object _val;

        /// <summary>
        /// 
        /// </summary>
        public virtual T Key
        {
            get
            {
                return this._key;                    
            }

            set
            {
                this._key = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual object Value
        {
            get
            {
                return this._val;
            }

            set
            {
                this._val = value;
            }
        }
    }
}
