namespace Nzl.Smth.Loaders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Nzl.Web.Page;
    using Nzl.Smth.Utils;

    public class ReplyStatus
    {
        #region Sington
        /// <summary>
        /// 
        /// </summary>
        public static readonly ReplyStatus Instance = new ReplyStatus();
        #endregion

        #region Event
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<ReplyStatusEventArgs> OnNewArrived;
        #endregion

        #region variable
        /// <summary>
        /// 
        /// </summary>
        private int _newCount = 0;

        /// <summary>
        /// 
        /// </summary>
        private object _objLocker = new object();
        #endregion

        #region updating
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        public void UpdateStatus(WebPage page)
        {
            this.UpdateStatus(page != null ? page.Html : "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        public void UpdateStatus(string html)
        {
            lock (_objLocker)
            {
                int srcNewCount = this._newCount;
                this._newCount = SmthUtil.GetNewReplyCount(html);
                if (this._newCount != srcNewCount)
                {
                    ReplyStatusEventArgs e = new ReplyStatusEventArgs();
                    e.HasNewArrived = this._newCount > 0;
                    e.NewArrivedCount = this._newCount;
                    if (this.OnNewArrived != null)
                    {
                        this.OnNewArrived(this, e);
                    }
                }
            }
        }
        #endregion
    }
}
