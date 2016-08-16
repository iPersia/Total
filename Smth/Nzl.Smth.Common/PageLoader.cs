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

        /// <summary>
        /// 
        /// </summary>
        private string _postUrl;

        /// <summary>
        /// 
        /// </summary>
        private string _postStr;

        /// <summary>
        /// 
        /// </summary>
        private object _result;
        #endregion

        #region Ctor.
        /// <summary>
        /// 
        /// </summary>
        PageLoader()
        {

        }

        /// <summary>
        /// For getting WebPage.
        /// </summary>
        public PageLoader(string url)
            : this()
        {
            this._url = url;
        }

        /// <summary>
        /// For posting data.
        /// </summary>
        public PageLoader(string postUrl, string postStr)
        {
            this._postUrl = postUrl;
            this._postStr = postStr;
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
                ///Get page by post.
                if (this._postUrl != null)
                {
                    this._result = WebPageFactory.Post(this._postUrl, this._postStr);
                }
                else
                {
                    this._result = WebPageFactory.CreateWebPage(this._url);
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
        public object GetResult()
        {
            return this._result;
        }
        #endregion
    }
}
