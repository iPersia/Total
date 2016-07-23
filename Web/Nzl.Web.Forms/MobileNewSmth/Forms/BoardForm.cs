namespace Nzl.Web.Forms.MobileNewSmth.Forms
{
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using Nzl.Web.Util;
    using Nzl.Web.Page;
    using Nzl.Web.Forms.MobileNewSmth.Datas;
    using Nzl.Web.Forms.MobileNewSmth.Controls;
    using Nzl.Web.Forms.MobileNewSmth.Utils;

    /// <summary>
    /// Class.
    /// </summary>
    public partial class BoardForm : BaseForm
    {
        #region Variable
        /// <summary>
        /// 
        /// </summary>
        private int _margin = 4;

        /// <summary>
        /// 
        /// </summary>
        private System.Threading.SynchronizationContext _uiContext = WindowsFormsSynchronizationContext.Current;
        #endregion

        #region Ctor.
        /// <summary>
        /// Ctor.
        /// </summary>
        public BoardForm()
        {
            InitializeComponent();
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
        public BoardForm(string boardUrl)
            : this()
        {
            this.SetBaseUrl(boardUrl);
        }
        #endregion

        #region override
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.panel.MouseWheel += new MouseEventHandler(BoardForm_MouseWheel);
            FetchPage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        protected override void DoWork(UrlInfo info)
        {
            base.DoWork(info);
            IList<Topic> topics = TopicFactory.GetTopics(info.WebPage);
            IList<BaseItem> items = new List<BaseItem>();
            foreach (Topic topic in topics)
            {
                items.Add(topic);
            }

            info.Result = items;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        protected override void WorkerCompleted(UrlInfo info)
        {
            base.WorkerCompleted(info);
            IList<Topic> topics = info.Result as IList<Topic>;
            if (topics != null)
            {
                int count = 1;
#if (DEBUG)
                foreach (Topic tpc in topics)
                {
                    tpc.Title = "<" + info.Index.ToString().PadLeft(2, '0') + "-"
                              + (count++).ToString().PadLeft(2, '0') 
                              + "> " + tpc.Title;
                }
#endif
                this.UpdateTopics(topics, !info.IsAppend);
            }

            UpdateBoardTitle(info.WebPage);

            this.lblPage1.Text = info.Index.ToString().PadLeft(3, '0') + "/" + info.Total.ToString().PadLeft(3, '0');
            this.lblPage2.Text = this.lblPage1.Text;
            if (LoginForm.IsLogin)
            {
                this.lblUser.Text = LoginForm.UserID;
            }
            else
            {
                this.lblUser.Text = "Not Login!";
            }

            SetBtnEnabled(true);
        }
        #endregion

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
                           + @"(?'TopicUrl'/article/[\w, %2E, %5F, \., _]+/\d+)\W(| "
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
            topic.IsTop = !String.IsNullOrEmpty(CommonUtil.GetMatch(pattern, content, "IsTop"));
            topic.Title = CommonUtil.GetMatch(pattern, content, "Title");
            topic.Replies = System.Convert.ToInt32(CommonUtil.GetMatch(pattern, content, "Replies"));
            topic.CreateDateTime = CommonUtil.GetMatch(pattern, content, "CreateDateTime");
            topic.CreateID = CommonUtil.GetMatch(pattern, content, "CreateID");
            topic.LastThreadDateTime = CommonUtil.GetMatch(pattern, content, "LastThreadDateTime");
            topic.LastThreadID = CommonUtil.GetMatch(pattern, content, "LastThreadID");
            return topic;
        }
        #endregion

        #region event handler.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFirst_Click(object sender, EventArgs e)
        {
            this.SetUrlInfo(1, false);
            this.FetchPage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrev_Click(object sender, EventArgs e)
        {
            this.SetUrlInfo(false);
            this.FetchPrevPage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            this.SetUrlInfo(false);
            this.FetchNextPage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLast_Click(object sender, EventArgs e)
        {
            this.SetUrlInfo(false);
            this.FetchLastPage();
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

                this.SetUrlInfo(pageIndex, false);
                this.FetchPage();

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
            CommonUtil.OpenUrl(this.GetCurrentUrl());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoardForm_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {

                int panelContainerHeight = this.splitContainer2.Panel1.Height; //panel容器高度
#if (DEBUG)
                System.Diagnostics.Debug.WriteLine("---------------------***TopicForm_MouseWheel***---------------------");
                System.Diagnostics.Debug.WriteLine("Sender is:" + sender.GetType().ToString());
                System.Diagnostics.Debug.WriteLine("panelContainerHeight:" + panelContainerHeight);
#endif
                if (this.panel.Height > panelContainerHeight)
                {
#if (DEBUG)
                    System.Diagnostics.Debug.WriteLine("oldYPos:" + this.panel.Location.Y);
                    System.Diagnostics.Debug.WriteLine("Delta  :" + e.Delta);
#endif
                    int newYPos = this.panel.Location.Y + e.Delta;
                    newYPos = newYPos > this._margin ? this._margin : newYPos;
                    newYPos = newYPos < panelContainerHeight - this.panel.Height - this._margin
                         ? panelContainerHeight - this.panel.Height - this._margin : newYPos;
#if (DEBUG)
                    System.Diagnostics.Debug.WriteLine("newYPos:" + newYPos);
#endif
                    this.panel.Location = new Point(this.panel.Location.X, newYPos);
                    if (newYPos == panelContainerHeight - this.panel.Height - this._margin)
                    {
                        this.SetUrlInfo(true);
                        this.SetBtnEnabled(!this.FetchNextPage());
#if (DEBUG)
                        System.Diagnostics.Debug.WriteLine("FetchNextPage: XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" + newYPos);
#endif
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
        #endregion

        #region Fetch page.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        private void SetBtnEnabled(bool flag)
        {
            this.Enabled = flag;

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
        /// <param name="wp"></param>
        private void UpdateBoardTitle(WebPage wp)
        {
            if (wp != null && wp.IsGood)
            {
                this.Text = CommonUtil.GetMatch("<div class=\"menu sp\"><a href=\"/\" accesskey=\"0\">首页</a>\\|版面-(?'Region'.+)</div><div id=\"m_main\">", wp.Html, 1);
            }
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
                    //清空现有列表
                    if (clearCurrent)
                    {
                        this.panel.Controls.Clear();
                    }

                    int accumulateHeight = 0;
                    if (this.panel.Controls.Count > 0)
                    {
                        accumulateHeight = this.panel.Height;
                    }

                    int width = this.panel.Width - 2;
                    bool flag = false;

                    foreach (Topic topic in topicList)
                    {
                        TopicControl tc = new TopicControl(topic);
                        tc.Width = width;
                        tc.Top = accumulateHeight + 1;
                        tc.Left = 1;
                        tc.OnTopicLinkClicked += new LinkLabelLinkClickedEventHandler(TopicControl_OnTopicLinkClicked);
                        tc.OnCreateIDLinkClicked += new LinkLabelLinkClickedEventHandler(TopicControl_OnCreateIDLinkClicked);
                        tc.OnLastIDLinkClicked += new LinkLabelLinkClickedEventHandler(TopicControl_OnLastIDLinkClicked);

                        if (flag)
                        {
                            tc.BackColor = Color.White;
                        }

                        if (topic.IsTop)
                        {
                            tc.ForeColor = Color.Red;
                        }

                        flag = !flag;
                        accumulateHeight += tc.Height + 1;
                        this.panel.Controls.Add(tc);
                    }
                }
                else
                {
                    this.Text = "指定的版块不存在或链接错误";
                }

                int panelHeight = 2;
                foreach (Control ctr in this.panel.Controls)
                {
                    panelHeight += ctr.Height + 1;
                }

                this.panel.Height = panelHeight;


                //新取页面
                if (clearCurrent)
                {
                    this.panel.Location = new Point(this.panel.Location.X, this._margin);
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
                //TopicForm topicForm = new TopicForm(e.Link.LinkData.ToString());
                //topicForm.StartPosition = FormStartPosition.WindowsDefaultLocation;
                //topicForm.Show();

                //MobileNewSmthForm.Browser.AddTopic(e.Link.LinkData.ToString(), linkLabel.Text);
                //MobileNewSmthForm.Browser.Show();
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
