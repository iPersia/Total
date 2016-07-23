namespace Nzl.Web.Core
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class Product
    {
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
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Vendor
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal MarketPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal Price
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsInStock
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Uri
        {
            get;
            set;
        }

        public string QuickUri
        {
            get;
            set;
        }
    }
}
