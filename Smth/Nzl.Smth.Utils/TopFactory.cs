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
    public static class TopFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static IList<Top> CreateTops(WebPage page)
        {
            if (page != null && page.IsGood)
            {
                return CreateTops(page.Html);
            }

            return null;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private static IList<Top> CreateTops(string html)
        {
            MatchCollection mtCollection = CommonUtil.GetMatchCollection(@"(?'Sequence'\d{1,2})\|<a href=\W(?'Url'/article/(?'Board'[\w, %2E, %5F]+)/(?'Index'\d+))\W>(?'Title'[^<]+)(\(<span\s+style=\Wcolor:red\W>(?'Replies'\d+)</span>\))?</a></li>", html);
            IList<Top> topList = new List<Top>();
            foreach (Match mt in mtCollection)
            {
                if (mt.Success)
                {
                    Top top = RecycledQueues.GetRecycled<Top>();
                    if (top == null)
                    {
                        top = new Top();
                    }

                    top.Sequence = System.Convert.ToInt32(mt.Groups["Sequence"].Value);
                    top.Uri = Configuration.BaseUrl + mt.Groups["Url"].ToString();
                    top.Board = mt.Groups["Board"].ToString().Replace("%5F", "_").Replace("%2E", ".");
                    top.Index = mt.Groups["Index"].ToString();
                    top.Title = CommonUtil.ReplaceSpecialChars(mt.Groups["Title"].ToString());
                    top.Replies = mt.Groups["Replies"].Value.ToString() == "" ? 0 : System.Convert.ToInt32(mt.Groups["Replies"].Value);
                    topList.Add(top);
                }
            }

            return topList;
        }
    }
}
