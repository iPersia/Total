﻿namespace Nzl.Smth.Controls.Containers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;        
    using Nzl.Smth.Configurations;
    using Nzl.Smth.Controls.Base;
    using Nzl.Smth.Controls.Elements;
    using Nzl.Smth.Datas;
    using Nzl.Smth.Utils;
    using Nzl.Web.Util;
    using Nzl.Web.Page;

    /// <summary>
    /// 
    /// </summary>
    public partial class TopContainer : BaseContainer<TopControl, Top>
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
        TopContainer()
        {
            InitializeComponent();
            Configuration.OnSectionTopsUpdatingIntervalChanged += Configuration_OnSectionTopsUpdatingIntervalChanged;
            this.panelContainer.Size = new Size(this.Width - 10, TopControl.ControlHeight * 10 + 12);
            this.Height = this.panelContainer.Height + 11;
            this.Text = "Section top topic";            
        }
        
        /// <summary>
        /// 
        /// </summary>
        public TopContainer(string url)
            : this()
        {
            this.SetBaseUrl(url);
            this._updatingTimer.Interval = Configuration.SectionTopsUpdatingInterval;
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
        protected override string GetUrl(UrlInfo<TopControl, Top> info)
        {
            return info.BaseUrl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        protected override void DoWork(UrlInfo<TopControl, Top> info)
        {
            base.DoWork(info);
            info.Subject = SmthUtil.GetSectionName(info.WebPage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wp"></param>
        /// <returns></returns>
        protected override IList<Top> GetItems(WebPage wp)
        {
            IList<Top> list = TopFactory.CreateTops(wp);
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
        protected override void InitializeControl(TopControl ctl, Top topic)
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
        protected override void WorkCompleted(UrlInfo<TopControl, Top> info)
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
            this._updatingTimer.Interval = Configuration.SectionTopsUpdatingInterval;
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