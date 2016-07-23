namespace Nzl.Web.Pub.MobileNewSmth
{
    using System;
    using System.Windows.Forms;
    using Nzl.Web.Util;

    /// <summary>
    /// Class.
    /// </summary>
    public partial class MailControl : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnMailLinkClick;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnIDLinkClick;

        /// <summary>
        /// Ctor.
        /// </summary>
        public MailControl()
        {
            InitializeComponent();
            this.linklblAuthor.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblAuthor_LinkClicked);
            this.linklblTitle.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblTitle_LinkClicked);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mail"></param>
        public MailControl(Mail mail)
            : this()
        {
            this.lblIndex.Text = mail.Index.ToString("00");
            this.linklblTitle.Text = CommonUtil.ReplaceSpecialChars(mail.Title);
            this.linklblTitle.Links.Add(0, this.linklblTitle.Text.Length, mail.Url);
            this.linklblAuthor.Text = mail.Author;
            this.linklblAuthor.Links.Add(0, mail.Author.Length, mail.Author);
            this.lblDT.Text = mail.DateTime;
            if (mail.IsNew)
            {
                this.linklblTitle.BackColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblAuthor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnIDLinkClick != null)
            {
                this.OnIDLinkClick(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblTitle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnMailLinkClick != null)
            {
                this.OnMailLinkClick(sender, e);
            }
        }
    }
}
