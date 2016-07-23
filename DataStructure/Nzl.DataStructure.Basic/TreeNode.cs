namespace Nzl.DataStructure.Basic
{
    using System;

    /// <summary>
    /// The tree node class.
    /// </summary>
    public class TreeNode<T>
        where T : IComparable<T>, new()
    {
        /// <summary>
        /// 
        /// </summary>
        private TreeNode<T> _parent;

        /// <summary>
        /// 
        /// </summary>
        private TreeNode<T> _sibling;

        /// <summary>
        /// 
        /// </summary>
        private TreeNode<T> _firstChild;

        /// <summary>
        /// 
        /// </summary>
        public TreeNode<T> FirstChild
        {
            get
            {
                return this._firstChild;
            }

            set
            {
                this._firstChild = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public TreeNode<T> Sibling
        {
            get
            {
                return this._sibling;
            }

            set
            {
                this._sibling = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public TreeNode<T> Parent
        {
            get
            {
                return this._parent;
            }

            set
            {
                this._parent = value;
            }
        }
    }
}
