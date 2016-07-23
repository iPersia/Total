namespace Nzl.DataStructure.Basic
{
    using System;
    using Nzl.DataStructure;

    /// <summary>
    /// The linked list base class.
    /// </summary>
    public class LinkedList<T> : DataStructure        
    {
        /// <summary>
        /// The name.
        /// </summary>
        public override string Name
        {
            get
            {
                return base.Name + " - Linked List ";
            }
        }    
    }
}
