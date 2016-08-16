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
        public static event LinkLabelLinkClickedEventHandler OnTopLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public static event LinkLabelLinkClickedEventHandler OnTopBoardLinkClicked;
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

        #region Recyled controls.
        /// <summary>
        /// Recycled the unused controls.
        /// </summary>
        private static Queue<BaseControl> RecycledControls = new Queue<BaseControl>();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Queue<BaseControl> GetRecycledControls()
        {
            return RecycledControls;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static BaseControl GetRecycledControl()
        {
            lock (RecycledControls)
            {
                try
                {
                    if (RecycledControls.Count > 0)
                    {
                        return RecycledControls.Dequeue();
                    }

                    return null;
                }
                catch
                {
                    return null;
                }
            }
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
                BaseControl bc = GetRecycledControl();
                if (bc != null)
                {
                    TopControl recycledopControl = bc as TopControl;
                    if (recycledopControl != null)
                    {
                        recycledopControl.Initialize(topic);
                        return recycledopControl;
                    }
                }

                TopControl tc = new TopControl();
                tc.Name = "tcTop" + topic.ID;
                tc.Initialize(topic);
                tc.Width = this.panelContainer.Width - 4;
                return tc;
            }

            return base.CreateControl(item);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wp"></param>
        /// <returns></returns>
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
            if (linklbl != null && OnTopLinkClicked != null)
            {
                OnTopLinkClicked(sender, e);
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
            if (linklbl != null && OnTopBoardLinkClicked != null)
            {
                OnTopBoardLinkClicked(sender, e);
                e.Link.Visited = true;
            }
        }
        #endregion
    }
}
