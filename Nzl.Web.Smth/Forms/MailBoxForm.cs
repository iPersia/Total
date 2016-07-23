namespace Nzl.Web.Smth.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using Nzl.Web.Util;
    using Nzl.Web.Page;
    using Nzl.Web.Smth.Datas;
    using Nzl.Web.Smth.Controls;

    /// <summary>
    /// Class.
    /// </summary>
    public partial class MailBoxForm : Form
    {
        #region Variable
        /// <summary>
        /// 
        /// </summary>
        private string _inboxUrl = "http://m.newsmth.net/mail/inbox";

        /// <summary>
        /// 
        /// </summary>
        private string _sentUrl = "http://m.newsmth.net/mail/outbox";

        /// <summary>
        /// 
        /// </summary>
        private string _trashUrl = "http://m.newsmth.net/mail/deleted";

        /// <summary>
        /// 
        /// </summary>
        private int _currentPage;

        /// <summary>
        /// 
        /// </summary>
        private int _totalPage;

        /// <summary>
        /// 
        /// </summary>
        private System.Threading.SynchronizationContext _uiContext = WindowsFormsSynchronizationContext.Current;
        #endregion

        #region Ctor
        /// <summary>
        /// Ctor.
        /// </summary>
        public MailBoxForm()
        {
            InitializeComponent();
            //this.panel.MouseWheel += new MouseEventHandler(panel_MouseWheel);
        }
        #endregion        

        #region Get Information
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        private IList<Mail> GetMailList(string html)
        {
            string pattern = @"(<li>|<li class=\Whla\W>)\s*"
                           + @"(?'Index'[1-9]0?)\.<a href=\W"
                           + @"(?'MailUrl'/mail/[a-z]+/\d+)\W"
                           + @"(?'IsNew'(\Wclass=\Wtop\W)?)>"
                           + @"(?'MailTitle'[^<]+)</a><br\s*/><a href=\W/user/query/"
                           + @"(?'Author'[a-zA-z][a-zA-Z0-9]{1,11})\W>[a-zA-z][a-zA-Z0-9]{1,11}</a>\|"
                           + @"(?'DateTime'[0-9,\-]{10}\s[0-9,\:]{8})</li>";

            MatchCollection mtMailCollection = Nzl.Web.Util.CommonUtil.GetMatchCollection(pattern, html);
            if (mtMailCollection != null)
            {
                IList<Mail> mailList = new List<Mail>();
                foreach (Match mt in mtMailCollection)
                {
                    Mail mail = new Mail(System.Convert.ToInt32(mt.Groups["Index"].Value),
                                         @"http://m.newsmth.net" + mt.Groups["MailUrl"].Value.ToString(),
                                         mt.Groups["MailTitle"].Value.ToString(),
                                         mt.Groups["Author"].Value.ToString(),
                                         mt.Groups["DateTime"].Value.ToString());

                    if (string.IsNullOrEmpty(mt.Groups["IsNew"].Value.ToString()) ==false)
                    {
                        mail.IsNew = true;
                    }

                    mailList.Add(mail);
                }

                return mailList;
            }

            return null;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private void GetPageInfo(object state)
        {
            WebPage page = state as WebPage;
            if (page != null)
            {
                MatchCollection mtCollection = CommonUtil.GetMatchCollection(@"<a class=\Wplant\W>(?'Current'\d+)/(?'Total'\d+)</a>", page.Html);
                if (mtCollection.Count == 2)
                {
                    this._currentPage = System.Convert.ToInt32(mtCollection[0].Groups[1].Value);
                    this._totalPage = System.Convert.ToInt32(mtCollection[0].Groups[2].Value);
                    this.lblPage.Text = this._currentPage.ToString("00") + "/" + this._totalPage.ToString("00");
                }
                else
                {
                    this.lblPage.Text = "?/?";
                }
            }
        }
        #endregion      

        #region Fetch page
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        private void SetBtnEnabled(bool flag)
        {
            this.btnFirst.Enabled = flag;
            this.btnPrev.Enabled = flag;
            this.btnNext.Enabled = flag;
            this.btnLast.Enabled = flag;
            this.panel.Enabled = flag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        private void FetchMorePage(int index)
        {
            if (index < 0)
            {
                return;
            }

            this.bwFetchPage = new System.ComponentModel.BackgroundWorker();
            this.bwFetchPage.DoWork += new System.ComponentModel.DoWorkEventHandler(bwFetchPage_DoWork);
            this.bwFetchPage.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwFetchPage_RunWorkerCompleted2);            
            this.bwFetchPage.RunWorkerAsync(index + "/" + 100 + "/" + this.tcMailBox.SelectedTab.Tag.ToString());
            this.SetBtnEnabled(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        private void FetchNewPage(int index)
        {
            if (index < 0)
            {
                return;
            }

            this.bwFetchPage = new System.ComponentModel.BackgroundWorker();
            this.bwFetchPage.DoWork += new System.ComponentModel.DoWorkEventHandler(bwFetchPage_DoWork);
            this.bwFetchPage.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwFetchPage_RunWorkerCompleted);
            this.bwFetchPage.RunWorkerAsync(index + "/" + 100 + "/" + this.tcMailBox.SelectedTab.Tag.ToString());
            this.SetBtnEnabled(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwFetchPage_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (this._uiContext != null)
                {
                    BackgroundWorker bw = sender as BackgroundWorker;
                    string pageInfo = e.Argument as string;
                    int pos = pageInfo.IndexOf("/");
                    int pageIndex = System.Convert.ToInt32(pageInfo.Substring(0, pos));
                    pageInfo = pageInfo.Substring(pos + 1);
                    pos = pageInfo.IndexOf("/");
                    int totalPage = System.Convert.ToInt32(pageInfo.Substring(0, pos));
                    string mailUrl = pageInfo.Substring(pos + 1);

                    {
                        string targetUrl = mailUrl + "?p=" + pageIndex;
                        WebPage wp = WebPageFactory.CreateWebPage(targetUrl);
                        if (wp != null && wp.IsGood)
                        {
                            this._uiContext.Post(GetPageInfo, wp);
                            e.Result = GetMailList(wp.Html);
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                if (Program.LoggerEnabled)
                {
                    Program.Logger.Error(exp.Message);
                }

#if (DEBUG)
                CommonUtil.ShowMessage(typeof(MailBoxForm), exp.Message);
#endif
                e.Result = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwFetchPage_RunWorkerCompleted2(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                MessageBox.Show("Canceled");
            }
            else
            {
                UpdateMails(e.Result as IList<Mail>, false);
            }

            SetBtnEnabled(true);
            this.panel.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwFetchPage_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                MessageBox.Show("Canceled");
            }
            else
            {
                UpdateMails(e.Result as IList<Mail>);
            }

            SetBtnEnabled(true);
            this.panel.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mailList"></param>
        /// <param name="clear"></param>
        private void UpdateMails(IList<Mail> mailList)
        {
            UpdateMails(mailList, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mailList"></param>
        /// <param name="clear"></param>
        private void UpdateMails(IList<Mail> mailList, bool clearCurrent)
        {
            try
            {
                if (mailList != null && mailList.Count > 0)
                {
                    if (clearCurrent)
                    {
                        this.panel.Controls.Clear();
                    }

                    int accumulateHeight = 0;
                    if (this.panel.Controls.Count > 0)
                    {
                        accumulateHeight = this.panel.Height;
                    }

                    //int width = this.panel.Width - 19;
                    int width = this.panel.Width - 4;
                    bool flag = false;
                    foreach (Mail mail in mailList)
                    {
                        MailControl mc = new MailControl(mail);
                        mc.OnIDLinkClick += new LinkLabelLinkClickedEventHandler(MailControl_OnIDClick);
                        mc.OnMailLinkClick += new LinkLabelLinkClickedEventHandler(MailControl_OnMailLinkClick);
                        mc.Width = width;
                        mc.Top = accumulateHeight;
                        if (flag)
                        {
                            mc.BackColor = Color.White;
                        }

                        flag = !flag;
                        accumulateHeight += mc.Height;
                        this.panel.Controls.Add(mc);
                    }
                }
                else
                {
                    this.Text = "指定的文章不存在或链接错误";
                }
            }
            catch (Exception exp)
            {
                if (Program.LoggerEnabled)
                {
                    Program.Logger.Error(exp.Message);
                }

#if (DEBUG)
                CommonUtil.ShowMessage(this, exp.Message);
#endif
            }
        }
        #endregion        

        #region Event handler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel_MouseWheel(object sender, MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            Panel panel = sender as Panel;
            if (panel != null)
            {
                if (panel.VerticalScroll.Value >= ((panel.VerticalScroll.Maximum - panel.VerticalScroll.LargeChange)))
                {
                    if (this._currentPage < this._totalPage)
                    {
                        FetchMorePage(this._currentPage + 1);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            NewMailForm newMailForm = new NewMailForm();
            newMailForm.StartPosition = FormStartPosition.CenterParent;
            newMailForm.ShowDialog(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            FetchNewPage(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MailControl_OnIDClick(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                UserForm userForm = new UserForm(e.Link.LinkData.ToString());
                userForm.StartPosition = FormStartPosition.CenterParent;
                userForm.ShowDialog(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MailControl_OnMailLinkClick(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                MailDetailForm mailDetailForm = new MailDetailForm(e.Link.LinkData.ToString());
                mailDetailForm.StartPosition = FormStartPosition.CenterParent;
                if (mailDetailForm.ShowDialog(this) == System.Windows.Forms.DialogResult.Yes)
                {
                    FetchNewPage(1);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tcMailBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FetchNewPage(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (this._currentPage == 1)
            {
                return;
            }

            FetchNewPage(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (this._currentPage == 1)
            {
                return;
            }

            FetchNewPage(this._currentPage - 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this._currentPage == this._totalPage)
            {
                return;
            }

            FetchNewPage(this._currentPage + 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLast_Click(object sender, EventArgs e)
        {
            if (this._currentPage == this._totalPage)
            {
                return;
            }

            FetchNewPage(this._totalPage);
        }
        #endregion
    }
}
