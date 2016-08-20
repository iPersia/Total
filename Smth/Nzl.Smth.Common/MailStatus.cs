namespace Nzl.Smth.Datas
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
        public event EventHandler<MailStatusEventArgs> LoginStatusChanged;
        #endregion
    }
}
