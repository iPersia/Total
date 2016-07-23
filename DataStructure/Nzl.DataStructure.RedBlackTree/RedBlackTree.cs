namespace Nzl.DataStructure.RedBlackTree
{
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// Red black tree.
    /// </summary>
    public static class RedBlackTree
    {
        /// <summary>
        /// The static nil node.
        /// </summary>
        private static readonly RedBlackTreeNode _nil = new RedBlackTreeNode(Int32.MinValue, "NIL", RedBlackTreeNodeColor.Black);

        /// <summary>
        /// In order traverse the binary search tree.
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static void InOrderTraverse(RedBlackTreeNode root, ref List<object> list)
        {
            if (root == RedBlackTree.NIL)
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
        public static RedBlackTreeNode Search(RedBlackTreeNode node, int k)
        {
            if (node == RedBlackTree.NIL || k == node.Key)
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
        public static RedBlackTreeNode Maximum(RedBlackTreeNode node)
        {
            while (node.RightChild != RedBlackTree.NIL)
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
        public static RedBlackTreeNode Minimum(RedBlackTreeNode node)
        {
            while (node.LeftChild != RedBlackTree.NIL)
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
        public static RedBlackTreeNode Predecessor(RedBlackTreeNode node)
        {
            if (node.LeftChild != RedBlackTree.NIL)
            {
                return RedBlackTree.Maximum(node.LeftChild);
            }

            RedBlackTreeNode p = node.Parent;
            while (p != RedBlackTree.NIL && node == p.LeftChild)
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
        public static RedBlackTreeNode Successor(RedBlackTreeNode node)
        {
            if (node.RightChild != RedBlackTree.NIL)
            {
                return RedBlackTree.Minimum(node.RightChild);
            }

            RedBlackTreeNode p = node.Parent;
            while (p != RedBlackTree.NIL && node == p.RightChild)
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
        public static void Insert(ref RedBlackTreeNode root, RedBlackTreeNode z)
        {
            RedBlackTreeNode x = root;
            RedBlackTreeNode y = RedBlackTree.NIL;
            while (x != RedBlackTree.NIL)
            {
                y = x;
                if (z.Key < x.Key)
                {
                    x = x.LeftChild;
                }
                else
                {
                    x = x.RightChild;
                }
            }

            z.Parent = y;
            if (y == RedBlackTree.NIL)
            {
                root = z;
                root.Parent = RedBlackTree.NIL;
                root.Color = RedBlackTreeNodeColor.Black;
                return;
            }

            if (z.Key < y.Key)
            {
                y.LeftChild = z;
            }
            else
            {
                y.RightChild = z;
            }

            z.LeftChild = RedBlackTree.NIL;
            z.RightChild = RedBlackTree.NIL;
            z.Color = RedBlackTreeNodeColor.Red;
            InsertFixup(ref root, z);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        private static void LeftRotate(ref RedBlackTreeNode root, RedBlackTreeNode x)
        {
            RedBlackTreeNode y = x.RightChild;
            x.RightChild = y.LeftChild;
            if (y.LeftChild != RedBlackTree.NIL)
            {
                y.LeftChild.Parent = x;
            }

            y.Parent = x.Parent;
            if (x.Parent == RedBlackTree.NIL)
            {
                root = y;
            }
            else
            {
                if (x.Parent.LeftChild == x)
                {
                    x.Parent.LeftChild = y;
                }
                else
                {
                    x.Parent.RightChild = y;
                }
            }

            y.LeftChild = x;
            x.Parent = y;
        }

        private static void RightRotate(ref RedBlackTreeNode root, RedBlackTreeNode x)
        {
            RedBlackTreeNode y = x.LeftChild;
            x.LeftChild = y.RightChild;
            if (y.RightChild != RedBlackTree.NIL)
            {
                y.RightChild.Parent = x;
            }

            y.Parent = x.Parent;
            if (x.Parent == RedBlackTree.NIL)
            {
                root = y;
            }
            else
            {
                if (x.Parent.LeftChild == x)
                {
                    x.Parent.LeftChild = y;
                }
                else
                {
                    x.Parent.RightChild = y;
                }
            }

            y.RightChild = x;
            x.Parent = y;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <param name="node"></param>
        private static void InsertFixup(ref RedBlackTreeNode root, RedBlackTreeNode z)
        {
            while (z.Parent != RedBlackTree.NIL && z.Parent.Color == RedBlackTreeNodeColor.Red)
            {
                if (z.Parent == z.Parent.Parent.LeftChild)
                {
                    RedBlackTreeNode y = z.Parent.Parent.RightChild;
                    if (y != RedBlackTree.NIL && y.Color == RedBlackTreeNodeColor.Red)
                    {
                        z.Parent.Color = RedBlackTreeNodeColor.Black;
                        y.Color = RedBlackTreeNodeColor.Black;
                        z.Parent.Parent.Color = RedBlackTreeNodeColor.Red;
                        z = z.Parent.Parent;
                    }
                    else
                    {
                        if (z == z.Parent.RightChild)
                        {
                            z = z.Parent;
                            LeftRotate(ref root, z);
                        }

                        z.Parent.Color = RedBlackTreeNodeColor.Black;
                        z.Parent.Parent.Color = RedBlackTreeNodeColor.Red;
                        RightRotate(ref root, z.Parent.Parent);
                    }
                }
                else
                {
                    RedBlackTreeNode y = z.Parent.Parent.LeftChild;
                    if (y != RedBlackTree.NIL && y.Color == RedBlackTreeNodeColor.Red)
                    {
                        z.Parent.Color = RedBlackTreeNodeColor.Black;
                        y.Color = RedBlackTreeNodeColor.Black;
                        z.Parent.Parent.Color = RedBlackTreeNodeColor.Red;
                        z = z.Parent.Parent;
                    }
                    else
                    {
                        if (z == z.Parent.LeftChild)
                        {
                            z = z.Parent;
                            RightRotate(ref root, z);
                        }

                        z.Parent.Color = RedBlackTreeNodeColor.Black;
                        z.Parent.Parent.Color = RedBlackTreeNodeColor.Red;
                        LeftRotate(ref root, z.Parent.Parent);
                    }
                }
            }

            root.Color = RedBlackTreeNodeColor.Black;
        }

        /// <summary>
        /// Delete a key.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="newnode"></param>
        public static RedBlackTreeNode Delete(ref RedBlackTreeNode root, RedBlackTreeNode z)
        {
            RedBlackTreeNode x = RedBlackTree.NIL;
            RedBlackTreeNode y = RedBlackTree.NIL;
            if (z.LeftChild == RedBlackTree.NIL || z.RightChild == RedBlackTree.NIL)
            {
                y = z;
            }
            else
            {
                y = Successor(z);
            }

            if (y.LeftChild != RedBlackTree.NIL)
            {
                x = y.LeftChild;
            }
            else
            {
                x = y.RightChild;
            }

            x.Parent = y.Parent;
            if (y.Parent == RedBlackTree.NIL)
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

            if (y.Color == RedBlackTreeNodeColor.Black)
            {
                DeleteFixup(ref root, x);
            }

            return y;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <param name="x"></param>
        private static void DeleteFixup(ref RedBlackTreeNode root, RedBlackTreeNode x)
        {
            RedBlackTreeNode w = RedBlackTree.NIL;
            while (x != root && x.Color == RedBlackTreeNodeColor.Black)
            {
                if (x.Parent.LeftChild == x)
                {
                    w = x.Parent.RightChild;
                    if (w.Color == RedBlackTreeNodeColor.Red)
                    {
                        w.Color = RedBlackTreeNodeColor.Black;
                        x.Parent.Color = RedBlackTreeNodeColor.Red;
                        LeftRotate(ref root, x.Parent);
                        w = x.Parent.RightChild;
                    }

                    if (w.LeftChild.Color == RedBlackTreeNodeColor.Black && w.RightChild.Color == RedBlackTreeNodeColor.Black)
                    {
                        w.Color = RedBlackTreeNodeColor.Red;
                        x = x.Parent;
                    }
                    else
                    {
                        if (w.RightChild.Color == RedBlackTreeNodeColor.Black)
                        {
                            w.LeftChild.Color = RedBlackTreeNodeColor.Black;
                            w.Color = RedBlackTreeNodeColor.Red;
                            RightRotate(ref root, w);
                            w = x.Parent.RightChild;
                        }

                        w.Color = x.Parent.Color;
                        x.Parent.Color = RedBlackTreeNodeColor.Black;
                        w.RightChild.Color = RedBlackTreeNodeColor.Black;
                        LeftRotate(ref root, x.Parent);
                        x = root;
                    }
                }
                else
                {
                    w = x.Parent.LeftChild;
                    if (w.Color == RedBlackTreeNodeColor.Red)
                    {
                        w.Color = RedBlackTreeNodeColor.Black;
                        x.Parent.Color = RedBlackTreeNodeColor.Red;
                        RightRotate(ref root, x.Parent);
                        w = x.Parent.LeftChild;
                    }

                    if (w.RightChild.Color == RedBlackTreeNodeColor.Black && w.LeftChild.Color == RedBlackTreeNodeColor.Black)
                    {
                        w.Color = RedBlackTreeNodeColor.Red;
                        x = x.Parent;
                    }
                    else
                    {
                        if (w.LeftChild.Color == RedBlackTreeNodeColor.Black)
                        {
                            w.RightChild.Color = RedBlackTreeNodeColor.Black;
                            w.Color = RedBlackTreeNodeColor.Red;
                            LeftRotate(ref root, w);
                            w = x.Parent.LeftChild;
                        }

                        w.Color = x.Parent.Color;
                        x.Parent.Color = RedBlackTreeNodeColor.Black;
                        w.LeftChild.Color = RedBlackTreeNodeColor.Black;
                        RightRotate(ref root, x.Parent);
                        x = root;
                    }
                }
            }

            x.Color = RedBlackTreeNodeColor.Black;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static int GetBlackDepth(RedBlackTreeNode node)
        {
            int depth = 0;
            RedBlackTreeNode x = node;
            while (x != RedBlackTree.NIL)
            {
                if (x.Color == RedBlackTreeNodeColor.Black)
                {
                    depth++;
                }

                x = x.Parent;
            }

            return depth;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static int GetDepth(RedBlackTreeNode node)
        {
            int depth = 0;
            RedBlackTreeNode x = node;
            while (x != RedBlackTree.NIL)
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
                return "Red Black Tree";
            }
        }

        /// <summary>
        /// The nil red black tree node.
        /// </summary>
        public static RedBlackTreeNode NIL
        {
            get
            {
                return _nil;
            }
        }
    }
}
