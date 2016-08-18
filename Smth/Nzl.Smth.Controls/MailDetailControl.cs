//#define DESIGNMODE
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
#if (DESIGNMODE)
    public partial class MailDetailControl : UserControl
#else
    public partial class MailDetailControl : BaseControl<Mail>
#endif
    {
#if (DESIGNMODE)
#else
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
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mail"></param>
        public override void Initialize(Mail mail)
        {
            base.Initialize(mail);
            if (mail != null)
            {
                try
                {
                    this.lblTitle.Text = mail.Title;
                    this.linklblID.Text = mail.Author;

                    ///Delete url.
                    this.linklblDelete.Visible = false;
                    if (string.IsNullOrEmpty(mail.DeleteUrl) == false)
                    {
                        this.linklblDelete.Visible = true;
                        this.linklblDelete.Links.Clear();
                        this.linklblDelete.Links.Add(0, this.linklblDelete.Text.Length, mail.DeleteUrl);
                    }

                    ///ID url.
                    this.linklblID.Visible = false;
                    if (string.IsNullOrEmpty(mail.Author) == false)
                    {
                        this.linklblID.Text = mail.Author;
                        this.linklblID.Visible = true;
                        this.linklblID.Links.Clear();
                        this.linklblID.Links.Add(0, this.linklblID.Text.Length, mail.Author);
                    }

                    ///Reply url.
                    this.linklblReply.Visible = false;
                    if (string.IsNullOrEmpty(mail.ReplyUrl) == false)
                    {
                        this.linklblReply.Visible = true;
                        this.linklblReply.Links.Clear();
                        this.linklblReply.Links.Add(0, this.linklblReply.Text.Length, mail.ReplyUrl);
                        this.linklblReply.Tag = mail;
                    }

                    ///Reply url.
                    this.linklblTransfer.Visible = false;
                    if (string.IsNullOrEmpty(mail.TransferUrl) == false)
                    {
                        this.linklblTransfer.Visible = true;
                        this.linklblTransfer.Links.Clear();
                        this.linklblTransfer.Links.Add(0, this.linklblTransfer.Text.Length, mail.TransferUrl);
                    }

                    ///Content.
                    this.richtxtContent.Clear();
                    if (mail.Content != null)
                    {
                        this.richtxtContent.AppendText(mail.Content);
                    }
                }
                catch (Exception exp)
                {
                    System.Diagnostics.Debug.WriteLine("Initialize(Mail mail)-" + exp.Message + "\n" + exp.StackTrace);
                }
            }

        }

        public override void SetWidth(int width)
        {
            this.richtxtContent.Width = width - (this.Width - this.richtxtContent.Width);
            this.Width = width;
        }

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
#endif
    }
}
