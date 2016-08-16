namespace Nzl.Web.Page
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public static class WebPageFactory
    {
        /// <summary>
        /// 
        /// </summary>
        private static decimal _networkFlow = 0;

        /// <summary>
        /// 
        /// </summary>
        public static decimal NetworkFlow
        {
            get
            {
                return _networkFlow;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static WebPage CreateWebPage(string url)
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                {
                    return null;
                }

                WebPage wp = new WebPage(url);
                if (wp != null && wp.Html != null)
                {
                    _networkFlow += wp.Html.Length;
                }

                return wp;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static WebPage CreateWebPage(string url, string loginurl, string post)
        {
            try
            {
                if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(loginurl) || string.IsNullOrEmpty(post))
                {
                    return null;
                }

                WebPage wp = new WebPage(url, loginurl, post);
                if (wp != null && wp.Html != null)
                {
                    _networkFlow += wp.Html.Length;
                }

                return wp;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postUrl"></param>
        /// <param name="postStr"></param>
        /// <returns></returns>
        public static string Post(string postUrl, string postStr)
        {
            try
            {
                if (string.IsNullOrEmpty(postUrl))
                {
                    return null;
                }

                return WebPage.Post(postUrl, postStr);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public static void RemoveCookie(string url)
        {
            try
            {
                WebPage.RemoveCookie(url);
            }
            catch { };
        }
    }
}
