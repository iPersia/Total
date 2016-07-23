namespace Nzl.Web.Forms.MobileNewSmth.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using System.ComponentModel;
    using Nzl.Web.Page;
    using Nzl.Web.Util;
    using Nzl.Web.Forms.MobileNewSmth.Utils;
    using Nzl.Web.Forms.MobileNewSmth.Datas;

    /// <summary>
    /// 
    /// </summary>
    public class BaseForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private UrlInfo _urlInfo = new UrlInfo();

        /// <summary>
        /// 
        /// </summary>
        private System.ComponentModel.BackgroundWorker bwFetchPage;

        /// <summary>
        /// 
        /// </summary>
        public BaseForm()
            : base()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseUrl"></param>
        protected void SetBaseUrl(string baseUrl)
        {
            this._urlInfo.BaseUrl = baseUrl;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void SetUrlInfo(int index, bool isAppend)
        {
            this._urlInfo.Index = index;
            this._urlInfo.IsAppend = isAppend;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void SetUrlInfo(bool isAppend)
        {
            this._urlInfo.IsAppend = isAppend;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual string GetCurrentUrl()
        {
            return this._urlInfo.BaseUrl + "?p=" + this._urlInfo.Index;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual string GetUrl(UrlInfo info)
        {
            return info.BaseUrl + "?p=" + info.Index;
        }

        /// <summary>
        /// 
        /// </summary>
        protected bool FetchPage()
        {
            UrlInfo info = new UrlInfo(this._urlInfo);
            return FetchPage(info);
        }

        /// <summary>
        /// 
        /// </summary>
        protected bool FetchPrevPage()
        {
            UrlInfo info = new UrlInfo(this._urlInfo);
            info.Index = this._urlInfo.Index - 1;
            return FetchPage(info);
        }

        /// <summary>
        /// 
        /// </summary>
        protected bool FetchNextPage()
        {
            UrlInfo info = new UrlInfo(this._urlInfo);
            info.Index = this._urlInfo.Index + 1;
            return FetchPage(info);
        }

        /// <summary>
        /// 
        /// </summary>
        protected bool FetchLastPage()
        {
            UrlInfo info = new UrlInfo(this._urlInfo);
            info.Index = this._urlInfo.Total;
            return FetchPage(info);
        }

        /// <summary>
        /// 
        /// </summary>
        protected bool FetchPage(UrlInfo urlInfo)
        {
            if (urlInfo.Index > 0 && urlInfo.Index <= urlInfo.Total)
            {
                this.bwFetchPage = new System.ComponentModel.BackgroundWorker();
                this.bwFetchPage.DoWork += new System.ComponentModel.DoWorkEventHandler(bwFetchPage_DoWork);
                this.bwFetchPage.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(bwFetchPage_RunWorkerCompleted);
                this.bwFetchPage.RunWorkerAsync(urlInfo);
#if (DEBUG)
                Nzl.Web.Util.CommonUtil.ShowMessage(System.DateTime.Now + "\t Page index is equal to " + urlInfo.Index);
#endif
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state">State is DoWorkEventArgs!</param>
        protected void DoWorkBase(object state)
        {
            DoWorkEventArgs e = state as DoWorkEventArgs;
            try
            {                
                UrlInfo urlInfo = e.Result as UrlInfo;
                if (urlInfo != null)
                {
                    DoWork(urlInfo);
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch (Exception exp)
            {
                e.Cancel = true;

#if (DEBUG)
                CommonUtil.ShowMessage(typeof(BaseForm), exp.Message);
#endif
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        protected virtual void DoWork(UrlInfo info)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state">State is RunWorkerCompletedEventArgs!</param>
        protected void WorkerCompletedBase(object state)
        {
            RunWorkerCompletedEventArgs e = state as RunWorkerCompletedEventArgs;
            try
            {
                UrlInfo urlInfo = e.Result as UrlInfo;
                if (urlInfo != null)
                {
                    WorkerCompleted(urlInfo);
                }
            }
            catch (Exception exp)
            {                
#if (DEBUG)
                CommonUtil.ShowMessage(typeof(BaseForm), exp.Message);
#endif
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        protected virtual void WorkerCompleted(UrlInfo info)
        {
            if (info != null)
            {
                LoginForm.UpdateLoginStatus(info.WebPage);
                UpdatePageInfo(info.WebPage, info);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwFetchPage_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker bw = sender as BackgroundWorker;
                UrlInfo urlInfo = e.Argument as UrlInfo;
                string targetUrl = this.GetUrl(urlInfo);
                WebPage wp = WebPageFactory.CreateWebPage(targetUrl);
                if (wp != null && wp.IsGood)
                {
                    urlInfo.WebPage = wp;
                    e.Result = urlInfo;
                    DoWorkBase(e);

                    MessageQueue.Enqueue(MessageFactory.CreateMessage(this.GetType().ToString(), "Get webpage '" + targetUrl + "' success!"));
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch (Exception exp)
            {
                if (Program.LoggerEnabled)
                {
                    Program.Logger.Error(exp.Message);
                }

                MessageQueue.Enqueue(MessageFactory.CreateMessage(exp));
                e.Result = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwFetchPage_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                MessageBox.Show("Canceled");
            }
            else
            {
                WorkerCompletedBase(e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private void UpdatePageInfo(WebPage page, UrlInfo info)
        {
            if (page != null)
            {
                MatchCollection mtCollection = CommonUtil.GetMatchCollection(@"<a class=\Wplant\W>(?'Current'\d+)/(?'Total'\d+)</a>", page.Html);
                if (mtCollection.Count == 2)
                {
                    this._urlInfo.Index = System.Convert.ToInt32(mtCollection[0].Groups[1].Value);
                    this._urlInfo.Total = System.Convert.ToInt32(mtCollection[0].Groups[2].Value);

                    info.Index = System.Convert.ToInt32(mtCollection[0].Groups[1].Value);
                    info.Total = System.Convert.ToInt32(mtCollection[0].Groups[2].Value);
                }
            }
        }
    }
}
