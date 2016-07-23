namespace Nzl.DataStructure.BinarySearchTree
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Binary search tree class.
    /// </summary>
    public static class BinarySearchTree
    {
        /// <summary>
        /// The nil node.
        /// </summary>
        private static readonly BinarySearchTreeNode _nil = new BinarySearchTreeNode(Int32.MinValue, "NIL");

        /// <summary>
        /// In order traverse the binary search tree.
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static void InOrderTraverse(BinarySearchTreeNode root, ref List<object> list)
        {
            if (root == BinarySearchTree.NIL)
            {
                return;
            }

            InOrderTraverse(root.LeftChild, ref list);
            list.Add(root.Key);
            InOrderTraverse(root.RightChild, ref list);
        }

        /// <summary>
        /// Search
        /// </summary>
        /// <param name="node"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static BinarySearchTreeNode Search(BinarySearchTreeNode node, int k)
        {
            if (node == BinarySearchTree.NIL || k == node.Key)
            {          
                return node;
            }

            if (k < node.Key)
            {
                return Search(node.LeftChild, k);
            }
            else // (k > node.Key)
            {
                return Search(node.RightChild, k);
            }            
        }

        /// <summary>
        /// Get maximum node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static BinarySearchTreeNode Maximum(BinarySearchTreeNode node)
        {
            while (node.RightChild != BinarySearchTree.NIL)
            {
                node = node.RightChild;
            }

            return node;
        }

        /// <summary>
        /// Get maniimum node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static BinarySearchTreeNode Minimum(BinarySearchTreeNode node)
        {
            while (node.LeftChild != BinarySearchTree.NIL)
            {
                node = node.LeftChild;
            }

            return node;
        }
                 
        /// <summary>
        /// Get the Predecessor.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static BinarySearchTreeNode Predecessor(BinarySearchTreeNode node)
        {
            if (node.LeftChild != BinarySearchTree.NIL)
            {
                return BinarySearchTree.Maximum(node.LeftChild);
            }

            BinarySearchTreeNode p = node.Parent;
            while (p != BinarySearchTree.NIL && node == p.LeftChild)
            {
                node = p;
                p = p.Parent;
            }

            return p;
        }

        /// <summary>
        /// Get the Successor.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static BinarySearchTreeNode Successor(BinarySearchTreeNode node)
        {
            if (node.RightChild != BinarySearchTree.NIL)
            {
                return BinarySearchTree.Minimum(node.RightChild);
            }

            BinarySearchTreeNode p = node.Parent;
            while (p != BinarySearchTree.NIL && node == p.RightChild)
            {
                node = p;
                p = p.Parent;                
            }

            return p;
        }

        /// <summary>
        /// Insert a key.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="newnode"></param>
        public static void Insert(ref BinarySearchTreeNode root, BinarySearchTreeNode z)
        {
            BinarySearchTreeNode x = root;
            BinarySearchTreeNode y = BinarySearchTree.NIL;
            while (x != BinarySearchTree.NIL)
            {
                y = x;
                if (z.Key < y.Key)
                {
                    x = y.LeftChild;
                }
                else
                {
                    x = y.RightChild;
                }
            }

            z.Parent = y;
            if (y == BinarySearchTree.NIL)
            {
                root = z;
            }
            else
            {
                if (z.Key < y.Key)
                {
                    y.LeftChild = z;
                }
                else
                {
                    y.RightChild = z;
                }
            }
        }

        /// <summary>
        /// Delete a key.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="newnode"></param>
        public static BinarySearchTreeNode Delete(ref BinarySearchTreeNode root, BinarySearchTreeNode z)
        {
            BinarySearchTreeNode x = BinarySearchTree.NIL;
            BinarySearchTreeNode y = BinarySearchTree.NIL;
            if (z.LeftChild == BinarySearchTree.NIL || z.RightChild == BinarySearchTree.NIL)
            {
                y = z;
            }
            else
            {
                y = Successor(z);
            }

            if (y.LeftChild != BinarySearchTree.NIL)
            {
                x = y.LeftChild;
            }
            else
            {
                x = y.RightChild;
            }

            if (x != BinarySearchTree.NIL)
            {
                x.Parent = y.Parent;
            }

            if (y.Parent == BinarySearchTree.NIL)
            {
                root = x;
            }
            else
            {
                if (y == y.Parent.LeftChild)
                {
                    y.Parent.LeftChild = x;
                }
                else
                {
                    y.Parent.RightChild = x;
                }                    
            }

            if (y != z)
            {
                z.Key = y.Key;
                z.Value = y.Value;
            }

            return y;   
        }

        /// <summary>
        /// The nil node.
        /// </summary>
        public static BinarySearchTreeNode NIL
        {
            get
            {
                return _nil;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static int GetDepth(BinarySearchTreeNode node)
        {
            int depth = 0;
            BinarySearchTreeNode x = node;
            while (x != null)
            {
                x = x.Parent;
                depth++;
            }

            return depth;
        }

        /// <summary>
        /// Algorithm name.
        /// </summary>
        public static string Name
        {
            get
            {
                return "Binary Search Tree";
            }
        }
    }
}
