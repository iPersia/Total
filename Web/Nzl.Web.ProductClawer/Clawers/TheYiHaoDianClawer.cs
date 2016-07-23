namespace Nzl.Web.ProductClawer.Clawers
{
    using System;
    using Nzl.Web.Core;
    using Nzl.Web.Core.EventArgs;
    using Nzl.Web.Page;
    using Nzl.Web.Util;

    /// <summary>
    /// The yihaodian clawer class.
    /// </summary>
    internal class TheYiHaoDianClawer : BaseProductClawer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="interval"></param>
        /// <param name="targetPrice"></param>
        public TheYiHaoDianClawer(string name, string url, int interval, decimal targetPrice)
            : base(name, url, interval, targetPrice)
        {
            this.Verdor = "一号店";
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
                string ipriceInfo = page.getSpecialWordFromHtml(@"\b1号店价 ￥\d*\.?\d{1}?\b");
                string mpriceInfo = page.getSpecialWord(@"\b市场价[\D, ：, ¥, ￥]*\d*\.?\d{1}?\b");
                if (mpriceInfo != "" && ipriceInfo != "")
                {
                    ipriceInfo = ipriceInfo.Replace("1号店价", "价格");
                    string mPrice = ProductClawerUtil.FindNumber(mpriceInfo);
                    string iPrice = ProductClawerUtil.FindNumber(ipriceInfo);
                    e.Product.MarketPrice = System.Convert.ToDecimal(mPrice);
                    e.Product.Price = System.Convert.ToDecimal(iPrice);
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
            e.Product.IsInStock = page.getSpecialWord(@"现货") != "";
            return base.GetStockInformation(page, e);
        }
    }
}
