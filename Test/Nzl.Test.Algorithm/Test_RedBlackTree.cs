namespace Nzl.Test.Algorithm
{
    using System;
    using System.Collections.Generic;
    using Nzl.Algorithm;
    using Nzl.Core;
    using Nzl.Core.Interface;
    using Nzl.DataStructure;
    using Nzl.DataStructure.BinarySearchTree;
    using Nzl.DataStructure.RedBlackTree;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Test_RedBlackTree : ITest
    {

        /// <summary>
        /// 
        /// </summary>
        private int size = 100;

        /// <summary>
        /// 
        /// </summary>
        public void Test()
        {
            //CompareBSTnRBT();
            //return;

            int[] keys = Util.GetRandomizedArray(size);
            RedBlackTreeNode root = RedBlackTree.NIL;
            foreach (int key in keys)
            {
                RedBlackTreeNode node = new RedBlackTreeNode(key, key.ToString(), RedBlackTreeNodeColor.Red);
                RedBlackTree.Insert(ref root, node);
            }

            string error = IsARedBlackTree(root);
            if (string.IsNullOrEmpty(error))
            {
                System.Console.WriteLine(error);
            }

            //
            List<object> list = new List<object>();
            RedBlackTree.InOrderTraverse(root, ref list);

            //For test delete
            int step = size / 10000;
            step = step < 1 ? 1 : step;
            for (int i = 0; i < size - 1; i += step)
            {
                RedBlackTreeNode node = RedBlackTree.Search(root, keys[i]);
                RedBlackTree.Delete(ref root, node);
                error = IsARedBlackTree(root);
                if (string.IsNullOrEmpty(error) == false)
                {
                    System.Console.WriteLine(error);
                    System.Console.ReadLine();
                }
            }

            System.Console.WriteLine("Test over");
        }

        private void CompareBSTnRBT()
        {
            switch (1)
            {
                case 0://Max depth
                    {
                        int[] keys = Util.GetRandomizedArray(size);
                        BinarySearchTreeNode rootBST = null;
                        foreach (int key in keys)
                        {
                            BinarySearchTreeNode nodeBST = new BinarySearchTreeNode(key, key.ToString());
                            BinarySearchTree.Insert(ref rootBST, nodeBST);
                        }
                        List<BinarySearchTreeNode> leafListBST = GetLeafList(rootBST);
                        System.Console.WriteLine("BST:\t" + leafListBST[leafListBST.Count - 1].Value.ToString().Length);
                        GC.Collect();

                        RedBlackTreeNode rootRBT = RedBlackTree.NIL;
                        foreach (int key in keys)
                        {
                            RedBlackTreeNode nodeRBT = new RedBlackTreeNode(key, key.ToString(), RedBlackTreeNodeColor.Red);
                            RedBlackTree.Insert(ref rootRBT, nodeRBT);
                        }
                        List<RedBlackTreeNode> leafListRBT = GetLeafList(rootRBT);
                        System.Console.WriteLine("RBT:\t" + leafListRBT[leafListRBT.Count - 1].Value.ToString().Length);
                        GC.Collect();
                    }
                    break;
                case 1://Avg search depth
                    {
                        int[] keys = Util.GetRandomizedArray(size);
                        BinarySearchTreeNode rootBST = null;
                        foreach (int key in keys)
                        {
                            BinarySearchTreeNode nodeBST = new BinarySearchTreeNode(key, key.ToString());
                            BinarySearchTree.Insert(ref rootBST, nodeBST);
                        }

                        int depth = 0;
                        foreach (int key in keys)
                        {
                            BinarySearchTreeNode node = BinarySearchTree.Search(rootBST, key);
                            depth += BinarySearchTree.GetDepth(node);
                        }

                        System.Console.WriteLine("BST Avg Search Depth:\t {0}", (double)depth / (double)size);
                        List<BinarySearchTreeNode> leafListBST = GetLeafList(rootBST);
                        System.Console.WriteLine("BST:\t" + leafListBST[leafListBST.Count - 1].Value.ToString().Length);
                        GC.Collect();

                        RedBlackTreeNode rootRBT = RedBlackTree.NIL;
                        foreach (int key in keys)
                        {
                            RedBlackTreeNode nodeRBT = new RedBlackTreeNode(key, key.ToString(), RedBlackTreeNodeColor.Red);
                            RedBlackTree.Insert(ref rootRBT, nodeRBT);
                        }

                        depth = 0;
                        foreach (int key in keys)
                        {
                            RedBlackTreeNode node = RedBlackTree.Search(rootRBT, key);
                            depth += RedBlackTree.GetDepth(node);
                        }

                        System.Console.WriteLine("RBT Avg Search Depth:\t {0}", (double)depth / (double)size);
                        List<RedBlackTreeNode> leafListRBT = GetLeafList(rootRBT);
                        System.Console.WriteLine("RBT:\t" + leafListRBT[leafListRBT.Count - 1].Value.ToString().Length);
                        GC.Collect();
                    }
                    break;
                case 2:
                    {

                    }
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private List<RedBlackTreeNode> GetLeafList(RedBlackTreeNode root)
        {
            List<RedBlackTreeNode> leafList = new List<RedBlackTreeNode>();
            Queue<RedBlackTreeNode> queue = new Queue<RedBlackTreeNode>();
            root.Value = 1;
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                RedBlackTreeNode node = queue.Dequeue();
                if (node.LeftChild != RedBlackTree.NIL)
                {
                    queue.Enqueue(node.LeftChild);
                }

                if (node.RightChild != RedBlackTree.NIL)
                {
                    queue.Enqueue(node.RightChild);
                }

                if (node.LeftChild == RedBlackTree.NIL && node.RightChild == RedBlackTree.NIL)
                {
                    leafList.Add(node);
                }

                if (node != root)
                    node.Value = node.Parent.Value.ToString() + "1";
            }

            return leafList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private List<BinarySearchTreeNode> GetLeafList(BinarySearchTreeNode root)
        {
            List<BinarySearchTreeNode> leafList = new List<BinarySearchTreeNode>();
            Queue<BinarySearchTreeNode> queue = new Queue<BinarySearchTreeNode>();
            root.Value = "1";
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                BinarySearchTreeNode node = queue.Dequeue();
                if (node.LeftChild != null)
                {
                    queue.Enqueue(node.LeftChild);
                }

                if (node.RightChild != null)
                {
                    queue.Enqueue(node.RightChild);
                }

                if (node.LeftChild == null && node.RightChild == null)
                {
                    leafList.Add(node);
                }

                if (node != root)
                    node.Value = node.Parent.Value.ToString() + "1";
            }

            return leafList;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private string IsARedBlackTree(RedBlackTreeNode root)
        {
            //Check Order
            List<object> list = new List<object>();
            RedBlackTree.InOrderTraverse(root, ref list);
            if (Util.CheckOrder(list) == false)
            {
                return "Order is bad!";
            }

            int upperDepth = (int)(2 * Math.Log((double)(size + 1), (double)2));
            int[] bins = new int[upperDepth];

            List<RedBlackTreeNode> leafList = new List<RedBlackTreeNode>();
            Queue<RedBlackTreeNode> queue = new Queue<RedBlackTreeNode>();
            root.Value = "1";
            bins[1]++;
            if (root.LeftChild != RedBlackTree.NIL)
            {
                root.LeftChild.Value = "1" + ((int)(root.LeftChild.Color)).ToString();
                queue.Enqueue(root.LeftChild);
            }

            if (root.RightChild != RedBlackTree.NIL)
            {
                root.RightChild.Value = "1" + ((int)(root.RightChild.Color)).ToString();
                queue.Enqueue(root.RightChild);
            }

            while (queue.Count > 0)
            {
                RedBlackTreeNode node = queue.Dequeue();
                node.Value = node.Parent.Value.ToString() + ((int)(node.Color)).ToString();
                if (node.LeftChild != RedBlackTree.NIL)
                {
                    queue.Enqueue(node.LeftChild);
                    if (node.Color == RedBlackTreeNodeColor.Red && node.LeftChild.Color == RedBlackTreeNodeColor.Red)
                    {
                        return "The color of the child is red, so is the parent!";
                    }
                }

                if (node.RightChild != RedBlackTree.NIL)
                {
                    queue.Enqueue(node.RightChild);
                    if (node.Color == RedBlackTreeNodeColor.Red && node.RightChild.Color == RedBlackTreeNodeColor.Red)
                    {
                        return "The color of the child is red, so is the parent!";
                    }
                }

                if (node.LeftChild == RedBlackTree.NIL && node.RightChild == RedBlackTree.NIL)
                {
                    leafList.Add(node);
                }

                bins[node.Value.ToString().Length]++;
            }

            if (leafList.Count < 1)
            {
                return string.Empty;
            }

            //Check depth
            int num = NumberOfOne(leafList[0].Value.ToString());
            for (int i = 0; i < leafList.Count; i++)
            {
                if (num != NumberOfOne(leafList[i].Value.ToString()))
                {
                    return "Black Depth are different!";
                }
            }
            System.Console.WriteLine("Black Depth: {0}", num);

            double avg = 0.0;
            for (int i = 0; i < upperDepth; i++)
            {
                //System.Console.WriteLine("Depth: {0}\t Number: {1} Ratio: {2}", i, bins[i], (double)bins[i] / (double)(list.Count));
                avg += bins[i] * i;
            }

            avg = avg / (double)(list.Count);
            System.Console.WriteLine("Avg: {0}", avg);
            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private int NumberOfOne(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }

            int count = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '1')
                {
                    count++;
                }
            }

            return count;
        }
    }
}
