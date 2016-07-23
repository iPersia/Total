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
    using Nzl.Web.Forms.MobileNewSmth.Forms;
    using Nzl.Web.Forms.MobileNewSmth.Utils;

    /// <summary>
    /// 
    /// </summary>
    public partial class SectionTopsControl : BaseControl
    {
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnTopLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        private Control _parentControl = null;

        /// <summary>
        /// 
        /// </summary>
        private Timer _updatingTimer = new Timer();

        /// <summary>
        /// 
        /// </summary>
        SectionTopsControl()
        {
            InitializeComponent();
            this.panelContainer.Size = new Size(this.Width - 10, TopControl.TopControlHeight * 10 + 12);
            this.Height = this.panelContainer.Height + 11;
        }

        /// <summary>
        /// 
        /// </summary>
        public SectionTopsControl(string url)
            : this()
        {
            this.SetBaseUrl(url);
            this._updatingTimer.Interval = 5 * 60 * 1000;//5 minutes.
            this._updatingTimer.Tick += new EventHandler(_updatingTimer_Tick);
            this._updatingTimer.Start();
        }
               
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
        /// <returns></returns>
        protected override Panel GetContainer()
        {
            return this.panelContainer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string GetCurrentUrl()
        {
            return base.GetCurrentUrl();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        protected override void DoWork(UrlInfo info)
        {
            base.DoWork(info);
            info.Subject = SmthUtil.GetSectionTitle(info.WebPage);
            IList<Topic> topics = TopicFactory.GetTop10Topics(info.WebPage);
            IList<BaseItem> list = new List<BaseItem>();
            foreach (Topic topic in topics)
            {
                list.Add(topic);
            }

            info.Result = list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override Control CreateControl(BaseItem item)
        {
            TopControl tc = new TopControl(item as Topic);
            tc.OnTopLinkClicked += new LinkLabelLinkClickedEventHandler(TopControl_OnTopLinkClicked);
            tc.Width = this.panelContainer.Width - 4;
            return tc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        protected override void WorkerCompleted(UrlInfo info)
        {
            base.WorkerCompleted(info);
            if (this._parentControl != null)
            {
                this._parentControl.Text = info.Subject;
                this._parentControl.Refresh();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctl"></param>
        public void SetParent(Control ctl)
        {
            this._parentControl = ctl;
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
            }
        }
    }
}
