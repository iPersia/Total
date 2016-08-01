namespace Nzl.Web.Smth.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Datas;
    using Page;
    using Utils;

    public partial class FavorControl : BaseControl
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
        public FavorControl()
        {
            InitializeComponent();
            this.SetBaseUrl(@"http://m.newsmth.net/favor");
        }
        #endregion

        #region override
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.SetUrlInfo(false);
            this.FetchPage();
            this.panel.MouseWheel += Panel_MouseWheel;
            this.btnRefresh.Left = this.panelUp.Width / 2 - this.btnRefresh.Width / 2;
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
            IList<BaseItem> list = SectionUtil.GetSectionsAndBoards(wp);
            foreach(BaseItem bi in list)
            {
                Board board = bi as Board;
                if (board != null)
                {
                    board.Name = board.Name.Replace("(" + board.Code + ")", "");
                }
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
            Board board = item as Board;
            if (board != null)
            {
                BoardControl bc = new BoardControl(board);
                bc.OnLinkClicked += Bc_OnLinkClicked;
                return bc;
            }

            return base.CreateControl(item);
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
