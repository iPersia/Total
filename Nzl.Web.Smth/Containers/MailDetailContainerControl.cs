namespace Nzl.Web.Smth.Containers
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
    using Controls;
    using Utils;

    /// <summary>
    /// 
    /// </summary>
    public partial class MailDetailContainerControl : BaseControl
    {
        #region events.
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnMailAuthorLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnMailReplyLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnMailTransferLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnMailDeleteLinkClicked;
        #endregion

        #region variable
        /// <summary>
        /// 
        /// </summary>
        private Control _parentControl = null;

        /// <summary>
        /// 
        /// </summary>
        private string _mailUrl = null;
        #endregion

        #region Ctor.
        /// <summary>
        /// 
        /// </summary>
        public MailDetailContainerControl()
        {
            InitializeComponent();
            this.Text = "Mail Detail";
        }

        /// <summary>
        /// 
        /// </summary>
        public MailDetailContainerControl(string url)
            : this()
        {
            this.SetBaseUrl(url);
        }

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        public string MailUrl
        {
            get
            {
                return this._mailUrl;
            }

            set
            {
                this._mailUrl = value;
                this.SetBaseUrl(this._mailUrl);
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctl"></param>
        public void SetParentControl(Control ctl)
        {
            this._parentControl = ctl;
        }
        #endregion        

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
        /// <returns></returns>
        protected override Panel GetContainer()
        {
            return this.panelContainer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override Control CreateControl(BaseItem item)
        {
            Mail mail = item as Mail;
            if (mail != null)
            {
                MailDetailControl mdc = new MailDetailControl(mail);
                mdc.OnDeleteLinkClicked += Mdc_OnDeleteLinkClicked;
                mdc.OnReplyLinkClicked += Mdc_OnReplyLinkClicked;
                mdc.OnTransferLinkClicked += Mdc_OnTransferLinkClicked;
                mdc.OnUserLinkClicked += Mdc_OnUserLinkClicked;
                return mdc;
            }

            return base.CreateControl(item);
        }

        #region eventhandler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mdc_OnUserLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnMailAuthorLinkClicked != null)
            {
                this.OnMailAuthorLinkClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mdc_OnTransferLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnMailTransferLinkClicked != null)
            {
                this.OnMailTransferLinkClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mdc_OnReplyLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnMailReplyLinkClicked != null)
            {
                this.OnMailReplyLinkClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mdc_OnDeleteLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnMailDeleteLinkClicked != null)
            {
                this.OnMailDeleteLinkClicked(sender, e);
            }
        }
        #endregion

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
            Mail mail = MailFactory.CreateMailDetail(wp);
            if (mail != null)
            {
                list.Add(mail);
            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        protected override void WorkCompleted(UrlInfo info)
        {
            base.WorkCompleted(info);
            if (this._parentControl != null)
            {
                if (info.Status == PageStatus.Normal && info.Result != null && info.Result.Count > 0)
                {
                    this._parentControl.Text = (info.Result[0] as Mail).Title;
                }
            }
        }
        #endregion
    }
}