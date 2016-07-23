namespace Nzl.Web.ProductClawer.Clawers
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    internal class TheNeweggClawer : BaseProductClawer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="interval"></param>
        /// <param name="targetPrice"></param>
        public TheNeweggClawer(string name, string url, int interval, decimal targetPrice)
            : base(name, url, interval, targetPrice)
        {
            this.Verdor = "新蛋网";
        }
    }
}
