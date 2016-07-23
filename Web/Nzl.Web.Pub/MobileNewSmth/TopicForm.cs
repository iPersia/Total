namespace Nzl.Web.Pub.MobileNewSmth
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using Nzl.Web.Util;
    using Nzl.Web.Page;

    /// <summary>
    /// The topic form.
    /// </summary>
    public partial class TopicForm : Form
    {
        #region Variable
        /// <summary>
        /// 
        /// </summary>
        private string _topic;

        /// <summary>
        /// The url.
        /// </summary>
        private string _topicUrl;

        /// <summary>
        /// 
        /// </summary>
        private int _currentPageIndex = 0;

        /// <summary>
        /// 
        /// </summary>
        private int _totalPage = 0;

        /// <summary>
        /// 
        /// </summary>
        private string _subject;

        /// <summary>
        /// 
        /// </summary>
        private string _postUrl;

        /// <summary>
        /// 
        /// </summary>
        private string _targetUserID;

        /// <summary>
        /// 
        /// </summary>
        private int _margin = 4;

        /// <summary>
        /// 
        /// </summary>
        private System.Threading.SynchronizationContext _uiContext = WindowsFormsSynchronizationContext.Current;

        /// <summary>
        /// 
        /// </summary>
        private static string _imageToken = "_I__IMAGE__URL__I_";
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string TopicUrl
        {
            get
            {
                return this._topicUrl;
            }

            set
            {
                this._topicUrl = value;
            }
        }
        #endregion

        #region Ctors.
        /// <summary>
        /// Ctor.
        /// </summary>
        public TopicForm()
        {
            InitializeComponent();
            this.MouseWheel += new MouseEventHandler(TopicForm_MouseWheel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        public TopicForm(string uri)
            : this()
        {
            this._topicUrl = uri;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        public TopicForm(string uri, string userID)
            : this(uri)
        {
            this._targetUserID = userID;
        }

        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            if (string.IsNullOrEmpty(this._targetUserID) == false)
            {
                this.btnGrow.Visible = false;
                this.btnShrink.Visible = false;
                this.Height = 480;
            }
            else
            {
                this.Height = System.Windows.Forms.SystemInformation.VirtualScreen.Height - 200;
                if (this.Height > 800)
                {
                    this.Height = 800;
                }

                if (this.Height < 480)
                {
                    this.Height = 480;
                }
            }
        }
        #endregion

        #region overrides
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Init();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            FetchNewPage(1);
        }
        #endregion

        #region Get information
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private IList<Thread> GetThreads(WebPage page)
        {
            MatchCollection mtContentCollection = CommonUtil.GetMatchCollection("<div class=\"sp\">", page.Html);
            //MatchCollection mtNaviCollection = CommonUtil.GetMatchCollection("<div class=\"nav hl\">", page.Html);
            IList<Thread> threadList = this.GetThreads(page.Html);
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
                            thread.Content = GetThreadContent(divstr);
                            string content = thread.Content;
                            IList<string> imageUrlList = this.GetImageUrls(ref content);
                            thread.Content = content;
                            if (imageUrlList.Count > 0)
                            {
                                IList<Image> imageList = new List<Image>();
                                foreach (string imageUrl in imageUrlList)
                                {
                                    imageList.Add(this.GetImage(imageUrl));
                                }

                                thread.ImageList = imageList;
                            }
                        }
                    }
                }
            }

            return threadList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wp"></param>
        /// <returns></returns>
        private string GetTopic(WebPage wp)
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
        /// <param name="html"></param>
        private IList<Thread> GetThreads(string html)
        {
            string pattern = @"<div class=\Wnav hl\W><div><a class=\Wplant\W>"
                           + @"(?'Floor'\d+楼|楼主)</a>\|<a href=\W/user/query/"
                           + @"(?'ID'[a-zA-z][a-zA-Z0-9]{1,11})\W>[a-zA-z][a-zA-Z0-9]{1,11}</a>\|<a class=\Wplant\W>"
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
                    Thread thread = new Thread();
                    thread.ID = mt.Groups["ID"].Value.ToString();
                    thread.DateTime = mt.Groups["DateTime"].Value.ToString();
                    thread.Floor = mt.Groups["Floor"].Value.ToString();
                    thread.QueryType = mt.Groups["QueryType"].Value.ToString();
                    thread.QueryUrl = @"http://m.newsmth.net" + mt.Groups["QueryUrl"].Value.ToString();
                    if (mt.Groups["ReplyUrl"].Value != null && mt.Groups["ReplyUrl"].Value != "")
                    {
                        thread.ReplyUrl = @"http://m.newsmth.net" + mt.Groups["ReplyUrl"].Value.ToString();
                    }

                    if (mt.Groups["MailUrl"].Value != null && mt.Groups["MailUrl"].Value != "")
                    {
                        thread.MailUrl = @"http://m.newsmth.net" + mt.Groups["MailUrl"].Value.ToString();
                    }

                    if (mt.Groups["TransferUrl"].Value != null && mt.Groups["TransferUrl"].Value != "")
                    {
                        thread.TransferUrl = @"http://m.newsmth.net" + mt.Groups["TransferUrl"].Value.ToString();
                    }
                    if (mt.Groups["EditUrl"].Value != null && mt.Groups["EditUrl"].Value != "")
                    {
                        thread.EditUrl = @"http://m.newsmth.net" + mt.Groups["EditUrl"].Value.ToString();
                    }

                    if (mt.Groups["DeleteUrl"].Value != null && mt.Groups["DeleteUrl"].Value != "")
                    {
                        thread.DeleteUrl = @"http://m.newsmth.net" + mt.Groups["DeleteUrl"].Value.ToString();
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
        /// <returns></returns>
        private bool GetLogInStatus(WebPage wp)
        {
            return !string.IsNullOrEmpty(CommonUtil.GetMatch(@"<a href=\W/user/logout\W accesskey=\W9\W>注销\([a-zA-z][a-zA-Z0-9]{1,11}\)</a>", wp.Html));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wp"></param>
        /// <returns></returns>
        private string GetSection(WebPage wp)
        {
            return CommonUtil.GetMatch("<div class=\"menu sp\">(?'Region'.+)</div><div id=\"m_main\">", wp.Html, 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="divstr"></param>
        /// <returns></returns>
        private Thread GetThread(string divstr)
        {
            if (string.IsNullOrEmpty(divstr) == false)
            {
                Thread thread = new Thread();
                thread.Floor = CommonUtil.GetMatch(@"\d+楼|楼主", divstr);
                thread.DateTime = CommonUtil.GetMatch(@"\d{4}-\d{2}-\d{2}\s*\d{2}:\d{2}:\d{2}", divstr);
                thread.ID = CommonUtil.GetMatch(@"/user/query/(?'ID'[a-zA-z][a-zA-Z0-9]{1,11})", divstr, 1);
                thread.ReplyUrl = CommonUtil.GetMatch(@"<div><a href=\W(?'ReplyUrl'/article/[\w,%2E,%5F,\.,_]+/post/\d+)\W>回复</a>", divstr, "ReplyUrl");
                thread.MailUrl = CommonUtil.GetMatch(@"<a href=\W(?'MailUrl'/mail/[\w,%2E,%5F,\.,_]+/send/\d+)\W>发信</a>", divstr, "MailUrl");
                return thread;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="divstr"></param>
        /// <returns></returns>
        private string GetThreadContent(string divstr)
        {
            if (string.IsNullOrEmpty(divstr) == false)
            {
                divstr = divstr.Replace("<br />", "\n");
                divstr = divstr.Replace("<br/>", "\n");
                divstr = divstr.Replace("<br>", "\n");
                divstr = divstr.Replace("<div class=\"sp\">", "");
                divstr = divstr.Replace("&nbsp;", " ");
                divstr = divstr.Replace("</div>", "");
                MatchCollection mtLinkCollection = CommonUtil.GetMatchCollection(@"<a target=\W\w+\W href=\W(?'Url'[\w,\.,/,\:,-]+)\W>[\w,\.,/,\:,-]+</a>", divstr);
                if (mtLinkCollection != null)
                {
                    foreach (Match mt in mtLinkCollection)
                    {
                        divstr = divstr.Replace(mt.Groups[0].Value.ToString(), " " + mt.Groups["Url"].Value.ToString() + " ");
                    }
                }

                //去除HTTP标签
                //divstr = new Regex(@"(?m)<script[^>]*>(\w|\W)*?</script[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(divstr, "");
                //divstr = new Regex(@"(?m)<style[^>]*>(\w|\W)*?</style[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(divstr, "");
                //divstr = new Regex(@"(?m)<select[^>]*>(\w|\W)*?</select[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(divstr, "");
                //Regex objReg = new System.Text.RegularExpressions.Regex("(<[^>]+?>)|&nbsp;", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                //divstr = objReg.Replace(divstr, "");

                return divstr;
            }

            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private void GetPageInfo(object state)
        {
            WebPage page = state as WebPage;
            if (page != null)
            {
                MatchCollection mtCollection = CommonUtil.GetMatchCollection(@"<a class=\Wplant\W>(?'Current'\d+)/(?'Total'\d+)</a>", page.Html);
                if (mtCollection.Count == 2)
                {
                    this._currentPageIndex = System.Convert.ToInt32(mtCollection[0].Groups[1].Value);
                    this._totalPage = System.Convert.ToInt32(mtCollection[0].Groups[2].Value);
                    this.lblPage1.Text = this.lblPage2.Text = this._currentPageIndex + "/" + this._totalPage;
                }
                else
                {
                    this.lblPage1.Text = this.lblPage2.Text = "?/?";
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wp"></param>
        /// <returns></returns>
        private IList<string> GetImageUrls(ref string content)
        {
            MatchCollection mtCollection = CommonUtil.GetMatchCollection(@"<a target=\W_blank\W href=\Whttp://att.newsmth.net/nForum/att/[\w, %2E, %5F]+/\d+/\d+\W><img border=\W0\W title=\W单击此查看原图\W src=\W(?'ImageUrl'http://att.newsmth.net/nForum/att/[\w, %2E, %5F]+/\d+/\d+/middle)\W class=\Wresizeable\W /></a>", content);
            IList<string> imageUrlList = new List<string>();
            if (mtCollection != null)
            {
                foreach (Match mt in mtCollection)
                {
                    imageUrlList.Add(mt.Groups["ImageUrl"].Value.ToString());
                    content = content.Replace(mt.Groups[0].Value.ToString(), TopicForm._imageToken);
                }
            }

            return imageUrlList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <returns></returns>
        private Image GetImage(string imageUrl)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] temp = wc.DownloadData(imageUrl);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(temp, 0, temp.Length);
            return Image.FromStream(ms);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        private void GetThreadActionInfo(object state)
        {
            string html = state as string;
            if (string.IsNullOrEmpty(html))
            {
                return;
            }

            this._subject = this._topic;//CommonUtil.GetMatch(@"<input type=\Whidden\W name=\Wsubject\W value=\W(?'subject'Re[0-9A-Z,%,~,-]+)\W\s/>", html, 1);
            this._postUrl = CommonUtil.GetMatch(@"<form action=\W(?'post'/article/[\w, %2E, %5F]+/post/\d+)\W method=\Wpost\W>", html, 1);
            if (string.IsNullOrEmpty(this._postUrl) == false)
            {
                this._postUrl.Replace("%2E", ".");
                this._postUrl.Replace("%5F", "_");
                this._postUrl = @"http://m.newsmth.net" + this._postUrl;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateLogInStatus(object state)
        {
            WebPage wp = state as WebPage;
            if (wp != null)
            {
                if (this.GetLogInStatus(wp) == false)
                {
                    this.btnReply.Enabled = false;
                    this.lblUser.Text = "未登录";
                }
                else
                {
                    this.btnReply.Enabled = true;
                    this.lblUser.Text = CommonUtil.GetMatch(@"<a href=\W/user/logout\W accesskey=\W9\W>注销\((?'ID'[a-zA-z][a-zA-Z0-9]{1,11})\)</a>", wp.Html, 1);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        private void UpdateLoginUser(object state)
        {
            if (state != null)
            {
                this.lblUser.Text = state.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateText(object state)
        {
            WebPage wp = state as WebPage;
            if (wp != null)
            {
                this._topic = this.GetTopic(wp);
                this.Text = this._topic + " - " + GetSection(wp);
                if (string.IsNullOrEmpty(this._targetUserID) == false)
                {
                    this.Text = "只看" + this._targetUserID + " - " + this.Text;
                }
            }
        }
        #endregion

        #region Event handler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TopicForm_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.panel.Height > this.splitContainer2.Panel1.Height)
                {
                    int yPos = this.panel.Location.Y + e.Delta;
                    yPos = yPos > _margin ? _margin : yPos;
                    yPos = yPos < this.splitContainer2.Panel1.Height - this.panel.Height - _margin
                         ? this.splitContainer2.Panel1.Height - this.panel.Height - _margin : yPos;
                    this.panel.Location = new Point(this.panel.Location.X, yPos);
                    if (yPos == this.splitContainer2.Panel1.Height - this.panel.Height - _margin)
                    {
                        if (this._currentPageIndex != this._totalPage && this.panel.Enabled)
                        {
                            FetchMorePage(this._currentPageIndex + 1);
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                if (Program.LoggerEnabled)
                {
                    Program.Logger.Error(exp.Message);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (this._currentPageIndex == 1)
            {
                return;
            }

            FetchNewPage(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (this._currentPageIndex == 1)
            {
                return;
            }

            FetchNewPage(this._currentPageIndex - 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this._currentPageIndex == this._totalPage)
            {
                return;
            }

            FetchNewPage(this._currentPageIndex + 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLast_Click(object sender, EventArgs e)
        {
            if (this._currentPageIndex == this._totalPage)
            {
                return;
            }

            FetchNewPage(this._totalPage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAll_Click(object sender, EventArgs e)
        {
            FetchNewPage(-1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                int pageIndex = Int32.MaxValue;
                if (btn.Name == "btnGo1")
                {
                    if (string.IsNullOrEmpty(this.txtGoTo1.Text) == false)
                    {
                        pageIndex = System.Convert.ToInt32(this.txtGoTo1.Text);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(this.txtGoTo2.Text) == false)
                    {
                        pageIndex = System.Convert.ToInt32(this.txtGoTo2.Text);
                    }
                }

                if (pageIndex > 0 && pageIndex <= this._totalPage)
                {
                    if (this._currentPageIndex != pageIndex)
                    {
                        FetchNewPage(pageIndex);
                    }
                }

                this.txtGoTo1.Text = this.txtGoTo2.Text = "";
            }
            catch (Exception exp)
            {
                if (Program.LoggerEnabled)
                {
                    Program.Logger.Error(exp.Message);
                }

#if (DEBUG)
                Nzl.Web.Util.CommonUtil.ShowMessage(typeof(TopicForm), exp.Message);
#endif
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShrink_Click(object sender, EventArgs e)
        {
            if (this.Height > 550)
            {
                this.Height -= 50;
            }

            this.panel.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGrow_Click(object sender, EventArgs e)
        {
            if (this.Height < System.Windows.Forms.SystemInformation.VirtualScreen.Height - 250)
            {
                this.Height += 50;
            }

            this.panel.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReply_Click(object sender, EventArgs e)
        {
            NewThreadForm threadForm = new NewThreadForm("回复 - " + this.Text, this._postUrl, "Re: " + this._subject);
            threadForm.StartPosition = FormStartPosition.CenterParent;
            if (DialogResult.OK == threadForm.ShowDialog(this))
            {
                FetchNewPage(this._totalPage);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="richTextBox"></param>
        /// <param name="thread"></param>
        private void AddReplyTitleToRichTextBox(RichTextBox richTextBox, Thread thread)
        {
            if (richTextBox != null && thread != null)
            {
                richTextBox.AppendText(thread.Floor + "\t" + thread.ID + "\t\t" + thread.DateTime + "\n");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="richTextBox"></param>
        /// <param name="thread"></param>
        private void AddAllReplyContentToRichTextBox(RichTextBox richTextBox, Thread thread)
        {
            if (richTextBox != null && thread != null)
            {
                string content = CommonUtil.ReplaceSpecialChars(thread.Content);
                int imageAccumulateHeight = 0;
                int imageCount = 0;
                if (thread.ImageList != null)
                {
                    MatchCollection mtCollection = CommonUtil.GetMatchCollection(TopicForm._imageToken, thread.Content);
                    int counter = 0;
                    imageCount = mtCollection.Count;
                    foreach (Image image in thread.ImageList)
                    {
                        string imageUrl = mtCollection[counter++].Groups[0].Value.ToString();
                        int pos = content.IndexOf(imageUrl);
                        string tempContent = content.Substring(0, pos);
                        {
                            //去除HTTP标签
                            tempContent = new Regex(@"(?m)<script[^>]*>(\w|\W)*?</script[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(tempContent, "");
                            tempContent = new Regex(@"(?m)<style[^>]*>(\w|\W)*?</style[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(tempContent, "");
                            tempContent = new Regex(@"(?m)<select[^>]*>(\w|\W)*?</select[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(tempContent, "");
                            Regex objReg = new System.Text.RegularExpressions.Regex("(<[^>]+?>)|&nbsp;", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                            tempContent = objReg.Replace(tempContent, "");
                        }
                        richTextBox.AppendText(tempContent);
                        content = content.Substring(pos + TopicForm._imageToken.Length);
                        Clipboard.SetDataObject(image);
                        richTextBox.Paste(DataFormats.GetFormat(DataFormats.Bitmap));
                        imageAccumulateHeight += image.Height;
                    }
                }

                richTextBox.AppendText(content);
                ResetRichTextBoxHeight(richTextBox);
                if (imageCount > 0)
                {
                    richTextBox.Height += imageAccumulateHeight - (richTextBox.Font.Height + 2) * imageCount;
                    richTextBox.Height += richTextBox.Font.Height + imageCount * 4;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="richTextBox"></param>
        private void ResetRichTextBoxHeight(RichTextBox richTextBox)
        {
            if (richTextBox != null)
            {
                int rowCount = richTextBox.GetLineFromCharIndex(richTextBox.SelectionStart) + 1;
                richTextBox.Height = (richTextBox.Font.Height + 2) * rowCount + 4;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadControl_OnIDClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                UserForm userForm = new UserForm(e.Link.LinkData.ToString());
                userForm.StartPosition = FormStartPosition.CenterParent;
                userForm.ShowDialog(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadControl_OnQueryTypeLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                string userID = Nzl.Web.Util.CommonUtil.GetMatch(@"au=(?'ID'[a-zA-z][a-zA-Z0-9]{1,11})", e.Link.LinkData.ToString(), 1);
                TopicForm topicForm = new TopicForm(Nzl.Web.Util.CommonUtil.GetUrlBase(e.Link.LinkData.ToString()), userID);
                topicForm.StartPosition = FormStartPosition.CenterParent;
                topicForm.ShowDialog(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadControl_OnReplyLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                Thread thread = linkLabel.Tag as Thread;
                if (thread != null)
                {
                    NewThreadForm newThreadForm = new NewThreadForm(this._topic, thread.ReplyUrl, "Re: " + this._topic, GetReplyContent(thread), true);
                    newThreadForm.StartPosition = FormStartPosition.CenterParent;
                    if (DialogResult.OK == newThreadForm.ShowDialog(this))
                    {
                        FetchNewPage(this._totalPage);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadControl_OnMailLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                Thread thread = linkLabel.Tag as Thread;
                if (thread != null)
                {
                    NewMailForm newMailForm = new NewMailForm(thread.ID, "Re: " + this._topic, GetReplyContent(thread));
                    newMailForm.StartPosition = FormStartPosition.CenterParent;
                    newMailForm.ShowDialog(this);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private string GetReplyContent(Thread thread)
        {
            if (thread != null)
            {
                Regex regex = new Regex(@"\s*--\sFROM\s[\d, \., \*]+");
                string content = regex.Replace(thread.Content, "");

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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadControl_OnTransferLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadControl_OnEditLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                Thread thread = linkLabel.Tag as Thread;
                if (thread != null)
                {
                    Regex regex = new Regex(@"\s*FROM\s[\d, \., \*]+");
                    string content = regex.Replace(thread.Content, "");
                    NewThreadForm newThreadForm = new NewThreadForm(this._topic, thread.EditUrl, "Re: " + this._topic, content, false);
                    newThreadForm.StartPosition = FormStartPosition.CenterParent;
                    if (DialogResult.OK == newThreadForm.ShowDialog(this))
                    {
                        FetchNewPage(this._totalPage);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadControl_OnDeleteLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                Thread thread = linkLabel.Tag as Thread;
                if (thread != null)
                {
                    Common.MessageForm confirmForm = new Common.MessageForm("提示", "确认删除此信息？");
                    confirmForm.StartPosition = FormStartPosition.CenterParent;
                    DialogResult dlgResult = confirmForm.ShowDialog(this);
                    if (dlgResult == DialogResult.OK)
                    {
                        WebPage page = WebPageFactory.CreateWebPage(thread.DeleteUrl);
                        string result = CommonUtil.GetMatch(@"<div id=\Wm_main\W><div class=\Wsp hl f\W>(?'Result'\w+)</div>", page.Html, "Result");
                        if (result != null && result.Contains("成功"))
                        {
                            FetchNewPage(this._currentPageIndex);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenInBrowser_Click(object sender, EventArgs e)
        {
            CommonUtil.OpenUrl(this._topicUrl + "?p=" + this._currentPageIndex);
        }
        #endregion

        #region Fetch page
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        private void SetBtnEnabled(bool flag)
        {
            this.btnAll1.Enabled = flag;
            this.btnFirst1.Enabled = flag;
            this.btnGo1.Enabled = flag;
            this.btnLast1.Enabled = flag;
            this.btnNext1.Enabled = flag;
            this.btnPrev1.Enabled = flag;

            this.btnAll2.Enabled = flag;
            this.btnFirst2.Enabled = flag;
            this.btnGo2.Enabled = flag;
            this.btnLast2.Enabled = flag;
            this.btnNext2.Enabled = flag;
            this.btnPrev2.Enabled = flag;

            this.txtGoTo1.Enabled = flag;
            this.txtGoTo2.Enabled = flag;

            this.panel.Enabled = flag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        private void FetchMorePage(int index)
        {
            if (index < 0 && DialogResult.No == MessageBox.Show("Are you sure to get ALL threads?", "Waring", MessageBoxButtons.YesNo))
            {
                return;
            }

            this.bwFetchPage = new System.ComponentModel.BackgroundWorker();
            this.bwFetchPage.WorkerReportsProgress = true; // 设置可以通告进度
            this.bwFetchPage.DoWork += new System.ComponentModel.DoWorkEventHandler(bwFetchPage_DoWork);
            this.bwFetchPage.ProgressChanged += new ProgressChangedEventHandler(bwFetchPage_ProgressChanged);
            this.bwFetchPage.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwFetchPage_RunWorkerCompleted2);
            this.bwFetchPage.RunWorkerAsync(index + "/" + this._totalPage + "/" + this._topicUrl);
            this.SetBtnEnabled(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        private void FetchNewPage(int index)
        {
            if (index < 0 && DialogResult.No == MessageBox.Show("Are you sure to get ALL threads?", "Waring", MessageBoxButtons.YesNo))
            {
                return;
            }

            this.bwFetchPage = new System.ComponentModel.BackgroundWorker();
            this.bwFetchPage.WorkerReportsProgress = true; // 设置可以通告进度
            this.bwFetchPage.DoWork += new System.ComponentModel.DoWorkEventHandler(bwFetchPage_DoWork);
            this.bwFetchPage.ProgressChanged += new ProgressChangedEventHandler(bwFetchPage_ProgressChanged);
            this.bwFetchPage.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwFetchPage_RunWorkerCompleted);
            this.bwFetchPage.RunWorkerAsync(index + "/" + this._totalPage + "/" + this._topicUrl);
            this.SetBtnEnabled(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwFetchPage_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (this._uiContext != null)
                {
                    BackgroundWorker bw = sender as BackgroundWorker;
                    string pageInfo = e.Argument as string;
                    int pos = pageInfo.IndexOf("/");
                    int pageIndex = System.Convert.ToInt32(pageInfo.Substring(0, pos));
                    pageInfo = pageInfo.Substring(pos + 1);
                    pos = pageInfo.IndexOf("/");
                    int totalPage = System.Convert.ToInt32(pageInfo.Substring(0, pos));
                    string topicUrl = pageInfo.Substring(pos + 1);

                    //Fetch All Replies.
                    if (pageIndex < 0)
                    {
                        IList<Thread> threadList = new List<Thread>();
                        for (int index = 0; index < totalPage; index++)
                        {
                            string targetUrl = topicUrl + "?p=" + (index + 1).ToString();
                            if (string.IsNullOrEmpty(this._targetUserID) == false)
                            {
                                targetUrl += "&au=" + this._targetUserID;
                            }

                            WebPage wp = WebPageFactory.CreateWebPage(targetUrl);
                            if (wp != null && wp.IsGood)
                            {
                                IList<Thread> tempReplyList = GetThreads(wp);
                                if (tempReplyList != null)
                                {
                                    foreach (Thread thread in tempReplyList)
                                    {
                                        threadList.Add(new Thread(thread));
                                    }
                                }
                            }

                            bw.ReportProgress(index + 1);
                        }

                        e.Result = threadList;
                    }
                    else
                    {
                        string targetUrl = topicUrl + "?p=" + pageIndex;
                        if (string.IsNullOrEmpty(this._targetUserID) == false)
                        {
                            targetUrl += "&au=" + this._targetUserID;
                        }

                        WebPage wp = WebPageFactory.CreateWebPage(targetUrl);
                        if (wp != null && wp.IsGood)
                        {
                            this._uiContext.Post(this.UpdateLoginUser, "Unknown");
                            this._uiContext.Post(this.UpdateText,wp);
                            this._uiContext.Post(this.GetPageInfo,wp);
                            this._uiContext.Post(this.GetThreadActionInfo,wp.Html);
                            this._uiContext.Post(this.UpdateLogInStatus,wp);
                            e.Result = GetThreads(wp);
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                if (Program.LoggerEnabled)
                {
                    Program.Logger.Error(exp.Message);
                }

#if (DEBUG)
                Nzl.Web.Util.CommonUtil.ShowMessage(typeof(TopicForm), exp.Message);
#endif
                e.Result = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwFetchPage_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this._currentPageIndex = e.ProgressPercentage;
            this.lblPage1.Text = this.lblPage2.Text = e.ProgressPercentage + "/" + this._totalPage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwFetchPage_RunWorkerCompleted2(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                MessageBox.Show("Canceled");
            }
            else
            {
                UpdateThreads(e.Result as IList<Thread>, false);
            }

            SetBtnEnabled(true);
            this.panel.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwFetchPage_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                MessageBox.Show("Canceled");
            }
            else
            {
                UpdateThreads(e.Result as IList<Thread>);
            }

            SetBtnEnabled(true);
            this.panel.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="threadList"></param>
        /// <param name="clear"></param>
        private void UpdateThreads(IList<Thread> threadList)
        {
            UpdateThreads(threadList, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="threadList"></param>
        /// <param name="clear"></param>
        private void UpdateThreads(IList<Thread> threadList, bool clearCurrent)
        {
            try
            {
                if (threadList != null && threadList.Count > 0)
                {
                    int accumulateHeight = 0;
                    if (clearCurrent)
                    {
                        foreach (Control ctr in this.panel.Controls)
                        {
                            ThreadControl tc = ctr as ThreadControl;
                            if (tc != null)
                            {
                                tc.Dispose();
                            }
                        }

                        this.panel.Controls.Clear();
                    }
                    else
                    {
                        if (this.panel.Controls.Count > 0)
                        {
                            accumulateHeight = this.panel.Height - 1;
                        }
                    }

                    int width = this.panel.Width - 4;
                    bool flag = false;
                    if (threadList.Count < 100)
                    {
                        foreach (Thread thread in threadList)
                        {
                            ThreadControl tc = new ThreadControl(thread);
                            tc.OnIDLinkClicked += new LinkLabelLinkClickedEventHandler(ThreadControl_OnIDClicked);
                            tc.OnQueryTypeLinkClicked += new LinkLabelLinkClickedEventHandler(ThreadControl_OnQueryTypeLinkClicked);
                            tc.OnEditLinkClicked += new LinkLabelLinkClickedEventHandler(ThreadControl_OnEditLinkClicked);
                            tc.OnDeleteLinkClicked += new LinkLabelLinkClickedEventHandler(ThreadControl_OnDeleteLinkClicked);
                            tc.OnReplyLinkClicked += new LinkLabelLinkClickedEventHandler(ThreadControl_OnReplyLinkClicked);
                            tc.OnMailLinkClicked += new LinkLabelLinkClickedEventHandler(ThreadControl_OnMailLinkClicked);
                            tc.OnTransferLinkClicked += new LinkLabelLinkClickedEventHandler(ThreadControl_OnTransferLinkClicked);
                            tc.AllWidth = width;
                            tc.Top = accumulateHeight + 1;
                            tc.Left = 1;
                            AddAllReplyContentToRichTextBox(tc.TextBox, thread);
                            tc.Height = tc.TextBox.Height + 48;
                            if (flag)
                            {
                                tc.BackGroundColor = Color.White;
                            }

                            flag = !flag;
                            accumulateHeight += tc.Height + 1;
                            tc.TextBox.ReadOnly = true;
                            this.panel.Controls.Add(tc);
                        }
                    }
                    else
                    {
                        ThreadControl tc = new ThreadControl();
                        tc.AllWidth = width;
                        tc.Top = 1;
                        tc.Left = 1;
                        foreach (Thread thread in threadList)
                        {
                            tc.TextBox.AppendText("-------**********------\n");
                            AddReplyTitleToRichTextBox(tc.TextBox, thread);
                            AddAllReplyContentToRichTextBox(tc.TextBox, thread);
                            tc.TextBox.AppendText("\n");
                        }

                        ResetRichTextBoxHeight(tc.TextBox);
                        tc.Height = tc.TextBox.Height + 48;
                        this.panel.Controls.Add(tc);
                    }

                    if (clearCurrent)
                    {
                        this.panel.Location = new Point(this.panel.Location.X, this._margin);
                    }

                    this.panel.Height = accumulateHeight + 1;
                }
                else
                {
                    this.Text = "指定的文章不存在或链接错误";
                }
            }
            catch (Exception exp)
            {
                if (Program.LoggerEnabled)
                {
                    Program.Logger.Error(exp.Message);
                }

#if (DEBUG)
                Nzl.Web.Util.CommonUtil.ShowMessage(this, exp.Message);
#endif
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="richtxtbox"></param>
        /// <param name="thread"></param>
        /// <returns></returns>
        private int AppendThread(RichTextBox richtxtbox, Thread thread)
        {
            return 0;
        }
        #endregion
    }
}
