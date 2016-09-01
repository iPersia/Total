namespace Nzl.Smth.Controls.Containers
{
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Nzl.Smth.Controls.Base;
    using Nzl.Smth.Controls.Elements;
    using Nzl.Smth.Datas;
    using Nzl.Smth.Utils;
    using Nzl.Smth.Logger;
    using Nzl.Web.Util;
    using Nzl.Web.Page;

    /// <summary>
    /// Class.
    /// </summary>
    public partial class TopicControlContainer : BaseControlContainer<TopicControl, Topic>
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

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<BoardSettingEventArgs> OnBoardSettingsClicked;
        #endregion

        #region Variable
        /// <summary>
        /// 
        /// </summary>
        private BoardSettingEventArgs _Settings = null;

        /// <summary>
        /// 
        /// </summary>
        private Timer _updatingTimer = new Timer();
        #endregion

        #region Ctor.
        /// <summary>
        /// Ctor.
        /// </summary>
        public TopicControlContainer()
        {
            InitializeComponent();
            this._updatingTimer.Tick += _updatingTimer_Tick;
            this._Settings = new BoardSettingEventArgs();
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        public string Url
        {
            set
            {
                this.SetBaseUrl(value);
            }
        }
        #endregion

        #region override
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wp"></param>
        /// <returns></returns>
        protected override IList<Topic> GetItems(WebPage wp)
        {
            IList<Topic> topics = TopicFactory.CreateTopics(wp);
            if (this._Settings.IsShowTop == false)
            {
                IList<Topic> tps = new List<Topic>();
                foreach (Topic topic in topics)
                {
                    if (topic.Mode == TopicMode.Normal ||
                        topic.Mode == TopicMode.Magic)
                    {
                        tps.Add(topic);
                    }
                    else
                    {
                        this.RecyclingItem(topic);
                    }
                }

                topics.Clear();
                return tps;
            }

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
            this.lblPage.Text = info.Index.ToString().PadLeft(3, '0') + "/" + info.Total.ToString().PadLeft(3, '0');
            if (this.GetPanel().Height < this.panelContainer.Height)
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
            if (ctl.Data != null)
            {
                if (ctl.Data.Mode == TopicMode.Top)
                {
                    ctl.ForeColor = Color.Red;
                }

                if (ctl.Data.Mode == TopicMode.Magic)
                {
                    ctl.ForeColor = Color.FromArgb(255, 128, 128);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Panel GetPanel()
        {
            return this.panel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Panel GetPanelContainer()
        {
            return this.panelContainer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="item"></param>
        protected override void InitializeControl(TopicControl ctl, Topic item)
        {
            base.InitializeControl(ctl, item);
            if (ctl != null && item != null)
            {
                ctl.Name = "tc" + item.ID;
                ctl.OnTopicLinkClicked += new LinkLabelLinkClickedEventHandler(TopicControl_OnTopicLinkClicked);
                ctl.OnCreateIDLinkClicked += new LinkLabelLinkClickedEventHandler(TopicControl_OnCreateIDLinkClicked);
                ctl.OnLastIDLinkClicked += new LinkLabelLinkClickedEventHandler(TopicControl_OnLastIDLinkClicked);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ctl"></param>
        protected override void RecylingControl(TopicControl ctl)
        {
            base.RecylingControl(ctl);
            if (ctl != null)
            {
                ctl.OnTopicLinkClicked -= new LinkLabelLinkClickedEventHandler(TopicControl_OnTopicLinkClicked);
                ctl.OnCreateIDLinkClicked -= new LinkLabelLinkClickedEventHandler(TopicControl_OnCreateIDLinkClicked);
                ctl.OnLastIDLinkClicked -= new LinkLabelLinkClickedEventHandler(TopicControl_OnLastIDLinkClicked);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        protected override void SetControlEnabled(bool flag)
        {
            base.SetControlEnabled(flag);
            this.btnRefresh.Enabled = true;

            this.panel.Enabled = flag;

            this.btnFirst.Enabled = flag;
            this.btnGo.Enabled = flag;
            this.btnLast.Enabled = flag;
            this.btnNext.Enabled = flag;
            this.btnPrev.Enabled = flag;
            this.txtGoTo.Enabled = flag;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Recycling()
        {
            base.Recycling();

            this._Settings = new BoardSettingEventArgs();
            this._updatingTimer.Stop();
        }
        #endregion

        #region event handler.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _updatingTimer_Tick(object sender, EventArgs e)
        {
            this.SetUrlInfo(false);
            this.FetchPage();
        }

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
                int pageIndex = Int32.MaxValue;
                if (string.IsNullOrEmpty(this.txtGoTo.Text) == false)
                {
                    pageIndex = System.Convert.ToInt32(this.txtGoTo.Text);
                }
         

                this.SetUrlInfo(pageIndex, false);
                this.FetchPage();
                this.txtGoTo.Text = "";
            }
            catch (Exception exp)
            {
                if (Logger.Enabled)
                {
                    Logger.Instance.Error(exp.Message + "\n" + exp.StackTrace);
                }

#if (DEBUG)
                CommonUtil.ShowMessage(typeof(ThreadControlContainer), exp.Message);
#endif
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSettings_Click(object sender, EventArgs e)
        {
            if (this.OnBoardSettingsClicked != null)
            {
                BoardSettingEventArgs bsEventArgs = new BoardSettingEventArgs();
                bsEventArgs.IsShowTop = this._Settings.IsShowTop;
                bsEventArgs.AutoUpdating = this._Settings.AutoUpdating;
                bsEventArgs.UpdatingInterval = this._Settings.UpdatingInterval;
                this.OnBoardSettingsClicked(sender, bsEventArgs);
                if (bsEventArgs.Tag != null && bsEventArgs.Tag.ToString() == "Updated")
                {
                    this._Settings.IsShowTop = bsEventArgs.IsShowTop;
                    this._Settings.AutoUpdating = bsEventArgs.AutoUpdating;
                    this._Settings.UpdatingInterval = bsEventArgs.UpdatingInterval;
                    ApplyBoardSetting();
                }
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

        /// <summary>
        /// 
        /// </summary>
        /// <summary>
        /// 
        /// </summary>
        private void ApplyBoardSetting()
        {
            this._updatingTimer.Stop();
            if (this._Settings.AutoUpdating)
            {
                this._updatingTimer.Interval = this._Settings.UpdatingInterval * 1000;
                this._updatingTimer.Start();
            }

            this.SetUrlInfo(false);
            this.FetchPage();
        }
        #endregion
    }
}
