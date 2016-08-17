namespace Nzl.Smth.Containers
{
    using System;
    using System.Reflection;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using Nzl.Web.Util;
    using Nzl.Web.Page;
    using Nzl.Smth.Datas;
    using Nzl.Smth.Controls;
    using Nzl.Smth.Utils;
    using Nzl.Smth.Logger;

    /// <summary>
    /// Class.
    /// </summary>
    public partial class BoardBrowserControl : BaseContainer<TopicControl, Topic>
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
            this.panel.MouseWheel += new MouseEventHandler(BoardBrowserControl_MouseWheel);
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
        /// <param name="wp"></param>
        /// <returns></returns>
        protected override IList<Topic> GetItems(WebPage wp)
        {
            IList<Topic> topics = TopicFactory.CreateTopics(wp);
            //foreach (Topic topic in topics)
            //{
            //    if (topic.Mode == TopicMode.Normal ||
            //        topic.Mode == TopicMode.Magic)
            //    {
            //        topics.Add(topic);
            //    }
            //}

            return topics;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        protected override void WorkCompleted(UrlInfo<TopicControl, Topic> info)
        {
            base.WorkCompleted(info);
            this.UpdateBoardTitle(info.WebPage);
            this.lblPage1.Text = info.Index.ToString().PadLeft(3, '0') + "/" + info.Total.ToString().PadLeft(3, '0');
            this.lblPage2.Text = this.lblPage1.Text;

            if (this.GetContainer().Height < this.panelContainer.Height)
            {
                this.SetUrlInfo(true);
                this.FetchNextPage();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="oeFlag"></param>
        protected override void SetControl(TopicControl ctl, bool oeFlag)
        {
            base.SetControl(ctl, oeFlag);
            Topic topic = ctl.Tag as Topic;
            if (topic != null)
            {
                if (topic.Mode == TopicMode.Top)
                {
                    ctl.ForeColor = Color.Red;
                }

                if (topic.Mode == TopicMode.Magic)
                {
                    ctl.ForeColor = Color.FromArgb(255, 128, 128);
                }
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
        protected override TopicControl CreateControl(Topic item)
        {
            return this.CreateTopicControl(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        protected override void SetControlEnabled(bool flag)
        {
            base.SetControlEnabled(flag);
            //this.scContainer.Enabled = flag;
            this.btnRefresh.Enabled = true;

            this.panel.Enabled = flag;

            this.btnFirst1.Enabled = flag;
            this.btnGo1.Enabled = flag;
            this.btnLast1.Enabled = flag;
            this.btnNext1.Enabled = flag;
            this.btnPrev1.Enabled = flag;
            this.txtGoTo1.Enabled = flag;

            this.btnFirst2.Enabled = flag;
            this.btnGo2.Enabled = flag;
            this.btnLast2.Enabled = flag;
            this.btnNext2.Enabled = flag;
            this.btnPrev2.Enabled = flag;
            this.txtGoTo2.Enabled = flag;

            //this.btnRefresh.Enabled = flag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thread"></param>
        /// <returns></returns>
        private TopicControl CreateTopicControl(Topic topic)
        {
            TopicControl tc = new TopicControl();
            tc.Initialize(topic);
            tc.Name = "tc" + topic.ID;
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
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.SetUrlInfo(false);
            this.FetchPage();
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
                if (Logger.Enabled)
                {
                    Logger.Instance.Error(exp.Message + "\n" + exp.StackTrace);
                }

#if (DEBUG)
                CommonUtil.ShowMessage(typeof(TopicBrowserControl), exp.Message);
#endif
            }
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
        private void BoardBrowserControl_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                int panelContainerHeight = this.splitContainer2.Panel1.Height; //panel容器高度

#if (X)
                System.Diagnostics.Debug.WriteLine("********************---BoardBrowserControl_MouseWheel---********************");
                //System.Diagnostics.Debug.WriteLine("Sender is:" + sender.GetType().ToString());
                //System.Diagnostics.Debug.WriteLine("HashCode is:" + sender.GetHashCode());
                //System.Diagnostics.Debug.WriteLine("panelContainerHeight:" + panelContainerHeight);
#endif
                if (this.panel.Height > panelContainerHeight)
                {
#if (X)
                    //System.Diagnostics.Debug.WriteLine("oldYPos:" + this.panel.Location.Y);
                    //System.Diagnostics.Debug.WriteLine("Delta  :" + e.Delta);
#endif
                    int newYPos = this.panel.Location.Y + e.Delta;
                    newYPos = newYPos > this._margin ? this._margin : newYPos;
                    newYPos = newYPos < panelContainerHeight - this.panel.Height - this._margin
                         ? panelContainerHeight - this.panel.Height - this._margin : newYPos;
#if (X)
                    System.Diagnostics.Debug.WriteLine("newYPos:" + newYPos);
#endif
                    this.panel.Location = new Point(this.panel.Location.X, newYPos);
                    if (newYPos == panelContainerHeight - this.panel.Height - this._margin)
                    {
#if (X)
                        System.Diagnostics.Debug.WriteLine("Cause BaseContainer's FetchNextPage!");
#endif
                        this.SetUrlInfo(true);
                        this.FetchNextPage();
                    }
                }
#if (X)
                System.Diagnostics.Debug.WriteLine("--------------------***BoardBrowserControl_MouseWheel***--------------------");
#endif
            }
            catch (Exception exp)
            {
                if (Logger.Enabled)
                {
                    Logger.Instance.Error(exp.Message + "\n" + exp.StackTrace);
                }
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

        #region private
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wp"></param>
        private void UpdateBoardTitle(WebPage wp)
        {
            if (wp != null && wp.IsGood)
            {
                this.Text = SmthUtil.GetBoard(wp);
            }
        }        
        #endregion
    }
}
