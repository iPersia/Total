namespace Nzl.Web.Smth.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Nzl.Web.Page;
    using Nzl.Web.Util;
    using Nzl.Web.Smth.Datas;

    /// <summary>
    /// 
    /// </summary>
    public static class SmthUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool GetLogInStatus(WebPage wp)
        {
            return !string.IsNullOrEmpty(CommonUtil.GetMatch(@"<a href=\W/user/logout\W accesskey=\W9\W>注销\([a-zA-z][a-zA-Z0-9]{1,11}\)</a>", wp.Html));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetLogInUserID(WebPage wp)
        {
            return CommonUtil.GetMatch(@"<a href=\W/user/logout\W accesskey=\W9\W>注销\((?'UserID'[a-zA-z][a-zA-Z0-9]{1,11})\)</a>", wp.Html, "UserID");
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
                string head = "\n\n【 在 " + thread.ID + " 的大作中提到: 】\n: ";
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
        /// 
        /// </summary>
        /// <param name="wp"></param>
        /// <returns></returns>
        public static string GetSectionTitle(WebPage wp)
        {
            try
            {
                return CommonUtil.GetMatch("<li class=\"f\">(?'Title'\\w+)</li>", wp.Html, 1).Replace("热门话题", "");
            }
            catch (Exception exp)
            {
                if (Program.LoggerEnabled)
                {
                    Program.Logger.Error(exp.Message);
                }

                return null;
            }
        }
    }
}
