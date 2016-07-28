namespace Nzl.Web.Smth.Utils
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Datas;
    using Page;
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
                    Mail mail = new Mail(System.Convert.ToInt32(mt.Groups["Index"].Value),
                                         @"http://m.newsmth.net" + mt.Groups["MailUrl"].Value.ToString(),
                                         mt.Groups["MailTitle"].Value.ToString(),
                                         mt.Groups["Author"].Value.ToString(),
                                         mt.Groups["DateTime"].Value.ToString());

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
    }
}
