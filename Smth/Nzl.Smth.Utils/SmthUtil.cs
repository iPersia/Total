namespace Nzl.Smth.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Nzl.Web.Page;
    using Nzl.Web.Util;
    using Nzl.Smth.Datas;
    using Nzl.Smth.Logger;

    /// <summary>
    /// 
    /// </summary>
    public static class SmthUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectionCode"></param>
        /// <returns></returns>
        public static string GetSectionUrl(string sectionCode)
        {
            return @"http://m.newsmth.net/section/" + sectionCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="boardCode"></param>
        /// <returns></returns>
        public static string GetBoardUrl(string boardCode)
        {
            return GetBoardUrl(boardCode, TopicBrowserType.Subject);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="boardCode"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetBoardUrl(string boardCode, TopicBrowserType type)
        {
            switch (type)
            {
                case TopicBrowserType.Classic:
                    return @"http://m.newsmth.net/board/" + boardCode + "/0";
                case TopicBrowserType.Digest:
                    return @"http://m.newsmth.net/board/" + boardCode + "/1";
                case TopicBrowserType.Subject:
                    return @"http://m.newsmth.net/board/" + boardCode + "/2";
                case TopicBrowserType.Reserved:
                    return @"http://m.newsmth.net/board/" + boardCode + "/3";
                default:
                    return @"http://m.newsmth.net/board/" + boardCode;
            }

            return @"http://m.newsmth.net/board/" + boardCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool GetLogInStatus(WebPage wp)
        {
            return GetLogInStatus(wp != null ? wp.Html : "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetLogInUserID(WebPage wp)
        {
            return GetLogInUserID(wp != null ? wp.Html : "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool GetLogInStatus(string html)
        {
            return !string.IsNullOrEmpty(CommonUtil.GetMatch(@"<a href=\W/user/logout\W accesskey=\W9\W>注销\([a-zA-z][a-zA-Z0-9]{1,11}\)</a>", html));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetLogInUserID(string html)
        {
            return CommonUtil.GetMatch(@"<a href=\W/user/logout\W accesskey=\W9\W>注销\((?'UserID'[a-zA-z][a-zA-Z0-9]{1,11})\)</a>", html, "UserID");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int GetNewMailCount(string html)
        {
            if (string.IsNullOrEmpty(html) == false)
            {
                string pattern = @"(<li>|<li class=\Whla\W>)\s*"
                               + @"(?'Index'[1-9]0?)\.<a href=\W"
                               + @"(?'MailUrl'/mail/[a-z]+/\d+)\W"
                               + @"(?'IsNew'(\Wclass=\Wtop\W))>"
                               + @"(?'MailTitle'[^<]+)</a><br\s*/><a href=\W/user/query/"
                               + @"(?'Author'[a-zA-z][a-zA-Z0-9]{1,11})(\.)?\W>[a-zA-z][a-zA-Z0-9]{1,11}(\.)?</a>\|"
                               + @"(?'DateTime'[0-9,\-]{10}\s[0-9,\:]{8})</li>";

                MatchCollection mc = CommonUtil.GetMatchCollection(pattern, html);
                return mc == null ? 0 : mc.Count;
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int GetNewAtCount(string html)
        {
            if (string.IsNullOrEmpty(html) == false)
            {
                string pattern = @"<li( class=\Whla\W)?><div><a href=\W"
                               + @"(?'Url'/refer/at/read\?index=\d+)\W"
                               + @"(?'IsNew' class=\Wtop\W)>"
                               + @"(?'Title'[^<]*)</a></div><div><a href=\W"
                               + @"(?'DeleteUrl'/refer/at/delete\?index=\d+)\W>删除</a>"
                               + @"(?'DateTime'[^<]*)<a href=\W/user/query/"
                               + @"(?'Author'[a-zA-z][a-zA-Z0-9]{1,11})\W>[a-zA-z][a-zA-Z0-9]{1,11}</a></div></li>";

                MatchCollection mc = CommonUtil.GetMatchCollection(pattern, html);
                return mc == null ? 0 : mc.Count;
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int GetNewReplyCount(string html)
        {
            if (string.IsNullOrEmpty(html) == false)
            {
                string pattern = @"<li( class=\Whla\W)?><div><a href=\W"
                               + @"(?'Url'/refer/reply/read\?index=\d+)\W"
                               + @"(?'IsNew' class=\Wtop\W)>"
                               + @"(?'Title'[^<]*)</a></div><div><a href=\W"
                               + @"(?'DeleteUrl'/refer/reply/delete\?index=\d+)\W>删除</a>"
                               + @"(?'DateTime'[^<]*)<a href=\W/user/query/"
                               + @"(?'Author'[a-zA-z][a-zA-Z0-9]{1,11})\W>[a-zA-z][a-zA-Z0-9]{1,11}</a></div></li>";

                MatchCollection mc = CommonUtil.GetMatchCollection(pattern, html);
                return mc == null ? 0 : mc.Count;
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wp"></param>
        /// <returns></returns>
        public static string GetBoard(WebPage wp)
        {
            return CommonUtil.GetMatch("<div class=\"menu sp\"><a href=\"/\" accesskey=\"0\">首页</a>\\|版面-(?'Region'.+)</div><div id=\"m_main\">", wp.Html, 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wp"></param>
        /// <returns></returns>
        public static string GetTopic(WebPage wp)
        {
            if (wp != null && wp.IsGood)
            {
                string html = wp.Html;
                string starter = "<li class=\"f\">主题:";
                int pos = html.IndexOf(starter);
                html = html.Substring(pos + starter.Length);
                pos = html.IndexOf("</li>");
                if (pos > 0)
                {
                    return CommonUtil.ReplaceSpecialChars(html.Substring(0, pos));
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string GetReplyContent(Thread thread)
        {
            if (thread != null)
            {
                Regex regex = new Regex(@"\s*--\sFROM\s[\d, \., \*]+");
                string content = regex.Replace(CommonUtil.ReplaceSpecialChars(thread.Tag.ToString()), "");

                //删除上层回复
                regex = new Regex(@"【\s在\s.+\s的大作中提到\: 】");
                content = TrimUrls(CommonUtil.ReplaceSpecialChars(content));
                //content = new Regex(@"(?m)<a[^>]*>(\w|\W)*?</a[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(content, "");
                MatchCollection mtCollection = regex.Matches(content);
                if (mtCollection != null && mtCollection.Count > 0)
                {
                    content = content.Substring(0, content.IndexOf(mtCollection[0].Groups[0].Value.ToString()));
                }

                //替换多次换行
                content = new Regex(@"[\n]+", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(content, "\n");
                content = new Regex(@"[\r]+", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(content, "\n");

                //删除尾部换行
                content = content.TrimEnd('\n');

                //保留一定的回复行数 （saveLastReplyLineCount + 1）
                mtCollection = new Regex(@"[\n]+", RegexOptions.Multiline | RegexOptions.IgnoreCase).Matches(content);
                int saveLastReplyLineCount = 5;
                string tail = string.Empty;
                string head = "\n\n【 在 " + thread.User + " 的大作中提到: 】\n: ";
                if (mtCollection != null && mtCollection.Count > saveLastReplyLineCount)
                {
                    for (int i = mtCollection.Count; i > saveLastReplyLineCount; i--)
                    {
                        content = content.Substring(0, content.LastIndexOf("\n"));
                    }

                    tail += "\n: ...................";
                }

                //添加回复头和尾
                content = head + content.Replace("\n", "\n: ") + tail;
                return content;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string GetReplyContent(string user, string html)
        {
            if (html != null && user != null)
            {
                html = ThreadFactory.TrimHtmlTag(html);

                Regex regex = new Regex(@"\s*--\sFROM\s[\d, \., \*]+");
                string content = regex.Replace(CommonUtil.ReplaceSpecialChars(html), "");

                //删除上层回复
                regex = new Regex(@"【\s在\s.+\s的大作中提到\: 】");
                content = TrimUrls(CommonUtil.ReplaceSpecialChars(content));
                //content = new Regex(@"(?m)<a[^>]*>(\w|\W)*?</a[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(content, "");
                MatchCollection mtCollection = regex.Matches(content);
                if (mtCollection != null && mtCollection.Count > 0)
                {
                    content = content.Substring(0, content.IndexOf(mtCollection[0].Groups[0].Value.ToString()));
                }

                //替换多次换行
                content = new Regex(@"[\n]+", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(content, "\n");
                content = new Regex(@"[\r]+", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(content, "\n");

                //删除尾部换行
                content = content.TrimEnd('\n');

                //保留一定的回复行数 （saveLastReplyLineCount + 1）
                mtCollection = new Regex(@"[\n]+", RegexOptions.Multiline | RegexOptions.IgnoreCase).Matches(content);
                int saveLastReplyLineCount = 5;
                string tail = string.Empty;
                string head = "\n\n【 在 " + user + " 的大作中提到: 】\n: ";
                if (mtCollection != null && mtCollection.Count > saveLastReplyLineCount)
                {
                    for (int i = mtCollection.Count; i > saveLastReplyLineCount; i--)
                    {
                        content = content.Substring(0, content.LastIndexOf("\n"));
                    }

                    tail += "\n: ...................";
                }

                //添加回复头和尾
                content = head + content.Replace("\n", "\n: ") + tail;
                return content;
            }

            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string TrimUrls(string content)
        {
            MatchCollection mtLinkCollection = CommonUtil.GetMatchCollection("<a target=\"\\w+\" href=\"(?'Url'[^\\\"]+)\"[^>]*>(?'Text'[^\\<]*)</a>", content);
            if (mtLinkCollection != null)
            {
                foreach (Match mt in mtLinkCollection)
                {
                    content = content.Replace(mt.Groups[0].Value.ToString(), mt.Groups["Text"].Value.ToString());
                }
            }

            return content;
        }

        /// <summary>
        /// For section tops.
        /// </summary>
        /// <param name="wp"></param>
        /// <returns></returns>
        public static string GetSectionName(WebPage wp)
        {
            try
            {
                string name = CommonUtil.GetMatch("<li class=\"f\">(?'Title'\\w+)</li>", wp.Html, "Title");
                if (name != null)
                {
                    name = name.Replace("热门话题", "");
                }

                return name;
            }
            catch (Exception exp)
            {
                if (Logger.Enabled)
                {
                    Logger.Instance.Error(exp.Message + "\n" + exp.StackTrace);
                }

                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetReplyTail()
        {
            return "\n\n\n"
                 + "------------------"
                 + "\n"
                 + "->>[b][url=" 
                 + GetReplyUrl()
                 + "]水木PC客户端[/url][/b]<<-";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetReplyUrl()
        {
            return "http://www.cnblogs.com/junier/archive/2013/03/25/2980547.html";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetReplyText()
        {
            return "------------------"
                 + "\n"
                 + "->>水木PC客户端#"
                 + GetReplyUrl()
                 + "<<-";
        }
    }
}
