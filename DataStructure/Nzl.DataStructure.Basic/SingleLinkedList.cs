namespace Nzl.DataStructure.Basic
{
    using System;
    using Nzl.DataStructure;

    /// <summary>
    /// The single linked list class.
    /// </summary>
    public class SingleLinkedList<T> : LinkedList<T>
        where T : IComparable<T>, new()
    {
        /// <summary>
        /// 
        /// </summary>
        private SingleLinkedNode<T> _nil = new SingleLinkedNode<T>(new T(), "NIL");

        /// <summary>
        /// 
        /// </summary>
        private int _length;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public SingleLinkedList()
        {
            this._nil.Next = this._nil;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public SingleLinkedNode<T> Find(T key)
        {
            SingleLinkedNode<T> target = this.Head;
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
        public void Add(SingleLinkedNode<T> node)
        {
            if (node == null)
            {
                return;
            }

            SingleLinkedNode<T> last = this.Head;
            while (last.Next != this._nil)
            {
                last = last.Next;
            }

            node.Next = last.Next;
            last.Next = node;
            this._length++;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public void Delete(SingleLinkedNode<T> node)
        {
            SingleLinkedNode<T> predecessor = this.Predecessor(node);
            if (predecessor == null)
            {
                return;
            }

            predecessor.Next = predecessor.Next.Next;
            this._length--;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public SingleLinkedNode<T> Predecessor(SingleLinkedNode<T> node)
        {
            if (node == null)
            {
                return null;
            }

            SingleLinkedNode<T> target = node.Next;
            while (target != null && target != node)
            {
                if (target.Next == node)
                {
                    return target;
                }

                target = target.Next;
            }

            if (node == this.Head)
            {
                return this._nil;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public SingleLinkedNode<T> Successor(SingleLinkedNode<T> node)
        {
            return node.Next;
        }

        /// <summary>
        /// 
        /// </summary>
        public SingleLinkedNode<T> Head
        {
            get
            {
                return this._nil.Next;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            System.Console.WriteLine(this.Name);
            System.Console.WriteLine(this._length);
            SingleLinkedNode<T> tmp = this.Head;
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
                return base.Name + " - Single Linked List ";
            }
        }
    }
}
