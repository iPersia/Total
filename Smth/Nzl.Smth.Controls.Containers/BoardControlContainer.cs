namespace Nzl.Smth.Controls.Containers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Nzl.Smth.Controls.Base;
    using Nzl.Smth.Controls.Elements;
    using Nzl.Smth.Datas;
    using Nzl.Web.Page;
    using Nzl.Smth.Utils;
    using Nzl.Smth.Logger;

    /// <summary>
    /// 
    /// </summary>
    public partial class BoardControlContainer : BaseControlContainer<BoardControl, Board>
    {
        #region event
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnBoardLinkClicked;
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
        public BoardControlContainer()
        {
            InitializeComponent();
            //this.panel.MouseWheel += Panel_MouseWheel;
            this.SetBaseUrl(@"http://m.newsmth.net/favor");
            this.Text = "User favourite board";
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
            this.btnRefresh.Left = this.panelMenu.Width / 2 - this.btnRefresh.Width / 2;
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
        /// <param name="info"></param>
        /// <returns></returns>
        protected override string GetUrl(UrlInfo<BoardControl, Board> info)
        {
            return info.BaseUrl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wp"></param>
        /// <returns></returns>
        protected override IList<Board> GetItems(WebPage wp)
        {
            return SectionUtil.GetFavorBoards(wp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="item"></param>
        protected override void InitializeControl(BoardControl ctl, Board item)
        {
            base.InitializeControl(ctl, item);
            if (ctl != null && item != null)
            {
                ctl.Name = "bc" + item.Code;
                ctl.OnLinkClicked += Bc_OnLinkClicked;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void FetchPageOnMouseWheel()
        {
            //Do nothing.
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ctl"></param>
        protected override void RecylingControl(BoardControl ctl)
        {
            base.RecylingControl(ctl);
            if (ctl != null)
            {
                ctl.OnLinkClicked -= Bc_OnLinkClicked;
            }
        }
        #endregion

        #region eventhandler
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
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.SetUrlInfo(false);
            this.FetchPage();
        }
        #endregion

        #region private
        #endregion
    }
}
