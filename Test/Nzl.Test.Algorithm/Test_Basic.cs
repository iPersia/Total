namespace Nzl.Test.Algorithm
{
    using System;
    using Nzl.Core;
    using Nzl.Core.Interface;
    using Nzl.DataStructure;
    using Nzl.DataStructure.Basic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Test_Basic : ITest
    {
        public void Test()
        {
            TestStack();
            TestQueue();
            TestSingleLinkedList();
            TestDoubleLinkedList();
        }

        /// <summary>
        /// 
        /// </summary>
        private void TestDoubleLinkedList()
        {
            int size = 10;
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
            System.Console.WriteLine(list.Name);
            for (int i = 0; i < size; i++)
            {
                list.Add(new DoubleLinkedNode<int>(size-i, size-i));
                list.ToString();
            }

            for (int i = size * 2; i >= size; i--)
            {
                DoubleLinkedNode<int> fnode = list.Find(i);
                list.Delete(fnode);
                list.ToString();
            }

            for (int i = 0; i < size; i++)
            {
                DoubleLinkedNode<int> fnode = list.Find(i);
                list.Delete(fnode);
                list.ToString();
            }

            for (int i = size * 2; i >= size; i--)
            {
                DoubleLinkedNode<int> fnode = list.Find(i);
                list.Delete(fnode);
                list.ToString();
            }

            System.Console.WriteLine("Test Over!");
        }

        /// <summary>
        /// 
        /// </summary>
        private void TestSingleLinkedList()
        {
            int size = 10;
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            System.Console.WriteLine(list.Name);
            for (int i = 1; i < size; i++)
            {
                list.Add(new SingleLinkedNode<int>(i, i));
                list.ToString();
            }

            for (int i = size*2; i >= size; i--)
            {
                SingleLinkedNode<int> fnode = list.Find(i);
                list.Delete(fnode);
                list.ToString();
            }            

            for (int i = 0; i < size; i++)
            {
                SingleLinkedNode<int> fnode = list.Find(i);
                list.Delete(fnode);
                list.ToString();
            }

            for (int i = size * 2; i >= size; i--)
            {
                SingleLinkedNode<int> fnode = list.Find(i);
                list.Delete(fnode);
                list.ToString();
            }

            System.Console.WriteLine("Test Over!");
        }

        private void TestQueue()
        {
            TestQueue(10);
            TestQueue(100);
        }

        private void TestQueue(int size)
        {
            Queue<decimal> queue = new Queue<decimal>();
            System.Console.WriteLine(queue.Name);
            for (int i = 0; i < size; i++)
            {
                queue.Enqueue(i);
                queue.Enqueue(i+1);
                queue.Dequeue();
            }

            while (queue.Count > 0)
            {
                System.Console.Write(queue.Dequeue() + ",");
            }

            System.Console.WriteLine();
        }

        private void TestStack()
        {
            TestStack(100);
        }

        private void TestStack(int size)
        {            
            Stack<decimal> stack = new Stack<decimal>();
            System.Console.WriteLine(stack.Name);
            for (int i = 0; i < size; i++)
            {
                stack.Push(i);
            }

            for (int i = 0; i < size; i++)
            {
                System.Console.Write(stack.Pop() + ",");
            }

            System.Console.WriteLine();
        }
    }
}
