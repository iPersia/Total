namespace Nzl.Smth.Controls
{
    using System;
    using System.Windows.Forms;
    using Nzl.Web.Util;
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
            this.linklblAuthor.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblAuthor_LinkClicked);
            this.linklblTitle.LinkClicked += new LinkLabelLinkClickedEventHandler(linklblTitle_LinkClicked);
        }

        public override void Initialize(Mail mail)
        {
            base.Initialize(mail);
            if (mail != null)
            {
                this.lblIndex.Text = mail.Index.ToString("00");
                this.linklblTitle.Text = CommonUtil.ReplaceSpecialChars(mail.Title);
                this.linklblTitle.Links.Clear();
                this.linklblTitle.Links.Add(0, this.linklblTitle.Text.Length, mail.Url);
                this.linklblAuthor.Text = mail.Author;
                this.linklblAuthor.Links.Clear();
                this.linklblAuthor.Links.Add(0, mail.Author.Length, mail.Author);
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
    }
}
