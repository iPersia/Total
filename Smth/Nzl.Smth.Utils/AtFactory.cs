namespace Nzl.Smth.Utils
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Nzl.Recycling;
    using Nzl.Smth.Configs;
    using Nzl.Smth.Datas;
    using Nzl.Web.Page;
    using Nzl.Web.Util;

    /// <summary>
    /// 
    /// </summary>
    public static class AtFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static IList<At> CreateAts(WebPage wp)
        {
            if (wp != null && wp.IsGood)
            {
                return CreateAts(wp.Html);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        private static IList<At> CreateAts(string html)
        {
            string pattern = @"<li( class=\Whla\W)?><div><a href=\W"
                           + @"(?'Url'/refer/at/read\?index=\d+)\W"
                           + @"(?'IsNew' class=\Wtop\W)?>"
                           + @"(?'Title'[^<]*)</a></div><div><a href=\W"
                           + @"(?'DeleteUrl'/refer/at/delete\?index=\d+)\W>删除</a>"
                           + @"(?'DateTime'[^<]*)<a href=\W/user/query/"
                           + @"(?'Author'[a-zA-z][a-zA-Z0-9]{1,11})\W>[a-zA-z][a-zA-Z0-9]{1,11}</a></div></li>";

            MatchCollection mtMailCollection = Nzl.Web.Util.CommonUtil.GetMatchCollection(pattern, html);
            if (mtMailCollection != null)
            {
                IList<At> atList = new List<At>();
                foreach (Match mt in mtMailCollection)
                {
                    At at = RecycledQueues.GetRecycled<At>();
                    if (at == null)
                    {
                        at = new At();
                    }

                    at.Url = Configuration.BaseUrl + mt.Groups["Url"].Value.ToString();
                    at.DeleteUrl = Configuration.BaseUrl + mt.Groups["DeleteUrl"].Value.ToString();
                    at.Title = mt.Groups["Title"].Value.ToString();
                    at.Author = mt.Groups["Author"].Value.ToString();
                    at.DateTime = mt.Groups["DateTime"].Value.ToString().Replace("&nbsp;", "");
                    at.IsNew = !string.IsNullOrEmpty(mt.Groups["IsNew"].Value.ToString());
                    atList.Add(at);
                }

                return atList;
            }

            return null;
        }
    }
}

