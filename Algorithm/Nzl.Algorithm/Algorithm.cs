namespace Nzl.Algorithm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Base class of algorithm.
    /// </summary>
    public abstract class Algorithm
    {
        /// <summary>
        /// Algorithm name.
        /// </summary>
        public virtual string Name
        {
            get
            {
                return "Algorithm";
            }
        }
    }
}
