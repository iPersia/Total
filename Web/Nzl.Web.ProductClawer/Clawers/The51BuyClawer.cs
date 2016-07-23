namespace Nzl.Web.ProductClawer.Clawers
{
    using System;
    using Nzl.Web.Core;
    using Nzl.Web.Core.EventArgs;
    using Nzl.Web.Page;
    using Nzl.Web.Util;
    
    
    /// <summary>
    /// The 51buy clawer.
    /// </summary>
    internal class The51BuyClawer : BaseProductClawer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="interval"></param>
        /// <param name="targetPrice"></param>
        public The51BuyClawer(string name, string url, int interval, decimal targetPrice)
            : base(name, url, interval, targetPrice)
        {
            this.Verdor = "易迅网";
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
                //string marketInfo = page.getSpecialWord(@"市场价格：&yen;\d*\.\d*");
                //string yixunInfo = page.getSpecialWord(@"易迅价格：&yen;\d*\.\d*");
                //string tuangouInfo = page.getSpecialWord(@"团购价：&yen;\d*\.\d*");
                string marketInfo = CommonUtil.GetMatch(@"市场价格：&yen;(?'Price'\d*\.\d*)", page.Context, "Price");
                string yixunInfo = CommonUtil.GetMatch(@"易迅价格： &yen;(?'Price'\d*\.\d*)", page.Context, "Price");
                string tuangouInfo = CommonUtil.GetMatch(@"团购价：&yen;(?'Price'\d*\.\d*)", page.Context, "Price");
                if (yixunInfo != "" || tuangouInfo != "")
                {
                    string mPrice = ProductClawerUtil.FindNumber(marketInfo);
                    string yPrice = ProductClawerUtil.FindNumber(yixunInfo);
                    string tPrice = ProductClawerUtil.FindNumber(tuangouInfo);
                    if (mPrice != "")
                    {
                        e.Product.MarketPrice = System.Convert.ToDecimal(mPrice);
                    }

                    if (yPrice != "")
                    {
                        e.Product.Price = System.Convert.ToDecimal(yPrice);
                    }

                    if (tPrice != "")
                    {
                        e.Product.Price = e.Product.Price > System.Convert.ToDecimal(tPrice) ? System.Convert.ToDecimal(tPrice) : e.Product.Price;
                    }
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
            e.Product.IsInStock = page.getSpecialWord(@"送 至： 有货") != "";
            return base.GetStockInformation(page, e);
        }
    }    
}
