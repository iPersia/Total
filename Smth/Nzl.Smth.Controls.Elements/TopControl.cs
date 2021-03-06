﻿//#define DESIGNMODE
namespace Nzl.Smth.Controls.Elements
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Nzl.Smth.Controls.Base;
    using Nzl.Smth.Datas;

    /// <summary>
    /// 
    /// </summary>
#if (DESIGNMODE)
    public partial class TopControl : UserControl
#else
    public partial class TopControl : BaseControl<Top>
#endif
    {
#if (DESIGNMODE)
#else
        #region Event
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnTopLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnTopBoardLinkClicked;
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
        public override void Initialize(Top topic)
        {
            base.Initialize(topic);
            if (topic != null)
            {
                this.lblIndex.Text = topic.Sequence.ToString("00");
                this.linklblTop.Text = "";
                this.linklblTop.Text = topic.Title;
                this.linklblTop.Links.Clear();
                this.linklblTop.Links.Add(0, this.linklblTop.Text.Length, topic.Uri);
                if (topic.Replies > 0)
                {
                    this.lblReplies.Visible = true;
                    this.lblReplies.Text = "(" + topic.Replies + ")";
                    this.lblReplies.Left = this.linklblTop.Left + this.linklblTop.Width + 1;
                }
                else
                {
                    this.lblReplies.Visible = false;
                }

                //string boardName = Boards.Instance.GetBoardName(topic.Board);
                //this.linklblBoard.Text = string.IsNullOrEmpty(boardName) ? topic.Board : boardName;
                this.linklblBoard.Text = topic.Board;
                this.linklblBoard.Links.Clear();
                this.linklblBoard.Links.Add(0, this.linklblBoard.Text.Length, topic.Board);
            }
        }

        public override void SetWidth(int width)
        {
            base.SetWidth(width);
            this.Width = width;
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public override Color BackColor
        {
            set
            {
                this.panel.BackColor = value;
            }
        }

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
            if (this.OnTopLinkClicked != null)
            {
                this.OnTopLinkClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinklblBoard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnTopBoardLinkClicked != null)
            {
                this.OnTopBoardLinkClicked(sender, e);
            }
        }
        #endregion
#endif
    }
}