namespace Nzl.DataStructure.Basic
{
    using System;
    using Nzl.DataStructure;

    /// <summary>
    /// The double linked list class.
    /// </summary>
    public class DoubleLinkedList<T> : LinkedList<T>
        where T : IComparable, new()
    {
        /// <summary>
        /// 
        /// </summary>
        private DoubleLinkedNode<T> _nil = new DoubleLinkedNode<T>(new T(), "NIL");

        /// <summary>
        /// 
        /// </summary>
        private int _length;
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public DoubleLinkedList()
        {
            this._nil.Next = this._nil;
            this._nil.Last = this._nil;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public DoubleLinkedNode<T> Find(T key)
        {
            DoubleLinkedNode<T> target = this.Head;
            while (target != this._nil)
            {
                if (target.Key.CompareTo(key) == 0)
                {
                    return target;
                }

                target = target.Next;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public void Add(DoubleLinkedNode<T> node)
        {
            if (node == null)
            {
                return;
            }

            DoubleLinkedNode<T> tail = this.Tail;
            this._nil.Last = node;
            node.Next = this._nil;
            tail.Next = node;
            node.Last = tail;
            this._length++;
        }       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public void Delete(DoubleLinkedNode<T> node)
        {            
            DoubleLinkedNode<T> predecessor = this.Predecessor(node);
            DoubleLinkedNode<T> successor = this.Successor(node);
            if (predecessor == null || successor == null)
            {
                return;
            }
            
            predecessor.Next = successor;
            successor.Last = predecessor;
            this._length--;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public DoubleLinkedNode<T> Predecessor(DoubleLinkedNode<T> node)
        {
            return node == null ? null : node.Last;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public DoubleLinkedNode<T> Successor(DoubleLinkedNode<T> node)
        {
            return node == null ? null : node.Next;
        }

        /// <summary>
        /// 
        /// </summary>
        public DoubleLinkedNode<T> Head
        {
            get
            {
                return this._nil.Next;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DoubleLinkedNode<T> Tail
        {
            get
            {
                return this._nil.Last;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            System.Console.WriteLine(this.Name);
            System.Console.WriteLine(this.Length);
            DoubleLinkedNode<T> tmp = this.Head;
            while (tmp != this._nil)
            {
                System.Console.Write(tmp.Value.ToString() + "\t");
                tmp = tmp.Next;
            }

            System.Console.WriteLine();
            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Length
        {
            get
            {
                return this._length;
            }
        }

        /// <summary>
        /// The name.
        /// </summary>
        public override string Name
        {
            get
            {
                return base.Name + " - Double Linked List ";
            }
        }    
    }
}
