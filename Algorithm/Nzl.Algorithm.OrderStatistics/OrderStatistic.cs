namespace Nzl.Algorithm.OrderStatistic
{
    using System;
    using Nzl.Algorithm;

    /// <summary>
    /// The order statistics class.
    /// </summary>
    public class OrderStatistic<T> : Algorithm
        where T : IComparable<T>
    {
        /// <summary>
        /// Algorithm name.
        /// </summary>
        public override string Name
        {
            get
            {
                return base.Name + " - OrderStatistic";
            }
        }
    }
}
