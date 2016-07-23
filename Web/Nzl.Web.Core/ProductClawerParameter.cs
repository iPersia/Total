namespace Nzl.Web.Core
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class ProductClawerParameter
    {
        /// <summary>
        /// 
        /// </summary>
        public ProductClawerParameter()
        {
            this.Interval = 2000;
            this.Name = "UnKnown";
        }

        /// <summary>
        /// 
        /// </summary>
        public ProductClawerParameter(string url)
            : this()
        {
            this.Uri = url;
        }

        /// <summary>
        /// 
        /// </summary>
        public ProductClawerParameter(string url, decimal targetPrice)
            : this(url)
        {
            this.TargetPrice = targetPrice;
        }

        /// <summary>
        /// 
        /// </summary>
        public ProductClawerParameter(string url, int interval, decimal targetPrice)
            : this(url, targetPrice)
        {
            this.Interval = interval;
        }

        /// <summary>
        /// 
        /// </summary>
        public ProductClawerParameter(string name, string url, int interval, decimal targetPrice)
            : this(url, interval, targetPrice)
        {
            this.Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Uri
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Interval
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal TargetPrice
        {
            get;
            set;
        }
    }
}
