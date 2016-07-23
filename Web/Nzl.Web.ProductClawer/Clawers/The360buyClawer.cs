namespace Nzl.Web.ProductClawer.Clawers
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Text.RegularExpressions;    
    using Nzl.Web.Core;
    using Nzl.Web.Core.EventArgs;
    using Nzl.Web.Page;
    using Nzl.Web.Util;

    /// <summary>
    /// The 360buy clawer.
    /// </summary>
    internal class The360buyClawer : BaseProductClawer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="interval"></param>
        /// <param name="targetPrice"></param>
        public The360buyClawer(string name, string url, int interval, decimal targetPrice)
            : base(name, url, interval, targetPrice)
        {
            this.Verdor = "京东商城";
            this.NeedWebPage = false;
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
                string id = CommonUtil.GetMatch(@"http://item\.jd\.com/(?'ID'\d+)\.html", this.ClawerParam.Uri, "ID");
                //string id = CommonUtil.GetMatch(@"skuid: (?'ID'\d+),", page.Html, "ID");
                if (string.IsNullOrEmpty(id) == false)
                {
                    System.Net.WebClient wc = new System.Net.WebClient();
                    byte[] temp = wc.DownloadData(@"http://p.3.cn/prices/get?skuid=J_" + id + @"&type=1&area=1_72_4137&callback=cnp");
                    string price = CommonUtil.GetMatch(@"p\W:\W(?'Price'\d+\.\d+)", System.Text.Encoding.Default.GetString(temp), "Price");
                    if (string.IsNullOrEmpty(price) == false)
                    {
                        e.Product.Price = System.Convert.ToDecimal(price);
                        e.Product.QuickUri = @"http://cart.jd.com/cart/dynamic/easyBuy.action?pid=" + id + @"&pcount=1&ptype=1";
                    }
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
        /// Get price information.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected string GetPriceInformation_V1(WebPage page, PriceClawingEventArgs e)
        {
            try
            {
                string id = this.GetProductID(page.Html);
                if (string.IsNullOrEmpty(id))
                {
                    string priceImageUrl = page.getSpecialWordFromHtml(@"http://jprice.360buyimg.com/price/\S+\.png");
                    e.Product.Price = The360buyPriceImageReader.GetPrice(priceImageUrl);
                }
                else
                {
                    System.Net.WebClient wc = new System.Net.WebClient();
                    byte[] temp = wc.DownloadData(@"http://p.3.cn/prices/get?skuid=J_" + id);
                    string price = CommonUtil.GetMatch(@"\Wp\W:\W(?'Price'\d+\.\d+)\W",  System.Text.Encoding.Default.GetString(temp), "Price");
                    if (string.IsNullOrEmpty(price) == false)
                    {
                        e.Product.Price = System.Convert.ToDecimal(price);
                    }
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
            //e.Product.IsInStock = page.getSpecialWord(@"有货") != "";
            return base.GetStockInformation(page, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private string GetProductID(string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                return string.Empty;
            }

            return CommonUtil.GetMatch(@"<div class=\Wdd\W><span>(?'ID'\d+)</span></div></li>", html, "ID");
        }
    }
}
