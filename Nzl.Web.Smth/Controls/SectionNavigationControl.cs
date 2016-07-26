namespace Nzl.Web.Smth.Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Datas;
    using Page;
    using Util;
    using Utils;

    /// <summary>
    /// 
    /// </summary>
    public partial class SectionNavigationControl : BaseControl
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
        #endregion

        #region Ctor.
        /// <summary>
        /// 
        /// </summary>
        public SectionNavigationControl()
        {
            InitializeComponent();
            this.SetBaseUrl(@"http://m.newsmth.net/section");
        }
        #endregion

        #region override
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.SetUrlInfo(false);
            this.FetchPage();
            this.linklblPrevious.LinkClicked += LinklblPrevious_LinkClicked;
            this.panel.MouseWheel += Panel_MouseWheel;
        }

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
                if (Program.LoggerEnabled)
                {
                    Program.Logger.Error(exp.Message);
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
        /// <param name="info"></param>
        /// <returns></returns>
        protected override string GetUrl(UrlInfo info)
        {
            return info.BaseUrl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wp"></param>
        /// <returns></returns>
        protected override IList<BaseItem> GetItems(WebPage wp)
        {
            return SectionUtil.GetSectionsAndBoards(wp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override Control CreateControl(BaseItem item)
        {
            Board board = item as Board;
            if (board != null)
            {
                BoardControl bc = new BoardControl(board);
                bc.OnLinkClicked += Bc_OnLinkClicked;
                return bc;
            }

            Section section = item as Section;
            if (section != null)
            {
                SectionControl sc = new SectionControl(section);
                sc.OnLinkClicked += Sc_OnLinkClicked;
                return sc;
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
            this.GetInfors(info.WebPage);
        }
        #endregion

        #region eventhandler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sc_OnLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.SetUrlInfo(false);
            this.SetBaseUrl(e.Link.LinkData.ToString());
            this.FetchPage();

            if (this.OnSectionLinkClicked != null)
            {
                this.OnSectionLinkClicked(sender, e);
            }
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
            this.SetBaseUrl(e.Link.LinkData.ToString());
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
                    this.linklblPrevious.Links.Add(0, this.linklblPrevious.Text.Length, @"http://m.newsmth.net" + url);
                }

                ///Section name.
                this.linklblSectionName.Text = CommonUtil.GetMatch(@"<div class=\Wmenu sp\W><a [^>]+>首页</a>\|(?'SectionName'[^<]+)</div>", wp.Html, "SectionName");
                this.linklblSectionName.Links.Clear();
            }
        }
        #endregion
    }
}
