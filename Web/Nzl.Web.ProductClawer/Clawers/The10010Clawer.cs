namespace Nzl.Web.ProductClawer.Clawers
{
    using System;
    using Nzl.Web.Core;
    using Nzl.Web.Core.EventArgs;
    using Nzl.Web.Page;
    using Nzl.Web.Util;

    internal class The10010Clawer : BaseProductClawer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="interval"></param>
        /// <param name="targetPrice"></param>
        public The10010Clawer(string name, string url, int interval, decimal targetPrice)
            : base(name, url, interval, targetPrice)
        {
            this.Verdor = "中国联通";
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
                string marketInfo = CommonUtil.GetMatch(@"价 格： ¥ (?'Price'\d*\.\d*)", page.Context, "Price");
                string yixunInfo = CommonUtil.GetMatch(@"价 格： ¥ (?'Price'\d*\.\d*)", page.Context, "Price");
                string tuangouInfo = CommonUtil.GetMatch(@"价 格： ¥ (?'Price'\d*\.\d*)", page.Context, "Price");
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
            e.Product.IsInStock = page.getSpecialWord(@"库 存： 有货") != "";
            return base.GetStockInformation(page, e);
        }
    }
}
