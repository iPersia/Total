namespace Nzl.Smth.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using Nzl.Recycling;
    using Nzl.Smth.Configs;
    using Nzl.Smth.Datas;    
    using Nzl.Smth.Logger;
    using Nzl.Web.Page;
    using Nzl.Web.Util;

    /// <summary>
    /// 
    /// </summary>
    public class ThreadFactory
    {
        #region Variables
        /// <summary>
        /// 
        /// </summary>
        private static string _tokenPrefix = "_<PREFIX>_";

        /// <summary>
        /// 
        /// </summary>
        private static string _tokenSuffix = "_<SUFFIX_>_";

        /// <summary>
        /// 
        /// </summary>
        private static string _imageToken = "IMAGE";

        /// <summary>
        /// 
        /// </summary>
        private static string _iconToken = "ICON";

        /// <summary>
        /// 
        /// </summary>
        private static string _anchorToken = "ANCHOR";
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public static string TokenPrefix
        {
            get
            {
                return _tokenPrefix;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string TokenSuffix
        {
            get
            {
                return _tokenSuffix;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string ImageToken
        {
            get
            {
                return _imageToken;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string IconToken
        {
            get
            {
                return _iconToken;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string AnchorToken
        {
            get
            {
                return _anchorToken;
            }
        }
        #endregion

        #region Publics.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static IList<Thread> CreateThreads(WebPage page)
        {
            try
            {
                MatchCollection mtContentCollection = CommonUtil.GetMatchCollection("<div class=\"sp\">", page.Html);
                //MatchCollection mtNaviCollection = CommonUtil.GetMatchCollection("<div class=\"nav hl\">", page.Html);
                IList<Thread> threadList = ThreadFactory.GetThreads(page.Html);
                string htmlNavi = page.Html;
                string htmlContent = page.Html;
                if (threadList.Count == mtContentCollection.Count)
                {
                    for (int i = 0; i < threadList.Count; i++)
                    {
                        Match mt = mtContentCollection[i];
                        Thread thread = threadList[i];
                        if (mt.Success)
                        {
                            int startPos = htmlContent.IndexOf(mt.Groups[0].Value.ToString());
                            htmlContent = htmlContent.Substring(startPos);
                            string endStr = @"</div>";
                            int endPos = htmlContent.IndexOf(endStr);
                            string divstr = htmlContent.Substring(0, endPos + endStr.Length);
                            htmlContent = htmlContent.Substring(endPos);
                            if (thread != null)
                            {
                                thread.Content = TrimHtmlTag(divstr);
                                thread.Tag = thread.Content;
                                string content = thread.Content;
                                IList<string> imageUrls = GetImageUrls(ref content);
                                if (imageUrls.Count > 0)
                                {
                                    Dictionary<string, Image> images = new Dictionary<string, Image>();
                                    foreach (string imageUrl in imageUrls)
                                    {
                                        if (images.ContainsKey(imageUrl) == false)
                                        {
                                            Image image = CommonUtil.GetWebImage(imageUrl);
                                            if (image != null)
                                            {
                                                image.Tag = RtfUtil.GetRtfCode(image);
                                                images.Add(imageUrl, image);
                                            }
                                        }                                    
                                    }

                                    thread.ImageUrls = imageUrls;
                                    thread.Images = images;
                                }

                                IList<string> iconUrls = GetIconUrls(ref content);
                                if (iconUrls.Count > 0)
                                {
                                    Dictionary<string, Image> icons = new Dictionary<string, Image>();
                                    foreach (string iconUrl in iconUrls)
                                    {
                                        if (icons.ContainsKey(iconUrl) == false)
                                        {
                                            Image image = CommonUtil.GetWebImage(iconUrl);
                                            if (image != null)
                                            {
                                                icons.Add(iconUrl, image);
                                            }
                                        }
                                    }

                                    thread.IconUrls = iconUrls;
                                    thread.Icons = icons;
                                }

                                thread.Anchors = GetAnchorUrls(ref content);
                                thread.Content = content;
                            }
                        }
                    }
                }

                return threadList;
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
        /// <param name="page"></param>
        /// <returns></returns>
        public static Thread CreateThread(string content)
        {
            if (string.IsNullOrEmpty(content) == false)
            {
                Thread thread = RecycledQueues.GetRecycled<Thread>();
                if (thread == null)
                {
                    thread = new Thread();
                }
                content = TrimHtmlTag(content);
                thread.Content = content;
                thread.Tag = thread.Content;
                IList<string> imageUrls = GetImageUrls(ref content);
                if (imageUrls.Count > 0)
                {
                    Dictionary<string, Image> images = new Dictionary<string, Image>();
                    foreach (string imageUrl in imageUrls)
                    {
                        if (images.ContainsKey(imageUrl) == false)
                        {
                            Image image = CommonUtil.GetWebImage(imageUrl);
                            if (image != null)
                            {
                                image.Tag = RtfUtil.GetRtfCode(image);
                                images.Add(imageUrl, image);
                            }
                        }
                    }

                    thread.ImageUrls = imageUrls;
                    thread.Images = images;
                }

                IList<string> iconUrls = GetIconUrls(ref content);
                if (iconUrls.Count > 0)
                {
                    Dictionary<string, Image> icons = new Dictionary<string, Image>();
                    foreach (string iconUrl in iconUrls)
                    {
                        if (icons.ContainsKey(iconUrl) == false)
                        {
                            Image image = CommonUtil.GetWebImage(iconUrl);
                            if (image != null)
                            {
                                icons.Add(iconUrl, image);
                            }
                        }
                    }

                    thread.IconUrls = iconUrls;
                    thread.Icons = icons;
                }

                thread.Anchors = GetAnchorUrls(ref content);
                thread.Content = content;
                return thread;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="divstr"></param>
        /// <returns></returns>
        public static string TrimHtmlTag(string divstr)
        {
            if (string.IsNullOrEmpty(divstr) == false)
            {
                divstr = divstr.Replace("<br />", "\n");
                divstr = divstr.Replace("<br/>", "\n");
                divstr = divstr.Replace("<br>", "\n");
                divstr = divstr.Replace("<div class=\"sp\">", "");
                divstr = divstr.Replace("&nbsp;", " ");
                divstr = divstr.Replace("</div>", "");
                divstr = divstr.Replace("</font>", "");
                //divstr = divstr.Replace("<b>", "『");
                //divstr = divstr.Replace("</b>", "』");
                string pattern = "<font[^>]*>";
                Regex regex = new Regex(pattern);
                divstr = regex.Replace(divstr, "");
                return divstr;
            }

            return string.Empty;
        }
        #endregion

        #region Privates
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wp"></param>
        /// <returns></returns>
        private static IList<string> GetImageUrls(ref string content)
        {
            string pattern = @"<a target=\W_blank\W href=\W("
                           + Configuration.AttachmentBaseUrl
                           + @")?/att/[\w, %2E, %5F]+/\d+/\d+\W><img border=\W0\W title=\W单击此查看原图\W src=\W(?'ImageUrl'(?'HostUrl'"
                           + Configuration.AttachmentBaseUrl
                           + @")?/att/[\w, %2E, %5F]+/\d+/\d+/middle)\W class=\Wresizeable\W /></a>";
            MatchCollection mtCollection = CommonUtil.GetMatchCollection(pattern, content);
            IList<string> imageUrlList = new List<string>();
            if (mtCollection != null)
            {
                foreach (Match mt in mtCollection)
                {
                    string hostUrl = string.IsNullOrEmpty(mt.Groups["HostUrl"].Value.ToString()) ? Configuration.AttachmentBaseUrl : "";
                    string imageUrl = hostUrl + mt.Groups["ImageUrl"].Value.ToString();
                    imageUrlList.Add(imageUrl);
                    content = content.Replace(mt.Groups[0].Value.ToString(), ThreadFactory.TokenPrefix
                                                                           + ThreadFactory.ImageToken
                                                                           + ThreadFactory.TokenSuffix);
                }
            }

            return imageUrlList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private static IList<string> GetIconUrls(ref string content)
        {
            MatchCollection mtCollection = CommonUtil.GetMatchCollection("<img src=\"(?'IconUrl'[^\"]+)\" style=\"display:inline;border-style:none\"/>", content);
            IList<string> iconUrlList = new List<string>();
            if (mtCollection != null)
            {
                foreach (Match mt in mtCollection)
                {
                    iconUrlList.Add("http://m.newsmth.net" + mt.Groups["IconUrl"].Value.ToString());
                    content = content.Replace(mt.Groups[0].Value.ToString(), ThreadFactory.TokenPrefix + ThreadFactory.IconToken + ThreadFactory.TokenSuffix);
                }
            }

            return iconUrlList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wp"></param>
        /// <returns></returns>
        private static IList<Anchor> GetAnchorUrls(ref string content)
        {
            IList<Anchor> anchorList = new List<Anchor>();
            MatchCollection mtLinkCollection = CommonUtil.GetMatchCollection("<a target=\"\\w+\" href=\"(?'Url'[^\\\"]+)\"[^>]*>(?'Text'[^\\<]*)</a>", content);
            if (mtLinkCollection != null)
            {
                foreach (Match mt in mtLinkCollection)
                {
                    Anchor anchor = new Anchor();
                    anchor.Text = mt.Groups["Text"].Value.ToString();
                    anchor.Url = mt.Groups["Url"].Value.ToString();
                    anchorList.Add(anchor);
                    content = content.Replace(mt.Groups[0].Value.ToString(), ThreadFactory.TokenPrefix + ThreadFactory.AnchorToken + ThreadFactory.TokenSuffix);
                }
            }

            return anchorList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private static IList<Thread> GetThreads(string html)
        {
            string pattern = @"<div class=\Wnav hl\W><div><a class=\Wplant\W>"
                           + @"(?'Floor'\d+楼|楼主)</a>\|<a href=\W/user/query/"
                           + @"(?'User'[a-zA-z][a-zA-Z0-9]{1,11})\W>[a-zA-z][a-zA-Z0-9]{1,11}</a>\|<a class=\Wplant\W>"
                           + @"(?'DateTime'\d{4}-\d{2}-\d{2}\s*\d{2}:\d{2}:\d{2})</a>\|<a href=\W"
                           + @"(?'QueryUrl'/article/[\w,%2E,%5F,\.,_]+/\d+\?au=[a-zA-z][a-zA-Z0-9]{1,11}|/article/[\w,%2E,%5F,\.,_]+/\d+\?s=\d+)\W>"
                           + @"(?'QueryType'只看此ID|展开)</a></div>"
                           + @"<div tid=\W(?'TID'\d+)\W>(<a href=\W"
                           + @"(?'ReplyUrl'/article/[\w,%2E,%5F,\.,_]+/post/\d+)\W>回复</a>\|<a href=\W"
                           + @"(?'MailUrl'/mail/[\w,%2E,%5F,\.,_]+/send/\d+)\W>发信</a>\|<a href=\W"
                           + @"(?'TransferUrl'/article/[\w,%2E,%5F,\.,_]+/forward/\d+)\W>转寄</a>(\|<a href=\W"
                           + @"(?'EditUrl'/article/[\w,%2E,%5F,\.,_]+/edit/\d+)\W>编辑</a>\|<a href=\W"
                           + @"(?'DeleteUrl'/article/[\w,%2E,%5F,\.,_]+/delete/\d+)\W>删除</a>)?)?</div>";

            MatchCollection mtColletion = CommonUtil.GetMatchCollection(pattern, html);
            IList<Thread> threadList = new List<Thread>();
            if (mtColletion != null)
            {
                foreach (Match mt in mtColletion)
                {
                    //Thread thread = new Thread();
                    Thread thread = RecycledQueues.GetRecycled<Thread>();
                    if (thread == null)
                    {
                        thread = new Thread();
                    }

                    thread.User = mt.Groups["User"].Value.ToString();
                    thread.ID = mt.Groups["TID"].Value.ToString();
                    thread.DateTime = mt.Groups["DateTime"].Value.ToString();
                    thread.Floor = mt.Groups["Floor"].Value.ToString();
                    thread.QueryType = mt.Groups["QueryType"].Value.ToString();
                    thread.QueryUrl = Configuration.BaseUrl + mt.Groups["QueryUrl"].Value.ToString();
                    if (mt.Groups["ReplyUrl"].Value != null && mt.Groups["ReplyUrl"].Value != "")
                    {
                        thread.ReplyUrl = Configuration.BaseUrl + mt.Groups["ReplyUrl"].Value.ToString();
                    }
                    else
                    {
                        thread.ReplyUrl = null;
                    }

                    if (mt.Groups["MailUrl"].Value != null && mt.Groups["MailUrl"].Value != "")
                    {
                        thread.MailUrl = Configuration.BaseUrl + mt.Groups["MailUrl"].Value.ToString();
                    }
                    else
                    {
                        thread.MailUrl = null;
                    }

                    if (mt.Groups["TransferUrl"].Value != null && mt.Groups["TransferUrl"].Value != "")
                    {
                        thread.TransferUrl = Configuration.BaseUrl + mt.Groups["TransferUrl"].Value.ToString();
                    }
                    else
                    {
                        thread.TransferUrl = null;
                    }

                    if (mt.Groups["EditUrl"].Value != null && mt.Groups["EditUrl"].Value != "")
                    {
                        thread.EditUrl = Configuration.BaseUrl + mt.Groups["EditUrl"].Value.ToString();
                    }
                    else
                    {
                        thread.EditUrl = null;
                    }

                    if (mt.Groups["DeleteUrl"].Value != null && mt.Groups["DeleteUrl"].Value != "")
                    {
                        thread.DeleteUrl = Configuration.BaseUrl + mt.Groups["DeleteUrl"].Value.ToString();
                    }
                    else
                    {
                        thread.DeleteUrl = null;
                    }

                    threadList.Add(thread);
                }

                return threadList;
            }

            return null;
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private string GetXml(string content)
        {
            if (string.IsNullOrEmpty(content))
                return null;

            {

            }

            {

            }

            {

            }

            {

            }

            return null;
        }
        #endregion
    }
}
