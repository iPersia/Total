namespace Nzl.Smth.Loaders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Nzl.Web.Page;
    using Nzl.Smth.Utils;

    public class MailStatus
    {
        #region Sington
        /// <summary>
        /// 
        /// </summary>
        public static readonly MailStatus Instance = new MailStatus();
        #endregion

        #region Event
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<MailStatusEventArgs> OnNewMaiArrived;
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
                this._newCount = SmthUtil.GetNewMailCount(html);
                if (this._newCount != srcNewCount)
                {
                    MailStatusEventArgs e = new MailStatusEventArgs();
                    e.NewArrived = this._newCount > 0;
                    e.NewCount = this._newCount;
                    if (this.OnNewMaiArrived != null)
                    {
                        this.OnNewMaiArrived(this, e);
                    }
                }
            }
        }
        #endregion
    }
}
