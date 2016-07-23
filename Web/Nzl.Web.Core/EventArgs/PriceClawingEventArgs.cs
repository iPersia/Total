namespace Nzl.Web.Core.EventArgs
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class PriceClawingEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public Product Product
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsUpdated
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Flag
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal NetworkFlow
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Message
        {
            get;
            set;
        }
    }
}
