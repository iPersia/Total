namespace Nzl.Web.Rss.Reader
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Nzl.Web.Core;    

    /// <summary>
    /// 
    /// </summary>
    public class EngadgetCNRssReader : BaseRssReader
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public EngadgetCNRssReader()
            : base(@"EngadgetCN", new Uri(@"http://cn.engadget.com/rss.xml"), 2 * 15 * 1000)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <returns></returns>
        protected override DateTime GetItemDateTime(XmlNode xmlNode)
        {
            try
            {
                DateTime dt = DateTime.Parse(xmlNode["pubDate"].InnerText.Replace("EST", "GMT"));
                return dt.AddHours(5);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Pre-processing method.
        /// 1. Trim special chars, like '\b' 0x08        
        /// 
        /// </summary>
        /// <param name="xml"></param>
        protected override void PreProcessing(ref string xml)
        {
            if (xml != null)
            {
                xml.Replace("\b", "");
            }
        }
    }
}
