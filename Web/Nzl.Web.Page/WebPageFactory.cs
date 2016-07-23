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

                return new WebPage(url);
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

                return new WebPage(url, loginurl, post);
            }
            catch
            {
                return null;
            }
        }
    }
}
