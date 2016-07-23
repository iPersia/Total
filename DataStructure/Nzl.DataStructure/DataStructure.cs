namespace Nzl.DataStructure
{
    using System;

    /// <summary>
    /// The data structure base class.
    /// </summary>
    public abstract class DataStructure
    {
        /// <summary>
        /// Algorithm name.
        /// </summary>
        public virtual string Name
        {
            get
            {
                return "Data Structure";
            }
        }
    }
}
