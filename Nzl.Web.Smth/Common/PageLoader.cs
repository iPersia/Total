namespace Nzl.Web.Smth.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Nzl.Dispatcher;
    using Datas;
    using Page;
    using Utils;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public delegate void PageLoadedCallback(UrlInfo info);

    /// <summary>
    /// 
    /// </summary>
    public class PageLoader : IExecute
    {
        /// <summary>
        /// 
        /// </summary>
        public EventHandler PageLoaded;

        /// <summary>
        /// 
        /// </summary>
        private string _url;

        /// <summary>
        /// 
        /// </summary>
        private WebPage _webPage = null;

        PageLoader()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public PageLoader(string url)
            : this()
        {
            this._url = url;
        }

        /// <summary>
        /// 
        /// </summary>
        public object Tag
        {
            get;
            set;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public bool Execute()
        {
            try
            {
                this._webPage = WebPageFactory.CreateWebPage(this._url);
                if (this.PageLoaded != null)
                {
                    this.PageLoaded(this, new EventArgs());
                }

#if (DEBUG)
                MessageQueue.Enqueue(MessageFactory.CreateMessage("Page loader", "Getting '" + this._url + "' completed!"));
#endif
                return true;
            }
            catch
            {
#if (DEBUG)
                MessageQueue.Enqueue(MessageFactory.CreateMessage("Page loader", "Getting '" + this._url + "' failed!"));
#endif
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public WebPage GetPage()
        {
            return this._webPage;
        }
    }
}
