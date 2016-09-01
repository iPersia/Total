namespace Nzl.Recycling
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public interface IRecycled
    {
        /// <summary>
        /// A boolean indicated whether the object is recycled.
        /// </summary>
        bool IsRecycled
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        RecycledStatus Status
        {
            get;
            set;
        }
        
        /// <summary>
        /// 
        /// </summary>
        void Reusing();
        
        /// <summary>
        /// 
        /// </summary>
        void Recycling();
    }
}
