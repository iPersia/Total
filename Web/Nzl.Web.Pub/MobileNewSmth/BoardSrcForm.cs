namespace Nzl.Web.Pub.MobileNewSmth
{
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using Nzl.Web.Util;
    using Nzl.Web.Page;

    /// <summary>
    /// Class.
    /// </summary>
    public partial class BoardSrcForm : Form
    {
        #region Variable
        /// <summary>
        /// 
        /// </summary>
        private string _boardUrl;

        /// <summary>
        /// 
        /// </summary>
        private int _currentPageIndex = 0;

        /// <summary>
        /// 
        /// </summary>
        private int _totalPage = 0;
        #endregion

        #region Ctor.
        /// <summary>
        /// Ctor.
        /// </summary>
        public BoardSrcForm()
        {
            InitializeComponent();
            this.panel.MouseWheel += new MouseEventHandler(panel_MouseWheel);
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

        /// <summary>
        /// Ctor.
        /// </summary>
        public BoardSrcForm(string boardUrl)
            : this()
        {
            this._boardUrl = boardUrl;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            FetchNewPage(1);
        }

        #region Get topic
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private IList<Topic> GetTopicList(WebPage page)
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
        private Topic GetTopic(string content)
        {
            string pattern = @"(<li>|<li class=\Whla\W>)<div><a href=\W"
                           + @"(?'TopicUrl'/article/[\w, %2E, %5F, \., _]+/\d+)\W(| class=\W\w+\W)>"
                           + @"(?'Title'[\w, \W]+)</a>\("
                           + @"(?'Count'\d+)\)</div><div>"
                           + @"(?'CreateDT'[\d, \:, \-, \s]+)<a href=\W/user/query/\w+\.?\W>"
                           + @"(?'CreateID'\w+)\.?</a>\|"
                           + @"(?'LastDT'[\d, \:, \-, \s]+)<a href=\W/user/query/\w+\.?\W>"
                           + @"(?'LastID'\w+)\.?</a></div></li>";

            Topic topic = new Topic();
            content = content.Replace("&nbsp;", " ");
            topic.Uri = @"http://m.newsmth.net" + CommonUtil.GetMatch(pattern, content, 3);
            topic.Title = CommonUtil.GetMatch(pattern, content, 4);
            topic.Replies = System.Convert.ToInt32(CommonUtil.GetMatch(pattern, content, 5));
            topic.CreateDateTime = CommonUtil.GetMatch(pattern, content, 6);
            topic.CreateID = CommonUtil.GetMatch(pattern, content, 7);
            topic.LastThreadDateTime = CommonUtil.GetMatch(pattern, content, 8);
            topic.LastThreadID = CommonUtil.GetMatch(pattern, content, 9);
            return topic;
        }
        #endregion

        #region Button click event handler.
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
                CommonUtil.ShowMessage(typeof(TopicForm), exp.Message);
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
        private void btnOpenInBrower_Click(object sender, EventArgs e)
        {
            CommonUtil.OpenUrl(this._boardUrl + "?p=" + this._currentPageIndex);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel_MouseWheel(object sender, MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            Panel panel = sender as Panel;
            if (panel != null)
            {
                if (panel.VerticalScroll.Value >= ((panel.VerticalScroll.Maximum - panel.VerticalScroll.LargeChange)))
                {
                    if (this._currentPageIndex != this._totalPage)
                    {
                        FetchMorePage(this._currentPageIndex + 1);
                    }
                }
            }
        }

        #region Fetch page.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        private void SetBtnEnabled(bool flag)
        {
            this.btnFirst1.Enabled = flag;
            this.btnGo1.Enabled = flag;
            this.btnLast1.Enabled = flag;
            this.btnNext1.Enabled = flag;
            this.btnPrev1.Enabled = flag;

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
            this.bwFetchPage.DoWork += new System.ComponentModel.DoWorkEventHandler(bwFetchPage_DoWork);
            this.bwFetchPage.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwFetchPage_RunWorkerCompleted2);
            this.bwFetchPage.RunWorkerAsync(index + "/" + this._totalPage + "/" + this._boardUrl);
            this.SetBtnEnabled(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        private void FetchNewPage(int index)
        {
            if (index < 0)
            {
                return;
            }

            this.bwFetchPage = new System.ComponentModel.BackgroundWorker();
            this.bwFetchPage.DoWork += new System.ComponentModel.DoWorkEventHandler(bwFetchPage_DoWork);
            this.bwFetchPage.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwFetchPage_RunWorkerCompleted);
            this.bwFetchPage.RunWorkerAsync(index + "/" + this._totalPage + "/" + this._boardUrl);
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
                BackgroundWorker bw = sender as BackgroundWorker;
                string pageInfo = e.Argument as string;
                int pos = pageInfo.IndexOf("/");
                int pageIndex = System.Convert.ToInt32(pageInfo.Substring(0, pos));
                pageInfo = pageInfo.Substring(pos + 1);
                pos = pageInfo.IndexOf("/");
                int totalPage = System.Convert.ToInt32(pageInfo.Substring(0, pos));
                string boardUrl = pageInfo.Substring(pos + 1);

                this.Invoke((EventHandler)delegate
                {
                    this.lblUser.Text = "Unkown!";
                });

                string targetUrl = boardUrl + "?p=" + pageIndex;
                WebPage wp = WebPageFactory.CreateWebPage(targetUrl);
                if (wp != null && wp.IsGood)
                {
                    this.Invoke((EventHandler)delegate
                    {
                        this.UpdateBoardTitle(wp);
                        this.UpdatePageInfo(wp);
                        this.UpdateLogInStatus(wp);
                    });

                    e.Result = GetTopicList(wp);
                }
            }
            catch (Exception exp)
            {
                if (Program.LoggerEnabled)
                {
                    Program.Logger.Error(exp.Message);
                }

#if (DEBUG)
                CommonUtil.ShowMessage(typeof(TopicForm), exp.Message);
#endif
                e.Result = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wp"></param>
        private void UpdateBoardTitle(WebPage wp)
        {
            if (wp != null && wp.IsGood)
            {
                this.Text = CommonUtil.GetMatch(@"<div class=\Wmenu sp\W>(?'BoardTitle'版面\-[\w, %2E, %5F, \., _, \(, \)]+)</div><div id=\Wm_main\W>", wp.Html, 1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private void UpdatePageInfo(WebPage page)
        {
            MatchCollection mtCollection = CommonUtil.GetMatchCollection(@"<a class=\Wplant\W>(?'Current'\d+)/(?'Total'\d+)</a>", page.Html);
            if (mtCollection.Count == 2)
            {
                this._currentPageIndex = System.Convert.ToInt32(mtCollection[0].Groups[1].Value);
                this._totalPage = System.Convert.ToInt32(mtCollection[0].Groups[2].Value);

                this.lblPage1.Text = this.lblPage2.Text = this._currentPageIndex + "/" + this._totalPage;
            }
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
        private void UpdateLogInStatus(WebPage wp)
        {
            if (this.GetLogInStatus(wp) == false)
            {
                this.lblUser.Text = "未登录";
            }
            else
            {
                this.lblUser.Text = CommonUtil.GetMatch(@"<a href=\W/user/logout\W accesskey=\W9\W>注销\((?'ID'[a-zA-z][a-zA-Z0-9]{1,11})\)</a>", wp.Html, 1);
            }
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
                UpdateTopics(e.Result as IList<Topic>, false);
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
                UpdateTopics(e.Result as IList<Topic>);
            }

            SetBtnEnabled(true);
            this.panel.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="replyList"></param>
        /// <param name="clear"></param>
        private void UpdateTopics(IList<Topic> topicList)
        {
            UpdateTopics(topicList, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="replyList"></param>
        /// <param name="clear"></param>
        private void UpdateTopics(IList<Topic> topicList, bool clearCurrent)
        {
            try
            {
                if (topicList != null && topicList.Count > 0)
                {
                    if (clearCurrent)
                    {
                        this.panel.Controls.Clear();
                    }

                    int accumulateHeight = 0;
                    if (this.panel.Controls.Count > 0)
                    {
                        accumulateHeight = this.panel.Height;
                    }

                    int width = this.panel.Width - 19;
                    bool flag = false;

                    foreach (Topic topic in topicList)
                    {
                        TopicControl tc = new TopicControl(topic.Uri, topic.Title, topic.Replies, topic.CreateDateTime, topic.CreateID, topic.LastThreadDateTime, topic.LastThreadID);
                        tc.Width = width;
                        tc.Top = accumulateHeight + 1;
                        tc.OnTopicLinkClicked += new LinkLabelLinkClickedEventHandler(TopicControl_OnTopicLinkClicked);
                        tc.OnCreateIDLinkClicked += new LinkLabelLinkClickedEventHandler(TopicControl_OnCreateIDLinkClicked);
                        tc.OnLastIDLinkClicked += new LinkLabelLinkClickedEventHandler(TopicControl_OnLastIDLinkClicked);

                        if (flag)
                        {
                            tc.BackColor = Color.White;
                        }

                        flag = !flag;
                        accumulateHeight += tc.Height + 3;
                        this.panel.Controls.Add(tc);
                    }
                }
                else
                {
                    this.Text = "指定的版块不存在或链接错误";
                }
            }
            catch (Exception exp)
            {
                if (Program.LoggerEnabled)
                {
                    Program.Logger.Error(exp.Message);
                }

#if (DEBUG)
                CommonUtil.ShowMessage(this, exp.Message);
#endif
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TopicControl_OnTopicLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                TopicForm topicForm = new TopicForm(e.Link.LinkData.ToString());
                topicForm.StartPosition = FormStartPosition.WindowsDefaultLocation;
                topicForm.Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TopicControl_OnCreateIDLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
        private void TopicControl_OnLastIDLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                UserForm userForm = new UserForm(e.Link.LinkData.ToString());
                userForm.StartPosition = FormStartPosition.CenterScreen;
                userForm.ShowDialog(this);
            }
        }
        #endregion
    }
}
