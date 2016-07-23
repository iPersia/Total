namespace Nzl.Test.Algorithm
{
    using System;
    using System.Collections.Generic;
    using Nzl.Algorithm;
    using Nzl.Core;
    using Nzl.Core.Interface;
    using Nzl.DataStructure.BinarySearchTree;

    public class Test_BinarySearchTree : ITest
    {
        /// <summary>
        /// 
        /// </summary>
        private int size = 10000;

        public void Test()
        {
            int[] keys = Util.GetRandomizedArray(size);
            BinarySearchTreeNode root = BinarySearchTree.NIL;
            foreach (int key in keys)
            {
                BinarySearchTreeNode node = new BinarySearchTreeNode(key, key.ToString());
                BinarySearchTree.Insert(ref root, node);
            }

            List<object> list = new List<object>();
            //BinarySearchTree.InOrderTraverse(root, ref list);
            //System.Console.WriteLine(string.Join(", ", list));

            ////For test Successor and Predecessor
            //for (int i = 5; i < 100; i += 5)
            //{
            //    BinarySearchTreeNode node = BinarySearchTree.Search(root, keys[i]);
            //    BinarySearchTreeNode succ = BinarySearchTree.Successor(node);
            //    BinarySearchTreeNode pred = BinarySearchTree.Predecessor(node);

            //    System.Console.WriteLine(node.Key + "\t Pred is :\t" + pred.Key);
            //    System.Console.WriteLine(node.Key + "\t Succ is :\t" + succ.Key);
            //}

            //For test delete
            for (int i = 2; i < size; i += 2)
            {
                BinarySearchTreeNode node = BinarySearchTree.Search(root, keys[i]);
                BinarySearchTree.Delete(ref root, node);

                list.Clear();
                BinarySearchTree.InOrderTraverse(root, ref list);
                bool flag = Util.CheckOrder(list);
                if (flag == false)
                {
                    System.Console.WriteLine(flag ? "Order is better" : "Order is bad");
                }

                //list.Clear();
                //BinarySearchTree.InOrderTraverse(root, ref list);
                //System.Console.WriteLine(string.Join(", ", list));
                //System.Console.WriteLine("After Delete " + keys[i]);
                //System.Console.WriteLine(string.Join(", ", list));
            }

            System.Console.WriteLine("Test over");
        }
    }
}
