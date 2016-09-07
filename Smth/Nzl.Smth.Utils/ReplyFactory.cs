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
    public static class ReplyFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static IList<Reply> CreateReplies(WebPage wp)
        {
            if (wp != null && wp.IsGood)
            {
                return CreateReplies(wp.Html);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        private static IList<Reply> CreateReplies(string html)
        {
            string pattern = @"<li( class=\Whla\W)?><div><a href=\W"
                           + @"(?'Url'/refer/reply/read\?index=\d+)\W"
                           + @"(?'IsNew' class=\Wtop\W)?>"
                           + @"(?'Title'[^<]*)</a></div><div><a href=\W"
                           + @"(?'DeleteUrl'/refer/reply/delete\?index=\d+)\W>删除</a>"
                           + @"(?'DateTime'[^<]*)<a href=\W/user/query/"
                           + @"(?'Author'[a-zA-z][a-zA-Z0-9]{1,11})\W>[a-zA-z][a-zA-Z0-9]{1,11}</a></div></li>";

            MatchCollection mtMailCollection = Nzl.Web.Util.CommonUtil.GetMatchCollection(pattern, html);
            if (mtMailCollection != null)
            {
                IList<Reply> referList = new List<Reply>();
                foreach (Match mt in mtMailCollection)
                {
                    Reply refer = RecycledQueues.GetRecycled<Reply>();
                    if (refer == null)
                    {
                        refer = new Reply();
                    }

                    refer.Url = Configuration.BaseUrl + mt.Groups["Url"].Value.ToString();
                    refer.DeleteUrl = Configuration.BaseUrl + mt.Groups["DeleteUrl"].Value.ToString();
                    refer.Title = mt.Groups["Title"].Value.ToString();
                    refer.Author = mt.Groups["Author"].Value.ToString();
                    refer.DateTime = mt.Groups["DateTime"].Value.ToString().Replace("&nbsp;", "");
                    refer.IsNew = !string.IsNullOrEmpty(mt.Groups["IsNew"].Value.ToString());
                    referList.Add(refer);
                }

                return referList;
            }

            return null;
        }       
    }
}

