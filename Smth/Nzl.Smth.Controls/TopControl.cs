namespace Nzl.Smth.Controls
{
    using System;
    using System.Windows.Forms;
    using Nzl.Smth.Common;
    using Nzl.Smth.Datas;

    /// <summary>
    /// 
    /// </summary>
    public partial class TopControl : BaseControl
    {
        #region Event
        /// <summary>
        /// 
        /// </summary>
        public static event LinkLabelLinkClickedEventHandler OnTopLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public static event LinkLabelLinkClickedEventHandler OnTopBoardLinkClicked;
        #endregion

        #region Ctor.
        /// <summary>
        /// 
        /// </summary>
        public TopControl()
        {
            InitializeComponent();
            this.Height = ControlHeight;
            this.linklblTop.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblTop_LinkClicked);
            this.linklblBoard.LinkClicked += LinklblBoard_LinkClicked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topic"></param>
        public override void Initialize(BaseItem item)
        {
            Topic topic = item as Topic;
            if (topic != null)
            {
                this.Tag = topic;
                this.lblIndex.Text = topic.TopSeq.ToString("00");
                this.linklblTop.Text = topic.Title;
                this.linklblTop.Links.Clear();
                this.linklblTop.Links.Add(0, this.linklblTop.Text.Length, topic.Uri);
                if (topic.Replies > 0)
                {
                    this.lblReplies.Visible = true;
                    this.lblReplies.Text = "(" + topic.Replies + ")";
                    this.lblReplies.Left = this.linklblTop.Left + this.linklblTop.Width + 1;
                }

                string boardName = SmthBoards.Instance.GetBoardName(topic.Board);
                this.linklblBoard.Text = string.IsNullOrEmpty(boardName) ? topic.Board : boardName;
                this.linklblBoard.Links.Clear();
                this.linklblBoard.Links.Add(0, this.linklblBoard.Text.Length, topic.Board);
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public static int ControlHeight
        {
            get
            {
                return 40;
            }
        }
        #endregion

        #region Eventhandler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblTop_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (OnTopLinkClicked != null)
            {
                OnTopLinkClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinklblBoard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (OnTopBoardLinkClicked != null)
            {
                OnTopBoardLinkClicked(sender, e);
            }
        }
        #endregion
    }
}