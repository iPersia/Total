namespace Nzl.DataStructure.BinarySearchTree
{
    using System;

    /// <summary>
    /// Binary search tree node class.
    /// </summary>
    public class BinarySearchTreeNode
    {
        /// <summary>
        /// Left child node.
        /// </summary>
        private BinarySearchTreeNode _lChild;

        /// <summary>
        /// Right child node.
        /// </summary>
        private BinarySearchTreeNode _rChild;

        /// <summary>
        /// Parent node.
        /// </summary>
        private BinarySearchTreeNode _pNode;

        /// <summary>
        /// The key.
        /// </summary>
        private int _key;

        /// <summary>
        /// The value.
        /// </summary>
        private object _val;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="?"></param>
        public BinarySearchTreeNode(int key, object val)
        {
            this._key = key;
            this._val = val;
            this._lChild = BinarySearchTree.NIL;
            this._rChild = BinarySearchTree.NIL;
            this._pNode = BinarySearchTree.NIL;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="p"></param>
        /// <param name="l"></param>
        /// <param name="r"></param>
        public BinarySearchTreeNode(int key, object val, BinarySearchTreeNode p, BinarySearchTreeNode l, BinarySearchTreeNode r)
            : this(key, val)
        {
            this._pNode = p;
            this._lChild = l;
            this._rChild = r;
        }

        /// <summary>
        /// Left child.
        /// </summary>
        public BinarySearchTreeNode LeftChild
        {
            get
            {
                return this._lChild;
            }

            set
            {
                this._lChild = value;
            }
        }

        /// <summary>
        /// Right child.
        /// </summary>
        public BinarySearchTreeNode RightChild
        {
            get
            {
                return this._rChild;
            }

            set
            {
                this._rChild = value;
            }
        }

        /// <summary>
        /// Parent node.
        /// </summary>
        public BinarySearchTreeNode Parent
        {
            get
            {
                return this._pNode;
            }

            set
            {
                this._pNode = value;
            }
        }

        /// <summary>
        /// The key.
        /// </summary>
        public int Key
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
        /// The value.
        /// </summary>
        public object Value
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
