namespace Nzl.Smth.Utils
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Nzl.Smth.Datas;
    using Nzl.Web.Page;
    using Nzl.Web.Util;

    /// <summary>
    /// 
    /// </summary>
    public static class MailFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static IList<Mail> CreateMails(WebPage wp)
        {
            if (wp != null && wp.IsGood)
            {
                return CreateMails(wp.Html);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static Mail CreateMailDetail(WebPage wp)
        {
            if (wp != null && wp.IsGood)
            {
                return CreateMailDetail(wp.Html);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        private static IList<Mail> CreateMails(string html)
        {
            string pattern = @"(<li>|<li class=\Whla\W>)\s*"
                           + @"(?'Index'[1-9]0?)\.<a href=\W"
                           + @"(?'MailUrl'/mail/[a-z]+/\d+)\W"
                           + @"(?'IsNew'(\Wclass=\Wtop\W)?)>"
                           + @"(?'MailTitle'[^<]+)</a><br\s*/><a href=\W/user/query/"
                           + @"(?'Author'[a-zA-z][a-zA-Z0-9]{1,11})(\.)?\W>[a-zA-z][a-zA-Z0-9]{1,11}(\.)?</a>\|"
                           + @"(?'DateTime'[0-9,\-]{10}\s[0-9,\:]{8})</li>";

            MatchCollection mtMailCollection = Nzl.Web.Util.CommonUtil.GetMatchCollection(pattern, html);
            if (mtMailCollection != null)
            {
                IList<Mail> mailList = new List<Mail>();
                foreach (Match mt in mtMailCollection)
                {
                    Mail mail = new Mail();
                    mail.Index = System.Convert.ToInt32(mt.Groups["Index"].Value);
                    mail.Url = Configurations.BaseUrl + mt.Groups["MailUrl"].Value.ToString();
                    mail.Title = mt.Groups["MailTitle"].Value.ToString();
                    mail.Author = mt.Groups["Author"].Value.ToString();
                    mail.DateTime = mt.Groups["DateTime"].Value.ToString();
                    if (string.IsNullOrEmpty(mt.Groups["IsNew"].Value.ToString()) == false)
                    {
                        mail.IsNew = true;
                    }

                    mailList.Add(mail);
                }

                return mailList;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private static Mail CreateMailDetail(string html)
        {
            Mail mail = new Mail();
            string pattern = @"<li><div class=\Wnav hl\W><a href=\W/user/query/[a-zA-z][a-zA-Z0-9]{1,11}(\.)?\W>"
                           + @"(?'Author'[a-zA-z][a-zA-Z0-9]{1,11})(\.)?</a>\|<a class=\Wplant\W>"
                           + @"(?'DateTime'[\d, \-, \:]+)</a>\|<a href=\W"
                           + @"(?'ReplyUrl'/mail/\w+/send/\d+)\W>回复</a>\|<a href=\W"
                           + @"(?'TransferUrl'/mail/\w+/forward/\d+)\W>转寄</a>\|<a href=\W"
                           + @"(?'DeleteUrl'/mail/\w+/delete/\d+)\W>删除</a>\|<a href=\W/mail/\w+\W>返回</a>";
            mail.Title = CommonUtil.ReplaceSpecialChars(CommonUtil.GetMatch(@"<li class=\Wf\W>标题:(?'Title'.+)</li><li><div class=\Wnav hl\W>", html, "Title"));
            MatchCollection mtColleton = CommonUtil.GetMatchCollection(pattern, html);
            if (mtColleton != null && mtColleton.Count > 0)
            {
                mail.Author = mtColleton[0].Groups["Author"].Value.ToString();
                if (mtColleton[0].Groups["ReplyUrl"].Value != null && string.IsNullOrEmpty(mtColleton[0].Groups["ReplyUrl"].Value) == false)
                {
                    mail.ReplyUrl = Configurations.BaseUrl + mtColleton[0].Groups["ReplyUrl"].Value.ToString();
                }

                if (mtColleton[0].Groups["DeleteUrl"].Value != null && string.IsNullOrEmpty(mtColleton[0].Groups["DeleteUrl"].Value) == false)
                {
                    mail.DeleteUrl = Configurations.BaseUrl + mtColleton[0].Groups["DeleteUrl"].Value.ToString();
                }

                if (mtColleton[0].Groups["TransferUrl"].Value != null && string.IsNullOrEmpty(mtColleton[0].Groups["TransferUrl"].Value) == false)
                {
                    mail.TransferUrl = Configurations.BaseUrl + mtColleton[0].Groups["TransferUrl"].Value.ToString();
                }
            }

            pattern = @"<div class=\Wsp\W>(?'Content'[\w,\W]+)</div></li></ul>";
            string content = CommonUtil.GetMatch(pattern, html, "Content");
            if (content != null)
            {
                content = content.Replace("<br />", "\n");
                content = content.Replace("<br/>", "\n");
                content = content.Replace("<br>", "\n");
                content = content.Replace("<div class=\"sp\">", "");
                content = content.Replace("&nbsp;", " ");
                content = content.Replace("</div>", "");
                content = CommonUtil.ReplaceSpecialChars(content);
                mail.Content = content;
            }

            return mail;
        }
    }
}
