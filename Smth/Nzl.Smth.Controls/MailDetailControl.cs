namespace Nzl.Smth.Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Nzl.Smth.Datas;
    using Nzl.Web.Page;
    using Nzl.Smth.Utils;

    /// <summary>
    /// 
    /// </summary>
    public partial class MailDetailControl : UserControl
    {
        #region events.
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnUserLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnReplyLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnTransferLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnDeleteLinkClicked;
        #endregion

        #region Ctor.
        /// <summary>
        /// 
        /// </summary>
        public MailDetailControl()
        {
            InitializeComponent();
            this.linklblID.LinkClicked += LinklblID_LinkClicked;
            this.linklblReply.LinkClicked += LinklblReply_LinkClicked;
            this.linklblDelete.LinkClicked += LinklblDelete_LinkClicked;
            this.linklblTransfer.LinkClicked += LinklblTransfer_LinkClicked;
            this.scContainer.Visible = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public MailDetailControl(Mail mail)
            : this()
        {
            if (mail != null)
            {
                this.scContainer.Visible = true;
                this.lblTitle.Text = mail.Title;
                this.linklblID.Text = mail.Author;
                this.linklblDelete.Links.Add(0, this.linklblDelete.Text.Length, mail.DeleteUrl);
                this.linklblID.Links.Add(0, this.linklblID.Text.Length, mail.Author);
                this.linklblReply.Links.Add(0, this.linklblReply.Text.Length, mail.ReplyUrl);
                this.linklblReply.Tag = mail;
                this.linklblTransfer.Links.Add(0, this.linklblTransfer.Text.Length, mail.TransferUrl);
                this.richtxtContent.AppendText(mail.Content);
            }
        }
        #endregion

        #region eventhandler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinklblTransfer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnTransferLinkClicked != null)
            {
                this.OnTransferLinkClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinklblDelete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnDeleteLinkClicked != null)
            {
                this.OnDeleteLinkClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinklblReply_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnReplyLinkClicked != null)
            {
                this.OnReplyLinkClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinklblID_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnUserLinkClicked != null)
            {
                this.OnUserLinkClicked(sender, e);
            }
        }
        #endregion
    }
}
