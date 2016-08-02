namespace Nzl.Web.Smth.Controls
{
    using System;
    using System.Windows.Forms;
    using Datas;
    using Utils;

    /// <summary>
    /// Class.
    /// </summary>
    public partial class BoardControl : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnLinkClicked;

        /// <summary>
        /// Ctor.
        /// </summary>
        public BoardControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        public BoardControl(Board board)
            : this()
        {
            if (board != null)
            {
                this.linklblBoard.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblBorS_LinkClicked);
                this.linklblBoard.Text = board.Name;
                LinkLabel.Link link = new LinkLabel.Link(0, this.linklblBoard.Text.Length, SmthUtil.GetBoardUrl(board.Code));
                link.Tag = "Board";
                this.linklblBoard.Links.Add(link);
            }
        }

        ///// <summary>
        ///// Ctor.
        ///// </summary>
        //public BoardControl(string url, string title)
        //    : this()
        //{
        //    this.linklblBoard.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblBorS_LinkClicked);
        //    this.linklblBoard.Text = title;
        //    LinkLabel.Link link = new LinkLabel.Link(0, this.linklblBoard.Text.Length, url);
        //    link.Tag = "Board";
        //    this.linklblBoard.Links.Add(link);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblBorS_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnLinkClicked != null)
            {
                OnLinkClicked(sender, e);
                ///e.Link.Visited = true;
            }
        }
    }
}
