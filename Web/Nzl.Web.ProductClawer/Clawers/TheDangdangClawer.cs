namespace Nzl.Web.ProductClawer.Clawers
{
    using System;
    using Nzl.Web.Core;
    using Nzl.Web.Core.EventArgs;
    using Nzl.Web.Page;
    using Nzl.Web.Util;

    /// <summary>
    /// The dangdang clawer.
    /// </summary>
    internal class TheDangdangClawer : BaseProductClawer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="interval"></param>
        /// <param name="targetPrice"></param>
        public TheDangdangClawer(string name, string url, int interval, decimal targetPrice)
            : base(name, url, interval, targetPrice)
        {
            this.Verdor = "当当网";
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
                string marketInfo = page.getSpecialWord(@"定 价： &yen;\d*\.\d*");
                string yixunInfo = page.getSpecialWord(@"当 当 价：&yen;\d*\.\d*");
                if (marketInfo != "" && yixunInfo != "")
                {
                    string mPrice = ProductClawerUtil.FindNumber(marketInfo);
                    string yPrice = ProductClawerUtil.FindNumber(yixunInfo);
                    e.Product.MarketPrice = System.Convert.ToDecimal(mPrice);
                    e.Product.Price = System.Convert.ToDecimal(yPrice);
                }
                else
                {
                    throw new Exception("提取价格失败！");
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
            e.Product.IsInStock = page.getSpecialWordFromHtml("title=\"购买\"") != "";
            return base.GetStockInformation(page, e);
        }
    }
}

