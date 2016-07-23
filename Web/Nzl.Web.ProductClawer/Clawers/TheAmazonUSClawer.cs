namespace Nzl.Web.ProductClawer.Clawers
{
    using System;
    using Nzl.Web.Core;
    using Nzl.Web.Core.EventArgs;
    using Nzl.Web.Page;
    using Nzl.Web.Util;
    
    /// <summary>
    /// 
    /// </summary>
    internal class TheAmazonUSClawer : BaseProductClawer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="interval"></param>
        /// <param name="targetPrice"></param>
        public TheAmazonUSClawer(string name, string url, int interval, decimal targetPrice)
            : base(name, url, interval, targetPrice)
        {
            this.Verdor = "亚马逊 - 美国";
            this.Currency = Currency.USD;
        }

        /// <summary>
        /// Get price information.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override string GetPriceInformation(WebPage page, PriceClawingEventArgs e)
        {
            try
            {
                string marketInfo = page.getSpecialWord(@"Price: $\d*,\d{3}\.\d{2}\b");
                string yixunInfo = page.getSpecialWord(@"价格.{2}￥ \d*,\d{3}\.\d{2}\b");
                if (marketInfo != "" && yixunInfo != "")
                {
                    string mPrice = ProductClawerUtil.FindNumber(marketInfo);
                    string yPrice = ProductClawerUtil.FindNumber(yixunInfo);
                    e.Product.MarketPrice = System.Convert.ToDecimal(mPrice);
                    e.Product.Price = System.Convert.ToDecimal(yPrice);
                }
                else
                {
                    throw new Exception(this.Verdor + " - " + this.ClawerParam.Name + " - 提取价格失败！");
                }
            }
            catch (Exception exp)
            {
#if (DEBUG)
                CommonUtil.ShowMessage(this, exp.Message);
#endif
                return exp.Message;
            }

            return base.GetPriceInformation(page, e);
        }

        /// <summary>
        /// Get stock information.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override string GetStockInformation(WebPage page, PriceClawingEventArgs e)
        {
            e.Product.IsInStock = page.getSpecialWord(@"In Stock") != "";
            if (e.Product.IsInStock == false)
            {
                e.Product.IsInStock = page.getSpecialWord(@"in stock") != "";
            }

            return base.GetStockInformation(page, e);
        }
    }
}
