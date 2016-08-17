namespace Nzl.Smth.Containers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Nzl.Smth.Controls;
    using Nzl.Smth.Datas;
    using Nzl.Web.Page;
    using Nzl.Web.Util;
    using Nzl.Smth.Utils;
    using Nzl.Smth.Logger;

    /// <summary>
    /// NOTE:
    /// When clear the linklabel's links, the linklabel will lose focus.
    /// This causes the SectionNavigationControl lost focus, and the 
    /// SectionNavigationControl's contaier will deactive.
    /// So when the linklabel lose focus, the SectionNavigationControl must
    /// be focused.
    /// </summary>
    public partial class SectionNavigationControl : BaseContainer<SectionControl,Section>
    {
        #region event
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnBoardLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnSectionLinkClicked;
        #endregion

        #region variable
        /// <summary>
        /// 
        /// </summary>
        private int _margin = 4;

        /// <summary>
        /// 
        /// </summary>
        private Control _parentControl = null;
        #endregion

        #region Ctor.
        /// <summary>
        /// 
        /// </summary>
        public SectionNavigationControl()
        {
            InitializeComponent();
            this.panel.MouseWheel += Panel_MouseWheel;
            this.linklblPrevious.LostFocus += LinklblPrevious_LostFocus;
            this.Text = "Section navigation";
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
            this.SetBaseUrl(@"http://m.newsmth.net/section");
            this.SetUrlInfo(false);
            this.FetchPage();
            this.linklblPrevious.LinkClicked += LinklblPrevious_LinkClicked;
            this.btnRefresh.Left = this.panelUp.Width / 2 - this.btnRefresh.Width / 2;
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
        /// <param name="info"></param>
        /// <returns></returns>
        protected override string GetUrl(UrlInfo<SectionControl, Section> info)
        {
            return info.BaseUrl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wp"></param>
        /// <returns></returns>
        protected override IList<Section> GetItems(WebPage wp)
        {
            return SectionUtil.GetSections(wp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override SectionControl CreateControl(Section item)
        {
            Section section = item as Section;
            if (section != null)
            {
                SectionControl sc = new SectionControl();
                sc.Initialize(section);
                sc.Name = "sc" + section.Code;
                sc.OnLinkClicked += Sc_OnLinkClicked;
                return sc;
            }

            return base.CreateControl(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        protected override void WorkCompleted(UrlInfo<SectionControl, Section> info)
        {
            base.WorkCompleted(info);
            this.GetInfors(info.WebPage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        protected override void SetControlEnabled(bool flag)
        {
            base.SetControlEnabled(flag);
            this.panel.Enabled = flag;
        }
        #endregion

        #region eventhandler
        /// <summary>
        /// When clear the linklabel's links, the linklabel will lose focus.
        /// This causes the SectionNavigationControl lost focus, and the 
        /// SectionNavigationControl's contaier will deactive.
        /// So when the linklabel lose focus, the SectionNavigationControl must
        /// be focused.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinklblPrevious_LostFocus(object sender, EventArgs e)
        {
            this.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                int panelContainerHeight = this.panelContainer.Height; //panel容器高度
                if (this.panel.Height > panelContainerHeight)
                {
                    int newYPos = this.panel.Location.Y + e.Delta;
                    newYPos = newYPos > this._margin ? this._margin : newYPos;
                    newYPos = newYPos < panelContainerHeight - this.panel.Height - this._margin
                         ? panelContainerHeight - this.panel.Height - this._margin : newYPos;
                    this.panel.Location = new Point(this.panel.Location.X, newYPos);
                }
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
        private void Sc_OnLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.SetBaseUrl(e.Link.LinkData.ToString());
            this.SetUrlInfo(false);            
            this.FetchPage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bc_OnLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnBoardLinkClicked != null)
            {
                this.OnBoardLinkClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinklblPrevious_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.SetUrlInfo(false);
            if (e.Link != null && e.Link.LinkData != null)
            {
                this.SetBaseUrl(e.Link.LinkData.ToString());
                this.FetchPage();
            }
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
        #endregion

        #region private
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wp"></param>
        private void GetInfors(WebPage wp)
        {
            if (wp != null && wp.IsGood)
            {
                ///Previous
                string url = CommonUtil.GetMatch(@"<div class=\Wsec sp\W><a href=\W(?'SectionUrl'.+)\W>上一层</a>", wp.Html, "SectionUrl");
                this.linklblPrevious.Text = "Previous";
                this.linklblPrevious.Links.Clear();
                if (string.IsNullOrEmpty(url) == false)
                {
                    this.linklblPrevious.Links.Add(0, this.linklblPrevious.Text.Length, Configurations.BaseUrl + url);
                }

                ///Section name.
                this.linklblSectionName.Text = CommonUtil.GetMatch(@"<div class=\Wmenu sp\W><a [^>]+>首页</a>\|(?'SectionName'[^<]+)</div>", wp.Html, "SectionName");
                this.linklblSectionName.Links.Clear();
                if (this._parentControl != null)
                {
                    this._parentControl.Text = this.linklblSectionName.Text;
                }
            }
        }
        #endregion        
    }
}
