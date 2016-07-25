namespace Nzl.Web.Smth.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;
    using System.Text.RegularExpressions;
    using Nzl.Web.Smth.Datas;
    using Nzl.Web.Smth.Interfaces;
    using Nzl.Web.Page;
    using Nzl.Web.Util;
    
    /// <summary>
    /// 
    /// </summary>
    public static class TopicFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static IList<Topic> GetTop10Topics(WebPage page)
        {
            if (page != null && page.IsGood)
            {
                return GetTop10Topics(page.Html);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static IList<Topic> GetTopics(WebPage page)
        {
            if (page != null && page.IsGood)
            {
                IList<string> targetList = CommonUtil.GetMatchList(@"(<li>|<li class=\Whla\W>)<div><a href=\W/article/[\w, %2E, %5F, \., _]+/\d+\W(| class=\W\w+\W)>", page.Html);
                if (targetList != null && targetList.Count > 0)
                {
                    string html = page.Html;
                    IList<Topic> topicList = new List<Topic>();
                    foreach (string target in targetList)
                    {
                        int startPos = html.IndexOf(target);
                        int endPos = html.IndexOf(@"</li>");
                        string content = html.Substring(startPos, endPos + @"</li>".Length - startPos);
                        html = html.Substring(endPos + @"</li>".Length);
                        Topic topic = GetTopic(content);
                        if (topic != null)
                        {
                            topicList.Add(topic);
                        }
                    }

                    return topicList;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        private static Topic GetTopic(string content)
        {
            string pattern = @"(<li>|<li class=\Whla\W>)<div><a href=\W"
                           + @"(?'TopicUrl'/article/(?'Board'[\w, %2E, %5F]+)/(?'Index'\d+))\W(| "
                           + @"(?'IsTop'class=\W\w+\W))>"
                           + @"(?'Title'[\w, \W]+)</a>\("
                           + @"(?'Replies'\d+)\)</div><div>"
                           + @"(?'CreateDateTime'[\d, \:, \-, \s]+)<a href=\W/user/query/\w+\.?\W>"
                           + @"(?'CreateID'\w+)\.?</a>\|"
                           + @"(?'LastThreadDateTime'[\d, \:, \-, \s]+)<a href=\W/user/query/\w+\.?\W>"
                           + @"(?'LastThreadID'\w+)\.?</a></div></li>";

            Topic topic = new Topic();
            content = content.Replace("&nbsp;", " ");
            topic.Uri = @"http://m.newsmth.net" + CommonUtil.GetMatch(pattern, content, "TopicUrl");
            topic.Board = CommonUtil.GetMatch(pattern, content, "Board");
            topic.Index = CommonUtil.GetMatch(pattern, content, "Index");
            topic.IsTop = !String.IsNullOrEmpty(CommonUtil.GetMatch(pattern, content, "IsTop"));
            topic.Title = CommonUtil.GetMatch(pattern, content, "Title");
            topic.Replies = System.Convert.ToInt32(CommonUtil.GetMatch(pattern, content, "Replies"));
            topic.CreateDateTime = CommonUtil.GetMatch(pattern, content, "CreateDateTime");
            topic.CreateID = CommonUtil.GetMatch(pattern, content, "CreateID");
            topic.LastThreadDateTime = CommonUtil.GetMatch(pattern, content, "LastThreadDateTime");
            topic.LastThreadID = CommonUtil.GetMatch(pattern, content, "LastThreadID");
            return topic;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private static IList<Topic> GetTop10Topics(string html)
        {
            MatchCollection mtCollection = CommonUtil.GetMatchCollection(@"(?'TopSeq'\d{1,2})\|<a href=\W(?'Url'/article/(?'Board'[\w, %2E, %5F]+)/(?'Index'\d+))\W>(?'Title'[^<]+)(\(<span\s+style=\Wcolor:red\W>(?'Replies'\d+)</span>\))?</a></li>", html);
            IList<Topic> topicList = new List<Topic>();
            foreach (Match mt in mtCollection)
            {
                if (mt.Success)
                {
                    Topic topic = new Topic();
                    topic.TopSeq = System.Convert.ToInt32(mt.Groups["TopSeq"].Value);
                    topic.Uri = @"http://m.newsmth.net" + mt.Groups["Url"].ToString();
                    topic.Board = mt.Groups["Board"].ToString().Replace("%5F", "_").Replace("%2E", ".");
                    topic.Index = mt.Groups["Index"].ToString();
                    topic.Title = CommonUtil.ReplaceSpecialChars(mt.Groups["Title"].ToString());
                    topic.Replies = mt.Groups["Replies"].Value.ToString() == "" ? 0 : System.Convert.ToInt32(mt.Groups["Replies"].Value);
                    topicList.Add(topic);
                }
            }

            return topicList;
        }
    }
}
