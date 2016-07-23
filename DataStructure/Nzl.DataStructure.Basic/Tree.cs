namespace Nzl.DataStructure.Basic
{
    using System;
    using Nzl.DataStructure;

    /// <summary>
    /// The rooted tree class.
    /// </summary>
    public abstract class Tree<T> : DataStructure
        where T : IComparable<T>, new()
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual TreeNode<T> Root
        {
            get;
            set;
        }

        /// <summary>
        /// The name.
        /// </summary>
        public override string Name
        {
            get
            {
                return base.Name + " - Tree";
            }
        }    
    }
}
