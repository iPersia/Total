namespace Nzl.Web.Rss.Reader
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;
    using System.Xml;
    using Nzl.Web.Core;
    using Nzl.Web.Util;    

    /// <summary>
    /// 
    /// </summary>
    public class CnBetaRssReader : BaseRssReader
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public CnBetaRssReader()
            : base(@"CnBeta", new Uri(@"http://www.cnbeta.com/backend.php"), 60 * 1000)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="ri"></param>
        protected override void GetImageAndDescription(string html, ref string description, Dictionary<string, Image> images)
        {
            try
            {
                string guidEnter = Guid.NewGuid().ToString();
                html = html.Replace("<p>", "  ");
                html = html.Replace("</p>", guidEnter);
                html = html.Replace("<br >", guidEnter);
                html = html.Replace("<br>", guidEnter);
                html = html.Replace("<br/>", guidEnter);
                html = html.Replace("<br />", guidEnter);
                description = CommonUtil.TrimHtml(html).Replace(guidEnter, "\n").Trim('\n');
            }
            catch { }
        }
    }
}
