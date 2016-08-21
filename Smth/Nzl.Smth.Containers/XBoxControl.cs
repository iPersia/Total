namespace Nzl.Smth.Containers
{
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Nzl.Smth.Common;
    using Nzl.Smth.Controls;
    using Nzl.Smth.Datas;    
    using Nzl.Smth.Utils;
    using Nzl.Smth.Logger;
    using Nzl.Web.Page;
    using Nzl.Web.Util;


    /// <summary>
    /// Class.
    /// </summary>
    internal partial class XBoxControl : BaseContainer<MailControl, Mail>
    {
        #region Event
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnMailLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnUserLinkClicked;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler OnNewMailClicked;
        #endregion

        #region Variable
        /// <summary>
        /// 
        /// </summary>
        private MailBoxType _mailBoxType = MailBoxType.Inbox;
        /// <summary>
        /// 
        /// </summary>
        private Control _parentControl = null;
        #endregion

        #region Ctor.
        /// <summary>
        /// Ctor.
        /// </summary>
        XBoxControl()
        {
            InitializeComponent();
            this.Text = "Mailbox";
            this.panel.Size = new Size(this.Width - 10, MailControl.ControlHeight * 10 + 11);
            this.Height = this.panel.Height + 7 + 60;
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        public XBoxControl(MailBoxType type)
            : this()
        {
            this._mailBoxType = type;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctl"></param>
        public void SetParent(Control ctl)
        {
            this._parentControl = ctl;
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        public string Url
        {
            set
            {
                this.SetBaseUrl(value);
            }
        }
        #endregion

        #region override
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Panel GetContainer()
        {
            return this.panel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wp"></param>
        /// <returns></returns>
        protected override IList<Mail> GetItems(WebPage wp)
        {
            return MailFactory.CreateMails(wp);
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="item"></param>
        protected override void InitializeControl(MailControl ctl, Mail item)
        {
            base.InitializeControl(ctl, item);
            if (ctl != null && item != null)
            {
                ctl.Name = "mc" + item.Url;
                ctl.OnMailLinkClicked += Tc_OnMailLinkClicked;
                ctl.OnUserLinkClicked += Tc_OnUserLinkClicked;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ctl"></param>
        protected override void RecylingControl(MailControl ctl)
        {
            base.RecylingControl(ctl);
            if (ctl != null)
            {
                ctl.OnMailLinkClicked -= Tc_OnMailLinkClicked;
                ctl.OnUserLinkClicked -= Tc_OnUserLinkClicked;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        protected override void WorkCompleted(UrlInfo<MailControl, Mail> info)
        {
            base.WorkCompleted(info);
            this.lblPage1.Text = info.Index.ToString().PadLeft(3, '0') + "/" + info.Total.ToString().PadLeft(3, '0');
            this.lblPage2.Text = this.lblPage1.Text;
            if (this._mailBoxType == MailBoxType.Inbox)
            {
                MailStatus.Instance.UpdateStatus(info.WebPage);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="oeFlag"></param>
        protected override void SetControl(MailControl ctl, bool oeFlag)
        {
            base.SetControl(ctl, oeFlag);
            if (ctl.Data != null)
            {
                if (ctl.Data.IsNew)
                {
                    ctl.ForeColor = Color.Red;
                }
                else
                {
                    ctl.ForeColor = Color.Blue;
                }
            }
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        protected override void SetControlEnabled(bool flag)
        {
            base.SetControlEnabled(flag);
            //this.scContainer.Enabled = flag;
            this.btnRefresh.Enabled = true;

            this.panel.Enabled = flag;

            this.btnFirst1.Enabled = flag;
            this.btnGo1.Enabled = flag;
            this.btnLast1.Enabled = flag;
            this.btnNext1.Enabled = flag;
            this.btnPrev1.Enabled = flag;
            this.txtGoTo1.Enabled = flag;

            this.btnFirst2.Enabled = flag;
            this.btnGo2.Enabled = flag;
            this.btnLast2.Enabled = flag;
            this.btnNext2.Enabled = flag;
            this.btnPrev2.Enabled = flag;
            this.txtGoTo2.Enabled = flag;

            //this.btnRefresh.Enabled = flag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thread"></param>
        /// <returns></returns>
        private MailControl CreateMailControl(Mail mail)
        {
            MailControl mc = new MailControl();
            
            return mc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tc_OnUserLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
        private void Tc_OnMailLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnMailLinkClicked != null)
            {
                this.OnMailLinkClicked(sender, e);
                if (e.Link.Tag != null && e.Link.Tag.ToString() == "Success")
                {
                    e.Link.Tag = null;
                    this.SetUrlInfo(false);
                    this.FetchPage();
                }
            }
        }
        #endregion

        #region event handler.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFirst_Click(object sender, EventArgs e)
        {
            this.SetUrlInfo(1, false);
            this.FetchPage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrev_Click(object sender, EventArgs e)
        {
            this.SetUrlInfo(false);
            this.FetchPrevPage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            this.SetUrlInfo(false);
            this.FetchNextPage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLast_Click(object sender, EventArgs e)
        {
            this.SetUrlInfo(false);
            this.FetchLastPage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.SetUrlInfo(false);
            this.FetchPage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                int pageIndex = Int32.MaxValue;
                if (btn.Name == "btnGo1")
                {
                    if (string.IsNullOrEmpty(this.txtGoTo1.Text) == false)
                    {
                        pageIndex = System.Convert.ToInt32(this.txtGoTo1.Text);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(this.txtGoTo2.Text) == false)
                    {
                        pageIndex = System.Convert.ToInt32(this.txtGoTo2.Text);
                    }
                }

                this.SetUrlInfo(pageIndex, false);
                this.FetchPage();
                this.txtGoTo1.Text = this.txtGoTo2.Text = "";
            }
            catch (Exception exp)
            {
                if (Logger.Enabled)
                {
                    Logger.Instance.Error(exp.Message + "\n" + exp.StackTrace);
                }

#if (DEBUG)
                CommonUtil.ShowMessage(typeof(TopicBrowserControl), exp.Message);
#endif
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenInBrower_Click(object sender, EventArgs e)
        {
            CommonUtil.OpenUrl(this.GetCurrentUrl());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            if (this.OnNewMailClicked != null)
            {
                this.OnNewMailClicked(sender, e);
            }
        }
        #endregion

        #region privates.

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    internal enum MailBoxType
    {
        /// <summary>
        /// 
        /// </summary>
        Inbox,

        /// <summary>
        /// 
        /// </summary>
        Outbox,

        /// <summary>
        /// 
        /// </summary>
        Dustbin
    }
}
