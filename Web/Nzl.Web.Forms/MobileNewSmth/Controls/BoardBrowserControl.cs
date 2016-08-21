namespace Nzl.Web.Forms.MobileNewSmth.Controls
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
    public partial class BoardBrowserControl : BaseControl
    {
        #region Event
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnTopicCreateIDLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnTopicLastIDLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnTopicLinkClicked;
        #endregion

        #region Variable
        /// <summary>
        /// 
        /// </summary>
        private int _margin = 4;
        #endregion

        #region Ctor.
        /// <summary>
        /// Ctor.
        /// </summary>
        public BoardBrowserControl()
        {
            InitializeComponent();
            this.MouseWheel += new MouseEventHandler(BoardForm_MouseWheel);
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
        public BoardBrowserControl(string boardUrl)
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
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.SetUrlInfo(false);
            this.FetchPage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        protected override void DoWork(UrlInfo info)
        {
            base.DoWork(info);
            IList<Topic> topics = TopicFactory.GetTopics(info.WebPage);
            IList<BaseData> items = new List<BaseData>();
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
            UpdateBoardTitle(info.WebPage);
            this.lblPage1.Text = info.Index.ToString().PadLeft(3, '0') + "/" + info.Total.ToString().PadLeft(3, '0');
            this.lblPage2.Text = this.lblPage1.Text;
            SetBtnEnabled(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="oeFlag"></param>
        protected override void SetControl(Control ctl, bool oeFlag)
        {
            base.SetControl(ctl, oeFlag);
            Topic topic = ctl.Tag as Topic;
            if (topic != null && topic.IsTop)
            {
                ctl.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Panel GetContainer()
        {
            return this.panel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override Control CreateControl(BaseData item)
        {
            return this.CreateThreadControl(item as Topic);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thread"></param>
        /// <returns></returns>
        private TopicControl CreateThreadControl(Topic topic)
        {
            TopicControl tc = new TopicControl(topic);
            tc.Width = this.panel.Width - 2;;
            tc.Left = 1;
            tc.OnTopicLinkClicked += new LinkLabelLinkClickedEventHandler(TopicControl_OnTopicLinkClicked);
            tc.OnCreateIDLinkClicked += new LinkLabelLinkClickedEventHandler(TopicControl_OnCreateIDLinkClicked);
            tc.OnLastIDLinkClicked += new LinkLabelLinkClickedEventHandler(TopicControl_OnLastIDLinkClicked);
            return tc;
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
                if (this.OnTopicLinkClicked != null)
                {
                    this.OnTopicLinkClicked(sender, e);
                }
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
                if (this.OnTopicCreateIDLinkClicked != null)
                {
                    this.OnTopicCreateIDLinkClicked(sender, e);
                }
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
                if (this.OnTopicLastIDLinkClicked != null)
                {
                    this.OnTopicLastIDLinkClicked(sender, e);
                }
            }
        }
        #endregion
    }
}
