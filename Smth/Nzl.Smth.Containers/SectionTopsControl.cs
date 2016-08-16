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
    using Nzl.Smth.Datas;
    using Nzl.Smth.Controls;
    using Nzl.Smth.Utils;

    /// <summary>
    /// 
    /// </summary>
    public partial class SectionTopsControl : BaseContainer
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

        /// <summary>
        /// The key is the url.
        /// </summary>
        private Dictionary<string, TopControl> _topControls = new Dictionary<string, TopControl>();
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
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
#if (DEBUG)
            System.Diagnostics.Debug.WriteLine("SectionTopsControl - " + this.Name);
#endif
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
        /// <param name="info"></param>
        /// <returns></returns>
        protected override string GetUrl(UrlInfo info)
        {
            return info.BaseUrl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        protected override void DoWork(UrlInfo info)
        {
            base.DoWork(info);
            info.Subject = SmthUtil.GetSectionName(info.WebPage);
        }

        protected override IList<BaseItem> GetItems(WebPage wp)
        {
            IList<Topic> topics = TopicFactory.CreateTop10Topics(wp);
            IList<BaseItem> list = new List<BaseItem>();
            foreach (Topic topic in topics)
            {
                list.Add(topic);
            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override Control CreateControl(BaseItem item)
        {
            Topic topic = item as Topic;
            if (topic != null)
            {
                if (this._topControls.ContainsKey(topic.Uri))
                {
                    TopControl topControl = this._topControls[topic.Uri];
                    topControl.Update(topic);
                    return topControl;
                }

                TopControl tc = new TopControl();
                tc.Name = "tcTop" + topic.ID;
                tc.Initialize(topic);
                tc.OnTopLinkClicked += new LinkLabelLinkClickedEventHandler(TopControl_OnTopLinkClicked);
                tc.OnTopBoardLinkClicked += TopControl_OnTopBoardLinkClicked;
                tc.Width = this.panelContainer.Width - 4;
                this._topControls.Add(topic.Uri, tc);
                return tc;
            }

            return base.CreateControl(item);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        protected override void WorkCompleted(UrlInfo info)
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
