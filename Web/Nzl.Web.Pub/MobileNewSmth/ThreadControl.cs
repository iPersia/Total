namespace Nzl.Web.Pub.MobileNewSmth
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using Nzl.Web.Util;

    /// <summary>
    /// Thread control.
    /// </summary>
    public partial class ThreadControl : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnIDLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnQueryTypeLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnReplyLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnMailLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnTransferLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnEditLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnDeleteLinkClicked;

        /// <summary>
        /// Ctor.
        /// </summary>
        public ThreadControl()
        {
            InitializeComponent();
            this.linklblID.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblID_LinkClicked);
            this.linklblQuryType.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblQuryType_LinkClicked);
            this.linklblReply.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblReply_LinkClicked);
            this.linklblMail.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblMail_LinkClicked);
            this.linklblTransfer.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblTransfer_LinkClicked);
            this.linklblEdit.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblEdit_LinkClicked);
            this.linklblDelete.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblDelete_LinkClicked);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thread"></param>
        public ThreadControl(Thread thread)
            : this()
        {
            if (thread != null)
            {
                //Floor
                this.lblFloor.Visible = true;
                this.lblFloor.Text = thread.Floor;

                //ID
                this.linklblID.Visible = true;
                this.linklblID.Text = thread.ID;
                if (thread.ID != null)
                {
                    this.linklblID.Links.Add(0, this.linklblID.Text.Length, thread.ID);
                }

                //DateTime
                this.lblDateTime.Visible = true;
                this.lblDateTime.Text = thread.DateTime;

                //Query type
                this.linklblQuryType.Text = thread.QueryType;
                this.linklblQuryType.Links.Add(0, this.linklblQuryType.Text.Length, thread.QueryUrl);

                //Reply link
                if (string.IsNullOrEmpty(thread.ReplyUrl) == false)
                {
                    this.linklblReply.Visible = true;
                    this.linklblReply.Links.Add(0, this.linklblReply.Text.Length, thread.ReplyUrl);
                    this.linklblReply.Tag = new Thread(thread);
                }

                //Mail url
                if (string.IsNullOrEmpty(thread.MailUrl) == false)
                {
                    this.linklblMail.Visible = true;
                    this.linklblMail.Links.Add(0, this.linklblMail.Text.Length, thread.MailUrl);
                    this.linklblMail.Tag = new Thread(thread);
                }

                //Transfer url
                if (string.IsNullOrEmpty(thread.TransferUrl) == false)
                {
                    this.linklblTransfer.Visible = true;
                    this.linklblTransfer.Links.Add(0, this.linklblTransfer.Text.Length, thread.TransferUrl);
                    this.linklblTransfer.Tag = new Thread(thread);
                }

                //Edit url
                if (string.IsNullOrEmpty(thread.EditUrl) == false)
                {
                    this.linklblEdit.Visible = true;
                    this.linklblEdit.Links.Add(0, this.linklblEdit.Text.Length, thread.EditUrl);
                    this.linklblEdit.Tag = new Thread(thread);
                }

                //Delete url
                if (string.IsNullOrEmpty(thread.DeleteUrl) == false)
                {
                    this.linklblDelete.Visible = true;
                    this.linklblDelete.Links.Add(0, this.linklblDelete.Text.Length, thread.DeleteUrl);
                    this.linklblDelete.Tag = new Thread(thread);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thread"></param>
        public ThreadControl(Thread thread, bool isShowQueryTypeLink)
            : this(thread)
        {
            this.linklblQuryType.Visible = isShowQueryTypeLink;
        }

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public int AllWidth
        {
            set
            {
                this.Width = value;
                this.richtxtContent.Width = this.Width - 30;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Color BackGroundColor
        {
            set
            {
                this.panel.BackColor = value;
                this.richtxtContent.BackColor = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public RichTextBox TextBox
        {
            get
            {
                return this.richtxtContent;
            }
        }
        #endregion

        #region Event handler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblID_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnIDLinkClicked != null)
            {
                this.OnIDLinkClicked(sender, e);
                e.Link.Visited = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblQuryType_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnQueryTypeLinkClicked != null)
            {
                this.OnQueryTypeLinkClicked(sender, e);
                e.Link.Visited = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblReply_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnReplyLinkClicked != null)
            {
                this.OnReplyLinkClicked(sender, e);
                e.Link.Visited = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblMail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnMailLinkClicked != null)
            {
                this.OnMailLinkClicked(sender, e);
                e.Link.Visited = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblTransfer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnTransferLinkClicked != null)
            {
                this.OnTransferLinkClicked(sender, e);
                e.Link.Visited = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnEditLinkClicked != null)
            {
                this.OnEditLinkClicked(sender, e);
                e.Link.Visited = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblDelete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnDeleteLinkClicked != null)
            {
                this.OnDeleteLinkClicked(sender, e);
                e.Link.Visited = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richtxtContent_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            CommonUtil.OpenUrl(e.LinkText);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richtxtContent_Enter(object sender, EventArgs e)
        {
            this.panel.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblCopy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                this.richtxtContent.SelectAll();
                Clipboard.SetData(DataFormats.Rtf, this.richtxtContent.SelectedRtf);
                e.Link.Visited = true;
            }
        }
        #endregion
    }
}
