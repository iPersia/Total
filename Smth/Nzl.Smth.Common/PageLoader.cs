namespace Nzl.Smth.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Nzl.Dispatcher;
    using Nzl.Smth.Datas;
    using Nzl.Smth.Logger;
    using Nzl.Web.Page;
    using Nzl.Smth.Utils;

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
        #region event
        /// <summary>
        /// 
        /// </summary>
        public EventHandler PageLoaded;
        #endregion

        #region variable
        /// <summary>
        /// 
        /// </summary>
        private string _url;

        private string _loginUrl;

        private string _postUrl;

        /// <summary>
        /// 
        /// </summary>
        private WebPage _webPage = null;
        #endregion

        #region Ctor.
        /// <summary>
        /// 
        /// </summary>
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
        public PageLoader(string url, string loginUrl, string postUrl)
            : this(url)
        {
            this._loginUrl = loginUrl;
            this._postUrl = postUrl;
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public object Tag
        {
            get;
            set;
        }
        #endregion

        #region Implements interfaces.
        /// <summary>
        /// 
        /// </summary>
        public bool Execute()
        {
            try
            {
                if (this._loginUrl != null && this._postUrl != null)
                {
                    this._webPage = WebPageFactory.CreateWebPage(this._url, this._loginUrl, this._postUrl);
                }
                else
                {
                    this._webPage = WebPageFactory.CreateWebPage(this._url);
                }

                if (this.PageLoaded != null)
                {
                    this.PageLoaded(this, new EventArgs());
                }

#if (DEBUG)
                MessageQueue.Enqueue(MessageFactory.CreateMessage("Page loader", "Executing PageLoader.Excute('" + this._url + "') completed!"));
#endif
                return true;
            }
            catch (Exception e)
            {
                if (Logger.Enabled)
                {
                    Logger.Instance.Error(e.Message + "\n" + e.StackTrace);
                }

#if (DEBUG)
                MessageQueue.Enqueue(MessageFactory.CreateMessage("Page loader", "Executing PageLoader.Excute('" + this._url + "') failed!"));
#endif

                return false;
            }
        }
        #endregion

        #region public
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public WebPage GetPage()
        {
            return this._webPage;
        }
        #endregion
    }
}
