namespace Nzl.Web.Rss.Reader
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;

    /// <summary>
    /// 
    /// </summary>
    public class WeiPhoneRssReader : BaseRssReader
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public WeiPhoneRssReader()
            : base(@"WeiPhone", new Uri(@"http://www.feng.com/rss.xml"), 2 * 60 * 1000)
        {
            this._autoLoad = false;
        }
    }
}
