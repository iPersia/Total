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
    public class SmzdmFxRssReader : BaseRssReader
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public SmzdmFxRssReader()
            : base(@"SmzdmFx", new Uri(@"http://fx.smzdm.com/feed"), 30 * 1000)
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