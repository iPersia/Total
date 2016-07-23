namespace Nzl.DataStructure.RedBlackTree
{
    using System;
    using Nzl.DataStructure;

    /// <summary>
    /// Red black tree node class.
    /// </summary>
    public class RedBlackTreeNode : DataStructure
    {
        /// <summary>
        /// Left child node.
        /// </summary>
        private RedBlackTreeNode _lChild;

        /// <summary>
        /// Right child node.
        /// </summary>
        private RedBlackTreeNode _rChild;

        /// <summary>
        /// Parent node.
        /// </summary>
        private RedBlackTreeNode _pNode;

        /// <summary>
        /// The key.
        /// </summary>
        private int _key;

        /// <summary>
        /// The value.
        /// </summary>
        private object _val;

        /// <summary>
        /// The color.
        /// </summary>
        private RedBlackTreeNodeColor _color;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="?"></param>
        public RedBlackTreeNode(int key, object val, RedBlackTreeNodeColor color)
        {
            this._key = key;
            this._val = val;
            this._color = color;
            this.Parent = RedBlackTree.NIL;
            this.LeftChild = RedBlackTree.NIL;
            this.RightChild = RedBlackTree.NIL;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="p"></param>
        /// <param name="l"></param>
        /// <param name="r"></param>
        public RedBlackTreeNode(int key, object val, RedBlackTreeNodeColor color, RedBlackTreeNode p, RedBlackTreeNode l, RedBlackTreeNode r)
            : this(key, val, color)
        {
            this._pNode = p;
            this._lChild = l;
            this._rChild = r;
        }

        /// <summary>
        /// Left child.
        /// </summary>
        public RedBlackTreeNode LeftChild
        {
            get
            {
                return this._lChild;
            }

            set
            {
                //if (this.Value.ToString() == "NIL")
                //{
                //    return;
                //}

                this._lChild = value;
            }
        }

        /// <summary>
        /// Right child.
        /// </summary>
        public RedBlackTreeNode RightChild
        {
            get
            {
                return this._rChild;
            }

            set
            {
                //if (this.Value.ToString() == "NIL")
                //{
                //    return;
                //}

                this._rChild = value;
            }
        }

        /// <summary>
        /// Parent node.
        /// </summary>
        public RedBlackTreeNode Parent
        {
            get
            {
                return this._pNode;
            }

            set
            {
                //if (this.Value.ToString() == "NIL")
                //{
                //    return;
                //}

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
                //if (this.Value.ToString() == "NIL")
                //{
                //    return;
                //}

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
                //if (this.Value.ToString() == "NIL")
                //{
                //    return;
                //}

                this._val = value;
            }
        }

        /// <summary>
        /// The color.
        /// </summary>
        public RedBlackTreeNodeColor Color
        {
            get
            {
                return this._color;
            }

            set
            {
                //if (this.Value.ToString() == "NIL")
                //{
                //    return;
                //}

                this._color = value;
            }
        }

        public override string Name
        {
            get
            {
                return base.Name + " - Red Black Tree ";
            }
        }
    }    
}
