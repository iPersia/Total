namespace Nzl.Smth.Controls.Elements
{
    using System;
    using System.Windows.Forms;
    using Nzl.Web.Util;
    using Nzl.Smth.Controls.Base;
    using Nzl.Smth.Datas;

    /// <summary>
    /// Class.
    /// </summary>
    public partial class MailControl : BaseControl<Mail>
    {
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnMailLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnDeleteLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnUserLinkClicked;

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public static int ControlHeight
        {
            get
            {
                return 45;
            }
        }
        #endregion

        /// <summary>
        /// Ctor.
        /// </summary>
        public MailControl()
        {
            InitializeComponent();
            this.Height = MailControl.ControlHeight;
            this.linklblAuthor.LinkClicked += linklblAuthor_LinkClicked;
            this.linklblTitle.LinkClicked += linklblTitle_LinkClicked;
            this.linklblDelete.LinkClicked += LinklblDelete_LinkClicked;
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mail"></param>
        public override void Initialize(Mail mail)
        {
            base.Initialize(mail);
            if (mail != null)
            {
                this.lblIndex.Text = mail.Index.ToString("00");
                this.InitializeLinkLabel(this.linklblTitle, CommonUtil.ReplaceSpecialChars(mail.Title), mail.Url);

                string deleteUrl = mail.Url;
                string tail = mail.Url.Substring(mail.Url.LastIndexOf("/"));
                deleteUrl = deleteUrl.Replace(tail, "/delete" + tail);

                this.InitializeLinkLabel(this.linklblDelete, deleteUrl);
                this.InitializeLinkLabel(this.linklblAuthor, mail.Author, mail.Author);
                this.lblDT.Text = mail.DateTime;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override System.Drawing.Color ForeColor
        {
            set
            {
                this.linklblTitle.LinkColor = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblAuthor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnUserLinkClicked != null)
            {
                this.OnUserLinkClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblTitle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnMailLinkClicked != null)
            {
                this.OnMailLinkClicked(sender, e);
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
    }
}
