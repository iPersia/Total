namespace Nzl.Smth.Loaders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Nzl.Web.Page;
    using Nzl.Smth.Utils;

    public class AtStatus
    {
        #region Sington
        /// <summary>
        /// 
        /// </summary>
        public static readonly AtStatus Instance = new AtStatus();
        #endregion

        #region Event
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<AtStatusEventArgs> OnNewArrived;
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
                this._newCount = SmthUtil.GetNewAtCount(html);
                if (this._newCount != srcNewCount)
                {
                    AtStatusEventArgs e = new AtStatusEventArgs();
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
