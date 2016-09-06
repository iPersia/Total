namespace Nzl.Smth.Loaders
{
    using System;
    using Nzl.Smth;
    using Nzl.Smth.Configs;
    using Nzl.Web.Util;

    /// <summary>
    /// 
    /// </summary>
    public class MailSender
    {
        /// <summary>
        /// 
        /// </summary>
        public EventHandler OnSucceeded;

        /// <summary>
        /// 
        /// </summary>
        public EventHandler OnFailed;

        MailSender()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postData"></param>
        public MailSender(string postData)
        {
            PageLoader pl = new PageLoader(Configuration.SendMailUrl, postData);
            pl.PageLoaded += NewMail_PageLoaded;
            pl.PageFailed += NewMail_PageFailed;
            PageDispatcher.Instance.Add(pl);
        }

        #region NewMail - PageLoaded & PageFailed
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewMail_PageLoaded(object sender, EventArgs e)
        {
            PageLoader pl = sender as PageLoader;
            if (pl != null)
            {
                string html = pl.GetResult() as string;
                string result = CommonUtil.GetMatch(@"<div id=\Wm_main\W><div class=\Wsp hl f\W>(?'Result'\w+)</div>", html, "Result");
                if (result != null && result.Contains("成功"))
                {
                    if (this.OnSucceeded != null)
                    {
                        this.OnSucceeded(this, new EventArgs());
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewMail_PageFailed(object sender, EventArgs e)
        {
            if (this.OnFailed != null)
            {
                this.OnFailed(this, new EventArgs());
            }
        }
        #endregion
    }
}
