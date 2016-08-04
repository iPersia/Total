namespace Nzl.Web.Smth.Forms
{
    using System;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using Nzl.Web.Util;    
    using Nzl.Web.Page;
    using Nzl.Web.Smth.Datas;
    
    /// <summary>
    /// Class.
    /// </summary>
    public partial class MailDetailForm : Form
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public MailDetailForm()
        {
            InitializeComponent();
            this.linklblID.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblID_LinkClicked);
            this.linklblReply.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblReply_LinkClicked);
            this.linklblDelete.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblDelete_LinkClicked);
            this.linklblTransfer.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblTransfer_LinkClicked);
            this.richtxtContent.ContentsResized += RichtxtContent_ContentsResized;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public MailDetailForm(string url)
            : this()
        {
            if (string.IsNullOrEmpty(url) == false)
            {
                WebPage page = WebPageFactory.CreateWebPage(url);
                if (page != null && page.IsGood)
                {
                    string pattern = @"<li><div class=\Wnav hl\W><a href=\W/user/query/[a-zA-z][a-zA-Z0-9]{1,11}\W>"
                                   + @"(?'ID'[a-zA-z][a-zA-Z0-9]{1,11})</a>\|<a class=\Wplant\W>"
                                   + @"(?'DateTime'[\d, \-, \:]+)</a>\|<a href=\W"
                                   + @"(?'ReplyUrl'/mail/\w+/send/\d+)\W>回复</a>\|<a href=\W"
                                   + @"(?'TransferUrl'/mail/\w+/forward/\d+)\W>转寄</a>\|<a href=\W"
                                   + @"(?'DeleteUrl'/mail/\w+/delete/\d+)\W>删除</a>\|<a href=\W/mail/\w+\W>返回</a>";
                    MatchCollection mtColleton = CommonUtil.GetMatchCollection(pattern, page.Html);
                    if (mtColleton != null && mtColleton.Count > 0)
                    {
                        this.linklblID.Visible = true;
                        this.linklblID.Text = mtColleton[0].Groups["ID"].Value.ToString();
                        this.linklblID.Links.Add(0, this.linklblID.Text.Length, this.linklblID.Text);
                        this.lblDateTime.Text = mtColleton[0].Groups["DateTime"].Value.ToString();
                        this.lblTitle.Text = CommonUtil.ReplaceSpecialChars(CommonUtil.GetMatch(@"<li class=\Wf\W>标题:(?'Title'.+)</li><li><div class=\Wnav hl\W>", page.Html, "Title"));
                        this.lblDateTime.Text = mtColleton[0].Groups["DateTime"].Value.ToString();
                        if (mtColleton[0].Groups["ReplyUrl"].Value != null && string.IsNullOrEmpty(mtColleton[0].Groups["ReplyUrl"].Value) == false)
                        {
                            this.linklblReply.Visible = true;
                            this.linklblReply.Links.Add(0, this.linklblReply.Text.Length, Configurations.BaseUrl + mtColleton[0].Groups["ReplyUrl"].Value.ToString());
                        }

                        if (mtColleton[0].Groups["DeleteUrl"].Value != null && string.IsNullOrEmpty(mtColleton[0].Groups["DeleteUrl"].Value) == false)
                        {
                            this.linklblDelete.Visible = true;
                            this.linklblDelete.Links.Add(0, this.linklblDelete.Text.Length, Configurations.BaseUrl + mtColleton[0].Groups["DeleteUrl"].Value.ToString());
                        }

                        if (mtColleton[0].Groups["TransferUrl"].Value != null && string.IsNullOrEmpty(mtColleton[0].Groups["TransferUrl"].Value) == false)
                        {
                            this.linklblTransfer.Visible = true;
                            this.linklblTransfer.Links.Add(0, this.linklblTransfer.Text.Length, Configurations.BaseUrl + mtColleton[0].Groups["TransferUrl"].Value.ToString());
                        }
                    }

                    pattern = @"<div class=\Wsp\W>(?'Content'[\w,\W]+)</div></li></ul>";
                    string content = CommonUtil.GetMatch(pattern, page.Html, "Content");
                    content = content.Replace("<br />", "\n");
                    content = content.Replace("<br/>", "\n");
                    content = content.Replace("<br>", "\n");
                    content = content.Replace("<div class=\"sp\">", "");
                    content = content.Replace("&nbsp;", " ");
                    content = content.Replace("</div>", "");
                    content = CommonUtil.ReplaceSpecialChars(content);
                    this.richtxtContent.AppendText(content);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mail"></param>
        public MailDetailForm(Mail mail)
            : this()
        {
            if (mail != null)
            {
                this.lblTitle.Text = mail.Title;
                this.lblDateTime.Text = mail.DateTime;
                this.linklblID.Text = mail.Author;
                this.linklblID.Links.Add(0, mail.Author.Length, mail.Author);                
                this.linklblReply.Links.Add(0, this.linklblReply.Text.Length, mail.ReplyUrl);
                this.linklblDelete.Links.Add(0, this.linklblDelete.Text.Length, mail.DeleteUrl);
                this.linklblTransfer.Links.Add(0, this.linklblTransfer.Text.Length, mail.TransferUrl);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private string GetReplyContent(string id, string content)
        {
            if (string.IsNullOrEmpty(content) == false)
            {
                Regex regex = new Regex(@"\s*--\sFROM\s[\d, \., \*]+");
                content = regex.Replace(content, "");

                //删除上层回复
                regex = new Regex(@"【\s在\s.+\s的大作中提到\: 】");
                content = new Regex(@"(?m)<a[^>]*>(\w|\W)*?</a[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(content, "");
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
                string head = "\n\n【 在 " + id + " 的大作中提到: 】\n: ";
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

        #region Event handler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblID_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                UserForm userForm = new UserForm(e.Link.LinkData.ToString());
                userForm.StartPosition = FormStartPosition.CenterScreen;
                userForm.ShowDialog(this);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblReply_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                string content = GetReplyContent(this.linklblID.Text, this.richtxtContent.Text);
                NewMailForm newMailForm = new NewMailForm(this.linklblID.Text, this.lblTitle.Text, content);
                newMailForm.StartPosition = FormStartPosition.CenterParent;
                newMailForm.ShowDialog(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblDelete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                if (string.IsNullOrEmpty(e.Link.LinkData.ToString()) == false)
                {
                    WebPage page = WebPageFactory.CreateWebPage(e.Link.LinkData.ToString());
                    string result = CommonUtil.GetMatch(@"<div id=\Wm_main\W><div class=\Wsp hl f\W>(?'Result'\w+)</div>", page.Html, "Result");
                    if (result != null && result.Contains("成功"))
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                        this.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblTransfer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RichtxtContent_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            RichTextBox rtb = sender as RichTextBox;
            if (rtb != null)
            {
                rtb.Size = e.NewRectangle.Size;
                this.Size = new System.Drawing.Size(rtb.Size.Width + 40, rtb.Size.Height + 125);
            }
        }
        #endregion
    }
}
