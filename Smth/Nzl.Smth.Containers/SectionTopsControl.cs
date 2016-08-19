namespace Nzl.Smth.Containers
{
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using Nzl.Web.Util;
    using Nzl.Web.Page;
    using Nzl.Smth.Common;
    using Nzl.Smth.Datas;
    using Nzl.Smth.Controls;
    using Nzl.Smth.Utils;

    /// <summary>
    /// 
    /// </summary>
    public partial class SectionTopsControl : BaseContainer<TopControl, Topic>
    {
        #region event
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnTopLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnTopBoardLinkClicked;
        #endregion

        #region variable
        /// <summary>
        /// 
        /// </summary>
        private Control _parentControl = null;

        /// <summary>
        /// 
        /// </summary>
        private Timer _updatingTimer = new Timer();
        #endregion

        #region Ctor.
        /// <summary>
        /// 
        /// </summary>
        SectionTopsControl()
        {
            InitializeComponent();
            Configurations.OnSectionTopsUpdatingIntervalChanged += Configuration_OnSectionTopsUpdatingIntervalChanged;
            this.panelContainer.Size = new Size(this.Width - 10, TopControl.ControlHeight * 10 + 12);
            this.Height = this.panelContainer.Height + 11;
            this.Text = "Section top topic";            
        }
        
        /// <summary>
        /// 
        /// </summary>
        public SectionTopsControl(string url)
            : this()
        {
            this.SetBaseUrl(url);
            this._updatingTimer.Interval = Configurations.SectionTopsUpdatingInterval;
            this._updatingTimer.Tick += new EventHandler(_updatingTimer_Tick);
            this._updatingTimer.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctl"></param>
        public void SetParent(Control ctl)
        {
            this._parentControl = ctl;
        }
        #endregion

        #region override
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Panel GetContainer()
        {
            return this.panelContainer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override string GetUrl(UrlInfo<TopControl, Topic> info)
        {
            return info.BaseUrl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        protected override void DoWork(UrlInfo<TopControl, Topic> info)
        {
            base.DoWork(info);
            info.Subject = SmthUtil.GetSectionName(info.WebPage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wp"></param>
        /// <returns></returns>
        protected override IList<Topic> GetItems(WebPage wp)
        {
            IList<Topic> list = TopicFactory.CreateTop10Topics(wp);
#if (DEBUG)
            System.Diagnostics.Debug.WriteLine("SectionTopsControl - GetItems - Item count is " + list.Count);
#endif
            return list;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="item"></param>
        protected override void InitializeControl(TopControl ctl, Topic topic)
        {
            base.InitializeControl(ctl, topic);
            if (ctl != null && topic != null)
            {
                ctl.Name = "tcTop" + topic.ID;
                ctl.OnTopLinkClicked += TopControl_OnTopLinkClicked;
                ctl.OnTopBoardLinkClicked += TopControl_OnTopBoardLinkClicked;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ctl"></param>
        protected override void RecylingControl(TopControl ctl)
        {
            base.RecylingControl(ctl);
            if (ctl != null)
            {
                ctl.OnTopLinkClicked -= TopControl_OnTopLinkClicked;
                ctl.OnTopBoardLinkClicked -= TopControl_OnTopBoardLinkClicked;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        protected override void WorkCompleted(UrlInfo<TopControl, Topic> info)
        {
            base.WorkCompleted(info);
            if (this._parentControl != null)
            {
                this._parentControl.Text = info.Subject;
            }
        }
        #endregion

        #region eventhandler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Configuration_OnSectionTopsUpdatingIntervalChanged(object sender, EventArgs e)
        {
            this._updatingTimer.Stop();
            this._updatingTimer.Interval = Configurations.SectionTopsUpdatingInterval;
            this._updatingTimer.Tick -= new EventHandler(_updatingTimer_Tick);
            this._updatingTimer.Tick += new EventHandler(_updatingTimer_Tick);
            this._updatingTimer.Start();
        }

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
        private void TopControl_OnTopLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linklbl = sender as LinkLabel;
            if (linklbl != null && this.OnTopLinkClicked != null)
            {
                this.OnTopLinkClicked(sender, e);
                e.Link.Visited = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TopControl_OnTopBoardLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linklbl = sender as LinkLabel;
            if (linklbl != null && this.OnTopBoardLinkClicked != null)
            {
                this.OnTopBoardLinkClicked(sender, e);
                e.Link.Visited = true;
            }
        }
        #endregion
    }
}
