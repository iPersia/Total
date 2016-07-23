namespace Nzl.Web.ProductClawer
{
    using System;
    using Nzl.Web.Core;
    using Nzl.Web.ProductClawer.Clawers;

    /// <summary>
    /// The product clawer factory.
    /// </summary>
    public static class ProductClawerFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctObj"></param>
        /// <returns></returns>
        public static BaseProductClawer CreateProductClawer(ProductClawerParameter ctObj)
        {
            return ProductClawerFactory.CreateProductClawer(ctObj.Name, ctObj.Uri, ctObj.Interval, ctObj.TargetPrice);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="interval"></param>
        /// <param name="targetPrice"></param>
        /// <returns></returns>
        public static BaseProductClawer CreateProductClawer(string name, string url, int interval, decimal targetPrice)
        {
            if (string.IsNullOrEmpty(url) == false)
            {
                if (url.ToUpper().Contains("360BUY.COM") || url.ToUpper().Contains("JD.COM"))
                {
                    return new The360buyClawer(name, url, interval, targetPrice);
                }

                if (url.ToUpper().Contains("51BUY.COM"))
                {
                    return new The51BuyClawer(name, url, interval, targetPrice);
                }

                if (url.ToUpper().Contains("AMAZON.CN"))
                {
                    return new TheAmazonCNClawer(name, url, interval, targetPrice);
                }

                if (url.ToUpper().Contains("AMAZON.COM"))
                {
                    return new TheAmazonUSClawer(name, url, interval, targetPrice);
                }

                if (url.ToUpper().Contains("DANGDANG.COM"))
                {
                    return new TheDangdangClawer(name, url, interval, targetPrice);
                }

                if (url.ToUpper().Contains("NEWEGG.COM"))
                {
                    return new TheNeweggClawer(name, url, interval, targetPrice);
                }

                if (url.ToUpper().Contains("YIHAODIAN.COM"))
                {
                    return new TheYiHaoDianClawer(name, url, interval, targetPrice);
                }

                if (url.ToUpper().Contains("10010.COM"))
                {
                    return new The10010Clawer(name, url, interval, targetPrice);
                }   
            }

            return null;
        }
    }
}
