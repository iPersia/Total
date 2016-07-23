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
    public class TechWebRssReader : BaseRssReader
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public TechWebRssReader()
            : base(@"TechWeb", new Uri(@"http://www.techweb.com.cn/rss/focus.xml"), 1 * 30 * 1000)
        {
        }
    }
}
