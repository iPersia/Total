namespace Nzl.Smth.Forms
{
    using System;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using Nzl.Smth;
    using Nzl.Smth.Configs;
    using Nzl.Smth.Loaders;
    using Nzl.Smth.Logger;
    using Nzl.Web.Util;
    using Nzl.Web.Page;

    /// <summary>
    /// 
    /// </summary>
    public partial class UserForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private string _userID;

        /// <summary>
        /// 
        /// </summary>
        public UserForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public UserForm(string userID)
            : this()
        {
            this._userID = userID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.Text = "Query User - " + this._userID;
            LoadUserInfor();
            this.txtUser.ContentsResized += TxtUser_ContentsResized;
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadUserInfor()
        {
            if (string.IsNullOrEmpty(this._userID))
            {
                this.txtUser.AppendText("\n\t没有指定用户ID！");
                return;
            }

            this.bgwFetchPage.DoWork += BgwFetchPage_DoWork;
            this.bgwFetchPage.RunWorkerCompleted += BgwFetchPage_RunWorkerCompleted;
            this.bgwFetchPage.RunWorkerAsync(this._userID);            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgwFetchPage_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                WebPage userInforPage = e.Result as WebPage;
                if (userInforPage != null && userInforPage.IsGood)
                {
                    string userInfor = CommonUtil.GetMatch(@"<li>[\w, \W]+</li>", userInforPage.Html);
                    if (string.IsNullOrEmpty(userInfor) == false)
                    {
                        userInfor = userInfor.Replace(@"<li>", "\t");
                        Regex objReg = new System.Text.RegularExpressions.Regex(@"[\n]", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                        userInfor = objReg.Replace(userInfor, "");
                        userInfor = userInfor.Replace(@"</li>", "\n\n");
                        objReg = new System.Text.RegularExpressions.Regex("(<[^>]+?>)", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                        userInfor = objReg.Replace(userInfor, "");
                        userInfor = CommonUtil.ReplaceSpecialChars(userInfor);
                        userInfor.TrimEnd('\n');
                        this.txtUser.AppendText("\n");
                        this.txtUser.AppendText(userInfor);
                        return;
                    }
                }

                this.txtUser.AppendText("\n\t没有查询到用户'" + this._userID + "'的信息！");
            }
            catch (Exception exp)
            {
                if (Logger.Enabled)
                {
                    Logger.Instance.Error(exp.Message + "\n" + exp.StackTrace);
                }

                this.txtUser.AppendText("\n\t没有查询到用户'" + this._userID + "'的信息！");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgwFetchPage_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                e.Result = WebPageFactory.CreateWebPage("http://m.newsmth.net/user/query/" + e.Argument.ToString());
            }
            catch (Exception exp)
            {
                if (Logger.Enabled)
                {
                    Logger.Instance.Error(exp.Message + "\n" + exp.StackTrace);
                }

                e.Result = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtUser_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            RichTextBox rtb = sender as RichTextBox;
            if (rtb != null)
            {
                Size newSize = e.NewRectangle.Size;
                this.Size = new Size(newSize.Width + this.Size.Width - rtb.Width, newSize.Height + this.Size.Height - rtb.Height);
                rtb.Size = e.NewRectangle.Size;                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendMail_Click(object sender, EventArgs e)
        {
            NewMailForm newMailForm = new NewMailForm(this._userID);
            newMailForm.StartPosition = FormStartPosition.CenterParent;
            if (newMailForm.ShowDialog(this) == DialogResult.OK)
            {
                PageLoader pl = new PageLoader(Configuration.SendMailUrl, newMailForm.GetPostString());
                pl.PageLoaded += NewMail_PageLoaded;
                pl.PageFailed += NewMail_PageFailed;
                PageDispatcher.Instance.Add(pl);
            }
        }

        #region NewMail - PageLoaded & PageFailed
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewMail_PageLoaded(object sender, EventArgs e)
        {
            PageLoader pl = sender as PageLoader;
            if (pl != null)
            {
                string html = pl.GetResult() as string;
                string result = CommonUtil.GetMatch(@"<div id=\Wm_main\W><div class=\Wsp hl f\W>(?'Result'\w+)</div>", html, "Result");
                if (result != null && result.Contains("成功"))
                {

                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewMail_PageFailed(object sender, EventArgs e)
        {
        }
        #endregion
    }
}
