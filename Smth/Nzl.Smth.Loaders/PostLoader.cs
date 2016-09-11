namespace Nzl.Smth.Loaders
{
    using System;
    using Nzl.Smth;
    using Nzl.Smth.Configs;
    using Nzl.Web.Page;
    using Nzl.Web.Util;

    /// <summary>
    /// 
    /// </summary>
    public class PostLoader
    {
        /// <summary>
        /// 
        /// </summary>
        public EventHandler Succeeded;

        /// <summary>
        /// 
        /// </summary>
        public EventHandler<MessageEventArgs> ErrorAccured;

        /// <summary>
        /// 
        /// </summary>
        public EventHandler Failed;

        /// <summary>
        /// 
        /// </summary>
        private string _successString = "成功";

        /// <summary>
        /// 
        /// </summary>
        private PageLoader _pageLoader = null;

        /// <summary>
        /// 
        /// </summary>
        PostLoader()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postData"></param>
        public PostLoader(string url)
        {
            this._pageLoader = new PageLoader(url);
            this._pageLoader.PageLoaded += PostLoader_PageLoaded;
            this._pageLoader.PageFailed += PostLoader_PageFailed;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postData"></param>
        public PostLoader(string postUrl, string postData)
        {
            this._pageLoader = new PageLoader(postUrl, postData);
            this._pageLoader.PageLoaded += PostLoader_PageLoaded;
            this._pageLoader.PageFailed += PostLoader_PageFailed;            
        }

        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            PageDispatcher.Instance.Add(this._pageLoader);
        }

        /// <summary>
        /// 
        /// </summary>
        public string SuccessString
        {
            get
            {
                return this._successString;
            }

            set
            {
                this._successString = value;
            }
        }

        #region  PageLoaded & PageFailed
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PostLoader_PageLoaded(object sender, EventArgs e)
        {
            PageLoader pl = sender as PageLoader;
            if (pl != null)
            {
                string html = pl.GetResult() as string;
                if (html == null)
                {
                    WebPage wp = pl.GetResult() as WebPage;
                    if (wp != null && wp.IsGood)
                    {
                        html = wp.Html;
                    }
                }

                string result = CommonUtil.GetMatch(@"<div id=\Wm_main\W><div class=\Wsp hl f\W>(?'Result'\w+)</div>", html, "Result");
                if (result != null)
                {
                    if (result.Contains(this.SuccessString))
                    {
                        if (this.Succeeded != null)
                        {
                            this.Succeeded(this, new EventArgs());
                        }
                    }
                    else
                    {
                        if (this.ErrorAccured != null)
                        {
                            this.ErrorAccured(this, new MessageEventArgs(result));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PostLoader_PageFailed(object sender, EventArgs e)
        {
            if (this.Failed != null)
            {
                this.Failed(this, new EventArgs());
            }
        }
        #endregion
    }
}
