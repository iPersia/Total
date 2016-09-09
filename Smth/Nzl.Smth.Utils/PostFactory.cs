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
    public static class PostFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static IList<Post> CreatePosts(WebPage wp)
        {
            if (wp != null && wp.IsGood)
            {
                return CreatePosts(wp.Html);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        private static IList<Post> CreatePosts(string html)
        {
            string pattern = @"<div class=\Wsec nav\W>(<a href=\W"
                           + @"(?'NewUrl'/article/[\w,%2E,%5F,\.,_]+/post\?s=\d+)\W>发表</a>\|)?<a href=\W"
                           + @"(?'ExpandUrl'/article/[\w,%2E,%5F,\.,_]+/\d+\?s=\d+)\W>展开</a>\|<a href=\W"
                           + @"(?'HostUrl'/article/[\w,%2E,%5F,\.,_]+/single/\d+)\W>楼主</a>\|<a href=\W"
                           + @"(?'SubjectExpandUrl'/article/[\w,%2E,%5F,\.,_]+/\d+)\W>同主题展开</a>(\|<a href=\W"
                           + @"(?'SourceUrl'/article/[\w,%2E,%5F,\.,_]+/single/\d+)\W>溯源</a>)?\|<a href=\W"
                           + @"(?'BoardUrl'/board/"
                           + @"(?'Board'[\w,%2E,%5F,\.,_]+)/\d+)\W>返回</a></div><div class=\Wsec nav\W>(<a href=\W"
                           + @"(?'LastUrl'/article/[\w,%2E,%5F,\.,_]+/single/\d+)\W>上一篇</a>)?(\|)?(<a href=\W"
                           + @"(?'NextUrl'/article/[\w,%2E,%5F,\.,_]+/single/\d+)\W>下一篇</a>)?(\|)?(<a href=\W"
                           + @"(?'SubjectLastUrl'/article/[\w,%2E,%5F,\.,_]+/single/\d+)\W>同主题上篇</a>)?(\|)?(<a href=\W"
                           + @"(?'SubjectNextUrl'/article/[\w,%2E,%5F,\.,_]+/single/\d+)\W>同主题下篇</a>)?</div><ul class=\Wlist sec\W><li class=\Wf\W>"
                           + @"(?'Subject'[^<]*)</li><li><div class=\Wnav hl\W><div><a href=\W/user/query/"
                           + @"(?'Author'[a-zA-z][a-zA-Z0-9]{1,11})\W>[a-zA-z][a-zA-Z0-9]{1,11}</a>\|<a class=\Wplant\W>"
                           + @"(?'DateTime'\d{4}-\d{2}-\d{2}\s*\d{2}:\d{2}:\d{2})</a>\|</div><div>((<a href=\W"
                           + @"(?'ReplyUrl'/article/[\w,%2E,%5F,\.,_]+/post/\d+\?s=1)\W>回复</a>)?\|<a href=\W"
                           + @"(?'MailUrl'/mail/[\w,%2E,%5F,\.,_]+/send/\d+)\W>发信</a>\|<a href=\W"
                           + @"(?'TransferUrl'/article/[\w,%2E,%5F,\.,_]+/forward/\d+\?s=1)\W>转寄</a>(\|<a href=\W"
                           + @"(?'EditUrl'/article/[\w,%2E,%5F,\.,_]+/edit/\d+\?s=\d+)\W>编辑</a>\|<a href=\W"
                           + @"(?'DeleteUrl'/article/[\w,%2E,%5F,\.,_]+/delete/\d+\?s=\d+)\W>删除</a>)?)?</div></div><div class=\Wsp\W>"
                           + @"(?'Content'.*)</div></li></ul>";

            MatchCollection mtMailCollection = Nzl.Web.Util.CommonUtil.GetMatchCollection(pattern, html);
            if (mtMailCollection != null)
            {
                IList<Post> referList = new List<Post>();
                foreach (Match mt in mtMailCollection)
                {
                    Post refer = RecycledQueues.GetRecycled<Post>();
                    if (refer == null)
                    {
                        refer = new Post();
                    }

                    refer.Author = mt.Groups["Author"].Value.ToString();
                    refer.Content = mt.Groups["Content"].Value.ToString();
                    refer.DateTime = mt.Groups["DateTime"].Value.ToString();
                    refer.Subject = CommonUtil.ReplaceSpecialChars(mt.Groups["Subject"].Value.ToString());
                    refer.Board = mt.Groups["Board"].Value.ToString();
                    if (mt.Groups["NewUrl"].Value != null &&
                        string.IsNullOrEmpty(mt.Groups["NewUrl"].Value.ToString()) == false)
                    {
                        refer.NewUrl = Configuration.BaseUrl + mt.Groups["NewUrl"].Value.ToString();
                    }
                    else
                    {
                        refer.NewUrl = null;
                    }

                    if (mt.Groups["ReplyUrl"].Value != null &&
                        string.IsNullOrEmpty(mt.Groups["ReplyUrl"].Value.ToString()) == false)
                    {
                        refer.ReplyUrl = Configuration.BaseUrl + mt.Groups["ReplyUrl"].Value.ToString();
                    }
                    else
                    {
                        refer.ReplyUrl = null;
                    }

                    if (mt.Groups["LastUrl"].Value != null &&
                        string.IsNullOrEmpty(mt.Groups["LastUrl"].Value.ToString()) == false)
                    {
                        refer.LastUrl = Configuration.BaseUrl + mt.Groups["LastUrl"].Value.ToString();
                    }
                    else
                    {
                        refer.LastUrl = null;
                    }

                    if (mt.Groups["NextUrl"].Value != null &&
                        string.IsNullOrEmpty(mt.Groups["NextUrl"].Value.ToString()) == false)
                    {
                        refer.NextUrl = Configuration.BaseUrl + mt.Groups["NextUrl"].Value.ToString();
                    }
                    else
                    {
                        refer.NextUrl = null;
                    }

                    if (mt.Groups["SubjectLastUrl"].Value != null &&
                        string.IsNullOrEmpty(mt.Groups["SubjectLastUrl"].Value.ToString()) == false)
                    {
                        refer.SubjectLastUrl = Configuration.BaseUrl + mt.Groups["SubjectLastUrl"].Value.ToString();
                    }
                    else
                    {
                        refer.SubjectLastUrl = null;
                    }

                    if (mt.Groups["SubjectNextUrl"].Value != null &&
                        string.IsNullOrEmpty(mt.Groups["SubjectNextUrl"].Value.ToString()) == false)
                    {
                        refer.SubjectNextUrl = Configuration.BaseUrl + mt.Groups["SubjectNextUrl"].Value.ToString();
                    }
                    else
                    {
                        refer.SubjectNextUrl = null;
                    }

                    if (mt.Groups["EditUrl"].Value != null &&
                        string.IsNullOrEmpty(mt.Groups["EditUrl"].Value.ToString()) == false)
                    {
                        refer.EditUrl = Configuration.BaseUrl + mt.Groups["EditUrl"].Value.ToString();
                    }
                    else
                    {
                        refer.EditUrl = null;
                    }

                    if (mt.Groups["DeleteUrl"].Value != null &&
                        string.IsNullOrEmpty(mt.Groups["DeleteUrl"].Value.ToString()) == false)
                    {
                        refer.DeleteUrl = Configuration.BaseUrl + mt.Groups["DeleteUrl"].Value.ToString();
                    }
                    else
                    {
                        refer.DeleteUrl = null;
                    }

                    if (mt.Groups["MailUrl"].Value != null &&
                        string.IsNullOrEmpty(mt.Groups["MailUrl"].Value.ToString()) == false)
                    {
                        refer.MailUrl = Configuration.BaseUrl + mt.Groups["MailUrl"].Value.ToString();
                    }
                    else
                    {
                        refer.MailUrl = null;
                    }

                    if (mt.Groups["TransferUrl"].Value != null &&
                        string.IsNullOrEmpty(mt.Groups["MailUrl"].Value.ToString()) == false)
                    {
                        refer.TransferUrl = Configuration.BaseUrl + mt.Groups["TransferUrl"].Value.ToString();
                    }
                    else
                    {
                        refer.TransferUrl = null;
                    }

                    refer.ExpandUrl = Configuration.BaseUrl + mt.Groups["ExpandUrl"].Value.ToString();
                    refer.HostUrl = Configuration.BaseUrl + mt.Groups["HostUrl"].Value.ToString();
                    refer.SubjectExpandUrl = Configuration.BaseUrl + mt.Groups["SubjectExpandUrl"].Value.ToString();
                    refer.SourceUrl = Configuration.BaseUrl + mt.Groups["SourceUrl"].Value.ToString();
                    refer.BoardUrl = Configuration.BaseUrl + mt.Groups["BoardUrl"].Value.ToString();
                    if (mt.Groups["Content"].Value != null)
                    {
                        refer.Data = ThreadFactory.CreateThread(mt.Groups["Content"].Value.ToString());
                    }

                    referList.Add(refer);
                }

                return referList;
            }

            return null;
        }
    }
}

