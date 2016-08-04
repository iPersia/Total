namespace Nzl.Web.Smth.Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Datas;
    using Page;
    using Utils;

    /// <summary>
    /// 
    /// </summary>
    public partial class MailDetailControl : BaseControl
    {
        /// <summary>
        /// 
        /// </summary>
        private Control _parentControl = null;

        /// <summary>
        /// 
        /// </summary>
        public MailDetailControl()
        {
            InitializeComponent();            
            this.Text = "Mail Detail";           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public MailDetailControl(string url)
            : this()
        {
            this.SetBaseUrl(url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctl"></param>
        public void SetParentControl(Control ctl)
        {
            this._parentControl = ctl;
        }

        #region override
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.SetUrlInfo(false);
            this.FetchPage();
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
            IList<BaseItem> list = new List<BaseItem>();
            list.Add(MailFactory.CreateMailDetail(wp));
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        protected override void WorkCompleted(UrlInfo info)
        {
            if (info.Status == PageStatus.Normal)
            {
                Mail mail = info.Result[0] as Mail;
                if (mail != null)
                {
                    this.Visible = true;

                    this.lblTitle.Text = mail.Title;
                    this.linklblID.Text = mail.Author;
                    this.linklblDelete.Links.Add(0, this.linklblDelete.Text.Length, mail.DeleteUrl);
                    this.linklblID.Links.Add(0, this.linklblID.Text.Length, mail.Author);
                    this.linklblReply.Links.Add(0, this.linklblReply.Text.Length, mail.ReplyUrl);
                    this.linklblTransfer.Links.Add(0, this.linklblTransfer.Text.Length, mail.TransferUrl);
                    this.richtxtContent.AppendText(mail.Content);

                    if (this._parentControl != null)
                    {
                        this._parentControl.Text = mail.Title;
                    }
                }
            }
        }
        #endregion
    }
}
