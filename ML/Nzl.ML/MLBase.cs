namespace Nzl.ML
{
    using System;

    /// <summary>
    /// The base class of machine learning algorithm.
    /// </summary>
    public abstract class MLBase
    {
        /// <summary>
        /// Algorithm name.
        /// </summary>
        public virtual string Name
        {
            get
            {
                return "ML";
            }
        }
    }
}
