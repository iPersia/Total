namespace Nzl.Web.Rss.Reader
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;
    using Nzl.Web.Core;
    using Nzl.Web.Util;

    /// <summary>
    /// 
    /// </summary>
    public class SmzdmRssReader : BaseRssReader
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public SmzdmRssReader()
            : base(@"Smzdm", new Uri(@"http://feed.smzdm.com"), 60 * 1000)
        {
            this._guidXmlToken = "link";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <returns></returns>
        protected override string GetItemTitle(XmlNode xmlNode)
        {
            string title = base.GetItemTitle(xmlNode);
            if (title != null)
            {
                title = title.Replace("&amp;", "&&");
            }

            return title;
        }
    }
}
